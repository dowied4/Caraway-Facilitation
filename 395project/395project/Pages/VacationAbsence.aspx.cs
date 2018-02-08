using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _395project.Pages
{
    public partial class VacationAbsence : System.Web.UI.Page
    {
        //Chooses master page based on User Role
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

        }
    }
}