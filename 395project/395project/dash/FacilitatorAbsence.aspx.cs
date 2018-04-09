using _395project.App_Code;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _395project.Pages
{
    public partial class FacilitatorAbsence : System.Web.UI.Page
    {
        //Chooses master page based on User Role
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            ChooseMaster choose = new ChooseMaster();
            MasterPageFile = choose.GetMaster();
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string[] fromDate = datepickerFrom.Text.Split('-');
            string[] toDate = datepickerTo.Text.Split('-');

            DateTime startValid = DateTime.Today;
            DateTime endValid = new DateTime(2020, 1, 1);


            try
            {
                DateTime startTime = new DateTime(Int32.Parse(fromDate[0]), Int32.Parse(fromDate[1]), Int32.Parse(fromDate[2]));
                DateTime endTime = new DateTime(Int32.Parse(toDate[0]), Int32.Parse(toDate[1]), Int32.Parse(toDate[2]));

                if (startTime < endTime && startTime > startValid && endTime < endValid)
                {
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                    conn.Open();
                    string insert = "insert into Absence(Email, StartDate, EndDate, Reason) values (@CurrentUser, @StartDate, @EndDate, @Reason)";
                    SqlCommand cmd = new SqlCommand(insert, conn);
                    cmd.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());
                    cmd.Parameters.AddWithValue("@StartDate", startTime);
                    cmd.Parameters.AddWithValue("@EndDate", endTime);
                    cmd.Parameters.AddWithValue("@Reason", Reason.Text);

                    cmd.ExecuteNonQuery();

                    string remove = "delete from Absence where Email = '' or StartDate = '' or EndDate = '' or Reason = ''";
                    SqlCommand rm = new SqlCommand(remove, conn);
                    rm.ExecuteNonQuery();
                    conn.Close();

                    Reason.Text = string.Empty;
                    ErrorMessages.ForeColor = System.Drawing.Color.Green;
                    ErrorMessages.Text = "Absence Request Sent!";
                }
                else
                {
                    ErrorMessages.ForeColor = System.Drawing.Color.Red;
                    ErrorMessages.Text = "Invalid date(s) chosen";
                }
            }
            catch (FormatException ex)
            {
                ErrorMessages.ForeColor = System.Drawing.Color.Red;
                ErrorMessages.Text = "Invalid date(s) chosen";
            }
        }
    }
}