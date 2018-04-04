using _395project.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using FusionCharts.Charts;
using System.Web.UI.WebControls;

namespace _395project.dash.Stats
{
    public partial class Date : System.Web.UI.Page
    {
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
            //Label3.Text = getTotalHours(3, 3, 2018, ID);
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

        protected void CreateDataXML()
        {
            StringBuilder Main = new StringBuilder();
            StringBuilder Title = new StringBuilder();
            StringBuilder Labels = new StringBuilder();
            StringBuilder Line = new StringBuilder();
            
            Title.Append("{\n" +
                "'chart': {"+
                "'caption': 'Monthly revenue for last year', " +
                "'subcaption': 'Harrys SuperMart', " +
                "'xAxisName': 'Month'," + 
                "'yAxisName': 'revenues'," +
                "'numberPrefix': '$'," +
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

            Title.Append("'data': [" +
        "{" +
                "'label': 'Jan'," +
            "'value': '420000'" +
        "}," +
        "{" +
                "'label': 'Feb'," +
            "'value': '810000'" +
        "}," +
        "]," +
        "}");
            //Label3.Text = Title.ToString();
            Chart sales = new Chart("column2d", "myChart", "600", "350", "json", Title.ToString());
            //Render the chart.
            Literal1.Text = sales.Render();
        }
        protected String getTotalHours(int Month, int Week, int Year, String ID)
        {
            
            //int firstWeekOfMonth = GetWeekOfMonth.FirstMonday(Convert.ToInt32(Month));
            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            //Gets each week, the month and the year for each facilitator
            string upc = "SELECT SUM(WeeklyHours) AS totalWeekly FROM Stats WHERE Id = @User AND Month = @Month" +
              " AND Year = @Year AND WeekOfMonth = @Week";
            SqlCommand getup = new SqlCommand(upc, con);
            getup.Parameters.AddWithValue("@Week", Week);
            getup.Parameters.AddWithValue("@Month", Month);
            getup.Parameters.AddWithValue("@Year", Year);
            getup.Parameters.AddWithValue("@User", ID);
            SqlDataReader reader = getup.ExecuteReader();
            if (reader.Read())
            {
                if (!String.IsNullOrEmpty(reader["totalWeekly"].ToString()))
                    return reader["totalWeekly"].ToString();
            }

            con.Close();
            return "0";
        }

     }
}