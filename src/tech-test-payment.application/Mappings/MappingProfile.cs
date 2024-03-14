using AutoMapper;
using tech_test_payment.application.Dtos;
using tech_test_payment.domain.Entities;
using tech_test_payment.domain.Shared;

namespace tech_test_payment.application.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Produto, ProdutoDto>().ReverseMap();
		CreateMap<Vendedor, VendedorDto>().ReverseMap();

		CreateMap<Venda, VendaDto>()
			.ForMember(dto => dto.Id, m => m.MapFrom(v => v.Id))
			.ForMember(dto => dto.Vendedor, m => m.MapFrom(v => v.Vendedor))						
			.ForMember(dto => dto.Status, m => m.MapFrom(v => v.Status.GetEnumDescription()))
			.ForMember(dto => dto.DataVenda, m => m.MapFrom(v => v.DataVenda))
			.ForMember(dto => dto.Produtos, m => m.MapFrom(v => v.VendaItems.Select(vi => new ItemsVendaDto
			{
				Id = vi.ProdutoId,
				Nome = vi.Produto.Nome,
				Preco = vi.Preco,
				Quantidade = vi.Quantidade
			})));			
	}
}
