using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsListing.author {
    public partial class AuthorDashboard : System.Web.UI.Page {

        const int MAX_CHARACTERS = 440;

        protected void Page_Load(object sender, EventArgs e) {
            if (Session["UID"] == null) {
                Response.Redirect("~/Login.aspx");
            } else {
                welcomeLabel.Text = "Welcome, " + Session["FNAME"] + " " + Session["LNAME"];
            }
        }

        protected void EditNews(object sender, CommandEventArgs e) {
            Session["AuthorTab"] = "Edit";
            Response.Redirect("~/author/EditArticle.aspx?newsId=" + e.CommandArgument);
        }

        protected void DeleteNews(object sender, CommandEventArgs e) {
            string DELETE_NEWS_STRING = "DELETE FROM [Article] WHERE id=@id";

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(DELETE_NEWS_STRING, conn)) {
                conn.Open();
                cmd.Parameters.AddWithValue("@id", e.CommandArgument);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                GridView1.DataBind();
            }
             
        }

        protected void GridView1_DataBound(object sender, EventArgs e) {
            if (GridView1.Rows.Count > 0) {
                NoArticlePanel.Visible = false;
                for (int i = 0; i < GridView1.Rows.Count; i++) {
                    Label content = (Label)GridView1.Rows[i].FindControl("lblContent");
                    Label date = (Label)GridView1.Rows[i].FindControl("lblCreatedAt");

                    date.Text = date.Text.Substring(0, date.Text.IndexOf(' '));
                    if (content.Text.Length > MAX_CHARACTERS) {
                        content.Text = content.Text.Substring(0, MAX_CHARACTERS) + "...";
                    }
                }
            } 
        }
    }
}