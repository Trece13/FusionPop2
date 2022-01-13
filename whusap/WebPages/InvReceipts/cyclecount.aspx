<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="cyclecount .aspx.cs" Inherits="whusap.WebPages.InvReceipts.cyclecount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
<style>
        #lblError
        {
            font-size : 15px;
            color : Red;
        }
<%--       #txPalletID,#lblItemDsca
        {
           width: 98%; 
        }--%>
<%--        #txZoneCode
        {
           width: 98%; 
        }--%>
        #detail
        {
            display:none;
        }
        #divPallet
        {
            display:none;
        }
</style>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txPalletID">
            Zone Code</label>
        <div class="col-sm-4 form-inline">
            <div class="col-2 p-0">
                <input type="text" class="form-control form-control-lg col-12" id="txZoneCode" placeholder="Zone Code"/>
            </div>
            <div class="col-1 p-0">
            </div>
            <div class="col-2 p-0">
                <button type="button" class="btn btn-primary col-12" id="btnClearZone">
                    Reset
                </button>
            </div>
            <div class="col-1 p-0">
            </div>
            <label id="lblZoneCode"></label> 
        </div>  
    </div>           
    <div id="divPallet">
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txPalletID">
            Pallet ID</label>
        <div class="col-sm-4 form-inline">
            <div class="col-10 p-0">
                <input type="text" class="form-control form-control-lg col-12" id="txPalletID" placeholder="Pallet ID">
            </div>
            <div class="col-2 p-0">
                <button type="button" class="btn btn-danger col-12" id="btnClear">
                    Reset
                </button>
            </div>
        </div>
    </div>
    </div>
    <hr />
    <div id="detail">
        <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txPalletID">
            Item</label>
        <div class="col-sm-4">
            <input type="text" class="col-10 form-control form-control-lg" id="lblItem" placeholder="Item" disabled />
        </div>
        <label id="lblItemDsca" for="lblItem">
        </label>
        </div>
        <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txItem">
            Lot</label>
        <div class="col-sm-4">
            <input type="text" class="col-10 form-control form-control-lg" id="txLot" placeholder="Lot" disabled />
        </div>
        <label id="lblLot" for="txLot">
        </label>
    </div>
        <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txQuantity">
            Warehouse</label>
        <div class="col-sm-4">
            <input type="text" class="col-10 form-control form-control-lg" id="txWarehouse" placeholder="Warehouse" />
        </div>
        <label id="lblWarehouse" for="txWarehouse">
        </label>
        </div>
        <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txLocation">
            Location</label>
        <div class="col-sm-4">
            <input type="text" class=" col-10 form-control form-control-lg" id="txLocation" placeholder="Location" />
        </div>
    </div>
        <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txQuantity">
            Quantity</label>
        <div class="col-sm-4">
            <input type="number" class=" col-10 form-control form-control-lg" id="txQuantity" placeholder="Quantity" readonly />
        </div>
        <label id="lblQuantity" for="txQuantity">
        </label>
    </div>
        <div class="form-group row">
        <input id="btnSave" type="button" class="btn btn-primary btn-lg" value="SAVE" />
    </div>
    </div>
    <div class="form-group row">
        <label id="lblError">
        </label>
    </div>


    <script>
        var ktlc = ""; 
        function printDiv(divID) {

            var monthNames = [
                "1", "2", "3",
                "4", "5", "6", "7",
                "8", "9", "10",
                "11", "12"
              ];

            //PRINT LOCAL HOUR
            var d = new Date();
            var LbdDate = $("#LblDate");
            LbdDate.html(
                monthNames[d.getMonth()] +
                "/" +
                d.getDate() +
                "/" +
                d.getFullYear() +
                " " +
                d.getHours() +
                ":" +
                d.getMinutes() +
                ":" +
                d.getSeconds()
                );
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

            var mywindow = window.open('', 'PRINT', 'height=400,width=600');

            mywindow.document.write('<html><head><title>' + document.title + '</title>');
            mywindow.document.write('</head><body >');
            //mywindow.document.write('<h1>' + document.title + '</h1>');
            mywindow.document.write(document.getElementById(divID).innerHTML);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/

            mywindow.print();
            mywindow.close();

            return true;
        };

        var timer;
        function stoper() {

            clearTimeout(timer);
        }


        function IniciarComponentes() {

            txItem = $('#txItem');
            txLot = $('#txLot');
            txWarehouse = $('#txWarehouse');
            txLocation = $('#txLocation');
            txQuantity = $('#txQuantity');

            lblItem = $('#lblItem');
            lblWarehouse = $('#lblWarehouse');
            lblQuantity = $('#lblQuantity');

            btnSave = $('#btnSave');

        };

        IniciarComponentes();


        function BloquearComponentes() {

            //$('#txItem').prop("disabled", true); 
            $('#txLot').prop("disabled", true);
            $('#txWarehouse').prop("disabled", true);
            $('#txLocation').prop("disabled", true);
            $('#txQuantity').prop("disabled", false);
            $('#btnSave').prop("disabled", false);

        };
        BloquearComponentes();

        var SuccesVerificarZoneCode = function (r) {
            debugger
            $('#txWarehouse').val("");
            var MyObj = JSON.parse(r.d);
            if (MyObj.error == true) {
                ImprimirMensaje(MyObj.typeMsgJs, MyObj.errorMsg);
                //                $('#lblWorkOrder').html("");
                $("#lblZoneCode").html("");
                $('#txPalletID').val("");
                $('#lblItem').val("");
                $('#lblItemDsca').html("");
                $('#txLot').val("");
                $('#txWarehouse').val("");
                $('#txLocation').val("");
                $('#txQuantity').val("");
                $('#lblQuantity').html("");
                $('#txWarehouse').prop("disabled", true);
                $('#txLocation').prop("disabled", true);
                $('#txQuantity').prop("disabled", false);
                $('#txPalletID').prop("disabled", true);
                $('#detail').hide(1000);
                $('#divPallet').hide(1000);
            }
            else {
                //$('#lblWorkOrder').html(MyObj.PAID);
                $("#lblZoneCode").html(MyObj.DSCA.trim());
                $('#txZoneCode').prop("disabled", true);
                $('#txPalletID').val("");
                $('#txPalletID').prop("disabled", false);
                $('#lblItem').val(MyObj.ITEM);
                $('#lblItemDsca').html(MyObj.DESCRIPTION);
                $('#txLot').val(MyObj.CLOT);
                $('#txWarehouse').val(MyObj.CWAR);
                $('#lblWarehouse').html(MyObj.DESCWRH);
                $('#txWarehouse').prop("disabled", true);
                $('#txQuantity').val(MyObj.QTY);
                $('#lblQuantity').html(MyObj.UN)
                $('#txLocation').val(MyObj.LOCA);
                $('#lblError').html("");
                $('#detail').hide(1000);
                $('#divPallet').show(1000);
            }
        };

        var SuccesVerificarPalletID = function (r) {
        debugger
            var MyObj = JSON.parse(r.d);
            if (MyObj.error == true) {
                ImprimirMensaje(MyObj.typeMsgJs, MyObj.errorMsg);
                //                $('#lblWorkOrder').html("");
                $('#lblItem').val("");
                $('#lblItemDsca').html("");
                $('#txLot').val("");
                $('#txWarehouse').val("");
                $('#txLocation').val("");
                $('#txQuantity').val("");
                $('#lblQuantity').html("");
                $('#txWarehouse').prop("disabled", true);
                $('#txLocation').prop("disabled", true);
                $('#txQuantity').prop("disabled", false);
                $('#txPalletID').prop("disabled", false);
                $('#detail').hide(1000);
            }
            else {
                //$('#lblWorkOrder').html(MyObj.PAID);
                $('#txPalletID').prop("disabled", true);
                $('#lblItem').val(MyObj.ITEM);
                $('#lblItemDsca').html(MyObj.DESCRIPTION);
                $('#txWarehouse').val(MyObj.WRH);
                $('#lblWarehouse').html(MyObj.DESCWRH);
                $('#txWarehouse').prop("disabled", false);
                $('#txQuantity').val(MyObj.QTY);
                $('#lblQuantity').html(MyObj.UN)
                $('#txLocation').val(MyObj.LOCA);
                if (MyObj.KLTC == "1") {
                    $('#txLot').prop("disabled", false);
                    $('#txLot').val(MyObj.LOT);
                }
                else {
                    $('#txLot').prop("disabled", true);
                    $('#txLot').val("");
                    $('#txWarehouse').prop("disabled", false);
                    $('#txLocation').prop("disabled", true);
                    $('#txQuantity').prop("disabled", false);
                }
                if (MyObj.SLOC == "1") {
                    $('#txLocation').prop("disabled", false);
                    $('#txQuantity').prop("disabled", false);
                    //$('#btnSave').prop("disabled", true);
                }
                else if (MyObj.SLOC != "1") {
                    $('#txLocation').prop("disabled", true);
                    $('#txQuantity').prop("disabled", false);
                    $('#btnSave').prop("disabled", false);
                }
                $('#lblError').html("");
                $('#detail').show(1000);
            }
        };

        $('#btnClear').bind('click', function () {
            $('#detail').hide(1000);
            $('#txPalletID').val("");
            $('#lblItem').val("");
            $('#lblItemDsca').html("");
            $('#txLot').val("");
            $('#txWarehouse').val("");
            $('#txLocation').val("");
            $('#txQuantity').val("");
            $('#lblQuantity').html("");
            $('#lblError').html("");
            $('#txWarehouse').prop("disabled", true);
            $('#txLocation').prop("disabled", true);
            $('#txQuantity').prop("disabled", true);
            $('#txPalletID').prop("disabled", false);
        });

        $('#btnClearZone').bind('click', function () {
            $("#lblZoneCode").html("");
            $('#divPallet').hide(1000);
            $('#detail').hide(1000);
            $('#txPalletID').val("");
            $('#lblItem').val("");
            $('#lblItemDsca').html("");
            $('#txLot').val("");
            $('#txWarehouse').val("");
            $('#txLocation').val("");
            $('#txQuantity').val("");
            $('#lblQuantity').html("");
            $('#txZoneCode').val("");
            $('#lblError').html("");
            $('#txWarehouse').prop("disabled", true);
            $('#txLocation').prop("disabled", true);
            $('#txQuantity').prop("disabled", false);
            $('#txPalletID').prop("disabled", true);
            $('#txZoneCode').prop("disabled", false);
        });

        var SuccesVerificarItem = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.error == true) {
                ImprimirMensaje(MyObj.typeMsgJs, MyObj.errorMsg);
                BloquearComponentes();
            }
            if (MyObj.error == false) {
                $('#lblError').html("");
                lblItem.val(MyObj.dsca);
                lblItem.html(MyObj.dsca);
                lblQuantity.html(MyObj.cuni);
                ktlc = MyObj.kltc;
                if (MyObj.kltc == "1") {
                    $('#txLot').prop("disabled", false);
                    lblLot.html(MyObj.clot);
                }
                else {
                    $('#txLot').prop("disabled", true);
                    $('#txWarehouse').prop("disabled", false);
                    $('#txLocation').prop("disabled", true);
                    $('#txQuantity').prop("disabled", false);
                }
            }

        }

        var SuccesVerificarLote = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.error == true) {
                ImprimirMensaje(MyObj.typeMsgJs, MyObj.errorMsg);
                $('#txWarehouse').prop("disabled", true);
                $('#btnSave').prop("disabled", true);
            }
            if (MyObj.error == false) {
                $('#lblError').html("");
                $('#txWarehouse').prop("disabled", false);
                $('#txLocation').prop("disabled", false);
                $('#txQuantity').prop("disabled", false);
                $('#btnSave').prop("disabled", false);
            }
            else {
                $('#txWarehouse').prop("disabled", true);
                $('#txLocation').prop("disabled", true);
                $('#txQuantity').prop("disabled", false);
                $('#btnSave').prop("disabled", true);
            }
        }

        var SuccesVerificarWarehouse = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.error == true) {
                ImprimirMensaje(MyObj.typeMsgJs, MyObj.errorMsg);
                $('#txLocation').prop("disabled", true);
                $('#txQuantity').prop("disabled", false);
                //$('#txLocation').val("");
                //$('#txQuantity').val("");
                $('#btnSave').prop("disabled", true);
            }
            if (MyObj.error == false) {
                $('#lblError').html("");
                //$('#txLocation').val("");
                //$('#txQuantity').val("");
                $('#btnSave').prop("disabled", true);
                lblWarehouse.html(MyObj.dsca)
                if (MyObj.sloc == "1") {
                    $('#txLocation').prop("disabled", false);
                    $('#txQuantity').prop("disabled", false);
                    //$('#btnSave').prop("disabled", true);
                }
                else {
                    $('#txQuantity').prop("disabled", false);
                    $('#btnSave').prop("disabled", false);
                    $('#txLocation').val("");
                    $('#txLocation').prop("disabled", true);
                }
            }
        }

        var SuccesVerificarLocation = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.error == true) {
                ImprimirMensaje(MyObj.typeMsgJs, MyObj.errorMsg);
                $('#txQuantity').prop("disabled", false);
                $('#btnSave').prop("disabled", true);
            }
            if (MyObj.error == false) {
                $('#lblError').html("");
                if ($('#txQuantity').val().trim() == "") {
                    //$('#btnSave').prop("disabled", true);
                    $('#txQuantity').prop("disabled", false);
                }
                else {
                    $('#btnSave').prop("disabled", false);
                    $('#txQuantity').prop("disabled", false);

                }
                
            }
        }

        var SuccesVerificarQuantity = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.error == true) {
                ImprimirMensaje(MyObj.typeMsgJs, MyObj.errorMsg);
            }
            if (MyObj.error == false) {
                $('#lblError').html("");
                if ($('#txQuantity').val() > 0) {
                    $('#btnSave').prop("disabled", false);
                }
                else {
                    //$('#btnSave').prop("disabled", true);
                    ImprimirMensaje(MyObj.typeMsgJs, MyObj.SuccessMsg);
                }
            }
        }


        function ImprimirMensaje(type, msg) {
            switch (type) {
                case "alert":
                    alert(msg);
                    break;
                case "console":
                    console.log(msg);
                    break;
                case "label":
                    $('#lblError').html(msg);
                    break;
            }
        }
        var SuccesClick_Save = function (r) {
            MyObject = JSON.parse(r.d);

            if (MyObject.error == false) {
                ////                $('#txPalletID').val("");
                ////                //$('#lblWorkOrder').html("");
                ////                $('#lblItem').val("");
                ////                $('#lblItemDsca').html("");
                ////                $('#txWarehouse').val("");
                ////                $('#txLocation').val("");
                ////                $('#txQuantity').val("");
                ////                $('#lblQuantity').html("");
                ////                $('#btnSave').prop("disabled", true);
                ////                $('#detail').hide();
                ////                $('#txZoneCode').val("");
                ////                $('#lblZoneCode').html("");
                ////                $('#txPalletID').val("");
                ////                $('#btnSave').prop("disabled", true);
                ////                $('#txZoneCode').prop("disabled", false);
                ////                $('#divPallet').hide(500);
                $('#btnClear').click();
                $('#lblError').html("");
                ImprimirMensaje(MyObject.typeMsgJs, MyObject.SuccessMsg);
                $('#txPalletID').focus();
            }
            else {
                $('#lblError').html("");
                ImprimirMensaje(MyObject.typeMsgJs, MyObject.SuccessMsg);
                $('#txPalletID').focus();
            }

        }

        var VerificarZoneCode = function () {
            //$('#btnSave').prop("disabled", true);
            var Data = "{'ZONE':'" + $('#txZoneCode').val().trim() + "'}";
            sendAjax("VerificarZoneCode", Data, SuccesVerificarZoneCode)
        }

        var VerificarPalletID = function () {
            //$('#btnSave').prop("disabled", true);
            var Data = "{'PAID':'" + $('#txPalletID').val().trim() + "','ZONE':'" + $('#txZoneCode').val().trim() + "'}";
            sendAjax("VerificarPalletID", Data, SuccesVerificarPalletID)
        }


        var VerificarItem = function () {
            //$('#btnSave').prop("disabled", true);
            var Data = "{'ITEM':'" + $('#txItem').val() + "'}";
            sendAjax("VerificarItem", Data, SuccesVerificarItem)
        }

        var VerificarLote = function () {
            //$('#btnSave').prop("disabled", true);
            var Data = "{'ITEM':'" + $('#lblItem').val() + "','CLOT':'" + $('#txLot').val() + "'}";
            sendAjax("VerificarLote", Data, SuccesVerificarLote)
        }

        var VerificarWarehouse = function () {
            //$('#btnSave').prop("disabled", true);
            var Data = "{'CWAR':'" + $('#txWarehouse').val() + "'}";
            sendAjax("VerificarWarehouse", Data, SuccesVerificarWarehouse)
        }

        var VerificarLocation = function () {
            //$('#btnSave').prop("disabled", true);
            var Data = "{'CWAR':'" + $('#txWarehouse').val() + "','LOCA':'" + $('#txLocation').val() + "'}";
            sendAjax("VerificarLocation", Data, SuccesVerificarLocation)
        }

        var VerificarQuantity = function () {
            //$('#btnSave').prop("disabled", true);
            var Data = "{'QTY':'" + $('#txQuantity').val() + "','CWAR':'" + $('#txWarehouse').val() + "','ITEM':'" + $('#lblItem').val() + "','LOCA':'" + $('#txLocation').val() + "','CLOT':'" + (ktlc == "1" ? $('#lblWorkOrder').html() : " ") + "'}";
            sendAjax("VerificarQuantity", Data, SuccesVerificarQuantity)
        }

        var Click_Save = function () {

            var Data = "{'PAID':'" + $('#txPalletID').val().trim() + "','ITEM':'" + $('#lblItem').val() + "','CWAR':'" + $('#txWarehouse').val() + "','LOCA':'" + $('#txLocation').val() + "','UNIT':'" + $('#lblQuantity').html() + "','QTYS':'" + $('#txQuantity').val() + "','CLOT':'" + $('#txLot').val() + "','ZONE':'" + $('#txZoneCode').val().trim() + "','USER':'" + GetUserUrl() + "'}";
            sendAjax("Click_Save", Data, SuccesClick_Save);

        }

        var GetUserUrl = function () {
            var valor1 = window.location.href.toString().substring(window.location.href.toString().toLowerCase().indexOf("valor1"), window.location.href.toString().toLowerCase().indexOf("valor2") - 1);
            return valor1.substring(valor1.indexOf("=") + 1);
        }

        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "InventoryTakingByPalletIDWarehousesNotControlledByLocation.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }


        $('#txPalletID').bind('paste keyup', function () {

                if ($('#txPalletID').val().trim() == "") {
                return;
            }

            stoper();
            timer = setTimeout("VerificarPalletID()", 500);
        });

        $('#txZoneCode').bind('paste keyup', function () {
                if ($('#txZoneCode').val().trim() == "") {
                return;
            }

            stoper();
            timer = setTimeout("VerificarZoneCode()", 500);
        });

        txLot.bind('paste keyup', function () {
            $('#txWarehouse').prop("disabled", true);
            $('#txLocation').prop("disabled", true);
            $('#txQuantity').prop("disabled", false);
            //$('#txWarehouse').val("");
            //$('#txLocation').val("");
            //$('#txQuantity').val("");
            //$('#btnSave').prop("disabled", true);

            if ($('#txLot').val().trim() == "") {
                return;
            }
            stoper();
            timer = setTimeout("VerificarLote()", 1000);
        });

        txWarehouse.bind('paste keyup', function () {

            $('#txLocation').prop("disabled", true);
            $('#txQuantity').prop("disabled", false);
            //$('#txLocation').val("");
            //$('#txQuantity').val("");
            //$('#btnSave').prop("disabled", true);

            if ($('#txWarehouse').val().trim() == "") {
                return;
            }
            stoper();
            timer = setTimeout("VerificarWarehouse()", 1000);
        });

        txLocation.bind('paste keyup', function () {

            $('#txQuantity').prop("disabled", false);
            //$('#txQuantity').val("");
            //$('#btnSave').prop("disabled", true);

            if ($('#txLocation').val().trim() == "") {
                return;
            }
            stoper();
            timer = setTimeout("VerificarLocation()", 1000);
        });

        txQuantity.bind('paste keyup', function () {
            //$('#btnSave').prop("disabled", true);
            if ($('#txQuantity').val().trim() == "") {
                return;
            }
            stoper();
            timer = setTimeout("VerificarQuantity()", 1000);
        });

        btnSave.bind('click', function () {
            Click_Save();
        });

    </script>
    <script src="styles/popper.min.js"></script>
    <script src="styles/bootstrap.min.js"></script>
    <script src="styles/jquery-3.1.1.min.js"></script>
</asp:Content>
