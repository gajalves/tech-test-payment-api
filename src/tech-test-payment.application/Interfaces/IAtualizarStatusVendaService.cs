using tech_test_payment.application.Dtos;
using tech_test_payment.domain.Shared;

namespace tech_test_payment.application.Interfaces;

public interface IAtualizarStatusVendaService
{
    Task<Result<VendaDto>> AtualizarStatusVenda(Guid vendaId, AtualizarStatusVendaDto novoStatus);
}
