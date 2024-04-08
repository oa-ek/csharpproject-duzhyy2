using System.ComponentModel.DataAnnotations.Schema;

namespace PFM.Domain.Entities;

[Table("types")]
public class TransactionType : IBaseEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    
    public virtual ICollection<Category> Categories { get; set; }
}