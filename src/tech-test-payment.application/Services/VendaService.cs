using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using tech_test_payment.application.Dtos;
using tech_test_payment.application.Interfaces;
using tech_test_payment.domain.Entities;
using tech_test_payment.domain.Enums;
using tech_test_payment.domain.Interfaces;
using tech_test_payment.domain.Shared;

namespace tech_test_payment.application.Services;

public class VendaService : IVendaService
{
    private readonly IMapper _mapper;
    private readonly IVendaRepository _vendaRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IVendedorRepository _vendedorRepository;

    public VendaService(IMapper mapper, IVendaRepository vendaRepository, 
                        IProdutoRepository produtoRepository, IVendedorRepository vendedorRepository)
    {
        _mapper = mapper;
        _vendaRepository = vendaRepository;
        _produtoRepository = produtoRepository;
        _vendedorRepository = vendedorRepository;
    }

    public async Task<Result<VendaDto>> AtualizarStatusVenda(Guid vendaId, AtualizarStatusVendaDto novoStatus)
    {
        if (!EnumHelper.EnumIsDefinedByType(typeof(VendaStatus), novoStatus.Status))
            return Result.Failure<VendaDto>(new Error("VendaService.AtualizarStatusVenda", "Status informado não existe!"));

        var venda = await _vendaRepository.GetByIdAsync(vendaId);

        if (venda == null)
            return Result.Failure<VendaDto>(new Error("VendaService.AtualizarStatusVenda", "Venda não encontrada!"));

        var statusFoiAlterado = venda.AlterarStatusDaVenda((VendaStatus)novoStatus.Status);

        if(statusFoiAlterado.IsFailure)
            return Result.Failure<VendaDto>(statusFoiAlterado.Error);

        var vendaAlterada = await _vendaRepository.UpdateStatusAsync(venda);

        return _mapper.Map<VendaDto>(vendaAlterada);
    }

    public async Task<VendaDto> ObterVendaPorId(Guid vendaId)
    {
        var venda = await _vendaRepository.GetByIdAsync(vendaId);

        return _mapper.Map<VendaDto>(venda);
    }

    public async Task<Result<VendaDto>> RegistrarVenda(RegistrarVendaDto venda)
    {
        var vendedor = await _vendedorRepository.GetByIdAsync(venda.VendedorId);

        if (vendedor == null)
            return Result.Failure<VendaDto>(new Error("VendaService.RegistrarVenda", "Vendedor informado não existe!"));

        var novaVenda = new Venda(vendedor);

        foreach(var item in venda.Items)
        {
            var produto = await _produtoRepository.GetByIdAsync(item.ProdutoId);
            
            if(produto == null)
                return Result.Failure<VendaDto>(new Error("VendaService.RegistrarVenda", $"Produto ({item.ProdutoId}) informado não existe!"));

            var vendaItem = new VendaItem(novaVenda, produto, item.Quantidade, item.Preco);

            novaVenda.AdicionarItemNaVenda(vendaItem);
        }

        var vendaValida = novaVenda.ValidarVenda();        
        if (vendaValida.IsFailure)
            return Result.Failure<VendaDto>(vendaValida.Error);

        var vendaCriada = await _vendaRepository.CreateAsync(novaVenda);

        return _mapper.Map<VendaDto>(vendaCriada);
    }
}
