using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using _395project.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using _395project.App_Code;

namespace _395project.dash.Admin
{
    public partial class AccountStat : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            ChooseMaster choose = new ChooseMaster();
            MasterPageFile = choose.GetMaster();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            String ID = Request.QueryString["ID"];
            head.InnerHtml = "Account: " + ID;

            //Open Connection
            SqlConnection con = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };

            con.Open();
            ID = "sullivanr5@mymacewan.ca";
            //Get Facilitators
            string Facilitators = "SELECT (F.FirstName + ' '+ F.LastName) AS FacilitatorName FROM dbo.Facilitators AS F WHERE " +
                            "F.Id = @CurrentUser";
            SqlCommand getFacilitators = new SqlCommand(Facilitators, con);
            getFacilitators.Parameters.AddWithValue("@CurrentUser", ID);

            //adapter.SelectCommand = new SqlCommand(Facilitators, con);

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


        }
    }
}