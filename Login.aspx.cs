using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            Response.Redirect("~/User/Home.aspx");
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=Z:\Projects\Asp.Net\CarParkingManagementSysterm\App_Data\CPMS.mdf;Integrated Security=True;User Instance=True";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT UserID, FullName, Role FROM Users WHERE Email=@Email AND Password=@Password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                // Store session data
                Session["UserID"] = reader["UserID"].ToString();
                Session["FullName"] = reader["FullName"].ToString();
                Session["Role"] = reader["Role"].ToString();

                // Redirect based on role
                string role = reader["Role"].ToString();
                if (role == "Admin")
                {
                    Response.Redirect("~/Admin/Dashboard.aspx");
                }
                else
                {
                    Response.Redirect("~/User/Home.aspx");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid email or password!');</script>");
            }
        }
    }
}