using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using _395project.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using _395project.App_Code;

namespace _395project.dash.Admin
{
    public partial class AccountStat : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            ChooseMaster choose = new ChooseMaster();
            MasterPageFile = choose.GetMaster();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            String ID = Request.QueryString["ID"];
            head.InnerHtml = "Account: " + ID;

            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();
           // ID = "sullivanr5@mymacewan.ca";
            //Get Facilitators
            string Facilitators = "SELECT (F.FirstName + ' '+ F.LastName) AS FacilitatorName FROM dbo.Facilitators AS F WHERE " +
                            "F.Id = @CurrentUser";
            SqlCommand getFacilitators = new SqlCommand(Facilitators, con);
            getFacilitators.Parameters.AddWithValue("@CurrentUser", ID);

            //adapter.SelectCommand = new SqlCommand(Facilitators, con);

            //Execture the querey
            SqlDataReader facilitatorReader = getFacilitators.ExecuteReader();
            //Assign results
            FacView.DataSource = facilitatorReader;
            //Bind the data
            FacView.DataBind();
            facilitatorReader.Close();

            //Get Children
            string Children = "SELECT (C.FirstName + ' '+ C.LastName) AS Name, C.Grade as Grade, C.Class as Classroom FROM dbo.Children AS C WHERE " +
                            "C.Id = @CurrentUser";
            SqlCommand getChildren = new SqlCommand(Children, con);
            getChildren.Parameters.AddWithValue("@CurrentUser", ID);

            //Execute the querey
            SqlDataReader childrenReader = getChildren.ExecuteReader();
            //Assign results
            ChildView.DataSource = childrenReader;
            //Bind the data
            ChildView.DataBind();
            childrenReader.Close();



            //Get Monthly Hours
            string MonthlyHours = "(SELECT SUM(S.WeeklyHours) AS MonthlyHours FROM dbo.Stats AS S WHERE S.Id = @CurrentUser AND S.Month = @Month " +
                "AND S.Year = @Year GROUP BY S.Id)";
            SqlCommand getMonthlyHours = new SqlCommand(MonthlyHours, con);
            getMonthlyHours.Parameters.AddWithValue("@CurrentUser", ID);
            getMonthlyHours.Parameters.AddWithValue("@Month", DateTime.Now.Month);
            getMonthlyHours.Parameters.AddWithValue("@Year", DateTime.Now.Year);

            SqlDataReader MonthlyHoursReader = getMonthlyHours.ExecuteReader();
            if (MonthlyHoursReader.Read())
                monthlyHoursLabel.Text = MonthlyHoursReader["MonthlyHours"].ToString();
            MonthlyHoursReader.Close();


            //Get Yearly Hours
            string YearlyHours = "(SELECT SUM(S.WeeklyHours) AS YearlyHours FROM dbo.Stats AS S WHERE S.Id = @CurrentUser AND S.Year = @Year GROUP BY S.Id)";
            SqlCommand getYearlyHours = new SqlCommand(YearlyHours, con);
            getYearlyHours.Parameters.AddWithValue("@CurrentUser", ID);
            getYearlyHours.Parameters.AddWithValue("@Year", DateTime.Now.Year);

            SqlDataReader YearlyHoursReader = getYearlyHours.ExecuteReader();
            if (YearlyHoursReader.Read())
                yearlyHoursLabel.Text = YearlyHoursReader["YearlyHours"].ToString();
            YearlyHoursReader.Close();




            //Get Week 1
            string weekHours = "(SELECT SUM(S.WeeklyHours) AS weekHours FROM dbo.Stats AS S WHERE S.Id = @CurrentUser AND S.Month = @Month " +
                "AND S.Year= @Year AND S.Week = @Week GROUP BY S.Id)"; 
            SqlCommand getWeek1 = new SqlCommand(weekHours, con);
            getWeek1.Parameters.AddWithValue("@CurrentUser", ID);
            getWeek1.Parameters.AddWithValue("@Month", DateTime.Now.Month);
            getWeek1.Parameters.AddWithValue("@Year", DateTime.Now.Year);
            getWeek1.Parameters.AddWithValue("@Week", 1);
            SqlDataReader Week1HourReader = getWeek1.ExecuteReader();
            if (Week1HourReader.Read())
                weekLabel1.Text = Week1HourReader["weekHours"].ToString();
            Week1HourReader.Close();

            //Get Week 2
            SqlCommand getWeek2 = new SqlCommand(weekHours, con);
            getWeek2.Parameters.AddWithValue("@CurrentUser", ID);
            getWeek2.Parameters.AddWithValue("@Month", DateTime.Now.Month);
            getWeek2.Parameters.AddWithValue("@Year", DateTime.Now.Year);
            getWeek2.Parameters.AddWithValue("@Week", 2);
            SqlDataReader Week2HourReader = getWeek2.ExecuteReader();
            if (Week2HourReader.Read())
                weekLabel2.Text = Week2HourReader["weekHours"].ToString();
            Week2HourReader.Close();

            //Get Week 3
            SqlCommand getWeek3 = new SqlCommand(weekHours, con);
            getWeek3.Parameters.AddWithValue("@CurrentUser", ID);
            getWeek3.Parameters.AddWithValue("@Month", DateTime.Now.Month);
            getWeek3.Parameters.AddWithValue("@Year", DateTime.Now.Year);
            getWeek3.Parameters.AddWithValue("@Week", 3);
            SqlDataReader Week3HourReader = getWeek3.ExecuteReader();
            if (Week3HourReader.Read())
                weekLabel3.Text = Week3HourReader["weekHours"].ToString();
            Week3HourReader.Close();

            //Get Week 4
            SqlCommand getWeek4 = new SqlCommand(weekHours, con);
            getWeek4.Parameters.AddWithValue("@CurrentUser", ID);
            getWeek4.Parameters.AddWithValue("@Month", DateTime.Now.Month);
            getWeek4.Parameters.AddWithValue("@Year", DateTime.Now.Year);
            getWeek4.Parameters.AddWithValue("@Week", 4);
            SqlDataReader Week4HourReader = getWeek4.ExecuteReader();
            if (Week4HourReader.Read())
                weekLabel4.Text = Week4HourReader["weekHours"].ToString();
            Week4HourReader.Close();
        }
    }
}