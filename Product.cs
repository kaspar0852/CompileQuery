using System.ComponentModel.DataAnnotations;

namespace CompiledQuery;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}