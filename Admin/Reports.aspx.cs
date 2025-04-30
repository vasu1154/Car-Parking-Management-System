using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }
        if (gvReport.Rows.Count == 0)
        {
            LoadReport();  // Ensure GridView has data before exporting
        }
    }

    private void LoadReport()
    {
        string connStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=Z:\Projects\Asp.Net\CarParkingManagementSysterm\App_Data\CPMS.mdf;Integrated Security=True;User Instance=True";

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string query = @"SELECT b.BookingID, u.FullName, p.SlotNumber, b.BookingDate, b.AmountPaid
                             FROM Bookings b
                             INNER JOIN Users u ON b.UserID = u.UserID
                             INNER JOIN ParkingSlots p ON b.SlotID = p.SlotID
                             ORDER BY b.BookingDate DESC";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvReport.DataSource = dt;
            gvReport.DataBind();
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        LoadReport();  // Ensure data is loaded

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;filename=ParkingReport.xls");
        Response.Charset = "";

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        gvReport.RenderControl(hw);

        // Wrap in table formatting for better Excel display
        string excelContent = "<html><head><style>td { border: 1px solid black; }</style></head><body>";
        excelContent += sw.ToString();
        excelContent += "</body></html>";

        Response.Output.Write(excelContent);
        Response.Flush();
        Response.End();
    }

    protected void btnExportPDF_Click(object sender, EventArgs e)
    {
        LoadReport();  // Ensure data is loaded

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=ParkingReport.pdf");
        Response.Charset = "";

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        gvReport.RenderControl(hw);

        string reportContent = "<html><body><h2>Parking Report</h2>" + sw.ToString() + "</body></html>";

        Response.Output.Write(reportContent.Replace("\n", "<br/>")); // Fix newline issue
        Response.Flush();
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for rendering GridView in export methods
    }
}
