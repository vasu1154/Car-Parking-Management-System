using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Payment : System.Web.UI.Page
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
            if (!string.IsNullOrEmpty(Request.QueryString["slotID"]) && !string.IsNullOrEmpty(Request.QueryString["amountPaid"]))
            {
                lblSlotID.Text = Request.QueryString["slotID"];
                lblAmount.Text = Request.QueryString["amountPaid"];
            }
            else
            {
                Response.Redirect("CheckAvailability.aspx");
            }
        }
    }

    protected void btnPayNow_Click(object sender, EventArgs e)
    {
        try
        {
            // ✅ Check for required values
            string slotID = lblSlotID.Text;
            string paymentMethod = ddlPaymentMethod.SelectedValue;
            decimal amountPaid = Convert.ToDecimal(lblAmount.Text);
            string userID = Session["UserID"].ToString();

            if (string.IsNullOrEmpty(userID))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                int bookingID = 0;

                // 🔹 Step 1: Find the existing booking
                string bookingQuery = "SELECT BookingID FROM Bookings WHERE SlotID = @SlotID AND UserID = @UserID AND PaymentStatus = 'Pending'";
                using (SqlCommand cmd = new SqlCommand(bookingQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@SlotID", slotID);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        bookingID = Convert.ToInt32(result);
                    }
                }

                // 🔹 Step 2: If no existing pending booking, return error
                if (bookingID == 0)
                {
                    lblMessage.Text = "No pending booking found for this slot.";
                    return;
                }

                // 🔹 Step 3: Insert Payment Record with Default PaymentDate
                int paymentID = 0;
                string paymentQuery = @"
                INSERT INTO Payments (BookingID, PaymentMethod, Amount, PaymentStatus, PaymentDate) 
                OUTPUT INSERTED.PaymentID
                VALUES (@BookingID, @PaymentMethod, @Amount, 'Completed', GETDATE())";  // 👈 Added PaymentDate = GETDATE()

                using (SqlCommand cmd = new SqlCommand(paymentQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@BookingID", bookingID);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@Amount", amountPaid);

                    object paymentResult = cmd.ExecuteScalar();
                    if (paymentResult != null)
                    {
                        paymentID = Convert.ToInt32(paymentResult);
                    }
                }

                // 🔹 Step 4: Update Booking PaymentStatus
                if (paymentID > 0)
                {
                    // ✅ Update Booking Status to Completed
                    string updateBookingQuery = "UPDATE Bookings SET PaymentStatus = 'Completed' WHERE BookingID = @BookingID";
                    using (SqlCommand cmd = new SqlCommand(updateBookingQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookingID", bookingID);
                        cmd.ExecuteNonQuery();
                    }

                    // ✅ Update Parking Slot Status to 'Booked'
                    string updateSlotQuery = "UPDATE ParkingSlots SET Status = 'Booked' WHERE SlotID = @SlotID";
                    using (SqlCommand cmd = new SqlCommand(updateSlotQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@SlotID", slotID);
                        cmd.ExecuteNonQuery();
                    }

                    // ✅ Redirect to Bill Page
                    Response.Redirect("Bill.aspx?paymentID=" + paymentID, false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    lblMessage.Text = "Payment failed. Please try again.";
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
        }
    }
}
