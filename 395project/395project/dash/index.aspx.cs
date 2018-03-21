using _395project.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Globalization;
using System.Data;

namespace _395project.Pages
{


    public partial class index : System.Web.UI.Page

    {
        protected void Page_Load(object sender, EventArgs e)
        {

            DateTime startDate;
            //startdate would be grabbed from database
            startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 45, 6);



            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            SqlDataAdapter adapter = new SqlDataAdapter();

            con.Open();

            //Upcoming Hours
            string upc = "SELECT (F.FacilitatorFirstName + ' '+ F.FacilitatorLastName) AS FacilitatorName, F.StartTime, F.EndTime, R.Room FROM dbo.Calendar AS F, Rooms as R WHERE " +
                            "F.Id = @CurrentUser and F.EndTime > @CurrentTime and F.RoomId = R.RoomId";
            SqlCommand getup = new SqlCommand(upc, con);
            getup.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());
            getup.Parameters.AddWithValue("@CurrentTime", DateTime.Now);
            adapter.SelectCommand = new SqlCommand(upc, con);

            //Execture the querey
            SqlDataReader upcompQuery = getup.ExecuteReader();
            GridView3.DataSource = upcompQuery;

            GridView3.DataBind();
            if (!upcompQuery.HasRows)
            {
                spacer.Controls.Add(new LiteralControl("<br />"));
                Label1.Visible = true;

            }
            upcompQuery.Close();



            //Completed Hours
            string comp = "SELECT (F.FacilitatorFirstName + ' '+ F.FacilitatorLastName) AS FacilitatorName, F.StartTime, " +
                            "F.EndTime, R.Room FROM dbo.Calendar AS F, Rooms as R WHERE " +
                            "F.Id = @CurrentUser and F.EndTime < @CurrentTime and R.RoomId = F.RoomId";
            SqlCommand getComp = new SqlCommand(comp, con);
            getComp.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());
            getComp.Parameters.AddWithValue("@CurrentTime", DateTime.Now);

            adapter.SelectCommand = new SqlCommand(comp, con);

            //Execture the querey
            SqlDataReader completedQuery = getComp.ExecuteReader();
            //Assign results
            GridView4.DataSource = completedQuery;
            //Bind the data
            GridView4.DataBind();
            if (!completedQuery.HasRows)
            {
                spacer.Controls.Add(new LiteralControl("<br />"));
                Label4.Visible = true;
            }
            completedQuery.Close();


            //Get Facilitators
            string Facilitators = "SELECT (F.FirstName + ' '+ F.LastName) AS FacilitatorName FROM dbo.Facilitators AS F WHERE " +
                            "F.Id = @CurrentUser";
            SqlCommand getFacilitators = new SqlCommand(Facilitators, con);
            getFacilitators.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());

            adapter.SelectCommand = new SqlCommand(Facilitators, con);

            //Execture the querey
            SqlDataReader facilitatorReader = getFacilitators.ExecuteReader();
            //Assign results
            GridView1.DataSource = facilitatorReader;
            //Bind the data
            GridView1.DataBind();
            facilitatorReader.Close();

            //Get children
            string Children = "SELECT (C.FirstName + ' '+ C.LastName) AS Name, C.Grade as Grade, C.Class as Classroom FROM dbo.Children AS C WHERE " +
                            "C.Id = @CurrentUser";
            SqlCommand getChildren = new SqlCommand(Children, con);
            getChildren.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());

            SqlDataReader childReader = getChildren.ExecuteReader();
            //Assign results
            GridView2.DataSource = childReader;
            //Bind the data
            GridView2.DataBind();
            childReader.Close();
           /*Label1.Visible = true;
            Label1.Text = GetWeekOfMonth.GetWeekOfYear(new DateTime(2018, 2,28)).ToString();*/

            //Get Weekly Hours
            string WeeklyHours = "SELECT SUM(S.WeeklyHours) as WeeklyHours FROM dbo.Stats AS S WHERE S.Id = @CurrentUser AND S.WeekOfYear = @WeekOfYear " +
                " and S.Year = @Year GROUP BY S.Id";
            SqlCommand getWeeklyHours = new SqlCommand(WeeklyHours, con);
            getWeeklyHours.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());
            getWeeklyHours.Parameters.AddWithValue("@WeekOfYear", GetWeekOfMonth.GetWeekOfYear(DateTime.Now));
            getWeeklyHours.Parameters.AddWithValue("@Year", DateTime.Now.Year);


            SqlDataReader WeeklyHoursReader = getWeeklyHours.ExecuteReader();
            if (WeeklyHoursReader.Read())
                WeeklyHoursLabel.Text = WeeklyHoursReader["WeeklyHours"].ToString();
            WeeklyHoursReader.Close();

            //Get Monthly Hours
            string MonthlyHours = "(SELECT SUM(S.WeeklyHours) AS MonthlyHours FROM dbo.Stats AS S WHERE S.Id = @CurrentUser AND S.Month = @Month " +
                "AND S.Year = @Year GROUP BY S.Id)";
            SqlCommand getMonthlyHours = new SqlCommand(MonthlyHours, con);
            getMonthlyHours.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());
            getMonthlyHours.Parameters.AddWithValue("@Month", DateTime.Now.Month);
            getMonthlyHours.Parameters.AddWithValue("@Year", DateTime.Now.Year);

            SqlDataReader MonthlyHoursReader = getMonthlyHours.ExecuteReader();
            if (MonthlyHoursReader.Read())
                MonthlyHoursLabel.Text = MonthlyHoursReader["MonthlyHours"].ToString();
            MonthlyHoursReader.Close();

            con.Close();


        }
        //Chooses master page based on User Role
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            ChooseMaster choose = new ChooseMaster();
            MasterPageFile = choose.GetMaster();
        }





        protected void ConfirmButton(object sender, System.EventArgs e)
        {
            //Get the button that raised the event
            LinkButton btn = (LinkButton)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            //Grid row number
            int num = gvr.RowIndex;
            int WeekOfMonth = GetWeekOfMonth.GetWeekNumberOfMonth(Convert.ToDateTime(gvr.Cells[2].Text));
           /* Label1.Visible = true;
            Label1.Text = WeekOfMonth.ToString(); */

            string[] name;
            string firstName;
            string lastName;
            string startTime;
            string endTime;
            string[] startDate;
            string[] endDate;
            string[] array;
            string month;
            string year;
            DateTime dt = Convert.ToDateTime(gvr.Cells[1].Text);
            DateTime dt1 = Convert.ToDateTime(gvr.Cells[2].Text);

            float totalHours = (float)(dt1 - dt).TotalHours;

            name = gvr.Cells[0].Text.Split(' ');
            firstName = name[0];
            lastName = name[1];

            startDate = gvr.Cells[1].Text.Split(' ');
            startTime = startDate[1];
            endDate = gvr.Cells[2].Text.Split(' ');
            endTime = endDate[1];
            //fixes the case where time is split on "-" or "/"
            if (endDate[0].Contains('-'))
            {
                array = endDate[0].Split('-');
                month = array[1];
                year = array[0];
            }
            else
            {
                array = endDate[0].Split('/');
                month = array[0];
                year = "20"+array[2];
            }


            //Checks if the timeslot is the lunch hour and gives double time if it is
            if((TimeSpan.Compare(dt.TimeOfDay, new TimeSpan(12,0,0)) == 0 || 
                TimeSpan.Compare(dt.TimeOfDay, new TimeSpan(12, 0, 0)) == -1) &&
               (TimeSpan.Compare(dt1.TimeOfDay, new TimeSpan(13, 0, 0)) == 0 ||
                TimeSpan.Compare(dt1.TimeOfDay, new TimeSpan(13, 0, 0)) == 1))
            {

                //Look for Field Trips (Only double time for lunch on regular days)
                SqlConnection fieldTripCheck = new SqlConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
                };

                SqlDataAdapter adapter = new SqlDataAdapter();

                fieldTripCheck.Open();

                SqlDataAdapter getFieldTrips = new SqlDataAdapter("Select Location From FieldTrips where CONVERT(DATE, StartTime) = @CurrentDate",
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                getFieldTrips.SelectCommand.Parameters.AddWithValue("@CurrentDate", dt.Date);
                DataTable table = new DataTable();
                getFieldTrips.Fill(table);

                //If not a fieldtrip give double time for lunch hour
                if (table.Rows.Count == 0)
                {
                    totalHours += 1;
                }
                fieldTripCheck.Close();
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            string CompletedHours = ("INSERT INTO Stats (Id, FacilitatorFirstName, FacilitatorLastName, " +
                                      "WeekOfMonth, WeekOfYear, Month, Year, WeeklyHours) VALUES (@CurrentUser, @FirstName, " +
                                      "@LastName, @WeekOfMonth, @WeekOfYear, @Month, @Year, @WeeklyHours); " +
                                      "DELETE FROM Calendar WHERE Id = @CurrentUser and FacilitatorFirstName = @FirstName and FacilitatorLastName = @LastName " +
                                      "and StartTime = @StartTime and EndTime = @EndTime and RoomId = @Room");
            SqlCommand GetCompletedHours = new SqlCommand(CompletedHours, con);
            GetCompletedHours.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());
            GetCompletedHours.Parameters.AddWithValue("@FirstName", firstName);
            GetCompletedHours.Parameters.AddWithValue("@LastName", lastName);
            GetCompletedHours.Parameters.AddWithValue("@WeekOfMonth", WeekOfMonth);
            GetCompletedHours.Parameters.AddWithValue("@WeekOfYear", GetWeekOfMonth.GetWeekOfYear(DateTime.Parse(gvr.Cells[2].Text)));
            GetCompletedHours.Parameters.AddWithValue("@Month", month);
            GetCompletedHours.Parameters.AddWithValue("@Year", year);
            GetCompletedHours.Parameters.AddWithValue("@WeeklyHours", totalHours);
            GetCompletedHours.Parameters.AddWithValue("@StartTime", dt);
            GetCompletedHours.Parameters.AddWithValue("@EndTime", dt1);
            GetCompletedHours.Parameters.AddWithValue("@Room", GetRoomId(gvr.Cells[3].Text));
            SqlDataReader addHoursReader = GetCompletedHours.ExecuteReader();
            //Page_Load(null, EventArgs.Empty);
            Response.Redirect(Request.RawUrl);
        }

        protected void DeclineButton(object sender, System.EventArgs e)
        {
            //Get the button that raised the event
            LinkButton btn = (LinkButton)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            //Grid row number
            int num = gvr.RowIndex;
            int WeekOfMonth = GetWeekOfMonth.GetWeekNumberOfMonth(Convert.ToDateTime(gvr.Cells[2].Text));

            string[] name;
            string firstName;
            string lastName;
            DateTime dt = Convert.ToDateTime(gvr.Cells[1].Text);
            DateTime dt1 = Convert.ToDateTime(gvr.Cells[2].Text);
            name = gvr.Cells[0].Text.Split(' ');
            firstName = name[0];
            lastName = name[1];


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            string CompletedHours = ("DELETE FROM Calendar WHERE Id = @CurrentUser " +
                                        "and FacilitatorFirstName = @FirstName and FacilitatorLastName = @LastName " +
                                        "and StartTime = @StartTime and EndTime = @EndTime and RoomId = @Room");
            SqlCommand GetCompletedHours = new SqlCommand(CompletedHours, con);
            GetCompletedHours.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());
            GetCompletedHours.Parameters.AddWithValue("@FirstName", firstName);
            GetCompletedHours.Parameters.AddWithValue("@LastName", lastName);
            GetCompletedHours.Parameters.AddWithValue("@StartTime", dt);
            GetCompletedHours.Parameters.AddWithValue("@EndTime", dt1);
            GetCompletedHours.Parameters.AddWithValue("@Room", GetRoomId(gvr.Cells[3].Text));
            SqlDataReader addHoursReader = GetCompletedHours.ExecuteReader();

            //Page_Load(null, EventArgs.Empty);
            Response.Redirect(Request.RawUrl);

        }


        public void OnConfirm(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked Yes!')", true);
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked No!')", true);
            }
        }

        //Takes the room name as a string (As it is stored in the gridview and gets the room Id for inserting into stats DB
        private int GetRoomId(string roomName)
        {
            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            SqlDataAdapter adapter = new SqlDataAdapter();

            con.Open();

            //Get Weekly Hours
            string roomId = "SELECT [RoomId] FROM [Rooms] WHERE [Room] = @SelectedRoom";
            SqlCommand getWeeklyHours = new SqlCommand(roomId, con);
            getWeeklyHours.Parameters.AddWithValue("@SelectedRoom", roomName);


            SqlDataReader roomIdReader = getWeeklyHours.ExecuteReader();
            if (roomIdReader.Read())
                return (int)roomIdReader["RoomId"];

            roomIdReader.Close();
            return 0;


        }


    }
}