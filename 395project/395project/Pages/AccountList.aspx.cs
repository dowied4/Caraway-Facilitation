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
            string query = "Select UserName,Email,EmailConfirmed,PhoneNumber,AccessFailedCount from [AspNetUsers]";

            SqlCommand SqlCommand = new SqlCommand(query, con);
            adapter.SelectCommand = new SqlCommand(query, con);

            //Execture the querey
            SqlDataReader reader = SqlCommand.ExecuteReader();

            //Assign results
            GridView1.DataSource = reader;

            //Bind the data
            GridView1.DataBind();
            
            
            /*
             * 
            //gets data from aspnetusers table
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "Select * from [AspNetUsers]",
                Connection = con
            };
            SqlDataReader rd = cmd.ExecuteReader();

            //creates a new stringbuilder and creates format of table, below is the context of the table
            table.Append("<table border = '1'>");
            table.Append("<tr><th>  Id  </th><th>  Email  </th><th>  Role Id  </th><th>  Stats  </th>");
            table.Append("</tr>");

            //checks if aspusers has rows and starts printing from database
            //needs to be able to use data from other databases as well but since it loops through just 1 currently it wont go asproles database for example
            if(rd.HasRows)
            {
                while(rd.Read())
                {
                    table.Append("<tr>");
                    table.Append("<td>" + rd[0] + "</td>");
                    table.Append("<td>" + rd[1] + "</td>");
                    table.Append("<td>  N/A  </td>");
                    table.Append("<td>  N/A  </td>");
                    table.Append("</tr");
                    table.Append(Environment.NewLine); //this new line may be why footer gets fucked

                }
            }

            
            table.Append("</table");
            
            df.Controls.Add(new Literal { Text = table.ToString() });
            

            rd.Close();


            */

        }
    }
}