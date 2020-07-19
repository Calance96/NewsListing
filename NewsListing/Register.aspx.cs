using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsListing {
    public partial class Register : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void RegisterBtn_Click(object sender, EventArgs e) {
            string CHECK_USER_STRING = "SELECT * FROM [User] WHERE username=@username";
            string INSERT_USER_STRING = "INSERT INTO [User](username, password, type, fname, lname) VALUES(@username, @password, 3, @fname, @lname)";

            try {
                using (SqlConnection conn = DatabaseConnection.GetConnection()) {
                    conn.Open();
                    using (SqlCommand checkCmd = new SqlCommand(CHECK_USER_STRING, conn)) {
                        checkCmd.Parameters.AddWithValue("@username", usernameTb.Text);
                        SqlDataAdapter adapter = new SqlDataAdapter(checkCmd);
                        DataSet results = new DataSet();
                        adapter.Fill(results);
                        if (results.Tables[0].Rows.Count > 0) {
                            registerMessageLabel.Text = "Username has been taken. Please try another username.";
                            registerMessagePanel.CssClass = "alert alert-danger alert-dismissible";
                            registerMessagePanel.Visible = true;

                        } else {
                            using (SqlCommand insertCmd = new SqlCommand(INSERT_USER_STRING, conn)) {
                                
                                insertCmd.Parameters.AddWithValue("@username", usernameTb.Text);
                                insertCmd.Parameters.AddWithValue("@password", passwordTb.Text);
                                insertCmd.Parameters.AddWithValue("@fname", firstNameTb.Text);
                                insertCmd.Parameters.AddWithValue("@lname", lastNameTb.Text);
                                int inserted = insertCmd.ExecuteNonQuery();
                                insertCmd.Dispose();

                                if (Convert.ToBoolean(inserted)) {
                                    registerMessageLabel.Text = "Registered successfully";
                                    registerMessagePanel.CssClass = "alert alert-success alert-dismissible";
                                    registerMessagePanel.Visible = true;
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() {window.location.replace('Login.aspx')}, 1000)", true);
                                }
                            }
                        }
                    }
                }
            } catch (SqlException) {
                registerMessageLabel.Text = "An error has occurred. Please try again later.";
                registerMessagePanel.CssClass = "alert alert-danger alert-dismissible";
                registerMessagePanel.Visible = true;
            }
        }
    }
}