using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace tech_test_payment.domain.Entities;

public abstract class EntidadeBase
{
    public Guid Id { get; init; }
        
    public EntidadeBase()
    {
        Id = Guid.NewGuid();        
    }
}