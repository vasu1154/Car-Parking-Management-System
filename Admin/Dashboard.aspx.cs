using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_AdminDashboard : System.Web.UI.Page
{
    string connStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=Z:\Projects\Asp.Net\CarParkingManagementSysterm\App_Data\CPMS.mdf;Integrated Security=True;User Instance=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadParkingSlotData();
        }
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }
    }
    private void LoadParkingSlotData()
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            try
            {
                conn.Open();

                // SQL Query to Get Total Slots, Booked Slots, and Available Slots
                string query = @"
                    SELECT 
                        (SELECT COUNT(*) FROM ParkingSlots) AS TotalSlots, 
                        (SELECT COUNT(*) FROM ParkingSlots WHERE Status = 'Booked') AS BookedSlots,
                        (SELECT COUNT(*) FROM ParkingSlots WHERE Status = 'Available') AS AvailableSlots";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Assign database values to Labels
                    lblTotalSlots.Text = reader["TotalSlots"].ToString();
                    lblBookedSlots.Text = reader["BookedSlots"].ToString();
                    lblAvailableSlots.Text = reader["AvailableSlots"].ToString();
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle exceptions (Log or display error)
                lblTotalSlots.Text = "Error!";
                lblBookedSlots.Text = "Error!";
                lblAvailableSlots.Text = "Error!";
                Console.WriteLine("Database Error: " + ex.Message);
            }
        }
    }
}