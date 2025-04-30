<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="User_Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .payment-container {
            max-width: 600px;
            margin: auto;
            background: white;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }
        .btn-pay {
            width: 100%;
            font-size: 18px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="container mt-4">
        <div class="payment-container">
            <h3 class="text-center">💳 Payment Details</h3>
            
            <div class="mb-3">
                <label class="form-label"><b>Slot Number:</b></label>
                <asp:Label ID="lblSlotID" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label"><b>Amount (₹):</b></label>
                <asp:Label ID="lblAmount" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label"><b>Select Payment Method:</b></label>
                <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                    <asp:ListItem Text="Card" Value="Card"></asp:ListItem>
                    <asp:ListItem Text="Online" Value="Online"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <asp:Button ID="btnPayNow" runat="server" CssClass="btn btn-success btn-pay" Text="Proceed to Pay" OnClick="btnPayNow_Click" />

            <div class="mt-4 text-center">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>

