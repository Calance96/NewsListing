using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsListing.Author {
    public partial class Login : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void LoginBtn_Click(object sender, EventArgs e) {
            string GET_USER_STRING = "SELECT * FROM [User] WHERE username=@username AND password=@password";

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(GET_USER_STRING, conn)) {
                conn.Open();
                cmd.Parameters.AddWithValue("@username", usernameTb.Text);
                cmd.Parameters.AddWithValue("@password", passwordTb.Text);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet results = new DataSet();
                adapter.Fill(results);

                if (results.Tables[0].Rows.Count > 0) {
                    Session["UID"] = results.Tables[0].Rows[0][0].ToString();
                    Session["TYPE"] = results.Tables[0].Rows[0][3].ToString();
                    Session["FNAME"] = results.Tables[0].Rows[0][4].ToString();
                    Session["LNAME"] = results.Tables[0].Rows[0][5].ToString();

                    if (Session["TYPE"].ToString() == "1" || Session["TYPE"].ToString() == "2") // Admin/author
                        Response.Redirect("~/author/AuthorDashboard.aspx");
                    else { // Normal user
                        Session["UserTab"] = "Home";
                        Response.Redirect("~/Home.aspx");
                    }

                } else {
                    loginMessageLabel.Text = "Invalid credentials.";
                    loginMessagePanel.CssClass = "alert alert-danger alert-dismissible";
                    loginMessagePanel.Visible = true;
                }
            }
        }
    }
}