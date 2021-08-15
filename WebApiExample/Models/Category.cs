using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Category Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string PhotoPath { get; set; }
    }
}