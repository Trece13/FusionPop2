<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="PickingUpdate.aspx.cs" Inherits="whusap.WebPages.WorkOrders.PickingUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="styles/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="styles/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="styles/sweetalert2.css"/>
    <link
        rel="stylesheet"
        href="styles/animate.min.css" />
    <script src="styles/sweetalert2.min.js"></script>

    <style>
        .loader:before,
        .loader:after,
        .loader {
            border-radius: 50%;
            width: 2.5em;
            height: 2.5em;
            -webkit-animation-fill-mode: both;
            animation-fill-mode: both;
            -webkit-animation: load7 1.8s infinite ease-in-out;
            animation: load7 1.8s infinite ease-in-out;
        }
        #StatusChangeBtn{
            display:none;
        }
        .loader {
            margin: 8em auto;
            font-size: 10px;
            position: relative;
            text-indent: -9999em;
            -webkit-animation-delay: 0.16s;
            animation-delay: 0.16s;
        }

            .loader:before {
                left: -3.5em;
            }

            .loader:after {
                left: 3.5em;
                -webkit-animation-delay: 0.32s;
                animation-delay: 0.32s;
            }

            .loader:before,
            .loader:after {
                content: '';
                position: absolute;
                top: 0;
            }

        @-webkit-keyframes load7 {
            0%, 80%, 100% {
                box-shadow: 0 2.5em 0 -1.3em #ffffff;
            }

            40% {
                box-shadow: 0 2.5em 0 0 #FFF;
            }
        }

        @keyframes load7 {
            0%, 80%, 100% {
                box-shadow: 0 2.5em 0 -1.3em #ffffff;
            }

            40% {
                box-shadow: 0 2.5em 0 0 #FFF;
            }
    </style>
    <style type="text/css">
        #MyEtiqueta label {
            font-size: 15px;
        }

        #LblDate {
            font-size: 14px !important;
        }

        #LblReprintInd,
        #LblReprint {
            display: none;
        }

        .isValid {
            border-bottom: solid;
            border-color: green;
        }

        .isNotValid {
            border-bottom: solid;
            border-color: red;
        }

        .fa-check {
            color: green;
        }

        .fa-times {
            color: red;
        }

        #checkItem,
        #checkLot,
        #checkWarehouse,
        #checkLoca,
        #checkPaid {
            display: none;
        }

        #exItem,
        #exLot,
        #exWarehouse,
        #exLoca,
        #exPaid {
            display: none;
        }

        #loadItem,
        #loadLot,
        #loadWarehouse,
        #loadLoca,
        #loadPaid {
            display: none;
        }

        tr {
            text-align: center;
        }

        th {
            text-align: center;
        }

        #myLabel {
            width: 6in;
            height: 4in;
            padding: 5px;
            border: 1px solid black;
            border-radius: 12px;
        }

        .alingRight {
            text-align: right;
        }

        .alingLeft {
            text-align: left;
        }

        #printButton {
            width: 6in;
        }

        #codePaid {
            display: block;
            margin: auto;
            height: 75px;
            width: 500px;
        }

        #codeMaterial {
            display: block;
            margin: auto;
            height: 50px;
            width: 220px;
        }

        #codeItem {
            display: block;
            margin: auto;
            height: 75px;
            width: 250px;
        }

        #itemDesc {
            vertical-align: middle;
            font-size: 21px;
        }

        .divDesc {
            text-align: center;
        }

        #lblDesc {
        }

        #lblMadein {
        }

        .borderTop {
            border-top: solid 1px gray;
        }

        #printContainer {
            margin-bottom: 100px;
        }

        #editTable {
            display: none;
        }

        #lblError {
            color: red;
            font-size: 30px;
        }

        .load {
            width: 10px;
            height: 10px;
            align-content: center;
            animation-name: spin;
            animation-duration: 5000ms;
            animation-iteration-count: infinite;
            animation-timing-function: linear;
        }

        #saveSection {
            display: none;
        }

        .notBorderBottom {
            border-bottom: none;
        }

        #divBarcode {
            height: 140px;
            padding: inherit;
        }

        @keyframes spin {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
        }

        #table {
            font-size: 10px;
        }

        .table td,
        .table th {
            padding: .1rem;
            border-top: 1px solid #dee2e6;
            font-size: 16px;
            text-align: left;
            vertical-align: middle;
            padding-left: 1em;
        }
    </style>
    <style>
        #ddWare {
            height: 40px;
            border-radius: 4px;
        }

        #btnStarPicking, #divPicketPending, #formPicking, #reasonLine, #MyEtiqueta, #lblReason, #ddReason, #txQtyc, #MainDiv {
            display: none;
        }

        td, th {
            font-size: 10px;
        }

        #lblConsigment {
            margin-left: 20px;
        }

        #txPaid, #txLoca, #txQtyc {
            height: 30px;
        }
    </style>
    <script>
        function printDiv(divID) {

            var monthNames = [
                "1", "2", "3",
                "4", "5", "6", "7",
                "8", "9", "10",
                "11", "12"
            ];

            //PRINT LOCAL HOUR
            var d = new Date();
            var dateNow = (monthNames[d.getMonth()] +
                "/" +
                d.getDate() +
                "/" +
                d.getFullYear() +
                " " +
                d.getHours() +
                ":" +
                d.getMinutes() +
                ":" +
                d.getSeconds());
            var LbdDate = $("#LblDate");
            LbdDate.html(dateNow);

            var LbdDate2 = $("#LblDate2");
            LbdDate2.html(dateNow);

            var mywindow = window.open('', 'PRINT');

            mywindow.document.write('<html><head><title>' + document.title + '</title>');
            mywindow.document.write('</head><body> <style>@page {size: 6in,3in;margin: 0; size: landscape;} #MyEtiquetaDrop{width:6in; height:3in;}</style>');
            mywindow.document.write(document.getElementById(divID).innerHTML);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/

            setTimeout(function () { mywindow.print() }, 3000);

            return true;
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container-fluid col-12 animate__animated animate__fadeInLeft" style="margin-bottom: 200px">
        <div class="row" id="formWarehouse">
            <div class="col-6">
                <form>
                    <div class="row">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-6">
                                    <label for="ddWare">Warehouse</label>
                                </div>
                                <div class="col-6">
                                    <%--                                 <div class="col-11 float-right">
                                        <input type="checkbox" class="form-check-input" id="chkConsigment">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <label class="form-check-label float-right" for="chkConsigment">Consigment</label>
                                    </div>--%>
                                    <div class="form-check float-right">
                                        <input type="checkbox" class="form-check-input" id="chkConsigment">
                                        <label class="form-check-label" for="chkConsigment" id="lblConsigment">Consigment</label>
                                    </div>
                                </div>
                            </div>

                            <select id="ddWare" class="form-control">
                                <option value="0" selected>Select Warehouse</option>
                            </select>
                        </div>
                    </div>
                    <br>
                    <div id="divStartPicking" class="row">
                        <div class="col-6">
                            <button class="btn btn-primary col-12 btn-sm" id="btnStarPicking" type="button">Start Picking&nbsp;<span><i class="fas fa-circle-notch fa-spin" style="color: silver; display:none" id='StartLoader'></i></span></button>
                            <label id="lblErrorInit" style="color:red; font-size:30px"></label>
                        </div>
                    </div>
                </form>
            </div>
        </div>
        <hr />
        <br />
        <div class="fa-5x container text-center" style="display: none" id="GetPicksLoader">
            <i class="fas fa-circle-notch fa-spin" style="color: silver;"></i>
        </div>
        <div id="MainDiv">
        <div id="" class="row col-12">
            <table id="" class="table animate__animated animate__fadeInLeft" style="width: 100%">
                <thead>
                    <tr>
                        <th scope="col">Pick Id</th>
                        <th scope="col">Work Order</th>
                        <th scope="col">Machine</th>
                        <th scope="col">Item</th>
                        <th scope="col">Description</th>
                        <th scope="col">Warehouse</th>
                        <th scope="col">Quantity</th>
                        <th scope="col">Unit</th>
                    </tr>
                </thead>
                <tbody id="bdPicket182">
                </tbody>
            </table>
        </div>
        <br />
        <br />
        <br />
        <div id="divPicketPending" class="col-12">
            <table id="tblPicketPending" class="table animate__animated animate__fadeInLeft" style="width: 100%">
                <thead>
                    <tr>
                        <th scope="col">Location</th>
                        <th scope="col">Quantity</th>
                    </tr>
                </thead>
                <tbody id="bdPicketPending">
                </tbody>
            </table>
        </div>
        <div class="row">
            <div class="col-5">
                <form id="">
                    <div class="row">
                        <div class="col-3">
                            <label>Pallet ID</label>
                        </div>
                        <div class="col-6">
                            <input type="text" class="col-12 form-control" id="txPaid" placeholder="Pallet ID" required>
                        </div>
                        <div class="col-3">
                            <button class="btn btn-danger col-12 btn-sm" id="StatusChangeBtn" type="button" onclick="StatusChange()"><span>Do you want to block this pallet?</span></button>
                        </div>
                    </div>
                    <br>
                    <div class="row">
                        <div class="col-3">
                            <label>Item</label>
                        </div>
                        <div class="col-9" id="LblItem">
                        </div>
                    </div>
                    <br>
                    <div class="row">
                        <div class="col-3">
                            <label>Warehouse</label>
                        </div>
                        <div class="col-2" id="lblWare">
                        </div>

                    </div>
                    <br>
                    <div class="row">
                        <div class="col-3">
                            <label>Location</label>
                        </div>
                        <div class="col-4" id="lblLoca">
                        </div>
                        <div class="col-5 p-0">
                            <input type="text" class="col-12 form-control" id="txLoca" placeholder="Location" required style="display:none">
                        </div>
                    </div>
                    <br>
                    <div class="row">
                        <div class="col-3">
                            <label>Quantity</label>
                        </div>
                        <div class="col-2" id="lblQtyc">
                        </div>
                        <div class="col-2" id="lblUnit">
                        </div>
                        <div class="col-5  p-0">
                            <input type="text" class="col-12 form-control" id="txQtyc" placeholder="Quantity" required>
                        </div>
                    </div>
                    <br>
                    <hr id="reasonLine" />
                    <div id="divReason" class="row">
                        <div class="col-2">
                            <label id="lblReason">Reason</label>
                        </div>
                        <div class="col-6">
                            <select class="form-control" id="ddReason" tabindex="1">
                                <option value="0">Select Reason</option>
                                <option value="1">Wrong Lot</option>
                                <option value="2">Aisle Blocked</option>
                                <option value="3">Wrong Location</option>
                            </select>
                        </div>
                        <div class="col-4">
                            <input id="bntChange" type="button" class="btn btn-primary col-12 btn-sm" style="display: none" onclick="IngresarCausales()"
                                value="Change" />
                        </div>
                    </div>

                    <br>
                    <div class="row">
                        <button class="btn btn-primary col-12 btn-sm mb-1" id="btnConfirm" onclick="Confirm(); return false;" style="display:none" type="button"><span>Confirm&nbsp;<i class='fas fa-circle-notch fa-spin' id="ConfirmLoader" style="display:none"></i></span></button>
                        <button class="btn btn-danger col-12 btn-sm" id="" type="button" onclick="Stop()";><span>Stop Picking&nbsp;<i class='fas fa-circle-notch fa-spin' style='color: silver; display: none' id="SkipLoader"></i></span></button>
                    </div>
                    <div class="row">
                        <label id="lblError" style="color: red; font-size: 30px;"></label>
                    </div>
                </form>
            </div>
            <div  class="col-2">
                </div>
            <div id="divTables" class="col-3">
            <table id="" class="table animate__animated animate__fadeInLeft" style="width: 100%">
                <thead>
                    <tr>
                        <th scope="col">Pallet ID</th>
                        <th scope="col">Warehouse</th>
                        <th scope="col">Location</th>
                        <th scope="col">Quantity</th>
                    </tr>
                </thead>
                <tbody id="bdPallets">
                </tbody>
            </table>
            </div>
        </div>
        </div>
        <div class="row">
            <div class="container">
                <iframe id="myLabelFrame" scrolling="no" title="" class="col-12" style="height: 50px; overflow: hidden; margin-bottom: 100px;" frameborder="0" src=""></iframe>
            </div>
            <div id="MyEtiqueta">
                <div id="myLabel">
                    <div class="row">
                        <div class="col-6 alingLeft">
                            <label>
                                <strong>
                                    <label id="lblItemDesc" runat="server">MATERIAL DESCRIPTION</label></strong>
                            </label>
                        </div>
                        <div class="col-6 alingRight">
                            <img id="CBItem" src="~/images/logophoenix_login.jpg" runat="server" />
                        </div>
                    </div>
                    <br />
                    <div class="col-12 borderTop" id="divBarcode">
                        <img id="CBPalletNO" src="~/images/logophoenix_login.jpg" runat="server" />
                    </div>
                    <div>
                        <table class="table mw-100">
                            <tbody>
                                <tr>
                                    <td><strong>WO Lot</strong>&nbsp;&nbsp;<label id="LblLotId" runat="server"></label></td>
                                    <td><strong>Quantity</strong>&nbsp;&nbsp;<label id="LblQuantity" runat="server"></label></td>
                                </tr>
                                <tr>
                                    <td colspan="2"><strong>Date</strong>&nbsp;&nbsp;<label id="LblDate" runat="server"></label></td>
                                    <td><strong>Pallet #</strong>&nbsp;&nbsp;<label id="lblPallet" runat="server"></label></td>
                                </tr>
                                <tr>
                                    <td><strong>Machine</strong>&nbsp;&nbsp;<label id="lblMachine" runat="server"></label></td>
                                    <td><strong>Made in Dublin VA</strong></td>
                                    <td><strong>Operator</strong>&nbsp;&nbsp;<label id="lblOperator" runat="server"></label></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div id="myLabel">
                    <div class="row">
                        <div class="col-6 alingLeft">
                            <label>
                                <strong>
                                    <label id="lblItemDesc2" runat="server">MATERIAL DESCRIPTION</label></strong>
                            </label>
                        </div>
                        <div class="col-6 alingRight">
                            <img id="CBItem2" src="~/images/logophoenix_login.jpg" runat="server" />
                        </div>
                    </div>
                    <br />
                    <div class="col-12 borderTop" id="divBarcode">
                        <img id="CBPalletNO2" src="~/images/logophoenix_login.jpg" runat="server" />
                    </div>
                    <div>
                        <table class="table mw-100">
                            <tbody>
                                <tr>
                                    <td><strong>WO Lot</strong>&nbsp;&nbsp;<label id="LblLotId2" runat="server"></label></td>
                                    <td><strong>Quantity</strong>&nbsp;&nbsp;<label id="LblQuantity2" runat="server"></label></td>
                                </tr>
                                <tr>
                                    <td colspan="2"><strong>Date</strong>&nbsp;&nbsp;<label id="LblDate2" runat="server"></label></td>
                                    <td><strong>Pallet #</strong>&nbsp;&nbsp;<label id="Label5" runat="server"></label></td>
                                </tr>
                                <tr>
                                    <td><strong>Machine</strong>&nbsp;&nbsp;<label id="Label6" runat="server"></label></td>
                                    <td><strong>Made in Dublin VA</strong></td>
                                    <td><strong>Operator</strong>&nbsp;&nbsp;<label id="Label7" runat="server"></label></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div id="MyEtiquetaDropPrint" style="display:none;"">
            <div id="MyEtiquetaDrop" style="width:6in; height:3.5in;">
                <table style="margin: auto;margin-top:15px;">
                    <tr>
                        <td>
                            <label style="font-size: 30px">
                                Pick ID</label>
                        </td>
                        <td colspan="4">
                            <img src="~/images/logophoenix_login.jpg" runat="server" id="bcPick" alt="" hspace="60"
                                vspace="5" style="width: 3in; height: 1in; margin: 0px !important" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-size: 30px">
                                Machine</label>
                        </td>
                        <td style="text-align: center;">
                            <label style="font-size: 30px" id="lbMcno">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-size: 30px">
                                Pallet(s)
                            </label>
                        </td>
                        <td style="text-align: center;">
                            <label style="font-size: 20px" id="lbPaid"></label>
                            <label style="font-size: 20px" id="lbSep">/</label>
                            <label style="font-size: 20px" id="lbQtyp"></label>
                        </td>
                    </tr>
                </table>
            </div>
            </div>
        </div>
    </div>
    <script>
        var ajaxShowCurrentOptionsWarehouse = null;
        var ajaxShowCurrentOptionsItem = null;
        var qty = "";
        var StarProcessing = false;
        var GetPicksProcessing = false;
        var TakeProcessing = false;
        var ChangeProcessing = false;
        var ConfirmProcessing = false;
        var SkipProcessing = false;
        var DropProcessing = false;
        var EndPickingProcessing = false;

        var indexTakeLoader;
        var indexDropLoader;

        var timer;
        var skip = false;

        var existPalletsPick = false;
        function stoper() {
            clearTimeout(timer);
        }
        //window.onunload = function (e) {
        //    EventoAjax("Actualizar307", "{}", null);
        //};

        var cnpk = "";
        var sloc = "";
        var paidOk = false;
        var locaOk = false;
        var qtytOk = false;
        var DisttinctLocaValid = false;

        function StatusChange() {
            Swal.fire({
                title: 'Are you sure?',
                text: "",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes'
            }).then(function (result) {
                if (result.value) {
                    Method = "BlockPick182"
                    Data = "{'PAID':'" + $('#txPaid').val() + "'}";
                    EventoAjax(Method, Data, SucceessStatusChange);
                    $("#StatusChange").hide(100);
                    Swal.fire(
                      'Blocked!',
                      'Pallet Id has been Blocked.',
                      'success'
                    )
                }
            })
        }

        var EventoAjax = function (Method, Data, MethodSuccess) {
            $.ajax({
                type: "POST",
                url: "PickingUpdate.aspx/" + Method.trim(),
                data: Data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: MethodSuccess
            })
        };

        var StartComponents = function () {
            var formWarehouse = document.getElementById("formWarehouse");
            var chkConsigment = document.getElementById("chkConsigment");
            var ddWare = document.getElementById("ddWare");
            var pickingResult = document.getElementById("pickingResult");
            var formPicking = document.getElementById("formPicking");
            //var lblPick = document.getElementById("lblPick");
            var lblPaid = document.getElementById("lblPaid");
            var txPaid = document.getElementById("txPaid");
            var LblItem = document.getElementById("LblItem");
            var lblLoca = document.getElementById("lblLoca");
            var txLoca = document.getElementById("txLoca");
            var lblWare = document.getElementById("lblWare");
            var lblQtyc = document.getElementById("lblQtyc");
            var lblUnit = document.getElementById("lblUnit");
            var txQtyc = document.getElementById("txQtyc");
            var btnStarPicking = document.getElementById("btnStarPicking");
            var btnConfirm = document.getElementById("btnConfirm");
            //var btnSkipPicking = document.getElementById("btnSkipPicking");
            var bdPicket182 = document.getElementById("bdPicket182");
            var bdPallets = document.getElementById("bdPallets");
            var ddReason = document.getElementById("ddReason");
            var bntChange = document.getElementById("bntChange");

            btnStarPicking.addEventListener("click", loadPage);
            ddWare.addEventListener("change", loadPicksPending);
            ddReason.addEventListener("change", changeReason);
            txPaid.addEventListener("input", verifyPallet);
            //txLoca.addEventListener("input", VerifyLocation);
            txQtyc.addEventListener("keyup", VerifyQuantity);
            chkConsigment.addEventListener('change', GetWarehouse);
            btnConfirm.addEventListener('click', Confirm)
        }

        var SucceessStatusChange = function (r) {
            ClearFormPicking();
        }
        var changeReason = function () {
            console.log(ddReason.value);
            if (ddReason.value != "0") {
                $(bntChange).show(500);
            }
            else {
                $(bntChange).hide(500);
            }
        }

        var GetWarehouse = function () {
            $('#divPicketPending').hide(500);
            for (let i = ddWare.options.length; i > 0; i--) {
                ddWare.remove(i);
            }
            chkConsigment.checked == true ? EventoAjax("GetWarehouse", "{'consigment':'true'}", SucceessGetWarehouse) : EventoAjax("GetWarehouse", "{'consigment':'false'}", SucceessGetWarehouse);
        }

        var SucceessGetWarehouse = function (r) {
            var MylistWarehouse = JSON.parse(r.d);
            MylistWarehouse.forEach(function (e) {
                var option = document.createElement("option");
                option.text = e
                option.value = e
                ddWare.add(option);
            });
        }

        function verifyPallet() {
            stoper();
            lstPallets = JSON.parse(window.localStorage.getItem('MyPalletList'));
            timer = setTimeout(function () {
                $("#lblError").html("");
                console.log("Pallet NO Igual");
                Method = "VerificarExistenciaPalletID"
                Data = "{'PAID_NEW':'" + $('#txPaid').val() + "'}";
                EventoAjax(Method, Data, PalletIDSuccess)
                lstPAllets = JSON.parse(window.localStorage.getItem('MyPalletList'));
            }, 1500);

        }

        var PalletIDSuccess = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.error == false) {
                $("#StatusChangeBtn").show(100);
                paidOk = true;
                console.log();
                validElement(txPaid);
                locaInvalid();
                hideShowNeutroInputText(txQtyc, true);
                //hideShowNeutroInputText(txLoca, true);
                //hideShowNeutroSelect(ddReason, true);
                cnpk = MyObj.CNPK;
                if (cnpk == "1") {
                    hideShowNeutroInputText(txQtyc, false);
                    hideShowNeutroInputText(btnConfirm, true);
                }
                else {
                    hideShowNeutroInputText(txQtyc, true);
                    hideShowNeutroInputText(btnConfirm, false);

                }
                //lblPick.innerHTML = MyObj.PICK;
                //lblPaid.innerHTML = MyObj.PALLETID;
                LblItem.innerHTML = MyObj.ITEM;
                lblLoca.innerHTML = MyObj.LOCA;
                lblWare.innerHTML = MyObj.WRH;
                lblQtyc.innerHTML = MyObj.QTYT;
                lblUnit.innerHTML = MyObj.UN;
                //(MyObj.CNPK == "1") ? $('#txQtyc').hide(500) : $('#txQtyc').show(500);
                //ShowCurrentOptionsItem();
                //ShowCurrentOptionsWarehouse();
            }
            else if (MyObj.error == true) {
                paidInvalid();
                $("#lblError").html(MyObj.errorMsg);
                $("#StatusChangeBtn").hide(100);
            }
        }

        //var VerificarPalletIDSuccess = function (r) {
        //    var MyObj = JSON.parse(r.d);
        //    if (MyObj.error == false) {
        //        console.log("ESTADOS CAMBIADOS");
        //        ClearFormPicking();
        //        cnpk = MyObj.CNPK;
        //        //lblPick.innerHTML = MyObj.PICK;
        //        //lblPaid.innerHTML = MyObj.PALLETID;
        //        LblItem.innerHTML = MyObj.ITEM;
        //        lblLoca.innerHTML = MyObj.LOCA;
        //        lblWare.innerHTML = MyObj.WRH;
        //        lblQtyc.innerHTML = MyObj.QTYT;
        //        lblUnit.innerHTML = MyObj.UN;
        //        //(MyObj.CNPK == "1") ? $('#txQtyc').hide(500) : $('#txQtyc').show(500);
        //        ShowCurrentOptionsItem();
        //        ShowCurrentOptionsWarehouse();
        //    }
        //    else if (MyObj.error == true) {
        //        console.log("Pallet not allowed");

        //    }
        //}

        function ShowCurrentOptionsItem() {
            //divTableItem.innerHTML = '';
            var bodyRows = ""
            ajaxShowCurrentOptionsItem = $.ajax({
                type: "POST",
                url: "PickingUpdate.aspx/ShowCurrentOptionsItem",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var MyObjLst = JSON.parse(r.d);
                    if (bdPallets.childElementCount > 0) {
                        for (let i = bdPallets.childElementCount - 1; i >= 0 ; i--) {
                            bdPallets.children[i].remove()
                        }
                    }
                    var bodyRows = "";
                    if (MyObjLst.length > 0) {
                        var validos = true;
                        MyObjLst.forEach(function (item, i) {
                            bodyRows += "<tr  row = '" + i + "' id='rowNum" + i + "' class = 'animate__animated animate__fadeInLeft'><td>" + item.PALLETID + "</td><td>" + item.WRH + "</td><td>" + item.LOCA + "</td><td>" + item.QTY + "</td></tr>";
                        });
                    }
                    else {
                        $('#bdPallets').hide(100);
                    }
                    $("#bdPallets").append(bodyRows);

                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        var loadPage = function () {
            skip = false;
            StartProcessing = false;
            $('#StartLoader').show(100);
            $("#StatusChangeBtn").hide(100);
            EventoAjax("loadPage", "{'CWAR':'" + ddWare.value + "'}", LoadPageSuccess);
        }

        var LoadPageSuccess = function (r) {
            $("#lblMcnoPick").hide(500);
            TakeProcessing = false;
            SkipProcessing = false;
            StartProcessing = false;
            $('#TakeLoader' + indexTakeLoader).hide(500);
            $('#SkipLoader').hide(500);
            $('#StartLoader').hide(500);
            ClearFormPicking();
            var divTables = document.getElementById('divTables');
            ddWare.value = 0;
            var MyObj = r.d;
            if (MyObj.error == false) {
                $("#lblErrorInit").hide(100);
                $("#lblErrorInit").html("");
                $('#divPicketPending').hide(100);
                //lblPick.innerHTML = MyObj.PICK;
                //if (MyObj.PICK != null) {
                //    cnpk = MyObj.CNPK;
                //    sloc = MyObj.SLOC;
                //    sessionStorage.setItem('PICK', MyObj.PICK);
                //    sessionStorage.setItem('CWAR', MyObj.WRH);
                //    lblPaid.innerHTML = MyObj.PALLETID;
                //    LblItem.innerHTML = MyObj.ITEM;
                //    lblLoca.innerHTML = MyObj.LOCA;
                //    lblWare.innerHTML = MyObj.WRH;
                //    lblQtyc.innerHTML = MyObj.QTYT;
                //    lblUnit.innerHTML = MyObj.UN;
                //    if (MyObj.MCNOPICK == true) {
                //        $("#lblMcnoPick").show(500);
                //    }
                //    else {
                //        $("#lblMcnoPick").hide(500);
                //    }
                //    //(MyObj.CNPK == "1") ? $('#txQtyc').hide(500) : $('#txQtyc').show(500);
                //    divTables.classList.add("col-7");
                //    divTables.classList.remove("col-12");
                //    $("#formPicking").show(500);
                //} else {
                //    $("#formPicking").hide(500);
                //    divTables.classList.remove("col-7");
                //    divTables.classList.add("col-12");
                //}
                //if (skip == false) {
                //    ShowCurrentOptionsWarehouse();
                //    ShowCurrentOptionsItem();
                //}
                //else {
                //    ShowCurrentOptionsItem();
                //}
            }
            else {
                $("#lblErrorInit").html(MyObj.errorMsg);
                $("#lblErrorInit").show(100);
                $("#formPicking").hide(100);
            }
            TakeProcessing = false;
        }

        var loadPicksPending = function () {
            $("#MainDiv").hide(100);
            if (bdPicket182.childElementCount > 0) {
                for (let i = bdPicket182.childElementCount - 1; i >= 0 ; i--) {
                    bdPicket182.children[i].remove()
                }
            }
            if (bdPallets.childElementCount > 0) {
                for (let i = bdPallets.childElementCount - 1; i >= 0 ; i--) {
                    bdPallets.children[i].remove()
                }
            }
            $("#lblErrorInit").hide(100);
            $("#lblMcnoPick").hide(100);
            if (ddWare.value == "0") {

                ajaxShowCurrentOptionsItem != null ? ajaxShowCurrentOptionsItem.abort() : ajaxShowCurrentOptionsItem;
                ajaxShowCurrentOptionsItem = null;
                ajaxShowCurrentOptionsWarehouse != null ? ajaxShowCurrentOptionsWarehouse.abort() : ajaxShowCurrentOptionsWarehouse;
                ajaxShowCurrentOptionsWarehouse = null;
                $('#divPicketPending').hide(100);
                $('#divPicketPending').hide(100);
                $("#formPicking").hide(100);
                //divTableWarehouse.innerHTML = '';
                //divTableItem.innerHTML = '';
            }
            else {
                /*$('#GetPicksLoader').show(10);*/
                ajaxShowCurrentOptionsItem != null ? ajaxShowCurrentOptionsItem.abort() : ajaxShowCurrentOptionsItem;
                ajaxShowCurrentOptionsItem = null;
                ajaxShowCurrentOptionsWarehouse != null ? ajaxShowCurrentOptionsWarehouse.abort() : ajaxShowCurrentOptionsWarehouse;
                ajaxShowCurrentOptionsWarehouse = null;
                $('#divPicket182').hide(100);
                $("#formPicking").hide(100);
                //divTableWarehouse.innerHTML = '';
                //divTableItem.innerHTML = '';
                EventoAjax("loadPicks182", "{'CWAR':'" + ddWare.value + "'}", loadPicks182Success);
            }
        }

        var loadPicks182Success = function (r) {

            var item = JSON.parse(r.d);

            var bodyRows = "";
            if (item.error == false) {
                var validos = true;
                bodyRows += "<tr  row = '" + 0 + "' id='rowNum" + 0 + "' class = 'animate__animated animate__fadeInLeft'><td>" + item.PICK + "</td><td>" + item.ORNO + "</td><td>" + item.MCNO + "</td><td>" + item.ITEM + "</td><td>" + item.DESCRIPTION + "</td><td>" + item.WRH + "</td><td id = 'qtyTbl'>" + item.QTY + "</td><td>" + item.UN + "</td>";
                if (validos) {
                    $('#bdPicket182').show(500);
                    $("#MainDiv").show(100);
                    ShowCurrentOptionsItem();
                }
            }
            else {
                $("#MainDiv").hide(100);
                $('#bdPicket182').hide(100);
                alert("No Picks Availables For This Warehouse");
            }
            $("#bdPicket182").append(bodyRows);


        }

        //var selectPicksPending = function (e) {
        //    ChangeProcessing = false;
        //    ConfirmProcessing = false;
        //    SkipProcessing = false;
        //    DropProcessing = false;
        //    EndPickingProcessing = false;

        //    skip = false;
        //    ClearFormPicking();
        //    if (e != undefined) {
        //        if (verificarLoadersInactivos()) {
        //            TakeProcessing = true;
        //            let pick = e.getAttribute('pallet').trim();
        //            let cwar = e.getAttribute('cwar').trim();
        //            let indexTakeLoader = e.getAttribute('row').trim();
        //            $('#TakeLoader' + indexTakeLoader).show(100);
        //            EventoAjax("ClickPickingPending", "{'PICK':'" + pick + "','CWAR':'" + cwar + "'}", LoadPageSuccess);
        //        }
        //    }
        //    else {
        //        $('#TakeLoader' + indexTakeLoader).show(100);
        //        EventoAjax("ClickPickingPending", "{'PICK':'" + sessionStorage.getItem('PICK') + "','CWAR':'" + sessionStorage.getItem('CWAR') + "'}", LoadPageSuccess);
        //    }
        //}

        var ClearFormPicking = function () {
            paidInvalid();
            txPaid.value = "";
            txQtyc.value = "";
            //txLoca.value = "";

            //lblPick.innerHTML = "";
            //lblPaid.innerHTML = "";
            LblItem.innerHTML = "";
            lblLoca.innerHTML = "";
            lblWare.innerHTML = "";
            lblQtyc.innerHTML = "";
            lblUnit.innerHTML = "";
            NeutroInputText(txPaid);
            NeutroInputText(txQtyc);
            //NeutroInputText(txLoca);

            hideShowNeutroSelect(ddReason, false);
        }

        var ClearFormWarehouse = function () {

        }

        var SuccessGetWarehouse = function () {

        }

        var StartPicking = function () {

        }

        var SucceessStartPicking = function () {

        }

        var VerifyPalletID = function () {

        }

        var SucceessVerifyPalletID = function () {

        }

        //var VerifyLocation = function () {
        //    var CurrentLoca = lblLoca.innerHTML.trim();
        //    stoper();
        //    timer = setTimeout(function () {
        //        var NewLoca = txLoca.value.trim().toUpperCase();
        //        if (CurrentLoca.trim() == NewLoca.trim()) {
        //            locaValid();
        //            $('#lblError').html('');
        //            DisttinctLocaValid = false;
        //        }
        //        else {
        //            console.log("Pallet NO Igual");
        //            $('#lblError').html('')
        //            Method = "VerificarLocate"
        //            Data = "{'CWAR':'" + lblWare.innerHTML.trim() + "','LOCA':'" + txLoca.value.trim().toUpperCase() + "'}";
        //            EventoAjax(Method, Data, SucceessVerifyLocation)
        //        }
        //    }, 2000);
        //}

        //var SucceessVerifyLocation = function (r) {
        //    if (r.d == true) {
        //        console.log("Location valida");
        //        locaValid();
        //        hideShowNeutroSelect(ddReason, true);
        //        hideShowNeutroInputText(txQtyc, false);
        //        DisttinctLocaValid = true;
        //    }
        //    else {
        //        console.log("Location no valida");
        //        locaInvalid();
        //        $('#lblError').html('Location invalid');
        //        DisttinctLocaValid = false;
        //    }
        //}

        var VerifyQuantity = function (e) {
            console.log(e.keyCode+" "+e.which);
            if (e.keyCode >= 48 && e.keyCode <= 57 || e.keyCode == 9 || e.keyCode == 8 || e.keyCode == 190) {
                if (lblUnit.innerHTML.trim().toUpperCase() != "KG" && lblUnit.innerHTML.trim().toUpperCase() != "LB") {
                    txQtyc.value = txQtyc.value.replace(',', '').replace('.', '');
                }
                var qtycAct = parseFloat(lblQtyc.innerHTML.trim()) <= parseFloat($("#qtyTbl").html()) ? parseFloat(lblQtyc.innerHTML.trim()) : parseFloat($("#qtyTbl").html());
                var qtycNew = parseFloat(txQtyc.value.trim());
                if (qtycAct >= qtycNew && qtycNew > 0) {
                    qtytValid();
                    $('#lblError').html('');
                    qty = qtycNew;
                }
                else {
                    qtytInvalid();
                    $('#lblError').html('Quantity invalid')
                }
            }
            else {
                txQtyc.value = qty;
            }
        }

        var SucceessVerifyQuantity = function () {

        }
        //var SkipPicking = function () {
        //    skip = true;
        //    if (verificarLoadersInactivos()) {
        //        SkipProcessing = true;
        //        $('#SkipLoader').show(100);
        //        EventoAjax("SkipPicking", "{}", LoadPageSuccess);
        //    }
        //}
        var Confirm = function () {
            if (verificarLoadersInactivos()) {
                ConfirmProcessing = true;
                $("#ConfirmLoader").show(100);
                if (parseFloat($("#Contenido_lblQuantity").val()) <= 0) {
                    $("#Contenido_lblQuantity").focus();
                    $('#LblError').html("The quantity cann´t be empty, zero less than zero");
                    ConfirmProcessing = false;
                    $("#ConfirmLoader").hide(500);
                    return;
                }
                dataS = "{'QTYT':'" + (txQtyc.value.trim() == "" ? lblQtyc.innerHTML.trim() : txQtyc.value.trim()) + "','consigment':'" + (chkConsigment.checked == true ? 'true' : 'false') + "'}";

                //"'CUNI':'" + $('#Contenido_lblQuantityDesc').html() + "', 'LOCA':'" + $('#Contenido_lbllocation').html() + "', 'CWAR':'" + $('#Contenido_lblWarehouse').html() + "', 'CLOT':'" + $('#Contenido_LblLotId').html() + "'"
                var AjaxConfirm = $.ajax({
                    type: "POST",
                    url: "PickingUpdate.aspx/Click_confirPKG",
                    data: dataS,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        ConfirmProcessing = false;

                        if (bdPicket182.childElementCount > 0) {
                            for (let i = bdPicket182.childElementCount - 1; i >= 0 ; i--) {
                                bdPicket182.children[i].remove()
                            }
                        }
                        var myObj = JSON.parse(response.d)
                        if (myObj.Error == false) {
                            if (true) {
                                //    printDiv("MyEtiqueta");
                                myLabelFrame = document.getElementById('myLabelFrame');
                                if (sessionStorage.getItem('nav').toString() == 'EDG') {
                                    myLabelFrame.src = '../Labels/RedesingLabels/4FinishedCupsDoubleME.aspx';
                                }
                                else {
                                    myLabelFrame.src = '../Labels/RedesingLabels/4FinishedCupsDoubleME.aspx';

                                }
                            }
                            alert("Information saved successfully");
                            //selectPicksPending();
                            $("#ConfirmLoader").hide(500);
                            ClearFormPicking();
                            loadPicksPending();
                        }
                        else {
                            alert(myObj.errorMsg);
                            //JC 131121 Removido   selectPicksPending();
                            //JC cambio por loadpickpendings
                            loadPicksPending();
                            $("#ConfirmLoader").hide(500);
                        }
                    },
                    failure: function (response) {
                        alert(response.d);
                        $("#ConfirmLoader").hide(500);
                    }
                });
            }
        }

        var Stop = function () {
            if (verificarLoadersInactivos()) {
                ConfirmProcessing = true;
                $("#ConfirmLoader").show(100);
                if (parseFloat($("#Contenido_lblQuantity").val()) <= 0) {
                    $("#Contenido_lblQuantity").focus();
                    $('#LblError').html("The quantity cann´t be empty, zero less than zero");
                    ConfirmProcessing = false;
                    $("#ConfirmLoader").hide(500);
                    return;
                }
                dataS = "{'QTYT':'" + (txQtyc.value.trim() == "" ? lblQtyc.innerHTML.trim() : txQtyc.value.trim()) + "','consigment':'" + (chkConsigment.checked == true ? 'true' : 'false') + "'}";

                //"'CUNI':'" + $('#Contenido_lblQuantityDesc').html() + "', 'LOCA':'" + $('#Contenido_lbllocation').html() + "', 'CWAR':'" + $('#Contenido_lblWarehouse').html() + "', 'CLOT':'" + $('#Contenido_LblLotId').html() + "'"
                var AjaxConfirm = $.ajax({
                    type: "POST",
                    url: "PickingUpdate.aspx/StopPicking",
                    data: dataS,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        window.history.back();
                    }

                });
            }
        }


        var SucceessConfirm = function () {

        }



        var paidInvalid = function () {
            if (txPaid.value.trim() != "") {
                invalidElement(txPaid);
                //hideShowNeutroInputText(txLoca, false);
                hideShowNeutroInputText(txQtyc, false);
                hideShowNeutroSelect(ddReason, false);
                paidOk = false;
                locaOk = false;
                qtytOk = false;
                formValid();
            }
        }

        var paidValid = function () {
            if (txPaid.value.trim() != "") {
                validElement(txPaid);
                hideShowNeutroInputText(txQtyc, false);
                hideShowNeutroSelect(ddReason, false);
                if (sloc == "1") {
                    //hideShowNeutroInputText(txLoca, true);
                    locaOk = false;
                }
                else {
                    //hideShowNeutroInputText(txLoca, false);
                    cnpk == "1" ? hideShowNeutroInputText(txQtyc, false) : hideShowNeutroInputText(txQtyc, true);
                    locaOk = true;
                }
                paidOk = true;
                //locaOk = false;
                qtytOk = false;
                formValid();
            }
        }

        var locaInvalid = function () {
            if (txLoca.value.trim() != "") {
                invalidElement(txLoca);
                hideShowNeutroInputText(txQtyc, false);
                hideShowNeutroSelect(ddReason, false);
                locaOk = false;
                qtytOk = false;
                formValid();
            }
        }

        var locaValid = function () {
            if (txLoca.value.trim() != "") {
                validElement(txLoca);
                cnpk == "1" ? hideShowNeutroInputText(txQtyc, false) : hideShowNeutroInputText(txQtyc, true);
                hideShowNeutroSelect(ddReason, false);
                locaOk = true;
                qtytOk = false;
                formValid();
            }
        }

        var qtytInvalid = function () {
            if (txQtyc.value.trim() != "") {
                invalidElement(txQtyc);
                qtytOk = false;
                formValid();
            }
            else {
                NeutroInputText(txQtyc);
                qtytOk = false;
                formValid();
            }
        }

        var qtytValid = function () {
            if (txQtyc.value.trim() != "") {
                validElement(txQtyc);
                qtytOk = true;
                formValid();
            }
            else {
                NeutroInputText(txQtyc);
                qtytOk = false;
                formValid();
            }
        }


        var hideShowNeutroSelect = function (elemnt, show) {
            hideShowElement(elemnt, show);
            NeutroSelect(elemnt);
            if (show) {
                $(lblReason).show(100);
                $(ddReason).show(100);
                $(bntChange).hide(100);
            } else {
                $(lblReason).hide(100);
                $(ddReason).hide(100);
                $(bntChange).hide(100);
            }
        }


        var hideShowNeutroInputText = function (elemnt, show) {
            hideShowElement(elemnt, show);
            NeutroInputText(elemnt);
        }

        var hideShowElement = function (elemnt, show) {
            if (show) {
                $(elemnt).show(100);
            }
            else {
                $(elemnt).hide(100);
            }
        }

        var validElement = function (elemnt) {
            elemnt.classList.remove('is-invalid');
            elemnt.classList.add('is-valid');
        }

        var invalidElement = function (elemnt) {
            elemnt.classList.remove('is-valid');
            elemnt.classList.add('is-invalid');
        }

        var NeutroInputText = function (elemnt) {
            elemnt.value = "";
            elemnt.classList.remove('is-invalid');
            elemnt.classList.remove('is-valid');
        }

        var NeutroSelect = function (elemnt) {
            elemnt.value = "0";
            elemnt.classList.remove('is-invalid');
            elemnt.classList.remove('is-valid');
        }

        function formValid() {
            if (cnpk == "1") {
                if (paidOk == true) {
                    $('#btnConfirm').show(300);
                }
                else {
                    $('#btnConfirm').hide(300);
                }
            }
            else if (paidOk == true && qtytOk == true) {
                $('#btnConfirm').show(300);
            }
            else {
                $('#btnConfirm').hide(300);
            }
        }

        var verificarLoadersInactivos = function () {
            if (ConfirmProcessing != false) {
                return false;
            }
            else {
                return true;
            }
        }

        $(function () {
            var drop = false;
            StartComponents();
            //loadPicksPending();
            GetWarehouse();
        });
    </script>
</asp:Content>
