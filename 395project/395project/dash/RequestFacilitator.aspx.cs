using _395project.App_Code;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _395project.Pages
{
    public partial class RequestFacilitator : System.Web.UI.Page
    {
        //Chooses master page based on User Role
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            ChooseMaster choose = new ChooseMaster();
            MasterPageFile = choose.GetMaster(); ;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (!FacilitatorFirst.Text.Contains(" ") && !FacilitatorLast.Text.Contains(" "))
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                string insert = "insert into RequestFacilitator(Email, FacilitatorFirstName, FacilitatorLastName) values (@CurrentUser, @FacilitatorFirst, @FacilitatorLast)";
                string check = "SELECT COUNT(*) FROM Facilitators WHERE (Id = @CurrentUser and FirstName = @FirstName and LastName = @LastName)";
                SqlCommand checkExists = new SqlCommand(check, conn);
                checkExists.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());
                checkExists.Parameters.AddWithValue("@FirstName", FacilitatorFirst.Text);
                checkExists.Parameters.AddWithValue("@LastName", FacilitatorLast.Text);
                int facilitatorExists = (int)checkExists.ExecuteScalar();

                if (facilitatorExists > 0)
                {
                    ErrorMessages.Visible = true;
                    ErrorMessages.ForeColor = System.Drawing.Color.Red;
                    ErrorMessages.Text = "Facilitator already associated with your account!";
                    conn.Close();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(insert, conn);
                    cmd.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());
                    cmd.Parameters.AddWithValue("@FacilitatorFirst", FacilitatorFirst.Text);
                    cmd.Parameters.AddWithValue("@FacilitatorLast", FacilitatorLast.Text);

                    cmd.ExecuteNonQuery();

                    string remove = "delete from RequestFacilitator where Email = '' or FacilitatorFirstName = '' or FacilitatorLastName = ''";
                    SqlCommand rm = new SqlCommand(remove, conn);
                    rm.ExecuteNonQuery();
                    conn.Close();

                    ErrorMessages.Visible = true;
                    ErrorMessages.ForeColor = System.Drawing.Color.Green;
                    ErrorMessages.Text = "Facilitator Request Sent!";
                }

                FacilitatorFirst.Text = string.Empty;
                FacilitatorLast.Text = string.Empty;
            }
            else
            {
                ErrorMessages.Visible = true;
                ErrorMessages.ForeColor = System.Drawing.Color.Red;
                ErrorMessages.Text = "Only one word names please!";
            }

        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            FacilitatorFirst.Text = string.Empty;
            FacilitatorLast.Text = string.Empty;
            ErrorMessages.Visible = false;
        }
    }
}