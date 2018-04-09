using _395project.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _395project.dash.Stats
{
    public partial class AllAccounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                MonthBind();

                YearDropDown.DataBind();

                /*Checks if the current year is in the dropdown (They have volunteered this year)
                * and if not makes the starting value the bottom value (the most recent year)
                */
                ListItem checkYear = YearDropDown.Items.FindByText(DateTime.Now.Year.ToString());
                if (checkYear != null)
                    YearDropDown.SelectedValue = DateTime.Now.Year.ToString();

                else
                {
                    YearDropDown.SelectedIndex = YearDropDown.Items.Count - 1;
                }

                //Sets MonthDropDown to the current month
                MonthDropDown.SelectedValue = DateTime.Now.Month.ToString();

                string month = MonthDropDown.Text;
                string year = YearDropDown.Text;

                BindFacilitatorHours(month, year);
            }

        }

        //Binds the 12 months of the year to the month drop down
        protected void MonthBind()
        {
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

            for (int i = 1; i < 13; i++)
            {
                MonthDropDown.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));
            }
        }

        //Updates the FacilitatorGridView with the new months/year
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            //Gets the Selected User
            string month = MonthDropDown.Text;
            string year = YearDropDown.Text;

            BindFacilitatorHours(month, year);
        }

        //Databinds FacilitatorHoursGridView
        private void BindFacilitatorHours(string month, string year)
        {
            int firstWeekOfMonth = GetWeekOfMonth.FirstMonday(Convert.ToInt32(month));
            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            //Gets each week, the month and the year for each facilitator
            string upc = "SELECT Facilitators.Id AS ID, Facilitators.Name, Week1.Week1, Week2.Week2,  Week3.Week3, " +
              "Week4.Week4, Monthly.MonthTotal, Yearly.YearTotal " +
              "FROM(SELECT(S.FacilitatorFirstName + ' ' + S.FacilitatorLastName) AS Name, sum(WeeklyHours) as MonthTotal " +
              "FROM Stats as S WHERE S.Month = @Month GROUP BY(S.FacilitatorFirstName + ' ' + S.FacilitatorLastName)) AS Monthly " +
              "FULL JOIN " +
              "(SELECT (S.FacilitatorFirstName +' ' + S.FacilitatorLastName) AS Name, sum(WeeklyHours) as YearTotal " +
              "FROM Stats as S WHERE S.Year = @Year GROUP BY(S.FacilitatorFirstName +' ' + S.FacilitatorLastName)) AS Yearly " +
              "ON Monthly.Name = Yearly.Name " +
              "FULL JOIN( " +
              "(SELECT SUM(S.WeeklyHours) AS Week1, (S.FacilitatorFirstName +' ' + S.FacilitatorLastName) AS Name " +
              "FROM dbo.Stats AS S WHERE S.Month = @Month " +
              "AND S.Year = @Year AND S.WeekOfYear = @Week1 GROUP BY(S.FacilitatorFirstName +' ' + S.FacilitatorLastName)) " +
              ") AS Week1 " +
              "ON Week1.Name = Yearly.Name " +
              "FULL JOIN " +
              "(SELECT SUM(S.WeeklyHours) AS Week2, (S.FacilitatorFirstName +' ' + S.FacilitatorLastName) AS Name " +
              "FROM dbo.Stats AS S WHERE S.Month = @Month " +
              "AND S.Year = @Year AND S.WeekOfYear = @Week2 GROUP BY(S.FacilitatorFirstName +' ' + S.FacilitatorLastName)) " +
              "AS Week2 " +
              "ON Week2.Name = Yearly.Name " +
              "FULL JOIN " +
              "(SELECT SUM(S.WeeklyHours) AS Week3, (S.FacilitatorFirstName +' ' + S.FacilitatorLastName) AS Name " +
              "FROM dbo.Stats AS S WHERE S.Month = @Month " +
              "AND S.Year = @Year AND S.WeekOfYear = @Week3 GROUP BY(S.FacilitatorFirstName +' ' + S.FacilitatorLastName)) " +
              "AS Week3 " +
              "ON Week3.Name = Yearly.Name " +
              "FULL JOIN " +
              "(SELECT SUM(S.WeeklyHours) AS Week4, (S.FacilitatorFirstName +' ' + S.FacilitatorLastName) AS Name " +
              "FROM dbo.Stats AS S WHERE S.Month = @Month " +
              "AND S.Year = @Year AND S.WeekOfYear = @Week4 GROUP BY(S.FacilitatorFirstName +' ' + S.FacilitatorLastName)) " +
              "AS Week4 " +
              "ON Week4.Name = Yearly.Name " +
              "RIGHT JOIN " +
              "(SELECT F.Id, (F.FirstName +' ' + F.LastName) AS Name FROM dbo.Facilitators AS F) AS Facilitators " +
              "ON Facilitators.Name = Yearly.Name";
            SqlCommand getup = new SqlCommand(upc, con);
            getup.Parameters.AddWithValue("@Week1", firstWeekOfMonth);
            getup.Parameters.AddWithValue("@Week2", firstWeekOfMonth + 1);
            getup.Parameters.AddWithValue("@Week3", firstWeekOfMonth + 2);
            getup.Parameters.AddWithValue("@Week4", firstWeekOfMonth + 3);
            getup.Parameters.AddWithValue("@Month", month);
            getup.Parameters.AddWithValue("@Year", year);

            //Execture the querey
            SqlDataReader upcompQuery = getup.ExecuteReader();
            FacilitatorHoursGridView.DataSource = upcompQuery;

            FacilitatorHoursGridView.DataBind();

            upcompQuery.Close();
            con.Close();
        }
    }
}