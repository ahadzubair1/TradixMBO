<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Checkout.aspx.cs" Inherits="LMSBackOfficeWebApplication.Checkout" %>


<asp:Content ID="CheckoutContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .txtWalletAddress {
            max-width: 350px !important;
        }
    </style>
    <div class="pc-container">
        <div class="pc-content">
            <!-- [ breadcrumb ] start -->
            <div class="page-header">
                <div class="page-block">
                    <div class="row align-items-center">
                        <div class="col-md-12">
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Dashboard.aspx">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0)">Checkout Form</a></li>
                            </ul>
                        </div>
                        <div class="col-md-12">
                            <div class="page-header-title">
                                <h2 class="mb-0">Checkout Form</h2>
                            </div>
                        </div>
                        <div class="col-md-12"></div>
                    </div>
                </div>
            </div>
            <!-- [ breadcrumb ] end -->
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-4 order-md-2 mb-4">
                            <h4 class="d-flex justify-content-between align-items-center mb-3">
                                <span class="text-muted">Your Cart</span>
                                <span class="badge badge-secondary badge-pill">3</span>
                            </h4>
                            <ul class="list-group mb-3">
                                <li class="list-group-item d-flex justify-content-between lh-condensed">
                                    <div>
                                        <h6 class="my-0">Topup Amount</h6>
                                    </div>
                                    <span class="text-muted">
                                        <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
                                    </span>
                                </li>


                                <li class="list-group-item d-flex justify-content-between">
                                    <span>Total (USD)</span>
                                    <strong>
                                        <asp:Label ID="lblTotalAmount" runat="server" Text=""></asp:Label>
                                    </strong>

                                </li>
                                <li class="list-group-item d-flex justify-content-between" id="activationfee" runat="server">
                                    <div>
                                        <small class="text-muted" style="font-size: 80%; color: maroon !important;">
                                            <asp:Label ID="lblFeeText" runat="server" Text=""></asp:Label>
                                        </small>
                                    </div>

                                </li>
                            </ul>

                        </div>
                        <div class="col-md-8 order-md-1">
                            <h4 class="mb-3">Billing Address</h4>
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label for="firstName">Member Code</label>
                                    <asp:TextBox ID="txtMemberCode" runat="server" CssClass="form-control mw-100" Enabled="False" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <label for="lastName">Member Full Name</label>
                                    <asp:TextBox ID="txtMemberFullName" runat="server" CssClass="form-control mw-100"></asp:TextBox>
                                </div>
                            </div>

                            <div class="mb-3">
                                <label for="email">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mw-100" ReadOnly="True"></asp:TextBox>

                            </div>

                            <hr class="mb-4">
                            <div class="row">
                                <div class="col-md-12 mb-3">
                                    <div class="text-end d-flex align-items-center gap-3">
                                        <div>
                                            <input id="agreement1" type="checkbox" name="agreement" value="" style="vertical-align: middle;" onchange="openCoinPaymentPopup(this)">
                                            <label for="agreement1" style="font-size: 12px; font-weight: normal; color: maroon !important;">Caution : Your Browser Popup Must be  Enabled in Order to Proceed</label>
                                        </div>
                                        <asp:ImageButton ID="CheckoutImageBtn" CausesValidation="false" runat="server" ImageUrl="https://www.coinpayments.net/images/pub/checkout-blue.png" OnClick="CheckoutBtn_ServerClick" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 mb-3">
                                    <div class="text-end d-flex align-items-center gap-3">
                                        <div>
                                            <label for="agreement1" style="font-size: 12px; font-weight: normal; color: green !important;">
                                                NOTE: TRON is Supported for now but Provision of More Coins / Cryptocurrencies Will be Provided Soon.</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade modal-animate anim-blur" id="topupExistsModel" tabindex="-1" aria-labelledby="topupExistsModelLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content" style="color: #000 !important;">
                <div class="modal-header">
                    <h1 class="modal-title font-bold fs-4" id="topupExistsModelLabel">Message</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" onclick="CloseTopupPopup();"></button>
                </div>
                <div class="modal-body">
                    <p id="topupErrorMessage"></p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" onclick="CloseTopupPopup();">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <script type="text/javascript">
        var button = document.getElementById('<%=CheckoutImageBtn.ClientID%>');
        button.disabled = true;
        button.src = 'https://www.coinpayments.net/images/pub/checkout-grey.png';
        function HideSideBar(message) {
            if ($("#sidebar") != null || $("#sidebar") != undefined) {
                //  $("#sidebar").hide();
                $("#btnTopup").prop("disabled", true);
            }
        }

        function ShowTopupMessage(message) {
            var messageLabel = document.getElementById("topupErrorMessage");
            messageLabel.innerHTML = message;

            $("#topupExistsModel").modal("show");
            $('body').addClass('modal-open');
            $("#membershipExpiryeModal").removeClass("hide");
        }

        function CloseTopupPopup() {
            $("#topupExistsModel").modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $("#topupExistsModel").removeClass("show");
        }

        function openCoinPaymentPopup(evt) {
            //var url = 'https://www.coinpayments.net/index.php?ipn_version=1.0&cmd=_pay_auto&ipn_type=simple&ipn_mode=hmac&merchant=68e5db2609b76a2000db2471a3d68473&allow_extra=0https://www.coinpayments.net/index.php?ipn_version=1.0&cmd=_pay_auto&ipn_type=simple&ipn_mode=hmac&merchant=68e5db2609b76a2000db2471a3d68473&allow_extra=0&currency=USDhttps://www.coinpayments.net/index.php?ipn_version=1.0&cmd=_pay_auto&ipn_type=simple&ipn_mode=hmac&merchant=68e5db2609b76a2000db2471a3d68473&allow_extra=0&currency=USD&amountf=10.00&item_name=totup&address=TAfabv4ii5ceA2dP83PtBgEjhaQSumgGx1';
            //var newWindow = window.open(url, '_blank');
            //if (newWindow == null) {
            //    // Popup blocked, display a message to the user
            //    alert("Popup blocked. Please disable the popup blocker for this site.");
            //    return false;
            //}
            var button = document.getElementById('<%=CheckoutImageBtn.ClientID%>');

            if (evt.checked) {
                button.disabled = false;
                button.src = 'https://www.coinpayments.net/images/pub/checkout-blue.png';

            } else {
                button.disabled = true;
                button.src = 'https://www.coinpayments.net/images/pub/checkout-grey.png';
            }
        }
    </script>
</asp:Content>
