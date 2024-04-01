namespace Domain.Models;

public class Order : BaseEntity
{
    public string CustomerId { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime CloseDate { get; set; }
    public Status Status { get; set; }
    public string? DiscountPromoCode { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal TotalPrice { get; set; }
    public string CurrencyCode { get; set; }
    public double  ExchangeRate{ get; set; }
    public decimal ForignPrice { get; set; }
    public ICollection<OrderDetails> OrderDetails { get; set; }
    public ApplicationUser? Customer { get; set; }

    //TODO CustomerModel
}

public enum Status
{
    Open,
    Close,
}