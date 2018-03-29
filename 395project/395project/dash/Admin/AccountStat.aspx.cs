﻿using System;
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

                //Binds the Facilitator Room hour table to the current date
                BindFacilitatorRoomHours(MonthDropDown.Text, YearDropDown.Text);

                //Binds the Room hour table to the current date
                BindRoomHours(MonthDropDown.Text, YearDropDown.Text);

                //Binds the total stats table to the current date
                BindTotalStats(MonthDropDown.Text, YearDropDown.Text);

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
            BindFacilitatorRoomHours(MonthDropDown.Text, YearDropDown.Text);
            BindRoomHours(MonthDropDown.Text, YearDropDown.Text);
            BindTotalStats(MonthDropDown.Text, YearDropDown.Text);
        }

        //Databinds FacilitatorHoursGridView
        private void BindFacilitatorHours(string month, string year)
        {
            int firstWeekOfMonth = GetWeekOfMonth.FirstMonday(Convert.ToInt32(month));
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
        
        //Databinds FacilitatorRoomHours Gridview
        private void BindFacilitatorRoomHours(string month, string year)
        {
            int firstWeekOfMonth = GetWeekOfMonth.FirstMonday(Convert.ToInt32(month));
            String ID = Request.QueryString["ID"];
            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            //Gets each week, the month and the year for each facilitator
            string upc = "SELECT Facilitators.Name, Yearly.Room, Week1.Week1 AS 'Week 1', Week2.Week2 AS 'Week 2',  Week3.Week3 AS 'Week 3', " +
              "Week4.Week4 AS 'Week 4', Monthly.MonthTotal, Yearly.YearTotal " +
              "FROM(SELECT(S.FacilitatorFirstName + ' ' + S.FacilitatorLastName) AS Name, R.Room, sum(WeeklyHours) as MonthTotal " +
              "FROM Stats as S, Rooms as R WHERE R.RoomId = S.RoomId AND " +
              "S.Month = @Month GROUP BY(S.FacilitatorFirstName + ' ' + S.FacilitatorLastName), R.Room) AS Monthly " +
              "FULL JOIN " +
              "(SELECT (S.FacilitatorFirstName +' ' + S.FacilitatorLastName) AS Name, R.Room, sum(WeeklyHours) as YearTotal " +
              "FROM Stats as S, Rooms as R WHERE R.RoomId = S.RoomId AND " +
              "S.Year = @Year GROUP BY(S.FacilitatorFirstName +' ' + S.FacilitatorLastName), R.Room) AS Yearly " +
              "ON Monthly.Name = Yearly.Name AND Yearly.Room = Monthly.Room " +
              "FULL JOIN( " +
              "(SELECT SUM(S.WeeklyHours) AS Week1, (S.FacilitatorFirstName +' ' + S.FacilitatorLastName) AS Name, R.Room " +
              "FROM dbo.Stats AS S, Rooms AS R WHERE R.RoomId = S.RoomId AND S.Id = @User AND S.Month = @Month " +
              "AND S.Year = @Year AND S.WeekOfYear = @Week1 GROUP BY(S.FacilitatorFirstName +' ' + S.FacilitatorLastName), R.Room) " +
              ") AS Week1 " +
              "ON Week1.Name = Yearly.Name AND Yearly.Room = Week1.Room " +
              "FULL JOIN " +
              "(SELECT SUM(S.WeeklyHours) AS Week2, (S.FacilitatorFirstName +' ' + S.FacilitatorLastName) AS Name, R.Room " +
              "FROM dbo.Stats AS S, Rooms AS R WHERE R.RoomId = S.RoomId AND S.Id = @User AND S.Month = @Month " +
              "AND S.Year = @Year AND S.WeekOfYear = @Week2 GROUP BY(S.FacilitatorFirstName +' ' + S.FacilitatorLastName), R.Room) " +
              "AS Week2 " +
              "ON Week2.Name = Yearly.Name AND Yearly.Room = Week2.Room " +
              "FULL JOIN " +
              "(SELECT SUM(S.WeeklyHours) AS Week3, (S.FacilitatorFirstName +' ' + S.FacilitatorLastName) AS Name, R.Room " +
              "FROM dbo.Stats AS S, Rooms AS R WHERE R.RoomId = S.RoomId AND S.Id = @User AND S.Month = @Month " +
              "AND S.Year = @Year AND S.WeekOfYear = @Week3 GROUP BY(S.FacilitatorFirstName +' ' + S.FacilitatorLastName), R.Room) " +
              "AS Week3 " +
              "ON Week3.Name = Yearly.Name AND Yearly.Room = Week3.Room " +
              "FULL JOIN " +
              "(SELECT SUM(S.WeeklyHours) AS Week4, (S.FacilitatorFirstName +' ' + S.FacilitatorLastName) AS Name, R.Room " +
              "FROM dbo.Stats AS S, Rooms AS R WHERE R.RoomId = S.RoomId AND S.Id = @User AND S.Month = @Month " +
              "AND S.Year = @Year AND S.WeekOfYear = @Week4 GROUP BY(S.FacilitatorFirstName +' ' + S.FacilitatorLastName), R.Room) " +
              "AS Week4 " +
              "ON Week4.Name = Yearly.Name AND Yearly.Room = Week4.Room " +
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
            FacilitatorRoomHoursGridView.DataSource = upcompQuery;

            FacilitatorRoomHoursGridView.DataBind();

            upcompQuery.Close();
        }

        //Databinds RoomHoursGridView
        private void BindRoomHours(string month, string year)
        {
            int firstWeekOfMonth = GetWeekOfMonth.FirstMonday(Convert.ToInt32(month));
            String ID = Request.QueryString["ID"];
            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            //Gets each week, the month and the year for each facilitator
            string upc = "SELECT Rooms.Room, Week1.Week1 AS 'Week 1', Week2.Week2 AS 'Week 2',  Week3.Week3 AS 'Week 3', " +
                "Week4.Week4 AS 'Week 4', Monthly.MonthTotal, Yearly.YearTotal " +
                "FROM(SELECT R.Room, sum(WeeklyHours) as MonthTotal " +
                "FROM Stats as S, Rooms as R WHERE R.RoomId = S.RoomId AND " +
                "S.Month = 3 GROUP BY R.Room) AS Monthly " +
                "LEFT JOIN " +
                "(SELECT R.Room, sum(WeeklyHours) as YearTotal " +
                "FROM Stats as S, Rooms as R WHERE R.RoomId = S.RoomId AND " +
                "S.Year = 2018 GROUP BY R.Room) AS Yearly " +
                "ON Yearly.Room = Monthly.Room " +
                "FULL JOIN( " +
                "(SELECT SUM(S.WeeklyHours) AS Week1, R.Room " +
                "FROM dbo.Stats AS S, Rooms AS R WHERE R.RoomId = S.RoomId AND S.Id = 'sullivanr5@mymacewan.ca' AND S.Month = 3 " +
                "AND S.Year = 2018 AND S.WeekOfYear = 10 GROUP BY R.Room)) " +
                "AS Week1 " +
                "ON Yearly.Room = Week1.Room " +
                "FULL JOIN " +
                "(SELECT SUM(S.WeeklyHours) AS Week2, R.Room " +
                "FROM dbo.Stats AS S, Rooms AS R WHERE R.RoomId = S.RoomId AND S.Id = 'sullivanr5@mymacewan.ca' AND S.Month = 3 " +
                "AND S.Year = 2018 AND S.WeekOfYear = 11 GROUP BY R.Room) AS Week2 " +
                "ON Week2.Room = Yearly.Room " +
                "FULL JOIN " +
                "(SELECT SUM(S.WeeklyHours) AS Week3, R.Room " +
                "FROM dbo.Stats AS S, Rooms AS R WHERE R.RoomId = S.RoomId AND S.Id = 'sullivanr5@mymacewan.ca' AND S.Month = 3 " +
                "AND S.Year = 2018 AND S.WeekOfYear = 12 GROUP BY R.Room) " +
                "AS Week3 " +
                "ON Week3.Room = Yearly.Room " +
                "FULL JOIN " +
                "(SELECT SUM(S.WeeklyHours) AS Week4, R.Room " +
                "FROM dbo.Stats AS S, Rooms AS R WHERE R.RoomId = S.RoomId AND S.Id = 'sullivanr5@mymacewan.ca' AND S.Month = 3 " +
                "AND S.Year = 2018 AND S.WeekOfYear = 13 GROUP BY R.Room) AS Week4 " +
                "ON Week4.Room = Yearly.Room " +
                "RIGHT JOIN " +
                "(SELECT Room FROM Rooms) AS Rooms " +
                "ON Rooms.Room = Yearly.Room";

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
            RoomHoursGridView.DataSource = upcompQuery;

            RoomHoursGridView.DataBind();

            upcompQuery.Close();
        }

        //Binds the TotalStatsGridView **DOES NOT WORK WHEN AN ABSENCE EXTENDS INTO A NEW CALENDAR YEAR***
        protected void BindTotalStats(string month, string year)
        {
            String ID = Request.QueryString["ID"];
            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            //Gets the number of kids the family has
            string countKids = "SELECT COUNT(*) FROM Children Where Id = @User";
            SqlCommand getNumKids = new SqlCommand(countKids, con);
            getNumKids.Parameters.AddWithValue("@User", ID);
            int numKids = (int)getNumKids.ExecuteScalar();

            //Gets the total absent days the facilitator is missing for the year
            SqlDataAdapter adapter = new SqlDataAdapter();
            string absences = "SELECT StartDate, EndDate FROM Absence where Email = @User AND YEAR(StartDate) " +
                "= @Year AND Confirmed = 1";
            SqlCommand getAbsences = new SqlCommand(absences, con);
            adapter.SelectCommand = new SqlCommand(absences, con);
            getAbsences.Parameters.AddWithValue("@Year", year);
            getAbsences.Parameters.AddWithValue("@User", ID);
            SqlDataReader reader = getAbsences.ExecuteReader();
            Double absentDays = 0;

            //Gets the total weeks so far in the year (based on the selected dropdown date)
            Double totalWeeks = ((new DateTime(Int32.Parse(year), Int32.Parse(month), DateTime.DaysInMonth(Int32.Parse(year), Int32.Parse(month))) - new DateTime(DateTime.Now.Year, 1, 1)).TotalDays) / 7;
            Double totalYearlyHours;
            //Subtracts the absent days from the total days
            while (reader.Read())
            {
                DateTime start = (DateTime)reader.GetValue(0);
                DateTime end = (DateTime)reader.GetValue(1);
                absentDays += (end - start).Days;
            }
            Label2.Text = absentDays.ToString();
            totalWeeks -= (absentDays / 7);
            //Gets how many hours the facilitator needs to work for the year
            switch (numKids)
            {
                case 0:
                    totalYearlyHours = 0;
                    break;
                case 1:
                    totalYearlyHours = totalWeeks * 2.5;
                    break;
                default:
                    totalYearlyHours = totalWeeks * 5;
                    break; 
            }
            //Rounds to the nearest hour
            totalYearlyHours = Math.Round(totalYearlyHours);
            Label1.Text = totalYearlyHours.ToString();

            //Gets the total hours the family has worked this year
            DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();
            dr["MonthlyTotal"] = string.Empty;
            dr["YearlyTotal"] = (Int32.Parse(yearlyHoursLabel.Text) - totalYearlyHours).ToString();

            dt.Rows.Add(dr);
            TotalStatsGridView.DataSource = dt;
            TotalStatsGridView.DataBind();
            //TotalStatsGridView.Rows[0].Cells[1].Text = (Int32.Parse(yearlyHoursLabel.Text) - totalYearlyHours).ToString();
        }
    }
}