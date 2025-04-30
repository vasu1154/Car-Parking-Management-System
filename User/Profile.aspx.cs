using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class User_Profile : System.Web.UI.Page
{
    string connStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=Z:\Projects\Asp.Net\CarParkingManagementSysterm\App_Data\CPMS.mdf;Integrated Security=True;User Instance=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadUserProfile();
        }
    }
    private void LoadUserProfile()
    {
        string userID = Session["UserID"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT FullName, Email, Phone FROM Users WHERE UserID = @UserID";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", userID);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtFullName.Text = reader["FullName"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtContact.Text = reader["Phone"].ToString();
                    }
                }
            }
        }
    }
    protected void btnUpdateProfile_Click(object sender, EventArgs e)
    {
        string userID = Session["UserID"].ToString();
        string fullName = txtFullName.Text.Trim();
        string contact = txtContact.Text.Trim();
        string newPassword = txtPassword.Text.Trim();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string updateQuery = "UPDATE Users SET FullName = @FullName, Phone = @ContactNumber";

            if (!string.IsNullOrEmpty(newPassword))
            {
                updateQuery += ", Password = @Password";
            }
            updateQuery += " WHERE UserID = @UserID";

            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
            {
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@ContactNumber", contact);
                cmd.Parameters.AddWithValue("@UserID", userID);

                if (!string.IsNullOrEmpty(newPassword))
                {
                    cmd.Parameters.AddWithValue("@Password", newPassword);
                }

                cmd.ExecuteNonQuery();
            }
        }

        // Show success message
        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Profile updated successfully!');", true);
    }
    
}