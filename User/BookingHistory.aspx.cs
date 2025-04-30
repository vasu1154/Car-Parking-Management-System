using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_BookingHistory : System.Web.UI.Page
{
    string connStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=Z:\Projects\Asp.Net\CarParkingManagementSysterm\App_Data\CPMS.mdf;Integrated Security=True;User Instance=True";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null || string.IsNullOrEmpty(Session["UserID"].ToString()))
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadBookingHistory();
        }
    }

    private void LoadBookingHistory()
    {
        string userID = Session["UserID"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string query = @"SELECT B.BookingID, S.SlotNumber, B.BookingDate,B.CarNo,
                                    COALESCE(B.StartTime, GETDATE()) AS StartTime, 
                                    COALESCE(B.EndTime, GETDATE()) AS EndTime, 
                                    COALESCE(B.TotalHours, 0) AS TotalHours, 
                                    COALESCE(B.AmountPaid, 0.0) AS AmountPaid, 
                                    COALESCE(P.PaymentStatus, 'Pending') AS PaymentStatus
                             FROM Bookings B
                             JOIN ParkingSlots S ON B.SlotID = S.SlotID
                             LEFT JOIN Payments P ON B.BookingID = P.BookingID
                             WHERE B.UserID = @UserID
                             ORDER BY B.BookingDate DESC";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(userID)); // Ensure correct format
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvBookingHistory.DataSource = dt;
                    gvBookingHistory.DataBind();
                }
            }
        }
    }

    protected void gvBookingHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBookingHistory.PageIndex = e.NewPageIndex;
        LoadBookingHistory();
    }
    protected void gvBookingHistory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "CancelBooking")
        {
            int bookingID = Convert.ToInt32(e.CommandArgument);
            CancelBooking(bookingID);
        }
    }
    private void CancelBooking(int bookingID)
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            int slotID = 0;

            // Step 1: Get SlotID from the canceled booking
            string getSlotQuery = "SELECT SlotID FROM Bookings WHERE BookingID = @BookingID";
            using (SqlCommand cmdSlot = new SqlCommand(getSlotQuery, conn))
            {
                cmdSlot.Parameters.AddWithValue("@BookingID", bookingID);
                object result = cmdSlot.ExecuteScalar();
                if (result != null)
                {
                    slotID = Convert.ToInt32(result);
                }
            }

            if (slotID > 0)
            {
                // Step 2: Delete related payments
                string deletePaymentsQuery = "DELETE FROM Payments WHERE BookingID = @BookingID";
                using (SqlCommand cmdPayments = new SqlCommand(deletePaymentsQuery, conn))
                {
                    cmdPayments.Parameters.AddWithValue("@BookingID", bookingID);
                    cmdPayments.ExecuteNonQuery();
                }

                // Step 3: Delete the booking
                string deleteBookingQuery = "DELETE FROM Bookings WHERE BookingID = @BookingID";
                using (SqlCommand cmdBooking = new SqlCommand(deleteBookingQuery, conn))
                {
                    cmdBooking.Parameters.AddWithValue("@BookingID", bookingID);
                    cmdBooking.ExecuteNonQuery();
                }

                // Step 4: Update slot status to 'Available'
                string updateSlotQuery = "UPDATE ParkingSlots SET Status = 'Available' WHERE SlotID = @SlotID";
                using (SqlCommand cmdSlotUpdate = new SqlCommand(updateSlotQuery, conn))
                {
                    cmdSlotUpdate.Parameters.AddWithValue("@SlotID", slotID);
                    cmdSlotUpdate.ExecuteNonQuery();
                }
            }
        }

        // Refresh Booking History
        LoadBookingHistory();

        // Show confirmation message
        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
            "alert('Booking canceled successfully!');", true);
    }
}
