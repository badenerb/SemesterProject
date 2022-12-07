using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProject.Data;
using SemesterProject.Entities;
using SemesterProject;
using Microsoft.EntityFrameworkCore;

namespace SemesterProject.Pages
{
    public class productsModel : PageModel
    {
        private readonly SemesterProjectDbContext _semesterProjectDbcontext;
        public productsModel(SemesterProjectDbContext semesterProjectDbcontext)
        {
            _semesterProjectDbcontext = semesterProjectDbcontext;
        }

        public string CurrentFilter { get; set; } 
        public IList<ProductMaster> allproducts { get; set; }
        //public void OnGet()
        //{
        //    allproducts = _semesterProjectDbcontext.ProductMaster.ToList();
        //}

        //This is the search method that can search the item titles 
        public async Task OnGetAsync(string searchString)
        {
            CurrentFilter = searchString;

            IQueryable<ProductMaster> filterproductsIQ = from p in
                       _semesterProjectDbcontext.ProductMaster
                                                         select p;

            //Passes the string to search for the name
            if (!String.IsNullOrEmpty(searchString))
            {
                filterproductsIQ = 
                    filterproductsIQ.Where(p=>p.ProductName.Contains(searchString));
            }

            allproducts = await filterproductsIQ.AsNoTracking().ToListAsync();
        }

    }
}