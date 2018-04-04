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
            //Gets the Selected User
            String ID = Request.QueryString["ID"];
            head.InnerHtml = "Account: " + ID;
            string month;
            string year;

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

                month = MonthDropDown.Text;
                year = YearDropDown.Text;
                
                //Binds the Facilitator hour table to the current date
                BindFacilitatorHours(month, year, ID);

                //Binds the Facilitator Room hour table to the current date
                BindFacilitatorRoomHours(month, year, ID);

                //Binds the Room hour table to the current date
                BindRoomHours(month, year, ID);

                //Binds the total stats table to the current date
                BindTotalStats(month, year, ID);

                monthlyHoursLabel.Text = GetMonthlyHours(month, year, ID);

                yearlyHoursLabel.Text = GetYearlyHours(month, year, ID);

                BindDonationRecieved(month, year, ID);
                BindDonationGiven(month, year, ID);

            }

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
            con.Close();
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            //Gets the Selected User
            String ID = Request.QueryString["ID"];
            string month = MonthDropDown.Text;
            string year = YearDropDown.Text;

            BindFacilitatorHours(month, year, ID);
            BindFacilitatorRoomHours(month, year, ID);
            BindRoomHours(month, year, ID);
            BindTotalStats(month, year, ID);
            BindDonationRecieved(month, year, ID);
            BindDonationGiven(month, year, ID);
            monthlyHoursLabel.Text = GetMonthlyHours(month, year, ID);
            yearlyHoursLabel.Text = GetYearlyHours(month, year, ID);
        }

        //Gets the hours worked that month
        protected string GetMonthlyHours(string month, string year, string ID)
        {
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            string MonthlyHours = "(SELECT SUM(S.WeeklyHours) AS MonthlyHours FROM dbo.Stats AS S WHERE S.Id = @CurrentUser AND S.Month = @Month " +
                "AND S.Year = @Year)";
            SqlCommand getMonthlyHours = new SqlCommand(MonthlyHours, con);
            getMonthlyHours.Parameters.AddWithValue("@CurrentUser", ID);
            getMonthlyHours.Parameters.AddWithValue("@Month", month);
            getMonthlyHours.Parameters.AddWithValue("@Year", year);

            SqlDataReader MonthlyHoursReader = getMonthlyHours.ExecuteReader();
            if (MonthlyHoursReader.Read())
            {
                if (!String.IsNullOrEmpty(MonthlyHoursReader["MonthlyHours"].ToString()))
                    return MonthlyHoursReader["MonthlyHours"].ToString();
            }
            MonthlyHoursReader.Close();
            con.Close();
            return "0";
        }

        //Gets the total hours worked that year
        protected string GetYearlyHours(string month, string year, string ID)
        {
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            //Get Yearly Hours
            string YearlyHours = "(SELECT SUM(S.WeeklyHours) AS YearlyHours FROM dbo.Stats AS S WHERE S.Id = @CurrentUser AND S.Year = @Year GROUP BY S.Id)";
            SqlCommand getYearlyHours = new SqlCommand(YearlyHours, con);
            getYearlyHours.Parameters.AddWithValue("@CurrentUser", ID);
            getYearlyHours.Parameters.AddWithValue("@Year", YearDropDown.Text);

            SqlDataReader YearlyHoursReader = getYearlyHours.ExecuteReader();
            if (YearlyHoursReader.Read())
            {
                if (!String.IsNullOrEmpty(YearlyHoursReader["YearlyHours"].ToString()))
                    return YearlyHoursReader["YearlyHours"].ToString();
            }
            YearlyHoursReader.Close();
            con.Close();
            return "0";
        }

        //Databinds FacilitatorHoursGridView
        private void BindFacilitatorHours(string month, string year, string ID)
        {
            int firstWeekOfMonth = GetWeekOfMonth.FirstMonday(Convert.ToInt32(month));
            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            //Gets each week, the month and the year for each facilitator
            string upc = "SELECT Facilitators.Name, Week1.Week1, Week2.Week2,  Week3.Week3, " +
              "Week4.Week4, Monthly.MonthTotal, Yearly.YearTotal " +
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
            con.Close();
        }
        
        //Databinds FacilitatorRoomHours Gridview
        private void BindFacilitatorRoomHours(string month, string year, string ID)
        {
            int firstWeekOfMonth = GetWeekOfMonth.FirstMonday(Convert.ToInt32(month));
            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            //Gets each week, the month and the year for each facilitator
            string upc = "SELECT Facilitators.Name, Yearly.Room, Week1.Week1, Week2.Week2,  Week3.Week3, " +
              "Week4.Week4, Monthly.MonthTotal, Yearly.YearTotal " +
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
            con.Close();
        }

        //Databinds RoomHoursGridView
        private void BindRoomHours(string month, string year, string ID)
        {
            int firstWeekOfMonth = GetWeekOfMonth.FirstMonday(Convert.ToInt32(month));
            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            //Gets each week, the month and the year for each facilitator
            string upc = "SELECT Rooms.Room, Week1.Week1, Week2.Week2,  Week3.Week3, " +
                "Week4.Week4, Monthly.MonthTotal, Yearly.YearTotal " +
                "FROM(SELECT R.Room, sum(WeeklyHours) as MonthTotal " +
                "FROM Stats as S, Rooms as R WHERE R.RoomId = S.RoomId AND " +
                "S.Month = @Month AND S.Id = @ID GROUP BY R.Room) AS Monthly " +
                "FULL JOIN " +
                "(SELECT R.Room, sum(WeeklyHours) as YearTotal " +
                "FROM Stats as S, Rooms as R WHERE R.RoomId = S.RoomId AND " +
                "S.Year = @Year AND S.Id = @ID GROUP BY R.Room) AS Yearly " +
                "ON Yearly.Room = Monthly.Room " +
                "FULL JOIN( " +
                "(SELECT SUM(S.WeeklyHours) AS Week1, R.Room " +
                "FROM dbo.Stats AS S, Rooms AS R WHERE R.RoomId = S.RoomId AND S.Id = @ID AND S.Month = @MONTH " +
                "AND S.Year = @YEAR AND S.WeekOfYear = @Week1 GROUP BY R.Room)) " +
                "AS Week1 " +
                "ON Yearly.Room = Week1.Room " +
                "FULL JOIN " +
                "(SELECT SUM(S.WeeklyHours) AS Week2, R.Room " +
                "FROM dbo.Stats AS S, Rooms AS R WHERE R.RoomId = S.RoomId AND S.Id = @ID AND S.Month = @Month " +
                "AND S.Year = @Year AND S.WeekOfYear = @Week2 GROUP BY R.Room) AS Week2 " +
                "ON Week2.Room = Yearly.Room " +
                "FULL JOIN " +
                "(SELECT SUM(S.WeeklyHours) AS Week3, R.Room " +
                "FROM dbo.Stats AS S, Rooms AS R WHERE R.RoomId = S.RoomId AND S.Id = @ID AND S.Month = @Month " +
                "AND S.Year = @Year AND S.WeekOfYear = @Week3 GROUP BY R.Room) " +
                "AS Week3 " +
                "ON Week3.Room = Yearly.Room " +
                "FULL JOIN " +
                "(SELECT SUM(S.WeeklyHours) AS Week4, R.Room " +
                "FROM dbo.Stats AS S, Rooms AS R WHERE R.RoomId = S.RoomId AND S.Id = @ID AND S.Month = @Month " +
                "AND S.Year = @Year AND S.WeekOfYear = @Week4 GROUP BY R.Room) AS Week4 " +
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
            getup.Parameters.AddWithValue("@ID", ID);

            //Execture the querey
            SqlDataReader upcompQuery = getup.ExecuteReader();
            RoomHoursGridView.DataSource = upcompQuery;

            RoomHoursGridView.DataBind();

            upcompQuery.Close();
            con.Close();
        }

        //Binds the TotalStatsGridView **DOES NOT WORK WHEN AN ABSENCE EXTENDS INTO A NEW CALENDAR YEAR***
        protected void BindTotalStats(string month, string year, string ID)
        {
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
            getAbsences.Parameters.AddWithValue("@Month", month);
            SqlDataReader reader = getAbsences.ExecuteReader();
            Double yearAabsentDays = 0;

            //Gets the total weeks so far in the year (based on the selected dropdown date)
            Double yearTotalWeeks = GetWeekOfMonth.GetWeekOfYear(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)));
            //((new DateTime(Int32.Parse(year), Int32.Parse(month), DateTime.DaysInMonth(Int32.Parse(year), Int32.Parse(month))) - new DateTime(Int32.Parse(year), 1, 1)).TotalDays) / 7;
            Double totalYearlyHours;
            //Subtracts the absent days from the total days
            while (reader.Read())
            {
                DateTime start = (DateTime)reader.GetValue(0);
                DateTime end = (DateTime)reader.GetValue(1);
                yearAabsentDays += (end - start).Days;
            }
            yearTotalWeeks -= (yearAabsentDays / 7);
            reader.Close();

            //Gets the total absent days the facilitator is missing for the Month
            SqlDataAdapter monthAdapter = new SqlDataAdapter();
            string monthAbsences = "SELECT StartDate, EndDate FROM Absence where Email = @User AND YEAR(StartDate) " +
                "= @Year AND MONTH(StartDate) = @Month AND Confirmed = 1";
            SqlCommand getMonthAbsences = new SqlCommand(monthAbsences, con);
            monthAdapter.SelectCommand = new SqlCommand(monthAbsences, con);
            getMonthAbsences.Parameters.AddWithValue("@Year", year);
            getMonthAbsences.Parameters.AddWithValue("@Month", month);
            getMonthAbsences.Parameters.AddWithValue("@User", ID);
            SqlDataReader monthReader = getMonthAbsences.ExecuteReader();
            Double monthAbsentDays = 0;

            //Gets the total weeks so far in the year (based on the selected dropdown date)
            Double monthTotalWeeks = GetWeekOfMonth.MondaysInMonth(new DateTime(Int32.Parse(year), Int32.Parse(month), 1));
            Double totalMonthlyHours;
            //Subtracts the absent days from the total days
            while (monthReader.Read())
            {
                DateTime start = (DateTime)monthReader.GetValue(0);
                DateTime end = (DateTime)monthReader.GetValue(1);
                monthAbsentDays += (end - start).Days;
            }
            monthTotalWeeks -= (monthAbsentDays / 7);
            monthReader.Close();
            //Gets how many hours the facilitator needs to work for the year
            switch (numKids)
            {
                case 0:
                    totalYearlyHours = 0;
                    totalMonthlyHours = 0;
                    break;
                case 1:
                    totalYearlyHours = yearTotalWeeks * 2.5;
                    totalMonthlyHours = monthTotalWeeks * 2.5;
                    break;
                default:
                    totalYearlyHours = yearTotalWeeks * 5;
                    totalMonthlyHours = monthTotalWeeks * 5;
                    break; 
            }
            //Rounds to the nearest hour
            totalYearlyHours = Math.Round(totalYearlyHours, 1);
            totalMonthlyHours = Math.Round(totalMonthlyHours, 2);

            //Gets the total hours the family has worked this year
            DataTable dt = new DataTable();
            dt.Columns.Add("MonthlyTotal", typeof(string));
            dt.Columns.Add("YearlyTotal", typeof(string));
            DataRow dr = dt.NewRow();
            dr["MonthlyTotal"] = (Double.Parse(GetMonthlyHours(month, year, ID)) - totalMonthlyHours).ToString();
            dr["YearlyTotal"] = (Double.Parse(GetYearlyHours(month, year, ID)) - totalYearlyHours).ToString();

            dt.Rows.Add(dr);
            TotalStatsGridView.DataSource = dt;
            TotalStatsGridView.DataBind();
            con.Close();
        }

        /*Binds which users donated and how many hours the user received from them in the 
         * selected month and how many that user has given the selected year*/
        protected void BindDonationRecieved(string month, string year, string ID)
        {
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            //Get users that donated to selected user in the selected month and year and the month total
            string query = "SELECT Donate, SUM(WeeklyHours) AS totalDonated FROM Stats WHERE Id = @CurrentUser AND " +
                "Month = @Month AND Year = @Year AND datalength(Donate)!=0 GROUP BY Donate ORDER BY Donate";

            SqlCommand recieved = new SqlCommand(query, con);
            recieved.Parameters.AddWithValue("@CurrentUser", ID);
            recieved.Parameters.AddWithValue("@Month", month);
            recieved.Parameters.AddWithValue("@Year", year);

            SqlDataReader reader = recieved.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("From", typeof(string));
            dt.Columns.Add("Month Total", typeof(string));
            dt.Columns.Add("Year Total", typeof(float));
            DataRow dr;

            //Add a row for each unique user
            while (reader.Read())
            {
                dr = dt.NewRow();
                dr["From"] = reader.GetValue(0);
                dr["Month Total"] = reader.GetValue(1).ToString();
                dr["Year Total"] = 0.00;
                dt.Rows.Add(dr);
            }
            reader.Close();

            //Get users that donated to selected user in the selected year and the year total
            query = "SELECT Donate, SUM(WeeklyHours) AS totalDonated FROM Stats WHERE Id = @CurrentUser AND " +
                "Year = @Year AND datalength(Donate)!=0 GROUP BY Donate ORDER BY Donate";

            recieved = new SqlCommand(query, con);
            recieved.Parameters.AddWithValue("@CurrentUser", ID);
            recieved.Parameters.AddWithValue("@Year", year);

            reader = recieved.ExecuteReader();
            int index = 0;
            DataRow[] foundRows;
            string expression;

            //Read the results
            while (reader.Read())
            {
                /*Only add the year total if the user who donated to the selected user exists in 
                 * the table (only if they donated the selected month)*/
                expression = "From = '" + reader.GetValue(0) + "'";
                foundRows = dt.Select(expression);
                if (foundRows.Length > 0)
                {
                    dr = dt.Rows[index];
                    dr.SetField("Year Total", reader.GetValue(1));
                    index += 1;
                }
            }

            reader.Close();
            hoursRecievedGrid.DataSource = dt;
            hoursRecievedGrid.DataBind();
            con.Close();
        }

        /*Binds which users received and how many hours the user gave to them in the 
         * selected month and how many that user has given the selected year*/
        protected void BindDonationGiven(string month, string year, string ID)
        {
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            //Get users that received donated hours from the selected user in the selected month and year and the month total
            string query = "SELECT Id, SUM(WeeklyHours) AS totalDonated FROM Stats WHERE Donate = @CurrentUser AND " +
                "Month = @Month AND Year = @Year GROUP BY Id ORDER BY Id";

            SqlCommand recieved = new SqlCommand(query, con);
            recieved.Parameters.AddWithValue("@CurrentUser", ID);
            recieved.Parameters.AddWithValue("@Month", month);
            recieved.Parameters.AddWithValue("@Year", year);

            SqlDataReader reader = recieved.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Recipient", typeof(string));
            dt.Columns.Add("Month Total", typeof(string));
            dt.Columns.Add("Year Total", typeof(float));
            DataRow dr;

            //Add a row for each unique user
            while (reader.Read())
            {
                dr = dt.NewRow();
                dr["Recipient"] = reader.GetValue(0);
                dr["Month Total"] = reader.GetValue(1).ToString();
                dr["Year Total"] = 0.00;
                dt.Rows.Add(dr);
            }
            reader.Close();

            //Get users that recieved donated hours from the selected user in the selected year and the year total
            query = "SELECT Id, SUM(WeeklyHours) AS totalDonated FROM Stats WHERE Donate = @CurrentUser AND " +
                "Year = @Year AND datalength(Donate)!=0 GROUP BY Id ORDER BY Id";

            recieved = new SqlCommand(query, con);
            recieved.Parameters.AddWithValue("@CurrentUser", ID);
            recieved.Parameters.AddWithValue("@Year", year);

            reader = recieved.ExecuteReader();
            int index = 0;
            DataRow[] foundRows;
            string expression;

            //Read the results
            while (reader.Read())
            {
                /*Only add the year total if the user who received hours from the selected user 
                 * exists in the table (only if they received the selected month)*/
                expression = "Recipient = '" + reader.GetValue(0) + "'";
                foundRows = dt.Select(expression);
                if (foundRows.Length > 0)
                {
                    dr = dt.Rows[index];
                    dr.SetField("Year Total", reader.GetValue(1));
                    index += 1;
                }
            }

            reader.Close();
            hoursGivenGrid.DataSource = dt;
            hoursGivenGrid.DataBind();
            con.Close();
        }
    }
}