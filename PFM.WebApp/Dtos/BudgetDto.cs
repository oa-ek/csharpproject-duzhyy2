namespace PFM.WebApp.Dtos;

public class BudgetCreateDto
{
    public decimal Amount { get; set; }
    public Guid CategoryId { get; set; }
}

public class BudgetUpdateDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public Guid CategoryId { get; set; }
}