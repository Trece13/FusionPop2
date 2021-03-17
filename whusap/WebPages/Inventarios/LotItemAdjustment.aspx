<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="LotItemAdjustment.aspx.cs" Inherits="whusap.WebPages.Inventarios.LotItemAdjustment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
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
            padding: 20px;
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
            width: 400px;
        }

        #codeItem {
            display: block;
            margin: auto;
            height: 75px;
            width: 400px;
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
            display: none;
        }

        #editTable {
            display: none;
        }

        #lblError {
            color: red;
            font-size: 13px;
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

        @keyframes spin {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
        }
    </style>
    <form id="form1" class="container">
        <br />
        <br />
        <div class="form-group row">
            <label class="col-sm-1 col-form-label-lg" for="txPalletID">
                Pallet ID</label>
            <div class="col-sm-1 alingRight">
                <button type="button" class="btn btn-link col-4" id="btnRestart"><i class="fas fa-undo-alt fa-1x"></i></button>
            </div>
            <div class="col-sm-4 alingRight">
                <input type="text" class="form-control form-control-lg" id="txPalletID" placeholder="">
                <label class="col-sm-12 col-form-label-md" id="lblError"></label>
            </div>
            <div class="col-1 fa-2x">
                <i id="checkPaid" class="fas fa-check"></i>
                <i id="exPaid" class="fas fa-times"></i>
                <i id="loadPaid" class="fas fa-circle-notch fa-spin"></i>
            </div>
        </div>
    </form>
    <br />
    <hr />
    <br />
    <div id="editTable">
        <table class="table">
            <thead>
                <tr>
                    <th scope="col"></th>
                    <th scope="col"></th>
                    <th scope="col" colspan="">Item</th>
                    <th scope="col"></th>
                    <th scope="col"></th>
                    <th scope="col" colspan="">Lot</th>
                    <th scope="col"></th>
                    <th scope="col">Actual Qty</th>
                    <th scope="col">Unit</th>
                    <th scope="col" colspan="">Warehouse</th>
                    <th scope="col"></th>
                    <th scope="col" colspan="">Location</th>
                    <th scope="col"></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row" class="alingLeft">Actual Data</th>
                    <td></td>
                    <td id="lbItemActual">OOBPW-00600014</td>
                    <td></td>
                    <td id="lbItemDscaActual">OOBPW-00600014</td>
                    <td id="lbLotActual">OO0003923</td>
                    <td></td>
                    <td id="lbQtyActual">18</td>
                    <td id="lbUnitActual">CJ</td>
                    <td id="lbWarehouseActual">WFV900</td>
                    <td></td>
                    <td id="lbLocaActual">D300110303</td>
                    <td></td>

                </tr>
                <tr>
                    <th scope="row" class="alingLeft">Adjusted Data</th>
                    <td><i id="btnRestartForm" class="fas fa-undo"></i></td>
                    <td id="lbItemAdjusted" contenteditable="true">OOBPW-00600014</td>
                    <td><i id="checkItem" class="fas fa-check"></i><i id="exItem" class="fas fa-times"></i><i id="loadItem" class="fas fa-ellipsis-h"></i></td>
                    <td id="lbItemDscaAdjusted">OOBPW-00600014</td>
                    <td id="lbLotAdjusted" contenteditable="true">OO0003923</td>
                    <td><i id="checkLot" class="fas fa-check"></i><i id="exLot" class="fas fa-times"></i><i id="loadLot" class="fas fa-ellipsis-h"></i></td>
                    <td id="lbQtyAdjusted">18</td>
                    <td id="lbUnitAdjusted">CJ</td>
                    <td id="lbWarehouseAdjusted" contenteditable="true">WFV900</td>
                    <td><i id="checkWarehouse" class="fas fa-check"></i><i id="exWarehouse" class="fas fa-times"></i><i id="loadWarehouse" class="fas fa-ellipsis-h"></i></td>
                    <td id="lbLocaAdjusted" contenteditable="true">D300110303</td>
                    <td><i id="checkLoca" class="fas fa-check"></i><i id="exLoca" class="fas fa-times"></i><i id="loadLoca" class="fas fa-ellipsis-h"></i></td>
                </tr>
                <tr>
                    <th scope="row" class="alingLeft">Reason Code</th>
                    <td></td>
                    <td colspan="3">
                        <asp:DropDownList runat="server" ID="dropDownReasonCodes" CssClass="TextBoxBig"></asp:DropDownList>
                    </td>
                </tr>
                <tr style="border-top: none">
                    <th scope="row" class="alingLeft">Cost Center</th>
                    <td></td>
                    <td colspan="3">
                        <asp:DropDownList runat="server" ID="dropDownCostCenters" CssClass="TextBoxBig"></asp:DropDownList>
                    </td>
                </tr>
                <tr style="border-top: none" id="saveSection" class="notBorderBottom">
                    <th scope="row" colspan="3" class="notBorderBottom">
                        <button id="btnSave" type="button" class="btn btn-primary col-12">Save</button></th>
                </tr>
            </tbody>
        </table>
    </div>
    <div id="printContainer">
        <div id="printSpace">
            <div id="myLabel" class="container">
                <div class="row">
                    <div class="col-6 alingLeft">
                        <label id="lblitemDesc">LBRT ORG BLACK CHERRY 105</label>
                    </div>
                    <div class="col-6 alingRight">
                        <label id="lblMadein">MADE IN: DUBLIN - VA</label>
                    </div>
                </div>
                <br />
                <div class="col-12 divDesc">
                    <img id="codeItem" />
                </div>
                <div class="col-12 borderTop">
                    <img id="codePaid" />
                </div>
                <br />
                <div>
                    <table class="table">
                        <thead>
                            <tr>
                                <th scope="col">Work Order Lot</th>
                                <th scope="col">Pallet Number</th>
                                <th scope="col">Inspector Initial</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td id="lblWorkOrder">OM00180016</td>
                                <td id="lblPalletNum">1</td>
                                <td id="lblInspector"></td>
                            </tr>
                        </tbody>
                        <thead>
                            <tr>
                                <th scope="col">Date</th>
                                <th scope="col">Shift</th>
                                <th scope="col">Case Per Pallet</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td id="lblDate">Date</td>
                                <td id="lblShift">A,B,C,D</td>
                                <td id="lblQuantity">18</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <br />
        <div id="printButton" class="container">
            <button type="button" onclick="printLabel()" class="btn btn-link col-12"><i class="fas fa-print" id="btnPrint" ></i></button>
        </div>
    </div>
    <script type="text/javascript">
        
        class Ent_twhcol028 {
            PAID = "0";
            CDIS = "0";
            EMNO = "0";
            SITM = "0";
            SWAR = "0";
            SLOC = "0";
            SLOT = "0";
            SQTY = "0";
            TITM = "0";
            TWAR = "0";
            TLOC = "0";
            TLOT = "0";
            TQTY = "0";
            LOGN = "0";
            DATR = "0";
            PROC = "0";
            SORN = "0";
            SPON = "0";
            TORN = "0";
            TPON = "0";
            MESS = "0";
            REFCNTD = "0";
            REFCNTU = "0";
            UNIT = "";
            USER = ""
        }

        var restart = false;
        var waitSecontsPallet;
        var timeOutPallet = 0;
        var timeOutItem = 0;
        var timeOutLot = 0;
        var timeOutWarehouse = 0;
        var timeOutLoca = 0;

        function IdentificarControles() {
            //Form
            txPalletID = document.getElementById("txPalletID");
            lbItemActual = document.getElementById("lbItemActual");
            lbItemDscaActual = document.getElementById("lbItemDscaActual");
            lbLotActua = document.getElementById("lbLotActua");
            lbQtyActual = document.getElementById("lbQtyActual");
            lbUnitActual = document.getElementById("lbUnitActual");
            lbWarehouseActual = document.getElementById("lbWarehouseActual");
            lbLocaActual = document.getElementById("lbLocaActual");
            lbItemAdjusted = document.getElementById("lbItemAdjusted");
            lbItemDscaAdjusted = document.getElementById("lbItemDscaAdjusted");
            lbLotAdjusted = document.getElementById("lbLotAdjusted");
            lbQtyAdjusted = document.getElementById("lbQtyAdjusted");
            lbUnitAdjusted = document.getElementById("lbUnitAdjusted");
            lbWarehouseAdjusted = document.getElementById("lbWarehouseAdjusted");
            lbLocaAdjusted = document.getElementById("lbLocaAdjusted");
            dropDownReasonCodes = document.getElementById("dropDownReasonCodes");
            dropDownCostCenters = document.getElementById("dropDownCostCenters");
            btnSave = document.getElementById("btnSave");
            btnRestart = document.getElementById("btnRestart");
            btnRestartForm = document.getElementById("btnRestartForm");
            btnPrint = document.getElementById("btnPrint");
            
            loadWarehouse = document.getElementById("loadWarehouse");
            loadItem = document.getElementById("loadItem");
            loadLot = document.getElementById("loadLot");
            loadLoca = document.getElementById("loadLoca");
            loadPaid = document.getElementById("loadPaid");

            checkItem = document.getElementById("checkItem");
            checkLot = document.getElementById("checkLot");
            checkWarehouse = document.getElementById("checkWarehouse");
            checkLoca = document.getElementById("checkLoca");
            checkPaid = document.getElementById("checkPaid");

            exItem = document.getElementById("exItem");
            exLot = document.getElementById("exLot");
            exWarehouse = document.getElementById("exWarehouse");
            exLoca = document.getElementById("exLoca");
            exPaid = document.getElementById("exPaid");

            editTable = document.getElementById("editTable");
            printContainer = document.getElementById("printContainer");


            lblError = document.getElementById("lblError");
            //Label

            codeItem         = document.getElementById("codeItem");
            codePaid         = document.getElementById("codePaid");
            lblitemDesc      = document.getElementById("lblitemDesc");
            lblWorkOrder     = document.getElementById("lblWorkOrder");
            lblPalletNum     = document.getElementById("lblPalletNum");
            lblInspector     = document.getElementById("lblInspector");
            lblDate          = document.getElementById("lblDate");
            lblShift         = document.getElementById("lblShift");
            lblQuantity      = document.getElementById("lblQuantity");

            txPalletID.addEventListener("input", sendPallet, false);
            lbItemAdjusted.addEventListener("input", sendItem, false);
            lbLotAdjusted.addEventListener("input", sendLot, false);
            lbWarehouseAdjusted.addEventListener("input", sendWarehouse, false);
            lbLocaAdjusted.addEventListener("input", sendLoca, false);
            btnSave.addEventListener("click", sendInfo, false);
            btnRestart.addEventListener("click", restartAll, false);
            btnRestartForm.addEventListener("click", restartInfo, false);
            
        }

        function printDiv(divID) {

            //            //Get the HTML of div
            //            var divElements = document.getElementById(divID).innerHTML;
            //            //Get the HTML of whole page
            //            var oldPage = document.body.innerHTML;
            //            //Reset the page's HTML with div's HTML only
            //            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body></html>";
            //            //Print Page
            //            window.print();
            //            //Restore orignal HTML
            //            document.body.innerHTML = oldPage;
            //            window.close();
            //            return true;

            var mywindow = window.open('', 'PRINT', 'height=400px,width=600px');
            mywindow.document.write('<html><head>');
            mywindow.document.write('<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl" crossorigin="anonymous">');
            mywindow.document.write('<style>*{font-size: small !important;} #codePaid {display: block;margin: auto;height: 75px;width: 400px;} #codeItem {display: block;margin: auto;height: 75px;width: 400px;} #myLabel {width: 6in;height: 4in;padding: 20px;border: 1px solid black;border-radius: 12px;}.alingRight {text-align: right;}.alingLeft {text-align: left;}</style>');
            mywindow.document.write('</head><body >');
            mywindow.document.write(document.getElementById(divID).innerHTML);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/

            return true;
        };

        var printLabel = function(){
            printDiv("printSpace");
        }

        var handerTimeout = function(currentTimeOut, currentMethod) {
            clearTimeout(currentTimeOut);
            return setTimeout(currentMethod, 2000);
        }

        var restartAll = function(e) {
            restart = true;
            $("#editTable").hide(500);
            $("#printContainer").hide(500);
            $("#checkItem").hide(500);
            $("#exItem").hide(500);
            $("#loadItem").hide(500);
            $("#checkLot").hide(500);
            $("#exLot").hide(500);
            $("#loadLot").hide(500);
            $("#checkWarehouse").hide(500);
            $("#exWarehouse").hide(500);
            $("#loadWarehouse").hide(500);
            $("#checkLoca").hide(500);
            $("#exLoca").hide(500);
            $("#loadLoca").hide(500);
            $("#checkPaid").hide(500);
            $("#exPaid").hide(500);
            $("#loadPaid").hide(500);
            txPalletID.value = "";
            lblError.textContent = "";
            lbItemActual.textContent        = "";
            lbLocaActual.textContent        = "";
            lbLotActual.textContent         = "";
            lbWarehouseActual.textContent   = "";
            lbLocaActual.textContent        = "";
            lbItemDscaActual.textContent    = "";
            lbItemAdjusted.textContent      = "";
            lbLocaAdjusted.textContent      = "";
            lbLotAdjusted.textContent       = "";
            lbWarehouseAdjusted.textContent = "";
            lbLocaAdjusted.textContent      = "";
            lbItemDscaAdjusted.textContent  = "";
            lbItemAdjusted.classList.remove("isNotValid");
            lbItemAdjusted.classList.remove("isValid");
            lbLotAdjusted.classList.remove("isNotValid");
            lbLotAdjusted.classList.remove("isValid");
            lbWarehouseAdjusted.classList.remove("isNotValid");
            lbWarehouseAdjusted.classList.remove("isValid");
            lbLocaAdjusted.classList.remove("isNotValid");
            lbLocaAdjusted.classList.remove("isValid");
            $("#Contenido_dropDownReasonCodes").val("");
            $("#Contenido_dropDownCostCenters").val("")
            txPalletID.focus();
        }

        var restartInfo = function(e) {
            restart = true;
            $("#saveSection").hide(500);
            $("#checkItem").hide(500);
            $("#exItem").hide(500);
            $("#loadItem").hide(500);
            $("#checkLot").hide(500);
            $("#exLot").hide(500);
            $("#loadLot").hide(500);
            $("#checkWarehouse").hide(500);
            $("#exWarehouse").hide(500);
            $("#loadWarehouse").hide(500);
            $("#checkLoca").hide(500);
            $("#exLoca").hide(500);
            $("#loadLoca").hide(500);
            while (restart) {
                lbItemAdjusted.textContent = lbItemActual.textContent;
                lbLocaAdjusted.textContent = lbLocaActual.textContent;
                lbLotAdjusted.textContent = lbLotActual.textContent;
                lbWarehouseAdjusted.textContent = lbWarehouseActual.textContent;
                lbLocaAdjusted.textContent = lbLocaActual.textContent;
                lbItemDscaAdjusted.textContent = lbItemDscaActual.textContent;
                lbItemAdjusted.classList.remove("isNotValid");
                lbItemAdjusted.classList.remove("isValid");
                lbLotAdjusted.classList.remove("isNotValid");
                lbLotAdjusted.classList.remove("isValid");
                lbWarehouseAdjusted.classList.remove("isNotValid");
                lbWarehouseAdjusted.classList.remove("isValid");
                lbLocaAdjusted.classList.remove("isNotValid");
                lbLocaAdjusted.classList.remove("isValid");
                restart = false;
            }
        }

        var sendInfo = function(e) {
            var Obj028 = new Ent_twhcol028();
            Obj028.PAID = txPalletID.value.trim().toUpperCase();
            Obj028.CDIS = $("#Contenido_dropDownReasonCodes").val();
            Obj028.EMNO = $("#Contenido_dropDownCostCenters").val();
            Obj028.SITM = lbItemActual.textContent.trim();
            Obj028.SWAR = lbWarehouseActual.textContent.trim();
            Obj028.SLOC = lbLocaActual.textContent.trim();
            Obj028.SLOT = lbLotActual.textContent.trim();
            Obj028.SQTY = lbQtyActual.textContent.trim();
            Obj028.TITM = lbItemAdjusted.textContent.trim();
            Obj028.TWAR = lbWarehouseAdjusted.textContent.trim();
            Obj028.TLOC = lbLocaAdjusted.textContent.trim();
            Obj028.TLOT = lbLotAdjusted.textContent.trim();
            Obj028.TQTY = lbQtyActual.textContent.trim();
            Obj028.UNIT = lbUnitAdjusted.textContent.trim();
            Obj028.LOGN = "";
            Obj028.PROC = "2";
            Obj028.SORN = " ";
            Obj028.SPON = " ";
            Obj028.TORN = " ";
            Obj028.TPON = " ";
            Obj028.MESS = " ";
            Obj028.REFCNTD = " ";
            Obj028.REFCNTU = " ";

            var Data = "{twhcol028:" + JSON.stringify(Obj028) + "}";
            sendAjax("LotItemAdjustment.aspx/Save", Data, saveSuccess);
        }

        var sendPallet = function(e) {
            $("#checkPaid").hide(500);
            $("#exPaid").hide(500);
            $('#loadPaid').css("display","inline-block");
            timeOutPallet = handerTimeout(timeOutPallet, verifyPallet);
        }

        var sendItem = function(e) {
            $("#checkItem").hide(500);
            $("#exItem").hide(500);
            $("#loadItem").show(500);
            timeOutItem = handerTimeout(timeOutItem, verifyItem);
        }

        var sendLot = function(e) {
            $("#checkLot").hide(500);
            $("#exLot").hide(500);
            $("#loadLot").show(500);
            timeOutLot = handerTimeout(timeOutLot, verifyLot);
        }

        var sendWarehouse = function(e) {
            $("#checkWarehouse").hide(500);
            $("#exWarehouse").hide(500);
            $("#loadWarehouse").show(500);
            timeOutPallet = handerTimeout(timeOutWarehouse, verifyWarehouse);
        }

        var sendLoca = function(e) {
            $("#checkLoca").hide(500);
            $("#exLoca").hide(500);
            $("#loadLoca").show(500);
            timeOutPallet = handerTimeout(timeOutLoca, verifyLoca);
        }

        var verifyPallet = function(e) {
            var Data = "{'PAID':'" + txPalletID.value.trim().toUpperCase() + "'}";
            sendAjax("LotItemAdjustment.aspx/verifyPallet", Data, verifyPalletSuccess);
        }

        var verifyItem = function(e) {
            var Data = "{'ITEM':'" + lbItemAdjusted.textContent.trim().toUpperCase() + "'}";
            sendAjax("LotItemAdjustment.aspx/verifyItem", Data, verifyItemSuccess)
        }

        var verifyLot = function(e) {
            var Data = "{'ITEM':'" + lbItemAdjusted.textContent.trim().toUpperCase() + "','LOT':'" + lbLotAdjusted.textContent.trim().toUpperCase() + "'}";
            sendAjax("LotItemAdjustment.aspx/verifyLot", Data, verifyLotSuccess)
        }

        var verifyWarehouse = function(e) {
            var Data = "{'CWAR':'" + lbWarehouseAdjusted.textContent.trim().toUpperCase() + "'}";
            sendAjax("LotItemAdjustment.aspx/verifyWarehouse", Data, verifyWarehouseSuccess);
        }

        var verifyLoca = function(e) {
            var Data = "{'CWAR':'" + lbWarehouseAdjusted.textContent.trim().toUpperCase() + "','LOCA':'" + lbLocaAdjusted.textContent.trim().toUpperCase() + "'}";
            sendAjax("LotItemAdjustment.aspx/verifyLoca", Data, verifyLocaSuccess)
        }

        var verifyPalletSuccess = function(res) {
            var MyObj = JSON.parse(res.d);
            if(MyObj.Error == false){
                $("#loadPaid").hide(300);
                $("#exPaid").hide(500);
                $("#printContainer").hide(500);
                $("#checkPaid").show(500);
                lbItemActual.textContent = MyObj.ITEM;
                lbItemDscaActual.textContent = MyObj.DSCA;
                lbItemActual.setAttribute("KTLC", MyObj.KTLC);
                lbLotActual.textContent = MyObj.CLOT;
                lbQtyActual.textContent = MyObj.QTYA;
                lbUnitActual.textContent = MyObj.UNIT;
                lbWarehouseActual.textContent = MyObj.CWAR;
                lbWarehouseActual.setAttribute("sloc", MyObj.SLOC);
                lbLocaActual.textContent = MyObj.LOCA;
                lbItemAdjusted.textContent = MyObj.ITEM;
                lbItemDscaAdjusted.textContent = MyObj.DSCA;
                lbItemAdjusted.setAttribute("KTLC", MyObj.KTLC);
                if (MyObj.KTLC == "1") {
                    lbLotAdjusted.setAttribute("contentEditable", true);
                    lbLotAdjusted.focus();
                    checkLot.style.display = "none";
                    exLot.style.display = "none";
                } else {
                    lbLotAdjusted.textContent = "";
                    lbLotAdjusted.setAttribute("contentEditable", false);
                    checkLot.style.display = "none";
                    exLot.style.display = "none";
                }
                lbLotAdjusted.textContent = MyObj.CLOT;
                lbQtyAdjusted.textContent = MyObj.QTYA;
                lbUnitAdjusted.textContent = MyObj.UNIT;
                lbWarehouseAdjusted.textContent = MyObj.CWAR;
                lbWarehouseAdjusted.setAttribute("sloc", MyObj.SLOC);
                if (MyObj.SLOC == "1") {
                    lbLocaAdjusted.setAttribute("contentEditable", true);
                    checkLoca.style.display = "none";
                    exLoca.style.display = "none";
                    lbLocaAdjusted.focus();

                } else {
                    lbLocaAdjusted.textContent = "";
                    lbLocaAdjusted.setAttribute("contentEditable", false);
                    checkLoca.style.display = "none";
                    exLoca.style.display = "none";
                }
                lbLocaAdjusted.textContent = MyObj.LOCA;
                lblError.innerHTML = "";
                $('#editTable').show(500);
            }
            else{
                lblError.innerHTML = MyObj.errorMsg;
                $("#printContainer").hide(500);
                $('#editTable').hide(500);
                $("#loadPaid").hide(300);
                $("#checkPaid").hide(500);
                $("#exPaid").show(500);

            }
        }

        var saveSuccess = function(res) {
            var MyObjTwhcol028 = JSON.parse(res.d);
            if(MyObjTwhcol028.Error == false){
                
                JsBarcode("#codePaid", MyObjTwhcol028.PAID);
                JsBarcode("#codeItem", MyObjTwhcol028.SITM);
                lblitemDesc.textContent = lbItemDscaAdjusted.textContent;
                lblWorkOrder.textContent = MyObjTwhcol028.PAID.substring(0,(MyObjTwhcol028.PAID.indexOf("-")));;
                lblPalletNum.textContent =  MyObjTwhcol028.PAID.substring((MyObjTwhcol028.PAID.indexOf("-"))+1);
                lblInspector.textContent = MyObjTwhcol028.LOGN;
                lblDate.textContent = MyObjTwhcol028.DATR;
                lblShift.textContent = $('#LblShif1').text().replace("Shift:","");;
                lblQuantity.textContent = lbQtyAdjusted.textContent;
                $("#editTable").hide(500);
                $('#printContainer').show(500);
            }
            else{
                alert("error no insert");
                $('#printContainer').hide(500);
            }
        }

        var verifyItemSuccess = function(res) {
            var MyObj = JSON.parse(res.d);
            if (MyObj.Error) {
                lblError.innerHTML = MyObj.errorMsg;
                lbItemAdjusted.textContent = lbItemAdjusted.textContent.trim()
                $("#loadItem").hide(500);
                $("#checkItem").hide(500);
                $("#checkLot").hide(500);
                $("#exLot").hide(500);
                $("#checkLot").hide(500);
                $("#exItem").show(500);
                console.log(MyObj.errorMsg);
                lbItemAdjusted.classList.remove("isValid");
                lbItemAdjusted.classList.add("isNotValid");
                lbLotAdjusted.textContent = "";
                lbLotAdjusted.setAttribute("contentEditable", false);
                lbLotAdjusted.classList.remove("isValid");
                lbLotAdjusted.classList.remove("isNotValid");
                lbItemDscaAdjusted.textContent = "";
                verifyInfoForm();
            } else {
                lblError.innerHTML.replace(MyObj.errorMsg,"");
                lbItemAdjusted.textContent = lbItemAdjusted.textContent.trim()
                $("#loadItem").hide(500);
                $("#checkItem").show(500);
                $("#checkLot").hide(500);
                $("#exLot").hide(500);
                $("#exItem").hide(500);
                lbItemAdjusted.classList.remove("isNotValid");
                lbItemAdjusted.classList.add("isValid");
                lbItemAdjusted.setAttribute("ktlc", MyObj.KTLC);
                lbItemDscaAdjusted.textContent = MyObj.DSCA;
                lbUnitAdjusted.textContent = MyObj.UNIT;
                lbLotAdjusted.textContent = "";
                if (MyObj.KTLC == "1") {
                    lbLotAdjusted.setAttribute("contentEditable", true);
                    lbLotAdjusted.focus();
                    checkLot.style.display = "none";
                    exLot.style.display = "none";
                } else {
                    lbLotAdjusted.textContent = "";
                    lbLotAdjusted.setAttribute("contentEditable", false);
                    checkLot.style.display = "none";
                    exLot.style.display = "none";
                }
                console.log("Exito Item");
                verifyInfoForm();
            }
        }

        var verifyLotSuccess = function(res) {
            var MyObj = JSON.parse(res.d);
            if (MyObj.Error) {
                lbLotAdjusted.textContent = lbLotAdjusted.textContent.trim()
                $("#loadLot").hide(500);
                $("#checkLot").hide(500);
                $("#exLot").show(500);
                lbLotAdjusted.classList.remove("isValid");
                lbLotAdjusted.classList.add("isNotValid");
                verifyInfoForm();
            } else {
                lbLotAdjusted.textContent = lbLotAdjusted.textContent.trim()
                $("#loadLot").hide(500);
                $("#checkLot").show(500);
                $("#exLot").hide(500);
                lbLotAdjusted.classList.remove("isNotValid");
                lbLotAdjusted.classList.add("isValid");
                console.log("Exito Lot");
                verifyInfoForm();
            }
        }

        var verifyWarehouseSuccess = function(res) {
            var MyObj = JSON.parse(res.d);
            if (MyObj.Error) {
                lbWarehouseAdjusted.textContent = lbWarehouseAdjusted.textContent.trim()
                $("#loadWarehouse").hide(500);
                $("#checkWarehouse").hide(500);
                $("#checkLoca").hide(500);
                $("#exLoca").hide(500);
                $("#exWarehouse").show(500);
                console.log(MyObj.errorMsg);
                lbWarehouseAdjusted.classList.remove("isValid");
                lbWarehouseAdjusted.classList.add("isNotValid");
                lbLocaAdjusted.textContent = "";
                lbLocaAdjusted.setAttribute("contentEditable", false);
                lbLocaAdjusted.classList.remove("isValid");
                lbLocaAdjusted.classList.remove("isNotValid");
                verifyInfoForm();
            } else {
                lbWarehouseAdjusted.textContent = lbWarehouseAdjusted.textContent.trim()
                $("#exLoca").hide(500);
                $("#checkLoca").hide(500);
                $("#loadWarehouse").hide(500);
                $("#exWarehouse").hide(500);
                $("#checkWarehouse").show(500);
                checkWarehouse.style.display = "inline-block";
                exWarehouse.style.display = "none";
                lbLocaAdjusted.textContent = "";
                lbWarehouseAdjusted.classList.remove("isNotValid");
                lbWarehouseAdjusted.classList.add("isValid");
                lbLocaAdjusted.classList.remove("isValid");
                lbLocaAdjusted.classList.remove("isNotValid");
                lbWarehouseAdjusted.setAttribute("sloc", MyObj.SLOC);
                if (MyObj.SLOC == "1") {
                    lbLocaAdjusted.setAttribute("contentEditable", true);
                    checkLoca.style.display = "none";
                    exLoca.style.display = "none";
                    lbLocaAdjusted.focus();

                } else {
                    lbLocaAdjusted.textContent = "";
                    lbLocaAdjusted.setAttribute("contentEditable", false);
                    checkLoca.style.display = "none";
                    exLoca.style.display = "none";
                }
                console.log("Exito Warehouse");
                verifyInfoForm();
            }
        }

        var verifyLocaSuccess = function(res) {
            var MyObj = JSON.parse(res.d);
            if (MyObj.Error) {
                lbLocaAdjusted.textContent = lbLocaAdjusted.textContent.trim()
                $("#loadLoca").hide(500);
                $("#checkLoca").hide(500);
                $("#exLoca").show(500);
                checkLoca.style.display = "none";
                exLoca.style.display = "inline-block";
                console.log(MyObj.errorMsg);
                lbLocaAdjusted.classList.remove("isValid");
                lbLocaAdjusted.classList.add("isNotValid");
                lblError.innerHTML = MyObj.errorMsg;
                verifyInfoForm();
            } else {
                lbLocaAdjusted.textContent = lbLocaAdjusted.textContent.trim()
                $("#loadLoca").hide(500);
                $("#checkLoca").show(500);
                $("#exLoca").hide(500);
                checkLoca.style.display = "inline-block";
                exLoca.style.display = "none";
                lbLocaAdjusted.classList.remove("isNotValid");
                lbLocaAdjusted.classList.add("isValid");
                console.log("Exito Loca");
                lblError.innerHTML = "";
                verifyInfoForm();
            }
        }
        $("#Contenido_dropDownReasonCodes").change(
            function(){verifyInfoForm();}
            );

        $("#Contenido_dropDownCostCenters").change(
            function(){verifyInfoForm();}
            );

        var verifyInfoForm = function (){
            var sameItemLot = false;
            var sameWarehouseLoca = false;
            var combinationItemValid = false;
            var combinationWarehouseValid = false;
            var itemValid   = false;
            var lotValid    = false;
            var warehouseValid   = false;
            var itemNotValid       = false; 
            var lotNotValid        = false; 
            var warehouseNotValid  = false; 
            var locaNotValid       = false; 
            var locaValid   = false;
            var reasonValid = false;
            var codeValid = false;
            var ktlc = lbItemAdjusted.getAttribute("ktlc");
            var sloc = lbWarehouseAdjusted.getAttribute("sloc");

            itemNotValid        = lbItemAdjusted.classList.contains("isNotValid");
            lotNotValid         = lbLotAdjusted.classList.contains("isNotValid");
            warehouseNotValid   = lbWarehouseAdjusted.classList.contains("isNotValid");;
            locaNotValid        = lbLocaAdjusted.classList.contains("isNotValid");

            itemValid = lbItemAdjusted.classList.contains("isValid");
            lotValid = lbLotAdjusted.classList.contains("isValid");
            warehouseValid = lbWarehouseAdjusted.classList.contains("isValid");;
            locaValid = lbLocaAdjusted.classList.contains("isValid");

            reasonValid  = $("#Contenido_dropDownReasonCodes").val() == "" ? false : true;
            codeValid = $("#Contenido_dropDownCostCenters").val()  == "" ? false : true;

            if(ktlc === "1"){
                if(lbItemAdjusted.textContent.trim().toUpperCase() == lbItemActual.textContent.trim().toUpperCase()){
                    if(lbLotAdjusted.textContent.trim().toUpperCase() == lbLotActual.textContent.trim().toUpperCase()){
                        sameItemLot = true;
                    }
                }
            }
            else{
                if(lbItemAdjusted.textContent.trim().toUpperCase() == lbItemActual.textContent.trim().toUpperCase()){
                    sameItemLot = true;
                }
            }

            if(sloc === "1"){
                if(lbWarehouseAdjusted.textContent.trim().toUpperCase() == lbWarehouseActual.textContent.trim().toUpperCase()){
                    if(lbLocaAdjusted.textContent.trim().toUpperCase() == lbLocaActual.textContent.trim().toUpperCase()){
                        sameWarehouseLoca = true;
                    }
                }
            }
            else{
                if(lbWarehouseAdjusted.textContent.trim().toUpperCase() == lbWarehouseActual.textContent.trim().toUpperCase()){
                    sameWarehouseLoca = true;
                }
            }

            if(sameItemLot === true && sameWarehouseLoca === true){
                $("#saveSection").hide(500);
                lblError.textContent = "Actual Data and Adjusted Data cann't be the same";
                return;
            }
            else if(sameItemLot === true && sameWarehouseLoca === false){
                combinationItemValid = true;
            }
            else if(sameItemLot === false  && sameWarehouseLoca === true){
                combinationWarehouseValid = true;
            }
            else if(sameItemLot === false  && sameWarehouseLoca === false ){
            }

            if(itemValid && combinationItemValid == false){
                if(ktlc === "1"){
                    if(lotValid){
                        combinationItemValid = true;
                    }
                    else{
                        combinationItemValid = false;
                        lbLotAdjusted.focus();
                        $("#saveSection").hide(500);
                        return;
                    }
                }   
                else{
                    combinationItemValid = true;
                }
            }
            else if (combinationItemValid == true){

            }
            else{
                lbItemAdjusted.focus();
                $("#saveSection").hide(500);
                return;
            }

            if(warehouseValid && combinationWarehouseValid == false){
                if(sloc === "1"){
                    if(locaValid){
                        combinationWarehouseValid = true;
                    }
                    else{
                        combinationWarehouseValid = false;
                        lbLocaAdjusted.focus();
                        $("#saveSection").hide(500);
                        return;
                    }
                }   
                else{
                    combinationWarehouseValid = true;
                }
            }
            else if (combinationWarehouseValid == true){

            }
            else{
                lbWarehouseAdjusted.focus();
                $("#saveSection").hide(500);
                return;
            }
            if(!reasonValid){
                $("#Contenido_dropDownReasonCodes").focus();
                $("#saveSection").hide(500);
                return;
            }

            if(!codeValid){
                $("#Contenido_dropDownCostCenters").focus();
                $("#saveSection").hide(500);
                return;
            }

            if( combinationItemValid === true && combinationWarehouseValid === true && reasonValid === true && codeValid === true ){
                $("#saveSection").show(500);
            }
            else{
                $("#saveSection").hide(500);
            }
        }

        $(function() {

            IdentificarControles();
        });

    </script>
    <script src="../../Scripts/script.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/jsbarcode@3.11.0/dist/JsBarcode.all.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.1.1.min.js"></script>
</asp:Content>
