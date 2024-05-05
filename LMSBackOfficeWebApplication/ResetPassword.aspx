<%@ Page Title="Reset Password" Language="C#" CodeBehind="ResetPassword.aspx.cs" Inherits="LMSBackOfficeWebApplication.ResetPassword" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Include common CSS files -->

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <link href="./Content/css/style.css" rel="stylesheet" />
    <link href="./Content/css/icons.css" rel="stylesheet" />
    <link href="./Content/css/typography.css" rel="stylesheet" />
    <link href="./Content/fonts/tabler-icons.min.css" rel="stylesheet" />
    <link href="./Content/fonts/feather.css" rel="stylesheet" />
    <link href="./Content/fonts/fontawesome.css" rel="stylesheet" />

    <style>
        .theGlobe {
            text-align: center;
            position: absolute;
            top: 70%;
            z-index: -1;
            right: 0;
            opacity: 0.6;
            left: 0;
            margin: 0 auto;
            transform: translateY(-50%);
        }
        .toggle-password {
    cursor: pointer;
    position: absolute;
    right: 29%;
    top: 32%;
}
    </style>


</head>

<body>
    <header class="v-header w-100 py-3">
        <div class="mobilenav d-lg-none">
            <div class="collapse" id="navbarToggleExternalContent">
                <div class="bg-dark p-4">
                    <div class="header-nav">
                        <ul class="p-0 m-0">
                            <li><a href="index.html">Home</a> </li>
                            <li><a href="#master-head">About Us</a> </li>
                            <li><a href="#v-course-list">Courses</a> </li>
                            <li><a href="#">memberships</a> </li>
                            <li><a href="#v-educators">Educators</a> </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="container text-center">
            <div class="row gap-3 gap-md-0 align-items-center flex-column flex-md-row just-content-center justify-content-md-between">
                <div class="col flex-grow-0 d-flex justify-content-center align-items-center gap-4">
                    <i class="icon-fi-br-menu-burger d-md-none"></i>
                    <a href="/">
                        <img src="./Content/images/tradiix-logo.png" alt="Vewards" class="header-logo" /></a>
                </div>
                <div class="col flex-grow-1 d-none d-lg-block">
                    <div class="header-nav">
                        <ul class="p-0 m-0">
                            <li><a href="index.html">Home</a> </li>
                            <li><a href="#master-head">about us</a> </li>
                            <li><a href="#v-course-list">courses</a> </li>
                            <li><a href="#">memberships</a> </li>
                            <li><a href="#v-educators">educators</a> </li>
                        </ul>
                    </div>
                </div>

                <div class="col text-md-end flex-grow-0 justify-content-center d-flex gap-3">
                    <nav class="navbar navbar-dark d-lg-none p-0">
                        <div class="container-fluid">
                            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarToggleExternalContent" aria-controls="navbarToggleExternalContent" aria-expanded="false" aria-label="Toggle navigation">
                                <span class="navbar-toggler-icon"></span>
                            </button>
                        </div>
                    </nav>
                    <a href="Register.Aspx" class="btn btn-outline-light rounded-5 text-capitalize px-4">register</a>
                    <button data-bs-toggle="modal" data-bs-target="#loginModal" type="button" class="btn btn-primary btn-outline-light text-nowrap rounded-5 d-none text-capitalize px-4">Change Password</button>
                </div>
            </div>
        </div>
    </header>
    <section id="master-head" class="pb-5">
        <!-- Master Header -->
        <div class="container py-5 pt-xs-0 position-relative">
            <div class="row align-items-center justify-content-center">
                <div class="col-md-6">
                <div class="bg-dark p-3 border-primary rounded">
                    <div class="col-md-12 text-center">
                        <h5 id="ResponseMessage" runat="server" style="display: none;"></h5>
                    </div>
                    <form class="form" id="myForm" runat="server">
                        <div class="mb-3">
                            <label for="" class="text-white">Password</label>
                            <input class="form-control" type="password" name="password" id="password" placeholder="Password" required="required" runat="server" />
                            <span class="toggle-password" onclick="togglePasswordVisibility()">
                                <i id="eyeIcon" class="fa fa-eye-slash"></i>
                            </span>
                        </div>
                        <div class="mb-3">
                            <label for="" class="text-white">Confirm Password</label>
                            <input class="form-control" type="password" name="confirmpassword" id="confirmpassword" placeholder="Confirm Password" required="required" runat="server" />
                            <span class="toggle-password" onclick="togglePasswordVisibility()">
                            <i id="eyeIcon1" class="fa fa-eye-slash"></i>
                        </span>
                        </div>
                       <div class="mb-3">
                            <input type="submit" class="btn w-100 btn-primary btn-outline-light text-nowrap rounded-5 text-capitalize px-4" value="Reset Password" id="btnSubmit" runat="server" onserverclick="btnSubmit_Reset" />
                        </div>
                        <div class="mb-3">
                            <a href="Login.aspx" target="_blank" class="text-white">Back to Login</a>
                        </div>
                    </form>
                </div>
                </div>
            </div>
        </div>
        <span class="arrow-down vp-gradient"><i class="icon-arrow-1"></i></span>
    </section>

     <script>
         function togglePasswordVisibility() {
             var passwordField = document.getElementById('password');
             var eyeIcon = document.getElementById('eyeIcon');

             if (passwordField.type === "password") {
                 passwordField.type = "text";
                 eyeIcon.classList.remove('fa-eye-slash');
                 eyeIcon.classList.add('fa-eye');
             } else {
                 passwordField.type = "password";
                 eyeIcon.classList.remove('fa-eye');
                 eyeIcon.classList.add('fa-eye-slash');
             }
         }
         "use strict"; // Paul Slaymaker, paul25882@gmail.com
         const body = document.getElementsByTagName("body").item(0);
         body.style.background = "#000";
         const TP = 2 * Math.PI;
         const CSIZE = 400;
         const CSO = 52;

         const ctx = (() => {
             let d = document.createElement("div");
             d.classList.add("theGlobe");
             d.style.textAlign = "center";
             body.append(d);
             let c = document.createElement("canvas");
             c.width = c.height = 2 * CSIZE;
             d.append(c);
             return c.getContext("2d");
         })();
         ctx.setTransform(1, 0, 0, 1, CSIZE, CSIZE);

         const ctxo = (() => {
             let c = document.createElement("canvas");
             c.width = c.height = 2 * CSO;
             return c.getContext("2d", { "willReadFrequently": true });
         })();
         ctxo.setTransform(1, 0, 0, 1, CSO, CSO);
         ctxo.lineWidth = 1;

         const ctxo2 = (() => {
             let c = document.createElement("canvas");
             c.width = c.height = 2 * CSO;
             return c.getContext("2d", { "willReadFrequently": true });
         })();
         ctxo2.setTransform(1, 0, 0, 1, CSO, CSO);

         onresize = () => {
             let D = Math.min(window.innerWidth, window.innerHeight) - 40;
             ctx.canvas.style.width = ctx.canvas.style.height = D + "px";
         }

         const getRandomInt = (min, max, low) => {
             if (low) return Math.floor(Math.random() * Math.random() * (max - min)) + min;
             else return Math.floor(Math.random() * (max - min)) + min;
         }

         function Color() {
             const CBASE = 64;
             const CT = 255 - CBASE;
             const RKO = Math.random();
             let GKO = Math.random();
             let BKO = Math.random();
             let RKK = 100 + 200 * Math.random();
             let GKK = 100 + 200 * Math.random();
             let BKK = 100 + 200 * Math.random();
             this.getRGB = () => {
                 this.fr = 0.8 * (1 - Math.cos(RKO + t / this.RKF)) / 2;
                 this.fg = 0.8 * (1 - Math.cos(GKO + t / this.GKF)) / 2;
                 this.fb = 0.8 * (1 - Math.cos(BKO + t / this.BKF)) / 2;
                 this.RK3 = 6 + 36 * (1 - Math.sin(t / RKK)) / 2;
                 this.GK3 = 6 + 36 * (1 - Math.sin(t / GKK)) / 2;
                 this.BK3 = 6 + 36 * (1 - Math.sin(t / BKK)) / 2;
                 let red = Math.round(CBASE + CT * (1 + this.fr * Math.cos(this.RK2 + t / this.RK1) + (1 - this.fr) * Math.cos(this.RK2 + t / this.RK3)) / 2);
                 let grn = Math.round(CBASE + CT * (1 + this.fg * Math.cos(this.GK2 + t / this.GK1) + (1 - this.fg) * Math.cos(this.GK2 + t / this.GK3)) / 2);
                 let blu = Math.round(CBASE + CT * (1 + this.fb * Math.cos(this.BK2 + t / this.BK1) + (1 - this.fb) * Math.cos(this.BK2 + t / this.BK3)) / 2);
                 return "rgb(" + red + "," + grn + "," + blu + ")";
             }
             this.randomize = () => {
                 this.RK1 = 1000 + 1000 * Math.random();
                 this.GK1 = 1000 + 1000 * Math.random();
                 this.BK1 = 1000 + 1000 * Math.random();
                 this.RK2 = TP * Math.random();
                 this.GK2 = TP * Math.random();
                 this.BK2 = TP * Math.random();
                 this.RKF = 100 + 200 * Math.random();
                 this.GKF = 100 + 200 * Math.random();
                 this.BKF = 100 + 200 * Math.random();
             }
             this.randomize();
         }

         const dmx = new DOMMatrix([-1, 0, 0, 1, 0, 0]);
         const dmy = new DOMMatrix([1, 0, 0, -1, 0, 0]);
         const dmr = new DOMMatrix([0, 1, -1, 0, 0, 0]);

         var stopped = true;
         var start = () => {
             if (stopped) {
                 stopped = false;
                 requestAnimationFrame(animate);
             } else {
                 stopped = true;
             }
         }
         //ctx.canvas.addEventListener("click", start, false);

         var t = 0;
         var animate = (ts) => {
             if (stopped) return;
             t++;
             if (!(t % 5)) draw();
             requestAnimationFrame(animate);
         }

         const S6 = Math.sin(TP / 6);
         const tta = [0, 0.5, S6, 1, S6, 0.5, 0, -0.5, -S6, -1, -S6, -0.5];
         const ttb = [0, 1, 2, 2, 2, 1, 0, -1, -2, -2, -2, -1];

         var drawTiles = () => {
             for (let i = 0; i < 12; i += 2) {
                 let a1 = (i + 4) % 12, a2 = (i + 1) % 12, a3 = (i + 5) % 12, a4 = (i + 2) % 12;
                 ctx.setTransform(tta[a1], tta[a2], tta[a3], tta[a4], CSIZE, CSIZE);
                 ctx.drawImage(ctxo.canvas, 0, 0);
                 a1 = (i + 4) % 12, a2 = (i + 1) % 12, a3 = (i + 3) % 12, a4 = (i + 0) % 12;
                 ctx.setTransform(tta[a1], tta[a2], tta[a3], tta[a4], CSIZE, CSIZE);
                 ctx.drawImage(ctxo.canvas, 0, 0);
             }
             ctxo2.putImageData(ctxo.getImageData(0, 0, 2 * CSO, 2 * CSO), 0, 0);
             ctxo2.fillStyle = "#00000010";
             ctxo2.fillRect(-CSO, -CSO, 2 * CSO, 2 * CSO);
             for (let i = 0; i < 12; i += 2) {
                 let a1 = (i + 2) % 12, a2 = (i + 5) % 12, a3 = (i + 6) % 12, a4 = (i + 9) % 12;
                 let b1 = (i + 1) % 12, b2 = (i + 0) % 12, b3 = (i + 3) % 12, b4 = (i + 4) % 12;
                 ctx.setTransform(tta[a1], tta[a2], tta[a3], tta[a4],
                     CSIZE + (ttb[b1] + ttb[b2] * S6) * CSO, CSIZE + (ttb[b3] + ttb[b4] * S6) * CSO);
                 ctx.drawImage(ctxo2.canvas, 0, 0);
                 a1 = (i + 9) % 12, a3 = (i + 5) % 12, a4 = (i + 8) % 12;
                 b1 = (i + 3) % 12, b2 = (i + 4) % 12, b3 = (i + 7) % 12, b4 = (i + 6) % 12;
                 ctx.setTransform(tta[a1], tta[i], tta[a3], tta[a4],
                     CSIZE + (ttb[b1] + ttb[b2] * S6) * CSO, CSIZE + (ttb[b3] + ttb[b4] * S6) * CSO);
                 ctx.drawImage(ctxo2.canvas, 0, 0);
             }
             ctxo2.fillStyle = "#00000020";
             ctxo2.fillRect(-CSO, -CSO, 2 * CSO, 2 * CSO);
             for (let i = 0; i < 12; i += 2) {
                 let a1 = (i + 3) % 12, a4 = (i + 9) % 12;
                 let b1 = (i + 8) % 12, b2 = (i + 7) % 12, b3 = (i + 4) % 12, b4 = (i + 5) % 12;
                 ctx.setTransform(tta[a1], tta[i], tta[i], tta[a4],
                     CSIZE + (1.5 * ttb[b1] + 2 * ttb[b2] * S6) * CSO, CSIZE + (1.5 * ttb[b3] + 2 * ttb[b4] * S6) * CSO);
                 ctx.drawImage(ctxo2.canvas, 0, 0);
                 a1 = (i + 6) % 12; let a2 = (i + 3) % 12; let a3 = (i + 3) % 12;
                 b1 = (i + 5) % 12, b2 = (i + 4) % 12, b3 = (i + 1) % 12, b4 = (i + 2) % 12;
                 ctx.setTransform(tta[a1], tta[a2], tta[a3], tta[i],
                     CSIZE + (ttb[b1] + ttb[b2] * S6) * CSO, CSIZE + (ttb[b3] + ttb[b4] * S6) * CSO);
                 ctx.drawImage(ctxo2.canvas, 0, 0);
             }
             ctxo2.fillStyle = "#00000030";
             ctxo2.fillRect(-CSO, -CSO, 2 * CSO, 2 * CSO);
             for (let i = 0; i < 12; i += 2) {
                 let a1 = (i + 2) % 12, a2 = (i + 5) % 12, a4 = (i + 3) % 12;
                 let b1 = (i + 4) % 12, b2 = (i + 5) % 12, b3 = (i + 8) % 12, b4 = (i + 7) % 12;
                 ctx.setTransform(tta[a1], tta[a2], tta[i], tta[a4],
                     CSIZE + (1.5 * ttb[b1] + 2 * ttb[b2] * S6) * CSO, CSIZE + (1.5 * ttb[b3] + 2 * ttb[b4] * S6) * CSO);
                 ctx.drawImage(ctxo2.canvas, 0, 0);
                 a1 = (i + 5) % 12, a2 = (i + 2) % 12; let a3 = (i + 7) % 12; a4 = (i + 4) % 12;
                 b1 = (i + 10) % 12, b2 = (i + 9) % 12, b3 = (i + 6) % 12, b4 = (i + 7) % 12;
                 ctx.setTransform(tta[a1], tta[a2], tta[a3], tta[a4],
                     CSIZE + (1.5 * ttb[b1] + 2 * ttb[b2] * S6) * CSO, CSIZE + (1.5 * ttb[b3] + 2 * ttb[b4] * S6) * CSO);
                 ctx.drawImage(ctxo2.canvas, 0, 0);
             }
             let id = ctxo.getImageData(0, 0, 2 * CSO, 2 * CSO);
             ctxo2.fillStyle = "#00000040";
             ctxo2.fillRect(-CSO, -CSO, 2 * CSO, 2 * CSO);
             for (let i = 0; i < 12; i += 2) {
                 let a1 = (i + 2) % 12, a2 = (i + 11) % 12, a3 = (i + 7) % 12, a4 = (i + 4) % 12;
                 let b1 = (i + 4) % 12, b2 = (i + 5) % 12, b3 = (i + 2) % 12, b4 = (i + 1) % 12;
                 ctx.setTransform(tta[a1], tta[a2], tta[a3], tta[a4],
                     CSIZE + (1.5 * ttb[b1] + 2 * ttb[b2] * S6) * CSO, CSIZE + (1.5 * ttb[b3] + 2 * ttb[b4] * S6) * CSO);
                 ctx.drawImage(ctxo2.canvas, 0, 0);
                 a1 = (i + 2) % 12, a2 = (i + 5) % 12, a3 = (i + 7) % 12, a4 = (i + 10) % 12;
                 b1 = (i + 4) % 12, b2 = (i + 5) % 12, b3 = (i + 8) % 12, b4 = (i + 7) % 12;
                 ctx.setTransform(tta[a1], tta[a2], tta[a3], tta[a4],
                     CSIZE + (1.5 * ttb[b1] + 2 * ttb[b2] * S6) * CSO, CSIZE + (1.5 * ttb[b3] + 2 * ttb[b4] * S6) * CSO);
                 ctx.drawImage(ctxo2.canvas, 0, 0);
             }
         }

         var col = new Color();
         var path = new Path2D();
         path.moveTo(CSO, CSO);
         path.lineTo(CSO, 0);
         path.addPath(path, dmx);
         path.addPath(path, dmy);
         path.addPath(path, dmr);

         var draw = () => {
             var id = ctxo.getImageData(CSO + 1, CSO + 1, CSO - 1, CSO - 1);
             ctxo.putImageData(id, CSO, CSO);
             id = ctxo.getImageData(0, CSO + 1, CSO - 1, CSO - 1);
             ctxo.putImageData(id, 1, CSO);
             id = ctxo.getImageData(CSO + 1, 0, CSO - 1, CSO - 1);
             ctxo.putImageData(id, CSO, 1);
             id = ctxo.getImageData(0, 0, CSO - 1, CSO - 1);
             ctxo.putImageData(id, 1, 1);
             ctxo.strokeStyle = col.getRGB();
             ctxo.setLineDash([30 + 50 * (1 + Math.cos(t / 490)) / 2, 10 + 20 * (1 + Math.sin(t / 400)) / 2]);
             ctxo.lineDashOffset = t / 5;
             ctxo.stroke(path);
             drawTiles();
         }

         onresize();

         drawTiles();

         start();
     </script>
</body>
</html>
