namespace PFM.WebApp.Dtos;

public class BillCreateDto
{
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public Guid TypeId { get; set; }
}

public class BillUpdateDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public Guid TypeId { get; set; }
}