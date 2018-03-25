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
    public partial class FieldTrip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Sets the default time to 8:30-4:00 (all day)
            if (!IsPostBack)
            {
                StartTimeTextBox.Text = "8:45";
                EndTimeTextBox.Text = "15:15";
                Calendar.SelectedDate = DateTime.Now.Date;
            }
        }

        //Adds the FieldTrip to the database
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            //Takes the selected date and adds the start/end times to it
            DateTime day = Calendar.SelectedDate;
            DateTime startTime = day.Add(TimeSpan.Parse(StartTimeTextBox.Text));
            DateTime endTime = day.Add(TimeSpan.Parse(EndTimeTextBox.Text));
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            string insert = "insert into FieldTrips(StartTime, EndTime, Location) values (@StartTime, @EndTime, @Location)";
            SqlCommand cmd = new SqlCommand(insert, con);
            cmd.Parameters.AddWithValue("@StartTime", startTime);
            cmd.Parameters.AddWithValue("@EndTime", endTime);
            cmd.Parameters.AddWithValue("@Location", LocationTextBox.Text);
            cmd.ExecuteNonQuery();
            LocationTextBox.Text = String.Empty;
        }
    }
}