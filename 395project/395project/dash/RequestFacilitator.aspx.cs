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
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            conn.Open();
            string insert = "insert into RequestFacilitator(Email, FacilitatorFirstName, FacilitatorLastName) values (@CurrentUser, @FacilitatorFirst, @FacilitatorLast)";
            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());
            cmd.Parameters.AddWithValue("@FacilitatorFirst", FacilitatorFirst.Text);
            cmd.Parameters.AddWithValue("@FacilitatorLast", FacilitatorLast.Text);

            cmd.ExecuteNonQuery();

            string remove = "delete from RequestFacilitator where Email = '' or FacilitatorFirstName = '' or FacilitatorLastName = ''";
            SqlCommand rm = new SqlCommand(remove, conn);
            rm.ExecuteNonQuery();
            conn.Close();

            FacilitatorFirst.Text = string.Empty;
            FacilitatorLast.Text = string.Empty;
            ErrorMessage.Visible = true;
            ErrorMessage.Text = "Request Facilitator Sent";
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            FacilitatorFirst.Text = string.Empty;
            FacilitatorLast.Text = string.Empty;
            ErrorMessage.Visible = false;
        }
    }
}