using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public partial class User_CheckAvailability : System.Web.UI.Page
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
            LoadParkingSlots();
        }
    }

    private void LoadParkingSlots(string filter = "", string statusFilter = "")
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT SlotID, SlotNumber, Status, PricePerHour FROM ParkingSlots WHERE 1=1"; // Ensure valid SQL syntax

            if (!string.IsNullOrEmpty(filter))
            {
                query += " AND SlotNumber LIKE @Filter";
            }
            if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "All")
            {
                query += " AND Status = @StatusFilter";
            }

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    cmd.Parameters.AddWithValue("@Filter", "%" + filter + "%");
                }
                if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "All")
                {
                    cmd.Parameters.AddWithValue("@StatusFilter", statusFilter);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GenerateParkingSlotCards(dt);
            }
        }
    }

    private void GenerateParkingSlotCards(DataTable dt)
    {
        slotContainer.Controls.Clear();

        foreach (DataRow row in dt.Rows)
        {
            string slotID = row["SlotID"].ToString();
            string slotNumber = row["SlotNumber"].ToString();
            string status = row["Status"].ToString();
            string price = row["PricePerHour"].ToString();
            string statusClass = status == "Available" ? "available" : "booked";

            HtmlGenericControl divCol = new HtmlGenericControl("div");
            divCol.Attributes["class"] = "col-md-4 mb-4";

            HtmlGenericControl divCard = new HtmlGenericControl("div");
            divCard.Attributes["class"] = "card slot-card";

            // Redirect on click
            if (status == "Available")
            {
                divCard.Attributes["onclick"] = "window.location='Booking.aspx?slotID=" + slotID + "';";
                divCard.Style["cursor"] = "pointer";
            }

            HtmlGenericControl divBody = new HtmlGenericControl("div");
            divBody.Attributes["class"] = "card-body";

            HtmlGenericControl h5Title = new HtmlGenericControl("h5");
            h5Title.Attributes["class"] = "card-title";
            h5Title.InnerText = "Slot " + slotNumber;

            HtmlGenericControl pPrice = new HtmlGenericControl("p");
            pPrice.InnerHtml = "<b>Price:</b> ₹" + price + "/hour";

            HtmlGenericControl pStatus = new HtmlGenericControl("p");
            pStatus.Attributes["class"] = statusClass;
            pStatus.InnerText = status;

            divBody.Controls.Add(h5Title);
            divBody.Controls.Add(pPrice);
            divBody.Controls.Add(pStatus);

            divCard.Controls.Add(divBody);
            divCol.Controls.Add(divCard);
            slotContainer.Controls.Add(divCol);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string filter = txtSearch.Text.Trim();
        string statusFilter = ddlStatus.SelectedValue;

        LoadParkingSlots(filter, statusFilter);
    }
}
