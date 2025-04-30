using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            Response.Redirect("dashboard.aspx"); // Redirect to the dashboard if already logged in
        }
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string fullName = txtFullName.Text.Trim();
        string email = txtEmail.Text.Trim();
        string password = txtPassword.Text.Trim();
        string phone = txtPhone.Text.Trim();

        if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phone))
        {
            Response.Write("<script>alert('All fields are required!');</script>");
            return;
        }

        string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=Z:\Projects\Asp.Net\CarParkingManagementSysterm\App_Data\CPMS.mdf;Integrated Security=True;User Instance=True";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            // Check if the email is already registered
            string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
            using (SqlCommand checkCmd = new SqlCommand(checkUserQuery, con))
            {
                checkCmd.Parameters.AddWithValue("@Email", email);
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    Response.Write("<script>alert('Email is already registered!');</script>");
                    return;
                }
            }

            // Insert the new user into the database
            string insertQuery = "INSERT INTO Users (FullName, Email, Password, Phone, Role) VALUES (@FullName, @Email, @Password, @Phone, 'User')";
            using (SqlCommand cmd = new SqlCommand(insertQuery, con))
            {
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password); // Ideally, hash the password before storing
                cmd.Parameters.AddWithValue("@Phone", phone);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Response.Write("<script>alert('Registration Successfully... Please try again!');</script>");
                            Response.Redirect("Login.aspx"); // Redirect after successful registration
                }
                else
                {
                    Response.Write("<script>alert('Registration failed. Please try again!');</script>");
                }
            }
        }
    }
}