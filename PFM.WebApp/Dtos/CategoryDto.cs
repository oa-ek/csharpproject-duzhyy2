namespace PFM.WebApp.Dtos;

public class CategoryCreateDto
{
    public string Title { get; set; }
    public Guid TransactionTypeId { get; set; }
}

public class CategoryUpdateDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}