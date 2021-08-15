using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Sale
    {
        public int Id { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}