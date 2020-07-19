using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsListing.author {
    public partial class addArticle : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (Session["UID"] == null) {
                Response.Redirect("~/Login.aspx");
            } else {
                authorIdLbl.Text = Session["UID"].ToString();
                authorNameLbl.Text = String.Format("{0} {1}", Session["FNAME"].ToString(), Session["LNAME"].ToString());
                dateLbl.Text = DateTime.Now.ToShortDateString();
            }
        }

        protected void SubmitBtn_Click(object sender, EventArgs e) {
            string INSERT_NEWS_ARTICLE_STRING = "INSERT INTO [Article](headline, content, createdAt, author, image) VALUES(@headline, @content, @createdAt, @author, @image) SELECT SCOPE_IDENTITY()";

            try {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                using (SqlCommand cmd = new SqlCommand(INSERT_NEWS_ARTICLE_STRING, conn)) {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@headline", HeadlineTb.Text);
                    cmd.Parameters.AddWithValue("@content", ContentTb.Text);
                    cmd.Parameters.AddWithValue("@createdAt", DateTime.Now.ToShortDateString());
                    cmd.Parameters.AddWithValue("@author", Convert.ToInt32(Session["UID"]));

                    if (newsImage.HasFile && newsImage.PostedFile != null) {
                        cmd.Parameters.AddWithValue("@image", newsImage.FileBytes);
                    } else {
                        SqlParameter imageParam = new SqlParameter("@image", SqlDbType.Image);
                        imageParam.Value = DBNull.Value;
                        cmd.Parameters.Add(imageParam);
                    }

                    int publishId = Convert.ToInt32(cmd.ExecuteScalar());
                    publishMessageLabel.Text = "Published successfuly! News Article #" + publishId + " Redirecting...";
                    publishMessagePanel.CssClass = "alert alert-success alert-dismissible";
                    publishMessagePanel.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() {window.location.replace('AuthorDashboard.aspx')}, 1000)", true);
                    Session["AuthorTab"] = "Manage article";
                }
            } catch (SqlException exc) {
                publishMessageLabel.Text = "An error has occurred while publishing your news article. Please try again later. " + exc.Message;
                publishMessagePanel.CssClass = "alert alert-danger alert-dismissible";
                publishMessagePanel.Visible = true;
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e) {
            Session["AuthorTab"] = "Manage article";
            Response.Redirect("~/author/AuthorDashboard.aspx");
        }
    }
}