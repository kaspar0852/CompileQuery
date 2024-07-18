

using Microsoft.EntityFrameworkCore;

namespace CompiledQuery;

public class ProductContext : DbContext
{
    
    public DbSet<Product> Products { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=product;Username=postgres;Password=0852");
    }
    

}