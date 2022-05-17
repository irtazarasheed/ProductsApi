using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsApi.Models;

namespace ProductsApi.ProductData
{
    public interface IProductData
    {
        List<Product> GetProducts();

        Product GetProductByValue(Guid id, string name);

        Product AddProduct(Product product);

        void DeleteProduct(Product product);

        Product UpdateProduct(Product product);
    }
}
