﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="PickingConsignmentMaterial.aspx.cs" Inherits="whusap.WebPages.WorkOrders.PickingConsignmentMaterial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="http://code.jquery.com/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="http://cdn.datatables.net/1.10.2/js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/limonte-sweetalert2/7.33.1/sweetalert2.css"
        integrity="sha512-3QG6i4RNIYVKJ4nysdP4qo87uoO+vmEzGcEgN68abTpg2usKfuwvaYU+sk08z8k09a0vwflzwyR6agXZ+wgfLA=="
        crossorigin="anonymous" />
    <link
        rel="stylesheet"
        href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/limonte-sweetalert2/7.33.1/sweetalert2.min.js"
        integrity="sha512-aDa+VOyQu6doCaYbMFcBBZ1z5zro7l/aur7DgYpt7KzNS9bjuQeowEX0JyTTeBTcRd0wwN7dfg5OThSKIWYj3A=="
        crossorigin="anonymous"></script>
    <style>
        #ddWare {
            height: 40px;
            border-radius: 4px;
        }

        #btnStarPicking, #divPicketPending, #formPicking, #reasonLine, #MyEtiqueta, #lblReason, #ddReason, #bntChange,
        #btnConfirm, #txLoca, #txQtyc {
            display: none;
        }

        td, th {
            font-size: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container-fluid col-11 animate__animated animate__fadeInLeft">
        <div class="row" id="formWarehouse">
            <div class="col-5">
                <form>
                    <div class="row">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-6">
                                    <label for="ddWare">Warehouse</label>
                                </div>
                                <div class="col-6">
                                    <div class="col-7 float-right">
                                        <input type="checkbox" class="form-check-input" id="chkConsigment">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <label class="form-check-label float-right" for="chkConsigment">Consigment</label>
                                    </div>
                                </div>
                            </div>

                            <select id="ddWare" class="form-control">
                                <option value="0" selected>Select Warehouse</option>
                            </select>
                        </div>
                    </div>
                    <br>
                    <div id="divStartPicking" class="row">
                        <div class="col-6">
                            <button class="btn btn-primary col-12 btn-sm" id="btnStarPicking" type="button">Start Picking</button>
                        </div>
                    </div>
                </form>
            </div>
        </div>
        <hr />
        <br />
        <div id="divPicketPending" class="col-12">
            <table id="tblPicketPending" class="table animate__animated animate__fadeInLeft" style="width: 100%">
                <thead>
                    <tr>
                        <th scope="col">Picking ID</th>
                        <th scope="col">Warehouse</th>
                        <th scope="col"></th>
                    </tr>
                </thead>
                <tbody id="bdPicketPending">
                </tbody>
            </table>
        </div>
        <div class="row">
            <div class="col-5">
                <form id="formPicking">
                    <div class="row">
                        <div class="col-2">
                            <label>Pick ID</label>
                        </div>
                        <div class="col-2">
                            <label id="lblPick">1030638831</label>
                        </div>
                    </div>
                    <br>
                    <div class="row">
                        <div class="col-2">
                            <label>Pallet ID</label>
                        </div>
                        <div class="col-4" id="lblPaid">
                        </div>
                        <div class="col-6">
                            <input type="text" class="col-12 form-control" id="txPaid" required>
                        </div>
                    </div>
                    <br>
                    <div class="row">
                        <div class="col-2">
                            <label>ITEM</label>
                        </div>
                        <div class="col-10" id="LblItem">
                        </div>
                    </div>
                    <br>
                    <div class="row">
                        <div class="col-2">
                            <label>Location</label>
                        </div>
                        <div class="col-4" id="lblLoca">
                        </div>
                        <div class="col-6">
                            <input type="text" class="col-12 form-control" id="txLoca" required>
                        </div>
                    </div>
                    <br>
                    <div class="row">
                        <div class="col-2">
                            <label>Warehouse</label>
                        </div>
                        <div class="col-2" id="lblWare">
                        </div>
                    </div>
                    <br>
                    <div class="row">
                        <div class="col-2">
                            <label>Quantity</label>
                        </div>
                        <div class="col-2" id="lblQtyc">
                        </div>
                        <div class="col-2" id="lblUnit">
                        </div>
                        <div class="col-6">
                            <input type="text" class="col-12 form-control" id="txQtyc" required>
                        </div>
                    </div>
                    <br>
                    <hr id="reasonLine" />
                    <div id="divReason" class="row">
                        <div class="col-2">
                            <label id="lblReason">Reason</label>
                        </div>
                        <div class="col-6">
                            <select class="form-control" id="ddReason" tabindex="1">
                                <option value="0">Select Reason</option>
                                <option value="1">Wrong Lot</option>
                                <option value="2">Aisle Blocked</option>
                                <option value="3">Wrong Location</option>
                            </select>
                        </div>
                        <div class="col-4">
                            <input id="bntChange" type="button" class="btn btn-primary col-12 btn-sm" onclick="IngresarCausales()"
                                value="Change" />
                        </div>
                    </div>

                    <br>
                    <div class="row">
                        <button class="btn btn-primary col-12 btn-sm mb-1" id="btnConfirm">Confirm</button>
                        <button class="btn btn-primary col-12 btn-sm" id="btnSkipPicking">Skip Picking</button>
                    </div>
                </form>
            </div>
            <div id="divTables" class="col-7">
                <div id="divTableItem" class="col-12">
                </div>
                <br />
                <div id="divTableWarehouse" class="col-12">
                </div>
            </div>
        </div>
        <div class="row">
            <div id="MyEtiqueta">
                <table style="margin: auto">
                    <tr>
                        <td>
                            <%--<label style="font-size: 11px">
                        Pallet ID</label>--%>
                        </td>
                        <td colspan="4">
                            <img src="~/images/logophoenix_login.jpg" runat="server" id="CBPalletNO" alt="" hspace="60"
                                vspace="5" style="width: 4in; height: .5in; margin: 0px !important" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-size: 11px">
                                ITEM</label>
                        </td>
                        <td colspan="4">
                            <label id="lblItemID" style="display: none; font-size: 11px">
                            </label>
                            <img src="~/images/logophoenix_login.jpg" runat="server" id="CBItem" alt="" hspace="60"
                                vspace="5" style="width: 4in; height: .5in; margin: 0px !important" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="center">
                            <label id="lblItemDesc" style="font-size: 14px">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-size: 11px">
                                QUANTITY</label>
                        </td>
                        <td colspan="4">

                            <img src="~/images/logophoenix_login.jpg" runat="server" id="CBQuantity" alt="" hspace="60"
                                vspace="5" style="width: 1in; height: .5in; margin: 0px !important" />
                            <label id="LblQuantity" style="display: none; font-size: 11px">
                            </label>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="center">
                            <label id="LblUnit" style="font-size: 11px">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-size: 11px">
                                LOT</label>
                        </td>
                        <td>
                            <label id="LblLotId" style="display: none; font-size: 11px">
                            </label>
                            <img src="~/images/logophoenix_login.jpg" runat="server" id="CBLot" alt="" hspace="60"
                                vspace="5" style="width: 4in; height: .5in; margin: 0px !important" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-size: 9px">
                                RECEIPT DATE</label>
                        </td>
                        <td>
                            <label id="LblDate" style="font-size: 9px">
                            </label>
                        </td>
                        <!--<td>
                    <label>
                        REPRINT:</label>
                </td>
                <td>
                    <label id="LblReprint">
                    </label>
                </td>-->
                    </tr>
                </table>
                <br />
                <br />
                <table style="margin: auto">
                    <tr>
                        <td>
                            <%--<label style="font-size: 11px">
                        Pallet ID</label>--%>
                        </td>
                        <td colspan="4">
                            <img src="~/images/logophoenix_login.jpg" runat="server" id="CBPalletNO2" alt="" hspace="60"
                                vspace="5" style="width: 4in; height: .5in; margin: 0px !important" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-size: 11px">
                                ITEM</label>
                        </td>
                        <td colspan="4">
                            <label id="lblItemID" style="display: none; font-size: 11px">
                            </label>
                            <img src="~/images/logophoenix_login.jpg" runat="server" id="CBItem2" alt="" hspace="60"
                                vspace="5" style="width: 4in; height: .5in; margin: 0px !important" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="center">
                            <label id="lblItemDesc2" style="font-size: 14px">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-size: 11px">
                                QUANTITY</label>
                        </td>
                        <td colspan="4">

                            <img src="~/images/logophoenix_login.jpg" runat="server" id="CBQuantity2" alt="" hspace="60"
                                vspace="5" style="width: 1in; height: .5in; margin: 0px !important" />
                            <label id="LblQuantity2" style="display: none; font-size: 11px">
                            </label>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="center">
                            <label id="LblUnit2" style="font-size: 11px">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-size: 11px">
                                LOT</label>
                        </td>
                        <td>
                            <label id="LblLotId2" style="display: none; font-size: 11px">
                            </label>
                            <img src="~/images/logophoenix_login.jpg" runat="server" id="CBLot2" alt="" hspace="60"
                                vspace="5" style="width: 4in; height: .5in; margin: 0px !important" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-size: 9px">
                                RECEIPT DATE</label>
                        </td>
                        <td>
                            <label id="LblDate2" style="font-size: 9px">
                            </label>
                        </td>
                        <!--<td>
                    <label>
                        REPRINT:</label>
                </td>
                <td>
                    <label id="LblReprint">
                    </label>
                </td>-->
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <script>
        var cnpk = "";
        var paidOk = false;
        var locaOk = false;
        var qtytOk = false;
        var EventoAjax = function (Method, Data, MethodSuccess) {
            $.ajax({
                type: "POST",
                url: "PickingConsignmentMaterial.aspx/" + Method.trim(),
                data: Data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: MethodSuccess
            })
        };

        var StartComponents = function () {
            var formWarehouse = document.getElementById("formWarehouse");
            var chkConsigment = document.getElementById("chkConsigment");
            var ddWare = document.getElementById("ddWare");
            var pickingResult = document.getElementById("pickingResult");
            var formPicking = document.getElementById("formPicking");
            var lblPick = document.getElementById("lblPick");
            var lblPaid = document.getElementById("lblPaid");
            var txPaid = document.getElementById("txPaid");
            var LblItem = document.getElementById("LblItem");
            var lblLoca = document.getElementById("lblLoca");
            var txLoca = document.getElementById("txLoca");
            var lblWare = document.getElementById("lblWare");
            var lblQtyc = document.getElementById("lblQtyc");
            var lblUnit = document.getElementById("lblUnit");
            var txQtyc = document.getElementById("txQtyc");
            var btnStarPicking = document.getElementById("btnStarPicking");
            var btnConfirm = document.getElementById("btnConfirm");
            var btnSkipPicking = document.getElementById("btnSkipPicking");
            var bdPicketPending = document.getElementById("bdPicketPending");
            //var btnNextPicking = document.getElementById("btnNextPicking");
            //var btnEndPicking = document.getElementById("btnEndPicking");
            //var roetest = document.getElementById("roetest");
            //var tblPickingsItems = document.getElementById("tblPickingsItems");
            //var tblPickingsToBeProcessedWarehouse = document.getElementById("tblPickingsToBeProcessedWarehouse");

            btnStarPicking.addEventListener("click", loadPage);
            btnConfirm.addEventListener("click", Confirm);
            //btnSkipPicking.addEventListener("click", );
            //btnNextPicking.addEventListener("click", loadPage);
            //btnEndPicking.addEventListener("click", );f
            ddWare.addEventListener("change", loadPicksPending);
            txPaid.addEventListener("input", verifyPallet);
            txLoca.addEventListener("input", VerifyLocation);
            txQtyc.addEventListener("input", VerifyQuantity);
            chkConsigment.addEventListener('change', GetWarehouse)
            //$("#btnEndPicking").hide();
            //$("#btnNextPicking").hide();
        }

        var GetWarehouse = function () {
            $("#btnStarPicking").hide(500);
            $('#divPicketPending').hide(500);
            for (let i = ddWare.options.length; i > 0; i--) {
                ddWare.remove(i);
            }
            chkConsigment.checked == true ? EventoAjax("GetWarehouse", "{'consigment':'true'}", SucceessGetWarehouse) : EventoAjax("GetWarehouse", "{'consigment':'false'}", SucceessGetWarehouse);
        }

        var SucceessGetWarehouse = function (r) {
            var MylistWarehouse = JSON.parse(r.d);
            MylistWarehouse.forEach(function (e) {
                var option = document.createElement("option");
                option.text = e
                option.value = e
                ddWare.add(option);
            });
        }

        function verifyPallet() {
            var CurrentPaid = lblPaid.innerHTML.trim();
            var NewPaid = txPaid.value.trim();
            if (CurrentPaid == NewPaid) {
                paidValid();
            }
            else {
                console.log("Pallet NO Igual");
                Method = "VerificarExistenciaPalletID"
                Data = "{'PAID_NEW':'" + NewPaid + "'}";
                EventoAjax(Method, Data, PalletIDSuccess)
            }
        }

        var PalletIDSuccess = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.error == false) {
                console.log("Pallet exists");
                validElement(txPaid);
                locaInvalid();
                hideShowNeutroInputText(txQtyc, false);
                hideShowNeutroInputText(txLoca, false);
                hideShowNeutroSelect(ddReason, true);
            }
            else if (MyObj.error == true) {
                paidInvalid();
                console.log("Pallet not exists");
            }
        }

        var VerificarPalletIDSuccess = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.error == false) {
                console.log("ESTADOS CAMBIADOS");
                cnpk = MyObj.CNPK;
                lblPick.innerHTML = MyObj.PICK;
                lblPaid.innerHTML = MyObj.PALLETID;
                LblItem.innerHTML = MyObj.ITEM + "" + MyObj.DSCA;
                lblLoca.innerHTML = MyObj.LOCA;
                lblWare.innerHTML = MyObj.WRH;
                lblQtyc.innerHTML = MyObj.QTYT;
                lblUnit.innerHTML = MyObj.UN;
                //(MyObj.CNPK == "1") ? $('#txQtyc').hide(500) : $('#txQtyc').show(500);
                ClearFormPicking();
                ShowCurrentOptionsItem();
                ShowCurrentOptionsWarehouse();
            }
            else if (MyObj.error == true) {
                console.log("Pallet not exists");

            }
        }

        function ShowCurrentOptionsItem() {
            divTableItem.innerHTML = '';
            var bodyRows = ""
            $.ajax({
                type: "POST",

                url: "PickingConsignmentMaterial.aspx/ShowCurrentOptionsItem",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    myObj = JSON.parse(response.d);
                    window.localStorage.setItem('MyPalletList', JSON.stringify(myObj));
                    for (var i = 0; i < myObj.length; i++) {
                        bodyRows += "<tr onClick='selectNewPallet(this)' id='rowNum" + i + "'><td>" + myObj[i].PALLETID + "</td><td>" + myObj[i].LOCA + "</td><td>" + myObj[i].ITEM + "</td><td>" + myObj[i].DESCRIPTION + "</td><td>" + myObj[i].QTY + "</td><td>" + myObj[i].UN + "</td></tr>";
                    }
                    var tableOptions = "<table id ='tblItems' class='table animate__animated animate__fadeIn' style='width:100% display:none'>" +
                                                "<thead>" +
                                                  "<tr>" +
                                                    "<th scope='col'>Pallet</th>" +
                                                    "<th scope='col'>Location</th>" +
                                                    "<th scope='col'>Item</th>" +
                                                    "<th scope='col'>Description</th>" +
                                                    "<th scope='col'>Quantity</th>" +
                                                    "<th scope='col'>Unit</th>" +
                                                "</tr>" +
                                               "</thead>" +
                                               "<tbody>" +
                                               bodyRows
                    "</tbody>" +
                                            "</table>";


                    $("#divTableItem").append(tableOptions);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        function ShowCurrentOptionsWarehouse() {
            var bodyRows = ""
            divTableWarehouse.innerHTML = '';
            $.ajax({
                type: "POST",

                url: "PickingConsignmentMaterial.aspx/GetAllsPAlletsPending",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var dropPending = false;
                    myObj = JSON.parse(response.d);
                    window.localStorage.setItem('MyPalletList', JSON.stringify(myObj));
                    for (var i = 0; i < myObj.length; i++) {

                        if (myObj[i].T$STAT == 1) {
                            bodyRows += "<tr onClick='selectNewPallet(this)' id='rowNum" + i + "'><td>" + myObj[i].T$ORNO + "</td><td>" + myObj[i].T$MCNO + "</td><td>" + myObj[i].T$CWAR + "</td><td>" + myObj[i].T$ITEM + "</td><td></td><td>" + myObj[i].T$QTYT + "</td><td>" + myObj[i].T$UNIT + "</td><td>" + myObj[i].T$PAID + "</td><td></td></tr>";
                        }
                        else {
                            dropPending = true;
                            bodyRows += "<tr onClick='selectNewPallet(this)' id='rowNum" + i + "'><td>" + myObj[i].T$ORNO + "</td><td>" + myObj[i].T$MCNO + "</td><td>" + myObj[i].T$CWAR + "</td><td>" + myObj[i].T$ITEM + "</td><td></td><td>" + myObj[i].T$QTYT + "</td><td>" + myObj[i].T$UNIT + "</td><td>" + myObj[i].T$PAID + "</td><td><button class='btn btn-danger col-12 btn-sm' type='button' id='btnPickingPending" + i + "'>Drop</button></td></tr>";
                        }
                    }
                    var tableOptions = "<table id ='tblWare' class='table animate__animated animate__fadeInt' style='width:100%;'>" +
                                                "<thead>" +
                                                  "<tr>" +
                                                    "<th scope='col'>Work Order</th>" +
                                                    "<th scope='col'>Machine</th>" +
                                                    "<th scope='col'>Warehouse</th>" +
                                                    "<th scope='col'>Item</th>" +
                                                    "<th scope='col'>Description</th>" +
                                                    "<th scope='col'>Quantity</th>" +
                                                    "<th scope='col'>Unit</th>" +
                                                    "<th scope='col'>Pallet ID</th>" +
                                                    "<th scope='col'></th>" +
                                                "</tr>" +
                                               "</thead>" +
                                               "<tbody>" +
                                               bodyRows +
                    "</tbody>" +
                                            "</table>";
                    dropPending == true ? tableOptions += "<button class='btn btn-danger col-12 btn-sm animate__animated animate__fadeIn' type='button' id='btnPickingPending" + i + "'>Drop</button>" : tableOptions;


                    $("#divTableWarehouse").append(tableOptions);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        var loadPage = function () {
            EventoAjax("loadPage", "{'CWAR':'" + ddWare.value + "'}", LoadPageSuccess);
        }

        var LoadPageSuccess = function (r) {
            var divTables = document.getElementById('divTables');
            ddWare.value = 0;
            $("#btnStarPicking").hide(500);
            var MyObj = r.d;
            if (MyObj.error == false) {
                $('#divPicketPending').hide(100);
                lblPick.innerHTML = MyObj.PICK;
                if (MyObj.PICK != null) {
                    lblPaid.innerHTML = MyObj.PALLETID;
                    LblItem.innerHTML = MyObj.ITEM;
                    lblLoca.innerHTML = MyObj.LOCA;
                    lblWare.innerHTML = MyObj.WRH;
                    lblQtyc.innerHTML = MyObj.QTYT;
                    lblUnit.innerHTML = MyObj.UN;
                    //(MyObj.CNPK == "1") ? $('#txQtyc').hide(500) : $('#txQtyc').show(500);
                    divTables.classList.add("col-7");
                    divTables.classList.remove("col-12");
                    $("#formPicking").show(500);
                    ShowCurrentOptionsItem();
                } else {
                    
                    divTables.classList.remove("col-7");
                    divTables.classList.add("col-12");
                }
                   
                ShowCurrentOptionsWarehouse();
            }
            else {
                $("#formPicking").hide(100);
            }
        }

        var loadPicksPending = function () {
            if (ddWare.value == "0") {
                $("#btnStarPicking").hide(100);
                $('#divPicketPending').hide(100);
                $("#formPicking").hide(100);
                divTableWarehouse.innerHTML = '';
                divTableItem.innerHTML = '';
            }
            else {
                $("#formPicking").hide(100);
                divTableWarehouse.innerHTML = '';
                divTableItem.innerHTML = '';
                EventoAjax("loadPicksPending", "{'CWAR':'" + ddWare.value + "'}", loadPicksPendingSuccess);
            }
        }

        var loadPicksPendingSuccess = function (r) {

            var MyObjLst = JSON.parse(r.d);
            if (bdPicketPending.childElementCount > 0) {
                for (let i = bdPicketPending.childElementCount - 1; i >= 0 ; i--) {
                    bdPicketPending.children[i].remove()
                }
            }
            $("#btnStarPicking").show(100);
            var bodyRows = "";
            if (MyObjLst.length > 0) {
                $('#divPicketPending').show(100);
                MyObjLst.forEach(function (item, i) {

                    if (item.T$STAT == 1) {
                        bodyRows += "<tr onClick='selectPicksPending(this)' row = '" + i + "' id='rowNum" + i + "' class = 'animate__animated animate__fadeInLeft'><td>" + item.T$PAID + "</td><td>" + item.T$CWAR + "</td><td><button class='btn btn-primary col-12 btn-sm' type='button' id='btnPickingPending" + i + "'>Take</button></td>";
                    }
                    else {
                        bodyRows += "<tr onClick='selectPicksPending(this)' row = '" + i + "' id='rowNum" + i + "' class = 'animate__animated animate__fadeInLeft'><td>" + item.T$PAID + "</td><td>" + item.T$CWAR + "</td><td><button class='btn btn-primary col-12 btn-sm' type='button' id='btnPickingPending" + i + "' disabled>Take</button></td>";
                    }
                });
            }
            else {
                $('#divPicketPending').hide(100);
            }
            $("#bdPicketPending").append(bodyRows);
        }

        var selectPicksPending = function (e) {
            ClearFormPicking();
            EventoAjax("ClickPickingPending", "{'PICK':'" + e.children[0].innerHTML.trim() + "','CWAR':'" + e.children[1].innerHTML.trim() + "'}", LoadPageSuccess);
        }

        var ClearFormPicking = function () {
            txPaid.value = "";
            txQtyc.value = "";
            txLoca.value = "";

            lblPick.innerHTML = "";
            lblPaid.innerHTML = "";
            LblItem.innerHTML = "";
            lblLoca.innerHTML = "";
            lblWare.innerHTML = "";
            lblQtyc.innerHTML = "";
            lblUnit.innerHTML = "";
            paidInvalid();
            NeutroInputText(txPaid);
            NeutroInputText(txQtyc);
            NeutroInputText(txLoca);

            hideShowNeutroSelect(ddReason, false);
        }

        var ClearFormWarehouse = function () {

        }

        var SuccessGetWarehouse = function () {

        }

        var StartPicking = function () {

        }

        var SucceessStartPicking = function () {

        }

        var VerifyPalletID = function () {

        }

        var SucceessVerifyPalletID = function () {

        }

        var VerifyLocation = function () {
            var CurrentLoca = lblLoca.innerHTML.trim();
            var NewLoca = txLoca.value.trim();
            if (CurrentLoca == NewLoca) {
                locaValid();
            }
            else {
                console.log("Pallet NO Igual");
                Method = "VerificarLocate"
                Data = "{'CWAR':'" + lblWare.innerHTML.trim() + "','LOCA':'" + txLoca.value.trim() + "'}";
                EventoAjax(Method, Data, SucceessVerifyLocation)
            }
        }

        var SucceessVerifyLocation = function (r) {
            if (r.d == true) {
                console.log("Location valida");
                locaValid();
                hideShowNeutroSelect(ddReason, true);
                hideShowNeutroInputText(txQtyc, false);
            }
            else {
                console.log("Location no valida");
                locaInvalid();
            }
        }

        var VerifyQuantity = function () {
            var qtycAct = parseFloat(lblQtyc.innerHTML.trim());
            var qtycNew = parseFloat(txQtyc.value.trim());
            if (qtycAct >= qtycNew && qtycNew > 0) {
                qtytValid();
            }
            else {
                qtytInvalid();
            }
        }

        var SucceessVerifyQuantity = function () {

        }

        var Confirm = function () {
            if (parseFloat($("#Contenido_lblQuantity").val()) <= 0) {
                $("#Contenido_lblQuantity").focus();
                $('#LblError').html("The quantity cann´t be empty, zero less than zero");
                return;
            }

            dataS = "{}";

            //"'CUNI':'" + $('#Contenido_lblQuantityDesc').html() + "', 'LOCA':'" + $('#Contenido_lbllocation').html() + "', 'CWAR':'" + $('#Contenido_lblWarehouse').html() + "', 'CLOT':'" + $('#Contenido_LblLotId').html() + "'"
            $.ajax({
                type: "POST",
                url: "PickingConsignmentMaterial.aspx/Click_confirPKG",
                data: dataS,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var myObj = JSON.parse(response.d)
                    if (myObj.Error == false) {
                        ////window.location = "/WebPages/Login/whMenuI.aspx";
                        var newCant = parseFloat($("#Contenido_lblQuantity").val());
                        var oldCant = parseFloat($("#Contenido_lblQuantityAux").html());

                        $('#Contenido_CBPalletNO').attr("src", myObj.PAID_OLD_URL);
                        $('#Contenido_CBItem').attr("src", myObj.ITEM_URL);
                        //$('#lblItemDesc').html($('#Contenido_lblItemDesc').html());
                        $('#Contenido_CBQuantity').attr("src", myObj.QTYC_URL);
                        $('#LblUnit').html($('#Contenido_lblQuantityDesc').html());

                        $('#Contenido_CBLot').attr("src", myObj.CLOT_URL);
                        if (myObj.CLOT_URL != undefined) {
                            myObj.CLOT_URL.trim() == "" ? $('#Contenido_CBLot').hide() : $('#Contenido_CBLot').show();
                        }
                        //$('#LblDate').html();

                        $('#Contenido_CBPalletNO2').attr("src", myObj.PAID_URL);
                        $('#Contenido_CBItem2').attr("src", myObj.ITEM_URL);
                        $('#lblItemDesc2').html($('#Contenido_lblItemDesc').html());
                        $('#Contenido_CBQuantity2').attr("src", myObj.QTYC1_URL);
                        $('#LblUnit2').html($('#Contenido_lblQuantityDesc').html());

                        $('#Contenido_CBLot2').attr("src", myObj.CLOT_URL);
                        if (myObj.CLOT_URL != undefined) {
                            myObj.CLOT_URL.trim() == "" ? $('#Contenido_CBLot2').hide() : $('#Contenido_CBLot2').show();
                        }
                        //$('#LblDate2').html();
                        var newCant = parseFloat($("#Contenido_lblQuantity").val());
                        var oldCant = parseFloat($("#Contenido_lblQuantityAux").html());
                        alert("Information saved successfully");
                        //console.log(myObj.urpt);

                        if (myObj.qtyaG > 0) {
                            printDiv("MyEtiqueta");
                        }




                        $('#Contenido_lblPalletID').html("");
                        $('#Contenido_lblItemID').html("");
                        $('#Contenido_LblLotId').html("");
                        $('#Contenido_lblWarehouse').html("");
                        $('#Contenido_lbllocation').html("");
                        $('#Contenido_lblQuantity').html("");
                        $('#Contenido_lblItemDesc').html("");
                        $('#Contenido_lblWareDescr').html("");
                        $('#Contenido_lblQuantityDesc').html("");
                        $('#Contenido_txtPalletID').val("");
                        $('#txtlocation').val("");

                    }
                    else {
                        alert(myObj.errorMsg);
                        $('#Contenido_lblPalletID').html("");
                        $('#Contenido_lblItemID').html("");
                        $('#Contenido_LblLotId').html("");
                        $('#Contenido_lblWarehouse').html("");
                        $('#Contenido_lbllocation').html("");
                        $('#Contenido_lblQuantity').html("");
                        $('#Contenido_lblItemDesc').html("");
                        $('#Contenido_lblWareDescr').html("");
                        $('#Contenido_lblQuantityDesc').html("");
                        $('#Contenido_txtPalletID').val("");
                        $('#txtlocation').val("");

                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        var SucceessConfirm = function () {

        }

        var SkipPicking = function () {

        }

        var SucceessSkipPicking = function () {

        }


        var paidInvalid = function () {
            if (txPaid.value.trim() != "") {
                invalidElement(txPaid);
                hideShowNeutroInputText(txLoca, false);
                hideShowNeutroInputText(txQtyc, false);
                hideShowNeutroSelect(ddReason, false);
                paidOk = false;
                locaOk = false;
                qtytOk = false;
                formValid();
            }
        }

        var paidValid = function () {
            if (txPaid.value.trim() != "") {
                validElement(txPaid);
                hideShowNeutroInputText(txLoca, true);
                hideShowNeutroInputText(txQtyc, false);
                hideShowNeutroSelect(ddReason, false);
                paidOk = true;
                locaOk = false;
                qtytOk = false;
                formValid();
            }
        }

        var locaInvalid = function () {
            if (txLoca.value.trim() != "") {
                invalidElement(txLoca);
                hideShowNeutroInputText(txQtyc, false);
                hideShowNeutroSelect(ddReason, false);
                locaOk = false;
                qtytOk = false;
                formValid();
            }
        }

        var locaValid = function () {
            if (txLoca.value.trim() != "") {
                validElement(txLoca);
                cnpk == "1" ? hideShowNeutroInputText(txQtyc, false) : hideShowNeutroInputText(txQtyc, true);
                hideShowNeutroSelect(ddReason, false);
                locaOk = true;
                qtytOk = false;
                formValid();
            }
        }

        var qtytInvalid = function () {
            if (txQtyc.value.trim() != "") {
                invalidElement(txQtyc);
                qtytOk = false;
                formValid();
            }
            else {
                NeutroInputText(txQtyc);
                qtytOk = false;
                formValid();
            }
        }

        var qtytValid = function () {
            if (txQtyc.value.trim() != "") {
                validElement(txQtyc);
                qtytOk = true;
                formValid();
            }
            else {
                NeutroInputText(txQtyc);
                qtytOk = false;
                formValid();
            }
        }


        var hideShowNeutroSelect = function (elemnt, show) {
            hideShowElement(elemnt, show);
            NeutroSelect(elemnt);
            if (show) {
                $(lblReason).show(100);
                $(ddReason).show(100);
                $(bntChange).show(100);
            } else {
                $(lblReason).hide(100);
                $(ddReason).hide(100);
                $(bntChange).hide(100);
            }
        }


        var hideShowNeutroInputText = function (elemnt, show) {
            hideShowElement(elemnt, show);
            NeutroInputText(elemnt);
        }

        var hideShowElement = function (elemnt, show) {
            if (show) {
                $(elemnt).show(100);
            }
            else {
                $(elemnt).hide(100);
            }
        }

        var validElement = function (elemnt) {
            elemnt.classList.remove('is-invalid');
            elemnt.classList.add('is-valid');
        }

        var invalidElement = function (elemnt) {
            elemnt.classList.remove('is-valid');
            elemnt.classList.add('is-invalid');
        }

        var NeutroInputText = function (elemnt) {
            elemnt.value = "";
            elemnt.classList.remove('is-invalid');
            elemnt.classList.remove('is-valid');
        }

        var NeutroSelect = function (elemnt) {
            elemnt.value = "0";
            elemnt.classList.remove('is-invalid');
            elemnt.classList.remove('is-valid');
        }

        function formValid() {
            if (cnpk == "1") {
                if (paidOk == true && locaOk == true) {
                    $('#btnConfirm').show(300);
                }
                else {
                    $('#btnConfirm').hide(300);
                }
            }
            else if (paidOk == true && locaOk == true && qtytOk == true) {
                $('#btnConfirm').show(300);
            }
            else {
                $('#btnConfirm').hide(300);
            }
        }

        function IngresarCausales() {
            $.ajax({
                type: "POST",
                url: "PickingConsignmentMaterial.aspx/Click_confirCausal",
                data: "{'PAID':'" + txPaid.value.toString() +
                      "','Causal':'" + document.getElementById("ddReason").value +
                      "','txtPallet':'" + txPaid.value +
                      "','LOCA':'" + lblLoca.innerHTML.toString() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d == true) {
                        var CurrentPaid = lblPaid.innerHTML.trim();
                        var NewPaid = txPaid.value.trim();
                        Method = "VerificarPalletID"
                        Data = "{'PAID_NEW':'" + NewPaid + "','PAID_OLD':'" + CurrentPaid + "','selectOptionPallet' : 'false'}";
                        EventoAjax(Method, Data, VerificarPalletIDSuccess);
                    }
                    else {
                    }
                },
                failure: function (response) {
                    //alert(response.d);
                    //document.getElementById("btnconfirPKG").disabled = true;

                }
            });
        }
        $(function () {
            StartComponents();
            //loadPicksPending();
            GetWarehouse();
        });
    </script>
</asp:Content>