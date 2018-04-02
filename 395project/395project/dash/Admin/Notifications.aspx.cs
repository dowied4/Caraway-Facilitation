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
            query = "Select * from [Absence] where Confirmed = 0";


            SqlCommand = new SqlCommand(query, con);
            adapter.SelectCommand = new SqlCommand(query, con);

            //Execute the querey
            reader = SqlCommand.ExecuteReader();

            displayAbsenceRequests(reader);
            reader.Close();
            con.Close();

            if (alerts.Controls.Count > 0)
                noNotifications.Visible = false;
            else
                noNotifications.Visible = true;
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (((Button)sender).CommandName.Equals("Facilitator")) {
                string vars = ((Button)sender).CommandArgument;
                string[] splitVars = vars.Split(',');
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                string insert = "BEGIN IF NOT EXISTS (select * from Facilitators as F where F.Id = @Email and F.FirstName = @FacilitatorFirst and F.LastName = @FacilitatorLast)" +
                    " BEGIN insert into Facilitators(Id,FirstName, LastName) values (@Email,@FacilitatorFirst, @FacilitatorLast) END END";
                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@Email", splitVars[0]);
                cmd.Parameters.AddWithValue("@FacilitatorFirst", splitVars[1]);
                cmd.Parameters.AddWithValue("@FacilitatorLast", splitVars[2]);
                cmd.ExecuteNonQuery();

                //Remove if one of the fields is empty
                string remove = "delete from Facilitators where ID = '' or FirstName = '' or LastName = ''";
                SqlCommand rm = new SqlCommand(remove, conn);
                rm.ExecuteNonQuery();
                conn.Close();
                removeOnConfirm(Int32.Parse(splitVars[3]), ((Button)sender).CommandName);
                ConfirmPopupExtender.Hide();
                Response.Redirect(Request.RawUrl);
                //ErrorMessage.Text = "Added " + splitVars[1] + " " + splitVars[2] + " as a Facilitator to " + splitVars[0];

            }
            //add query so it updates the confirmed column to 1 and subtract hours they
            //are missing from monthly and yearly
            if (((Button)sender).CommandName.Equals("Absence")) {
                string confirm = ((Button)sender).CommandArgument;
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                string update = "UPDATE Absence SET Confirmed = @confirm Where Id = @Id";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@confirm", 1);
                cmd.Parameters.AddWithValue("@Id", Int32.Parse(confirm));
                cmd.ExecuteNonQuery();
                conn.Close();
                AbsencePopupExtender.Hide();
                Response.Redirect(Request.RawUrl);
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

        protected void removeOnConfirm(int id, string requestType)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            string remove = "";

            if (requestType.Equals("Facilitator"))
                remove = "Delete from RequestFacilitator where Id = @RequestID";

            if (requestType.Equals("Absence"))
                remove = "Delete from Absence where Id = @RequestID";

            SqlCommand rm = new SqlCommand(remove, con);
            rm.Parameters.AddWithValue("@RequestID", id);
            SqlDataReader reader = rm.ExecuteReader();
            reader.Close();
            con.Close();

        }

        private void displayFacilRequests(SqlDataReader reader)
        {
            while (reader.Read())
            {
                //Label
                Label notificationLabel = makeLabel("Facilitator", reader);

                //Confirm
                string confirmCmdArg = reader.GetValue(1).ToString() + "," + reader.GetValue(2).ToString() + "," + reader.GetValue(3).ToString() + "," + reader.GetValue(0).ToString();
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
                Button confirm = makeConfirm(reader.GetValue(0).ToString(), "Absence");

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
            if (requestType.Equals("Facilitator"))
                confirm.Click += new EventHandler(confirmFacilitator);
            if(requestType.Equals("Absence"))
                confirm.Click += new EventHandler(confirmAbsenceReq);
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

        protected void confirmFacilitator(object sender, System.EventArgs e)
        {
            ConfirmPopupExtender.Show();
            string vars = ((Button)sender).CommandArgument;
            string[] splitVars = vars.Split(',');
            facilConfirmButton.CommandName = ((Button)sender).CommandName;
            facilConfirmButton.CommandArgument = vars;

            infoLabel.Text = "Add " + splitVars[1] + " " + splitVars[2] + " as a facilitator to " + splitVars[0];

        }

        protected void cancelFacilitator(object sender, System.EventArgs e)
        {
            ConfirmPopupExtender.Hide();
        }

        protected void confirmAbsenceReq(object sender, System.EventArgs e)
        {
            AbsencePopupExtender.Show();
            confirmAbsence.CommandName = ((Button)sender).CommandName;
            confirmAbsence.CommandArgument = ((Button)sender).CommandArgument;

            infoLabel2.Text = "Allow absence for stated period?";
        }

        protected void cancelAbsenceReq(object sender, System.EventArgs e)
        {
            AbsencePopupExtender.Hide();
        }
    }
}