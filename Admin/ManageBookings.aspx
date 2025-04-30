<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageBookings.aspx.cs" Inherits="Admin_ManageBookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="stylesheet" type="text/css" href="../Asset/css/bootstrap.min.css" />
    <div class="container mt-4">
            <h2>🅿️ Manage Bookings</h2>

            <!-- GridView to Display Bookings -->
            <asp:GridView ID="gvBookings" runat="server" CssClass="table table-bordered"
                AutoGenerateColumns="False" DataKeyNames="BookingID" 
                OnRowCommand="gvBookings_RowCommand" onrowdeleting="gvBookings_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="BookingID" HeaderText="ID" />
                    <asp:BoundField DataField="FullName" HeaderText="User" />
                    <asp:BoundField DataField="SlotNumber" HeaderText="Slot No." />
                    <asp:BoundField DataField="CarNo" HeaderText="Car Number" ReadOnly="True" 
                        SortExpression="CarNo" />
                    <asp:BoundField DataField="BookingDate" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                    <asp:BoundField DataField="AmountPaid" HeaderText="Amount Paid" />
                    <asp:BoundField DataField="PaymentStatus" HeaderText="Status" />

                    
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnApprove" runat="server" CommandName="Approve" CommandArgument='<%# Eval("BookingID") %>' 
                                Text="Approve" CssClass="btn btn-success btn-sm" />
                            <asp:Button ID="btnReject" runat="server" CommandName="Reject" CommandArgument='<%# Eval("BookingID") %>' 
                                Text="Reject" CssClass="btn btn-danger btn-sm" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("BookingID") %>' 
                                Text="❌ Delete" CssClass="btn btn-dark btn-sm" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            
        </div>
</asp:Content>

