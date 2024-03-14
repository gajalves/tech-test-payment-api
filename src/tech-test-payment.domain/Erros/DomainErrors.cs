using tech_test_payment.domain.Shared;

namespace tech_test_payment.domain.Erros;

public static class DomainErrors
{
    public static class VendaError
    {
        public static readonly Error VendaDevePossuirItems = new Error(
            "Venda.ValidarVenda",
            "A Venda deve possuir pelo menos 1 item.");

        public static Error NaoEPossivelAlterarStatus(string statusOrigem, string statusDestino) => new Error(
            "Venda.AlterarStatusDaVenda",
            $"Não é possivel alterar o status da venda de {statusOrigem} para {statusDestino}");
    }
}
