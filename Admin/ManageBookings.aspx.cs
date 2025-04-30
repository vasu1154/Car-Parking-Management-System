using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_ManageBookings : System.Web.UI.Page
{
    private string connStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=Z:\Projects\Asp.Net\CarParkingManagementSysterm\App_Data\CPMS.mdf;Integrated Security=True;User Instance=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBookings();
        }
    }
    private void LoadBookings()
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = @"SELECT b.BookingID, u.FullName, p.SlotNumber, b.BookingDate, b.AmountPaid, b.PaymentStatus,b.CarNo 
                             FROM Bookings b
                             INNER JOIN Users u ON b.UserID = u.UserID
                             INNER JOIN ParkingSlots p ON b.SlotID = p.SlotID
                             ORDER BY b.BookingDate DESC";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvBookings.DataSource = dt;
            gvBookings.DataBind();
        }
    }
    protected void gvBookings_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int bookingID = Convert.ToInt32(e.CommandArgument);
        string action = e.CommandName;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            SqlCommand cmd;

            if (action == "Approve")
            {
                cmd = new SqlCommand("UPDATE Bookings SET PaymentStatus='Paid' WHERE BookingID=@BookingID", conn);
            }
            else if (action == "Reject")
            {
                cmd = new SqlCommand("UPDATE Bookings SET PaymentStatus='Failed' WHERE BookingID=@BookingID", conn);
            }
            else if (action == "Delete")
            {
                cmd = new SqlCommand("DELETE FROM Payments WHERE BookingID=@BookingID", conn);
                cmd.Parameters.AddWithValue("@BookingID", bookingID);
                cmd.ExecuteNonQuery();

                // Then delete the booking
                cmd = new SqlCommand("DELETE FROM Bookings WHERE BookingID=@BookingID", conn);
            }
            else
            {
                return;
            }

            cmd.Parameters.AddWithValue("@BookingID", bookingID);
            cmd.ExecuteNonQuery();
        }

        LoadBookings(); // Refresh GridView
    }
    protected void gvBookings_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}