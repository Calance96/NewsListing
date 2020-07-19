using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace NewsListing {

    [System.Web.Services.WebService]
    [System.Web.Script.Services.ScriptService]
    public class CommentService : System.Web.Services.WebService {

        public class Comment {
            public string user { get; set; }
            public string comment { get; set; }
            public string date { get; set; }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetComments(string postId) {
            string divContent = "";

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT Comment.*, CONCAT([User].[Fname], ' ', [User].[Lname]) as commenterName, [User].[Type] FROM [Comment] JOIN [User] ON [Comment].[UserId]=[User].[Id] WHERE ArticleId=@AID", conn)) {
                conn.Open();
                cmd.Parameters.AddWithValue("@AID", postId);

                bool first = true;
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    if (reader.HasRows) {
                        while (reader.Read()) {
                            if (first) {
                                first = false;
                            } else {
                                divContent += "<hr/>";
                            }
                            string user = reader["commenterName"].ToString();
                            string content = reader["Content"].ToString();
                            string date = reader["PostDate"].ToString();
                            bool isAdmin = reader["Type"].ToString() == "1";
                            bool isAuthor = reader["Type"].ToString() == "2";
                            string userBadge = isAdmin ? "<span class=\"badge badge-danger\">Admin</span>" : isAuthor ? "<span class=\"badge badge-success\">Author</span>" : "";
                            date = date.Substring(0, date.IndexOf(' '));
                            divContent += "<div>";
                            divContent += String.Format("<span class=\"badge badge-info\">{0}</span>\t{1}<br/><span class=\"text-secondary text-sm-center font-italic\">{2}</span><br/>", user, userBadge, date);
                            divContent += "<p>" + content + "</p>";
                            divContent += "</div>";
                        }
                    } else {
                        divContent = "<div class=\"text-sm-center text-secondary\">No comments</div>";
                    }
                }
            }
            return divContent;
        }

        [WebMethod]
        public string SubmitComment(string postId, string userId, string comment) {
            String ADD_COMMENT = "INSERT INTO Comment(UserId, Content, ArticleId, PostDate) VALUES(@UID, @Content, @AID, GETDATE())";
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(ADD_COMMENT, conn)) {
                conn.Open();
                cmd.Parameters.AddWithValue("@UID", userId);
                cmd.Parameters.AddWithValue("@Content", comment);
                cmd.Parameters.AddWithValue("@AID", postId);
                int success = cmd.ExecuteNonQuery();

                if (Convert.ToBoolean(success)) {
                    return "Success";
                } else {
                    return "Failure";
                }
            }
        }
    }
}
