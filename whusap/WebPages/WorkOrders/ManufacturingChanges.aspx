<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="ManufacturingChanges.aspx.cs" Inherits="whusap.WebPages.WorkOrders.ManufacturingChanges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style>
        #toWareDiv {
            display: none;
        }

        #toSaveDiv {
            display: none;
        }

        #toTableDiv {
            display: none;
        }
    </style>
    <div>
        <div class="row p-1">
            <label for="" class="col-2">From Warehouse</label>
            <input class="form-control col-3" type="text" placeholder="From Warehouse" id="txWareFrom">
            <label for="" class="col-2" id="lblWareFrom"></label>
        </div>
        <div id="toWareDiv">
            <div class="row p-1">
                <label for="" class="col-2">To Warehouse</label>
                <input class="form-control col-3" type="text" placeholder="To Warehouse" id="txWareTo">
                <label for="" class="col-2" id="lblWareTo"></label>
            </div>
        </div>
        <div id="">
            <div class="row p-1">
                <label id="lblError" class="col-2" style="color:red"></label>
            </div>
        </div>
        <div id="toSaveDiv">
            <div class="row p-1">
                <div class="col-2 "></div>
                <input type="button" class="btn btn-primary col-3" value="Save" id="btnSave">
            </div>
        </div>
    </div>
    <div class="mt-4" id="toTableDiv">
        <table class="table">
            <thead>
                <tr>
                    <th scope="col">Secuence</th>
                    <th scope="col">Item</th>
                    <th scope="col"></th>
                    <th scope="col">Available  Inv</th>
                    <th scope="col">Unit</th>
                    <th scope="col">Required Quantity</th>
                </tr>
            </thead>
            <tbody>
                <tr row="0">
                    <th scope="row">1</th>
                    <td>
                        <input class="form-control col-10" type="text" id="txItem0" oninput="verifyItem('0')" /></td>
                    <td>
                        <label class="col-12" id="lblItem0"></label>
                    </td>
                    <td>
                        <label id="lblAvalI0"></label>
                    </td>
                    <td>
                        <label id="lblUnit0"></label>
                    </td>
                    <td>
                        <input class="form-control col-3" type="text" id="txReqQty0" /></td>
                </tr>
                <tr row="1">
                    <th scope="row">2</th>
                    <td>
                        <input class="form-control col-10" type="text" id="txItem1" oninput="verifyItem('1')" /></td>
                    <td>
                        <label class="col-12" id="lblItem1"></label>
                    </td>
                    <td>
                        <label id="lblAvalI1"></label>
                    </td>
                    <td>
                        <label id="lblUnit1"></label>
                    </td>
                    <td>
                        <input class="form-control col-3" type="text" id="txReqQty1" /></td>
                </tr>
                <tr row="2">
                    <th scope="row">3</th>
                    <td>
                        <input class="form-control col-10" type="text" id="txItem2" oninput="verifyItem('2')" /></td>
                    <td>
                        <label class="col-12" id="lblItem2"></label>
                    </td>
                    <td>
                        <label id="lblAvalI2"></label>
                    </td>
                    <td>
                        <label id="lblUnit2"></label>
                    </td>
                    <td>
                        <input class="form-control col-3" type="text" id="txReqQty2" /></td>
                </tr>
                <tr row="3">
                    <th scope="row">4</th>
                    <td>
                        <input class="form-control col-10" type="text" id="txItem3" oninput="verifyItem('3')" /></td>
                    <td>
                        <label class="col-12" id="lblItem3"></label>
                    </td>
                    <td>
                        <label id="lblAvalI3"></label>
                    </td>
                    <td>
                        <label id="lblUnit3"></label>
                    </td>
                    <td>
                        <input class="form-control col-3" type="text" id="txReqQty3" /></td>
                </tr>
                <tr row="4">
                    <th scope="row">5</th>
                    <td>
                        <input class="form-control col-10" type="text" id="txItem4" oninput="verifyItem('4')" /></td>
                    <td>
                        <label class="col-12" id="lblItem4"></label>
                    </td>
                    <td>
                        <label id="lblAvalI4"></label>
                    </td>
                    <td>
                        <label id="lblUnit4"></label>
                    </td>
                    <td>
                        <input class="form-control col-3" type="text" id="txReqQty4" /></td>
                </tr>
            </tbody>
        </table>
    </div>
    <script>
        var timer;
        let timer0;
        let timer1;
        let timer2;
        let timer3;
        let timer4;

        function stoper() {

            clearTimeout(timer);
        }

        function stoperD(MyTimer) {

            clearTimeout(MyTimer);
        }

        let initComponents = function () {

            txWareFrom = document.getElementById('txWareFrom');
            lblWareFrom = document.getElementById('lblWareFrom');
            txWareTo = document.getElementById('txWareTo');
            lblWareTo = document.getElementById('lblWareTo');
            btnSave = document.getElementById('btnSave');

            txItem0 = document.getElementById('txItem0');
            lblItem0 = document.getElementById('lblItem0');
            lblAvalI0 = document.getElementById('lblAvalI0');
            lblUnit0 = document.getElementById('lblUnit0');
            txReqQty0 = document.getElementById('txReqQty0');

            txItem1 = document.getElementById('txItem1');
            lblItem1 = document.getElementById('lblItem1');
            lblAvalI1 = document.getElementById('lblAvalI1');
            lblUnit1 = document.getElementById('lblUnit1');
            txReqQty1 = document.getElementById('txReqQty1');

            txItem2 = document.getElementById('txItem2');
            lblItem2 = document.getElementById('lblItem2');
            lblAvalI2 = document.getElementById('lblAvalI2');
            lblUnit2 = document.getElementById('lblUnit2');
            txReqQty2 = document.getElementById('txReqQty2');

            txItem3 = document.getElementById('txItem3');
            lblItem3 = document.getElementById('lblItem3');
            lblAvalI3 = document.getElementById('lblAvalI3');
            lblUnit3 = document.getElementById('lblUnit3');
            txReqQty3 = document.getElementById('txReqQty3');

            txItem4 = document.getElementById('txItem4');
            lblItem4 = document.getElementById('lblItem4');
            lblAvalI4 = document.getElementById('lblAvalI4');
            lblUnit4 = document.getElementById('lblUnit4');
            txReqQty4 = document.getElementById('txReqQty4');

            txWareFrom.addEventListener('input', verifyWareFrom);
            txWareTo.addEventListener('input', verifyWareTo);

            btnSave.addEventListener('click', save);
        }


        function sendAjax(WebMethod, Data, FuncitionSucces) {
            var options = {
                type: "POST",
                url: "ManufacturingChanges.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

        }

        let verifyItem = function (i) {
            txItem = document.getElementById('txItem' + i);
            switch (i) {
                case '0':
                    stoperD(timer0);
                    timer0 = setTimeout(
                        function () {
                            sendAjax('VerifyItem', "{'ITEM':'" + txItem.value.trim() + "','ROW':'" + i + "'}", verifyItemSucces)
                        }
                        , 2000);
                    break;
                case '1':
                    stoperD(timer1);
                    timer1 = setTimeout(
                        function () {
                            sendAjax('VerifyItem', "{'ITEM':'" + txItem.value.trim() + "','ROW':'" + i + "'}", verifyItemSucces)
                        }
                        , 2000);
                    break;
                case '2':
                    stoperD(timer2);
                    timer2 = setTimeout(
                        function () {
                            sendAjax('VerifyItem', "{'ITEM':'" + txItem.value.trim() + "','ROW':'" + i + "'}", verifyItemSucces)
                        }
                        , 2000);
                    break;
                case '3':
                    stoperD(timer3);
                    timer3 = setTimeout(
                        function () {
                            sendAjax('VerifyItem', "{'ITEM':'" + txItem.value.trim() + "','ROW':'" + i + "'}", verifyItemSucces)
                        }
                        , 2000);
                    break;
                case '4':
                    stoperD(timer4);
                    timer4 = setTimeout(
                        function () {
                            sendAjax('VerifyItem', "{'ITEM':'" + txItem.value.trim() + "','ROW':'" + i + "'}", verifyItemSucces)
                        }
                        , 2000);
                    break;
            }

        }

        let verifyItemSucces = function (r) {

            let MyObj = JSON.parse(r.d);
            MytxItem = document.getElementById('txItem' + MyObj.Row);
            MylblItem = document.getElementById('lblItem' + MyObj.Row);
            MylblAvalI = document.getElementById('lblAvalI' + MyObj.Row);
            MylblUnit = document.getElementById('lblUnit' + MyObj.Row);
            MytxReqQty = document.getElementById('txReqQty' + MyObj.Row);

            if (MyObj.Error == false) {

                MytxItem.value = MyObj.ITEM.trim();
                MylblItem.innerHTML = MyObj.DSCA.trim();
                MylblAvalI.innerHTML = MyObj.QTYS;
                MylblUnit.innerHTML = MyObj.UNIT;
                MytxReqQty.value = "";
                $("#lblError").html("");

            }
            else {
                MytxItem.value = "";
                MylblItem.innerHTML = "";
                MylblAvalI.innerHTML = "";
                MylblUnit.innerHTML = "";
                MytxReqQty.value = "";
                $("#lblError").html(twhcol130.Msg);
            }
        }

        let ClearTable = function (r) {

            for (var i = 0; i < 5; i++) {

                MytxItem = document.getElementById('txItem' + i);
                MylblItem = document.getElementById('lblItem' + i);
                MylblAvalI = document.getElementById('lblAvalI' + i);
                MylblUnit = document.getElementById('lblUnit' + i);
                MytxReqQty = document.getElementById('txReqQty' + i);

                MytxItem.value = "";
                MylblItem.innerHTML = "";
                MylblAvalI.innerHTML = "";
                MylblUnit.innerHTML = "";
                MytxReqQty.value = "";

            }

        }

        let save = function () {
            for (var row = 0; row < 5 ; row++) {
                if ($('#txItem' + row).val().trim() != "" && $('#lblUnit' + row).html().trim() != "" && $('#txReqQty' + row).val().trim() != "") {
                    sendAjax('Save', "{'CWOR':'" + $('#txWareFrom').val() + "','CWDE':'" + $('#txWareTo').val() + "','ITEM':'" + $('#txItem' + row).val() + "','OQTY':'" + $('#lblAvalI' + row).html() + "','UNIT':'" + $('#lblUnit' + row).html() + "','RQTY':'" + $('#txReqQty' + row).val() + "'}",null)
                }
            }
            ClearTable();
            txWareFrom.value = "";
            txWareTo.value = "";
            lblWareFrom.innerHTML = "";
            lblWareTo.innerHTML = "";
            $("#toWareDiv").fadeOut(500);
            $("#toSaveDiv").fadeOut(500);
            $("#toTableDiv").fadeOut(500);
        }

        let saveSucces = function (r) {
            ClearTable();
        }

        let verifyWareFrom = function () {
            if (txWareFrom.value.trim() != "") {
                stoper();
                timer = setTimeout(
                    function () {
                        sendAjax('VerificarWarehouse', "{'WARE':'" + txWareFrom.value + "'}", verifyWareFromSucces)
                    }
                    , 2000);
            }
        }

        let verifyWareTo = function () {
            if (txWareTo.value.trim != "" && txWareTo.value.trim() != txWareFrom.value.trim()) {
                stoper();
                timer = setTimeout(
                    function () {
                        sendAjax('VerificarWarehouse', "{'WARE':'" + txWareTo.value + "'}", verifyWareToSucces)
                    }
                    , 2000);
            }
        }

        let verifyWareFromSucces = function (r) {
            let myObj = JSON.parse(r.d);
            if (myObj.Error != true) {
                $("#toWareDiv").fadeIn(500);
                lblWareFrom.innerHTML = myObj.DSCA;
                $("#lblError").html("");
            }
            else {
                $("#toWareDiv").fadeOut(500);
                $("#lblError").html(myObj.MsgError);
            }
        }

        let verifyWareToSucces = function (r) {
            let myObj = JSON.parse(r.d);
            if (myObj.Error != true) {
                $("#toSaveDiv").fadeIn(500);
                $("#toTableDiv").fadeIn(500);
                lblWareTo.innerHTML = myObj.DSCA;
                $("#lblError").html("");
            }
            else {
                $("#toSaveDiv").fadeOut(500);
                $("#toTableDiv").fadeOut(500);
                $("#lblError").html(myObj.MsgError);
            }
        }

        let clearForm = function () {

            txWareFrom.value = '';
            lblWareTo.innerHTML = '';
            txWareFrom.value = '';
            lblWareTo.innerHTML = '';
            //btnSave

            txItem0.value = '';
            lblItem0.innerHTML = '';
            lblAvalI0.innerHTML = '';
            lblUnit0.innerHTML = '';
            txReqQty0.value = '';

            txItem1.value = '';
            lblItem1.innerHTML = '';
            lblAvalI1.innerHTML = '';
            lblUnit1.innerHTML = '';
            txReqQty1.value = '';

            txItem2.value = '';
            lblItem2.innerHTML = '';
            lblAvalI2.innerHTML = '';
            lblUnit2.innerHTML = '';
            txReqQty2.value = '';

            txItem3.value = '';
            lblItem3.innerHTML = '';
            lblAvalI3.innerHTML = '';
            lblUnit3.innerHTML = '';
            txReqQty3.value = '';

            txItem4.value = '';
            lblItem4.innerHTML = '';
            lblAvalI4.innerHTML = '';
            lblUnit4.innerHTML = '';
            txReqQty4.value = '';
        }

        initComponents();

    </script>
</asp:Content>
