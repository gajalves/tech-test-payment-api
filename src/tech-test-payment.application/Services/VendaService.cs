﻿using AutoMapper;
using tech_test_payment.application.Dtos;
using tech_test_payment.application.Errors;
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
        var vendedor = await ObterVendedorAsync(venda.VendedorId);

        if (vendedor == null)
            return Result.Failure<VendaDto>(ApplicationErrors.VendaError.VendedorNaoEncontrado);

        var novaVenda = await CriarNovaVenda(vendedor, venda.Items);
        if (novaVenda.IsFailure)
            return Result.Failure<VendaDto>(novaVenda.Error);

        var vendaCriada = await SalvarNovaVendaAsync(novaVenda.Value);

        return _mapper.Map<VendaDto>(vendaCriada);
    }

    private async Task<Vendedor> ObterVendedorAsync(Guid vendedorId)
    {
        return await _vendedorRepository.GetByIdAsync(vendedorId);
    }

    private async Task<Result<Venda>> CriarNovaVenda(Vendedor vendedor, List<ItemsRegistrarVendaDto> items)
    {
        var novaVenda = new Venda(vendedor);

        foreach (var item in items)
        {
            var produto = await ObterProdutoAsync(item.ProdutoId);
            if (produto == null)
                return Result.Failure<Venda>(ApplicationErrors.VendaError.ProdutoNaoEncontrado);

            var vendaItem = new VendaItem(novaVenda, produto, item.Quantidade, item.Preco);
            novaVenda.AdicionarItemNaVenda(vendaItem);
        }

        var vendaValida = novaVenda.ValidarVenda();
        if (vendaValida.IsFailure)
            return Result.Failure<Venda>(vendaValida.Error);

        return Result.Success(novaVenda);
    }

    private async Task<Produto> ObterProdutoAsync(Guid produtoId)
    {
        return await _produtoRepository.GetByIdAsync(produtoId);
    }

    private async Task<Venda> SalvarNovaVendaAsync(Venda novaVenda)
    {
        return await _vendaRepository.CreateAsync(novaVenda);
    }
}
