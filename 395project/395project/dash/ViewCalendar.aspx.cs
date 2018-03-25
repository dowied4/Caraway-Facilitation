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
    public partial class ViewCalendar : System.Web.UI.Page
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

        //Gets a DateTime object from the selected calendar date
        public DateTime ChangeTime(int hour, int minute)
        {
            return new DateTime(Calendar.SelectedDate.Year, Calendar.SelectedDate.Month, Calendar.SelectedDate.Day, hour, minute, 0);
        }
        
    }
}