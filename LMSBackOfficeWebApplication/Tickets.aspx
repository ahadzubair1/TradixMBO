<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Tickets.aspx.cs" Inherits="LMSBackOfficeWebApplication.Tickets" %>

<asp:Content ID="TransactionContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        input, select, textarea {
            max-width: 100% !important;
        }

        .button-position {
            float: right;
        }

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
        }
    </style>
    <main>
        <!-- [ Main Content ] start -->
        <div class="pc-container">
            <div class="pc-content">
                <!-- [ breadcrumb ] start -->
                <div class="page-header">
                    <div class="page-block">
                        <div class="row align-items-center">
                            <div class="col-md-6">
                                <div class="page-header-title">
                                    <h2 class="mb-0">Tickets</h2>
                                </div>
                            </div>
                            <div class="col-md-6 text-end">
                                <ul class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="index.html">Home</a></li>
                                    <li class="breadcrumb-item"><a href="javascript: void(0)">Tickets</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- [ breadcrumb ] end -->

                <!-- [ Main Content ] start -->
                <div class="col-md-12 order-md-1">
                    <div class="row g-3 mb-3">

                        <div class="col-md-4">
                            <asp:TextBox ID="txtKeyword" runat="server" CssClass="form-control mw-100" placeholder="Enter Ticket Code / Ticket Type"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <%--<button class="btn btn-primary" type="submit">Search</button>--%>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" CausesValidation="false" />
                        </div>

                        <div class="col-md-6">
                            <button type="button" class="btn btn-primary" data-bs-toggle="modal" style="float: right;"
                                data-bs-target="#ticketModal" data-amount="1500" data-activation-fee="50" data-membership-name="Infinite">
                                Create Ticket</button>
                        </div>
                    </div>



                </div>
                <div class="tab-content pb-5" id="myTabContent">
                    <div class="tab-pane fade show active" id="home">

                        <div class="card">
                            <div class="card-body">
                                <div class="col-md-12 order-md-1">
                                    <div class="row g-3 mb-3">
                                        <div class="col-md-12" style="float: right;">
                                            <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="btn btn-primary button-position" OnClick="btnExportToExcel_Click" CausesValidation="false" />
                                        </div>
                                        <div class="overflow-x-auto">
                                            <asp:GridView ID="gv_tickets" runat="server" AutoGenerateColumns="False" GridLines="Vertical" AllowPaging="True" CssClass="table table-striped table-hover">
                                                <AlternatingRowStyle BackColor="Gainsboro" />
                                                <Columns>
                                                    <asp:BoundField DataField="TicketTitle" HeaderText="Title">
                                                        <ControlStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TicketCode" HeaderText="Ticket Code">
                                                        <ControlStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TicketType" HeaderText="Ticket Type">
                                                        <ControlStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Description" HeaderText="Description">
                                                        <ControlStyle Width="250px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TicketPriority" HeaderText="Priority">
                                                        <ControlStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CreatedDate" HeaderText="Created Date">
                                                        <ControlStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Status" HeaderText="Status">
                                                        <ControlStyle Width="100px" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                    <ControlStyle Width="100px" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="UpdatedDate" HeaderText="Updated Date">
                                                    <ControlStyle Width="100px" />
                                                    </asp:BoundField>

                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No Record Found<br />
                                                </EmptyDataTemplate>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <HeaderStyle BackColor="#232f45" CssClass="gvheader" ForeColor="White" Height="35" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Right" />
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- [ Main Content ] end -->
        </div>
        <div class="modal fade modal-animate anim-blur " id="ticketModal" tabindex="-1" aria-labelledby="ticketModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class=" modal-title font-bold fs-4" id="ticketModalLabel">Add New Ticket</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>

                    <div class="modal-body">
                        <div class="mb-3 col-12">
                            <label for="title" class="form-label">Title</label>
                            <input type="text" style="max-width: 100%" class="form-control w-100" id="txtTitle" name="title" />
                            <span class="errormessage" id="titleerror"></span>
                        </div>
                        <div class="mb-3 col-12">
                            <label for="amount" class="form-label">Ticket Type</label>
                            <%--<input type="text" style="max-width: 100%" class="form-control w-100" id="amount" name="amount" readonly />--%>
                            <asp:DropDownList ID="ddlTicketType" runat="server" CssClass="form-control w-100"></asp:DropDownList>
                        </div>
                        <div class="mb-3 col-12">
                            <label for="activationFee" class="form-label">Priority </label>
                            <%--<input type="text" style="max-width: 100%" class="form-control w-100" id="activationFee" name="activationFee" readonly />--%>
                            <asp:DropDownList ID="ddlPriority" runat="server" CssClass="form-control w-100">
                                <asp:ListItem Selected="True" Value="1">High</asp:ListItem>
                                <asp:ListItem Value="2">Low</asp:ListItem>
                                <asp:ListItem Value="3">Medium</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="mb-3 col-12">
                            <label for="description" class="form-label">Description</label>
                            <%--<textarea style="max-width: 100%" class="form-control w-100" id="txtDescription" name="description" />--%>
                            <textarea class="form-control" rows="4" id="txtDescription" name="description"></textarea>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ControlToValidate="txtDescription" ErrorMessage="Please Write Description"></asp:RequiredFieldValidator>--%>
                            <%--<input type="text" style="max-width: 100%" class="form-control w-100" id="membershipCode" name="membershipCode" readonly />--%>
                            <span class="errormessage" id="descriptionerror"></span>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" id="btnSubmit" class="btn btn-primary" onclick="Submit();">Submit</button>
                        <%--<asp:Button ID="submit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClientClick="Submit();" CausesValidation="false" />--%>
                    </div>

                </div>
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

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>

    <script type="text/javascript">
        function Submit() {
            debugger
            var isValid = false;
            var error = document.getElementById("descriptionerror");
            var titleError = document.getElementById("titleerror");
            var title = $('#txtTitle').val();
            if (title == "") {
                titleError.innerHTML = "Please Enter Title";
                titleError.style.visibility = 'visible';
                return false;

            }
            else {
                titleError.innerHTML = "";
                titleError.style.visibility = 'hidden';
            }

            var description = $('#txtDescription').val();
            if (description == "") {
                error.innerHTML = "Please Enter Description";
                error.style.visibility = 'visible';
                return false;

            }
            else {
                error.innerHTML = "";
                error.style.visibility = 'hidden';
            }
            var model = {
                "TicketTitle": $('#txtTitle').val(),
                "TicketType": $('#<%=ddlTicketType.ClientID%> option:selected').text(),
                "Priority": $('#<%=ddlPriority.ClientID%> option:selected').text(),
                "Description": description
            };

            SaveTicket(model);
        }
        <%--$('#btnSubmit').click(function () {
            var model = {
                "TicketTitle": $('#txtTitle').val(),
                "TicketType": $('#<%=ddlTicketType.ClientID%> option:selected').text(),
                "Priority": $('#<%=ddlPriority.ClientID%> option:selected').text(),
                "Description": document.getElementById('<%=txtDescription.ClientID%>').value
            };

            SaveTicket(model);
        });--%>

        function SaveTicket(ticket) {
            $.ajax({
                type: 'POST',
                url: 'Tickets.aspx/SaveTicket',
                data: JSON.stringify({ model: ticket }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.d == "success") {
                        $('#ticketModal').modal('hide');
                        window.location.reload();
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    // remove lion which variable doesn't exist.
                    console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + ticket);
                }
            });
        }

    </script>
</asp:Content>
