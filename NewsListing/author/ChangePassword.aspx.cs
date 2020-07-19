using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsListing.author {
    public partial class ChangePassword : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void CancelBtn_Click(object sender, EventArgs e) {
            Session["AuthorTab"] = "Home";
            Response.Redirect("AuthorDashboard.aspx");
        }

        protected void ConfirmBtn_Click(object sender, EventArgs e) {
            string UPDATE_PASSWORD_STRING = "UPDATE [User] SET Password=@NewPassword WHERE Id=@Id AND Password=@OldPassword";

            try {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                using (SqlCommand cmd = new SqlCommand(UPDATE_PASSWORD_STRING, conn)) {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@NewPassword", newPasswordTb.Text);
                    cmd.Parameters.AddWithValue("@Id", Session["UID"]);
                    cmd.Parameters.AddWithValue("@OldPassword", currentPasswordTb.Text);

                    int updated = cmd.ExecuteNonQuery();
                    if (Convert.ToBoolean(updated)) {
                        changePasswordMessageLabel.Text = "Password changed successfully";
                        changePasswordMessagePanel.CssClass = "alert alert-success alert-dismissible";
                        changePasswordMessagePanel.Visible = true;
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() {window.location.replace('AuthorDashboard.aspx')}, 1000)", true);
                        Session["AuthorTab"] = "Manage article";

                    } else {
                        changePasswordMessageLabel.Text = "Incorrect old password";
                        changePasswordMessagePanel.CssClass = "alert alert-danger alert-dismissible";
                        changePasswordMessagePanel.Visible = true;
                    }
                }
            } catch (SqlException) {
                changePasswordMessageLabel.Text = "Database error";
                changePasswordMessagePanel.CssClass = "alert alert-danger alert-dismissible";
                changePasswordMessagePanel.Visible = true;
            }
        }
    }

}