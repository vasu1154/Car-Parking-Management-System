<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Admin_AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        /* Dashboard Page Styles */
.dashboard-container {
    padding: 20px;
    background-color: #f8f9fa;
}

/* Cards Styling */
.card {
    border-radius: 10px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    transition: transform 0.3s ease-in-out, box-shadow 0.3s ease-in-out;
}

.card:hover {
    transform: translateY(-5px);
    box-shadow: 0 6px 12px rgba(0, 0, 0, 0.15);
}

.card h5 {
    font-size: 1.2rem;
    font-weight: bold;
}

.card h3 {
    font-size: 2rem;
}

/* Colors for different sections */
.bg-primary {
    background-color: #007bff !important;
}

.bg-danger {
    background-color: #dc3545 !important;
}

.bg-success {
    background-color: #28a745 !important;
}

/* Chart Container */
.chart-container {
    background: white;
    border-radius: 10px;
    padding: 20px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    margin-top: 20px;
}

/* Responsive Layout */
@media (max-width: 768px) {
    .card h3 {
        font-size: 1.5rem;
    }

    .card h5 {
        font-size: 1rem;
    }
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <center>
    <div class="dashboard-container">
        <h2 class="mb-4">📊 Admin Dashboard</h2>

        <div class="row">
            <!-- Total Slots -->
            <div class="col-md-4">
                <div class="card bg-primary text-white text-center p-4">
                    <h5>Total Parking Slots</h5>
                    <h3><asp:Label ID="lblTotalSlots" runat="server" Text="0"></asp:Label></h3>
                </div>
            </div>

            <!-- Booked Slots -->
            <div class="col-md-4">
                <div class="card bg-danger text-white text-center p-4">
                    <h5>Booked Slots</h5>
                    <h3><asp:Label ID="lblBookedSlots" runat="server" Text="0"></asp:Label></h3>
                </div>
            </div>

            <!-- Available Slots -->
            <div class="col-md-4">
                <div class="card bg-success text-white text-center p-4">
                    <h5>Available Slots</h5>
                    <h3><asp:Label ID="lblAvailableSlots" runat="server" Text="0"></asp:Label></h3>
                </div>
            </div>
        </div>

        <!-- Booking Overview Chart -->
        <div class="chart-container">
            <h5>📈 Bookings Overview</h5>
            <canvas id="bookingChart"></canvas>
        </div>
    </div>
</asp:Content>

