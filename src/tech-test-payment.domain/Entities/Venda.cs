using tech_test_payment.domain.Enums;
using tech_test_payment.domain.Erros;
using tech_test_payment.domain.Shared;

namespace tech_test_payment.domain.Entities;

public class Venda : EntidadeBase
{    
    public VendaStatus Status { get; private set; }
    public DateTime DataVenda { get; private set; }

    public Guid VendedorId { get; private set; }
    public Vendedor Vendedor { get; private set; }

    public List<VendaItem> VendaItems { get; private set; } = new();

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

    public void AlterarStatus(VendaStatus novoStatus)
    {
        Status = novoStatus;
    }

    public Result ValidarVenda()
    {
        if(!VendaItems.Any())
            return Result.Failure(DomainErrors.VendaError.VendaDevePossuirItems);

        return Result.Success();
    }
}
