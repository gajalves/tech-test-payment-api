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

        public static Error NaoEPossivelAlterarStatus(string statusOrigem, string statusDestino) => new Error(
            "Venda.AlterarStatusDaVenda",
            $"Não é possivel alterar o status da venda de {statusOrigem} para {statusDestino}");
    }

    public static class StatusVendaError
    {       
        public static Error NaoEPossivelAlterarStatus(string statusOrigem, string statusDestino) => new Error(
            "Venda.AlterarStatusDaVenda",
            $"Não é possivel alterar o status da venda de {statusOrigem} para {statusDestino}");

        public static Error StatusInformadoNaoExiste = new Error(
            "VendaService.AtualizarStatusVenda",
            "Status informado não existe!");
    }
}
