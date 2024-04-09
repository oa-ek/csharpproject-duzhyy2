using System.ComponentModel.DataAnnotations.Schema;

namespace PFM.Domain.Entities;

[Table("bills")]
public class Bill : IBaseEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public Guid TypeId { get; set; }
    public virtual BillType Type { get; set; }
    public virtual AppUser AppUser { get; set; }
}