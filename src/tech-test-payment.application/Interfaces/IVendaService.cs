using tech_test_payment.application.Dtos;
using tech_test_payment.domain.Shared;

namespace tech_test_payment.application.Interfaces;

public interface IVendaService
{
    Task<Result<VendaDto>> RegistrarVenda(RegistrarVendaDto venda);
    Task<Result<VendaDto>> AtualizarStatusVenda(Guid vendaId, AtualizarStatusVendaDto novoStatus);
    Task<VendaDto> ObterVendaPorId(Guid vendaId);
}
