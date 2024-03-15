using tech_test_payment.application.Dtos;

namespace tech_test_payment.application.Interfaces;

public interface IObterVendaService
{        
    Task<VendaDto> ObterVendaPorId(Guid vendaId);
}
