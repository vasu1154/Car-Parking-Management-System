using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Booking : System.Web.UI.Page
{
    string connStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=Z:\Projects\Asp.Net\CarParkingManagementSysterm\App_Data\CPMS.mdf;Integrated Security=True;User Instance=True";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["SlotID"] != null)
            {
                hfSlotID.Value = Request.QueryString["SlotID"];
                LoadSlotDetails();
            }
            else
            {
                Response.Redirect("~/User/CheckAvailability.aspx"); // Redirect if no slot is selected
            }
        }
    }

    private void LoadSlotDetails()
    {
       
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT SlotNumber, PricePerHour FROM ParkingSlots WHERE SlotID = @SlotID";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@SlotID", hfSlotID.Value);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblSlotNumber.Text = reader["SlotNumber"].ToString();
                    lblPricePerHour.Text = reader["PricePerHour"].ToString();
                }
                conn.Close();
            }
        }
    }

    protected void btnConfirmBooking_Click(object sender, EventArgs e)
    {
        try
        {
            string slotID = hfSlotID.Value;
            string userID = Session["UserID"] != null ? Session["UserID"].ToString() : "";
            string startTime = txtStartTime.Text;
            string endTime = txtEndTime.Text;

            if (string.IsNullOrEmpty(slotID) || string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(startTime) || string.IsNullOrEmpty(endTime))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please fill all fields.');", true);
                return;
            }

            DateTime start = DateTime.Parse(startTime);
            DateTime end = DateTime.Parse(endTime);

            if (end <= start)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('End time must be after start time.');", true);
                return;
            }

            int totalHours = (int)(end - start).TotalHours;
            if (totalHours <= 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Minimum booking time should be 1 hour.');", true);
                return;
            }

            decimal pricePerHour = Convert.ToDecimal(lblPricePerHour.Text);
            decimal amountPaid = totalHours * pricePerHour;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO Bookings (UserID, SlotID, BookingDate, StartTime, EndTime, TotalHours, AmountPaid, PaymentStatus,CarNo) " +
                               "VALUES (@UserID, @SlotID, @BookingDate, @StartTime, @EndTime, @TotalHours, @AmountPaid, 'Pending',@carno)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@SlotID", slotID);
                    cmd.Parameters.AddWithValue("@BookingDate", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@StartTime", start);
                    cmd.Parameters.AddWithValue("@EndTime", end);
                    cmd.Parameters.AddWithValue("@TotalHours", totalHours);
                    cmd.Parameters.AddWithValue("@AmountPaid", amountPaid);
                    cmd.Parameters.AddWithValue("@carno", txtcarno.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Response.Redirect("Payment.aspx?slotID=" + slotID + "&amountPaid=" + amountPaid.ToString("F2"), true);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Booking failed. Please try again.');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error: " + ex.Message.Replace("'", "\\'") + "');", true);
        }
    }
}
