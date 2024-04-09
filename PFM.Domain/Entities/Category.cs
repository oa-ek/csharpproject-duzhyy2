using System.ComponentModel.DataAnnotations.Schema;

namespace PFM.Domain.Entities;

[Table("categories")]
public class Category : IBaseEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public Guid TransactionTypeId { get; set; }
    public virtual TransactionType TransactionType { get; set; }
    public virtual AppUser AppUser { get; set; }
}