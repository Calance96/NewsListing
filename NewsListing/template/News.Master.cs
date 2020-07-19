using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsListing {
    public partial class News : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            if (Session["UserTab"] == null) {
                Session["UserTab"] = "Home";
                HomeBtn.CssClass += " active";
            } else {
                removeCurrentTabClass();
                switch(Session["UserTab"]) {
                    case "Home":
                        HomeBtn.CssClass += " active";
                        break;
                    case "Change password":
                        ChangePasswordBtn.CssClass += " active";
                        break;
                }
            }
            AdjustUIToLoginStatus();
        }

        private void AdjustUIToLoginStatus() {
            if (Session["TYPE"] != null) {
                userFullName.Text = Session["FNAME"] + " " + Session["LNAME"];
                LoginBtn.Visible = false;
                RegisterBtn.Visible = false;
                LogoutBtn.Visible = true;
                ChangePasswordBtn.Visible = true;
            }
        }

        private void removeCurrentTabClass() {
            HomeBtn.CssClass.Replace("active", "").Trim();
            ChangePasswordBtn.CssClass.Replace("active", "").Trim();
        }

        protected void LoginBtn_Click(object sender, EventArgs e) {
            Session["UserTab"] = "Login";
            Response.Redirect("~/Login.aspx");
        }

        protected void RegisterBtn_Click(object sender, EventArgs e) {
            Session["UserTab"] = "Register";
            Response.Redirect("~/Register.aspx");
        }

        protected void BrandLinkBtn_Click(object sender, EventArgs e) {
            Session["UserTab"] = "Home";
            Response.Redirect("~/Home.aspx");
        }

        protected void HomeBtn_Click(object sender, EventArgs e) {
            Session["UserTab"] = "Home";
            Response.Redirect("~/Home.aspx");
        }

        protected void ChangePasswordBtn_Click(object sender, EventArgs e) {
            Session["UserTab"] = "Change password";
            Response.Redirect("~/UserChangePassword.aspx");
        }

        protected void LogoutBtn_Click(object sender, EventArgs e) {
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}