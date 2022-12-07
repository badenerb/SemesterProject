using System.ComponentModel.DataAnnotations;

namespace SemesterProject.Entities
{
    public class ProductMaster
    {
        [Key]
        public int ProductMasterID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDesc { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }   
        public string ImagePath { get; set; } = string.Empty;
        public int ProductCategory { get; set; } 

    }
}