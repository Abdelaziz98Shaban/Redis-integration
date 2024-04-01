using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels;

public class ItemVM
{
    public int Id { get; set; }

    [Display(Name = "Unit of measure")]
    public int UomId { get; set; }
    public string? UomName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
