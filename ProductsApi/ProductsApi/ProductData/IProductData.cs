using ProductsApi.Models;
using System;
using System.Collections.Generic;

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
