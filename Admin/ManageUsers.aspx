<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="Admin_ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        /* Container Styling */
.manage-users-container {
    max-width: 900px;
    margin: 0 auto;
    padding: 20px;
}

/* Card Styling */
.card {
    border-radius: 10px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    transition: transform 0.3s, box-shadow 0.3s;
}

.card:hover {
    transform: translateY(-5px);
    box-shadow: 0 6px 12px rgba(0, 0, 0, 0.15);
}

/* Table Styling */
.table {
    margin-top: 10px;
    text-align: center;
    border-radius: 10px;
    overflow: hidden;
}

.table-bordered {
    border: 1px solid #dee2e6;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="manage-users-container">
        <h2 class="text-center mb-4">👤 Manage Users</h2>

        <!-- Users List -->
        <div class="card p-4">
            <h5 class="text-center">📋 Registered Users</h5>
            <asp:GridView ID="gvUsers" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="UserID"
                OnRowEditing="gvUsers_RowEditing" OnRowCancelingEdit="gvUsers_RowCancelingEdit" OnRowUpdating="gvUsers_RowUpdating"
                OnRowDeleting="gvUsers_RowDeleting">
                
                <Columns>
                    <asp:BoundField DataField="UserID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                    <asp:BoundField DataField="Role" HeaderText="Role" />

                    
                    <asp:CommandField ShowEditButton="True" ButtonType="Button" EditText="✏️ Edit" />
                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" DeleteText="❌ Delete" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

