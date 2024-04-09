using System.ComponentModel.DataAnnotations.Schema;

namespace PFM.Domain.Entities;

[Table("budgets")]
public class Budget : IBaseEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Amount { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual AppUser AppUser { get; set; }
}