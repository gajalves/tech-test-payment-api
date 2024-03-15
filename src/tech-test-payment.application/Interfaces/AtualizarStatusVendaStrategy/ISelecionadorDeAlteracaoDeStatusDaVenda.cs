using tech_test_payment.domain.Enums;

namespace tech_test_payment.application.Interfaces.AtualizarStatusVendaStrategy;

public interface ISelecionadorDeAlteracaoDeStatusDaVenda
{
    IAtualizarStatusVenda Selecionar(VendaStatus statusAtual);
}
