<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="User_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        /* Hero Section */
        .hero-section {
            background: url('../Asset/images/parking-banner.jpg') no-repeat center center/cover;
            color: white;
            text-align: center;
            padding: 80px 20px;
            position: relative;
        }

        .hero-section::before {
            content: "";
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 0.5);
        }

        .hero-content {
            position: relative;
            z-index: 2;
        }

        .hero-section h1 {
            font-size: 42px;
            font-weight: bold;
        }

        .hero-section p {
            font-size: 20px;
            margin-top: 10px;
        }

        /* Card Styling */
        .card {
            box-shadow: 0 5px 10px rgba(0, 0, 0, 0.2);
            border-radius: 12px;
            transition: transform 0.3s ease-in-out;
            background: white;
        }

        .card:hover {
            transform: translateY(-5px);
        }

        .card-icon {
            font-size: 40px;
            color: #007bff;
        }

        .stats {
            font-size: 26px;
            font-weight: bold;
        }

        /* Quick Actions */
        .quick-actions {
            text-align: center;
            margin-top: 40px;
        }

        .quick-actions a {
            display: inline-block;
            background: #007bff;
            color: white;
            padding: 12px 20px;
            margin: 10px;
            border-radius: 6px;
            text-decoration: none;
            transition: 0.3s ease;
        }

        .quick-actions a:hover {
            background: #0056b3;
        }

        /* Footer */
        .footer {
            background: #343a40;
            color: white;
            text-align: center;
            padding: 15px;
            margin-top: 50px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    <!-- Hero Section -->
    <div class="hero-section">
        <div class="hero-content">
            <h1>🚗 Welcome to Car Parking Management</h1>
            <p>Check availability, book slots, and manage your parking with ease!</p>
        </div>
    </div>
    <div class="col-md-12 mt-4">
    <div class="card text-center p-4">
        <i class="card-icon bi bi-geo-alt-fill"></i>
        <h5 class="mt-3">📍 Parking Location</h5>
        <asp:Label ID="lblParkingAddress" runat="server" CssClass="stats text-dark">123 Main Street, Downtown, Anand</asp:Label>
    </div>
</div>
    <!-- Dashboard Overview -->
    <div class="container mt-5">
        <div class="row">
            <!-- Available Slots -->
            <div class="col-md-4">
                <div class="card text-center p-4">
                    <i class="card-icon bi bi-car-front-fill"></i>
                    <h5 class="mt-3">Available Slots</h5>
                    <asp:Label ID="lblAvailableSlots" runat="server" CssClass="stats text-success"></asp:Label>
                </div>
            </div>

            <!-- Booked Slots -->
            <div class="col-md-4">
                <div class="card text-center p-4">
                    <i class="card-icon bi bi-calendar-check-fill"></i>
                    <h5 class="mt-3">Booked Slots</h5>
                    <asp:Label ID="lblBookedSlots" runat="server" CssClass="stats text-danger"></asp:Label>
                </div>
            </div>

            <!-- Total Slots -->
            <div class="col-md-4">
                <div class="card text-center p-4">
                    <i class="card-icon bi bi-grid-fill"></i>
                    <h5 class="mt-3">Total Slots</h5>
                    <asp:Label ID="lblTotalSlots" runat="server" CssClass="stats text-primary"></asp:Label>
                </div>
            </div>
        </div>

        <!-- Quick Actions -->
        <div class="quick-actions">
            <h3 class="mt-5">🚀 Quick Actions</h3>
            <a href="CheckAvailability.aspx">Check Availability</a>
            <a href="BookingHistory.aspx">View Booking History</a>
            <a href="Profile.aspx">Edit Profile</a>
        </div>

        <!-- Recent Bookings -->
        <div class="mt-5">
            <h3>📅 Recent Bookings</h3>
            <asp:GridView ID="gvRecentBookings" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="SlotNumber" HeaderText="Slot Number" />
                    <asp:BoundField DataField="BookingDate" HeaderText="Booking Date" />
                    <asp:BoundField DataField="StartTime" HeaderText="Start Time" />
                    <asp:BoundField DataField="EndTime" HeaderText="End Time" />
                    <asp:BoundField DataField="AmountPaid" HeaderText="Amount Paid (₹)" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    
</asp:Content>
