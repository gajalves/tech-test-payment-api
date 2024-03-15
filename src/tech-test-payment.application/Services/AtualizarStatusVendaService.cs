using AutoMapper;
using tech_test_payment.application.Dtos;
using tech_test_payment.application.Errors;
using tech_test_payment.application.Interfaces;
using tech_test_payment.application.Interfaces.AtualizarStatusVendaStrategy;
using tech_test_payment.domain.Entities;
using tech_test_payment.domain.Enums;
using tech_test_payment.domain.Interfaces;
using tech_test_payment.domain.Shared;

namespace tech_test_payment.application.Services;

public class AtualizarStatusVendaService : IAtualizarStatusVendaService
{
    private readonly IMapper _mapper;
    private readonly IVendaRepository _vendaRepository;
    private readonly ISelecionadorDeAlteracaoDeStatusDaVenda _selecionadorDeAlteracaoDeStatusDaVenda;

    public AtualizarStatusVendaService(IMapper mapper, IVendaRepository vendaRepository, 
                                       ISelecionadorDeAlteracaoDeStatusDaVenda selecionadorDeAlteracaoDeStatusDaVenda)
    {
        _mapper = mapper;
        _vendaRepository = vendaRepository;
        _selecionadorDeAlteracaoDeStatusDaVenda = selecionadorDeAlteracaoDeStatusDaVenda;
    }

    public async Task<Result<VendaDto>> AtualizarStatusVenda(Guid vendaId, AtualizarStatusVendaDto novoStatus)
    {        
        var venda = await ObterVendaPorIdAsync(vendaId);
        if (venda == null)
            return Result.Failure<VendaDto>(ApplicationErrors.VendaError.VendaNaoEncontradaAtualizarStatus);

        var statusFoiAlterado = AlterarStatusVenda(venda, (VendaStatus)novoStatus.Status);
        if (statusFoiAlterado.IsFailure)
            return Result.Failure<VendaDto>(statusFoiAlterado.Error);

        var vendaAlterada = await SalvarVendaAsync(venda);
        return _mapper.Map<VendaDto>(vendaAlterada);
    }

    private async Task<Venda> ObterVendaPorIdAsync(Guid vendaId)
    {
        return await _vendaRepository.GetByIdAsync(vendaId);
    }

    private async Task<Venda> SalvarVendaAsync(Venda venda)
    {
        return await _vendaRepository.UpdateStatusAsync(venda);
    }

    private Result AlterarStatusVenda(Venda venda, VendaStatus novoStatus)
    {
        var statusInformadoExiste = StatusInformadoExiste(novoStatus);
        if(!statusInformadoExiste)
            return Result.Failure<VendaDto>(ApplicationErrors.StatusVendaError.StatusInformadoNaoExiste);

        var atualizador = _selecionadorDeAlteracaoDeStatusDaVenda.Selecionar(venda.Status);
        if (atualizador == null)
            return Result.Failure(
                ApplicationErrors.StatusVendaError
                    .NaoEPossivelAlterarStatus(venda.Status.GetEnumDescription(), novoStatus.GetEnumDescription()));

        var statusFoiAlterado = atualizador.AlterarStatusDaVenda(venda, novoStatus);   
        if (statusFoiAlterado.IsFailure)
            return Result.Failure(statusFoiAlterado.Error);

        return Result.Success();
    }

    private bool StatusInformadoExiste(VendaStatus novoStatus)
    {
        return EnumHelper.EnumIsDefinedByType(typeof(VendaStatus), novoStatus);            
    }
}
