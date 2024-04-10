namespace PFM.WebApp.Dtos;

public class DebtCreateDto
{
    public decimal Amount { get; set; }
    public string Note { get; set; }
}

public class DebtUpdateDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; }
}