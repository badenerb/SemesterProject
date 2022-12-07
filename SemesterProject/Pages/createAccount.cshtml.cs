using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace SemesterProject.Pages
{
    public class createAccountModel : PageModel
    {
        //Creates all of the variables that are taken from the Database.
        public string Message { get; set; } = string.Empty;
        public string MessageColor { get; set; } = "Red";
        [BindProperty]
        public string EmailUsername { get; set; } = String.Empty;
        [BindProperty]
        public string UserPassword { get; set; } = String.Empty;
        [BindProperty]
        public string FirstName { get; set; } = String.Empty;
        [BindProperty]
        public string LastName { get; set; } = String.Empty;
        [BindProperty]
        public string ConfirmUserPassword { get; set; } = String.Empty;
        private readonly IConfiguration configuration;
        public createAccountModel(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public void OnGet()
        {

        }
        //Clears the variables when the user presses the cancel button
        public void OnPostCancel()
        {
            EmailUsername = String.Empty;
            UserPassword = String.Empty;
            LastName = String.Empty;
            ConfirmUserPassword = String.Empty;
            FirstName = String.Empty;
            ModelState.Clear();
        }
        //Validates the input and then that data is passed into the SQL Database
        public IActionResult OnPostCreate()
        {
            if (ModelState.IsValid)
            {
                var strConn = configuration.GetConnectionString("DefaultConnection");

                using SqlConnection sqlConn = new(strConn);

                SqlCommand CreateNewUser = new SqlCommand("spNewUserRecord", sqlConn);
                CreateNewUser.CommandType = CommandType.StoredProcedure;

                CreateNewUser.Parameters.AddWithValue("@FirstName", FirstName);
                CreateNewUser.Parameters.AddWithValue("@LastName", LastName);
                CreateNewUser.Parameters.AddWithValue("@EmailUsername", EmailUsername);
                CreateNewUser.Parameters.AddWithValue("@UserPassword", UserPassword);

                try
                {
                    sqlConn.Open();
                    CreateNewUser.ExecuteNonQuery();
                    string SuccessMessage = "User record successfully created. Please login.";

                    HttpContext.Session.SetString("SuccesfulUserMessage", SuccessMessage);

                    EmailUsername = String.Empty;
                    UserPassword = String.Empty;
                    LastName = String.Empty;
                    ConfirmUserPassword = String.Empty;
                    FirstName = String.Empty;
                    Message = string.Empty;
                    MessageColor = string.Empty;
                    ModelState.Clear();

                    return Redirect("/login");
                }
                catch (Exception exc)
                {
                    Message = exc.Message;
                    return Page();
                }
            }
            else
                return Page();
        }
    }
}
