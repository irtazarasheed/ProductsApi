using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;

namespace ProductsApi.Data
{
    public class ProductContext: DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
    }
}
