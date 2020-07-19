using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsListing.template {
    public partial class Author : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            if (Session["UID"] == null) {
                Response.Redirect("~/Login.aspx");
                return;
            }

            userFullName.Text = Session["FNAME"] + " " + Session["LNAME"];

            if (Session["AuthorTab"] == null) {
                Session["AuthorTab"] = "Manage article";
                AuthorHomeBtn.CssClass += " active";
            } else {
                removeCurrentTabClass();
                switch (Session["AuthorTab"]) {
                    case "View all news":
                        NewsHomeBtn.CssClass += " active";
                        break;
                    case "Manage article":
                        AuthorHomeBtn.CssClass += " active";
                        break;
                    case "Add article":
                        AddNewsBtn.CssClass += " active";
                        break;
                    case "Add author":
                        AddAuthorBtn.CssClass += " active";
                        break;
                    case "Change password":
                        ChangePasswordBtn.CssClass += " active";
                        break;
                }
            }

            if (Session["TYPE"].ToString() == "1")
                AddAuthorBtn.Visible = true;
        }

        private void removeCurrentTabClass() {
            NewsHomeBtn.CssClass.Replace("active ", "").Trim();
            AuthorHomeBtn.CssClass.Replace("active", "").Trim();
            AddNewsBtn.CssClass.Replace("active", "").Trim();
            AddAuthorBtn.CssClass.Replace("active ", "").Trim();
            ChangePasswordBtn.CssClass.Replace("active ", "").Trim();
        }

        protected void AuthorHome_Click(object sender, EventArgs e) {
            Session["AuthorTab"] = "Manage article";
            Response.Redirect("~/author/AuthorDashboard.aspx");
        }

        protected void AddNewsBtn_Click(object sender, EventArgs e) {
            Session["AuthorTab"] = "Add article";
            Response.Redirect("~/author/AddArticle.aspx");
        }

        protected void AddAuthorBtn_Click(object sender, EventArgs e) {
            Session["AuthorTab"] = "Add author";
            Response.Redirect("~/author/AddAuthor.aspx");
        }

        protected void ChangePasswordBtn_Click(object sender, EventArgs e) {
            Session["AuthorTab"] = "Change password";
            Response.Redirect("~/author/ChangePassword.aspx");
        }

        protected void NewsHomeBtn_Click(object sender, EventArgs e) {
            Session["AuthorTab"] = "View all news";
            Response.Redirect("~/author/AuthorHome.aspx");
        }

        protected void LogoutBtn_Click(object sender, EventArgs e) {
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}