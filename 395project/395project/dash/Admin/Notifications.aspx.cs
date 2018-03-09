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
    public partial class Notifications : System.Web.UI.Page
    {
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
            string query = "Select * from [AspNetUsers]";

            SqlCommand SqlCommand = new SqlCommand(query, con);
            adapter.SelectCommand = new SqlCommand(query, con);

            //Execture the querey
            SqlDataReader reader = SqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Label txtLabel1 = new Label(); //Generating a label
                txtLabel1.Text = reader.GetValue(1).ToString() + " has requested a (type of request here). Testing concatenating. " + reader.GetValue(2).ToString() + " <- Email Confirmed";
                txtLabel1.Attributes.Add("class", "input-header");
                Button confirm = new Button();
                Button clear = new Button();

                //Confirm
                confirm.CommandArgument = reader.GetValue(1).ToString() + "," + reader.GetValue(2).ToString() + "," + reader.GetValue(3).ToString();
                confirm.Attributes.Add("runat", "server");
                confirm.Attributes.Add("class", "mybutton");
                confirm.Click += new EventHandler(Confirm_Click);
                confirm.Text = "Confirm";
                confirm.Style.Add("float", "right");
                confirm.Style.Add("margin-right", "20px");

                //Clear
                clear.Text = "Clear";
                clear.Attributes.Add("class", "mybutton");
                clear.Style.Add("float", "right");

                //Add controls
                alerts.Controls.Add(txtLabel1);
                alerts.Controls.Add(new LiteralControl("<br />"));
                alerts.Controls.Add(clear);
                alerts.Controls.Add(confirm);
                alerts.Controls.Add(new LiteralControl("<br />"));
                alerts.Controls.Add(new LiteralControl("<hr />"));
            }
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            Session["register"] = ((Button)sender).CommandArgument;
            Server.Transfer("Register.aspx", true);
        }
    }
}