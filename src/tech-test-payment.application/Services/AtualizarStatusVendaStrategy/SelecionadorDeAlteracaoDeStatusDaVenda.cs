using tech_test_payment.application.Interfaces.AtualizarStatusVendaStrategy;
using tech_test_payment.domain.Enums;

namespace tech_test_payment.application.Services.AtualizarStatusVendaStrategy;

public class SelecionadorDeAlteracaoDeStatusDaVenda : ISelecionadorDeAlteracaoDeStatusDaVenda
{
    private readonly IList<IAtualizarStatusVenda> _atualizadoresStatusVenda;

    public SelecionadorDeAlteracaoDeStatusDaVenda(IList<IAtualizarStatusVenda> atualizadoresStatusVenda)
    {
        _atualizadoresStatusVenda = atualizadoresStatusVenda;
    }

    public IAtualizarStatusVenda Selecionar(VendaStatus status)
    {
        return  _atualizadoresStatusVenda.FirstOrDefault(a => a.Atende(status));        
    }
}
