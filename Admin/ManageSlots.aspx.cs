using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_ManageSlots : System.Web.UI.Page
{
    string connStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=Z:\Projects\Asp.Net\CarParkingManagementSysterm\App_Data\CPMS.mdf;Integrated Security=True;User Instance=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadParkingSlots();
        }
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }
    }
    private void LoadParkingSlots()
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM ParkingSlots", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvSlots.DataSource = dt;
            gvSlots.DataBind();
        }
    }
    protected void btnAddSlot_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "INSERT INTO ParkingSlots (SlotNumber, Status, PricePerHour) VALUES (@SlotNumber, 'Available', @Price)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@SlotNumber", txtSlotNumber.Text);
            cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            LoadParkingSlots();
            txtSlotNumber.Text = "";
            txtPrice.Text = "";
        }
    }
    protected void gvSlots_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int slotID = Convert.ToInt32(gvSlots.DataKeys[e.RowIndex].Value);

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "DELETE FROM ParkingSlots WHERE SlotID=@SlotID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@SlotID", slotID);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            LoadParkingSlots();
        }
    }
    protected void gvSlots_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSlots.EditIndex = -1;
        LoadParkingSlots();
    }
    protected void gvSlots_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvSlots.EditIndex = e.NewEditIndex;
        LoadParkingSlots();
    }
    protected void gvSlots_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int slotID = Convert.ToInt32(gvSlots.DataKeys[e.RowIndex].Value);
        string slotNumber = (gvSlots.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox).Text;
        string status = (gvSlots.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox).Text;
        decimal price = Convert.ToDecimal((gvSlots.Rows[e.RowIndex].Cells[3].Controls[0] as TextBox).Text);

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "UPDATE ParkingSlots SET SlotNumber=@SlotNumber, Status=@Status, PricePerHour=@Price WHERE SlotID=@SlotID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@SlotID", slotID);
            cmd.Parameters.AddWithValue("@SlotNumber", slotNumber);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Price", price);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            gvSlots.EditIndex = -1;
            LoadParkingSlots();
        }
    }
}