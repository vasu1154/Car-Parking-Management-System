using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Admin_ManageUsers : System.Web.UI.Page
{
    string connStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=Z:\Projects\Asp.Net\CarParkingManagementSysterm\App_Data\CPMS.mdf;Integrated Security=True;User Instance=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUsers();
        }
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }
    }
    private void LoadUsers()
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT UserID, FullName, Email, Phone, Role FROM Users", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvUsers.DataSource = dt;
            gvUsers.DataBind();
        }
    }
    protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvUsers.EditIndex = -1;
        LoadUsers();
    }
    protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int userID = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "DELETE FROM Users WHERE UserID=@UserID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserID", userID);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            LoadUsers();
        }
    }
    protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvUsers.EditIndex = e.NewEditIndex;
        LoadUsers();
    }
    protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int userID = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
        string fullName = (gvUsers.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox).Text;
        string email = (gvUsers.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox).Text;
        string phone = (gvUsers.Rows[e.RowIndex].Cells[3].Controls[0] as TextBox).Text;
        string role = (gvUsers.Rows[e.RowIndex].Cells[4].Controls[0] as TextBox).Text;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "UPDATE Users SET FullName=@FullName, Email=@Email, Phone=@Phone, Role=@Role WHERE UserID=@UserID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@FullName", fullName);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@Role", role);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            gvUsers.EditIndex = -1;
            LoadUsers();
        }
    }
}