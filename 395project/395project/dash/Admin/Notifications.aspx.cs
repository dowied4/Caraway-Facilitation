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

            //Query to fetch data from the request facilitators db
            string query = "Select * from [RequestFacilitator]";


            SqlCommand SqlCommand = new SqlCommand(query, con);
            adapter.SelectCommand = new SqlCommand(query, con);

            //Execute the querey
            SqlDataReader reader = SqlCommand.ExecuteReader();

            displayFacilRequests(reader);
            reader.Close();

            //Query to fetch data from the absence db
            query = "Select * from [Absence]";


            SqlCommand = new SqlCommand(query, con);
            adapter.SelectCommand = new SqlCommand(query, con);

            //Execute the querey
            reader = SqlCommand.ExecuteReader();

            displayAbsenceRequests(reader);
            reader.Close();
            con.Close();
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (((Button)sender).CommandName.Equals("Facilitator")){
                Session["register"] = ((Button)sender).CommandArgument;
                Server.Transfer("Register.aspx", true);
            }
            if (((Button)sender).CommandName.Equals("Absence")){
                Session["absence"] = ((Button)sender).CommandArgument;
                Server.Transfer("GiveAbsence.aspx", true);
            }
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            string remove = "";
            string cmdName = ((Button)sender).CommandName;

            if (cmdName.Equals("Facilitator"))
                remove = "Delete from RequestFacilitator where Id = @RequestID";
  
            if (cmdName.Equals("Absence"))
                remove = "Delete from Absence where Id = @RequestID";

            SqlCommand rm = new SqlCommand(remove, con);
            rm.Parameters.AddWithValue("@RequestID", Int32.Parse(((Button)sender).CommandArgument));
            SqlDataReader reader = rm.ExecuteReader();
            reader.Close();
            con.Close();
            Response.Redirect(Request.RawUrl);
        }

        private void displayFacilRequests(SqlDataReader reader)
        {
            while (reader.Read())
            {
                //Label
                Label notificationLabel = makeLabel("Facilitator", reader);

                //Confirm
                string confirmCmdArg = reader.GetValue(1).ToString() + "," + reader.GetValue(2).ToString() + "," + reader.GetValue(3).ToString();
                Button confirm = makeConfirm(confirmCmdArg, "Facilitator");

                //Clear
                Button clear = makeClear(reader.GetValue(0).ToString(), "Facilitator");

                //Add controls
                alerts.Controls.Add(notificationLabel);
                alerts.Controls.Add(new LiteralControl("<br />"));
                alerts.Controls.Add(clear);
                alerts.Controls.Add(confirm);
                alerts.Controls.Add(new LiteralControl("<br />"));
                alerts.Controls.Add(new LiteralControl("<hr />"));
            }
        }

        private void displayAbsenceRequests(SqlDataReader reader)
        {
            while (reader.Read())
            {
                //Label
                Label notificationLabel = makeLabel("Absence", reader);

                //Confirm
                string confirmCmdArg = reader.GetValue(1).ToString() + "," + reader.GetValue(2).ToString() + "," + reader.GetValue(3).ToString() + "," + reader.GetValue(4).ToString();
                Button confirm = makeConfirm(confirmCmdArg, "Absence");

                //Clear
                Button clear = makeClear(reader.GetValue(0).ToString(), "Absence");

                //Add controls
                alerts.Controls.Add(notificationLabel);
                alerts.Controls.Add(new LiteralControl("<br />"));
                alerts.Controls.Add(clear);
                alerts.Controls.Add(confirm);
                alerts.Controls.Add(new LiteralControl("<br />"));
                alerts.Controls.Add(new LiteralControl("<hr />"));
            }
        }

        private Button makeConfirm(string cmdArg, string requestType)
        {
            Button confirm = new Button();

            confirm.CommandName = requestType;
            confirm.CommandArgument = cmdArg;
            confirm.Attributes.Add("runat", "server");
            confirm.Attributes.Add("class", "mybutton");
            confirm.Click += new EventHandler(Confirm_Click);
            confirm.Text = "Confirm";
            confirm.Style.Add("float", "right");
            confirm.Style.Add("margin-right", "20px");

            return confirm;
        }

        private Button makeClear(string databaseID, string requestType)
        {
            Button clear = new Button();
            clear.CommandName = requestType;
            clear.CommandArgument = databaseID;
            clear.Attributes.Add("class", "mybutton");
            clear.Click += new EventHandler(Clear_Click);
            clear.Text = "Clear";
            clear.Style.Add("float", "right");

            return clear;
        }

        private Label makeLabel(string requestType, SqlDataReader reader)
        {
            Label notificationsLabel = new Label();

            if (requestType.Equals("Facilitator"))
                notificationsLabel.Text = reader.GetValue(1).ToString() + " has requested an " + "ADDITIONAL FACILITATOR: " + reader.GetValue(2).ToString() + " " + reader.GetValue(3).ToString();
            if (requestType.Equals("Absence"))
                notificationsLabel.Text = reader.GetValue(1).ToString() + " has requested an " + "ABSENCE from hours FROM: " + reader.GetValue(2).ToString() + " TO: " + reader.GetValue(3).ToString() + ". Reason: " + reader.GetValue(4).ToString();
            notificationsLabel.Attributes.Add("class", "input-header");

            return notificationsLabel;
        }
    }
}