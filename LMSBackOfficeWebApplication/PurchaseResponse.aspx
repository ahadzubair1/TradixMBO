<%@ Page Title="Tradix: Membership Purchase Response" Language="C#" CodeBehind="PurchaseResponse.aspx.cs" Inherits="LMSBackOfficeWebApplication.PurchaseResponse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Include common CSS files -->

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <link href="./Content/css/style.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <!-- Include Select2 CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />
    <!-- Include Select2 JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>
    <link href="./Content/css/icons.css" rel="stylesheet" />
    <link href="./Content/css/typography.css" rel="stylesheet" />
</head>

<body class="bg-white h-100">
    <div class="container-fluid bg-white  align-content-center">
        <div class="row mx-0">
            <div class="col-md-12 text-center">
                <img src="Content/images/success.gif" alt="" width="200" />
                <h2 id="message">
                    <asp:Label ID="messageLabel" runat="server"></asp:Label></h2>
            </div>
        </div>
    </div>
</body>
</html>

</html>