using Microsoft.EntityFrameworkCore;
using ProductsApi.Data;
using ProductsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductsApi.ProductData
{
    public class ProductDataSql : IProductData
    {
        private ProductContext _productContext;

        public ProductDataSql(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public Product AddProduct(Product product)
        {
            product.Id = Guid.NewGuid();
            _productContext.Products.Add(product);
            try
            {
                _productContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
            return product;
        }

        public void DeleteProduct(Product product)
        {
            _productContext.Remove(product);
            try
            {
                _productContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
        }

        public List<Product> GetProducts()
        {
            return _productContext.Products.ToList();
        }

        public Product UpdateProduct(Product product)
        {
            var existingProduct = GetProductByValue(product.Id, string.Empty);

            if (existingProduct != null)
            {
                if (product.Name != null) 
                    existingProduct.Name = product.Name;
                if (product.Description != null)
                    existingProduct.Description = product.Description;
                _productContext.Products.Update(existingProduct);
                
                try
                {
                    _productContext.SaveChanges();
                }
                catch (DbUpdateException exception)
                {
                    throw new Exception(exception.InnerException.Message);
                }
            }
            return existingProduct;
        }

        public Product GetProductByValue(Guid id, string name)
        {
            Product product = new Product();
            product = (name == string.Empty)
                        ? _productContext.Products.SingleOrDefault(x => x.Id == id)
                        : _productContext.Products.SingleOrDefault(x => x.Id != id && string.Equals(x.Name, name));

            return product;
        }
    }
}
