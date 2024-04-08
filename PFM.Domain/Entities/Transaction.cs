using System.ComponentModel.DataAnnotations.Schema;

namespace PFM.Domain.Entities;

[Table("transactions")]
public class Transaction : IBaseEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Note { get; set; }
    public virtual Bill Bill { get; set; }
    public virtual Category Category { get; set; }
    public virtual TransactionType TransactionType { get; set; }
    public virtual AppUser AppUser { get; set; }
}