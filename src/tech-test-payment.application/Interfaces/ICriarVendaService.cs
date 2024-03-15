using tech_test_payment.application.Dtos;
using tech_test_payment.domain.Shared;

namespace tech_test_payment.application.Interfaces;

public interface ICriarVendaService
{
    Task<Result<VendaDto>> RegistrarVenda(RegistrarVendaDto venda);
}
