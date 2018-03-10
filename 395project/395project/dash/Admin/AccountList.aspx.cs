using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;


namespace _395project.Pages
{
    public partial class AccountList : System.Web.UI.Page
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

        StringBuilder table = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            //opens a connection to the server
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            SqlDataAdapter adapter = new SqlDataAdapter();

            con.Open();

            //Query to fetch data
            string query = "SELECT U.Email, (SELECT COUNT (*) FROM dbo.Facilitators AS F " + 
                "WHERE U.Email = F.Id GROUP BY F.Id) AS NumFacilitators, " +
				"(SELECT COUNT(*) FROM dbo.Children AS C WHERE U.Email = C.Id GROUP BY C.Id) AS NumChildren, " +
				"(SELECT SUM(WeeklyHours) FROM dbo.Stats AS S WHERE U.Email = S.Id AND S.Month = @Month GROUP BY S.Id) AS MonthlyHours, " +
				"(SELECT SUM(WeeklyHours) FROM dbo.Stats AS S WHERE U.Email = S.Id AND S.Year = @Year GROUP BY S.Id) AS YearlyHours " +
                "FROM dbo.AspNetUsers AS U";

            SqlCommand SqlCommand = new SqlCommand(query, con);
            SqlCommand.Parameters.AddWithValue("@Month", DateTime.Now.Month);
            SqlCommand.Parameters.AddWithValue("@Year", DateTime.Now.Year);
            adapter.SelectCommand = new SqlCommand(query, con);

            //Execture the querey
            SqlDataReader reader = SqlCommand.ExecuteReader();

            //Assign results
            GridView1.DataSource = reader;

            //Bind the data
            GridView1.DataBind();

            con.Close();
            
            

        }

        protected void EditButton(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            String ID;
            ID = gvr.Cells[0].Text;
            Response.Redirect("/dash/amin/EditAccount.aspx?ID=" + ID);

        }

            protected void Search_Click(object sender, EventArgs e)
        {
            //opens a connection to the server
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            SqlDataAdapter adapter = new SqlDataAdapter();

            con.Open();
            string query = "SELECT U.Email, (SELECT COUNT (*) FROM dbo.Facilitators AS F " +
               "WHERE U.Email = F.Id GROUP BY F.Id) AS NumFacilitators, " +
               "(SELECT COUNT(*) FROM dbo.Children AS C WHERE U.Email = C.Id GROUP BY C.Id) AS NumChildren, " +
               "(SELECT SUM(WeeklyHours) FROM dbo.Stats AS S WHERE U.Email = S.Id AND S.Month = @Month GROUP BY S.Id) AS MonthlyHours, " +
               "(SELECT SUM(WeeklyHours) FROM dbo.Stats AS S WHERE U.Email = S.Id AND S.Year = @Year GROUP BY S.Id) AS YearlyHours " +
               "FROM dbo.AspNetUsers AS U where U.Email like '%'+@Search+'%'";

            SqlCommand SqlCommand = new SqlCommand(query, con);
            SqlCommand.Parameters.AddWithValue("@Month", DateTime.Now.Month);
            SqlCommand.Parameters.AddWithValue("@Year", DateTime.Now.Year);
            SqlCommand.Parameters.AddWithValue("@Search", SearchBox.Text);
            adapter.SelectCommand = new SqlCommand(query, con);

            //Execture the querey
            SqlDataReader reader = SqlCommand.ExecuteReader();

            //Assign results
            GridView1.DataSource = reader;

            //Bind the data
            GridView1.DataBind();

            //Clear the textbox
            SearchBox.Text = String.Empty;

        }

    }
}