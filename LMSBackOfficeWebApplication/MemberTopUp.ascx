<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberTopUp.ascx.cs"  Inherits="LMSBackOfficeWebApplication.MemberTopUp" %>
<style type="text/css">
body
{
    margin: 0;
    padding: 0;
    font-family: Arial;
}
.progressmodal
{
    position: fixed;
    z-index: 10000;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: Black;
    filter: alpha(opacity=60);
    opacity: 0.6;
    -moz-opacity: 0.8;
}
.progresscenter
{
    z-index: 100011;
    margin: 300px auto;
    padding: 10px;
    width: 130px;
    background-color: White;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.progresscenter img
{
    height: 128px;
    width: 128px;
}
</style>

<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
<ProgressTemplate>
    <div class="progressmodal">
        <div class="progresscenter">
            <img alt="" src="img/Spinner.png" />
        </div>
    </div>
</ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
    <Triggers>        
        <%--<asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click"  />--%>   
        <asp:PostBackTrigger ControlID="btnSubmit"  />
    </Triggers>    
    <ContentTemplate>        
       <div class="modal fade modal-animate anim-blur" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
     <div class="modal-dialog">
         <div class="modal-content">
             <div class="modal-header">
                 <h1 class="modal-title font-bold fs-4" id="exampleModalLabel">Enter Amount to Add in USD</h1>
                 <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
             </div>
             <div class="modal-body">
                 <p>Enter amount you want to add to your credit wallet and choose a payment method.</p>
                 <%--<form action="/" method="post">--%>
                     <label class="mb-2">
                
                 Top Up Amount</label>
                     <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control w-100" Width="100%" MaxLength="4"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtAmount" ForeColor="Red">Please enter amount or select amount</asp:RequiredFieldValidator>
                 <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                 <%--<input type="text" name="name" id="txtAmount" runat="server" value="" class="form-control w-100" placeholder="Enter amount or select from below"
                         style="max-width: 100%;" />--%>
                     <div class="d-flex mt-3 justify-content-between">
                         <a class="btn btn-light-primary text-nowrap topupValue" data-value="50">$50</a>
                         <a class="btn btn-light-primary text-nowrap topupValue" data-value="250">$250</a>
                         <a class="btn btn-light-primary text-nowrap topupValue" data-value="500">$500</a>
                         <a class="btn btn-light-primary text-nowrap topupValue" data-value="750">$750</a>
                         <a class="btn btn-light-primary text-nowrap topupValue" data-value="1000">$1000</a>
                         <a class="btn btn-light-primary text-nowrap topupValue" data-value="1500">$1500</a>
                         <a class="btn btn-light-primary text-nowrap topupValue" data-value="">Clear</a>
                     </div>
                 <%--<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
                 <%--</form>--%>
             </div>
             <div class="modal-footer">
                 <button type="button" ID="Button1" class="btn btn-danger" data-bs-dismiss="modal">Cancel</button>
                 <%--<asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn btn-danger"  />--%>
                <%-- <button type="button" class="btn btn-primary"
                     id="btnSubmit" runat="server" onserverclick4="TotpUp_Click" >Top Up</button>--%>
                 <asp:Button ID="btnSubmit" runat="server" Text="Top Up" CssClass="btn btn-primary" Enabled="True" OnClick="TotpUp_Click" />
             </div>
         </div>
     </div>
 </div>
        <div class="modal fade modal-animate anim-blur" id="messageModal" tabindex="-1" aria-labelledby="messageModalLabel" aria-hidden="true">
                 <div class="modal-dialog">
         <div class="modal-content">
             <div class="modal-header">
                 <h1 class="modal-title font-bold fs-4" id="messageModalLabel">Message</h1>
                 <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
             </div>
             <div class="modal-body">
                 <p id="message"></p>
                   </div>
             <div class="modal-footer">
                 <button type="button" ID="btnOk" class="btn btn-default" data-bs-dismiss="modal" onclick="Close();">Ok</button>      
             </div>
         </div>
     </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
   <!-- Modal -->

<script type="text/javascript">
    $(document).ready(function () {
        $(".topupValue").click(function () {
            var value = $(this).attr('data-value');
            var txtFld = document.getElementById("<%=txtAmount.ClientID%>");
            txtFld.value = value;
        });
    });
</script>

<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
<script type="text/javascript">
    function ClosePopup(message) {
        var txtFld = document.getElementById("<%=txtAmount.ClientID%>");
        txtFld.value = "";
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();

        var messageLabel = document.getElementById("message");
        messageLabel.innerHTML = message;

  
        $("#messageModal").modal("show"); 
    }

    function Close() {
        debugger;
        var lblMessage = document.getElementById("<%=lblMessage.ClientID%>");
        lblMessage.value = "";
         var txtFld = document.getElementById("<%=txtAmount.ClientID%>");
        txtFld.value = "";

        $("#exampleModal").modal('hide');
         $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
        $("#messageModal").removeClass("show"); 
       // $("#messageModal").addClass("hide"); 
    }
</script>
  

