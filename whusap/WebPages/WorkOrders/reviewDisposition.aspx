<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="reviewDisposition.aspx.cs" Inherits="whusap.WebPages.WorkOrders.reviewDisposition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <form id="form1" class="container col-sm-12">
        <div class="row">
            <div class="col-sm-6">
                <div class="form-group row">
                    <label class="col-sm-4 col-form-label-lg" for="txItem">
                        Item</label>
                    <div class="col-sm-6">
                        <input type="text" class="form-control form-control-lg" id="txItem" placeholder="Item" tabindex="1">
                    </div>
                </div>
                <br />
                <div class="form-group row">
                    <label class="col-sm-4 col-form-label-lg" for="txWare">
                        Warehouse</label>
                    <div class="col-sm-6">
                        <input type="text" class="form-control form-control-lg" id="txWare" placeholder="Warehouse" tabindex="2">
                    </div>
                </div>
                <br />
                <div class="form-group row">
                    <label class="col-sm-4 col-form-label-lg" for="txPaid">
                        Pallet ID</label>
                    <div class="col-sm-6">
                        <input type="text" class="form-control form-control-lg" id="txPaid" placeholder="Pallet ID" tabindex="3">
                    </div>
                </div>
                <br />
                <div class="form-group row">
                    <label class="col-sm-4 col-form-label-lg" for="txLot">
                        Lot</label>
                    <div class="col-sm-6">
                        <input type="text" class="form-control form-control-lg" id="txLot" placeholder="Lot" tabindex="4">
                    </div>
                </div>
                <br />
                <div class="form-group row">
                    <label class="col-sm-4 col-form-label-lg" for="txDateI">
                        Date</label>
                    <div class="col-4">
                        <input type="date" class="form-control form-control-lg" id="txDateI" tabindex="5">
                    </div>
                    <div class="col-4">
                        <input type="date" class="form-control form-control-lg" id="txDateF" tabindex="6">
                    </div>
                </div>
                <br />
                <div class="form-group row">
                    <input type="button" class="btn btn-primary btn-lg col-10" id="btnQuery" value="Query" />&nbsp;
                </div>
                <br />
                <div class="form-group row">
                    <label class="col-sm-12 col-form-label-lg" style="color: red" id="txError"></label>
                </div>
            </div>
        </div>
    </form>
    <table class="table" id="tbReview" style="margin-bottom:100px">
        <thead>
            <tr>
                <th scope="col">Item</th>
                <th scope="col">MRB Warehouse</th>
                <th scope="col">Lot</th>
                <th scope="col">Quantity</th>
                <th scope="col">User</th>
                <th scope="col">Date</th>
                <th scope="col">Disposition</th>
                <th scope="col">Warehouse</th>
                <th scope="col">Regrind Code</th>
                <th scope="col">Pallet ID</th>
                <th scope="col">Pallet ID Disposition</th>
            </tr>
        </thead>
        <tbody id="tbody">
        </tbody>
    </table>
    <script>
        var timer;

        function stoper() {

            clearTimeout(timer);
        }

        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode, dynamicUrl) {
            var options = {
                type: "POST",
                url: "reviewDisposition.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: asyncMode != undefined ? asyncMode : true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }
        var ValidarItemSucces = function (r) {
            console.log(r.d)
        };
        var ValidarLoteSucces = function (r) {
            if (r.d) {
                $("#txError").html("");
            }
            else {
                $("#txError").html("Lot doesn't exist");
            }
        };
        var VerificarTipoWarehouseSucces = function (r) {
            if (r.d) {
                $("#txError").html("");
            }
            else {
                $("#txError").html("Warehouse doesn't exist");
            }
        };
        var VerificarPalletIDSucces = function (r) {
            console.log(r.d)
        };
        var SendSucces = function (r) {
            
            $("#tbody tr").detach();
            var Mylst = JSON.parse(r.d);
            if (Mylst.length > 0) {
                Mylst.forEach(function (row,i) {
                    $('#tbody').append('<tr><td>' + row.item + '</td><td>' + row.cwar + '</td><td>' + row.clot + '</td><td>' + row.qtyr + '</td><td>' + row.logr + '</td><td>' + row.datr + '</td><td>' + row.disp + '</td><td>' + row.stoc + '</td><td>' + row.ritm + '</td><td>' + row.paid + '</td><td>' + row.plld + '</td></tr>');
                });

            }
        }

        $('#txItem').bind("input", function () {
            stoper();
            timer = setTimeout(function () {
                Data = "{'ITEM':'" + $('#txItem').val().trim().toUpperCase() + "'}";
                sendAjax("ValidarItem", Data, ValidarItemSucces, true);
            }
            , 1500);
        });

        $('#txLot').bind("input", function () {
            stoper();
            timer = setTimeout(function () {
                Data = "{'ITEM':'" + $('#txItem').val().trim().toUpperCase() + "','CLOT':'" + $('#txLot').val().trim().toUpperCase() + "'}";
                sendAjax("ValidarLote", Data, ValidarLoteSucces, true);
            }
            , 1500);
        });

        $('#txWare').bind("input", function () {
            stoper();
            timer = setTimeout(function () {
                Data = "{'WARE':'" + $('#txWare').val().trim().toUpperCase() + "'}";
                sendAjax("VerificarTipoWarehouse", Data, VerificarTipoWarehouseSucces, true);
            }
            , 1500);
        });

        $('#txPaid').bind("input", function () {
            stoper();
            timer = setTimeout(function () {
                Data = "{'PAID':'" + $('#txPaid').val().trim().toUpperCase() + "'}";
                sendAjax("VerificarPalletID", Data, VerificarPalletIDSucces, true);
            }
            , 1500);
        });

        $('#btnQuery').bind("click", function (e) {
            Data = "{'ITEM':'" + $('#txItem').val().trim().toUpperCase() + "','WARE':'" + $('#txWare').val().trim().toUpperCase() + "','PAID':'" + $('#txPaid').val().trim().toUpperCase() + "','CLOT':'" + $('#txLot').val().trim().toUpperCase() + "','DATEI':'" + $('#txDateI').val().trim().toUpperCase() + "','DATEF':'" + $('#txDateF').val().trim().toUpperCase() + "'}";
            sendAjax("Send", Data, SendSucces, false);
        });

        var cleanForm = function () {
            $('#txItem').val("");
            $('#txWare').val("");
            $('#txPaid').val("");
            $('#txLot').val("");
            $('#txDateI').val("");
            $('#txDateF').val("");
            $('#txError').html("");

            $("#tbody tr").detach();
        }
        cleanForm();
    </script>

</asp:Content>
