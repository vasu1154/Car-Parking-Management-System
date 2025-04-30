<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeFile="Bill.aspx.cs" Inherits="User_Bill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
        .invoice-container {
            max-width: 600px;
            margin: 50px auto;
            padding: 20px;
            border: 1px solid #ddd;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            background: #fff;
        }
        .invoice-header {
            text-align: center;
            margin-bottom: 20px;
        }
        .btn-print {
            display: block;
            width: 100%;
            margin-top: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="invoice-container">
        <div class="invoice-header">
            <h2>Parking Management System</h2>
            <p><strong>Payment Receipt</strong></p>
        </div>

        <table class="table table-bordered">
            <tbody>
                <tr>
                    <th>Payment ID:</th>
                    <td><asp:Label ID="lblPaymentID" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Slot Number:</th>
                    <td><asp:Label ID="lblSlotNumber" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Car Number:</th>
                    <td><asp:Label ID="Label1" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Amount Paid:</th>
                    <td>₹<asp:Label ID="lblAmount" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Payment Method:</th>
                    <td><asp:Label ID="lblPaymentMethod" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Payment Date:</th>
                    <td><asp:Label ID="lblPaymentDate" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Status:</th>
                    <td><asp:Label ID="lblPaymentStatus" runat="server" CssClass="fw-bold"></asp:Label></td>
                </tr>
            </tbody>
        </table>

        <button class="btn btn-primary btn-print" onclick="window.print()">Print Receipt</button>
        <asp:Button ID="Button1" runat="server" Text="🏠 Home" 
            CssClass="btn btn-primary btn-print" PostBackUrl="~/User/Home.aspx" />
        
    </div>
</asp:Content>

