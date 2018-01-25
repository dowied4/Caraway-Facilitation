using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using _395project.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;

namespace _395project.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { Id = Email.Text, Email = Email.Text, UserName = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);

            if (result.Succeeded)
            {


                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                string insert = "insert into facilitators(Id,FirstName, LastName) values (@Email,@Facilitator1First, @Facilitator1Last)";
                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@Email", Email.Text);
                cmd.Parameters.AddWithValue("@Facilitator1First", Facilitator1First.Text);
                cmd.Parameters.AddWithValue("@Facilitator1Last", Facilitator1Last.Text);
                cmd.ExecuteNonQuery();
                string insert2 = "insert into facilitators(Id,FirstName, LastName) values (@Email2,@Facilitator2First, @Facilitator2Last)";
                SqlCommand cmd2 = new SqlCommand(insert2, conn);
                cmd2.Parameters.AddWithValue("@Email2", Email.Text);
                cmd2.Parameters.AddWithValue("@Facilitator2First", Facilitator2First.Text);
                cmd2.Parameters.AddWithValue("@Facilitator2Last", Facilitator2Last.Text);
                cmd2.ExecuteNonQuery();
                //Removes empty facilitators if 1 or 0 are entered
                string remove = "delete from facilitators where FirstName = '' and LastName = '' ";
                SqlCommand removeCommand = new SqlCommand(remove, conn);
                removeCommand.ExecuteNonQuery();
                conn.Close();





                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelper.GetResetPasswordRedirectUrl(code, Request);
                manager.SendEmail(user.Id, "Confirm your Caraway Facilitator account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                // signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                ErrorMessage.Text = "Account successfully created";
                Email.Text = string.Empty;
                Facilitator1First.Text = string.Empty;
                Facilitator1Last.Text = string.Empty;
                Facilitator2First.Text = string.Empty;
                Facilitator2Last.Text = string.Empty;
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}