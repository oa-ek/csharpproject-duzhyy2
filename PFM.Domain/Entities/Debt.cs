using System.ComponentModel.DataAnnotations.Schema;

namespace PFM.Domain.Entities;

[Table("debts")]
public class Debt : IBaseEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Amount { get; set; }
    public string Note { get; set; }
    
    public virtual AppUser AppUser { get; set; }
}