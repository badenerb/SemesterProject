using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Data;
using System.Data.SqlClient;
using System.Data;
using SemesterProject.Entities;



namespace SemesterProject.Pages
{
    [BindProperties]
    public class loginModel : PageModel
    {
        //Creates variables
        public string Message { get; set; } = string.Empty;
        public string MessageColor { get; set; } = "Red";
        public string EmailAddress { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;

        private readonly IConfiguration configuration;
        //Creates an empty list
        public loginModel(IConfiguration _configuration)
        {
            configuration = _configuration;

        }
        //Checks the login of the user
        public void OnGet()
        {
            string tempmesssage = HttpContext.Session.GetString("SuccesfulUserMessage");

            if(tempmesssage != null)
            {
                Message = tempmesssage;
            }
        }
        //Clears the user input when the Cancel button is pressed
        public void OnPostCancel()
        {            
            EmailAddress = String.Empty;
            Password = String.Empty;
            ModelState.Clear();
        }

        public IActionResult OnPostLogin()
        {
            var strConn = configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection sqlConn = new(strConn))
            {
                SqlDataAdapter sqldaValidateUser = new SqlDataAdapter("spValidateUser", sqlConn);
                sqldaValidateUser.SelectCommand.CommandType = CommandType.StoredProcedure;

                sqldaValidateUser.SelectCommand.Parameters.AddWithValue("@EmailUsername", EmailAddress);
                sqldaValidateUser.SelectCommand.Parameters.AddWithValue("@UserPassword", Password);

                try
                {
                    DataSet dsUserRecord = new DataSet();

                    sqldaValidateUser.Fill(dsUserRecord);

                    if (dsUserRecord.Tables[0].Rows.Count == 0)
                    {
                        Message = "Invalid login, please try again.";
                        return Page();
                    }
                    else
                    {
                        AccountMaster currentUser = new AccountMaster();
                        currentUser.UserID = Convert.ToInt32(dsUserRecord.Tables[0].Rows[0]["UserID"]);
                        currentUser.FirstName = dsUserRecord.Tables[0].Rows[0]["FirstName"].ToString();
                        currentUser.LastName = dsUserRecord.Tables[0].Rows[0]["LastName"].ToString();
                        currentUser.EmailUsername = dsUserRecord.Tables[0].Rows[0]["EmailUsername"].ToString();

                        HttpContext.Session.SetString("UserID", currentUser.UserID.ToString());
                        HttpContext.Session.SetString("FirstName", currentUser.FirstName.ToString());
                        HttpContext.Session.SetString("LastName", currentUser.LastName.ToString());
                        HttpContext.Session.SetString("EmailUsername", currentUser.EmailUsername.ToString());

                        EmailAddress = String.Empty;
                        Password = String.Empty;
                        ModelState.Clear();

                        return Redirect("/Index");
                    }
                }
                catch (Exception exc)
                {
                    Message = exc.Message;
                    MessageColor = "Red";

                    return Page();
                }
            }
        }


    }
}
