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
        //Chooses master page based on User Role
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            if (User.IsInRole("SuperUser"))
                MasterPageFile = "/Master/Main.master";
            else if (User.IsInRole("Admin"))
                MasterPageFile = "/Master/BoardMember.master";
            else if (User.IsInRole("Teacher"))
                MasterPageFile = "/Master/Teacher.master";
            else if (User.IsInRole("Facilitator"))
                MasterPageFile = "/Master/Facilitator.master";
        }
        protected void AddChild_Click(object sender, EventArgs e)
        {
            //Check if account already exists before creating it
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var check = manager.FindByName(ChildEmail.Text);
            if (check == null)
            {
                ErrorMessage.Text = "There is no account with that email";
                ChildEmail.Text = string.Empty;
                ChildFirst.Text = string.Empty;
                ChildLast.Text = string.Empty;
                Grade.Text = string.Empty;
                Class.Text = string.Empty;
            }
            else
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                string insert = "insert into Children(Id,FirstName, LastName, Grade, Class) values (@Email,@ChildFirst, @ChildLast, @Grade, @Class)";
                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@Email", ChildEmail.Text);
                cmd.Parameters.AddWithValue("@ChildFirst", ChildFirst.Text);
                cmd.Parameters.AddWithValue("@ChildLast", ChildLast.Text);
                cmd.Parameters.AddWithValue("@Grade", Grade.Text);
                cmd.Parameters.AddWithValue("@Class", Class.Text);
                cmd.ExecuteNonQuery();
                //Remove if one of the fields is empty
                string remove = "delete from Children where ID = '' or FirstName = '' or LastName = '' or Grade = '' or Class = ''";
                SqlCommand rm = new SqlCommand(remove, conn);
                rm.ExecuteNonQuery();
                conn.Close();
                ChildFirst.Text = string.Empty;
                ChildLast.Text = string.Empty;
                Grade.Text = string.Empty;
                Class.Text = string.Empty;
                ErrorMessage.Text = "Child Successfully Added";
            }
        }

        protected void AddFacilitator_Click(object sender, EventArgs e)
        {
            //Check if account already exists before creating it
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var check = manager.FindByName(FacilitatorEmail.Text);
            if (check == null)
            {
                ErrorMessage.Text = "There is no account with that email";
                FacilitatorEmail.Text = string.Empty;
                FacilitatorFirst.Text = string.Empty;
                FacilitatorLast.Text = string.Empty;
            }
            else
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                string insert = "BEGIN IF NOT EXISTS (select * from Facilitators as F where F.Id = @Email and F.FirstName = @FacilitatorFirst and F.LastName = @FacilitatorLast)" +
                    " BEGIN insert into Facilitators(Id,FirstName, LastName) values (@Email,@FacilitatorFirst, @FacilitatorLast) END END";
                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@Email", FacilitatorEmail.Text);
                cmd.Parameters.AddWithValue("@FacilitatorFirst", FacilitatorFirst.Text);
                cmd.Parameters.AddWithValue("@FacilitatorLast", FacilitatorLast.Text);
                cmd.ExecuteNonQuery();

                //Remove if one of the fields is empty
                string remove = "delete from Facilitators where ID = '' or FirstName = '' or LastName = ''";
                SqlCommand rm = new SqlCommand(remove, conn);
                rm.ExecuteNonQuery();
                conn.Close();
                FacilitatorFirst.Text = string.Empty;
                FacilitatorLast.Text = string.Empty;
                ErrorMessage.Text = "Facilitator Successfully Added";
            }

        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            //Check if account already exists before creating it
            var check = manager.FindByName(Email.Text);
            if (check != null)
            {
                ErrorMessage.Text = "There is already an account with that email";
                Email.Text = string.Empty;
            }
            else
            {
                var user = new ApplicationUser() { Id = Email.Text, Email = Email.Text, UserName = Email.Text };
                IdentityResult result = manager.Create(user, System.Web.Security.Membership.GeneratePassword(6, 0));
                var currentUser = manager.FindByName(user.UserName);

                var roleresult = manager.AddToRole(currentUser.Id, UserRoleDropDown.SelectedValue);

                if (result.Succeeded)
                {

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    string code = manager.GeneratePasswordResetToken(user.Id);
                    string callbackUrl = IdentityHelper.GetResetPasswordRedirectUrl(code, Request);
                    manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking the following link: " + callbackUrl);

                    /* signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response); */
                    ErrorMessage.Text = "Account Successfully Created";
                    //Set the child and facilitator email fields and clear the new account email field
                    FacilitatorEmail.Text = Email.Text;
                    ChildEmail.Text = Email.Text;
                    Email.Text = string.Empty;
                }
                else
                {
                    ErrorMessage.Text = result.Errors.FirstOrDefault();
                }
            }
        }
    }
}