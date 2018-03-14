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

            if (!IsPostBack)
            {
                //opens a connection to the server
                SqlConnection precon = new SqlConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
                };

                precon.Open();

                //Query to fetch data
                string query1 = "SELECT Id FROM AspNetUsers";
                SqlCommand SqlCommand1 = new SqlCommand(query1, precon);
                //Execture the querey
                SqlDataReader reader1 = SqlCommand1.ExecuteReader();

                //Assign results
                GridView1.DataSource = reader1;
                //Bind the data
                GridView1.DataBind();
                precon.Close();
            }

            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            //Query to fetch data
            string query = "SELECT Id FROM AspNetUsers";
            SqlCommand SqlCommand = new SqlCommand(query, con);
            SqlDataReader reader = SqlCommand.ExecuteReader();
            GridView1.DataSource = reader;
            GridView1.DataBind();
            con.Close();
        }

        protected void EditButton(object sender, EventArgs e)
        {
            
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            
            String ID;
            ID = gvr.Cells[0].Text;
            Response.Redirect("/dash/Admin/EditAccounts.aspx?ID=" + ID);

        }

        protected void StatButton(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            GridViewRow grid = (GridViewRow)link.NamingContainer;
            String ID;
            ID = grid.Cells[0].Text;
            Response.Redirect("/dash/Admin/AccountStat.aspx?ID=" + ID);
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            //opens a connection to the server
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            //SqlDataAdapter adapter = new SqlDataAdapter();

            con.Open();
            string query = "SELECT U.Id FROM AspNetUsers AS U where U.Id like '%'+@Search+'%'";

            SqlCommand SqlCommand = new SqlCommand(query, con);
            SqlCommand.Parameters.AddWithValue("@Search", SearchBox.Text);

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