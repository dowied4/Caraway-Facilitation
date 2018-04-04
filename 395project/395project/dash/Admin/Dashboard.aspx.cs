using _395project.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FusionCharts.Charts;
using System.Text;

namespace _395project.dash.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        //Chooses master page based on User Role
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            ChooseMaster choose = new ChooseMaster();
            MasterPageFile = choose.GetMaster();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBindFacilitatorAbsence();



            //The data to plot the sample multi-series chart is contained in the arrData array
            // having six rows and  three columns.
            // Each row stores the sales data for each product.
            // The first column stores the data labels for the products, and the second and
            // third columns store the current year and previous year sales figures, respectively.

            object[,] arrData = new object[6, 3];

            //Store product labels in the first column.
            arrData[0, 0] = "Product A";
            arrData[1, 0] = "Product B";
            arrData[2, 0] = "Product C";
            arrData[3, 0] = "Product D";
            arrData[4, 0] = "Product E";
            arrData[5, 0] = "Product F";

            //Store sales data for the current year in the second column.
            arrData[0, 1] = 567500;
            arrData[1, 1] = 815300;
            arrData[2, 1] = 556800;
            arrData[3, 1] = 734500;
            arrData[4, 1] = 676800;
            arrData[5, 1] = 648500;

            //Store sales data for previous year in the third column.
            arrData[0, 2] = 367300;
            arrData[1, 2] = 584500;
            arrData[2, 2] = 754000;
            arrData[3, 2] = 456300;
            arrData[4, 2] = 754500;
            arrData[5, 2] = 437600;

            // To render a chart from the above data, you will have to convert this data into

            // JSON data for the chart.

            // You can do this using string concatenation.

            //Create objects of the StringBuilder class to store the converted JSON strings.

            // Define the jsonData object to store the entire chart data as a JSON string.

            StringBuilder jsonData = new StringBuilder();

            // Define the categories object to store the product labels converted to
            // JSON strings.
            StringBuilder categories = new StringBuilder();

            //Define the currentYear and previousYear objects to store
            // the converted current and previous years sales data, respectively.
            StringBuilder currentYear = new StringBuilder();
            StringBuilder previousYear = new StringBuilder();

            //Initialize the chart object. Define  the chart-level attributes and
            // append them as strings to the chart data in the jsonData object
            // using the Append method.

            jsonData.Append("{" +

                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +

                    "'caption': 'Sales by Product'," +
                    "'numberPrefix': '$'," +
                    "'formatNumberScale': '1'," +
                    "'placeValuesInside': '1'," +
                    "'decimals': '0'" +
                "},");

            //Initialize the categories and category object arrays.

            //Using the Append method, append the initial part of array definition as

            // string to the categories StringBuilder object.
            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            //Using the Append method, append the dataset level attributes and the initial part of
            // the data object array definition to the
            // currentYear StringBuilder object.

            currentYear.Append("{" +
                        // dataset level attributes
                        "'seriesname': 'Current Year'," +
                        "'data': [");

            //Using the Append method, append the dataset level attributes and the initial part of

            // the data object array definition to the

            // previousYear StringBuilder object.

            previousYear.Append("{" +
                        // dataset level attributes
                        "'seriesname': 'Previous Year'," +
                        "'data': [");

            //Iterate through the data contained  in the arrData array.

            for (int i = 0; i < arrData.GetLength(0); i++)
            {
                if (i > 0)
                {
                    categories.Append(",");
                    currentYear.Append(",");
                    previousYear.Append(",");
                }

                //Append individual category-level data to the categories object.

                categories.AppendFormat("{{" +
                        // category level attributes
                        "'label': '{0}'" +
                    "}}", arrData[i, 0]);

                //Append current year’s sales data for each product to the currentYear object.

                currentYear.AppendFormat("{{" +
                        // data level attributes
                        "'value': '{0}'" +
                    "}}", arrData[i, 1]);

                //Append previous year’s sales data for each product to the currentYear object.

                previousYear.AppendFormat("{{" +
                          // data level attributes
                          "'value': '{0}'" +
                      "}}", arrData[i, 2]);
            }

            //Append as strings the closing part of the array definition of the

            // categories object array.

            categories.Append("]" +
                    "}" +
                "],");

            //Append as strings the closing part of the array definition of the data object array to the currentYear and previousYear objects.

            currentYear.Append("]" +
                    "},");
            previousYear.Append("]" +
                    "}");

            //Append the complete chart data converted to a string to the jsonData object.

            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            jsonData.Append(currentYear.ToString());
            jsonData.Append(previousYear.ToString());
            jsonData.Append("]" +
                    "}");

            // Initialize the chart.

            Chart sales = new Chart("msline", "myChart", "600", "350", "json", jsonData.ToString());
            // Render the chart.
            Literal1.Text = sales.Render();
        }

        protected void DataBindFacilitatorAbsence()
        {

            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();

            //Get facilitators that are currently on approved leave
            string upc = "SELECT Email, CONVERT(DATE, StartDate) as 'Start Date', CONVERT(DATE, EndDate) as 'End Date' " +
                "FROM Absence WHERE StartDate <= @CurrentDate " +
                "AND EndDate >= @CurrentDate AND Confirmed = 1";
            SqlCommand getup = new SqlCommand(upc, con);
            getup.Parameters.AddWithValue("@CurrentDate", DateTime.Now);


            //Execture the querey
            SqlDataReader upcompQuery = getup.ExecuteReader();
            GridView1.DataSource = upcompQuery;

            GridView1.DataBind();
            con.Close();
        }
    }
}