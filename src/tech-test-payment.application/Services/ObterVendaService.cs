using AutoMapper;
using tech_test_payment.application.Dtos;
using tech_test_payment.application.Interfaces;
using tech_test_payment.domain.Interfaces;

namespace tech_test_payment.application.Services;

public class ObterVendaService : IObterVendaService
{
    private readonly IMapper _mapper;
    private readonly IVendaRepository _vendaRepository;    

    public ObterVendaService(IMapper mapper, IVendaRepository vendaRepository)
    {
        _mapper = mapper;
        _vendaRepository = vendaRepository;        
    }    

    public async Task<VendaDto> ObterVendaPorId(Guid vendaId)
    {
        var venda = await _vendaRepository.GetByIdAsync(vendaId);

        return _mapper.Map<VendaDto>(venda);
    }        
}
