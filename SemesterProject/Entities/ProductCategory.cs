using System.ComponentModel.DataAnnotations;

namespace SemesterProject.Entities
{
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}
