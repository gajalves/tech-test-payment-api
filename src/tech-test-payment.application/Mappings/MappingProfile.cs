using AutoMapper;
using tech_test_payment.application.Dtos;
using tech_test_payment.domain.Entities;

namespace tech_test_payment.application.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Produto, ProdutoDto>().ReverseMap();
		CreateMap<Vendedor, VendedorDto>().ReverseMap();
	}
}
