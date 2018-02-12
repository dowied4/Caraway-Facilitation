using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using _395project.Models;
using _395project.App_Code;

namespace _395project.Account
{
    public partial class AddPhoneNumber : System.Web.UI.Page
    {
        //Chooses master page based on User Role
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            ChooseMaster choose = new ChooseMaster();
            MasterPageFile = choose.GetMaster();
        }
        protected void PhoneNumber_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var code = manager.GenerateChangePhoneNumberToken(User.Identity.GetUserId(), PhoneNumber.Text);
            if (manager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = PhoneNumber.Text,
                    Body = "Your security code is " + code
                };

                manager.SmsService.Send(message);
            }

            Response.Redirect("/dash/VerifyPhoneNumber?PhoneNumber=" + HttpUtility.UrlEncode(PhoneNumber.Text));
        }
    }
}