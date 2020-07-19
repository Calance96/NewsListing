using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsListing.author {
    public partial class WebForm1 : System.Web.UI.Page {

        private string newsId;

        protected void Page_Load(object sender, EventArgs e) {
            if (Session["UID"] == null) {
                Response.Redirect("~/Login.aspx");
            } else {
                this.newsId = Request["newsId"];
            }

            if (!IsPostBack) {
                PrefillForm();
            }
        }

        private void PrefillForm() {
            string GET_NEWS_ARTICLE_STRING = "SELECT [Article].id, [Article].author, [Article].createdAt, [Article].Headline, [Article].Content, [Article].Image, CONCAT([User].fname, ' ', [User].lname) FROM [Article] JOIN [User] ON [Article].author=[User].id WHERE [Article].id=@newsId";
            try {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                using (SqlCommand cmd = new SqlCommand(GET_NEWS_ARTICLE_STRING, conn)) {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@newsId", newsId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet results = new DataSet();
                    adapter.Fill(results);
                    if (results.Tables[0].Rows.Count > 0) {
                        articleId.Text = results.Tables[0].Rows[0][0].ToString();
                        authorIdLbl.Text = results.Tables[0].Rows[0][1].ToString();
                        authorNameLbl.Text = results.Tables[0].Rows[0][6].ToString();
                        dateLbl.Text = results.Tables[0].Rows[0][2].ToString();
                        dateLbl.Text = dateLbl.Text.Substring(0, dateLbl.Text.IndexOf(' ')); // Format the date string
                        HeadlineTb.Text = results.Tables[0].Rows[0][3].ToString();
                        ContentTb.Text = results.Tables[0].Rows[0][4].ToString();
                        byte[] imageBytes = results.Tables[0].Rows[0][5] == DBNull.Value ? null : (byte[])results.Tables[0].Rows[0][5];

                        if (imageBytes != null) { 
                            string imageStrBase64 = Convert.ToBase64String(imageBytes);
                            dbNewsImage.ImageUrl = "data:Image/jpg;base64," + imageStrBase64;
                        }

                        // Author is trying to edit another author's news article
                        if (authorIdLbl.Text != Session["UID"].ToString()) {
                            HeadlineTb.Enabled = false;
                            ContentTb.Enabled = false;
                            SaveBtn.Visible = false;
                            publishMessageLabel.Text = "You are not authorized to edit this news article!";
                            publishMessagePanel.CssClass = "alert alert-danger alert-dismissible";
                            publishMessagePanel.Visible = true;
                        }
                    }
                }
            } catch (SqlException) {
                publishMessageLabel.Text = "Error retrieving news article information! ";
                publishMessagePanel.CssClass = "alert alert-danger alert-dismissible";
                publishMessagePanel.Visible = true;
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e) {
            string UPDATE_NEWS_STRING = "UPDATE [Article] SET headline=@headline, content=@content, image=@image WHERE id=@id";
            string UPDATE_NEWS_WITHOUT_IMAGE_STRING = "UPDATE [Article] SET headline=@headline, content=@content WHERE id=@id";
            try {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                using (SqlCommand cmd = new SqlCommand()) {
                    conn.Open();
                    cmd.Connection = conn;
                    if (newsImage.HasFile && newsImage.PostedFile != null) {
                        cmd.CommandText = UPDATE_NEWS_STRING;
                        cmd.Parameters.AddWithValue("@image", newsImage.FileBytes);
                    } else {
                        cmd.CommandText = UPDATE_NEWS_WITHOUT_IMAGE_STRING;
                    }

                    cmd.Parameters.AddWithValue("@id", newsId);
                    cmd.Parameters.AddWithValue("@headline", HeadlineTb.Text);
                    cmd.Parameters.AddWithValue("@content", ContentTb.Text);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    publishMessageLabel.Text = "News article information updated successfully. Redirecting...";
                    publishMessagePanel.CssClass = "alert alert-success alert-dismissible";
                    publishMessagePanel.Visible = true;
                    Session["AuthorTab"] = "Home";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() {window.location.replace('AuthorDashboard.aspx')}, 1000)", true);
                }
            } catch (SqlException) {
                publishMessageLabel.Text = "Error update news article information! ";
                publishMessagePanel.CssClass = "alert alert-danger alert-dismissible";
                publishMessagePanel.Visible = true;
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e) {
            Session["AuthorTab"] = "Home";
            Response.Redirect("~/author/AuthorDashboard.aspx");
        }
    }
}