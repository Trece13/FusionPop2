﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvTransfers.aspx.cs" Inherits="whusap.WebPages.InvFloor.whInvTransfers" %>
<asp:content id="Content1" contentplaceholderid="Encabezado" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="Contenido" runat="server">
    <link rel="stylesheet" href="styles/bootstrap.min.css">
    <link rel="stylesheet" href="styles/font-awesome.min.css">

    <style>
        .CorrectWarehouse 
        {
            border-bottom: solid 3px green;
        }
        
        .IncorrectWarehouse
        {
            border-bottom: solid 3px red;
        }
        
        .ContenidoOculto
        {
           display: none;
        }
        
    </style>

    <form id="form1" class="container">
    <div id="divTransferHeader" style="display: block;">
    <div class="form-group row ">
        <label class="col-2 col-form-label-lg" for="txPalletId" id="lblTitPalletId">
            Pallet ID</label>
        <div class="col-3">
            <input type="text" class="form-control form-control-lg col-12" id="txPalletId" placeholder="Pallet ID">&nbsp
        </div>
        <div class="col-2">
            <button class="btn btn-lg btn-success col-12"  onclick="CleanPalletID(); return false;">Change Pallet ID </button>
        </div>
    </div>
    
    <div class="form-group row" id="divQueryAction">
        <div class="col-sm-2">
        </div>
        <div class="col-sm-4">
            <input type="button" class="btn btn-primary btn-lg col-sm-7 " id="btnQuery" value="Query" />
        </div>
        
    </div>
    </div>
    <div id="divTransferDetail">
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg"  id="lblTitItem">
            Item</label>
        <div class="col-sm-4">
                <label class="col-5 col-form-label" id="lblItemId" >-</label>
                <label class="col-6 col-form-label" id="lblItemDescription" >-</label>
        </div>
    </div>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" id="lblTitCurrentLocation">
            Current Location</label>
        <div class="col-sm-2">
              <input type="text" class="form-control form-control-lg col-sm-12" id="txbCurrentWarehouse" placeholder="Warehouse"/>
        </div>
        <div class="col-sm-2">
                <input type="text" class="form-control form-control-lg col-sm-12" id="txbCurrentLocation" placeholder="Locate"/>
                </div>
    </div>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" id="lblTitQuality">
            Quantity</label>
        <div class="col-3">
                <label class="col-9 col-form-label-lg" id ="lblQuatity" >-</label>
                <label class="col-2 col-form-label-lg" id ="lblUnit" >-</label>
        </div>
    </div>
    
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" id="lblTitTargetLocation">
            Target Location</label>
        <div class="col-sm-2">
                <input type="text" class="form-control form-control-lg col-sm-12" id="txbTargetWarehouse" placeholder="Warehouse" onchange="VerificarTipoWarehouse()"/>
        </div>
        <div class="col-sm-2">
                <input type="text" class="form-control form-control-lg col-sm-12"" id="txbTargetLocation" placeholder="Locate"/>
                </div>
        
    </div>

    <div class="form-group row">
        <input type="button" class="btn btn-primary btn-lg col-sm-6" id="btnTransfer" value="Transfer" />
    </div>

    </div>
    <label id="lblError" style="font-size:24px; color:red" >
    </label>
    </form>
    <script  type="text/javascript">
        var SLOC = false;
        var SuccesPalletConsult = function (r) {

            MyObject = JSON.parse(r.d)
            if (MyObject.Error == false) {
                //MyObject.TBL;
                //MyObject.CLOT = ,
                //MyObject.SQNB  = ,
                MyObject.sqnb;
                lblItemId.innerHTML = MyObject.mitm;
                //MyObject.DSCA  = "",
                txbCurrentWarehouse.value = MyObject.cwor;
                (txbCurrentWarehouse.value == "") ? DisabletxbCurrentWarehouse() : DisabletxbCurrentWarehouse();
                (txbCurrentWarehouse.value == "") ? StartCurrentWarehouse = false : StartCurrentWarehouse = true;

                txbCurrentLocation.value = MyObject.loor;
                (txbCurrentLocation.value.trim() == "" && MyObject.sloc == "1") ? EnabletxbCurrentLocation() : DisabletxbCurrentLocation();
                (txbCurrentLocation.value == "") ? StartCurrentLocation = false : StartCurrentLocation = true;


                (txbCurrentWarehouse.value == "") ? VerifyWarehouse() : "";

                //MyObject.CWDE  = ,
                //MyObject.LODE  = ,
                lblItemDescription.innerHTML = MyObject.dsca;
                lblQuatity.innerHTML = MyObject.qtdl;
                lblUnit.innerHTML = MyObject.cuni;
                //MyObject.DATE  = ,
                //MyObject.MESS  = ,
                //MyObject.USER = "JCUBILLOS";
                HidedivQueryAction();
                DisabletxPalletId();
                ShowdivTransferDetail();
                lblError.innerHTML = ""; 
            }
            else {
                lblError.innerHTML = MyObject.ErrorMsg; 
            }
        }

        var SuccesVerifyWarehouse = function (r) {
            if (r.d != undefined) {
                MyObjWarehoouse = JSON.parse(r.d);
                txbCurrentWarehouse.value = MyObjWarehoouse.CWAR.trim();
                localStorage.CurrentSloc = MyObjWarehoouse.SLOC.trim();
            }
            else {
            }
        }

        var SuccesTransferConsult = function (r) {

            MyObject = JSON.parse(r.d)

            if (MyObject.Success == true) {
                alert(MyObject.SuccessMsg);
                lblError.innerHTML = "";

                CleanPalletID();
                EnabletxPalletId();
                ShowdivQueryAction();
            }
            else if (MyObject.Error == true) {
                lblError.innerHTML = MyObject.ErrorMsg; 
            }

                
        }

        function IniciarElemntos() {

            //Div's
            divTransferHeader = document.getElementById("divTransferHeader");
            divQueryAction = document.getElementById("divQueryAction");
            divTransferDetail = document.getElementById("divTransferDetail");

            //Labels Titles                                                                          
            lblTitPalletId = document.getElementById("lblTitPalletId");
            lblTitItem = document.getElementById("lblTitItem");
            lblTitCurrentLocation = document.getElementById("lblTitCurrentLocation");
            lblTitQuality = document.getElementById("lblTitQuality");
            lblTitTargetLocation = document.getElementById("lblTitTargetLocation");

            //Labels Detail                                                                            
            lblItemId = document.getElementById("lblItemId");
            lblItemDescription = document.getElementById("lblItemDescription");
            lblQuatity = document.getElementById("lblQuatity");
            lblUnit = document.getElementById("lblUnit");

            lblError = document.getElementById("lblError");

            //Texbox                                                                            
            txPalletId = document.getElementById("txPalletId");
            txbCurrentWarehouse = document.getElementById("txbCurrentWarehouse");
            txbCurrentLocation  = document.getElementById("txbCurrentLocation");
            txbTargetWarehouse  = document.getElementById("txbTargetWarehouse");
            txbTargetLocation   = document.getElementById("txbTargetLocation");

            //Buttons                                                                             
            //btnCleanPallet = document.getElementById("btnCleanPallet");
            btnQuery = document.getElementById("btnQuery");
            btnTransfer = document.getElementById("btnTransfer");
            DisabletxbTargetLocation();
        }

        IniciarElemntos();

        function IniciarEventos() {

            //btnCleanPallet.addEventListener("click", CleanPalletID);
            btnQuery.addEventListener("click", SendPalletID);
            btnTransfer.addEventListener("click", SendTransfer);

            txbCurrentWarehouse.addEventListener("change", txbCurrentWarehousechange);
            txbTargetWarehouse.addEventListener("change", txbTargetWarehousechange);

            txbCurrentWarehouse.addEventListener("paste", txbCurrentWarehousechange);
            txbTargetWarehouse.addEventListener("paste", txbTargetWarehousechange);

            txbCurrentWarehouse.addEventListener("keyup", txbCurrentWarehousechange);
            txbTargetWarehouse.addEventListener("keyup", txbTargetWarehousechange);
        }
            
        // Disable/Enable textbox
        //Disable
        function DisabletxPalletId() {
            txPalletId.readOnly = true;
        }
        function DisabletxbCurrentWarehouse() {
            txbCurrentWarehouse.readOnly = true;
        }

        function DisabletxbCurrentLocation() {
            txbCurrentLocation.readOnly = true;
        }
        function DisabletxbTargetWarehouse() {
            txbTargetWarehouse.readOnly = true;
        }

        function DisabletxbTargetLocation() {
            txbTargetLocation.readOnly = true;
        }
        //Enable
        function EnabletxPalletId() {
            txPalletId.readOnly = false;
        }
        function EnabletxbCurrentWarehouse() {
            txbCurrentWarehouse.readOnly = false;
        }

        function EnabletxbCurrentLocation() {
            txbCurrentLocation.readOnly = false;
        }
        function EnabletxbTargetWarehouse() {
            txbTargetWarehouse.readOnly = false;
        }

        function EnabletxbTargetLocation() {
            txbTargetLocation.readOnly = false;
        }
        // Clean Elements
        function CleantxPalletId() {
            txPalletId.value = "";
        }
        function CleantxbCurrentWarehouse() {
            txbCurrentWarehouse.value = "";
        }
        function CleantxbCurrentLocation() {
            txbCurrentLocation.value = "";
        }
        function CleantxbTargetWarehouse() {
            txbTargetWarehouse.value = "";
        }
        function CleantxbTargetLocation() {
            txbTargetLocation.value = "";
        }
        function CleanlblItemId() {
            lblItemId.innerHTML = "-";
        }
        function CleanlblItemDescription() {
            lblItemDescription.innerHTML = "-";
        }
        function CleanlblQuatity() {
            lblQuatity.innerHTML = "-";
        }
        function CleanlblUnit() {
            lblUnit.innerHTML = "-";
        }
        // Hide/ show Divs
        //Hide
        function HidedivTransferHeader() {
            divTransferHeader.style.display = "none";
        }
        function HidedivQueryAction() {
            divQueryAction.style.display = "none";
        }
        function HidedivTransferDetail() {
            divTransferDetail.style.display = "none";
        }
        //Show
        function ShowdivTransferHeader() {
            divTransferHeader.style.display = "block";
        }
        function ShowdivQueryAction() {
            divQueryAction.style.display = "block";
        }
        function ShowdivTransferDetail() {
            divTransferDetail.style.display = "block";
        }
        // Hide/show buttons
        var SendPalletID = function () {

                var Data = "{'PAID':'" + txPalletId.value + "'}";
            sendAjax("clickQuery", Data, SuccesPalletConsult)
        }

        var VerifyWarehouse = function () {

            var Data = "{'LOCA':'" + txbCurrentLocation.value.trim() + "'}";
            sendAjax("VerifyWarehouse", Data, SuccesVerifyWarehouse)
        }

        var VerificarTipoWarehouse = function () {

            var Data = "{'WARE':'" + txbTargetWarehouse.value + "'}";
            sendAjax("VerificarTipoWarehouse", Data, SuccesVerificarTipoWarehouse)
        }

        var SuccesVerificarTipoWarehouse = function (r) {
            if (r.d != undefined) {
                MyObjWarehoouse = JSON.parse(r.d);
                if (MyObjWarehoouse.Error == true) {
                    lblError.innerHTML = MyObjWarehoouse.ErrorMsg;
                }
                else {
                }
            }
            else {
            }
        }

        var SendTransfer = function () {

            lstWarehouses = JSON.parse('<%=lstWarehouses %>');

            var lengthTargetWarehouse = txbTargetWarehouse.classList.length;
            var lengthCurrenttWarehouse = txbCurrentWarehouse.classList.length;

            var IncorrectTargetWarehouse = false;
            var IncorrectCurrenttWarehouse = false;
                
            for (var i = 0; i < lengthTargetWarehouse; i++) {
                if (txbTargetWarehouse.classList[i] == "IncorrectWarehouse") {
                    IncorrectTargetWarehouse = true;
                }
            }

            for (var j = 0; j < lengthCurrenttWarehouse; j++) {
                if (txbCurrentWarehouse.classList[j] == "IncorrectWarehouse") {
                    IncorrectCurrenttWarehouse = true;
                }
            }

            if (IncorrectTargetWarehouse == true) {
                lblError.innerHTML = "The Target Warehouse is invalid";
            }
            else if (IncorrectCurrenttWarehouse == true) {
                lblError.innerHTML = "The Current Warehouse is invalid";
            }

            else {
                if (txbTargetWarehouse.value.trim() == "") {
                    txbTargetWarehouse.focus();
                }
                else {
                    var Data = "{'PAID':'" + txPalletId.value.trim().toUpperCase() + "','CurrentWarehouse':'" + txbCurrentWarehouse.value.trim().toUpperCase() + "','CurrentSloc':'" + localStorage.CurrentSloc + "','CurrentLocation':'" + txbCurrentLocation.value.toUpperCase().trim() + "','TargetWarehouse':'" + txbTargetWarehouse.value.trim().toUpperCase() + "','TargetSloc':'" + (SLOC == true ? '1' : '0') + "','TargetLocation':'" + txbTargetLocation.value.toUpperCase().trim() + "','StartCurrentWarehouse':'" + StartCurrentWarehouse + "','StartCurrentLocation':'" + StartCurrentLocation + "'}";
                    sendAjax("clickTransfer", Data, SuccesTransferConsult);
                }
            }
        }


        var txbCurrentWarehousechange = function () {
            lstWarehouses = JSON.parse('<%=lstWarehouses %>');
            DisabletxbCurrentLocation();
            txbCurrentWarehouse.classList.add("IncorrectWarehouse");
            lstWarehouses.forEach(function (x) {
                if (x.CWAR.trim().toUpperCase() == txbCurrentWarehouse.value.trim().toUpperCase()) {
                    console.log("el warehouse existe");
                    txbCurrentWarehouse.classList.add("CorrectWarehouse");
                    txbCurrentWarehouse.classList.remove("IncorrectWarehouse");
                    lblError.innerHTML = "";
                    if (x.SLOC.trim().toUpperCase() == 1) {
                        EnabletxbCurrentLocation();
                    }

                }
            });
        };

        var txbTargetWarehousechange = function () {
            $('#txbTargetLocation').val("");
            lstWarehouses = JSON.parse('<%=lstWarehouses %>');
            DisabletxbTargetLocation();
            txbTargetWarehouse.classList.add("IncorrectWarehouse");
            lstWarehouses.forEach(function (x) {
                if (x.CWAR.trim().toUpperCase() == txbTargetWarehouse.value.trim().toUpperCase()) {
                    console.log("el warehouse existe");
                    txbTargetWarehouse.classList.add("CorrectWarehouse");
                    txbTargetWarehouse.classList.remove("IncorrectWarehouse");
                    lblError.innerHTML = "";
                    if (x.SLOC.trim().toUpperCase() == 1) {
                        EnabletxbTargetLocation();
                        txbTargetLocation.value = "";
                        SLOC = true;
                    }
                    else {
                        txbTargetLocation.value = "";
                        SLOC = false;
                    }
                    localStorage.TargetSloc = x.SLOC.trim();
                }
            });

        };

        var CleanPalletID = function () {

            CleantxPalletId();
            CleantxbCurrentWarehouse();
            CleantxbCurrentLocation();
            CleantxbTargetWarehouse();
            CleantxbTargetLocation();
            CleanlblItemId();
            CleanlblItemDescription();
            CleanlblQuatity();
            CleanlblUnit();

            txPalletId.readOnly = false;
            ShowdivQueryAction();
            ShowdivTransferDetail();
            HidedivTransferDetail();

        }
        IniciarEventos();
    </script>
    <script type="text/javascript">
        function DeshabilitartxbCurrentWarehouse() {
            txbCurrentWarehouse.readOnly = true;
        }

        function DeshabilitartxbtxbCurrentLocation() {
            txbCurrentLocation.readOnly = true;
        }

        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "whInvTransfers.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: asyncMode != undefined ? asyncMode : true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }

            lstWarehouses = JSON.parse('<%=lstWarehouses %>');
            console.log(lstWarehouses);

            HidedivTransferDetail();

    </script>
    <script src="styles//popper.min.js"></script>
    <script src="styles//bootstrap.min.js"></script>
    <script src="styles/jquery-3.1.1.min.js"></script>
</asp:content>
