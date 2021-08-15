using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class ProductCartModel
    {
        public Product Product { get; set; }
        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}