using _395project.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _395project.dash.Admin
{
    public partial class GiveAbsence : System.Web.UI.Page
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
            string vars = (string)(Session["absence"]);
            if (vars != null)
            {
                string[] myStrings = vars.Split(',');
                Email.Text = myStrings[0];
                fromDate.Text = myStrings[1];
                toDate.Text = myStrings[2];
                Reason.Text = myStrings[3];
                Session.Remove("absence");
            }
        }
    }
}