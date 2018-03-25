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
        protected void Page_Load(object sender, EventArgs e)
        {
            toDay.Enabled = false;
            toMonth.Enabled = false;
            fromDay.Enabled = false;
            fromMonth.Enabled = false;
            if (!IsPostBack) {
                fromMonthBind();
                toMonthBind();
            }
        }
        
        protected void ToYear_Change(object sender, EventArgs e)
        {
            toMonth.Enabled = true;
        }

        protected void ToMonth_Change(object sender, EventArgs e)
        {
            toDay.Enabled = true;
            toMonth.Enabled = true;
            int days = DateTime.DaysInMonth(Int32.Parse(toYear.SelectedValue), Int32.Parse(toMonth.SelectedValue));
            toDay.DataSource = Enumerable.Range(1, days);
            toDay.DataBind();
        }

        protected void FromYear_Change(object sender, EventArgs e)
        {
            fromMonth.Enabled = true;
        }
   
        protected void FromMonth_Change(object sender, EventArgs e)
        {
            fromDay.Enabled = true;
            fromMonth.Enabled = true;
            int days = DateTime.DaysInMonth(Int32.Parse(fromYear.SelectedValue), Int32.Parse(fromMonth.SelectedValue));
            fromDay.DataSource = Enumerable.Range(1, days);
            fromDay.DataBind();
        }

        protected void fromMonthBind()
        {
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

            for(int i = 1; i < 13; i++)
            {
                fromMonth.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));
            }
        }

        protected void toMonthBind()
        {
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

            for (int i = 1; i < 13; i++)
            {
                toMonth.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            //string startDate = fromDay.Text + "/" + fromMonth.SelectedValue + "/" + fromYear.Text;
            //fromLabel.Text = startDate;
            DateTime startTime = new DateTime(Int32.Parse(fromYear.Text), Int32.Parse(fromMonth.SelectedValue), Int32.Parse(fromDay.Text));

            //string endDate = toDay.Text + "/" + toMonth.SelectedValue + "/" + toYear.Text;
            //toLabel.Text = endDate;
            DateTime endTime = new DateTime(Int32.Parse(toYear.Text), Int32.Parse(toMonth.SelectedValue), Int32.Parse(toDay.Text));

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
        }
    }
}