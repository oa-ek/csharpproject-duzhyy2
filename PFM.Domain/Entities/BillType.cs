using System.ComponentModel.DataAnnotations.Schema;

namespace PFM.Domain.Entities;

[Table("bill_types")]
public class BillType : IBaseEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    
    public virtual ICollection<Bill> Bills { get; set; }
}