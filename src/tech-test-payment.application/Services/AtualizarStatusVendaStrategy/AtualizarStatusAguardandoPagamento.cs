using tech_test_payment.application.Errors;
using tech_test_payment.application.Interfaces.AtualizarStatusVendaStrategy;
using tech_test_payment.domain.Entities;
using tech_test_payment.domain.Enums;
using tech_test_payment.domain.Shared;

namespace tech_test_payment.application.Services.AtualizarStatusVendaStrategy;

public class AtualizarStatusAguardandoPagamento : IAtualizarStatusVenda
{
    private readonly VendaStatus StatusAtual = VendaStatus.AguardandoPagamento;

    public Result AlterarStatusDaVenda(Venda venda, VendaStatus novoStatus)
    {
        var permiteAlterar = novoStatus != StatusAtual &&
                             novoStatus == VendaStatus.PagamentoAprovado ||
                             novoStatus == VendaStatus.Cancelada;

        if (!permiteAlterar)
        {
            return Result.Failure(
                ApplicationErrors.StatusVendaError
                    .NaoEPossivelAlterarStatus(venda.Status.GetEnumDescription(), novoStatus.GetEnumDescription()));
        }
        
        venda.AlterarStatus(novoStatus);

        return Result.Success();
    }

    public bool Atende(VendaStatus status)
    {
        return status.Equals(StatusAtual);
    }
}
