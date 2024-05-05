<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CoinPaymentSuccess.aspx.cs" Inherits="LMSBackOfficeWebApplication.CoinPaymentSuccess" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="response-text">
        <hr />
        <h2>Thank you</h2>
        <hr />
        Your order has been successfully processed!
        <br />
        <br />
        ORDER NUMBER: <%:Session["OrderNo"] %>
    </div>
</asp:Content>
