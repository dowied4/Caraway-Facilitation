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
            string upc = "SELECT (F.FacilitatorFirstName + ' '+ F.FacilitatorLastName) AS FacilitatorName, F.StartTime, F.EndTime, F.RoomId FROM dbo.Calendar AS F WHERE " +
                            "F.Id = @CurrentUser and F.EndTime > @CurrentTime";
            SqlCommand getup = new SqlCommand(upc, con);
            getup.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());
            getup.Parameters.AddWithValue("@CurrentTime", DateTime.Now);
            adapter.SelectCommand = new SqlCommand(upc, con);

            //Execture the querey
            SqlDataReader upcompQuery = getup.ExecuteReader();
            GridView3.DataSource = upcompQuery;

            GridView3.DataBind();
            if (!upcompQuery.HasRows) {
                spacer.Controls.Add(new LiteralControl("<br />"));
                Label1.Visible = true;

            } 
            upcompQuery.Close();


            
            //Completed Hours
            string comp = "SELECT (F.FacilitatorFirstName + ' '+ F.FacilitatorLastName) AS FacilitatorName, F.StartTime, " +
                            "F.EndTime, F.RoomId FROM dbo.Calendar AS F WHERE " +
                            "F.Id = @CurrentUser and F.EndTime < @CurrentTime";
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


            //Get Weekly Hours
            string WeeklyHours = "SELECT SUM(S.WeeklyHours) as WeeklyHours FROM dbo.Stats AS S WHERE S.Id = @CurrentUser AND S.Week = @Week " +
                "AND S.Month = @Month and S.Year = @Year GROUP BY S.Id";
            SqlCommand getWeeklyHours = new SqlCommand(WeeklyHours, con);
            getWeeklyHours.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());
            getWeeklyHours.Parameters.AddWithValue("@Week", GetWeekOfMonth.GetWeekNumberOfMonth(DateTime.Now));
            getWeeklyHours.Parameters.AddWithValue("@Month", DateTime.Now.Month);
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
            //checks to see if time now is greater than startdate
            /*if(DateTime.Now >= startDate)
            {
                //runs script on startup
                ScriptManager.RegisterStartupScript(this, GetType(), "Confirm", "Confirm();", true);
                
                
                //problem is fetching the data from the javascript and using it in OnConfirm() function
                //a solution would be to create a button that appears on the page after the volunteeting date to confirm if they have went or not but issue with that
                // is that they may forgot to look at it

            }*/

            //close connection
            con.Close();

            
        }
        //Chooses master page based on User Role
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            ChooseMaster choose = new ChooseMaster();
            MasterPageFile = choose.GetMaster();
        }

        //this function is the 
        protected void ConfirmButton(object sender, System.EventArgs e)
        {
            //Get the button that raised the event
            LinkButton btn = (LinkButton)sender;
            
            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            //Grid row number
            int num = gvr.RowIndex;
            int WeekOfMonth = GetWeekOfMonth.GetWeekNumberOfMonth(Convert.ToDateTime(gvr.Cells[2].Text));
            //Label1.Text = WeekOfMonth.ToString();

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

            float totalHours = (float) (dt1 - dt).TotalHours;
            
            name = gvr.Cells[0].Text.Split(' ');
            firstName = name[0];
            lastName = name[1];

            startDate = gvr.Cells[1].Text.Split(' ');
            startTime = startDate[1];
            endDate = gvr.Cells[2].Text.Split(' ');
            endTime = endDate[1];
            array = endDate[0].Split('/');
            month = array[0];
            year = array[2];


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            string CompletedHours = ("INSERT INTO Stats (Id, FacilitatorFirstName, FacilitatorLastName, " +
                                      "Week, Month, Year, WeeklyHours) VALUES (@CurrentUser, @FirstName, " +
                                      "@LastName, @Week, @Month, @Year, @WeeklyHours); " +
                                      "DELETE FROM Calendar WHERE Id = @CurrentUser and FacilitatorFirstName = @FirstName and FacilitatorLastName = @LastName " +
                                      "and StartTime = @StartTime and EndTime = @EndTime and RoomId = @Room");
            SqlCommand GetCompletedHours = new SqlCommand(CompletedHours, con);
            GetCompletedHours.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());
            GetCompletedHours.Parameters.AddWithValue("@FirstName", firstName);
            GetCompletedHours.Parameters.AddWithValue("@LastName", lastName);
            GetCompletedHours.Parameters.AddWithValue("@Week", WeekOfMonth);
            GetCompletedHours.Parameters.AddWithValue("@Month", month);
            GetCompletedHours.Parameters.AddWithValue("@Year", year);
            GetCompletedHours.Parameters.AddWithValue("@WeeklyHours", totalHours);
            GetCompletedHours.Parameters.AddWithValue("@StartTime", dt);
            GetCompletedHours.Parameters.AddWithValue("@EndTime", dt1);
            GetCompletedHours.Parameters.AddWithValue("@Room", gvr.Cells[3].Text);
            SqlDataReader addHoursReader = GetCompletedHours.ExecuteReader();

            Page_Load(null, EventArgs.Empty);
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
            string CompletedHours =  ("DELETE FROM Calendar WHERE Id = @CurrentUser " +
                                        "and FacilitatorFirstName = @FirstName and FacilitatorLastName = @LastName " +
                                        "and StartTime = @StartTime and EndTime = @EndTime and RoomId = @Room");
            SqlCommand GetCompletedHours = new SqlCommand(CompletedHours, con);
            GetCompletedHours.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());
            GetCompletedHours.Parameters.AddWithValue("@FirstName", firstName);
            GetCompletedHours.Parameters.AddWithValue("@LastName", lastName);
            GetCompletedHours.Parameters.AddWithValue("@StartTime", dt);
            GetCompletedHours.Parameters.AddWithValue("@EndTime", dt1);
            GetCompletedHours.Parameters.AddWithValue("@Room", gvr.Cells[3].Text);
            SqlDataReader addHoursReader = GetCompletedHours.ExecuteReader();

            Page_Load(null, EventArgs.Empty);


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

    }
}