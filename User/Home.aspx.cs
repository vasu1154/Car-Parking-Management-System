using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class User_Home : System.Web.UI.Page
{
    string connStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=Z:\Projects\Asp.Net\CarParkingManagementSysterm\App_Data\CPMS.mdf;Integrated Security=True;User Instance=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadParkingStats();
        }
    }
    private void LoadParkingStats()
    {
        string connStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=Z:\Projects\Asp.Net\CarParkingManagementSysterm\App_Data\CPMS.mdf;Integrated Security=True;User Instance=True";
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            // Get total slots
            SqlCommand cmdTotal = new SqlCommand("SELECT COUNT(*) FROM ParkingSlots", conn);
            int totalSlots = (int)cmdTotal.ExecuteScalar();
            lblTotalSlots.Text = totalSlots.ToString();

            // Get booked slots
            SqlCommand cmdBooked = new SqlCommand("SELECT COUNT(*) FROM ParkingSlots WHERE Status = 'Booked'", conn);
            int bookedSlots = (int)cmdBooked.ExecuteScalar();
            lblBookedSlots.Text = bookedSlots.ToString();

            // Calculate available slots
            int availableSlots = totalSlots - bookedSlots;
            lblAvailableSlots.Text = availableSlots.ToString();
        }
    }
    private void LoadRecentBookings()
    {
        string userID = Session["UserID"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string query = @"SELECT S.SlotNumber, B.BookingDate, B.StartTime, B.EndTime, B.AmountPaid
                             FROM Bookings B
                             JOIN ParkingSlots S ON B.SlotID = S.SlotID
                             WHERE B.UserID = @UserID
                             ORDER BY B.BookingDate DESC
                             OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", userID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvRecentBookings.DataSource = dt;
                    gvRecentBookings.DataBind();
                }
            }
        }
    }
}