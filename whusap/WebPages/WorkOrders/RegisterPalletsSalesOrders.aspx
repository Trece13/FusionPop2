<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="RegisterPalletsSalesOrders.aspx.cs" Inherits="whusap.WebPages.InvReceipts.RegisterPalletsSalesOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        $("input[id$=Contenido_txtQuantity]").focus(function () {
            $("div[id$=Contenido_tooltip]").show();
        });

        $("input[id$=Contenido_txtQuantity]").blur(function () {
            $("div[id$=Contenido_tooltip]").hide();
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style>
        #MyEtiquetaOC,#MyEtiqueta
        {
            display: none;
        }
        #LblUnitOC
        {
            font-size:14px;
        }
        #lblError
        {
            font-size:30px;
            color:Red;
        }
        
        label
        {
            font-size:14px;
        }
    </style>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txItem">
            Sales Order</label>
        <div class="col-sm-4">
            <input type="text" class="form-control form-control-lg" id="txItem" placeholder="Sales Order">
        </div>
        <label id="lblSalesOrder" for="txOrder">
        </label>
    </div>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txPaid">
            Pallet Id</label>
        <div class="col-sm-4">
            <input type="text" class="form-control form-control-lg" id="txPaid" placeholder="Pallet Id">
        </div>
        <label id="lblItemdesc" for="txPaid" style="font-size:20px;">
        </label>
    </div>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txWarehouse">
            Warehouse</label>
        <div class="col-sm-4">
            <input type="text" class="form-control form-control-lg" id="txWarehouse" placeholder="Warehouse">
        </div>
    </div>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txLocation">
            Location</label>
        <div class="col-sm-4">
            <input type="text" class="form-control form-control-lg" id="txLocation" placeholder="Location">
        </div>
    </div>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txQuantity">
            Quantity</label>
        <div class="col-sm-4">
            <input type="text" class="form-control form-control-lg" id="txQuantity" placeholder="Quantity" pattern="\d+(\.\d)?">
        </div>
        <label id="lblQuantity" for="txQuantity">
        </label>
    </div>
    <div class="form-group row">
        <asp:Button  class="btn btn-primary btn-lg" ID="Button1" runat="server" onclick="Save_Click" Text="Save" />
    </div>
    <div class="form-group row">
        <label id="lblError"></label>
    </div>
    <!--<div class="form-group row">
        <input id="btnSave" type="button" class="btn btn-primary btn-lg" value="SAVE" />
    </div>-->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container">
                <iframe id="myLabelFrame" scrolling="no" title="" class ="col-12" style="height: 450px; overflow: hidden; margin-bottom: 100px;" frameborder="0" src=""></iframe>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>    
    <div>
    </div>
    <script>      
        var timer;
        function stoper() {

            clearTimeout(timer);
        }


        function IniciarComponentes() {

            txItem = $('#txItem');
            txPaid = $('#txPaid');
            txWarehouse = $('#txWarehouse');
            txLocation = $('#txLocation');
            txQuantity = $('#txQuantity');
            lblSalesOrder = $('#lblSalesOrder');
            lblItemdesc = $('#lblItemdesc');
            lblQuantity = $('#lblQuantity');
            btnSave = $('#Contenido_Button1');

        };

        IniciarComponentes();


        function BloquearComponentes() {

            $('#txPaid').prop("disabled", true);
            $('#txWarehouse').prop("disabled", true);
            $('#txLocation').prop("disabled", true);
            $('#txQuantity').prop("disabled", true);
            $('#Contenido_Button1').prop("disabled", true);

        };
        BloquearComponentes();
        var SuccesVerificarSalesOrder = function (r) {
            BloquearComponentes();
            $('#lblItemdesc').html("");
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                $('#txPaid').val("");
                $('#txWarehouse').val("");
                $('#txLocation').val("");
                $('#txQuantity').val("");
                $('#lblSalesOrder').html("");
                $('#lblItemdesc').html("");
                $('#lblQuantity').html("");

                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                BloquearComponentes();
            }
            if (MyObj.Error == false) {
                $('#lblError').html("");
                $('#txPaid').val("");
                $('#txWarehouse').val("");
                $('#txLocation').val("");
                $('#txQuantity').val("");
                $('lblSalesOrder').html("");
                $('lblItemdesc').html("");
                $('lblQuantity').html("");
                lblSalesOrder.html(MyObj.order);
                lblQuantity.html(MyObj.cuni);
                $('#txPaid').prop("disabled", false);
                $('#txWarehouse').prop("disabled", true);
                $('#txLocation').prop("disabled", true);
                $('#txQuantity').prop("disabled", true);
                }
            }

        var SuccesVerificarPallet = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                $('#txWarehouse').prop("disabled", true);
                $('#txLocation').prop("disabled", true);
                $('#txQuantity').prop("disabled", true);

                $('#txWarehouse').val("");
                $('#txLocation').val("");
                $('#txQuantity').val("");
                $('lblItemdesc').html("");
            }
            if (MyObj.Error == false) {

                $('#lblError').html("");
                $('#txWarehouse').prop("disabled", true);
                $('#txWarehouse').val(MyObj.cwar);
                $('#lblItemdesc').html(MyObj.dsca);
                $('#txLocation').val(MyObj.pdno);
                $('#txQuantity').val(MyObj.qtya);
                $('lblItemdesc').html("");
                $('#Contenido_Button1').prop("disabled", false);
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

        var VerificarSalesOrder = function () {
            $('#Contenido_Button1').prop("disabled", true);
            var Data = "{'ORDER':'" + $('#txItem').val() + "'}";
            sendAjax("VerificarSalesOrder", Data, SuccesVerificarSalesOrder)
        }

        var VerificarPallet = function () {
            $('#Contenido_Button1').prop("disabled", true);
            var Data = "{'PAID':'" + $('#txPaid').val() + "'}";
            sendAjax("VerificarPallet", Data, SuccesVerificarPallet)
        }

        var Click_Save = function () {

            var Data = "{'CWAR':'" + $('#txWarehouse').val().trim() + "','ITEM':'" + $('#txItem').val().trim().toUpperCase() + "','CLOT':'" + $('#txLot').val().trim() + "','LOCA':'" + $('#txLocation').val().trim() + "','QTYS':'" + $('#txQuantity').val().trim() + "','UNIT':'" + $('#lblQuantity').html() + "'}";
            sendAjax("Save_Click", Data, SuccesClick_Save);

        }

        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "RegisterPalletsSalesOrders.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }

        txItem.bind('paste keyup', function () {
            stoper();
            timer = setTimeout("VerificarSalesOrder()", 1000);
        });

        txPaid.bind('paste keyup', function () {
            stoper();
            timer = setTimeout("VerificarPallet()", 1000);
        });

    </script>
    <script src="styles/popper.min.js"></script>
    <script src="styles/bootstrap.min.js"></script>
    <script src="styles/jquery-3.1.1.min.js"></script>
</asp:Content>
