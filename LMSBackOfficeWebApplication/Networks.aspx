<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Networks.aspx.cs" Inherits="LMSBackOfficeWebApplication.Networks" %>

<asp:Content ID="NetworkContent1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        thead, tbody, tfoot, tr, td, th {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            padding: 0.5rem 0.5rem;
        }

        .errormessage {
            color: red;
        }

        .gvheader {
            text-transform: uppercase;
            font-size: 13px;
            padding: 10px !important;
        }

        .pagerstyle {
            padding: 0;
        }
    </style>
    <main>


        <div class="offcanvas pc-announcement-offcanvas offcanvas-end" tabindex="-1" id="announcement" aria-labelledby="announcementLabel">
            <div class="offcanvas-header">
                <h5 class="offcanvas-title" id="announcementLabel">What's new?</h5>
                <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
            </div>
            <div class="offcanvas-body">
                <p class="text-span">Today</p>
                <div class="card mb-3">
                    <div class="card-body">
                        <div class="align-items-center d-flex flex-wrap gap-2 mb-3">
                            <div class="badge bg-light-success f-12">Big News</div>
                            <p class="mb-0 text-muted">2 min ago</p>
                            <span class="badge dot bg-warning"></span>
                        </div>
                        <h5 class="mb-3">Forex Today</h5>
                        <p class="text-muted">Forex Today: The Dollar looks at NFP for fresh oxygen</p>
                        <img src="https://dixdeynibyck7.cloudfront.net/images/content/Forex/DOLLAR_03_L.jpg" alt="img" class="img-fluid mb-3" />
                        <div class="row">
                            <div class="col-12">
                                <div class="d-grid">
                                    <a
                                        class="btn btn-outline-secondary"
                                        href="#"
                                        target="_blank">Check Now</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card mb-3">
                    <div class="card-body">
                        <div class="align-items-center d-flex flex-wrap gap-2 mb-3">
                            <div class="badge bg-light-success f-12">Big News</div>
                            <p class="mb-0 text-muted">2 min ago</p>
                            <span class="badge dot bg-warning"></span>
                        </div>
                        <h5 class="mb-3">Forex Today</h5>
                        <p class="text-muted">Forex Today: The Dollar looks at NFP for fresh oxygen</p>
                        <img src="https://dixdeynibyck7.cloudfront.net/images/content/Forex/DOLLAR_03_L.jpg" alt="img" class="img-fluid mb-3" />
                        <div class="row">
                            <div class="col-12">
                                <div class="d-grid">
                                    <a
                                        class="btn btn-outline-secondary"
                                        href="#"
                                        target="_blank">Check Now</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <p class="text-span mt-4">Yesterday</p>
                <div class="card mb-3">
                    <div class="card-body">
                        <div class="align-items-center d-flex flex-wrap gap-2 mb-3">
                            <div class="badge bg-light-success f-12">Big News</div>
                            <p class="mb-0 text-muted">2 min ago</p>
                            <span class="badge dot bg-warning"></span>
                        </div>
                        <h5 class="mb-3">Forex Today</h5>
                        <p class="text-muted">Forex Today: The Dollar looks at NFP for fresh oxygen</p>
                        <img src="https://dixdeynibyck7.cloudfront.net/images/content/Forex/DOLLAR_03_L.jpg" alt="img" class="img-fluid mb-3" />
                        <div class="row">
                            <div class="col-12">
                                <div class="d-grid">
                                    <a
                                        class="btn btn-outline-secondary"
                                        href="#"
                                        target="_blank">Check Now</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card mb-3">
                    <div class="card-body">
                        <div class="align-items-center d-flex flex-wrap gap-2 mb-3">
                            <div class="badge bg-light-success f-12">Big News</div>
                            <p class="mb-0 text-muted">2 min ago</p>
                            <span class="badge dot bg-warning"></span>
                        </div>
                        <h5 class="mb-3">Forex Today</h5>
                        <p class="text-muted">Forex Today: The Dollar looks at NFP for fresh oxygen</p>
                        <img src="https://dixdeynibyck7.cloudfront.net/images/content/Forex/DOLLAR_03_L.jpg" alt="img" class="img-fluid mb-3" />
                        <div class="row">
                            <div class="col-12">
                                <div class="d-grid">
                                    <a
                                        class="btn btn-outline-secondary"
                                        href="#"
                                        target="_blank">Check Now</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- [ Header ] end -->



        <!-- [ Main Content ] start -->
        <div class="pc-container">
            <div class="pc-content">
                <!-- Banner -->
                <div class="banner wallets">
                    <div class="banner-caption">
                        <h2 class="text-white">More Referrals, Huge Bonus!</h2>
                        <p class="m-0">Unlock substantial rewards with our latest promotion: More Referrals, Huge Bonus! Refer your friends, colleagues, or connections and reap the benefits of our generous bonus program.</p>
                    </div>
                    <div class="leaf">
                        <div>
                            <img src="Content/images/Fall-Autumn-Leaves-Transparent-PNG.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Autumn-Fall-Leaves-Pictures-Collage-PNG.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Autumn-Fall-Leaves-Clip-Art-PNG.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Green-Leaves-PNG-File.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Transparent-Autumn-Leaves-Falling-PNG.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Realistic-Autumn-Fall-Leaves-PNG.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Fall-Autumn-Leaves-Transparent-PNG.png" height="75" width="75">
                        </div>
                    </div>

                    <div class="leaf leaf1">
                        <div>
                            <img src="Content/images/Fall-Autumn-Leaves-Transparent-PNG.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Autumn-Fall-Leaves-Pictures-Collage-PNG.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Autumn-Fall-Leaves-Clip-Art-PNG.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Green-Leaves-PNG-File.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Transparent-Autumn-Leaves-Falling-PNG.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Realistic-Autumn-Fall-Leaves-PNG.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Fall-Autumn-Leaves-Transparent-PNG.png" height="75" width="75">
                        </div>
                    </div>

                    <div class="leaf leaf2">
                        <div>
                            <img src="Content/images/Fall-Autumn-Leaves-Transparent-PNG.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Autumn-Fall-Leaves-Pictures-Collage-PNG.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Autumn-Fall-Leaves-Clip-Art-PNG.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Green-Leaves-PNG-File.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Transparent-Autumn-Leaves-Falling-PNG.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Realistic-Autumn-Fall-Leaves-PNG.png" height="75" width="75">
                        </div>
                        <div>
                            <img src="Content/images/Fall-Autumn-Leaves-Transparent-PNG.png" height="75" width="75">
                        </div>
                    </div>
                    <div class="bg-line">
                        <img src="Content/images/wave.svg" alt="Line">
                        <img src="Content/images/wave.svg" alt="Line">
                    </div>
                </div>
                <!-- [ breadcrumb ] start -->
                <div class="page-header">
                    <div class="page-block">
                        <div class="row align-items-center">
                            <div class="col-md-6">
                                <div class="page-header-title">
                                    <h2 class="mb-0">Networks</h2>
                                </div>
                            </div>
                            <div class="col-md-6 text-end">
                                <ul class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="index.html">Home</a></li>
                                    <li class="breadcrumb-item"><a href="javascript: void(0)">Networks</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- [ breadcrumb ] end -->
                <!-- [ Main Content ] start -->
                <ul class="nav nav-tabs mb-3" id="t_tabs" role="tablist">
                    <li class="nav-item">
                        <a class="nav-link active text-uppercase" id="home-tab" data-bs-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">Network</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link text-uppercase" id="referrels-tab" data-bs-toggle="tab" href="#referrels" role="tab" aria-controls="referrels" aria-selected="false">Direct Referrals</a>
                    </li>
                </ul>

                <div class="tab-content pb-5" id="myTabContent">
                    <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
                        <div class="card">
                            <div class="card-body">
                                <div class="mb-4 tree-controls gap-3 d-flex align-items-center">

                                    <div class="flex-column">
                                        <div class="input-group mb-md-0 mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fas fa-user fa-sm fa-fw text-gray-400"></i></span>
                                            </div>
                                            <%--                                            <input type="text" class="form-control" id="searchnode" placeholder="Find a user">--%>
                                            <asp:TextBox runat="server" ID="txtSearch" CssClass="form-control" Placeholder="Search a user"></asp:TextBox>
                                            <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn bg-primary-t text-white" Enabled="true" OnClick="btnSearch_Click" />
                                        </div>
                                        <div runat="server" id="divSerachMessage">
                                            <div class="alert alert-danger small py-1 m-0" role="alert">
                                                <i class="ti ti-alert-circle"></i>
                                                <asp:Label ID="lblSearchMessage" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="btn-group tree-filter bg-white p-2 shadow-sm">
                                        <%-- <a href="/networks?id={variable}" class="btn btn-light btn-shadow col fileinput-button dz-clickable">--%>
                                        <%--<a href="/networks?memberkey={{ leftFarNodeId }}" class="btn btn-success col fileinput-button dz-clickable">--%>
                                        <a href="/networks?memberkey=<%= leftFarNodeId %>" class="btn col fileinput-button dz-clickable">


                                            <i class="fas fa-solid fa-arrow-left" style="transform: rotate(-45deg)"></i>
                                            <span><strong>Far Left</strong></span>
                                        </a>

                                        <a href="/networks" class="btn col start">
                                            <span><strong>Top</strong></span>
                                        </a>

                                        <a href="/networks?memberkey=<%= rightFarNodeId %>" class="btn col cancel">
                                            <i class="fas fa-solid fa-arrow-right" style="transform: rotate(45deg)"></i>
                                            <span><strong>Far Right</strong></span>
                                        </a>
                                    </div>
                                </div>
                                <div id="orgChartContainer" style="padding-top: 100px;">
                                    <div id="orgChart">
                                        <div class="tree">
                                            <asp:Literal ID="litGeneratedHtml" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="referrels" role="tabpanel" aria-labelledby="referrels-tab">
                        <asp:UpdatePanel ChildrenAsTriggers="true" runat="server">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-body">
                                        <h5 class="card-title">My Referrals</h5>
                                        <div id="dvGrid">
                                            <asp:GridView ID="gvReferrelsTable" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" PageSize="10"
                                                CellPadding="3" AutoGenerateColumns="False" GridLines="Vertical" Width="100%" AllowPaging="True" OnPageIndexChanging="gvReferrelsTable_PageIndexChanging">
                                                <AlternatingRowStyle BackColor="Gainsboro" />
                                                <Columns>
                                                    <asp:BoundField DataField="MemberFullName" HeaderText="Member Name" ControlStyle-CssClass="gvheader">
                                                        <ControlStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Subscription" HeaderText="Subscription" ControlStyle-CssClass="gvheader">
                                                        <ControlStyle Width="100px" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="PurchasedDate" HeaderText="Membership Expiry" ControlStyle-CssClass="gvheader">
                                                        <ControlStyle Width="250px" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="MembershipStatus" HeaderText="Status">
                                                        <ControlStyle Width="100px" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No record found<br />
                                                </EmptyDataTemplate>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <HeaderStyle BackColor="#232f45" CssClass="gvheader" ForeColor="White" Height="45" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Right" CssClass="pagerstyle" />
                                                <RowStyle ForeColor="Black" BackColor="#EEEEEE" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#000065" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>

                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
            <!-- [ Main Content ] end -->
        </div>
        </div>
    
     <!-- [ Main Content ] end -->
        <footer class="pc-footer">
            <div class="footer-wrapper container-fluid">
                <div class="row flex-md-row flex-column">
                    <div class="col my-1">
                        <p class="m-0">
                            Copyright &#169; 2024 <a href="https://tradiix.com/" target="_blank">Tradiix.com</a>
                        </p>
                    </div>
                    <div class="col-auto my-1">
                        <ul class="list-inline footer-link mb-0">
                            <li class="list-inline-item"><a href="">Terms & Conditions</a></li>
                            <li class="list-inline-item"><a href="" target="_blank">Privacy Policy</a></li>
                            <li class="list-inline-item"><a href="" target="_blank">Support</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </footer>
    </main>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery.blockUI/2.70/jquery.blockUI.min.js"></script>
    <script type="text/javascript">
        $(function () {
            BlockUI("dvGrid");
            $.blockUI.defaults.css = {};
        });
        function BlockUI(elementID) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function () {
                $("#" + elementID).block({
                    message: '<div align = "center">' + '<img src="https://www.google.com/url?sa=i&url=https%3A%2F%2Fpngtree.com%2Ffree-png-vectors%2Floading-gif&psig=AOvVaw3TT7ZHlYDOoSpu6JC6kqMr&ust=1711716871551000&source=images&cd=vfe&opi=89978449&ved=0CBIQjRxqFwoTCIiA7LKAl4UDFQAAAAAdAAAAABAY"/></div>',
                    css: {},
                    overlayCSS: { backgroundColor: '#000000', opacity: 0.6, border: '3px solid #63B2EB' }
                });
            });
            prm.add_endRequest(function () {
                $("#" + elementID).unblock();
            });
        };
    </script>
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            var searchButton = document.getElementById('<%= btnSearch.ClientID %>');
            if (searchButton) {
                searchButton.addEventListener("click", function () {
                    searchButton.click();
                });
            }
        });
    </script>
</asp:Content>
