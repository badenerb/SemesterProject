using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProject.Data;
using SemesterProject.Entities;
using SemesterProject;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace SemesterProject.Pages
{
    public class productsCategoryModel : PageModel
    {
        private readonly SemesterProjectDbContext _semesterProjectDbcontext;
        public productsCategoryModel(SemesterProjectDbContext semesterProjectDbcontext)
        {
            _semesterProjectDbcontext = semesterProjectDbcontext;
        }

        public int CurrentCategory { get; set; } 
        public IList<ProductMaster> productsbycategory { get; set; }
        //public void OnGet()
        //{
        //    allproducts = _semesterProjectDbcontext.ProductMaster.ToList();
        //}

        //Method searches for the category in the Product Category SQL 
        public async Task OnGetAsync(int productcategoryID)
        {
            CurrentCategory = productcategoryID;

            IQueryable<ProductMaster> productsbycategoryIQ = from p in
                       _semesterProjectDbcontext.ProductMaster
                                                         select p;

            if (productcategoryID != 0)
            {
                productsbycategoryIQ =
                    productsbycategoryIQ.Where(p=>p.ProductCategory.Equals(productcategoryID));
            }

            productsbycategory = await productsbycategoryIQ.AsNoTracking().ToListAsync();
        }

    }
}