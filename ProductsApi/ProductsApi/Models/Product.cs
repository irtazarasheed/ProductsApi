using System;
using System.ComponentModel.DataAnnotations;

namespace ProductsApi.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }
}
