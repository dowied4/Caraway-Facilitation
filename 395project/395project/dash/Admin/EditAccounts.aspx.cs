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
        
        protected void ChildLast_Rename(object sender, EventArgs e)
        {
            String ID = Request.QueryString["ID"];
            String Name = ChildLast.Text;
            String[] FullName = EditChildrenDropDown.Text.Split(' ');
            UpdateVariableDB(ID, "Children", "LastName", Name, FullName);
            ErrorMessage.Text = "Sucessfully Renamed: " + EditChildrenDropDown.Text + " to: " + Name;
            EditChildrenDropDown.DataBind();
            ChildrenDropDown.DataBind();
            ChildLast.Text = "";
        }

        protected void ChildFirst_Rename(object sender, EventArgs e)
        {
            String ID = Request.QueryString["ID"];
            String Name = ChildFirst.Text;
            String[] FullName = EditChildrenDropDown.Text.Split(' ');
            UpdateVariableDB(ID, "Children", "FirstName", Name, FullName);
            ErrorMessage.Text = "Sucessfully Renamed: " + EditChildrenDropDown.Text + " to: " + Name;
            EditChildrenDropDown.DataBind();
            ChildrenDropDown.DataBind();
            ChildFirst.Text = "";
        }
        
        protected void FacilitatorFirst_Rename(object sender, EventArgs e)
        {
            String ID = Request.QueryString["ID"];
            String Name = FacilitatorFirst.Text;
            String[] FullName = EditFacilitatorDropDown.Text.Split(' ');
            UpdateVariableDB(ID, "Stats", "FacilitatorFirstName", Name, FullName);
            UpdateVariableDB(ID, "Facilitators", "FirstName", Name, FullName);
            ErrorMessage.Text = "Sucessfully Renamed: " + EditFacilitatorDropDown.Text + " to: " + Name;
            EditFacilitatorDropDown.DataBind();
            FacilitatorDropDown.DataBind();
            FacilitatorFirst.Text = "";
        }
        protected void FacilitatorLast_Rename(object sender, EventArgs e)
        {
            String ID = Request.QueryString["ID"];
            String Name = FacilitatorLast.Text;
            String[] FullName = EditFacilitatorDropDown.Text.Split(' ');
            UpdateVariableDB(ID, "Stats", "FacilitatorLastName", Name, FullName);
            UpdateVariableDB(ID, "Facilitators", "LastName", Name, FullName);
            ErrorMessage.Text = "Sucessfully Renamed: " + EditFacilitatorDropDown.Text + " to: " + Name;
            EditFacilitatorDropDown.DataBind();
            FacilitatorDropDown.DataBind();
            FacilitatorLast.Text = "";
        }
        
        protected void UpdateVariableDB(String ID, String DB, String DBValue, String Update, String[] Name)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();

            if(DB == "Stats")
            {
                string change = ("IF EXISTS (SELECT Id FROM " + DB + " WHERE Id = @CurrentUser AND FacilitatorFirstName = @FirstName AND FacilitatorLastName = @LastName)" +
                                    " UPDATE " + DB + " SET " + DBValue + " = @Update WHERE Id = @CurrentUser AND FacilitatorFirstName = @FirstName AND FacilitatorLastName = @LastName");
                SqlCommand rr = new SqlCommand(change, con);
                rr.Parameters.AddWithValue("@FirstName", Name[0]);
                rr.Parameters.AddWithValue("@LastName", Name[1]);
                rr.Parameters.AddWithValue("@Update", Update);
                rr.Parameters.AddWithValue("@CurrentUser", ID);
                rr.ExecuteNonQuery();
            } else { 
            
                string change = ("IF EXISTS (SELECT Id FROM " + DB + " WHERE Id = @CurrentUser AND FirstName = @FirstName AND LastName = @LastName)" +
                                    " UPDATE " + DB + " SET " + DBValue + " = @Update WHERE Id = @CurrentUser AND FirstName = @FirstName AND LastName = @LastName");
                SqlCommand rr = new SqlCommand(change, con);
                rr.Parameters.AddWithValue("@FirstName", Name[0]);
                rr.Parameters.AddWithValue("@LastName", Name[1]);
                rr.Parameters.AddWithValue("@Update", Update);
                rr.Parameters.AddWithValue("@CurrentUser", ID);
                rr.ExecuteNonQuery();

            }
            con.Close();

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

        protected void UpdateDB(String ID, String DB, String FirstName, String LastName, String[] Name)
        {
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
            EditChildrenDropDown.DataBind();
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
            
            RemoveDB(ID, "Facilitators", "FirstName", "LastName", name);
            ErrorMessage.Text = "Facilitator Successfully Removed";
            FacilitatorDropDown.DataBind();
            EditFacilitatorDropDown.DataBind();
            
        }

        protected void ChangeClass(object sender, EventArgs e)
        {
            String value = GradeDropDown.SelectedItem.Value;

            switch(value)
            {
                case "K": ClassDropDown.Enabled = true; break;
                case "1": ClassDropDown.Enabled = true; break;
                case "2": ClassDropDown.Enabled = true; break;
                default: ClassDropDown.Enabled = false; ClassDropDown.ToolTip = "Disabled"; break;
            }

           
        }
        protected void UpdateGrade(object sender, EventArgs e)
        {
            String ID = Request.QueryString["ID"];
            String[] ChildName = EditChildrenDropDown.SelectedItem.Text.Split(' ');
            String value = GradeDropDown.SelectedItem.Value;
            String change;
            int num = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();

            if (value == "K" || value == "1" || value == "2")
            {
                change = ("IF EXISTS (SELECT * FROM Children WHERE Id = @CurrentUser AND FirstName = @FirstName AND LastName = @LastName)" +
                    " UPDATE Children SET Grade = @Grade, Class = @Class WHERE Id = @CurrentUser" +
                    " AND FirstName = @FirstName AND LastName = @LastName");
                num++;
            }
            else
            {
                change = ("IF EXISTS (SELECT * FROM Children WHERE Id = @CurrentUser AND FirstName = @FirstName AND LastName = @LastName)" +
                                " UPDATE Children SET Grade = @Grade WHERE Id = @CurrentUser" +
                                " AND FirstName = @FirstName AND LastName = @LastName");
                
            }
            SqlCommand rr = new SqlCommand(change, con);
            rr.Parameters.AddWithValue("@FirstName", ChildName[0]);
            rr.Parameters.AddWithValue("@LastName", ChildName[1]);
            rr.Parameters.AddWithValue("@Grade", GradeDropDown.SelectedItem.Value);

            if(num == 1)
            {
                rr.Parameters.AddWithValue("@Class", ClassDropDown.SelectedItem.Text);
            }
            
            rr.Parameters.AddWithValue("@CurrentUser", ID);
            rr.ExecuteNonQuery();
            con.Close();
            ErrorMessage.Text = "Child Grade Sucessfully Changed to: " + GradeDropDown.SelectedItem.Text;
            GradeDropDown.SelectedIndex = 0;
            ClassDropDown.SelectedIndex = 0;
            ClassDropDown.Enabled = true;
        }
        }
}