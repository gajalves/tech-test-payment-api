using tech_test_payment.domain.Entities;
using tech_test_payment.domain.Enums;
using tech_test_payment.domain.Shared;

namespace tech_test_payment.application.Interfaces.AtualizarStatusVendaStrategy;

public interface IAtualizarStatusVenda
{
    public bool Atende(VendaStatus status);
    Result AlterarStatusDaVenda(Venda venda, VendaStatus novoStatus);
}
