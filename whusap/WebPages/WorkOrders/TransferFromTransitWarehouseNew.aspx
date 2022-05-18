<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="TransferFromTransitWarehouseNew.aspx.cs" Inherits="whusap.WebPages.WorkOrders.TransferFromTransitWarehouseNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style>
        #lblError {
            font-size: 15px;
            color: Red;
        }

        #txPalletID {
            width: 98%;
        }

        #detail {
            display: none;
        }

        #MyDynamicEtiqueta {
            margin-top: 0;
            margin-bottom: 0;
            padding-top: 0;
            padding-bottom: 0;
            display: none;
        }

        .form-group {
            margin-bottom: 1.5rem;
        }

        .lds-ellipsis {
            display: inline-block;
            position: relative;
            width: 80px;
            height: 80px;
        }

            .lds-ellipsis div {
                position: absolute;
                top: 33px;
                width: 13px;
                height: 13px;
                border-radius: 50%;
                background: blue;
                animation-timing-function: cubic-bezier(0, 1, 1, 0);
            }

                .lds-ellipsis div:nth-child(1) {
                    left: 8px;
                    animation: lds-ellipsis1 0.6s infinite;
                }

                .lds-ellipsis div:nth-child(2) {
                    left: 8px;
                    animation: lds-ellipsis2 0.6s infinite;
                }

                .lds-ellipsis div:nth-child(3) {
                    left: 32px;
                    animation: lds-ellipsis2 0.6s infinite;
                }

                .lds-ellipsis div:nth-child(4) {
                    left: 56px;
                    animation: lds-ellipsis3 0.6s infinite;
                }

        @keyframes lds-ellipsis1 {
            0% {
                transform: scale(0);
            }

            100% {
                transform: scale(1);
            }
        }

        @keyframes lds-ellipsis3 {
            0% {
                transform: scale(1);
            }

            100% {
                transform: scale(0);
            }
        }

        @keyframes lds-ellipsis2 {
            0% {
                transform: translate(0, 0);
            }

            100% {
                transform: translate(24px, 0);
            }
        }
    </style>
    <div class="form-group row" style="display: contents">
        <div class="col-sm-4 form-inline">
            <div class="col-8 p-0">
                <input type="text" class="form-control form-control-lg col-12" id="txPalletID" placeholder="Pallet ID">
            </div>
            <div class="col-2 p-0">
                <input type="button" class="btn btn-primary col-12" id="btnClear" value="Reset">
            </div>
            <div class="col-2 p-0">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <div class="lds-ellipsis" id="loader" style="display: none;height: 41px !important">
                    <div></div>
                    <div></div>
                    <div></div>
                    <div></div>
                </div>

            </div>
        </div>
    </div>
    <br />
    <div class="form-group row" style=" display: contents">
        <div id="detail" class="col-10">
        <div class="form-group row">
            <label class="col-sm-2 col-form-label-lg" for="txPalletID">
                Item</label>
            <div class="col-sm-4">
                <input type="text" class="form-control form-control-lg" id="lblItem" placeholder="Item" disabled>
            </div>
            <label id="lblItemDsca">
            </label>
        </div>
        <div class="form-group row">
            <label class="col-sm-2 col-form-label-lg" for="txQuantity">
                Current Location</label>
            <div class="col-sm-2">
                <input type="text" class="form-control form-control-lg" id="txWarehouseCrrnt" placeholder="Warehouse">
            </div>
            <div class="col-sm-2">
                <input type="text" class="form-control form-control-lg" id="txLocationCrrnt" placeholder="Location">
            </div>
            <%--            <label id="lblWarehouse" for="txWarehouseCrrnt">
            </label>--%>
        </div>
        <div class="form-group row">
            <label class="col-sm-2 col-form-label-lg" for="txQuantity">
                Quantity</label>
            <div class="col-sm-4">
                <input type="text" class="form-control form-control-lg" id="txQuantity" placeholder="Quantity">
            </div>
            <label id="lblQuantity" for="txQuantity">
            </label>
        </div>
        <div class="form-group row">
            <label class="col-sm-2 col-form-label-lg" for="txQuantityTotal">
                Total Quantity Real</label>
            <div class="col-sm-4">
                <input type="text" class="form-control form-control-lg" id="txQuantityTotal" placeholder="Qty Total ">
            </div>
        </div>
        <div class="form-group row">
            <label class="col-sm-2 col-form-label-lg" for="txQuantityPaidTotal">
                Pallet´s</label>
            <div class="col-sm-4">
                <input type="text" class="form-control form-control-lg" id="txQuantityPaidTotal" placeholder="Qty of Pallets">
            </div>
        </div>
        <div class="form-group row">
            <label class="col-sm-2 col-form-label-lg" for="txQuantity">
                Target Location</label>
            <div class="col-sm-2">
                <input type="text" class="form-control form-control-lg" id="txWarehouseTrgt" placeholder="Warehouse">
            </div>
            <div class="col-sm-2">
                <input type="text" class="form-control form-control-lg" id="txLocationTrgt" placeholder="Location">
            </div>
            <%--            <label id="lblWarehouse" for="txWarehouseCrrnt">
            </label>--%>
        </div>
        <%--        <div class="form-group row">
            <label class="col-sm-2 col-form-label-lg" for="txLocationCrrnt">
                Location</label>
            <div class="col-sm-4">
                <input type="text" class="form-control form-control-lg" id="txLocationCrrnt" placeholder="Location">
            </div>
        </div>--%>

        <div class="form-group row">
            <input id="btnProcess" type="button" class="btn btn-primary btn-lg col-6" value="Process" />
        </div>
        <div class="form-group row">
            <div id="MyDynamicEtiqueta">
            </div>
        </div>
        </div>
    </div>
    <div class="form-group row" style="display: contents">
        <label id="lblError">
        </label>
    </div>
    <div class="form-group row" style="padding-bottom: 3200px; display: contents">
        <table class="table col-12" id="DivPaids" style=" display: none; margin-bottom: 2000px">
            <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Paid</th>
                    <th scope="col">Item</th>
                    <th scope="col">Unid</th>
                    <th scope="col">Warehouse</th>
                    <th scope="col">Location</th>
                    <th scope="col">Quantity</th>
                </tr>
            </thead>
            <tbody id="tbody">
            </tbody>
        </table>
    </div>


    <script>
        var ktlc = "";
        var warehouseValid = false;
        var locationValid = false;
        function printDiv(divID) {

            var monthNames = [
                "1", "2", "3",
                "4", "5", "6", "7",
                "8", "9", "10",
                "11", "12"
            ];

            //PRINT LOCAL HOUR
            var d = new Date();
            var LbdDate = $(".LblDate");
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

            var mywindow = window.open('', 'targetWindow', 'toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400')
            mywindow.document.write('<html><head><title>' + document.title + '</title>');


            mywindow.document.write('<link rel="stylesheet" href="styles/all.css" />' +
                                    '<link rel="stylesheet" href="styles/bootstrap.min.css" />' +
                                    '<style type="text/css">' +
                                    '#MyEtiqueta label {' +
                                    'font-size: 15px;' +
                                    '}' +
                                    '#LblDate {' +
                                    'font-size: 14px !important;' +
                                    '}' +
                                    '#LblReprintInd,' +
                                    '#LblReprint {' +
                                    'display: none;' +
                                    '}' +
                                    '.isValid {' +
                                    'border-bottom: solid;' +
                                    'border-color: green;' +
                                    '}' +
                                    '.isNotValid {' +
                                    'border-bottom: solid;' +
                                    'border-color: red;' +
                                    '}' +
                                    '.fa-check {' +
                                    'color: green;' +
                                    '}' +
                                    '.fa-times {' +
                                    'color: red;' +
                                    '}' +
                                    '#checkItem,' +
                                    '#checkLot,' +
                                    '#checkWarehouse,' +
                                    '#checkLoca,' +
                                    '#checkPaid {' +
                                    'display: none;' +
                                    '}' +
                                    '#exItem,' +
                                    '#exLot,' +
                                    '#exWarehouse,' +
                                    '#exLoca,' +
                                    '#exPaid {' +
                                    'display: none;' +
                                    '}' +
                                    '#loadItem,' +
                                    '#loadLot,' +
                                    '#loadWarehouse,' +
                                    '#loadLoca,' +
                                    '#loadPaid {' +
                                    'display: none;' +
                                    '}' +
                                    'tr {' +
                                    'text-align: center;' +
                                    '}' +
                                    'th {' +
                                    'text-align: center;' +
                                    '}' +
                                    '#myLabel {' +
                                    'width: 6in;' +
                                    'height: 4in;' +
                                    'border-radius: 12px;' +
                                    '}' +
                                    '.alingRight {' +
                                    'text-align: right;' +
                                    '}' +
                                    '.alingLeft {' +
                                    'text-align: left;' +
                                    '}' +
                                    '#printButton {' +
                                    'width: 6in;' +
                                    '}' +
                                    '#codePaid {' +
                                    'display: block;' +
                                    'margin: auto;' +
                                    'height: 150px;' +
                                    'width: 750px;' +
                                    '}' +
                                    '#codeItem {' +
                                    'display: block;' +
                                    'margin: auto;' +
                                    'height: 75px;' +
                                    'width: 250px;' +
                                    '}' +
                                    '#itemDesc {' +
                                    'vertical-align: middle;' +
                                    'font-size: 21px;' +
                                    '}' +
                                    '.divDesc {' +
                                    'text-align: center;' +
                                    '}' +
                                    '#lblDesc {' +
                                    '}' +
                                    '#lblMadein {' +
                                    '}' +
                                    '.borderTop {' +
                                    'border-top: solid 1px gray;' +
                                    '}' +
                                    '#printContainer {' +
                                    'margin-bottom: 100px;' +
                                    '}' +
                                    '#editTable {' +
                                    'display: none;' +
                                    '}' +
                                    '#lblError {' +
                                    'color: red;' +
                                    'font-size: 13px;' +
                                    '}' +
                                    '.load {' +
                                    'width: 10px;' +
                                    'height: 10px;' +
                                    'align-content: center;' +
                                    'animation-name: spin;' +
                                    'animation-duration: 5000ms;' +
                                    'animation-iteration-count: infinite;' +
                                    'animation-timing-function: linear;' +
                                    '}' +
                                    '#saveSection {' +
                                    'display: none;' +
                                    '}' +
                                    '.notBorderBottom {' +
                                    'border-bottom: none;' +
                                    '}' +
                                    '#divBarcode {' +
                                    '--height: 186px;' +
                                    'padding: inherit;' +
                                    '}' +
                                    '@keyframes spin {' +
                                    'from {' +
                                    'transform: rotate(0deg);' +
                                    '}' +
                                    'to {' +
                                    'transform: rotate(360deg);' +
                                    '}' +
                                    '}' +
                                    '#table {' +
                                    'font-size: 10px;' +
                                    '}' +
                                    '.table td,' +
                                    '.table th {' +
                                    'padding: .1rem;' +
                                    'border-top: 1px solid #dee2e6;' +
                                    'font-size: 1.7rem;' +
                                    'text-align: left;' +
                                    'vertical-align: middle;' +
                                    'padding-left: 1em;' +
                                    '}' +
                                    '@page {' +
                                        'size: landscape;' +
                                        'margin: 3px;' +
                                    '}' +
                                    '</style>' +
                                    '</head><body>');

            mywindow.document.write(document.getElementById(divID).innerHTML);
            mywindow.document.write('</body></html>');
            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/
            setTimeout(function () { mywindow.print() }, 5000);
            //mywindow.close();

            return true;
        };

        var paidsUdp = 0;
        var timer;
        function stoper() {

            clearTimeout(timer);
        }


        function IniciarComponentes() {

            txItem = $('#txItem');
            txLot = $('#txLot');
            txWarehouseCrrnt = $('#txWarehouseCrrnt');
            txWarehouseTrgt = $('#txWarehouseTrgt');
            txLocationTrgt = $('#txLocationTrgt');
            //txLocationCrrnt = $('#txLocationCrrnt');
            txQuantity = $('#txQuantity');
            txQuantityTotal = $('#txQuantityTotal');
            txQuantityPaidTotal = $('#txQuantityPaidTotal');

            lblItem = $('#lblItem');
            //lblWarehouse = $('#lblWarehouse');
            lblQuantity = $('#lblQuantity');

            btnProcess = $('#btnProcess');

        };

        IniciarComponentes();


        function BloquearComponentes() {

            //$('#txItem').prop("disabled", true); 
            $('#txLot').prop("disabled", true);
            $('#txWarehouseCrrnt').prop("disabled", true);
            //$('#txLocationCrrnt').prop("disabled", true);
            $('#txQuantity').prop("disabled", true);
            $('#btnProcess').prop("disabled", true);

        };
        BloquearComponentes();

        var SuccesVerificarPalletID = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                //                $('#lblWorkOrder').html("");
                $('#lblItem').val("");
                $('#lblItemDsca').html("");
                $('#txWarehouseCrrnt').val("");
                //$('#txLocationCrrnt').val("");
                $('#txQuantity').val("");
                $('#lblQuantity').html("");
                $('#txWarehouseCrrnt').prop("disabled", true);
                //$('#txLocationCrrnt').prop("disabled", true);
                $('#txQuantity').prop("disabled", true);
                $('#txPalletID').prop("disabled", false);
                $('#detail').fadeOut(1000);
            }
            else {
                //$('#lblWorkOrder').html(MyObj.PAID);
                $('#txPalletID').prop("disabled", true);
                $('#lblItem').val(MyObj.ITEM);
                $('#lblItemDsca').html(MyObj.DSCA);
                $('#txWarehouseCrrnt').val(MyObj.CWAA);
                //$('#lblWarehouse').html(MyObj.DESCWRH);
                $('#txWarehouseCrrnt').prop("disabled", true);
                $('#txLocationCrrnt').prop("disabled", true);
                $('#txWarehouseTrgt').prop("disabled", false);
                $('#txLocationTrgt').prop("disabled", true);
                $('#txQuantity').val(MyObj.QTYT);
                $('#lblQuantity').html(MyObj.UNIT)
                $('#txLocationCrrnt').val(MyObj.LOCA);
                $('#lblError').html("");
                $('#detail').fadeIn(1000);
            }
        };

        $('#btnClear').bind('click', function () {
            $('#detail').fadeOut(1000);
            $('#txPalletID').val("");
            $('#lblItem').val("");
            $('#lblItemDsca').html("");
            $('#txWarehouseCrrnt').val("");
            //$('#txLocationCrrnt').val("");
            $('#txQuantity').val("");
            $('#lblQuantity').html("");
            $('#txWarehouseCrrnt').prop("disabled", true);
            //$('#txLocationCrrnt').prop("disabled", true);
            $('#txQuantity').prop("disabled", true);
            $('#txPalletID').prop("disabled", false);
            $('#DivPaids').hide();
            $("#tbody tr").remove();
            $('#txQuantityTotal').val("");
            $('#txQuantityPaidTotal').val("");
            $('#txWarehouseTrgt').val("");
            $('#txLocationTrgt').val("");
        });

        var SuccesVerificarItem = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                BloquearComponentes();
            }
            if (MyObj.Error == false) {
                $('#lblError').html("");
                lblItem.val(MyObj.dsca);
                lblItem.html(MyObj.dsca);
                lblQuantity.html(MyObj.cuni);
                ktlc = MyObj.kltc;
                if (MyObj.kltc == "1") {

                    $('#txLot').prop("disabled", false);
                }
                else {
                    $('#txLot').prop("disabled", true);
                    $('#txWarehouseCrrnt').prop("disabled", true);
                    $('#txLocationCrrnt').prop("disabled", true);
                    $('#txWarehouseTrgt').prop("disabled", true);
                    $('#txLocationTrgt').prop("disabled", true);
                    //$('#txLocationCrrnt').prop("disabled", true);
                    $('#txQuantity').prop("disabled", true);
                }
            }

        }

        var SuccesVerificarLote = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                $('#txWarehouseCrrnt').prop("disabled", true);
            }
            if (MyObj.Error == false) {
                $('#lblError').html("");
                $('#txWarehouseCrrnt').prop("disabled", false);
            }
        }

        var SuccesVerificarWarehouseTrgt = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                $('#txLocationTrgt').prop("disabled", true);
            }
            if (MyObj.Error == false) {
                $('#lblError').html("");
                if (MyObj.sloc == "1") {
                    $('#txLocationTrgt').prop("disabled", false);
                    warehouseValid = true;
                    locationValid = false;
                }
                else {
                    $('#txLocationTrgt').prop("disabled", true);
                    $('#txLocationTrgt').val("");
                    warehouseValid = false;
                    locationValid = true;
                }
            }
            VerificarForm()
        }

        var SuccesVerificarLocation = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                locationValid = false;
            }
            if (MyObj.Error == false) {
                $('#lblError').html("");
                locationValid = true;
            }
            VerificarForm();
        }

        var SuccesVerificarQuantity = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
            }
            if (MyObj.Error == false) {
                $('#lblError').html("");
                if ($('#txQuantity').val() > 0 && $('#txQuantity').val() <= parseInt(MyObj.stks, 10)) {
                    $('#btnProcess').prop("disabled", false);

                }
                else {
                    $('#btnProcess').prop("disabled", true);
                    ImprimirMensaje(MyObjy.TypeMsgJs, MyObj.SuccessMsg);
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
        var SuccesClick_Transfer = function (r) {
            MyObject = JSON.parse(r.d);

            if (MyObject.Error == false) {
                $('#txPalletID').val("");
                //$('#lblWorkOrder').html("");
                $('#lblItem').val("");
                $('#lblItemDsca').html("");
                $('#txWarehouseCrrnt').val("");
                //$('#txLocationCrrnt').val("");
                $('#txQuantity').val("");
                $('#lblQuantity').html("");
                $('#btnProcess').prop("disabled", true);
                alert("Registration was successful");
            }
            else {
                console.log("El registro no se realizo");
                alert(MyObject.ErrorMsg);
            }

        }


        var VerificarPalletID = function () {
            $('#btnProcess').prop("disabled", true);
            var Data = "{'PAID':'" + $('#txPalletID').val() + "'}";
            sendAjax("VerificarPalletID", Data, SuccesVerificarPalletID)
        }


        var VerificarItem = function () {
            $('#btnProcess').prop("disabled", true);
            var Data = "{'ITEM':'" + $('#txItem').val() + "'}";
            sendAjax("VerificarItem", Data, SuccesVerificarItem)
        }

        var VerificarLote = function () {
            $('#btnProcess').prop("disabled", true);
            var Data = "{'ITEM':'" + $('#txItem').val() + "','CLOT':'" + $('#txLot').val() + "'}";
            sendAjax("VerificarLote", Data, SuccesVerificarLote)
        }

        var VerificarWarehouse = function (cware, SuccessMethod) {
            $('#btnProcess').prop("disabled", true);
            var Data = "{'CWAR':'" + cware.toUpperCase().trim() + "'}";
            sendAjax("VerificarWarehouse", Data, SuccessMethod)
        }

        var VerificarLocation = function () {
            $('#btnProcess').prop("disabled", true);
            var Data = "{'CWAR':'" + $('#txWarehouseTrgt').val() + "','LOCA':'" + $('#txLocationTrgt').val() + "'}";
            sendAjax("VerificarLocation", Data, SuccesVerificarLocation)
        }

        var VerificarQuantity = function () {
            $('#btnProcess').prop("disabled", true);
            var Data = "{'CWAR':'" + $('#txWarehouseCrrnt').val() + "','ITEM':'" + $('#lblItem').val() + "','LOCA':'" + $('#txLocationCrrnt').val() + "','CLOT':'" + (ktlc == "1" ? $('#lblWorkOrder').html() : " ") + "'}";
            sendAjax("VerificarQuantity", Data, SuccesVerificarQuantity)
        }

        var Click_Transfer = function () {

            var Data = "{'QtyReal':'" + $('#txQuantityTotal').val() + "','Paids':'" + $('#txQuantityPaidTotal').val() + "','TargetCwar':'" + $('#txWarehouseTrgt').val() + "','TargetLoca':'" + $('#txLocationTrgt').val() + "'}";
            sendAjax("Click_TransferP1", Data, TransferSucces);

        }

        var Click_Process = function () {

            var Data = "{'QtyReal':'" + $('#txQuantityTotal').val() + "','Paids':'" + $('#txQuantityPaidTotal').val() + "','TargetCwar':'" + $('#txWarehouseTrgt').val() + "','TargetLoca':'" + $('#txLocationTrgt').val() + "'}";
            sendAjax("Click_Process", Data, ProcessSucces);

        }

        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "TransferFromTransitWarehouseNew.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: asyncMode != false ? true : false,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }


        $('#txPalletID').bind('paste keyup', function () {
            stoper();
            timer = setTimeout("VerificarPalletID()", 1000);
        });

        txLot.bind('paste keyup', function () {
            stoper();
            timer = setTimeout("VerificarLote()", 1000);
        });

        txWarehouseTrgt.bind('paste input', function () {
            $('#DivPaids').hide();
            $("#tbody tr").remove();
            $('#btnProcess').prop("disabled", true);
            stoper();
            timer = setTimeout("VerificarWarehouse('" + txWarehouseTrgt.val() + "',SuccesVerificarWarehouseTrgt)", 1000);
        });

        txLocationTrgt.bind('paste input', function () {
            $('#DivPaids').hide();
            $("#tbody tr").remove();
            $('#btnProcess').prop("disabled", true);
            stoper();
            timer = setTimeout("VerificarLocation()", 1000);
        });

        txQuantity.bind('paste keyup', function () {
            $('#DivPaids').hide();
            $("#tbody tr").remove();
            stoper();
            timer = setTimeout("VerificarQuantity()", 1000);
        });

        txQuantityTotal.bind('paste keyup', function () {
            $('#DivPaids').hide();
            $("#tbody tr").remove();
            $('#lblError').html("");
            VerificarForm();
        })

        txQuantityPaidTotal.bind('paste keyup', function () {
            $('#DivPaids').hide();
            $("#tbody tr").remove();
            $('#lblError').html("");
            VerificarForm();
        })

        $(".btnSaver").bind('click', function () {
            saveQty();
        });

        btnProcess.bind('click', function () {
            Click_Process();
        });



        var VerificarForm = function () {
            if (parseFloat($("#txQuantityTotal").val()) > parseFloat($("#txQuantity").val())) {
                $('#lblError').html("Total Quantity Real is greater than allowed");
                $('#btnProcess').prop("disabled", true);
            }
            else if ($("#txQuantityTotal").val().trim() == "") {
                $('#lblError').html("Total Quantity Real is Empty");
                $('#btnProcess').prop("disabled", true);
            }
            else if (parseFloat($("#txQuantityPaidTotal").val()) <= 0) {
                $('#btnProcess').prop("disabled", true);
            }
            else if (warehouseValid == false) {
                $('#btnProcess').prop("disabled", true);
            }
            else if (locationValid == false) {
                $('#btnProcess').prop("disabled", true);
            }
            else {
                $('#btnProcess').prop("disabled", false);
            }
        }

        var saveQty = function () {
            $("#loader").fadeIn();
            var paidA = [];
            var qtyS = [];
            var Data = '{}';
            var paids = $("#tbody tr").length;
                for (let i = 0 ; i < paids; i++) {
                    var qty = $('#txQty' + i).val()
                    var paid = $('#Paid' + i).html()
                    if (qty > 0) {
                        paidA.push(paid);
                        qtyS.push(qty);
                        var paidsJSon = paidA.toString();
                        var qtySJSon = qtyS.toString();
                        Data = "{'QtyReal':'" + $('#txQuantityTotal').val() + "','Paids':'" + paidsJSon + "','Qtys':'" + qtySJSon + "','TargetCwar':'" + $('#txWarehouseTrgt').val() + "','TargetLoca':'" + $('#txLocationTrgt').val() + "','final':'" + (i == (paids - 1) ? true : false) + "'}";
                    }
                }
                sendAjax("Click_TransferP1", Data,saveQtySucces,true);
        }

        var saveQtySucces = function (r) {
            paidsUdp = 0;
            $("#MyDynamicEtiqueta").empty();
            let myList131 = JSON.parse(r.d);
            myList131.forEach(function (MyObj, index) {
                if (MyObj.Error == false) {
                    paidsUdp += 1;
                    CBPalletNOd = MyObj.PAID_URL;
                    lblItemIDd = MyObj.ITEM;
                    lblItemDescd = MyObj.DSCA;
                    LblQuantityd = MyObj.QTYA;
                    LblUnitd = MyObj.UNIC;
                    LblLotIdd = MyObj.LOT;
                    CBPurchaseOrderd = MyObj.ORNO_URL;
                    CBItem = MyObj.ITEM_URL;
                    CBLotd = MyObj.CLOT_URL;
                    CBQuantityd = MyObj.QTYC_URL;
                    CBUnitd = MyObj.UNIC_URL;
                    LblPurchaseOCd = MyObj.ORNO;
                    LblItemOCd = MyObj.ITEM;
                    LblLotOCd = MyObj.CLOT;
                    LblUnitOCd = MyObj.UNIT;
                    LblQuantityOCd = MyObj.QTYA;
                    LblUser = MyList.LOGN;
                    LblSup = MyObj.NAMA;

                    var etiqueta =
                        '<div id="myLabel" style="width: 100%; height: 100%;">' +
                        '<div class="row">' +
                        '<div class="col-6 alingLeft" style="font-size: 30px; height: 1em;">' +
                        '<label id="lblMaterialDesc"><strong>' + lblItemDescd + '</strong></label>' +
                        '</div>' +
                        '<div class="col-4 alingRight" style="font-size: 30px; height: 1em;">' +
                        '<label id="lblMaterialCode"><strong>' + lblItemIDd + '</strong></label>' +
                        '</div>' +
                        '</div>' +
                        '<br />' +
                        '<div class="col-10 borderTop" id="divBarcode">' +
                        '<img src="' + CBPalletNOd + '" id="codePaid" alt="" style="margin-left:50px; width:680px"/>' +
                        '</div>' +
                        '<div>' +
                        '<table class="table mw-100">' +
                        '<tbody>' +
                        '<tr>' +
                        '<td><strong>LOT</strong>&nbsp;&nbsp;<label id="lblLot">' + LblLotIdd + '</label></td>' +
                        '<td><strong>Quantity</strong>&nbsp;&nbsp;<label id="lblQuantity">' + (MyList.PAIDS.length - 1 == index && MyList.PAIDS.length > 1 ? (MyList.QTYCFinal.replace(",", ".").trim() == "" ? LblQuantityd.replace(",", ".") : MyList.QTYCFinal.replace(",", ".")) : LblQuantityd.replace(",", ".")) + " " + MyList.UNIC + '</label></td>' +
                        '</tr>' +
                        '<tr>' +
                        '<td><strong>Origin Lot</strong>&nbsp;&nbsp;<label id="lblOrigin">' + LblLotIdd + '</label></td>' +
                        '<td><strong>Supplier</strong>&nbsp;&nbsp;<label id="lblSupplier">' + LblSup + '</label></td>' +
                        '</tr>' +
                        '<tr>' +
                        '<td><strong>Received By</strong>&nbsp;&nbsp;<label id="lblRecibedBy">' + LblUser + '</label></td>' +
                        '<td><strong>Received On</strong>&nbsp;&nbsp;<class="LblDate">' + MyList.DATE + '</label></td>' +
                        '</tr>' +
                        '</tbody>' +
                        '</table>' +
                        '</div>' +
                        '</div>';
                    $('#MyDynamicEtiqueta').append(etiqueta);
                }
            })


            if (paidsUdp == parseInt(localStorage.getItem("NmrPaids"))) {
                $('#DivPaids').hide(100);
                $("#tbody tr").remove();
                $('#btnClear').click();
                $("#loader").fadeOut(100)
                alert("Save success");
                printDiv('MyDynamicEtiqueta');
            }
            else {
                $('#DivPaids').hide(100);
                $("#tbody tr").remove();
                $('#btnClear').click();
                $("#loader").fadeOut(100)
                alert("Save failed to:" + (parseInt(localStorage.getItem("NmrPaids")) - paidsUdp) + "and success to:" + paidsUdp);
            }
        }

        let verifyQty = function (evnt, i) {
            if (document.getElementById('txQty' + i).value.indexOf('.') != -1) {
                var decimals = document.getElementById('txQty' + i).value.substring(document.getElementById('txQty' + i).value.indexOf('.') + 1).trim()
                if (decimals != "") {
                    if (decimals.length > 10) {
                        document.getElementById('txQty' + i).value = parseFloat(document.getElementById('txQty' + i).value).toFixed(4);
                    }
                }
            }
            let sumcount = 0.0000;
            let TtlQty = parseFloat(localStorage.getItem("TtlQty")).toFixed(4);
            let NmrPaids = parseInt(localStorage.getItem("NmrPaids"));
            for (var j = 0 ; j < NmrPaids; j++) {
                sumcount += parseFloat($("#txQty" + j).val());
            }
            if (parseFloat(sumcount).toFixed(4) == TtlQty) {
                $("#btnSaver" + i).fadeIn(100);
                $('#lblError').html("")
            }
            else {
                for (var j = 0 ; j < NmrPaids; j++) {
                    $("#btnSaver" + j).fadeOut(100);
                    $('#lblError').html("Total qty paids: " + parseFloat(sumcount).toFixed(4));
                }
            }

        }

        var TransferSucces = function (r) {
            localStorage.setItem("TtlQty", $('#txQuantityTotal').val());
            localStorage.setItem("NmrPaids", $('#txQuantityPaidTotal').val());
            $("#MyDynamicEtiqueta").empty();
            MyList = JSON.parse(r.d);

            if (MyList.PAIDS.length == undefined) {
                MyObject = JSON.parse(r.d);
                if (MyObject.Error == false) {
                    //Etiqueta Sin orden de compra

                    $('#pslip').val("");
                    CBPalletNO.attr("src", MyObject.PAID_URL);
                    lblItemID.html(MyObject.ITEM);
                    lblItemDesc.html(MyObject.DSCA);
                    LblQuantity.html(MyObject.QTYC);
                    LblUnit.html(MyObject.UNIC);
                    LblLotId.html(MyObject.CLOT);
                    //LblReprint.html(MyObject.PAID.substring(10, 13));

                    // Etiqueta orden de compra

                    CBPurchaseOrder.attr("src", MyObject.ORNO_URL);
                    CBItem.attr("src", MyObject.ITEM_URL);
                    CBLot.attr("src", MyObject.CLOT_URL);
                    if (MyObject.CLOT_URL == "") {
                        CBLot.hide();
                    }
                    else {
                        CBLot.show();
                    }
                    CBQuantity.attr("src", MyObject.QTYC_URL);
                    CBUnit.attr("src", MyObject.UNIC_URL);

                    LblPurchaseOC.html(MyObject.ORNO);
                    LblItemOC.html(MyObject.ITEM);
                    LblLotOC.html(MyObject.CLOT);
                    LblUnitOC.html(MyObject.UNIT);
                    LblQuantityOC.html(MyObject.QTYC);

                    //                if (MyObject.OORG != "2" && MyObject != undefined) {
                    //                    //btnMyEtiqueta.show();
                    //                }
                    //                else if (MyObject != undefined) {
                    //                    //btnMyEtiqueta.show();
                    //                }

                    DeshabilitarLimpiarControles();
                    myLabelFrame = document.getElementById('myLabelFrame');
                    if (sessionStorage.getItem('nav').toString() == 'EDG') {
                        myLabelFrame.src = '../Labels/RedesingLabels/1RawMaterialME.aspx';
                    }
                    else {
                        myLabelFrame.src = '../Labels/RedesingLabels/1RawMaterial.aspx';
                    }


                }
                else {
                    console.log("El registro no se realizo");
                    alert(MyObject.ErrorMsg);
                }
            }
            else {
                MyList.PAIDS.forEach(function (PAID, index) {

                    CBPalletNOd = MyList.PAIDS_URLS[index];
                    lblItemIDd = MyList.ITEM;
                    lblItemDescd = MyList.DSCA;
                    LblQuantityd = MyList.QTYAS[index];
                    LblUnitd = MyList.UNIC;
                    LblLotIdd = MyList.CLOT;
                    CBPurchaseOrderd = MyList.ORNO_URL;
                    CBItem = MyList.ITEM_URL;
                    CBLotd = MyList.CLOT_URL;
                    CBQuantityd = MyList.QTYC_URL;
                    CBUnitd = MyList.UNIC_URL;
                    LblPurchaseOCd = MyList.ORNO;
                    LblItemOCd = MyList.ITEM;
                    LblLotOCd = MyList.CLOT;
                    LblUnitOCd = MyList.UNIT;
                    LblQuantityOCd = MyList.QTYAS[index];
                    LblUser = MyList.LOGN;
                    LblSup = MyList.NAMA;

                    var etiqueta =
                        '<div id="myLabel" style="width: 100%; height: 100%;">' +
                        '<div class="row">' +
                        '<div class="col-6 alingLeft" style="font-size: 30px; height: 1em;">' +
                        '<label id="lblMaterialDesc"><strong>' + lblItemDescd + '</strong></label>' +
                        '</div>' +
                        '<div class="col-4 alingRight" style="font-size: 30px; height: 1em;">' +
                        '<label id="lblMaterialCode"><strong>' + lblItemIDd + '</strong></label>' +
                        '</div>' +
                        '</div>' +
                        '<br />' +
                        '<div class="col-10 borderTop" id="divBarcode">' +
                        '<img src="' + CBPalletNOd + '" id="codePaid" alt="" style="margin-left:50px; width:680px"/>' +
                        '</div>' +
                        '<div>' +
                        '<table class="table mw-100">' +
                        '<tbody>' +
                        '<tr>' +
                        '<td><strong>LOT</strong>&nbsp;&nbsp;<label id="lblLot">' + LblLotIdd + '</label></td>' +
                        '<td><strong>Quantity</strong>&nbsp;&nbsp;<label id="lblQuantity">' + (MyList.PAIDS.length - 1 == index && MyList.PAIDS.length > 1 ? (MyList.QTYCFinal.replace(",", ".").trim() == "" ? LblQuantityd.replace(",", ".") : MyList.QTYCFinal.replace(",", ".")) : LblQuantityd.replace(",", ".")) + " " + MyList.UNIC + '</label></td>' +
                        '</tr>' +
                        '<tr>' +
                        '<td><strong>Origin Lot</strong>&nbsp;&nbsp;<label id="lblOrigin">' + LblLotIdd + '</label></td>' +
                        '<td><strong>Supplier</strong>&nbsp;&nbsp;<label id="lblSupplier">' + LblSup + '</label></td>' +
                        '</tr>' +
                        '<tr>' +
                        '<td><strong>Received By</strong>&nbsp;&nbsp;<label id="lblRecibedBy">' + LblUser + '</label></td>' +
                        '<td><strong>Received On</strong>&nbsp;&nbsp;<class="LblDate">' + MyList.DATE + '</label></td>' +
                        '</tr>' +
                        '</tbody>' +
                        '</table>' +
                        '</div>' +
                        '</div>';
                    $('#MyDynamicEtiqueta').append(etiqueta);
                }
            );

                //DeshabilitarLimpiarControles();
                $('#DivPaids').show(100);
                printDiv('MyDynamicEtiqueta');

            }

        }
        var ProcessSucces = function (r) {
            $("#tbody tr").remove();
            localStorage.setItem("TtlQty", $('#txQuantityTotal').val());
            localStorage.setItem("NmrPaids", $('#txQuantityPaidTotal').val());
            $("#MyDynamicEtiqueta").empty();
            MyList = JSON.parse(r.d);

            if (MyList.PAIDS.length == undefined) {
                MyObject = JSON.parse(r.d);
                if (MyObject.Error == false) {
                    //Etiqueta Sin orden de compra

                    $('#pslip').val("");
                    CBPalletNO.attr("src", MyObject.PAID_URL);
                    lblItemID.html(MyObject.ITEM);
                    lblItemDesc.html(MyObject.DSCA);
                    LblQuantity.html(MyObject.QTYC);
                    LblUnit.html(MyObject.UNIC);
                    LblLotId.html(MyObject.CLOT);
                    //LblReprint.html(MyObject.PAID.substring(10, 13));

                    // Etiqueta orden de compra

                    CBPurchaseOrder.attr("src", MyObject.ORNO_URL);
                    CBItem.attr("src", MyObject.ITEM_URL);
                    CBLot.attr("src", MyObject.CLOT_URL);
                    if (MyObject.CLOT_URL == "") {
                        CBLot.hide();
                    }
                    else {
                        CBLot.show();
                    }
                    CBQuantity.attr("src", MyObject.QTYC_URL);
                    CBUnit.attr("src", MyObject.UNIC_URL);

                    LblPurchaseOC.html(MyObject.ORNO);
                    LblItemOC.html(MyObject.ITEM);
                    LblLotOC.html(MyObject.CLOT);
                    LblUnitOC.html(MyObject.UNIT);
                    LblQuantityOC.html(MyObject.QTYC);

                    //                if (MyObject.OORG != "2" && MyObject != undefined) {
                    //                    //btnMyEtiqueta.show();
                    //                }
                    //                else if (MyObject != undefined) {
                    //                    //btnMyEtiqueta.show();
                    //                }

                    DeshabilitarLimpiarControles();
                    myLabelFrame = document.getElementById('myLabelFrame');
                    if (sessionStorage.getItem('nav').toString() == 'EDG') {
                        myLabelFrame.src = '../Labels/RedesingLabels/1RawMaterialME.aspx';
                    }
                    else {
                        myLabelFrame.src = '../Labels/RedesingLabels/1RawMaterial.aspx';
                    }


                }
                else {
                    console.log("El registro no se realizo");
                    alert(MyObject.ErrorMsg);
                }
            }
            else {
                MyList.PAIDS.forEach(function (PAID, index) {

                    CBPalletNOd = MyList.PAIDS_URLS[index];
                    lblItemIDd = MyList.ITEM;
                    lblItemDescd = MyList.DSCA;
                    LblQuantityd = MyList.QTYAS[index];
                    LblUnitd = MyList.UNIC;
                    LblLotIdd = MyList.CLOT;
                    CBPurchaseOrderd = MyList.ORNO_URL;
                    CBItem = MyList.ITEM_URL;
                    CBLotd = MyList.CLOT_URL;
                    CBQuantityd = MyList.QTYC_URL;
                    CBUnitd = MyList.UNIC_URL;
                    LblPurchaseOCd = MyList.ORNO;
                    LblItemOCd = MyList.ITEM;
                    LblLotOCd = MyList.CLOT;
                    LblUnitOCd = MyList.UNIT;
                    LblQuantityOCd = MyList.QTYAS[index];
                    LblUser = MyList.LOGN;
                    LblSup = MyList.NAMA;

                    $('#tbody').append('<tr><th scope="row">' + (index + 1) + '</th><td id="Paid' + index + '">' + MyList.PAIDS[index] + '</td><td>' + MyList.ITEM + '</td><td>' + MyList.UNIC + '</td><td>' + MyList.CWAR + '</td><td>' + MyList.LOCA + '</td><td><input type="text" class="form-control form-control-lg col-12" id="txQty' + index + '" placeholder="' + MyList.QTYS + '" value="' + ((MyList.PAIDS.length - 1 == index) ? MyList.QTYAF : MyList.QTYS) + '" oninput="verifyQty(this,' + index + ')" step="0.0001"></td><td><input type="button" id="btnSaver' + index + '" value="Save" class="btn btn-primary col-12 btnSaver" ' + ((index == 0) ? "" : 'style="display:none"') + '/></td></tr>');
                    //var etiqueta =
                    //    '<div id="myLabel" style="width: 100%; height: 100%;">' +
                    //    '<div class="row">' +
                    //    '<div class="col-6 alingLeft" style="font-size: 30px; height: 1em;">' +
                    //    '<label id="lblMaterialDesc"><strong>' + lblItemDescd + '</strong></label>' +
                    //    '</div>' +
                    //    '<div class="col-4 alingRight" style="font-size: 30px; height: 1em;">' +
                    //    '<label id="lblMaterialCode"><strong>' + lblItemIDd + '</strong></label>' +
                    //    '</div>' +
                    //    '</div>' +
                    //    '<br />' +
                    //    '<div class="col-10 borderTop" id="divBarcode">' +
                    //    '<img src="' + CBPalletNOd + '" id="codePaid" alt="" style="margin-left:50px; width:680px"/>' +
                    //    '</div>' +
                    //    '<div>' +
                    //    '<table class="table mw-100">' +
                    //    '<tbody>' +
                    //    '<tr>' +
                    //    '<td><strong>LOT</strong>&nbsp;&nbsp;<label id="lblLot">' + LblLotIdd + '</label></td>' +
                    //    '<td><strong>Quantity</strong>&nbsp;&nbsp;<label id="lblQuantity">' + (MyList.PAIDS.length - 1 == index && MyList.PAIDS.length > 1 ? (MyList.QTYCFinal.replace(",", ".").trim() == "" ? LblQuantityd.replace(",", ".") : MyList.QTYCFinal.replace(",", ".")) : LblQuantityd.replace(",", ".")) + " " + MyList.UNIC + '</label></td>' +
                    //    '</tr>' +
                    //    '<tr>' +
                    //    '<td><strong>Origin Lot</strong>&nbsp;&nbsp;<label id="lblOrigin">' + LblLotIdd + '</label></td>' +
                    //    '<td><strong>Supplier</strong>&nbsp;&nbsp;<label id="lblSupplier">' + LblSup + '</label></td>' +
                    //    '</tr>' +
                    //    '<tr>' +
                    //    '<td><strong>Received By</strong>&nbsp;&nbsp;<label id="lblRecibedBy">' + LblUser + '</label></td>' +
                    //    '<td><strong>Received On</strong>&nbsp;&nbsp;<class="LblDate">' + MyList.DATE + '</label></td>' +
                    //    '</tr>' +
                    //    '</tbody>' +
                    //    '</table>' +
                    //    '</div>' +
                    //    '</div>';
                    //$('#MyDynamicEtiqueta').append(etiqueta);
                }
            );

                //DeshabilitarLimpiarControles();
                $('#DivPaids').show(100);
                //printDiv('MyDynamicEtiqueta');
                $(".btnSaver").bind('click', function () {
                    saveQty();
                });
            }

        }
    </script>
    <script src="styles/popper.min.js"></script>
    <script src="styles/bootstrap.min.js"></script>
    <script src="styles/jquery-3.1.1.min.js"></script>

</asp:Content>
