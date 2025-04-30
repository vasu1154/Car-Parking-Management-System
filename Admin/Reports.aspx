<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Admin_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .report-container {
            padding: 20px;
            background-color: #f8f9fa;
        }
        .table {
            margin-top: 20px;
            background: white;
            border-radius: 10px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
        }
        .btn {
            padding: 8px 12px;
            font-size: 14px;
            border-radius: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2 class="mb-4">📊 Parking Usage & Revenue Reports</h2>

    <!-- Filter Section -->
    <div>
            <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="true"></asp:GridView>
            <br />
            <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel" OnClick="btnExportExcel_Click" />
            <asp:Button ID="btnExportPDF" runat="server" Text="Export to PDF" OnClick="btnExportPDF_Click" />
        </div>
</asp:Content>

