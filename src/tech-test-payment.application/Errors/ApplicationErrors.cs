using tech_test_payment.domain.Shared;

namespace tech_test_payment.application.Errors;

public static class ApplicationErrors
{
    public static class VendaError
    {
        public static readonly Error VendedorNaoEncontrado = new Error(
            "VendaService.RegistrarVenda",
            "Vendedor informado não encontrado!");

        public static readonly Error ProdutoNaoEncontrado = new Error(
            "VendaService.RegistrarVenda",
            "Produto informado não encontrado!");

        public static readonly Error VendaNaoEncontradaAtualizarStatus = new Error(
            "VendaService.AtualizarStatusVenda",
            "Venda não encontrada!");
    }
}
