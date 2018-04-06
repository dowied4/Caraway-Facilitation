using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FusionCharts.Charts;

namespace _395project.dash.Stats
{
    public partial class Class : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            if (User.IsInRole("SuperUser"))
                MasterPageFile = "/Master/Main.master";
            else if (User.IsInRole("Admin"))
                MasterPageFile = "/Master/BoardMember.master";
            else if (User.IsInRole("Teacher"))
                MasterPageFile = "/Master/Teacher.master";
            else if (User.IsInRole("Facilitator"))
                MasterPageFile = "/Master/Facilitator.master";
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
            }

            String ID = User.Identity.Name;
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
            CreateDataXML();
        }
        protected void MonthBind()
        {
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

            for (int i = 1; i < 13; i++)
            {
                MonthDropDown.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));
            }
        }

        protected String getTotalClass(int Month, int Year, int Room, String ID)
        {
            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };
            con.Open();
            //Gets each week, the month and the year for each facilitator
            string upc = "SELECT COUNT(RoomId) AS totalRoom FROM Stats WHERE Month = @Month" +
              " AND Year = @Year AND RoomId = @Room";
            SqlCommand getup = new SqlCommand(upc, con);
            getup.Parameters.AddWithValue("@Room", Room);
            //getup.Parameters.AddWithValue("@Week", Week);
            getup.Parameters.AddWithValue("@Month", Month);
            getup.Parameters.AddWithValue("@Year", Year);
            //getup.Parameters.AddWithValue("@User", ID);
            SqlDataReader reader = getup.ExecuteReader();
            if (reader.Read())
            {
                if (!String.IsNullOrEmpty(reader["totalRoom"].ToString()))
                    return reader["totalRoom"].ToString();
            }

            con.Close();
            return "0";
        }

        protected void CreateDataXML()
        {
            String ID = User.Identity.Name;
            String hours;
            int month;
            int year;
            StringBuilder Title = new StringBuilder();
            //Label3.Text = getNumChildren().ToString();
            Title.Append("{\n" +
                "'chart': {" +
                "'caption': 'Frequency of All Facilitators in " + MonthDropDown.SelectedItem + "', " +
                "'xAxisName': 'Classrooms'," +
                "'yAxisName': 'Frequency'," +
                "'paletteColors': '#0075c2'," +
                "'bgColor': '#ffffff'," +
                "'borderAlpha': '20'," +
                "'canvasBorderAlpha': '0'," +
                "'usePlotGradientColor': '0'," +
                "'plotBorderAlpha': '10'," +
                "'placevaluesInside': '1'," +
                "'rotatevalues': '1'," +
                "'valueFontColor': '#ffffff'," +
                "'showXAxisLine': '1'," +
                "'xAxisLineColor': '#999999'," +
                "'divlineColor': '#999999'," +
                "'divLineIsDashed': '1'," +
                "'showAlternateHGridColor': '0'," +
                "'subcaptionFontBold': '0'," +
                "'subcaptionFontSize': '14'" +
                "},");

            //Label3.Text = getTotalHours(3, 3, 2018, ID);
            Title.Append("'data': [");
            for (int i = 1; i < 6; i++)
            {
                //week = Convert.ToInt32(WeekDropDown.SelectedValue);
                month = Convert.ToInt32(MonthDropDown.SelectedValue);
                year = Convert.ToInt32(YearDropDown.SelectedValue);
                hours = getTotalClass(month, year, i, ID);

                Title.Append("{");
                Title.Append("'label': ' " + ClassRoom(i) + "',");
                Title.Append("'value': '" + hours + "',");
                Title.Append(ClassColour(i));
                Title.Append("},");
            }
            Title.Append("],");
            Title.Append("}");
            Chart sales = new Chart("column2d", "myChart", "600", "350", "json", Title.ToString());
            //Render the chart.
            Literal2.Text = sales.Render();
        }

        protected String ClassColour(int Room)
        {
            switch (Room)
            {
                case 1: return "'color': '#0000ff'";
                case 2: return "'color': '#800080'";
                case 3: return "'color': '#008000'";
                case 4: return "'color': '#ff0000'"; 
                case 5: return "'color': '#808080'"; 
            }

            return "'color': '#008ee4'";
        }

        protected String ClassRoom(int Room)
        {

            switch(Room)
            {
                case 1: return "Blue";
                case 2: return "Purple";
                case 3: return "Green";
                case 4: return "Red";
                case 5: return "Grey";
            }
            return "0";
        }
    }

}