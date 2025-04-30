<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeFile="Booking.aspx.cs" Inherits="User_Booking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>/* General Styles */
body {
    font-family: Arial, sans-serif;
    background-color: #f8f9fa;
    margin: 0;
    padding: 0;
}

/* Container */
.container {
    width: 50%;
    margin: 50px auto;
    background: #fff;
    padding: 20px;
    border-radius: 10px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    text-align: center;
}

/* Header */
h2 {
    color: #333;
    margin-bottom: 20px;
}

/* Labels */
label {
    font-weight: bold;
    display: block;
    margin: 10px 0 5px;
    color: #555;
}

/* Input Fields */
input[type="text"], 
input[type="time"] {
    width: 100%;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
    margin-bottom: 15px;
    font-size: 16px;
}

/* Button */
button, 
input[type="submit"] {
    width: 100%;
    background: #007bff;
    color: white;
    border: none;
    padding: 12px;
    font-size: 18px;
    border-radius: 5px;
    cursor: pointer;
    transition: background 0.3s;
}

button:hover, 
input[type="submit"]:hover {
    background: #0056b3;
}

/* Success Message */
.success {
    color: green;
    font-weight: bold;
    margin-top: 10px;
}

/* Error Message */
.error {
    color: red;
    font-weight: bold;
    margin-top: 10px;
}

/* Responsive Design */
@media (max-width: 768px) {
    .container {
        width: 80%;
    }
}

@media (max-width: 480px) {
    .container {
        width: 90%;
        padding: 15px;
    }
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container">
            <h2>Confirm Booking</h2>
            <div>
                <label>Slot Number:</label>
                <asp:Label ID="lblSlotNumber" runat="server"></asp:Label>
            </div>
            <div>
                <label>Price Per Hour:</label>
                <asp:Label ID="lblPricePerHour" runat="server"></asp:Label>
            </div>
            <div>
                <label>Car Number:</label>
                <asp:TextBox ID="txtcarno" runat="server" ></asp:TextBox>
            </div>
            <div>
                <label>Start Time:</label>
                <asp:TextBox ID="txtStartTime" runat="server" TextMode="Time"></asp:TextBox>
            </div>
            <div>
                <label>End Time:</label>
                <asp:TextBox ID="txtEndTime" runat="server" TextMode="Time"></asp:TextBox>
            </div>
            <asp:HiddenField ID="hfSlotID" runat="server" />
            <div>
                <asp:Button ID="btnConfirmBooking" runat="server" Text="Confirm Booking" OnClick="btnConfirmBooking_Click" />
            </div>
        </div>
</asp:Content>

