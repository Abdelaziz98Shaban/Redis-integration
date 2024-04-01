namespace Domain.Models;

public class Item : BaseEntity
{
    public int UomId { get; set; }
    public int Quantity { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public UOM? UOM { get; set; }
}
