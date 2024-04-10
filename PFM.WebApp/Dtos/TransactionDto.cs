namespace PFM.WebApp.Dtos;

public class TransactionCreateDto
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Note { get; set; }
    public Guid BillId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid TransactionTypeId { get; set; }
}

public class TransactionUpdateDto
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Note { get; set; }
    public Guid BillId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid TransactionTypeId { get; set; }
}