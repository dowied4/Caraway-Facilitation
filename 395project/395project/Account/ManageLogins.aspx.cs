using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace _395project.Account
{
    public partial class ManageLogins : System.Web.UI.Page
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
        protected string SuccessMessage
        {
            get;
            private set;
        }
        protected bool CanRemoveExternalLogins
        {
            get;
            private set;
        }

        private bool HasPassword(ApplicationUserManager manager)
        {
            return manager.HasPassword(User.Identity.GetUserId());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            CanRemoveExternalLogins = manager.GetLogins(User.Identity.GetUserId()).Count() > 1;

            SuccessMessage = String.Empty;
            successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
        }

        public IEnumerable<UserLoginInfo> GetLogins()
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var accounts = manager.GetLogins(User.Identity.GetUserId());
            CanRemoveExternalLogins = accounts.Count() > 1 || HasPassword(manager);
            return accounts;
        }

        public void RemoveLogin(string loginProvider, string providerKey)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var result = manager.RemoveLogin(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            string msg = String.Empty;
            if (result.Succeeded)
            {
                var user = manager.FindById(User.Identity.GetUserId());
                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                msg = "?m=RemoveLoginSuccess";
            }
            Response.Redirect("~/Account/ManageLogins" + msg);
        }
    }
}