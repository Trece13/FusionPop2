<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvTransfersNew.aspx.cs" Inherits="whusap.WebPages.InvFloor.whInvTransfersNew" %>
<asp:content id="Content1" contentplaceholderid="Encabezado" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="Contenido" runat="server">
    <link rel="stylesheet" href="styles/bootstrap.min.css">
    <link rel="stylesheet" href="styles/font-awesome.min.css">

    <style>
        .CorrectWarehouse {
            border-bottom: solid 3px green;
        }

        .IncorrectWarehouse {
            border-bottom: solid 3px red;
        }

        .ContenidoOculto {
            display: none;
        }
        .input{
            font-size:15px;
        }
    </style>

    <form id="form1" class="container" style="display:none">
    <div id="divTransferHeader" style="display: none;">
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
        <input type="button" class="btn btn-primary btn-lg col-sm-6" id="" value="Transfer" />
    </div>

    </div>
    <label id="lblError" style="font-size:24px; color:red" >
    </label>
    <table class="table" id="TblPallets" style="margin-bottom:100px">
  <thead>
    <tr>
      <th scope="col">#</th>
      <th scope="col">Pallet</th>
      <th scope="col">Item</th>
      <th scope="col"></th>
      <th scope="col">Current Location</th>
      <th scope="col"></th>
      <th scope="col">Quantity</th>
      <th scope="col"></th>
      <th scope="col">Target Location</th>
      <th scope="col"></th>
      <th scope="col"></th>
      <th scope="col"></th>
    </tr>
  </thead>
  <tbody>
  </tbody>
</table>
    
    <script  type="text/javascript">
        var SLOC = false;
        var SuccesPalletConsult = function (r) {
            MyObject = JSON.parse(r.d)
            if (MyObject.Error == false) {
                MyObject.sqnb;
                lblItemId.innerHTML = MyObject.mitm;
                document.getElementById("txbCurrentWarehouse"+ MyObject.row).value = MyObject.cwor;
                (document.getElementById("txbCurrentWarehouse"+ MyObject.row).value == "") ? DisabletxbCurrentWarehouse() : DisabletxbCurrentWarehouse();
                (document.getElementById("txbCurrentWarehouse"+ MyObject.row).value == "") ? document.getElementById("txbCurrentWarehouse"+ MyObject.row).setAttribute("StartCurrentWarehouse",false) : document.getElementById("txbCurrentWarehouse"+ MyObject.row).setAttribute("StartCurrentWarehouse",true);
                document.getElementById("txbCurrentLocation"+ MyObject.row).value = MyObject.loor;
                (document.getElementById("txbCurrentLocation"+ MyObject.row).value.trim() == "" && MyObject.sloc == "1") ? EnabletxbCurrentLocation() : DisabletxbCurrentLocation();
                (document.getElementById("txbCurrentLocation" + MyObject.row).value == "") ? StartCurrentLocation = false : StartCurrentLocation = true;
                (document.getElementById("txbCurrentWarehouse" + MyObject.row).value == "") ? VerifyWarehouse() : "";
                document.getElementById("txbCurrentWarehouse" + MyObject.row).setAttribute('sloc', MyObject.sloc);
                document.getElementById("lblItemId" + MyObject.row).innerHTML =  MyObject.mitm;
                document.getElementById("lblItemDescription" + MyObject.row).innerHTML = MyObject.dsca;
                document.getElementById("lblQuatity" + MyObject.row).innerHTML = MyObject.qtdl;
                document.getElementById("lblUnit" + MyObject.row).innerHTML = MyObject.cuni;
                document.getElementById("txbTargetWarehouse" + MyObject.row).disabled = false
                document.getElementById("lblError" + MyObject.row).innerHTML = "";
            }
            else {
                document.getElementById("lblError" + MyObject.row).innerHTML = MyObject.ErrorMsg;
                CleanPalletID(MyObject.row,true);
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
                document.getElementById("lblError" + MyObject.row).innerHTML = "";
                CleanPalletID(MyObject.row);
            }
            else if (MyObject.Error == true) {
                document.getElementById("lblError" + MyObject.row).innerHTML = MyObject.ErrorMsg;
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
            txbCurrentLocation = document.getElementById("txbCurrentLocation");
            txbTargetWarehouse = document.getElementById("txbTargetWarehouse");
            txbTargetLocation = document.getElementById("txbTargetLocation");

            //Buttons                                                                             
            //btnCleanPallet = document.getElementById("btnCleanPallet");
            btnQuery = document.getElementById("btnQuery");
            //btnTransfer = document.getElementById("btnTransfer");
            DisabletxbTargetLocation();


            TblPallets = document.getElementById("TblPallets");
        }

        IniciarElemntos();

        function IniciarEventos() {

            //btnCleanPallet.addEventListener("click", CleanPalletID);
            btnQuery.addEventListener("click", SendPalletID);
            //btnTransfer.addEventListener("click", SendTransfer);

            txbCurrentWarehouse.addEventListener("change", txbCurrentWarehousechange);
            txbTargetWarehouse.addEventListener("change", txbTargetWarehousechange);

            txbCurrentWarehouse.addEventListener("paste", txbCurrentWarehousechange);
            txbTargetWarehouse.addEventListener("paste", txbTargetWarehousechange);

            txbCurrentWarehouse.addEventListener("keyup", txbCurrentWarehousechange);
            txbTargetWarehouse.addEventListener("keyup", txbTargetWarehousechange);
        }

        // Disable/Enable textbox
        //Disable
        //function DisabletxPalletId() {
        //    txPalletId.readOnly = true;
        //}
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
        //function EnabletxPalletId() {
        //    txPalletId.readOnly = false;
        //}
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
        function CleantxPalletId(row) {
            document.getElementById("txPalletID"+row).value = "";
        }
        function CleantxbCurrentWarehouse(row) {
            document.getElementById("txbCurrentWarehouse"+ row).value = "";
        }
        function CleantxbCurrentLocation(row) {
            document.getElementById("txbCurrentLocation"+ row).value = "";
        }
        function CleantxbTargetWarehouse(row) {
            document.getElementById("txbTargetWarehouse"+ row).value = "";
        }
        function CleantxbTargetLocation(row) {
            document.getElementById("txbTargetLocation"+ row).value = "";
        }
        function CleanlblItemId(row) {
            document.getElementById("lblItemId"+ row).innerHTML = "-";
        }
        function CleanlblItemDescription(row) {
            document.getElementById("lblItemDescription"+ row).innerHTML = "-";
        }
        function CleanlblQuatity(row) {
            document.getElementById("lblQuatity"+ row).innerHTML = "-";
        }
        function CleanlblUnit(row) {
            document.getElementById("lblUnit" + row).innerHTML = "-";
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
        var SendPalletID = function (row) {

            var Data = "{'PAID':'" + document.getElementById("txPalletID" + row).value + "','ROW':'" + row + "'}";
            sendAjax("clickQuery", Data, SuccesPalletConsult)
        }

        var VerifyWarehouse = function () {

            var Data = "{'LOCA':'" + txbCurrentLocation.value.trim() + "'}";
            sendAjax("VerifyWarehouse", Data, SuccesVerifyWarehouse)
        }

        var VerificarTipoWarehouse = function (row) {

            var Data = "{'WARE':'" + document.getElementById("txbTargetWarehouse"+row).value + "','ROW':'" + row + "'}";
            sendAjax("VerificarTipoWarehouse", Data, SuccesVerificarTipoWarehouse)
        }

        var VerificarLocation = function (row) {

            var Data = "{'CWAR':'" + document.getElementById("txbTargetWarehouse" + row).value + "','LOCA':'" + document.getElementById("txbTargetLocation" + row).value + "','ROW':'" + row + "'}";
            sendAjax("VerificarLocation", Data, SuccesVerificarLocation)
        }

        var SuccesVerificarLocation = function (r) {
            if (r.d != undefined) {
                MyObjWarehoouse = JSON.parse(r.d);
                if (MyObjWarehoouse.Error == true) {
                    document.getElementById("lblError" + MyObjWarehoouse.row).innerHTML = MyObjWarehoouse.ErrorMsg;
                    document.getElementById("txPalletID"+ MyObjWarehoouse.row).setAttribute("valid", false);
                }
                else {
                    if (document.getElementById("txbCurrentWarehouse" + MyObjWarehoouse.row).value.trim() === document.getElementById("txbTargetWarehouse" + MyObjWarehoouse.row).value.trim() && document.getElementById("txbTargetLocation" + MyObjWarehoouse.row).value.trim() === document.getElementById("txbCurrentLocation" + MyObjWarehoouse.row).value.trim()) {
                        document.getElementById("txPalletID" + MyObjWarehoouse.row).setAttribute("valid", false);
                        document.getElementById("lblError" + MyObjWarehoouse.row).innerHTML = "Location cannot be the same";
                    }
                    else {
                        document.getElementById("txPalletID" + MyObjWarehoouse.row).setAttribute("valid", true);
                        document.getElementById("lblError" + MyObjWarehoouse.row).innerHTML = "";
                    }
                }
            }
            else {
            }
        }

        var SuccesVerificarTipoWarehouse = function (r) {
            if (r.d != undefined) {
                MyObjWarehoouse = JSON.parse(r.d);
                if (MyObjWarehoouse.Error == true) {
                    document.getElementById("lblError" + MyObjWarehoouse.Row).innerHTML = MyObjWarehoouse.ErrorMsg;
                    document.getElementById("txbTargetLocation" + MyObjWarehoouse.Row).value = "";
                    document.getElementById("txbTargetLocation" + MyObjWarehoouse.Row).disabled = true;
                    document.getElementById("txPalletID" + MyObjWarehoouse.Row).setAttribute("valid", false);
                }
                else {
                    $('#lblError').html("");
                    if (MyObjWarehoouse.sloc == "1") {
                        document.getElementById("txbTargetLocation" + MyObjWarehoouse.Row).value = "";
                        document.getElementById("txbTargetLocation" + MyObjWarehoouse.Row).disabled = false;
                        document.getElementById("txPalletID" + MyObjWarehoouse.Row).setAttribute("valid", false);
                        document.getElementById("txbTargetWarehouse" + MyObjWarehoouse.Row).setAttribute("sloc", "1");
                        document.getElementById("lblError" + MyObjWarehoouse.Row).innerHTML = "";
                    }
                    else {
                        document.getElementById("txbTargetLocation" + MyObjWarehoouse.Row).value = "";
                        document.getElementById("txbTargetLocation" + MyObjWarehoouse.Row).disabled = true;
                        document.getElementById("txbTargetWarehouse" + MyObjWarehoouse.Row).setAttribute("sloc", "2");
                        if (document.getElementById("txbTargetWarehouse" + MyObjWarehoouse.Row).value.trim() === document.getElementById("txbCurrentWarehouse" + MyObjWarehoouse.Row).value.trim()) {
                            document.getElementById("txPalletID" + MyObjWarehoouse.Row).setAttribute("valid", false);
                            document.getElementById("lblError" + MyObjWarehoouse.Row).innerHTML = "Location cannot be the same";
                        }
                        else {
                            document.getElementById("txPalletID" + MyObjWarehoouse.Row).setAttribute("valid", true);
                            document.getElementById("lblError" + MyObjWarehoouse.Row).innerHTML = "";
                        }
                    }
                }
            }
            else {
            }
        }

        var SendTransfer = function () {
            for(i = 1 ; i < document.getElementById("TblPallets").rows.length; i++){
                if (document.getElementById("TblPallets").rows[i].cells[1].children[0].attributes["valid"].value == "true") {
                    var Data = "{'PAID':'" + document.getElementById("txPalletID" + (i - 1)).value.trim().toUpperCase() + "','CurrentWarehouse':'" + document.getElementById("txbCurrentWarehouse" + (i - 1)).value.trim().toUpperCase() + "','CurrentSloc':'" + document.getElementById("txbCurrentWarehouse" + (i - 1)).getAttribute("sloc") + "','CurrentLocation':'" + document.getElementById("txbCurrentLocation" + (i - 1)).value.toUpperCase().trim() + "','TargetWarehouse':'" + document.getElementById("txbTargetWarehouse" + (i - 1)).value.trim().toUpperCase() + "','TargetSloc':'" + document.getElementById("txbTargetWarehouse" + (i - 1)).getAttribute("sloc") + "','TargetLocation':'" + document.getElementById("txbTargetLocation" + (i - 1)).value.toUpperCase().trim() + "','row':'"+(i-1)+"','StartCurrentWarehouse':'" + document.getElementById("txbCurrentWarehouse" + (i - 1)).getAttribute("StartCurrentWarehouse") + "'}";
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

        var CleanPalletID = function (row,error) {

            //CleantxPalletId(row);
            CleantxbCurrentWarehouse(row);
            CleantxbCurrentLocation(row);
            CleantxbTargetWarehouse(row);
            CleantxbTargetLocation(row);
            CleanlblItemId(row);
            CleanlblItemDescription(row);
            CleanlblQuatity(row);
            CleanlblUnit(row);
            document.getElementById("txbTargetWarehouse" + row).disabled = true;
            document.getElementById("txbTargetLocation" + row).disabled = true;
            document.getElementById("txPalletID" + row).setAttribute("valid", false);
            error == true? "":document.getElementById("txPalletID" + row).value = "";            
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
                url: "whInvTransfersNew.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }
        lstWarehouses = JSON.parse('<%=lstWarehouses %>');
        var MyTableTransfer = document.getElementById("TblPallets");
        console.log(lstWarehouses);
        HidedivTransferDetail();
        var CargarRegistros = function (cantidad) {
            cantidad = 20;
            for(i = 0; i < cantidad; i++){
                MyTableTransfer.insertRow(-1).innerHTML = "<th scope='row'>" + (i + 1) + "</th>" +
                "<td><input type='text' row = " + i + "  valid = false class='form-control form-control-lg col-12 input' id='txPalletID" + i + "' placeholder='Pallet ID' oninput='SendPalletID(" + i + ")'>" +
                "<label id='lblError"+i+"' style='font-size:15px; color:red'></label></td>" +
                "<td><label class='col-12 col-form-label' style='font-size:small' id='lblItemId" + i + "' >-</label></td>" +
                "<td><label class='col-12 col-form-label' style='font-size:small' id='lblItemDescription" + i + "' >-</label></td>" +
                "<td><input type='text' class='form-control form-control-lg col-sm-12 input' id='txbCurrentWarehouse" + i + "' placeholder='Warehouse' disabled/></td>" +
                "<td><input type='text' class='form-control form-control-lg col-sm-12 input' id='txbCurrentLocation" + i + "' placeholder='Locate' disabled/></td>" +
                "<td><label class='col-9 col-form-label-lg' id ='lblQuatity" + i + "' >-</label></td>" +
                "<td><label class='col-2 col-form-label-lg' id ='lblUnit" + i + "' >-</label></td>" +
                "<td><input type='text' class='form-control form-control-lg col-sm-12 input' id='txbTargetWarehouse" + i + "' placeholder='Warehouse' oninput='VerificarTipoWarehouse(" + i + ")' disabled/></td>" +
                "<td><input type='text' class='form-control form-control-lg col-sm-12 input' id='txbTargetLocation" + i + "' placeholder='Locate' oninput='VerificarLocation(" + i + ")' disabled/></td>" +
                "<td><input type='button' class='btn btn-lg btn-success col-12 input'  onclick='CleanPalletID(" + i + "); return false;' value='Change Pallet ID'></button></td>" +
                "<td><input type='button' class='btn btn-lg btn-primary col-12 input' id='btnTransfer" + i + "' onclick='CleanPalletID(" + i + "); return false;' value='Transfer' style='display:none'></button></td>" +
                (i == 0 ? "<td><input type='button' class='btn btn-primary btn-lg col-sm-1' id='btnTransfer' value='Transfer' style='position:fixed' onclick='SendTransfer()'/></td>" : "<td></td>");
            }
        }
        CargarRegistros();
    </script>
    <script src="styles//popper.min.js"></script>
    <script src="styles//bootstrap.min.js"></script>
    <script src="styles/jquery-3.1.1.min.js"></script>
</asp:content>
