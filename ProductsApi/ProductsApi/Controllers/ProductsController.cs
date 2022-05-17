using Microsoft.AspNetCore.Mvc;
using ProductsApi.Models;
using ProductsApi.ProductData;
using System;
using System.Net;

namespace ProductsApi.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private IProductData _productData;

        public ProductsController(IProductData productData)
        {
            _productData = productData;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                return Ok(_productData.GetProducts());
            }
            catch (Exception exception)
            {
                return ValidationProblem(exception.Message, HttpContext.Request.Path, (int)HttpStatusCode.InternalServerError, "Error");
            }
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            try 
            { 
                string error = ValidateData(product);

                if (!string.IsNullOrEmpty(error))
                   return ValidationProblem(error, HttpContext.Request.Path, (int)HttpStatusCode.BadRequest, "Error");

                _productData.AddProduct(product);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + product.Id,
                product);
            }
            catch (Exception exception)
            {
                return ValidationProblem(exception.Message, HttpContext.Request.Path, (int)HttpStatusCode.InternalServerError, "Error");
            }
        }

        [HttpDelete]
        public IActionResult DeleteProduct(Product product)
        {
            try
            {
                var existingProduct = _productData.GetProductByValue(product.Id, string.Empty);

                if (existingProduct != null)
                {
                    _productData.DeleteProduct(existingProduct);
                    return Ok();
                }

                return ValidationProblem($"Product with id: {product.Id} was not found.", HttpContext.Request.Path,
                                            (int)HttpStatusCode.NotFound, "Error");
            }
            catch (Exception exception)
            {
                return ValidationProblem(exception.Message, HttpContext.Request.Path, (int)HttpStatusCode.InternalServerError, "Error");
            }

        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            try
            {
                string error = ValidateData(product);

                if (!string.IsNullOrEmpty(error))
                    return ValidationProblem(error, HttpContext.Request.Path, (int)HttpStatusCode.BadRequest, "Error");

                var existProduct = _productData.UpdateProduct(product);

                if (existProduct != null)
                    return Ok(existProduct);

                return ValidationProblem($"Product with id: {product.Id} was not found.", HttpContext.Request.Path,
                                            (int)HttpStatusCode.NotFound, "Error");
            }
            catch (Exception exception)
            {
                return ValidationProblem(exception.Message, HttpContext.Request.Path, (int)HttpStatusCode.InternalServerError, "Error");
            }

        }

        private string ValidateData(Product product)
        {
            var existingProduct = _productData.GetProductByValue(product.Id, product.Name);

            if ((product.Id == Guid.Empty && string.IsNullOrEmpty(product.Name)) || product.Name == string.Empty)
                return "Product name cannot be empty.";

            if (existingProduct != null)
                return "Product already exists.";

            return string.Empty;
        }
    }
}
