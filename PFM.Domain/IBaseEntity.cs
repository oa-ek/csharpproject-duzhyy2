using System.ComponentModel.DataAnnotations;

namespace PFM.Domain;

public interface IBaseEntity<T>
{
    [Key]
    T Id { get; set; }
}