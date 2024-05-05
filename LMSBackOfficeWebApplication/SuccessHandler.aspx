<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SuccessHandler.aspx.cs" Inherits="LMSBackOfficeWebApplication.SuccessHandler" %>

<asp:Content ID="CheckoutContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .txtWalletAddress {
            max-width: 350px !important;
        }
    </style>
    <div class="pc-container">
        <div class="pc-content">
            <div class="py-5 text-center">
                <h2>Payment Confirmation</h2>
                <asp:Label ID="lblConfirmation" runat="server" Text=""></asp:Label>
            </div>

        </div>
    </div>


</asp:Content>
