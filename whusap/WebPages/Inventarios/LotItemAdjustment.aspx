<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="LotItemAdjustment.aspx.cs" Inherits="whusap.WebPages.Inventarios.LotItemAdjustment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css"
        integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p"
        crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style type="text/css">
        #MyEtiqueta label {
            font-size: 15px;
        }

        #LblDate {
            font-size: 14px !important;
        }

        #LblReprintInd, #LblReprint {
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

        #loadWarehouse {
            display: none;
        }

        .fa-check {
            color: green;
        }

        .fa-times {
            color: red;
        }

        #checkItem, #checkLot, #checkWarehouse, #checkLoca {
            display: none;
        }

        #exItem, #exLot, #exWarehouse, #exLoca {
            display: none;
        }

        #loadItem, #loadLot, #loadWarehouse, #loadLoca {
            display: none;
        }
    </style>
    <form id="form1" class="container">
        <div class="form-group row">
            <label class="col-sm-2 col-form-label-lg" for="txPalletID">
                Pallet ID</label>
            <div class="col-sm-4">
                <input type="text" class="form-control form-control-lg" id="txPalletID" placeholder="Pallet ID">
            </div>
        </div>
    </form>
    <br />
    <hr />
    <div>
        <table class="table">
            <thead>
                <tr>
                    <th scope="col"></th>
                    <th scope="col"></th>
                    <th scope="col" colspan="2">Item</th>
                    <th scope="col"></th>
                    <th scope="col" colspan="2">Lot</th>
                    <th scope="col">Actual Qty</th>
                    <th scope="col">Unit</th>
                    <th scope="col" colspan="2">Warehouse</th>
                    <th scope="col" colspan="2">Location</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row" colspan="2">Actual Data</th>
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
                    <th scope="row">Adjusted Data</th>
                    <td><i id="btnRestart" class="fas fa-undo"></i></td>
                    <td id="lbItemAdjusted" contenteditable="true">OOBPW-00600014</td>
                    <td><i id="checkItem" class="fas fa-check"></i><i id="exItem" class="fas fa-times"></i><i id="loadItem" class="fas fa-spinner fa-pulse"></i></td>
                    <td id="lbItemDscaAdjusted">OOBPW-00600014</td>
                    <td id="lbLotAdjusted" contenteditable="true">OO0003923</td>
                    <td><i id="checkLot" class="fas fa-check"></i><i id="exLot" class="fas fa-times"></i><i id="loadLot" class="fas fa-spinner fa-pulse"></i></td>
                    <td id="lbQtyAdjusted">18</td>
                    <td id="lbUnitAdjusted">CJ</td>
                    <td id="lbWarehouseAdjusted" contenteditable="true">WFV900</td>
                    <td><i id="checkWarehouse" class="fas fa-check"></i><i id="exWarehouse" class="fas fa-times"></i><i id="loadWarehouse" class="fas fa-spinner fa-pulse"></i></td>
                    <td id="lbLocaAdjusted" contenteditable="true">D300110303</td>
                    <td><i id="checkLoca" class="fas fa-check"></i><i id="exLoca" class="fas fa-times"></i><i id="loadLoca" class="fas fa-spinner fa-pulse"></i></td>
                </tr>
                <tr>
                    <th scope="row">Reason Code</th>
                    <td></td>
                    <td colspan="3">
                        <asp:DropDownList runat="server" ID="dropDownReasonCodes" CssClass="TextBoxBig"></asp:DropDownList></td>
                </tr>
                <tr style="border-top: none">
                    <th scope="row">Cost Center</th>
                    <td></td>
                    <td colspan="3">
                        <asp:DropDownList runat="server" ID="dropDownCostCenters" CssClass="TextBoxBig"></asp:DropDownList></td>
                </tr>
                <tr style="border-top: none">
                    <th scope="row" colspan="2">
                        <button id="btnSave" type="button" class="btn btn-primary col-8">Save</button></th>
                </tr>
            </tbody>
        </table>
    </div>
    <script type="text/javascript">
        //var twhcol028 = {
        //    PAID: "",
        //    CDIS: "",
        //    EMNO: "",
        //    SITM: "",
        //    SWAR: "",
        //    SLOC: "",
        //    SLOT: "",
        //    SQTY: "",
        //    TITM: "",
        //    TWAR: "",
        //    TLOC: "",
        //    TLOT: "",
        //    TQTY: "",
        //    LOGN: "",
        //    DATR: "",
        //    PROC: "",
        //    SORN: "",
        //    SPON: "",
        //    TORN: "",
        //    TPON: "",
        //    MESS: "",
        //    REFCNTD: "",
        //    REFCNTU: ""
        //}
        class Ent_twhcol028  {
            PAID    = "0";
            CDIS    = "0";
            EMNO    = "0";
            SITM    = "0";
            SWAR    = "0";
            SLOC    = "0";
            SLOT    = "0";
            SQTY    = "0";
            TITM    = "0";
            TWAR    = "0";
            TLOC    = "0";
            TLOT    = "0";
            TQTY    = "0";
            LOGN    = "0";
            DATR    = "0";
            PROC    = "0";
            SORN    = "0";
            SPON    = "0";
            TORN    = "0";
            TPON    = "0";
            MESS    = "0";
            REFCNTD = "0";
            REFCNTU = "0";
        }

        var restart = false;
        var waitSecontsPallet;
        var timeOutPallet = 0;
        var timeOutItem = 0;
        var timeOutLot = 0;
        var timeOutWarehouse = 0;
        var timeOutLoca = 0;
        function IdentificarControles() {


            //MyEtiquetaOC = $('#MyEtiquetaOC');
            //MyEtiqueta = $('#MyEtiqueta');

            //Formulario
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

            loadWarehouse = document.getElementById("loadWarehouse");

            checkItem = document.getElementById("checkItem");
            checkLot = document.getElementById("checkLot");
            checkWarehouse = document.getElementById("checkWarehouse");
            checkLoca = document.getElementById("checkLoca");

            exItem = document.getElementById("exItem");
            exLot = document.getElementById("exLot");
            exWarehouse = document.getElementById("exWarehouse");
            exLoca = document.getElementById("exLoca");

            txPalletID.addEventListener("input", sendPallet, false);
            lbItemAdjusted.addEventListener("input", sendItem, false);
            lbLotAdjusted.addEventListener("input", sendLot, false);
            lbWarehouseAdjusted.addEventListener("input", sendWarehouse, false);
            lbLocaAdjusted.addEventListener("input", sendLoca, false);
            btnSave.addEventListener("click", sendInfo, false);
            btnRestart.addEventListener("click", restartInfo, false);

        }

        var handerTimeout = function (currentTimeOut, currentMethod) {
            clearTimeout(currentTimeOut);
            return setTimeout(currentMethod, 2000);
        }

        var restartInfo = function (e) {
            restart = true;
            while (restart) {
                lbItemAdjusted.textContent = lbItemActual.textContent;
                lbLotAdjusted.textContent = lbLotActual.textContent;
                lbWarehouseAdjusted.textContent = lbWarehouseActual.textContent;
                lbLocaAdjusted.textContent = lbLocaActual.textContent;

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

        var sendInfo = function (e) {
            var Obj028 = new Ent_twhcol028();
            Obj028.PAID = txPalletID.value.trim().toUpperCase();
            Obj028.CDIS = "0";
            Obj028.EMNO = "0";
            Obj028.SITM = "0";
            Obj028.SWAR = "0";
            Obj028.SLOC = "0";
            Obj028.SLOT = "0";
            Obj028.SQTY = "0";
            Obj028.TITM = "0";
            Obj028.TWAR = "0";
            Obj028.TLOC = "0";
            Obj028.TLOT = "0";
            Obj028.TQTY = "0";
            Obj028.LOGN = "0";
            Obj028.DATR = "0";
            Obj028.PROC = "0";
            Obj028.SORN = "0";
            Obj028.SPON = "0";
            Obj028.TORN = "0";
            Obj028.TPON = "0";
            Obj028.MESS = "0";
            Obj028.REFCNTD = "0";
            Obj028.REFCNTU = "0";

            var Data = "{twhcol028:" + JSON.stringify(Obj028) + "}";
            sendAjax("LotItemAdjustment.aspx/Save", Data, saveSuccess);
        }

        var sendPallet = function (e) {
            timeOutPallet = handerTimeout(timeOutPallet, verifyPallet);
        }

        var sendItem = function (e) {
            timeOutItem = handerTimeout(timeOutItem, verifyItem);
        }

        var sendLot = function (e) {
            timeOutLot = handerTimeout(timeOutLot, verifyLot);
        }

        var sendWarehouse = function (e) {
            timeOutPallet = handerTimeout(timeOutWarehouse, verifyWarehouse);
        }

        var sendLoca = function (e) {
            timeOutPallet = handerTimeout(timeOutLoca, verifyLoca);
        }

        var verifyPallet = function (e) {
            var Data = "{'PAID':'" + txPalletID.value.trim().toUpperCase() + "'}";
            sendAjax("LotItemAdjustment.aspx/verifyPallet", Data, verifyPalletSuccess);

        }

        var verifyItem = function (e) {
            var Data = "{'ITEM':'" + lbItemAdjusted.textContent.trim().toUpperCase() + "'}";
            sendAjax("LotItemAdjustment.aspx/verifyItem", Data, verifyItemSuccess)
        }

        var verifyLot = function (e) {
            var Data = "{'ITEM':'" + lbItemAdjusted.textContent.trim().toUpperCase() + "','LOT':'" + lbLotAdjusted.textContent.trim().toUpperCase() + "'}";
            sendAjax("LotItemAdjustment.aspx/verifyLot", Data, verifyLotSuccess)
        }

        var verifyWarehouse = function (e) {
            var Data = "{'CWAR':'" + lbWarehouseAdjusted.textContent.trim().toUpperCase() + "'}";
            sendAjax("LotItemAdjustment.aspx/verifyWarehouse", Data, verifyWarehouseSuccess);
        }

        var verifyLoca = function (e) {
            var Data = "{'LOCA':'" + lbLocaAdjusted.textContent.trim().toUpperCase() + "'}";
            sendAjax("LotItemAdjustment.aspx/verifyLoca", Data, verifyLocaSuccess)
        }



        var verifyPalletSuccess = function (res) {
            var MyObj = JSON.parse(res.d);
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
            lbLotAdjusted.textContent = MyObj.CLOT;
            lbQtyAdjusted.textContent = MyObj.QTYA;
            lbUnitAdjusted.textContent = MyObj.UNIT;
            lbWarehouseAdjusted.textContent = MyObj.CWAR;
            lbWarehouseAdjusted.setAttribute("sloc", MyObj.SLOC);
            lbLocaAdjusted.textContent = MyObj.LOCA;
        }
        var saveSuccess = function (res) {
            console.log("save ok")
        }

        var verifyItemSuccess = function (res) {
            var MyObj = JSON.parse(res.d);
            if (MyObj.Error) {
                checkItem.style.display = "none";
                exItem.style.display = "inline-block";
                checkLot.style.display = "none";
                exLot.style.display = "none";
                console.log(MyObj.errorMsg);
                lbItemAdjusted.classList.remove("isValid");
                lbItemAdjusted.classList.add("isNotValid");
                lbLotAdjusted.textContent = "";
                lbLotAdjusted.setAttribute("contentEditable", false);
                lbLotAdjusted.classList.remove("isValid");
                lbLotAdjusted.classList.remove("isNotValid");
                lbItemDscaAdjusted.textContent = "";
            }
            else {
                checkItem.style.display = "inline-block";
                exItem.style.display = "none";
                lbItemAdjusted.classList.remove("isNotValid");
                lbItemAdjusted.classList.add("isValid");
                lbItemAdjusted.setAttribute("ktlc", MyObj.KTLC);
                lbItemDscaAdjusted.textContent = MyObj.DSCA;
                lbUnitAdjusted.textContent = MyObj.UNIT;
                if (MyObj.KTLC == "1") {
                    lbLotAdjusted.setAttribute("contentEditable", true);
                    lbLotAdjusted.focus();
                    checkLot.style.display = "none";
                    exLot.style.display = "none";
                }
                else {
                    lbLotAdjusted.textContent = "";
                    lbLotAdjusted.setAttribute("contentEditable", false);
                    checkLot.style.display = "none";
                    exLot.style.display = "none";
                }
                console.log("Exito Item");
            }
        }

        var verifyLotSuccess = function (res) {
            var MyObj = JSON.parse(res.d);
            if (MyObj.Error) {
                checkLot.style.display = "none";
                exLot.style.display = "inline-block";
                console.log(MyObj.errorMsg);
                lbLotAdjusted.classList.remove("isValid");
                lbLotAdjusted.classList.add("isNotValid");
            }
            else {
                checkLot.style.display = "inline-block";
                exLot.style.display = "none";
                lbLotAdjusted.classList.remove("isNotValid");
                lbLotAdjusted.classList.add("isValid");
                console.log("Exito Lot");
            }
        }

        var verifyWarehouseSuccess = function (res) {

            var MyObj = JSON.parse(res.d);
            if (MyObj.Error) {
                checkWarehouse.style.display = "none";
                exWarehouse.style.display = "inline-block";
                checkLoca.style.display = "none";
                exLoca.style.display = "none";
                console.log(MyObj.errorMsg);
                lbWarehouseAdjusted.classList.remove("isValid");
                lbWarehouseAdjusted.classList.add("isNotValid");
                lbLocaAdjusted.textContent = "";
                lbLocaAdjusted.setAttribute("contentEditable", false);
                lbLocaAdjusted.classList.remove("isValid");
                lbLocaAdjusted.classList.remove("isNotValid");
            }
            else {
                checkWarehouse.style.display = "inline-block";
                exWarehouse.style.display = "none";
                lbWarehouseAdjusted.classList.remove("isNotValid");
                lbWarehouseAdjusted.classList.add("isValid");
                lbWarehouseAdjusted.setAttribute("sloc", MyObj.SLOC);
                if (MyObj.SLOC == "1") {
                    lbLocaAdjusted.setAttribute("contentEditable", true);
                    checkLoca.style.display = "none";
                    exLoca.style.display = "none";
                    lbLocaAdjusted.focus();

                }
                else {
                    lbLocaAdjusted.textContent = "";
                    lbLocaAdjusted.setAttribute("contentEditable", false);
                    checkLoca.style.display = "none";
                    exLoca.style.display = "none";
                }
                console.log("Exito Warehouse");
            }
        }

        var verifyLocaSuccess = function (res) {
            var MyObj = JSON.parse(res.d);
            if (MyObj.Error) {
                checkLoca.style.display = "none";
                exLoca.style.display = "inline-block";
                console.log(MyObj.errorMsg);
                lbLocaAdjusted.classList.remove("isValid");
                lbLocaAdjusted.classList.add("isNotValid");
            }
            else {
                checkLoca.style.display = "inline-block";
                exLoca.style.display = "none";
                lbLocaAdjusted.classList.remove("isNotValid");
                lbLocaAdjusted.classList.add("isValid");
                console.log("Exito Loca");
            }
        }

        $(function () {

            IdentificarControles();


        });
    </script>
    <script src="../../Scripts/script.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"
        integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1"
        crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"
        integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM"
        crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.1.1.min.js"></script>
</asp:Content>
