using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProject.Data;
using SemesterProject.Entities;

namespace SemesterProject.Pages
{
    public class categoryModel : PageModel
    {
        private readonly SemesterProjectDbContext _semesterProjectDbcontext;
        public categoryModel(SemesterProjectDbContext semesterProjectDbcontext)
        {
            _semesterProjectDbcontext = semesterProjectDbcontext;
        }
        //Constructors for the List
        
        public string CurrentCategory { get; set; }
        //This creates an empty list 
        public IList<ProductCategory> categories { get; set; }
        //On get runs and creates a list of all of the categories stored in the SQL Database
        public void OnGet()
        {
            categories = _semesterProjectDbcontext.ProductCategory.ToList();
        }
    }
}
