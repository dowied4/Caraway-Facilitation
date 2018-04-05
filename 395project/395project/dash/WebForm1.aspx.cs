using _395project.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _395project.dash
{
    public partial class WebForm1 : System.Web.UI.Page
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
            //opens a connection to the server
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };
            con.Open();

            getRoom("Blue", 1, Color.DodgerBlue, con, blueRoom);
            getRoom("Purple", 2, Color.Purple, con, purpleRoom);
            getRoom("Green", 3, Color.ForestGreen, con, greenRoom);
            getRoom("Red", 4, Color.Red, con, redRoom);
            getRoom("Grey", 5, Color.Gray,con, greenRoom);
            con.Close();
        }

        protected void getRoom(string room, int roomId, Color textColor, SqlConnection con, PlaceHolder roomHolder)
        {
            string query = "Select FacilitatorFirstName, FacilitatorLastName, StartTime FROM Calendar WHERE RoomId = @RoomId AND (StartTime > @startTodayTime AND StartTime < @endTodayTime) Order BY StartTime";
            DateTime today = DateTime.Now;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@RoomId", roomId);
            cmd.Parameters.AddWithValue("@startTodayTime", new DateTime(today.Year, today.Month, today.Day, 0, 0, 0));
            cmd.Parameters.AddWithValue("@endTodayTime", new DateTime(today.Year, today.Month, today.Day, 23, 59, 59));


            //Execute the querey
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Label person = makeLabel(room, reader, textColor);
                
                roomHolder.Controls.Add(person);
                roomHolder.Controls.Add(new LiteralControl("<br />"));
            }
            reader.Close();
        }

        private Label makeLabel(string color, SqlDataReader reader, Color textColor)
        {
            Label personLabel = new Label();
            string[] vars = reader.GetValue(2).ToString().Split(' ');
            if (vars[2].Length == 7)
            {
                vars[1] = vars[1] + " ";
            }
            personLabel.Text = vars[1] + "  " + vars[2] + " " + reader.GetValue(0).ToString() + " " + reader.GetValue(1).ToString();
            personLabel.Attributes.Add("class", "stats-info");
            personLabel.ForeColor = textColor;


            return personLabel;
        }
    }
}