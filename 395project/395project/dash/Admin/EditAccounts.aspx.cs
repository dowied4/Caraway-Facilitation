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
using _395project.App_Code;

namespace _395project.dash.Admin
{
    public partial class EditAccounts : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            ChooseMaster choose = new ChooseMaster();
            MasterPageFile = choose.GetMaster();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            String ID = Request.QueryString["ID"];
            head.InnerHtml = "Account: " + ID;
            
            if (!IsPostBack)
            {
                Children.SelectParameters.Add("CurrentUser", ID);
                Facilitators.SelectParameters.Add("CurrentUser", ID);// "sullivanr5@mymacewan.ca");
                loadRole(ID);
            }
            string vars = (string)(Session["register"]);
            if (vars != null)
            {
                string[] myStrings = vars.Split(',');
                FacilitatorFirst.Text = myStrings[1];
                FacilitatorLast.Text = myStrings[2];
            }
        }

        protected void AddChild_Click(object sender, EventArgs e)
        {

            String ID = Request.QueryString["ID"];
            //Check if account already exists before creating it
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var check = manager.FindByName(ID);
            if (check == null)
            {
                ErrorMessage.Text = "There is no account with that email";
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
                cmd.Parameters.AddWithValue("@Email", ID);
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
        protected void loadRole(String ID)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            con.Open();
            string getRole = ("SELECT RoleId FROM AspNetUserRoles WHERE UserId = @CurrentUser");
            SqlCommand sqlRole = new SqlCommand(getRole, con);
            sqlRole.Parameters.AddWithValue("@CurrentUser", ID);// "sullivanr5@mymacewan.ca");
            SqlDataReader roleReader = sqlRole.ExecuteReader();
            roleReader.Read();
            if (roleReader.HasRows)
            {

                String num = roleReader["RoleId"].ToString();

                switch (num)
                {
                    case "1": role.InnerHtml = "Edit Role: (Current Role: Facilitator)"; break;
                    case "2": role.InnerHtml = "Edit Role: (Current Role: Teacher)"; break;
                    case "3": role.InnerHtml = "Edit Role: (Current Role: Admin)"; break;
                    case "4": role.InnerHtml = "Edit Role: (Current Role: SuperUser)"; break;
                }



            }
            con.Close();

        }
        protected void ChangeRole(object sender, EventArgs e)
        {
            String ID = Request.QueryString["ID"];
            int role = RoleDropDown.SelectedIndex + 1 ;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();

            string change = ("IF EXISTS (SELECT UserId FROM AspNetUserRoles WHERE UserId = @CurrentUser)" +
                                " UPDATE AspNetUserRoles SET RoleId = @Roles WHERE UserId = @CurrentUser ELSE" +
                             " INSERT INTO AspNetUserRoles(UserId, RoleId) VALUES (@CurrentUser, @Roles) ");
            SqlCommand rr = new SqlCommand(change, con);
            rr.Parameters.AddWithValue("@Roles", role);
            rr.Parameters.AddWithValue("@CurrentUser", ID);
            rr.ExecuteNonQuery();
            
            
            con.Close();
            ErrorMessage.Text = "Role Sucessfully Changed to: " + role;
            loadRole(ID);
        }

        protected void RemoveChild(object sender, EventArgs e)
        {
            String ID = Request.QueryString["ID"];
            String[] name = ChildrenDropDown.Text.Split(' ');
            String firstName = name[0];
            String lastName = name[1];

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            string removeQuery = ("DELETE FROM Children WHERE Id = @CurrentUser " +
                                        "and FirstName = @FirstName and LastName = @LastName ");
            SqlCommand Remove = new SqlCommand(removeQuery, con);
            Remove.Parameters.AddWithValue("@CurrentUser", ID);// "sullivanr5@mymacewan.ca");
            Remove.Parameters.AddWithValue("@FirstName", firstName);
            Remove.Parameters.AddWithValue("@LastName", lastName);
            SqlDataReader addHoursReader = Remove.ExecuteReader();

            ErrorMessage.Text = "Child Successfully Removed";

            ChildrenDropDown.DataBind();
            con.Close();
        }

        protected void RemoveDB(String ID, String DB, String FirstName, String LastName, String[] Name)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            string changeQuery = ("IF EXISTS (SELECT * FROM " + DB + " WHERE Id = @CurrentUser" +
                                    " AND " + FirstName + " = @FirstName AND " + LastName + "= @LastName)" +
                                   " DELETE FROM "+ DB + " WHERE Id = @CurrentUser AND " + FirstName + " = @FirstName" + 
                                   " AND " + LastName + " = @LastName");
            SqlCommand change = new SqlCommand(changeQuery, con);
            change.Parameters.AddWithValue("@CurrentUser", ID);
            change.Parameters.AddWithValue("@FirstName", Name[0]);
            change.Parameters.AddWithValue("@LastName", Name[1]);
            SqlDataReader exc_remove = change.ExecuteReader();

            con.Close();

        }



        protected void RemoveFacilitator(object sender, EventArgs e)
        {
            String ID = Request.QueryString["ID"];
            
            String[] name = FacilitatorDropDown.Text.Split(' ');
            String firstName = name[0];
            String lastName = name[1];

            RemoveDB(ID, "Stats", "FacilitatorFirstName", "FacilitatorLastName", name);
            RemoveDB(ID, "Calendar", "FacilitatorFirstName", "FacilitatorLastName", name);
            RemoveDB(ID, "Facilitators", "FirstName", "LastName", name);
            ErrorMessage.Text = "Facilitator Successfully Removed";
            FacilitatorDropDown.DataBind();
            
        }

        protected void AddFacilitator_Click(object sender, EventArgs e)
        {
            //Check if account already exists before creating it
            String ID = Request.QueryString["ID"];
            //String ID = "sullivanr5@mymacewan.ca";
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var check = manager.FindByName(ID);
            if (check == null)
            {
                ErrorMessage.Text = "There is no account with that email";
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
                cmd.Parameters.AddWithValue("@Email", ID);
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
                FacilitatorDropDown.DataBind();

            }

        }

     
    }
}