using Microsoft.AspNetCore.Identity;

namespace PFM.Domain.Entities;

public class AppUser : IdentityUser
{
    public virtual ICollection<Bill> Bills { get; set; }
    public virtual ICollection<Budget> Budgets { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<Debt> Debts { get; set; }
    public virtual ICollection<Goal> Goals { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; }
}