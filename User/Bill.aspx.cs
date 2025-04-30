using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class User_Bill : System.Web.UI.Page
{
    string connStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=Z:\Projects\Asp.Net\CarParkingManagementSysterm\App_Data\CPMS.mdf;Integrated Security=True;User Instance=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["paymentID"] != null)
            {
                string paymentID = Request.QueryString["paymentID"];
                LoadBillDetails(paymentID);
            }
            else
            {
                Response.Redirect("CheckAvailability.aspx");
            }
        }
    }
    private void LoadBillDetails(string paymentID)
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = @"
                SELECT P.PaymentID, B.SlotID,B.CarNo, S.SlotNumber, P.Amount, P.PaymentMethod, 
                       P.PaymentDate, P.PaymentStatus 
                FROM Payments P
                INNER JOIN Bookings B ON P.BookingID = B.BookingID
                INNER JOIN ParkingSlots S ON B.SlotID = S.SlotID
                WHERE P.PaymentID = @PaymentID";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PaymentID", paymentID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Label1.Text = !reader.IsDBNull(reader.GetOrdinal("CarNo")) ? reader["CarNo"].ToString() : "N/A";
                    lblPaymentID.Text = reader["PaymentID"].ToString();
                    lblSlotNumber.Text = !reader.IsDBNull(reader.GetOrdinal("SlotNumber")) ? reader["SlotNumber"].ToString() : "N/A";
                    lblAmount.Text = !reader.IsDBNull(reader.GetOrdinal("Amount")) ? Convert.ToDecimal(reader["Amount"]).ToString("F2") : "0.00";
                    lblPaymentMethod.Text = !reader.IsDBNull(reader.GetOrdinal("PaymentMethod")) ? reader["PaymentMethod"].ToString() : "Unknown";
                    lblPaymentDate.Text = !reader.IsDBNull(reader.GetOrdinal("PaymentDate")) ? Convert.ToDateTime(reader["PaymentDate"]).ToString("dd-MMM-yyyy HH:mm") : "N/A";
                    lblPaymentStatus.Text = !reader.IsDBNull(reader.GetOrdinal("PaymentStatus")) ? reader["PaymentStatus"].ToString() : "Pending";
                }
                else
                {
                    Response.Redirect("CheckAvailability.aspx");
                }
                conn.Close();
            }
        }
    }
}