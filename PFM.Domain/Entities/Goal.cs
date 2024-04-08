using System.ComponentModel.DataAnnotations.Schema;

namespace PFM.Domain.Entities;

[Table("goals")]
public class Goal : IBaseEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Amount { get; set; }
    public string Note { get; set; }
    public DateTime DateEnd { get; set; }
    public virtual Bill Bill { get; set; }
    public virtual AppUser AppUser { get; set; }
}