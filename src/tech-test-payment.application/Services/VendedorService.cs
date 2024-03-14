using AutoMapper;
using tech_test_payment.application.Dtos;
using tech_test_payment.application.Interfaces;
using tech_test_payment.domain.Interfaces;

namespace tech_test_payment.application.Services;

public class VendedorService : IVendedorService
{
    private readonly IMapper _mapper;
    private readonly IVendedorRepository _repository;

    public VendedorService(IMapper mapper, IVendedorRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<List<VendedorDto>> ListarTodosOsVendedores()
    {
        var vendedores = await _repository.GetAllAsync();

        return _mapper.Map<List<VendedorDto>>(vendedores);
    }
}
