<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeFile="BookingHistory.aspx.cs" Inherits="User_BookingHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        table { width: 100%; border-collapse: collapse; }
        th, td { padding: 10px; text-align: left; border: 1px solid #ddd; }
        th { background-color: #f4f4f4; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Your Booking History</h2>
    <asp:GridView ID="gvBookingHistory" runat="server" AutoGenerateColumns="False"
    OnRowCommand="gvBookingHistory_RowCommand" AllowPaging="True" 
    OnPageIndexChanging="gvBookingHistory_PageIndexChanging">
    
    <Columns>
        <asp:BoundField DataField="BookingID" HeaderText="Booking ID" />
        <asp:BoundField DataField="SlotNumber" HeaderText="Slot Number" />
        <asp:BoundField DataField="BookingDate" HeaderText="Booking Date" DataFormatString="{0:yyyy-MM-dd}" />
        <asp:BoundField DataField="StartTime" HeaderText="Start Time" DataFormatString="{0:hh:mm tt}" />
        <asp:BoundField DataField="EndTime" HeaderText="End Time" DataFormatString="{0:hh:mm tt}" />
        <asp:BoundField DataField="TotalHours" HeaderText="Total Hours" />
        <asp:BoundField DataField="AmountPaid" HeaderText="Amount Paid" />
        <asp:BoundField DataField="PaymentStatus" HeaderText="Payment Status" />

        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:Button ID="btnCancelBooking" runat="server" Text="Cancel" CommandName="CancelBooking" 
                    CommandArgument='<%# Eval("BookingID") %>' CssClass="btn btn-danger"
                    OnClientClick="return confirm('Are you sure you want to cancel this booking?');" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="CarNo" HeaderText="Car Number" ReadOnly="True" 
            SortExpression="CarNo" />
    </Columns>
</asp:GridView>

</asp:Content>

