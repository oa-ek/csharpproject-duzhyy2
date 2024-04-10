namespace PFM.WebApp.Dtos;

public class GoalCreateDto
{
    public decimal Amount { get; set; }
    public string Note { get; set; }
    public DateTime DateEnd { get; set; }
    public Guid BillId { get; set; }
}

public class GoalUpdateDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; }
    public DateTime DateEnd { get; set; }
    public Guid BillId { get; set; }
}