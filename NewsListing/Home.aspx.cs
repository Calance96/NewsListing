using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsListing {
    public partial class Home : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void NewsRepeater_ItemCreated(object sender, RepeaterItemEventArgs e) {
            if (Session["UID"] == null) {
                e.Item.FindControl("openCommentModalBtn").Visible = false;
            }
        }
    }
}