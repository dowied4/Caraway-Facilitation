﻿using _395project.App_Code;
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
            string WeeklyHours = "SELECT SUM(S.WeeklyHours) as WeeklyHours FROM dbo.Stats AS S WHERE S.Id = @CurrentUser AND S.Week = @Week GROUP BY S.Id";
            SqlCommand getWeeklyHours = new SqlCommand(WeeklyHours, con);
            getWeeklyHours.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());
            getWeeklyHours.Parameters.AddWithValue("@Week", GetWeekOfMonth.GetWeekNumberOfMonth(DateTime.Now));

            SqlDataReader WeeklyHoursReader = getWeeklyHours.ExecuteReader();
            if (WeeklyHoursReader.Read())
                WeeklyHoursLabel.Text = WeeklyHoursReader["WeeklyHours"].ToString();
            WeeklyHoursReader.Close();

            //Get Monthly Hours
            string MonthlyHours = "(SELECT SUM(S.WeeklyHours) AS MonthlyHours FROM dbo.Stats AS S WHERE S.Id = @CurrentUser AND S.Month = @Month GROUP BY S.Id)";
            SqlCommand getMonthlyHours = new SqlCommand(MonthlyHours, con);
            getMonthlyHours.Parameters.AddWithValue("@CurrentUser", User.Identity.GetUserId());
            getMonthlyHours.Parameters.AddWithValue("@Month", DateTime.Now.Month);

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
            Label1.Text = startDate.ToString();
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