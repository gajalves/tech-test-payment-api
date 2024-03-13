using System.Data;
using tech_test_payment.domain.Enums;
using tech_test_payment.domain.Shared;

namespace tech_test_payment.domain.Entities;

public class Venda : EntidadeBase
{    
    public VendaStatus Status { get; private set; }
    public DateTime DataVenda { get; private set; }

    public int VendedorId { get; private set; }
    public Vendedor Vendedor { get; private set; }

    public List<VendaItem> VendaItems { get; private set; }

    public Venda()
    { }

    public Venda(Vendedor vendedor)
    {
        Status = VendaStatus.AguardandoPagamento;
        DataVenda = DateTime.Now;        
        Vendedor = vendedor;
    }

    public void AdicionarItemNaVenda(VendaItem item)
    {
        if(VendaItems == null)
            VendaItems = new List<VendaItem>();

        VendaItems.Add(item);
    }

    public Result AlterarStatusDaVenda(VendaStatus novoStatus)
    {
        var possiveisTransicoesDeStatus = new Dictionary<VendaStatus, List<VendaStatus>>
        {
            { VendaStatus.AguardandoPagamento, new List<VendaStatus> { VendaStatus.PagamentoAprovado, VendaStatus.Cancelada } },
            { VendaStatus.PagamentoAprovado, new List<VendaStatus> { VendaStatus.EnviadoParaTransportadora, VendaStatus.Cancelada } },
            { VendaStatus.EnviadoParaTransportadora, new List<VendaStatus> { VendaStatus.Entregue } }
        };

        if(possiveisTransicoesDeStatus.TryGetValue(Status, out var estadosPermitidos) && estadosPermitidos.Contains(novoStatus))
        {
            Status = novoStatus;
        }
        else
        {
            return Result.Failure<Venda>(new Error(
                "Venda.AlterarStatusDaVenda",
                $"Não é possivel alterar o status da venda de {Status.GetEnumDescription()} para {novoStatus.GetEnumDescription()}"));
        }

        return Result.Success();
    }

    public Result ValidarVenda()
    {
        if(!VendaItems.Any())
            return Result.Failure<Venda>(new Error(
                "Venda.ValidarVenda",
                $"A Venda deve possuir pelo menos 1 item."));


        return Result.Success();
    }
}
