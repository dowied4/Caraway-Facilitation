using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using _395project.Models;
using _395project.App_Code;

namespace _395project.Account
{
    public partial class TwoFactorAuthenticationSignIn : System.Web.UI.Page
    {
        //Chooses master page based on User Role
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            ChooseMaster choose = new ChooseMaster();
            MasterPageFile = choose.GetMaster();
        }

        private ApplicationSignInManager signinManager;
        private ApplicationUserManager manager;

        public TwoFactorAuthenticationSignIn()
        {
            manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = signinManager.GetVerifiedUserId<ApplicationUser, string>();
            if (userId == null)
            {
                Response.Redirect("/dash/Error", true);
            }
            var userFactors = manager.GetValidTwoFactorProviders(userId);
            Providers.DataSource = userFactors.Select(x => x).ToList();
            Providers.DataBind();            
        }

        protected void CodeSubmit_Click(object sender, EventArgs e)
        {
            bool rememberMe = false;
            bool.TryParse(Request.QueryString["RememberMe"], out rememberMe);
            
            var result = signinManager.TwoFactorSignIn<ApplicationUser, string>(SelectedProvider.Value, Code.Text, isPersistent: rememberMe, rememberBrowser: RememberBrowser.Checked);
            switch (result)
            {
                case SignInStatus.Success:
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    break;
                case SignInStatus.LockedOut:
                    Response.Redirect("/dash/Lockout");
                    break;
                case SignInStatus.Failure:
                default:
                    FailureText.Text = "Invalid code";
                    ErrorMessage.Visible = true;
                    break;
            }
        }

        protected void ProviderSubmit_Click(object sender, EventArgs e)
        {
            if (!signinManager.SendTwoFactorCode(Providers.SelectedValue))
            {
                Response.Redirect("/dash/Error");
            }

            var user = manager.FindById(signinManager.GetVerifiedUserId<ApplicationUser, string>());
            if (user != null)
            {
                var code = manager.GenerateTwoFactorToken(user.Id, Providers.SelectedValue);
            }

            SelectedProvider.Value = Providers.SelectedValue;
            sendcode.Visible = false;
            verifycode.Visible = true;
        }
    }
}