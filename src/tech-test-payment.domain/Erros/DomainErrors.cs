using tech_test_payment.domain.Shared;

namespace tech_test_payment.domain.Erros;

public static class DomainErrors
{
    public static class VendaError
    {
        public static readonly Error VendaDevePossuirItems = new Error(
            "Venda.ValidarVenda",
            "A Venda deve possuir pelo menos 1 item.");        
    }
}
