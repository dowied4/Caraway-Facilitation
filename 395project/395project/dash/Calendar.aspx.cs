using _395project.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _395project.dash
{
    public partial class Calendar1 : System.Web.UI.Page
    {
        //Chooses the correct master page
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            ChooseMaster choose = new ChooseMaster();
            MasterPageFile = choose.GetMaster();
        }

        //Loads the calendar and scheduler
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadResources();
                DayPilotScheduler1.StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                DayPilotScheduler1.DataSource = DbGetEvents(DayPilotScheduler1.StartDate, 1);
                DayPilotScheduler1.DataBind();
                Calendar.SelectedDate = DayPilotScheduler1.StartDate;
                Label1.Text = Calendar.SelectedDate.ToShortDateString();
                Label1.Visible = true;
            }
            else
            {
                DayPilotScheduler1.StartDate = Calendar.SelectedDate;
                Label1.Text = Calendar.SelectedDate.ToShortDateString();
                DayPilotScheduler1.DataSource = DbGetEvents(DayPilotScheduler1.StartDate, 1);
                DayPilotScheduler1.DataBind();
            }
        }
        
        //Loads the classrooms for DayPilot
        private void LoadResources()
        {
            DayPilotScheduler1.Resources.Clear();

            SqlDataAdapter da = new SqlDataAdapter("SELECT [RoomId], [Room] FROM [Rooms]", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow r in dt.Rows)
            {
                string name = (string)r["Room"];
                string id = Convert.ToString(r["RoomId"]);

                DayPilotScheduler1.Resources.Add(name, id);
            }
        }

        //Gets all the events and fills the calendar from the database
        private DataTable DbGetEvents(DateTime start, int days)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [FullName], [StartTime], [EndTime], [RoomId], [VolunteerId] FROM [Calendar] WHERE NOT (([StartTime] <= @start) OR ([EndTime] >= @end))", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            da.SelectCommand.Parameters.AddWithValue("start", start);
            da.SelectCommand.Parameters.AddWithValue("end", start.AddDays(days));
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        //Changes the day when its selected on the calendar
        protected void Calendar_SelectionChanged(object sender, EventArgs e)
        {
            Page_Load(null, EventArgs.Empty);
        }

        //Gets the current user to display their facilitators
        protected void Page_Init(object sender, EventArgs e)
        {
            Facilitators.SelectParameters["CurrentUser"].DefaultValue = User.Identity.Name;
        }

        //Gets the RoomId for the selected room during signup
        protected int GetRoom(string room)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT [RoomId] FROM [Rooms] WHERE [Room] = @SelectedRoom", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            da.SelectCommand.Parameters.AddWithValue("SelectedRoom", RoomDropDown.Text);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow r in dt.Rows)
            {
                return (int)r["RoomId"];
            }
            return 1;

        }

        //Adds a signup to the database
        protected void SignUpButton_Click(object sender, EventArgs e)
        {   //Split the name into first and last name
            string[] name = FacilitatorDropDown.Text.Split(' ');
            //Get Start and End times
            DateTime Start = new DateTime(Calendar.SelectedDate.Year, Calendar.SelectedDate.Month, Calendar.SelectedDate.Day, 11, 00, 00);
            DateTime End = new DateTime(Calendar.SelectedDate.Year, Calendar.SelectedDate.Month, Calendar.SelectedDate.Day, 12, 00, 00);
            //Temporary, 3 default timeslots
            switch (TimeSlotDropDown.Text)
            {
                case "Morning":
                    Start = new DateTime(Calendar.SelectedDate.Year, Calendar.SelectedDate.Month, Calendar.SelectedDate.Day, 8, 30, 00);
                    End = new DateTime(Calendar.SelectedDate.Year, Calendar.SelectedDate.Month, Calendar.SelectedDate.Day, 12, 00, 00);
                    break;

                case "Lunch":
                    Start = new DateTime(Calendar.SelectedDate.Year, Calendar.SelectedDate.Month, Calendar.SelectedDate.Day, 12, 00, 00);
                    End = new DateTime(Calendar.SelectedDate.Year, Calendar.SelectedDate.Month, Calendar.SelectedDate.Day, 13, 00, 00);
                    break;

                case "Afternoon":
                    Start = new DateTime(Calendar.SelectedDate.Year, Calendar.SelectedDate.Month, Calendar.SelectedDate.Day, 13, 00, 00);
                    End = new DateTime(Calendar.SelectedDate.Year, Calendar.SelectedDate.Month, Calendar.SelectedDate.Day, 15, 45, 00);
                    break;
            }
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            conn.Open();
            string volunteer = "Insert into Calendar(Id, FacilitatorFirstName, FacilitatorLastName, StartTime, EndTime, RoomId) " +
                                "Values (@Id, @FirstName, @LastName, @Start, @End, @Room)";
            SqlCommand cmd = new SqlCommand(volunteer, conn);
            cmd.Parameters.AddWithValue("@Id", User.Identity.Name);
            cmd.Parameters.AddWithValue("@FirstName", name[0]);
            cmd.Parameters.AddWithValue("@LastName", name[1]);
            cmd.Parameters.AddWithValue("@Start", Start);
            cmd.Parameters.AddWithValue("@End", End);
            cmd.Parameters.AddWithValue("@Room", GetRoom(RoomDropDown.Text));
            cmd.ExecuteNonQuery();
            conn.Close();
            Page_Load(null, EventArgs.Empty);
        }
    }
}