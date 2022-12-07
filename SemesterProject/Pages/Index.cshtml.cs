using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace SemesterProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        //Variables that store the loggin message.
        public string Message { get; private set; }
        public string Fullname { get; private set; }
        //It then will fill the variables in the fields to show the message
        public void OnGet()
        {
            var emailaddress = HttpContext.Session.GetString("EmailUsername");
            var firstname = HttpContext.Session.GetString("FirstName");
            var lastname = HttpContext.Session.GetString("LastName");
            var userid = HttpContext.Session.GetString("UserID");

            if (emailaddress != null && emailaddress != "")
            {
                Message = "My account ID is " + userid + " and my username is " + emailaddress;
                Fullname = firstname + " " + lastname;
            }
        }
    }
}