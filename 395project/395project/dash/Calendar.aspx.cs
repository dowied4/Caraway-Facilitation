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
            ErrorMessage.Visible = false;
            if (!IsPostBack)
            {   
                LoadResources();
                DayPilotScheduler1.StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                DayPilotScheduler1.DataSource = DbGetEvents(DayPilotScheduler1.StartDate, 1);
                DayPilotScheduler1.DataBind();
                Calendar.SelectedDate = DayPilotScheduler1.StartDate;
                Label1.Text = Calendar.SelectedDate.ToShortDateString();
                Label1.Visible = true;
                BindTimeSlots();
                SetTimeTextBoxes();
            }
            else
            {
                DayPilotScheduler1.StartDate = Calendar.SelectedDate;
                Label1.Text = Calendar.SelectedDate.ToShortDateString();
                DayPilotScheduler1.DataSource = DbGetEvents(DayPilotScheduler1.StartDate, 1);
                DayPilotScheduler1.DataBind();
            }

        }

        //DataBind TimeSlotDropDown with TimeSlots and FieldTrips
        private void BindTimeSlots()
        {

            //Look for Field Trips to fill Time Slot DropDown
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            SqlDataAdapter adapter = new SqlDataAdapter();

            con.Open();

            SqlDataAdapter getFieldTrips = new SqlDataAdapter("Select Location From FieldTrips where CONVERT(DATE, StartTime) = @CurrentDate",
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            getFieldTrips.SelectCommand.Parameters.AddWithValue("@CurrentDate", Calendar.SelectedDate.Date);
            DataTable dt = new DataTable();
            getFieldTrips.Fill(dt);

            //Add regular time slots if no fieldtrips
            if (dt.Rows.Count == 0)
            {
                SqlDataAdapter getTimeSlots = new SqlDataAdapter("Select Location From TimeSlots order by StartTime",
                    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                getTimeSlots.Fill(dt);
            }

            TimeSlotDropDown.DataSource = dt;
            TimeSlotDropDown.DataTextField = "Location";
            TimeSlotDropDown.DataBind();
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
            BindTimeSlots();
            SetTimeTextBoxes();
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
        protected Boolean checkHours(String FirstName, String LastName, DateTime Current)
        {
            String[] checkCurrent = Current.ToString().Split(' ');
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            conn.Open();
            SqlCommand checkTimeSlot = new SqlCommand("SELECT * FROM Calendar WHERE Id = @CurrentUser AND" +
                " FacilitatorFirstName = @FirstName AND FacilitatorLastName = @LastName", conn);
            checkTimeSlot.Parameters.AddWithValue("@CurrentUser", User.Identity.Name);
            checkTimeSlot.Parameters.AddWithValue("@FirstName", FirstName);
            checkTimeSlot.Parameters.AddWithValue("@LastName", LastName);
            SqlDataReader reader = checkTimeSlot.ExecuteReader();
            while(reader.Read())
            {
                String[] time = reader["StartTime"].ToString().Split(' ');

                if(time[0] == checkCurrent[0])
                {
                    return false;
                }

            }
            
            return true;
        }

        //Adds a signup to the database
        protected void SignUpButton_Click(object sender, EventArgs e)            
        {
            //check if they are in upcoming table, and if the function returns true then give error message
            //Split the name into first and last name
            string[] name = FacilitatorDropDown.Text.Split(' ');
            //Get Start and End times
            int startHour = 0;
            int startMinute = 0;
            int endHour = 0;
            int endMinute = 0;
            
            //Checks for a properly formatted time in start time
            if (StartTimeTextBox.Text.Contains(':'))
                {
                string[] startTime = StartTimeTextBox.Text.Split(':');
                Int32.TryParse(startTime[0], out startHour);
                Int32.TryParse(startTime[1], out startMinute);
                }
            //Checks for a properly formatted time in end time
            if (EndTimeTextBox.Text.Contains(':'))
            {
                string[] endTime = EndTimeTextBox.Text.Split(':');
                Int32.TryParse(endTime[0], out endHour);
                Int32.TryParse(endTime[1], out endMinute);
            }
            //Adds to the database if startTime and endTime are properly formatted
            if (StartTimeTextBox.Text.Contains(':') && EndTimeTextBox.Text.Contains(':'))
            {
                DateTime Start = ChangeTime(startHour, startMinute);
                DateTime End = ChangeTime(endHour, endMinute);
                DateTime Current = new DateTime(Calendar.SelectedDate.Year, Calendar.SelectedDate.Month, Calendar.SelectedDate.Day);
                Boolean check = checkHours(name[0], name[1], Current);
                if (check == true)
                {
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
                } else
                {
                    ErrorMessage.Visible = true;
                    ErrorMessage.Text = "You already have a facilitator for that day!";
                }
            }


        }

        //Gets a DateTime object from the selected calendar date
        public DateTime ChangeTime(int hour, int minute)
        {
            return new DateTime(Calendar.SelectedDate.Year, Calendar.SelectedDate.Month, Calendar.SelectedDate.Day, hour, minute, 0);
        }

        //Runs SetTimeTextBoxes when the dropdown menu choice is changed
        protected void TimeSlotDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ind = TimeSlotDropDown.SelectedIndex;
            //If Custom Time Exists Remove it
            TimeSlotDropDown.SelectedIndex = 0;
            if (TimeSlotDropDown.Text.Contains("Custom"))
            {
                TimeSlotDropDown.Items.RemoveAt(0);
                ind -= 1;
            }

            TimeSlotDropDown.SelectedIndex = ind;
            SetTimeTextBoxes();
        }

        //Sets the text boxes with the start/end times of the selected time Slot
        private void SetTimeTextBoxes()
        {
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };
            //Gets the time from either StartTime or Fieldtrips
            string times = "Select CAST(StartTime AS time) as StartTime , CAST(EndTime AS TIME) as EndTime from FieldTrips " +
                            "where Location = @Location UNION Select CAST(StartTime AS time) as StartTime, " +
                            "CAST(EndTime AS TIME) as EndTime from dbo.TimeSlots where Location = @Location";
            SqlCommand getTimes = new SqlCommand(times, con);
            getTimes.Parameters.AddWithValue("@Location", TimeSlotDropDown.Text);
            con.Open();
            getTimes.ExecuteNonQuery();
            SqlDataReader dr = getTimes.ExecuteReader();
            while (dr.Read())
            {
                StartTimeTextBox.Text = (string)dr["StartTime"].ToString();

                dr["StartTime"].ToString();
                EndTimeTextBox.Text = (string)dr["EndTime"].ToString();
            }
            con.Close();
        }
        protected void onCancel(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
            SetTimeTextBoxes();
        }


        protected void onConfirm(object sender, EventArgs e)
        {
            
            //Get Start and End times
            int startHour = 0;
            int startMinute = 0;
            int endHour = 0;
            int endMinute = 0;
            string customLine = "Custom Time (";
            //If Custom Time Exists Remove it
            if (TimeSlotDropDown.Text.Contains("Custom"))
                TimeSlotDropDown.Items.RemoveAt(0);

            //Checks for a properly formatted time in start time
            if (StartTimeTextBox.Text.Contains(':'))
            {
                string[] startTime = StartTimeTextBox.Text.Split(':');
                Int32.TryParse(startTime[0], out startHour);
                Int32.TryParse(startTime[1], out startMinute);
            }
            //Checks for a properly formatted time in end time
            if (EndTimeTextBox.Text.Contains(':'))
            {
                string[] endTime = EndTimeTextBox.Text.Split(':');
                Int32.TryParse(endTime[0], out endHour);
                Int32.TryParse(endTime[1], out endMinute);
            }
            customLine += startHour;
            if (startMinute == 0)
                customLine += ":00" + " - ";
            else
                customLine += ":" + startMinute + " - ";

            customLine += endHour;
            if (endMinute == 0)
                customLine += ":00)";
            else
                customLine += ":" + endMinute + ")";

            TimeSlotDropDown.Items.Insert(0, new ListItem(customLine));
            TimeSlotDropDown.SelectedIndex = 0;
            ModalPopupExtender1.Hide();
        }
        protected void editClick(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }
    }
}