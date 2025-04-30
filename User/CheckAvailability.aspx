
<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeFile="CheckAvailability.aspx.cs" Inherits="User_CheckAvailability" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
        .page-title {
            font-size: 28px;
            font-weight: bold;
            text-align: center;
            margin-bottom: 20px;
        }

        .search-container {
            display: flex;
            justify-content: center;
            gap: 10px;
            margin-bottom: 20px;
        }

        .slot-card {
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            transition: transform 0.2s ease-in-out;
            cursor: pointer;
            text-align: center;
            padding: 15px;
            background-color: white;
        }

        .slot-card:hover {
            transform: scale(1.05);
        }

        .available {
            color: green;
            font-weight: bold;
        }

        .booked {
            color: red;
            font-weight: bold;
        }

        .card-body {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container">
        <h2 class="page-title">🅿️ Check & Book Parking Slots</h2>

        <!-- Search & Filter Section -->
        <div class="search-container">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="🔍 Search Slot Number..." />
            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                <asp:ListItem Value="">All</asp:ListItem>
                <asp:ListItem Value="Available">Available</asp:ListItem>
                <asp:ListItem Value="Booked">Booked</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
        </div>
        <div class="col-md-12 mt-4">
    <div class="card text-center p-4">
        <i class="card-icon bi bi-geo-alt-fill"></i>
        <h5 class="mt-3">📍 Parking Location</h5>
        <asp:Label ID="lblParkingAddress" runat="server" CssClass="stats text-dark">123 Main Street, Downtown, Anand</asp:Label>
    </div>
</div><br />
        <!-- Slot Display Section -->
        <div class="row" id="slotContainer" runat="server">
            <!-- Slots will be dynamically generated -->
        </div>

        <!-- Booking Confirmation Modal -->
        
    </div>

    
</asp:Content>

