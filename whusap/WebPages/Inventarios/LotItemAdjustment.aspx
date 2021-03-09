<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="LotItemAdjustment.aspx.cs" Inherits="whusap.WebPages.Inventarios.LotItemAdjustment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
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
    </style>
    <form id="form1" class="container">
        <div class="form-group row">
            <label class="col-sm-2 col-form-label-lg" for="txPalletID">
                Pallet ID</label>
            <div class="col-sm-4">
                <input type="text" class="form-control form-control-lg" id="txPalletID" placeholder="Pallet ID"
                    data-method="ValidarQuantity">
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
                    <th scope="col">Item</th>
                    <th scope="col">Lot</th>
                    <th scope="col">Actual Qty</th>
                    <th scope="col">Unit</th>
                    <th scope="col">Warehouse</th>
                    <th scope="col">Location</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">Actual Data</th>
                    <td id="lbItemActual">OOBPW-00600014</td>
                    <td id="lbLotActual">OO0003923</td>
                    <td id="lbQtyActual">18</td>
                    <td id="lbUnitActual">CJ</td>
                    <td id="lbWarehouseActual">WFV900</td>
                    <td id="lbLocaActual">D300110303</td>
                </tr>
                <tr>
                    <th scope="row">Adjusted Data</th>
                    <td id="lbItemAdjusted">OOBPW-00600014</td>
                    <td id="lbLotAdjusted">OO0003923</td>
                    <td id="lbQtyAdjusted">18</td>
                    <td id="lbUnitAdjusted">CJ</td>
                    <td id="lbWarehouseAdjusted">WFV900</td>
                    <td id="lbLocaAdjusted">D300110303</td>
                </tr>
                <tr>
                    <th scope="row">Reason Code</th>
                    <td colspan="2"><asp:DropDownList runat="server" ID="dropDownReasonCodes" CssClass="TextBoxBig"></asp:DropDownList></td>
                </tr>
                <tr style="border-top:none">
                    <th scope="row">Cost Center</th>
                    <td colspan="2"><asp:DropDownList runat="server" ID="dropDownCostCenters" CssClass="TextBoxBig"></asp:DropDownList></td>
                </tr>
                <tr style="border-top:none">
                    <th scope="row" colspan="2"><button id="btnSave" type="button" class="btn btn-primary col-8">Save</button></th>
                </tr>
            </tbody>
        </table>
    </div>
    <script type="text/javascript">
        var waitSecontsPallet;
        function IdentificarControles() {


            //MyEtiquetaOC = $('#MyEtiquetaOC');
            //MyEtiqueta = $('#MyEtiqueta');

            //Formulario
            txPalletID = document.getElementById("txPalletID");
            lbItemActual = document.getElementById("lbItemActual");
            lbLotActua = document.getElementById("lbLotActua");
            lbQtyActual = document.getElementById("lbQtyActual");
            lbUnitActual = document.getElementById("lbUnitActual");
            lbWarehouseActual = document.getElementById("lbWarehouseActual");
            lbLocaActual = document.getElementById("lbLocaActual");
            lbItemAdjusted = document.getElementById("lbItemAdjusted");
            lbLotAdjusted = document.getElementById("lbLotAdjusted");
            lbQtyAdjusted = document.getElementById("lbQtyAdjusted");
            lbUnitAdjusted = document.getElementById("lbUnitAdjusted");
            lbWarehouseAdjusted = document.getElementById("lbWarehouseAdjusted");
            lbLocaAdjusted = document.getElementById("lbLocaAdjusted");
            dropDownReasonCodes = document.getElementById("dropDownReasonCodes");
            dropDownCostCenters = document.getElementById("dropDownCostCenters");
            btnSave = document.getElementById("btnSave");

            txPalletID.addEventListener("input", verifyPallet, false);
            btnSave.addEventListener("click", sendInfo, false);

        }

        var verifyPallet = function (e) {
            
            clearTimeout(waitSecontsPallet);
            waitSecontsPallet = setTimeout(sendPAlletID, 10000);
        }

        var sendPAlletID = function (e) {
            var Data = "{'PAID':'" + txPalletID.value.trim().toUpperCase() + "'}";
            sendAjax("LotItemAdjustment.aspx/verifyPallet", Data, verifyPalletSuccess)
        }

        var verifyPalletSuccess = function (res) {
            var MyObj = JSON.parse(res.d);
            lbItemActual.textContent = MyObj.ITEM + " - " + MyObj.DSCA;
            lbLotActual.textContent = MyObj.CLOT;
            lbQtyActual.textContent = MyObj.QTYA;
            lbUnitActual.textContent = MyObj.UNIT;
            lbWarehouseActual.textContent = MyObj.CWAR + " - " + MyObj.DSCAW;
            lbLocaActual.textContent = MyObj.LOCA;
        }

        var sendInfo = function (e) {
            console.log(e.data);
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
