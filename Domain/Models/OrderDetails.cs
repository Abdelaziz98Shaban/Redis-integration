namespace Domain.Models;

public class OrderDetails
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public decimal ItemPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public Order? Order { get; set; }
    public Item? Item { get; set; }
}
