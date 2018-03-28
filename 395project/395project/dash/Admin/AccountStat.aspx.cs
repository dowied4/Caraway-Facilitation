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
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

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
        
        //Adds all the months of the year to the month dropdown
        protected void MonthBind()
        {
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

            for (int i = 1; i < 13; i++)
            {
                MonthDropDown.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                //Adds the months of the year to the MonthDropDown
                MonthBind();
                //Databinds the years to YearDropDown
                YearDropDown.DataBind();
                
                /*Checks if the current year is in the dropdown (They have volunteered this year)
                 * and if not makes the starting value the bottom value (the most recent year)
                 * */
                ListItem checkYear = YearDropDown.Items.FindByText(DateTime.Now.Year.ToString());
                if (checkYear != null)
                    YearDropDown.SelectedValue = DateTime.Now.Year.ToString();

                else
                {
                    YearDropDown.SelectedIndex = YearDropDown.Items.Count - 1;
                }

                //Sets MonthDropDown to the current month
                MonthDropDown.SelectedValue = DateTime.Now.Month.ToString();
                //Binds the Facilitator hour table to the current date
                BindFacilitatorHours(MonthDropDown.Text, YearDropDown.Text);
            }


            //Gets the Selected User
            String ID = Request.QueryString["ID"];
            head.InnerHtml = "Account: " + ID;

            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            //Get Facilitators
            string Facilitators = "SELECT (F.FirstName + ' '+ F.LastName) AS FacilitatorName FROM dbo.Facilitators AS F WHERE " +
                            "F.Id = @CurrentUser";
            SqlCommand getFacilitators = new SqlCommand(Facilitators, con);
            getFacilitators.Parameters.AddWithValue("@CurrentUser", ID);

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
            getMonthlyHours.Parameters.AddWithValue("@Month", MonthDropDown.Text);
            getMonthlyHours.Parameters.AddWithValue("@Year", YearDropDown.Text);

            SqlDataReader MonthlyHoursReader = getMonthlyHours.ExecuteReader();
            if (MonthlyHoursReader.Read())
                monthlyHoursLabel.Text = MonthlyHoursReader["MonthlyHours"].ToString();
            MonthlyHoursReader.Close();


            //Get Yearly Hours
            string YearlyHours = "(SELECT SUM(S.WeeklyHours) AS YearlyHours FROM dbo.Stats AS S WHERE S.Id = @CurrentUser AND S.Year = @Year GROUP BY S.Id)";
            SqlCommand getYearlyHours = new SqlCommand(YearlyHours, con);
            getYearlyHours.Parameters.AddWithValue("@CurrentUser", ID);
            getYearlyHours.Parameters.AddWithValue("@Year", YearDropDown.Text);

            SqlDataReader YearlyHoursReader = getYearlyHours.ExecuteReader();
            if (YearlyHoursReader.Read())
                yearlyHoursLabel.Text = YearlyHoursReader["YearlyHours"].ToString();
            YearlyHoursReader.Close();

            //Gets the week of the year of the first full week of the month
            int firstWeekOfMonth = GetWeekOfMonth.FirstMonday(DateTime.Now.Month);
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            BindFacilitatorHours(MonthDropDown.Text, YearDropDown.Text);
        }

        //Databinds FacilitatorHoursGridView
        private void BindFacilitatorHours(string month, string year)
        {
            int firstWeekOfMonth = GetWeekOfMonth.FirstMonday(Convert.ToInt32(month));
            Label3.Text = firstWeekOfMonth.ToString();
            String ID = Request.QueryString["ID"];
            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            //Gets each week, the month and the year for each facilitator
            string upc = "SELECT Facilitators.Name, Week1.Week1 AS 'Week 1', Week2.Week2 AS 'Week 2',  Week3.Week3 AS 'Week 3', " +
              "Week4.Week4 AS 'Week 4', Monthly.MonthTotal, Yearly.YearTotal " +
              "FROM(SELECT(S.FacilitatorFirstName + ' ' + S.FacilitatorLastName) AS Name, sum(WeeklyHours) as MonthTotal " +
              "FROM Stats as S WHERE S.Month = @Month GROUP BY(S.FacilitatorFirstName + ' ' + S.FacilitatorLastName)) AS Monthly " +
              "FULL JOIN " +
              "(SELECT (S.FacilitatorFirstName +' ' + S.FacilitatorLastName) AS Name, sum(WeeklyHours) as YearTotal " +
              "FROM Stats as S WHERE S.Year = @Year GROUP BY(S.FacilitatorFirstName +' ' + S.FacilitatorLastName)) AS Yearly " +
              "ON Monthly.Name = Yearly.Name " +
              "FULL JOIN( " +
              "(SELECT SUM(S.WeeklyHours) AS Week1, (S.FacilitatorFirstName +' ' + S.FacilitatorLastName) AS Name " +
              "FROM dbo.Stats AS S WHERE S.Id = @User AND S.Month = @Month " +
              "AND S.Year = @Year AND S.WeekOfYear = @Week1 GROUP BY(S.FacilitatorFirstName +' ' + S.FacilitatorLastName)) " +
              ") AS Week1 " +
              "ON Week1.Name = Yearly.Name " +
              "FULL JOIN " +
              "(SELECT SUM(S.WeeklyHours) AS Week2, (S.FacilitatorFirstName +' ' + S.FacilitatorLastName) AS Name " +
              "FROM dbo.Stats AS S WHERE S.Id = @User AND S.Month = @Month " +
              "AND S.Year = @Year AND S.WeekOfYear = @Week2 GROUP BY(S.FacilitatorFirstName +' ' + S.FacilitatorLastName)) " +
              "AS Week2 " +
              "ON Week2.Name = Yearly.Name " +
              "FULL JOIN " +
              "(SELECT SUM(S.WeeklyHours) AS Week3, (S.FacilitatorFirstName +' ' + S.FacilitatorLastName) AS Name " +
              "FROM dbo.Stats AS S WHERE S.Id = @User AND S.Month = @Month " +
              "AND S.Year = @Year AND S.WeekOfYear = @Week3 GROUP BY(S.FacilitatorFirstName +' ' + S.FacilitatorLastName)) " +
              "AS Week3 " +
              "ON Week3.Name = Yearly.Name " +
              "FULL JOIN " +
              "(SELECT SUM(S.WeeklyHours) AS Week4, (S.FacilitatorFirstName +' ' + S.FacilitatorLastName) AS Name " +
              "FROM dbo.Stats AS S WHERE S.Id = @User AND S.Month = @Month " +
              "AND S.Year = @Year AND S.WeekOfYear = @Week4 GROUP BY(S.FacilitatorFirstName +' ' + S.FacilitatorLastName)) " +
              "AS Week4 " +
              "ON Week4.Name = Yearly.Name " +
              "RIGHT JOIN " +
              "(SELECT (F.FirstName +' ' + F.LastName) AS Name FROM dbo.Facilitators AS F WHERE F.Id = @User) AS Facilitators " +
              "ON Facilitators.Name = Yearly.Name";
            SqlCommand getup = new SqlCommand(upc, con);
            getup.Parameters.AddWithValue("@Week1", firstWeekOfMonth);
            getup.Parameters.AddWithValue("@Week2", firstWeekOfMonth + 1);
            getup.Parameters.AddWithValue("@Week3", firstWeekOfMonth + 2);
            getup.Parameters.AddWithValue("@Week4", firstWeekOfMonth + 3);
            getup.Parameters.AddWithValue("@Month", month);
            getup.Parameters.AddWithValue("@Year", year);
            getup.Parameters.AddWithValue("@User", ID);

            //Execture the querey
            SqlDataReader upcompQuery = getup.ExecuteReader();
            FacilitatorHoursGridView.DataSource = upcompQuery;

            FacilitatorHoursGridView.DataBind();

            upcompQuery.Close();
        }
    }
}