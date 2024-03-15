using AutoMapper;
using tech_test_payment.application.Dtos;
using tech_test_payment.application.Errors;
using tech_test_payment.application.Interfaces;
using tech_test_payment.domain.Entities;
using tech_test_payment.domain.Enums;
using tech_test_payment.domain.Interfaces;
using tech_test_payment.domain.Shared;

namespace tech_test_payment.application.Services;

public class AtualizarStatusVendaService : IAtualizarStatusVendaService
{
    private readonly IMapper _mapper;
    private readonly IVendaRepository _vendaRepository;    

    public AtualizarStatusVendaService(IMapper mapper, IVendaRepository vendaRepository)
    {
        _mapper = mapper;
        _vendaRepository = vendaRepository;        
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
        if (!EnumHelper.EnumIsDefinedByType(typeof(VendaStatus), novoStatus))
            return Result.Failure<VendaDto>(new Error("VendaService.AtualizarStatusVenda", "Status informado não existe!"));

        var statusFoiAlterado = venda.AlterarStatusDaVenda(novoStatus);
        if (statusFoiAlterado.IsFailure)
            return Result.Failure(statusFoiAlterado.Error);

        return Result.Success();
    }
}
