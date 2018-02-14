using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace _395project.App_Code
{
    public class ChooseMaster : Page
    {
        public string GetMaster()
        {
            if (User.IsInRole("SuperUser"))
                return "/Master/Main.master";
            else if (User.IsInRole("Admin"))
                return "/Master/BoardMember.master";
            else if (User.IsInRole("Teacher"))
                return "/Master/Teacher.master";
            else if (User.IsInRole("Facilitator"))
                return "/Master/Facilitator.master";
            else return null;
        }
    }
}