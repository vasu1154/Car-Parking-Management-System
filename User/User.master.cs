using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_User : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserID"] != null && Session["FullName"] != null)
            {
                // User is logged in
                lblUsername.Text =  Session["FullName"].ToString();
                lblUsername.Visible = true;
                btnLogout.Visible = true;
                btnLogin.Visible = false;
            }
            else
            {
                // User is not logged in
                lblUsername.Visible = false;
                btnLogout.Visible = false;
                btnLogin.Visible = true;
            }
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("~/Login.aspx");
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");
    }
}
