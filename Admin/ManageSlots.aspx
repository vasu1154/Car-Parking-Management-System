<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageSlots.aspx.cs" Inherits="Admin_ManageSlots" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        /* Container Styling */
.manage-slots-container {
    max-width: 800px;
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

/* Form Inputs */
.form-group {
    margin-bottom: 15px;
}

.form-control {
    border-radius: 5px;
    padding: 8px;
}

/* Button Styling */
.btn {
    font-size: 16px;
    border-radius: 5px;
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
    <div class="manage-slots-container">
        <h2 class="text-center mb-4">🅿️ Manage Parking Slots</h2>

        <!-- Add Slot Form -->
        <div class="card p-4">
            <h5 class="text-center">➕ Add New Slot</h5>
            <div class="form-group">
                <label>Slot Number:</label>
                <asp:TextBox ID="txtSlotNumber" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Price Per Hour ($):</label>
                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <asp:Button ID="btnAddSlot" runat="server" Text="Add Slot" CssClass="btn btn-primary mt-2" OnClick="btnAddSlot_Click" />
        </div>

        <!-- Parking Slots List -->
        <div class="card p-4 mt-4">
            <h5 class="text-center">📋 Parking Slots List</h5>
            <asp:GridView ID="gvSlots" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="SlotID"
                OnRowEditing="gvSlots_RowEditing" OnRowCancelingEdit="gvSlots_RowCancelingEdit" OnRowUpdating="gvSlots_RowUpdating"
                OnRowDeleting="gvSlots_RowDeleting">
                
                <Columns>
                    <asp:BoundField DataField="SlotID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="SlotNumber" HeaderText="Slot Number" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="PricePerHour" HeaderText="Price Per Hour ($)" />

                    
                    <asp:CommandField ShowEditButton="True" ButtonType="Button" EditText="✏️ Edit" />
                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" DeleteText="❌ Delete" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

