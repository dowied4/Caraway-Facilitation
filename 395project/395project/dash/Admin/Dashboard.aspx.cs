using _395project.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _395project.dash.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        //Chooses master page based on User Role
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            ChooseMaster choose = new ChooseMaster();
            MasterPageFile = choose.GetMaster();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBindFacilitatorAbsence();
        }

        protected void DataBindFacilitatorAbsence()
        {

            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            //Get facilitators that are currently on approved leave
            string upc = "SELECT Email, CONVERT(DATE, StartDate) as 'Start Date', CONVERT(DATE, EndDate) as 'End Date' " +
                "FROM Absence WHERE StartDate <= @CurrentDate " +
                "AND EndDate >= @CurrentDate AND Confirmed = 1";
            SqlCommand getup = new SqlCommand(upc, con);
            getup.Parameters.AddWithValue("@CurrentDate", DateTime.Now);


            //Execture the querey
            SqlDataReader upcompQuery = getup.ExecuteReader();
            GridView1.DataSource = upcompQuery;

            GridView1.DataBind();
            con.Close();
        }
    }
}