﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="whInvReceiptRawMaterialNew.aspx.cs" Inherits="whusap.WebPages.InvReceipts.whInvReceiptRawMaterialNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <script type="text/javascript">
        var timer;

        function stoper() {

            clearTimeout(timer);
        }

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

            var mywindow = window.open('','targetWindow','toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400')
            mywindow.document.write('<html><head><title>' + document.title + '</title>');

            
            mywindow.document.write('<link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />'+
                                    '<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" crossorigin="anonymous">'+
                                    '<style type="text/css">' +
                                    '#MyEtiqueta label {'+
                                    'font-size: 15px;'+
                                    '}'+
                                    '#LblDate {'+
                                    'font-size: 14px !important;'+
                                    '}'+
                                    '#LblReprintInd,'+
                                    '#LblReprint {'+
                                    'display: none;'+
                                    '}'+
                                    '.isValid {'+
                                    'border-bottom: solid;'+
                                    'border-color: green;'+
                                    '}'+
                                    '.isNotValid {'+
                                    'border-bottom: solid;'+
                                    'border-color: red;'+
                                    '}'+
                                    '.fa-check {'+
                                    'color: green;'+
                                    '}'+
                                    '.fa-times {'+
                                    'color: red;'+
                                    '}'+
                                    '#checkItem,'+
                                    '#checkLot,'+
                                    '#checkWarehouse,'+
                                    '#checkLoca,'+
                                    '#checkPaid {'+
                                    'display: none;'+
                                    '}'+
                                    '#exItem,'+
                                    '#exLot,'+
                                    '#exWarehouse,'+
                                    '#exLoca,'+
                                    '#exPaid {'+
                                    'display: none;'+
                                    '}'+
                                    '#loadItem,'+
                                    '#loadLot,'+
                                    '#loadWarehouse,'+
                                    '#loadLoca,'+
                                    '#loadPaid {'+
                                    'display: none;'+
                                    '}'+
                                    'tr {'+
                                    'text-align: center;'+
                                    '}'+
                                    'th {'+
                                    'text-align: center;'+
                                    '}'+
                                    '#myLabel {'+
                                    'width: 6in;'+
                                    'height: 4in;'+
                                    'padding: 20px;'+
                                    'border: 1px solid black;'+
                                    'border-radius: 12px;'+
                                    '}'+
                                    '.alingRight {'+
                                    'text-align: right;'+
                                    '}'+
                                    '.alingLeft {'+
                                    'text-align: left;'+
                                    '}'+
                                    '#printButton {'+
                                    'width: 6in;'+
                                    '}'+
                                    '#codePaid {'+
                                    'display: block;'+
                                    'margin: auto;'+
                                    'height: 150px;'+
                                    'width: 500px;'+
                                    '}'+
                                    '#codeItem {'+
                                    'display: block;'+
                                    'margin: auto;'+
                                    'height: 75px;'+
                                    'width: 250px;'+
                                    '}'+
                                    '#itemDesc {'+
                                    'vertical-align: middle;'+
                                    'font-size: 21px;'+
                                    '}'+
                                    '.divDesc {'+
                                    'text-align: center;'+
                                    '}'+
                                    '#lblDesc {'+
                                    '}'+
                                    '#lblMadein {'+
                                    '}'+
                                    '.borderTop {'+
                                    'border-top: solid 1px gray;'+
                                    '}'+
                                    '#printContainer {'+
                                    'margin-bottom: 100px;'+
                                    '}'+
                                    '#editTable {'+
                                    'display: none;'+
                                    '}'+
                                    '#lblError {'+
                                    'color: red;'+
                                    'font-size: 13px;'+
                                    '}'+
                                    '.load {'+
                                    'width: 10px;'+
                                    'height: 10px;'+
                                    'align-content: center;'+
                                    'animation-name: spin;'+
                                    'animation-duration: 5000ms;'+
                                    'animation-iteration-count: infinite;'+
                                    'animation-timing-function: linear;'+
                                    '}'+
                                    '#saveSection {'+
                                    'display: none;'+
                                    '}'+
                                    '.notBorderBottom {'+
                                    'border-bottom: none;'+
                                    '}'+
                                    '#divBarcode {'+
                                    '--height: 186px;'+
                                    'padding: inherit;'+
                                    '}'+
                                    '@keyframes spin {'+
                                    'from {'+
                                    'transform: rotate(0deg);'+
                                    '}'+
                                    'to {'+
                                    'transform: rotate(360deg);'+
                                    '}'+
                                    '}'+
                                    '#table {'+
                                    'font-size: 10px;'+
                                    '}'+
                                    '.table td,'+
                                    '.table th {'+
                                    'padding: .1rem;'+
                                    'border-top: 1px solid #dee2e6;'+
                                    'font-size: 12px;'+
                                    'text-align: left;'+
                                    'vertical-align: middle;'+
                                    'padding-left: 1em;'+
                                    '}'+
                                    '</style>'+
                                    '</head><body>');

            mywindow.document.write(document.getElementById(divID).innerHTML);
            mywindow.document.write('</body></html>');
            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/
            setTimeout(function () { mywindow.print() }, 1000);
            //mywindow.close();

            return true;
        };
    </script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"
        integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"
        crossorigin="anonymous">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous">
    <style type="text/css">
        #loadingIcon
        {
            display:none;
        }
        
        #ShowModal
        {
            display:none;
        }
        #ShowModalMsg
        {
            display:none;
        }
        .form-group
        {
            margin-bottom: 0.5rem;
        }
        
        #MyEtiqueta
        {
            display: none;
            padding-left: 50px;
        }
        
        #MyEtiquetaOC
        {
            display: none;
        }
        
        #lblError
        {
            color: Red;
            font-size: 24px;
        }
        
        .FontPopUp
        {
            font-size:15px;
        }
        
        #LabelQuantityDiv
        {
            display:none;
        }
        
        #MyDynamicEtiqueta
        {
            margin-top:0;
            margin-bottom:0;
            padding-top:0;
            padding-bottom:0;
            display:none;
        }
        #lblsItemDesc
        {
            display:none;
        }
        <style type="text/css">
        #MyEtiqueta label {
            font-size: 15px;
        }

        #LblDate {
            font-size: 14px !important;
        }

        #LblReprintInd,
        #LblReprint {
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

        .fa-check {
            color: green;
        }

        .fa-times {
            color: red;
        }

        #checkItem,
        #checkLot,
        #checkWarehouse,
        #checkLoca,
        #checkPaid {
            display: none;
        }

        #exItem,
        #exLot,
        #exWarehouse,
        #exLoca,
        #exPaid {
            display: none;
        }

        #loadItem,
        #loadLot,
        #loadWarehouse,
        #loadLoca,
        #loadPaid {
            display: none;
        }

        tr {
            text-align: center;
        }

        th {
            text-align: center;
        }

        #myLabel {
            width: 6in;
            height: 4in;
            padding: 20px;
            border: 1px solid black;
            border-radius: 12px;
        }

        .alingRight {
            text-align: right;
        }

        .alingLeft {
            text-align: left;
        }

        #printButton {
            width: 6in;
        }

        #codePaid {
            display: block;
            margin: auto;
            height: 150px;
            width: 500px;
        }

        #codeItem {
            display: block;
            margin: auto;
            height: 75px;
            width: 250px;
        }

        #itemDesc {
            vertical-align: middle;
            font-size: 21px;
        }

        .divDesc {
            text-align: center;
        }

        #lblDesc {
        }

        #lblMadein {
        }

        .borderTop {
            border-top: solid 1px gray;
        }

        #printContainer {
            margin-bottom: 100px;
        }

        #editTable {
            display: none;
        }

        #lblError {
            color: red;
            font-size: 13px;
        }

        .load {
            width: 10px;
            height: 10px;
            align-content: center;
            animation-name: spin;
            animation-duration: 5000ms;
            animation-iteration-count: infinite;
            animation-timing-function: linear;
        }

        #saveSection {
            display: none;
        }

        .notBorderBottom {
            border-bottom: none;
        }

        #divBarcode {
            --height: 186px;
            padding: inherit;
        }

        @keyframes spin {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
        }

        #table {
            font-size: 10px;
        }

        .table td,
        .table th {
            padding: .1rem;
            border-top: 1px solid #dee2e6;
            font-size: 12px;
            text-align: left;
            vertical-align: middle;
            padding-left: 1em;
        }
    </style>
        
      
    <script type="text/javascript">
//        function IniciarListasOrderType() {

//            LstSalesOrder = JSON.parse('<%= Session["LstSalesOrderJSON"]%>');
//            LstTransferOrder = JSON.parse('<%=Session["LstTransferOrderJSON"]%>');
//            LstPurchaseOrders = JSON.parse('<%= Session["LstPurchaseOrdersJSON"]%>');
////            console.log(LstSalesOrder);
////            console.log(LstTransferOrder);
////            console.log(LstPurchaseOrders);
//        }

//        IniciarListasOrderType();
    </script>
    <form id="form1" class="container col-sm-12">

    
    <button  id="ShowModal" type="button" class="btn btn-primary" data-toggle="modal" data-target="#Modal1">
    </button>
    
    <!-- Modal -->
    <div class="modal fade" id="Modal1" tabindex="-1" role="dialog" aria-labelledby="Modal1Label" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-body FontPopUp">
          <label id="label1"></label>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-primary" data-dismiss="modal" data-toggle="modal" data-target="#Modal2">Yes</button>
            <button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>
          </div>
        </div>
      </div>
    </div>
    
    <div class="modal fade" id="Modal2" tabindex="-1" role="dialog" aria-labelledby="Modal2Label" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-body FontPopUp">
                Packing Slip Number
            <input type="text" id="pslip"/>
          </div>
          <div class="modal-footer">
            <button type="button" id="BtnSavePslip" class="btn btn-primary" data-dismiss="modal" data-toggle="modal">Save</button>
          </div>
        </div>
      </div>
    </div>
    
    <button  id="ShowModalMsg" type="button" class="btn btn-primary"  data-toggle="modal" data-target="#Modal3">
    </button>

    <div class="modal fade" id="Modal3" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-body FontPopUp">
            It could be final receipt, total receivedquantity equal to total ordered quantity
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-primary" data-dismiss="modal">Continue</button>
          </div>
        </div>
      </div>
    </div>


    <div class="col-sm-6">
        <div class="form-group row">
            <label class="col-sm-4 col-form-label-lg" for="TxOrderType">
                Order Type</label>
            <div class="col-sm-6">
                <select class="form-control form-control-lg" id="TxOrderType" tabindex="1">
                    <option value="0">Select Order Type</option>
                    <option value="1">Sales Order Return</option>
                    <option value="2">Purchase orders</option>
                    <option value="22">Transfer Order</option>
                </select>
            </div>
        </div>
        <div class="form-group row">
            <label class="col-sm-4 col-form-label-lg" for="txOrderID">
                Order ID</label>
            <div class="col-sm-6">
                <input type="text" class="form-control form-control-lg" id="txOrderID" placeholder="Order ID"
                    data-method="ValidarOrderID" tabindex="1" maxlength="9">
            </div>
        </div>
        <div class="form-group row">
            <label class="col-sm-4 col-form-label-lg" for="txPosition">
                Position</label>
            <div class="col-sm-6" style ="display:none">
                <input type="text" class="form-control form-control-lg" id="txPosition" placeholder="Position "
                    data-method="ValidarPosition" tabindex="1">
            </div>
            <div class="col-sm-6" >
                <select class="form-control form-control-lg" id="DdPosition" tabindex="1">
                    <option value="0">Select Position</option>
                </select>
            </div>
        </div>
        <div class="form-group row">
            <label class="col-sm-4 col-form-label-lg" for="txItem">
                Item</label>
            <div class="col-sm-6">
                <input type="text" class="form-control form-control-lg" id="txItem" placeholder="Item"
                    data-method="ValidarItem" tabindex="1">
            </div>
        </div>
        <div class="form-group row" id="lblsItemDesc">
            <label class="col-sm-4 col-form-label-lg">
            </label>
            <div class="col-sm-6">
                <label class="col-sm-10 col-form-label-lg" id="lblUnidDsca">
                    -</label>
                <label class="col-sm-2 col-form-label-lg" id="lblUnidSt">
                    -</label>
            </div>
        </div>
        <div class="form-group row">
            <label class="col-sm-4 col-form-label-lg" for="txLot">
                Lot</label>
            <div class="col-sm-6">
                <input type="text" class="form-control form-control-lg" id="txLot" placeholder="Lot"
                    data-method="ValidarLote" tabindex="1">
            </div>
        </div>
        <div class="form-group row" style="display:none">
            <label class="col-sm-4 col-form-label-lg" for="DdUnit">
                Unit</label>
            <div class="col-sm-6">
                <asp:DropDownList class="form-control form-control-lg" ID="DdUnit" runat="server">
                </asp:DropDownList>
            </div>
            <label id="Label1" for="txUnit">
            </label>
        </div>
        <div class="form-group row">
            <label class="col-sm-4 col-form-label-lg" for="txQuantity">
                Quantity</label>
            <div class="col-sm-6">
                <input type="text" class="form-control form-control-lg" id="txQuantity" placeholder="Quantity"
                    data-method="ValidarQuantity" tabindex="1">
            </div>
            <label id="lblUnis" for="txQuantity">
            </label>
        </div>
        <div class="form-group row" id="LabelQuantityDiv">
            <label class="col-sm-4 col-form-label-lg" for="txQuantity">
                Label quantity</label>
            <div class="col-sm-6">
                <input type="number" class="form-control form-control-lg" id="LabelQuantity" min="0" max="50" placeholder="Label quantity">
            </div>
            <label id="Label2" for="txQuantity">
            </label>
        </div>
         
        <div class="form-group row" id="finalReceipt">
            <label class="col-sm-4 col-form-label-lg" for="">
            </label>
            <div class=" col-sm-6">
                <input class="" type="checkbox" id="ChkfinalReceipt" disabled>
                <label class="" for="ChkfinalReceipt">
                    Final Receipt
                </label>
            </div>
        </div>
        <div class="form-group row">
            <input type="button" class="btn btn-primary btn-lg" id="btnEnviar" value="Confirm"/>&nbsp
            <button id="btnMyEtiqueta" class="btn btn-primary btn-lg" type="button" onclick="printDiv('MyEtiqueta')">
                Print</button>&nbsp
                <i class="fa fa-spinner fa-pulse fa-3x fa-fw" id="loadingIcon"></i>
            

        </div>
    </div>
    
    <div class="col-sm-6">
        <table class="table">
            <thead>
                <tr>
                    <th>
                        Order Number
                    </th>
                    <th>
                        Position
                    </th>
                    <th>
                        Item Code
                    </th>
                    <th>
                        Description
                    </th>
                    <th>
                        PI.Re Date
                    </th>
                    <th colspan="2" style="text-align: center">
                        Expected
                    </th>
                    <th>
                        Warehouse
                    </th>
                </tr>
            </thead>
            <tbody id="DetailBody">
            </tbody>
        </table>
    </div>
    
    <label id="lblError" class="col-sm-12">
    </label>
    <div id="MyEtiqueta">
        <table style="margin: auto">
            <tr>
                <td>
                    <label style="font-size: 11px">
                        ID</label>
                </td>
                <td colspan="4">
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBPalletNO" alt="" hspace="60"
                        vspace="5" style="width: 4in; height: .5in; margin: 0px !important;" />
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
                        vspace="5" style="width: 4in; height: .5in; margin: 0px !important;" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="center">
                    <label id="lblItemDesc" style="font-size: 11px">
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 11px">
                        QUANTITY</label>
                </td>
                <td colspan="4">
                    <label id="LblQuantity" style="display: none; font-size: 11px">
                    </label>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBQuantity" alt="" hspace="60"
                        vspace="5" style="width: 4in; height: .5in; margin: 0px !important;" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
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
                <td colspan="4">
                    <label id="LblLotId" style="display: none; font-size: 11px">
                    </label>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBLot" alt="" hspace="60"
                        vspace="5" style="width: 4in; height: .5in; margin: 0px !important;" />
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
            </tr>
        </table>
    </div>
    <div id="MyDynamicEtiqueta">
    </div>
    <div id="MyEtiquetaOC">
        <table>
            <tr>
                <td colspan="2">
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBPurchaseOrder" alt=""
                        hspace="60" vspace="5" style="width: 2in; height: .5in;" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <center>
                        <label>
                            PURCHASE ORDER</label>
                        <label style="display: none" id="LblPurchaseOC">
                        </label>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <center>
                        <label>
                            ITEM</label>
                        <label style="display: none" id="LblItemOC">
                        </label>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <center>
                        <label>
                            LOT</label>
                        <label style="display: none" id="LblLotOC">
                        </label>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <center>
                        <label>
                            UNIT</label>
                        <label style="display: none" id="LblUnitOC">
                        </label>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBUnit" alt="" hspace="60"
                        vspace="5" style="width: 2in; height: .5in;" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <center>
                        <label>
                            QUANTITY</label>
                        <label style="display: none" id="LblQuantityOC">
                        </label>
                    </center>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <!-- Referencias de estilo-->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo"
        crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"
        integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1"
        crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"
        integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM"
        crossorigin="anonymous"></script>
    <script type="text/javascript">


        function Enviar() {

        }

        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "whInvReceiptRawMaterialNew.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: asyncMode != undefined ? asyncMode : true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }

        function IniciarControles() {
            loadingIcon = $('#loadingIcon');

            btnEnviar = $('#btnEnviar');

            btnMyEtiqueta = $('#btnMyEtiqueta');

            TxOrderType = $('#TxOrderType');
            txOrderID = $('#txOrderID');
            txItem = $('#txItem');
            txPosition = $('#txPosition');
            ddPosition = $('#DdPosition');
            txLot = $('#txLot');
            txQuantity = $('#txQuantity');
            lblUnis = $('#lblUnis');
            DdUnis = $('#Contenido_DdUnit');


            CBPurchaseOrder = $('#Contenido_CBPurchaseOrder');
            CBItem = $('#Contenido_CBItem');
            CBLot = $('#Contenido_CBLot');
            CBQuantity = $('#Contenido_CBQuantity');
            CBUnit = $('#Contenido_CBUnit');



            //Etiqueta
            CBPalletNO = $('#Contenido_CBPalletNO');
            lblItemID = $('#lblItemID');
            lblItemDesc = $('#lblItemDesc');
            LblQuantity = $('#LblQuantity');
            LblUnit = $('#LblUnit');
            LblLotId = $('#LblLotId');
            LblDate = $('#LblDate');
            LblReprint = $('#LblReprint');

            //Etiqueta Orden de compra

            LblPurchaseOC = $('#LblPurchaseOC');
            LblItemOC = $('#LblItemOC');
            LblLotOC = $('#LblLotOC');
            LblUnitOC = $('#LblUnitOC');
            LblQuantityOC = $('#LblQuantityOC');

            //label 

            lblError = $('#lblError');

        }

        

        $('#BtnSavePslip').click(function(){                
            if(finalReceiptAuto){
                $('#ShowModalMsg').click();
            }
        });

        function DeshabilitarLimpiarControles() {

            $('#ChkfinalReceipt').prop('checked', false);
            btnEnviar.attr('disabled', true);

            txOrderID.attr('disabled', true);
            TxOrderType.attr('disabled', false);
            txItem.attr('disabled', true);
            txLot.attr('disabled', true);
            txPosition.attr('disabled', true);
            txQuantity.attr('disabled', true);
            DdUnis.attr('disabled', true);

            txOrderID.val("");
            TxOrderType.val(0);
            txItem.val("");
            txLot.val("");
            txPosition.val("");
            limpiarPosition();
            txQuantity.val("");
            DdUnis.val("");
            $('#LabelQuantity').val("");
        }

        function DeshabilitarOrderType() {

            btnEnviar.attr('disabled', true);
            txOrderID.attr('disabled', false);
            TxOrderType.attr('disabled', false);
            txItem.attr('disabled', true);
            txLot.attr('disabled', true);
            txPosition.attr('disabled', true);
            txQuantity.attr('disabled', true);
            DdUnis.attr('disabled', true);

            txOrderID.val("");
            txItem.val("");
            txLot.val("");
            txPosition.val("");
            limpiarPosition();
            txQuantity.val("");
            DdUnis.val("");
            
            
        }


        var InsertSucces = function (r) {
            $('#loadingIcon').hide();
            $('#btnEnviar').show();
            $("#MyDynamicEtiqueta").empty();
            MyList = JSON.parse(r.d);

            if (MyList.length == undefined) {
                MyObject = JSON.parse(r.d);
                if (MyObject.error == false) {
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
                    printDiv('MyEtiqueta');


                }
                else {
                    console.log("El registro no se realizo");
                    alert(MyObject.errorMsg);
                }
            }
            else {
                MyList.forEach(function (MyObject) {

                    CBPalletNOd = MyObject.PAID_URL;
                    lblItemIDd = MyObject.ITEM;
                    lblItemDescd = MyObject.DSCA;
                    LblQuantityd = MyObject.QTYC;
                    LblUnitd = MyObject.UNIC;
                    LblLotIdd = MyObject.CLOT;
                    CBPurchaseOrderd = MyObject.ORNO_URL;
                    CBItem = MyObject.ITEM_URL;
                    CBLotd = MyObject.CLOT_URL;
                    CBQuantityd = MyObject.QTYC_URL;
                    CBUnitd = MyObject.UNIC_URL;
                    LblPurchaseOCd = MyObject.ORNO;
                    LblItemOCd = MyObject.ITEM;
                    LblLotOCd = MyObject.CLOT;
                    LblUnitOCd = MyObject.UNIT;
                    LblQuantityOCd = MyObject.QTYC;
                    LblUser = MyObject.LOGN;



                    var etiqueta =
                        '<div id="myLabel">' +
                        '<div class="row">' +
                        '<div class="col-6 alingLeft">' +
                        '<label id="lblMaterialDesc"><strong>' + lblItemDescd + '</strong></label>' +
                        '</div>' +
                        '<div class="col-6 alingRight">' +
                        '<label id="lblMaterialCode"><strong>' + lblItemIDd + '</strong></label>' +
                        '</div>' +
                        '</div>' +
                        '<br />' +
                        '<div class="col-12 borderTop" id="divBarcode">' +
                        '<img src="' + CBPalletNOd + '" id="codePaid" alt="" />' +
                        '</div>' +
                        '<div>' +
                        '<table class="table mw-100">' +
                        '<tbody>' +
                        '<tr>' +
                        '<td><strong>LOT</strong>&nbsp;&nbsp;<label id="lblLot">' + LblLotIdd + '</label></td>' +
                        '<td><strong>Quantity</strong>&nbsp;&nbsp;<label id="lblQuantity">' + LblQuantityd.replace(",",".") + '</label></td>' +
                        '</tr>' +
                        '<tr>' +
                        '<td><strong>Origin Lot</strong>&nbsp;&nbsp;<label id="lblOrigin">' + LblLotIdd + '</label></td>' +
                        '<td><strong>Supplier</strong>&nbsp;&nbsp;<label id="lblSupplier">' + LblUser + '</label></td>' +
                        '</tr>' +
                        '<tr>' +
                        '<td><strong>Received By</strong>&nbsp;&nbsp;<label id="lblRecibedBy">' + LblUser + '</label></td>' +
                        '<td><strong>Received On</strong>&nbsp;&nbsp;<class="LblDate"></label></td>' +
                        '</tr>' +
                        '</tbody>' +
                        '</table>' +
                        '</div>' +
                        '</div>';

                            //'<div class="page" style="padding:0; margin: auto; height:4in;width:6in"><table style="margin: auto">                                                               ' +
                            //'<tr>                                                                                       ' +
                            //'<td colspan="4"><br><br>' +
                            //'<img src="' + CBPalletNOd + '" id="CBPalletNOd" alt="" hspace="60"' +
                            //'vspace="5" style="width: 4in; height: .5in;" />                                            ' +
                            //'</td>                                                                                      ' +
                            //'</tr>                                                                                      ' +
                            //'<tr>                                                                                       ' +
                            //'<td>                                                                                       ' +
                            //'<label style="font-size: 11px">                                                            ' +
                            //'ITEM</label>                                                                               ' +
                            //'</td>                                                                                      ' +
                            //'<td>                                                                                       ' +
                            //'<label id="lblItemIDd" style="display: none; font-size: 10px">                              ' +
                            //'' + lblItemIDd + '</label>' +
                            //'<img src="' + CBItem + '" id="CBItem" alt="" hspace="60"    ' +
                            //'vspace="5" style="width: 3in; height: .5in;" />                                            ' +
                            //'</td>                                                                                      ' +
                            //'</tr>                                                                                      ' +
                            //'<tr>                                                                                       ' +
                            //'<td>                                                                                       ' +
                            //'</td>                                                                                      ' +
                            //'<td align="center">                                                                        ' +
                            //'<label id="lblItemDescd" style="font-size: 11px">                                           ' +
                            //'' + lblItemDescd + '</label>' +
                            //'</td>                                                                                      ' +
                            //'</tr>                                                                                      ' +
                            //'<tr>                                                                                       ' +
                            //'<td>                                                                                       ' +
                            //'<label style="font-size: 11px">                                                            ' +
                            //'QUANTITY</label>                                                                           ' +
                            //'</td>                                                                                      ' +
                            //'<td>                                                                                       ' +
                            //'<label id="LblQuantityd" style="display: none; font-size: 11px">                            ' +
                            //'' + LblQuantityd + '</label>' +
                            //'<img src="' + CBQuantityd + '" id="CBQuantityd" alt="" hspace="60"' +
                            //'vspace="5" style="width: 2in; height: .5in;" />                                            ' +
                            //'</td>                                                                                      ' +
                            //'<td>                                                                                       ' +
                            //'</td>                                                                                      ' +
                            //'</tr>                                                                                      ' +
                            //'<tr>                                                                                       ' +
                            //'<td>                                                                                       ' +
                            //'</td>                                                                                      ' +
                            //'<td align="center">                                                                        ' +
                            //'<label id="LblUnitd" style="font-size: 11px">                                               ' +
                            //'' + LblUnitd + '</label>' +
                            //'</td>                                                                                      ' +
                            //'</tr>                                                                                      ' +
                            //'<tr>                                                                                       ' +
                            //'<td>                                                                                       ' +
                            //'<label style="font-size: 11px">                                                            ' +
                            //'LOT</label>                                                                                ' +
                            //'</td>                                                                                      ' +
                            //'<td>                                                                                       ' +
                            //'<label id="LblLotIdd" style="display: none; font-size: 11px">                               ' +
                            //'' + LblLotIdd + '</label>                                                                                   ' +
                            //'<img src="' + CBLotd + '" id="CBLotd" alt="" hspace="60"     ' +
                            //'vspace="5" style="width: 3in; height: .5in;" />                                            ' +
                            //'</td>                                                                                      ' +
                            //'</tr>                                                                                      ' +
                            //'<tr>                                                                                       ' +
                            //'<td>                                                                                       ' +
                            //'<label style="font-size: 11px">                                                            ' +
                            //'RECEIPT DATE</label>                                                                       ' +
                            //'</td>                                                                                      ' +
                            //'<td>                                                                                       ' +
                            //'<label class="LblDate" style="font-size: 11px">                                               ' +
                            //'</label>                                                                                   ' +
                            //'</td>                                                                                      ' +
                            //'</tr>                                                                                      ' +
                            //'</table>'+
                            //'</div>';
                    $('#MyDynamicEtiqueta').append(etiqueta);
               }
            );

                DeshabilitarLimpiarControles();
                printDiv('MyDynamicEtiqueta');

            }

        }

        var SuccesQuantityUnity = function (r) {

            lstFactor = JSON.parse(r.d);
        };

        var ConsultarSumatoria = function (r) {

            SumatoriaDevuelta = 0.0;

            if (r.d != "") {
                SumatoriaDevuelta = parseFloat(r.d)
            }
        };


        var FactorSucces = function (r) {
            Factor = JSON.parse(r.d);
            if (Factor.MsgError != "") {

            }
            else {
                if (Factor.Tipo == "Div") {
                    res = Factor.FactorD
                    console.log(res);
                }
                if (Factor.Tipo == "Mul") {
                    res = 15 * Factor.FactorD
                    console.log(res);
                }
            }
        }

        var ItemSucces = function (r) {
            var itemJSON = JSON.parse(r.d)
            console.log(itemJSON);
            lblUnis.html(itemJSON.CUNI);
            if (itemJSON.KLTC == "1") {
                txLot.attr('disabled', false);
            }
            else {
                txLot.attr('disabled', true);
            }
        }

        var ValidarLoteSucces = function (retorno) {
            if (retorno.d == false) {
                lblError.html("The lot does not exist or is not associated with the item");
                txLot.focus();
                btnEnviar.attr('disabled', true);
            }
            if (retorno.d == true) {
                lblError.html("");
                txPosition.focus();
                ddPosition.focus();
                btnEnviar.attr('disabled', true);
            }
        }
        var OrderIDSucces = function (r) {
            var OrderJSON = JSON.parse(r.d)
            if (r.d.length > 0) {
                lblUnis.html(OrderJSON.STUN);
                txItem.attr('disabled', false);
            }
            else {
                txItem.attr('disabled', true);
            }
        }

        $(function () {
            IniciarControles();
            qtylabelsrec = "<%=qtylabelsrec%>";
            $('#TxOrderType').change(function (e) {

                console.log(e);
                console.log(e.currentTarget.value)
                switch (e.currentTarget.value) {
                    case "0":
                        $('#lblsItemDesc').hide(500);
                        $('#LabelQuantityDiv').hide(500)
                        $('#ChkfinalReceipt').prop('disabled', true);
                        $('#ChkfinalReceipt').prop('checked', false);
                        DeshabilitarLimpiarControles();
                        //                        $('#finalReceipt').show(100);
                        break;
                    case "2":
                        $('#lblsItemDesc').hide(500);
                        qtylabelsrec == "True" ? $('#LabelQuantityDiv').show(500) : $('#LabelQuantityDiv').hide(500);
                        $('#ChkfinalReceipt').prop('disabled', true);
                        $('#ChkfinalReceipt').prop('checked', false);
                        txOrderID.attr('disabled', false);
                        DeshabilitarOrderType();
                        txOrderID.focus();
                        //                        $('#finalReceipt').hide(100);
                        break;
                    default:
                        $('#lblsItemDesc').hide(500);
                        $('#LabelQuantityDiv').hide(500)
                        $('#ChkfinalReceipt').prop('disabled', true);
                        $('#ChkfinalReceipt').prop('checked', false);
                        txOrderID.attr('disabled', false);
                        DeshabilitarOrderType();
                        txOrderID.focus();
                        //                        $('#finalReceipt').show(100);
                        break;
                }
            });

            txQuantity.bind("input paste", function (e) {

                if ($('#lblUnidSt').html().toUpperCase().trim() == "UN") {
                    var straux = "";
                    str = $('#txQuantity').val();
                    for (var i = 0; i < $('#txQuantity').val().length; i++) {
                        straux += str.charAt(i).replace(",", "").replace(".", "");
                    }
                    $('#txQuantity').val(straux);
                }else {
                    var straux = "";
                    str = $('#txQuantity').val();
                    for (var i = 0; i < $('#txQuantity').val().length; i++) {
                        straux += str.charAt(i).replace(",", ".");
                    }
                    $('#txQuantity').val(straux);
                }

                if (TxOrderType.val() == "1") {
                    //MyObject = LstSalesOrder.find(x => x.ITEM==txItem.val().trim() && x.ORNO==txOrderID.val().trim() && x.PONO==txPosition.val().trim());                        
                    LstSalesOrder.forEach(function (x) {
                        if (x.ITEM == txItem.val().trim() && x.ORNO == txOrderID.val().trim().toUpperCase() && x.PONO == txPosition.val().trim()) {
                            MyObject = x;
                        }
                    });
                }

                if (TxOrderType.val() == "2") {
                    //MyObject = LstPurchaseOrders.find(x => x.ITEM==txItem.val().trim() && x.ORNO==txOrderID.val().trim() && x.PONO==txPosition.val().trim());
                    LstPurchaseOrders.forEach(function (x) {
                        if (x.ITEM.trim() == txItem.val().trim() && x.ORNO.trim() == txOrderID.val().trim().toUpperCase() && x.PONO.trim() == txPosition.val().trim()) {
                            MyObject = x;
                        }
                    });
                }

                if (TxOrderType.val() == "22") {
                    //MyObject = LstTransferOrder.find(x => x.ITEM==txItem.val().trim() && x.ORNO==txOrderID.val().trim() && x.PONO==txPosition.val().trim());                        
                    LstTransferOrder.forEach(function (x) {
                        if (x.ITEM == txItem.val().trim() && x.ORNO == txOrderID.val().trim().toUpperCase() && x.PONO == txPosition.val().trim()) {
                            MyObject = x;
                        }
                    });
                }

                var mycant = parseFloat(MyObject.OQUA);
                //var rangBajo = parseFloat(MyObject.OQUA) * (1 - parseFloat(MyObject.RTQP) / 100);
                var rangAlto = parseFloat(MyObject.OQUA) * (1 + parseFloat(MyObject.RTQP) / 100);

                var mycantView = parseFloat(txQuantity.val());

                if (MyObject.STUN.trim() != DdUnis.val().trim()) {

                    FactorMul = undefined; //lstFactor.find(x => x.BASU.trim() == MyObject.STUN.trim() && x.UNIT.trim() == DdUnis.val().trim());
                    FactorDiv = undefined; //lstFactor.find(x => x.BASU.trim() == DdUnis.val().trim() && x.UNIT.trim() == MyObject.STUN.trim());

                    if (lstFactor[0].Error != true) {
                        lstFactor.forEach(function (x) {
                            if (x.BASU.trim() == MyObject.STUN.trim() && x.UNIT.trim() == DdUnis.val().trim()) {
                                FactorMul = x;
                            }
                            else if (x.BASU.trim() == DdUnis.val().trim() && x.UNIT.trim() == MyObject.STUN.trim()) {
                                FactorDiv = x;
                            }
                        });

                    }


                    if (FactorMul != undefined || FactorDiv != undefined) {

                        if (FactorDiv != undefined) {
                            mycantView = parseFloat(txQuantity.val()) / parseFloat(FactorDiv.FACTOR);
                        }
                        else if (FactorMul != undefined) {
                            mycantView = parseFloat(txQuantity.val()) * parseFloat(FactorMul.FACTOR);
                        }

                        if (TxOrderType.val() == "2") {
                            if (rangAlto >= mycantView + SumatoriaDevuelta && mycantView > 0) {
                                console.log("Cantidad dentro de rango.");
                                lblError.html("");
                                btnEnviar.attr('disabled', false);
                                if (mycantView + SumatoriaDevuelta == rangAlto) {
                                    $('#ChkfinalReceipt').prop('checked', true);
                                    $('#ShowModal').click();
                                    finalReceiptAuto = true;


                                }
                                else {
                                    $('#ChkfinalReceipt').prop('checked', false);
                                    $('#ChkfinalReceipt').prop('disabled', false);
                                }

                            }
                            else {
                                lblError.html('<%= lblQuantityError%>');
                                btnEnviar.attr('disabled', true);
                            }
                        }
                        else {
                            if (mycantView + SumatoriaDevuelta <= mycant && mycantView > 0) {
                                console.log("Cantidad dentro de rango.");
                                lblError.html("");
                                btnEnviar.attr('disabled', false);

                                if (mycantView + SumatoriaDevuelta == rangAlto) {
                                    $('#ChkfinalReceipt').prop('checked', true);

                                }
                                else {
                                    $('#ChkfinalReceipt').prop('checked', false);
                                }
                            }
                            else {
                                lblError.html('<%= lblQuantityError%>');
                                btnEnviar.attr('disabled', true);
                            }
                        }
                    }
                }

                else if (MyObject.STUN.trim() == DdUnis.val().trim()) {

                    if (TxOrderType.val() == "2") {
                        if (rangAlto >= mycantView + SumatoriaDevuelta && mycantView > 0) {
                            console.log("Cantidad dentro de rango");
                            lblError.html("");
                            btnEnviar.attr('disabled', false);

                            if (mycantView + SumatoriaDevuelta == rangAlto) {
                                $('#ChkfinalReceipt').prop('checked', true);
                                $('#ShowModal').click();
                                finalReceiptAuto = true;
                            }
                            else {
                                $('#ChkfinalReceipt').prop('checked', false);
                                $('#ChkfinalReceipt').prop('disabled', false);
                                finalReceiptAuto = false;
                            }
                        }
                        else {
                            lblError.html('<%= lblQuantityError%>');
                            btnEnviar.attr('disabled', true);
                            $('#ChkfinalReceipt').prop('checked', false);
                        }
                    }
                    else {
                        if (mycantView + SumatoriaDevuelta <= mycant && mycantView > 0) {
                            console.log("Cantidad dentro de rango.");
                            lblError.html("");
                            btnEnviar.attr('disabled', false);

                            if (mycantView + SumatoriaDevuelta == rangAlto) {
                                $('#ChkfinalReceipt').prop('checked', true);

                            }
                            else {
                                $('#ChkfinalReceipt').prop('checked', false);
                            }
                        }
                        else {
                            lblError.html('<%= lblQuantityError%>');
                            btnEnviar.attr('disabled', true);
                        }
                    }

                }
            });



            DdUnis.change(function (e) {

                txQuantity.val("");
                btnEnviar.attr('disabled', true);
                if (TxOrderType.val() == "1") {
                    LstSalesOrder.forEach(function (x) {
                        if (x.ITEM == txItem.val().trim().toUpperCase() && x.ORNO == txOrderID.val().trim().toUpperCase() && x.PONO == txPosition.val().toUpperCase().trim()) {
                            MyObject = x;
                        }
                    });
                }

                if (TxOrderType.val() == "2") {
                    LstPurchaseOrders.forEach(function (x) {
                        if (x.ITEM == txItem.val().trim().toUpperCase() && x.ORNO == txOrderID.val().trim().toUpperCase() && x.PONO == txPosition.val().trim().toUpperCase()) {
                            MyObject = x;
                        }
                    });
                }

                if (TxOrderType.val() == "22") {
                    //MyObject = LstTransferOrder.find(x => x.ITEM==txItem.val().trim() && x.ORNO==txOrderID.val().trim() && x.PONO==txPosition.val().trim());
                    LstTransferOrder.forEach(function (x) {
                        if (x.ITEM == txItem.val().trim().toUpperCase() && x.ORNO == txOrderID.val().trim().toUpperCase() && x.PONO == txPosition.val().trim().toUpperCase()) {
                            MyObject = x;
                        }
                    });
                }

                FactorMul = undefined; //lstFactor.find(x => x.BASU.trim() == MyObject.STUN.trim() && x.UNIT.trim() == DdUnis.val().trim());
                FactorDiv = undefined; // lstFactor.find(x => x.BASU.trim() == DdUnis.val().trim() && x.UNIT.trim() == MyObject.STUN.trim());
                if (lstFactor[0].Error != true) {
                    lstFactor.forEach(function (x) {
                        if (x.BASU.trim() == MyObject.STUN.trim().toUpperCase() && x.UNIT.trim() == DdUnis.val().trim().toUpperCase()) {
                            FactorMul = x;
                        }
                        else if (x.BASU.trim() == DdUnis.val().trim().toUpperCase() && x.UNIT.trim() == MyObject.STUN.trim().toUpperCase()) {
                            FactorDiv = x;
                        }
                    });
                }

                if (MyObject.STUN.trim() != DdUnis.val().trim()) {

                    if (FactorMul != undefined || FactorDiv != undefined) {
                        txQuantity.attr('disabled', false);
                        txQuantity.focus();

                    }
                    else {
                        txQuantity.attr('disabled', true);
                        lblError.html('<%= lblConvertionError%>');
                        txQuantity.val("");
                        btnEnviar.attr('disabled', true);
                    }
                }

                else if (MyObject.STUN.trim() == DdUnis.val().trim()) {
                    txQuantity.attr('disabled', false);
                    txQuantity.focus();
                }
            });

            function ConsultarSumatoria130(OrdenID, Position, SEQNR) {

                var Data = "{'ORNO':'" + OrdenID + "','PONO':'" + Position + "','SEQNR':'" + SEQNR + "'}";
                WebMethod = "ConsultarSumatoriaCantidades130";
                sendAjax(WebMethod, Data, ConsultarSumatoria, false);

            }

            var ListasOdenCompraSuccess = function (r) {
                var Lista = JSON.parse(r.d);
                LstSalesOrder = JSON.parse(Lista[0]);
                LstTransferOrder = JSON.parse(Lista[1]);
                LstPurchaseOrders = JSON.parse(Lista[2]);
            }

            function ListasOdenCompra(OrdenID, OrderType) {

                var Data = "{'ORNO':'" + OrdenID + "','TYPE_ORNO':'" + OrderType + "'}";
                WebMethod = "ListasOdenCompra";
                sendAjax(WebMethod, Data, ListasOdenCompraSuccess, false);

            }

            function ConsultarSumatoria130(OrdenID, Position) {

                var Data = "{'ORNO':'" + OrdenID + "','PONO':'" + Position + "'}";
                WebMethod = "ConsultarSumatoriaCantidades130";
                sendAjax(WebMethod, Data, ConsultarSumatoria, false);

            }

            function ConsultarFactoresporItem() {

                var Data = "{'ITEM':'" + $('#txItem').val().trim() + "'}";
                WebMethod = "ConsultafactoresporItem";
                sendAjax(WebMethod, Data, SuccesQuantityUnity, false);

            }

            $('#btnEnviar').click(function () {
                $('#btnEnviar').hide();
                $('#loadingIcon').show();
                if (TxOrderType.val() == "1") {
                    //MyObject = LstSalesOrder.find(x => x.ITEM==txItem.val().trim().toUpperCase() && x.ORNO==txOrderID.val().trim().toUpperCase() && x.PONO==txPosition.val().trim().toUpperCase());


                    LstSalesOrder.forEach(function (x) {
                        if (x.ITEM == txItem.val().trim().toUpperCase() && x.ORNO == txOrderID.val().trim().toUpperCase() && x.PONO == txPosition.val().trim().toUpperCase()) {
                            MyObject = x
                        }
                    });


                    if (MyObject != undefined) {

                        OORG = TxOrderType.val().toUpperCase();
                        ORNO = MyObject.ORNO.toUpperCase();
                        ITEM = MyObject.ITEM.toUpperCase();
                        PONO = MyObject.PONO.toUpperCase();
                        LOT = txLot.val().toUpperCase();
                        QUANTITY = parseFloat(txQuantity.val().trim()).toFixed(4);
                        STUN = MyObject.STUN.trim();
                        CUNI = MyObject.CUNI.trim();
                        CWAR = MyObject.CWAR.toUpperCase();
                        FIRE = $('#ChkfinalReceipt').is(':checked') == true ? "1" : "2";
                        PSLIP = " ";
                        console.log(MyObject);

                        var Data = "{'OORG':'" + OORG + "', 'ORNO':'" + ORNO + "',  'ITEM':'" + ITEM + "',  'PONO':'" + PONO + "',  'LOT':'" + LOT + "',  'QUANTITY':'" + QUANTITY + "',  'STUN':'" + STUN + "',  'CUNI':'" + CUNI + "', 'CWAR':'" + CWAR + "', 'FIRE':'" + FIRE + "','PSLIP':'" + PSLIP + "','CICLE':'1','INIT':'true'}";
                        sendAjax("InsertarReseiptRawMaterial", Data, InsertSucces, false);
                    } else {
                    }
                }
                else if (TxOrderType.val() == "2") {
                    //MyObject = LstPurchaseOrders.find(x => x.ITEM==txItem.val().trim() && x.ORNO==txOrderID.val().trim() && x.PONO==txPosition.val().trim());


                    LstPurchaseOrders.forEach(function (x) {
                        if (x.ITEM == txItem.val().trim().toUpperCase() && x.ORNO == txOrderID.val().trim().toUpperCase() && x.PONO == txPosition.val().trim().toUpperCase()) {
                            MyObject = x
                        }
                    });




                    if (MyObject != undefined) {

                        if ((MyObject.KLTC == "1" && txLot.val().trim() != "") || MyObject.KLTC != "1") {

                            OORG = TxOrderType.val().toUpperCase();
                            ORNO = MyObject.ORNO.toUpperCase();
                            ITEM = MyObject.ITEM.toUpperCase();
                            PONO = MyObject.PONO.toUpperCase();
                            LOT = txLot.val().trim().toUpperCase();
                            QUANTITY = parseFloat(txQuantity.val().trim()).toFixed(4);
                            STUN = MyObject.STUN.trim();
                            CUNI = MyObject.CUNI.trim();
                            CWAR = MyObject.CWAR.toUpperCase();
                            FIRE = $('#ChkfinalReceipt').is(':checked') == true ? "1" : "2";
                            PSLIP = $('#pslip').val().trim();
                            console.log(MyObject);

                            var Data = "{'OORG':'" + OORG + "', 'ORNO':'" + ORNO + "',  'ITEM':'" + ITEM + "',  'PONO':'" + PONO + "',  'LOT':'" + LOT + "',  'QUANTITY':'" + QUANTITY + "',  'STUN':'" + STUN + "',  'CUNI':'" + CUNI + "', 'CWAR':'" + CWAR + "', 'FIRE':'" + FIRE + "','PSLIP':'" + PSLIP + "','CICLE':'" + (($('#LabelQuantity').val().trim() == "") ? "1" : $('#LabelQuantity').val().trim()) + "','INIT':'true'}";
                            sendAjax("InsertarReseiptRawMaterial", Data, InsertSucces, false);
                        }
                        else {

                            lblError.html("The Lot cannot be empty");
                        }

                    } else {
                    }


                }
                else if (TxOrderType.val() == "22") {
                    //MyObject = LstTransferOrder.find(x => x.ITEM==txItem.val().trim() && x.ORNO==txOrderID.val().trim() && x.PONO==txPosition.val().trim());
                    LstTransferOrder.forEach(function (x) {
                        if (x.ITEM == txItem.val().trim().toUpperCase() && x.ORNO == txOrderID.val().trim().toUpperCase() && x.PONO == txPosition.val().trim().toUpperCase()) {
                            MyObject = x
                        }
                    });
                    if (MyObject != undefined) {

                        OORG = TxOrderType.val().toUpperCase();
                        ORNO = MyObject.ORNO.toUpperCase();
                        ITEM = MyObject.ITEM.toUpperCase();
                        PONO = MyObject.PONO.toUpperCase();
                        LOT = txLot.val().trim().toUpperCase();
                        QUANTITY = parseFloat(txQuantity.val().trim()).toFixed(4);
                        STUN = MyObject.STUN.trim();
                        CUNI = MyObject.CUNI.trim();
                        CWAR = MyObject.CWAR.toUpperCase();
                        FIRE = $('#ChkfinalReceipt').is(':checked') == true ? "1" : "2";
                        PSLIP = " ";
                        console.log(MyObject);


                        var Data = "{'OORG':'" + OORG + "', 'ORNO':'" + ORNO + "',  'ITEM':'" + ITEM + "',  'PONO':'" + PONO + "',  'LOT':'" + LOT + "',  'QUANTITY':'" + QUANTITY + "',  'STUN':'" + STUN + "',  'CUNI':'" + CUNI + "', 'CWAR':'" + CWAR + "', 'FIRE':'" + FIRE + "','PSLIP':'" + PSLIP + "','CICLE':'1','INIT':'true'}";
                        sendAjax("InsertarReseiptRawMaterial", Data, InsertSucces, false);

                    } else {

                    }
                }

            });

            function DeshabilitarControl() {

                btnEnviar.attr('disabled', true);

                txOrderID.attr('disabled', true);
                TxOrderType.attr('disabled', false);
                txItem.attr('disabled', true);
                txLot.attr('disabled', true);
                txPosition.attr('disabled', true);
                txQuantity.attr('disabled', true);
                DdUnis.attr('disabled', true);
                btnMyEtiqueta.hide();
                //btnMyEtiquetaOC.hide();
            }


            DeshabilitarControl();


            txOrderID.bind("change paste keyup", function (e) {
                lblError.html('');
                if (e.currentTarget.value.trim() != "" && e.currentTarget.value.trim().length == 9 && e.currentTarget.value != undefined && e.currentTarget.value != null) {
                    var OrdenID = txOrderID.val().trim().toUpperCase();

                    if (TxOrderType.val() == "1") {
                        var Order = undefined; //LstSalesOrder.find(x => x.ORNO.toUpperCase() == OrdenID.toUpperCase());
                        ListasOdenCompra(OrdenID.toUpperCase(), TxOrderType.val());
                        LstSalesOrder.forEach(function (x) {
                            if (x.ORNO.toUpperCase() == OrdenID.toUpperCase()) {
                                Order = x
                            }
                        });

                        if (Order != undefined) {
                            addOption();
                            console.log("Si existe el order");
                            txPosition.attr('disabled', false);
                            txPosition.focus();
                            ddPosition.focus();
                            setTimeout(function () { lblError.html(""); }, 2500);

                        } else {
                            setTimeout(function () { lblError.html('<%= lblOrderError%>'); }, 500);

                            btnEnviar.attr('disabled', true);
                            txItem.attr('disabled', true);
                            txPosition.attr('disabled', true);
                            txLot.attr('disabled', true);
                            DdUnis.attr('disabled', true);
                            txQuantity.attr('disabled', true);

                            txItem.val("");
                            txPosition.val("");
                            limpiarPosition();
                            txLot.val("");
                            DdUnis.val("");
                            txQuantity.val("");

                            var MyElemnt = "#" + e.currentTarget.id;
                            $(MyElemnt).focus();
                        }
                    }
                    else if (TxOrderType.val() == "2") {
                        var Order = undefined; //LstPurchaseOrders.find(x => x.ORNO.toUpperCase() == OrdenID.toUpperCase());
                        ListasOdenCompra(OrdenID.toUpperCase(), TxOrderType.val());
                        //IniciarListasOrderType();
                        LstPurchaseOrders.forEach(function (x) {
                            if (x.ORNO.toUpperCase() == OrdenID.toUpperCase()) {
                                Order = x
                            }
                        });


                        txPosition.attr('disabled', false);
                        if (Order != undefined) {
                            addOption();
                            console.log("Si existe el order");
                            txPosition.focus();
                            ddPosition.focus();
                            setTimeout(function () { lblError.html(""); }, 2500);
                        } else {
                            setTimeout(function () { lblError.html('<%= lblOrderError%>'); }, 500);

                            btnEnviar.attr('disabled', true);
                            txItem.attr('disabled', true);
                            txPosition.attr('disabled', true);
                            txLot.attr('disabled', true);
                            DdUnis.attr('disabled', true);
                            txQuantity.attr('disabled', true);

                            txItem.val("");
                            txPosition.val("");
                            limpiarPosition();
                            txLot.val("");
                            DdUnis.val("");
                            txQuantity.val("");

                            var MyElemnt = "#" + e.currentTarget.id;
                            $(MyElemnt).focus();
                        }
                    }
                    else if (TxOrderType.val() == "22") {
                        var Order = undefined; // LstTransferOrder.find(x => x.ORNO.toUpperCase() == OrdenID.toUpperCase() );
                        ListasOdenCompra(OrdenID.toUpperCase(), TxOrderType.val());
                        //IniciarListasOrderType();
                        LstTransferOrder.forEach(function (x) {
                            if (x.ORNO.toUpperCase() == OrdenID.toUpperCase()) {
                                Order = x
                            }
                        });

                        if (Order != undefined) {
                            console.log("Si existe el order");
                            addOption();
                            txPosition.attr('disabled', false);
                            txPosition.focus();
                            ddPosition.focus();
                            setTimeout(function () { lblError.html(""); }, 2500);

                        } else {
                            setTimeout(function () { lblError.html('<%= lblOrderError%>'); }, 500);


                            btnEnviar.attr('disabled', true);
                            txItem.attr('disabled', true);
                            txPosition.attr('disabled', true);
                            txLot.attr('disabled', true);
                            DdUnis.attr('disabled', true);
                            txQuantity.attr('disabled', true);

                            txItem.val("");
                            txPosition.val("");
                            limpiarPosition();
                            txLot.val("");
                            DdUnis.val("");
                            txQuantity.val("");

                            var MyElemnt = "#" + e.currentTarget.id;
                            $(MyElemnt).focus();

                        }
                    }
                }
            });


            txItem.bind("change paste keyup", function (e) {

                if (e.currentTarget.value.trim() != "" && e.currentTarget.value != undefined && e.currentTarget.value != null) {
                    var OrdenID = txOrderID.val().trim();
                    var MyItem = txItem.val().trim().toUpperCase();
                    if (TxOrderType.val() == "1") {
                        var Item = undefined; //LstSalesOrder.find(x => x.ORNO.toUpperCase() == OrdenID.toUpperCase() && x.ITEM.toUpperCase() == Item.toUpperCase());

                        LstSalesOrder.forEach(function (x) {
                            if (x.ORNO.toUpperCase() == OrdenID.toUpperCase() && x.ITEM.toUpperCase().trim() == MyItem.trim()) {
                                Item = x
                            }
                        });

                        if (Item != undefined) {
                            console.log("Si existe el Item");
                            txPosition.attr('disabled', false);
                            Item.KLTC == "1" ? txLot.attr('disabled', false) : txLot.attr('disabled', true);
                            ConsultarFactoresporItem();
                            txPosition.focus();
                            ddPosition.focus();
                            setTimeout(function () { lblError.html(""); }, 4000);
                        } else {
                            lblError.html('<%= lblItemError%>');

                            btnEnviar.attr('disabled', true);

                            txPosition.attr('disabled', true);
                            txLot.attr('disabled', true);
                            DdUnis.attr('disabled', true);
                            txQuantity.attr('disabled', true);

                            txPosition.val("");
                            limpiarPosition();
                            txLot.val("");
                            DdUnis.val("");
                            txQuantity.val("");

                            var MyElemnt = "#" + e.currentTarget.id;
                            $('#txItem').focus();
                        }
                    }
                    else if (TxOrderType.val() == "2") {
                        var Item = undefined; // LstPurchaseOrders.find(x => x.ORNO == OrdenID.toUpperCase() && x.ITEM == Item.toUpperCase());

                        LstPurchaseOrders.forEach(function (x) {
                            if (x.ORNO.toUpperCase() == OrdenID.toUpperCase() && x.ITEM.toUpperCase().trim() == MyItem.trim()) {
                                Item = x
                            }
                        });

                        if (Item != undefined) {
                            console.log("Si existe el Item");
                            txPosition.attr('disabled', false);
                            Item.KLTC == "1" ? txLot.attr('disabled', false) : txLot.attr('disabled', true);
                            ConsultarFactoresporItem();
                            txPosition.focus();
                            ddPosition.focus();
                            setTimeout(function () { lblError.html(""); }, 4000);
                        } else {
                            lblError.html('<%= lblItemError%>');

                            btnEnviar.attr('disabled', true);

                            txPosition.attr('disabled', true);
                            txLot.attr('disabled', true);
                            DdUnis.attr('disabled', true);
                            txQuantity.attr('disabled', true);

                            txPosition.val("");
                            limpiarPosition();
                            txLot.val("");
                            DdUnis.val("");
                            txQuantity.val("");

                            var MyElemnt = "#" + e.currentTarget.id;
                            $('#txItem').focus();
                        }
                    }
                    else if (TxOrderType.val() == "22") {
                        var Item = undefined; //LstTransferOrder.find(x => x.ORNO.toUpperCase() == OrdenID.toUpperCase() && x.ITEM.toUpperCase() == Item.toUpperCase());
                        LstTransferOrder.forEach(function (x) {
                            if (x.ORNO.toUpperCase() == OrdenID.toUpperCase() && x.ITEM.toUpperCase().trim() == MyItem.trim()) {
                                Item = x
                            }
                        });
                        if (Item != undefined) {
                            console.log("Si existe el Item");
                            txPosition.attr('disabled', false);
                            Item.KLTC == "1" ? txLot.attr('disabled', false) : txLot.attr('disabled', true);
                            ConsultarFactoresporItem();
                            txPosition.focus();
                            ddPosition.focus();
                            setTimeout(function () { lblError.html(""); }, 4000);
                        } else {
                            lblError.html('<%= lblItemError%>');

                            btnEnviar.attr('disabled', true);

                            txPosition.attr('disabled', true);
                            txLot.attr('disabled', true);
                            DdUnis.attr('disabled', true);
                            txQuantity.attr('disabled', true);

                            txPosition.val("");
                            limpiarPosition();
                            txLot.val("");
                            DdUnis.val("");
                            txQuantity.val("");

                            var MyElemnt = "#" + e.currentTarget.id;
                            $('#txItem').focus();
                        }
                    }
                }
            });

            txPosition.bind("change paste keyup", function (e) {

                if (e.currentTarget.value.trim() != "" && e.currentTarget.value != undefined && e.currentTarget.value != null) {
                    var Position = txPosition.val().trim();
                    var MyItem = txItem.val().trim().toUpperCase();
                    var OrdenID = txOrderID.val().trim();

                    if (TxOrderType.val() == "1") {
                        var Item = undefined; //LstSalesOrder.find(x => x.ORNO == OrdenID.toUpperCase() && x.ITEM == Item.toUpperCase() && x.PONO == Position);

                        LstSalesOrder.forEach(function (x) {
                            if (x.ORNO == OrdenID.toUpperCase() && x.PONO == Position) {
                                Item = x
                            }
                        });

                        if (Item != undefined) {
                            $('#lblsItemDesc').show(500);
                            //$('#lblUnidSt').val(Item.STUN);
                            $('#lblUnidSt').html(Item.STUN);
                            $('#lblUnidDsca').html(Item.DSCA);
                            $('#lblUnidDsca').show();
                            //$("#Contenido_DdUnit option[value='" + Item.STUN.trim() + "']").attr("selected", true);
                            $("#Contenido_DdUnit").val(Item.STUN.trim());
                            //$("#DdUnit option[value='" + Item.STUN.trim() + "']").attr("selected", true);
                            $("#DdUnit").val(Item.STUN.trim());
                            console.log("Si existe el la posicion con respecto al item y a la orden.");
                            lblError.html("");
                            if (Item.TERM == "1") {
                                txQuantity.attr('disabled', false);
                                txItem.attr('disabled', true);
                                console.log("El registro tiene Fecha vigente");
                                lblError.html("");
                                //txItem.focus();
                                $('#txItem').val(Item.ITEM);
                                if (Item.KLTC == "1") {
                                    txLot.attr('disabled', false);
                                    txLot.focus();
                                    if (Item.LSEL != "") {
                                        txLot.val(Item.LSEL);
                                        txLot.attr('disabled', true);
                                    }

                                }
                                else {
                                    txLot.attr('disabled', true);
                                    //DdUnis.focus();
                                }
                                ConsultarSumatoria130(OrdenID, Position, Item.SEQNR);
                                ConsultarFactoresporItem();
                                DdUnis.attr('disabled', true);
                                txQuantity.attr('disabled', false);

                                var mycant = parseFloat(Item.OQUA);

                                if (SumatoriaDevuelta == mycant) {
                                    txQuantity.attr('disabled', true);
                                    btnEnviar.attr('disabled', true);
                                    $('#ChkfinalReceipt').prop('checked', true);
                                }
                            }
                            else {

                                $('#lblUnidSt').html("");
                                $('#lblUnidDsca').html("");


                                btnEnviar.attr('disabled', true);

                                //txLot.attr('disabled', true);
                                txItem.attr('disabled', true);
                                DdUnis.attr('disabled', true);
                                txQuantity.attr('disabled', true);

                                //txLot.val("");
                                txItem.val("");
                                txQuantity.val("");

                                var MyElemnt = "#" + e.currentTarget.id;
                                $(MyElemnt).focus();
                                lblError.html('<%= lblDateError%>');
                            }

                        } else {
                            $('#lblsItemDesc').hide(500);
                            lblError.html('<%= lblPositionError%>');

                            btnEnviar.attr('disabled', true);

                            //txLot.attr('disabled', true);
                            txItem.attr('disabled', true);
                            DdUnis.attr('disabled', true);
                            txQuantity.attr('disabled', true);

                            //txLot.val("");
                            txItem.val("");
                            txQuantity.val("");

                            var MyElemnt = "#" + e.currentTarget.id;
                            $(MyElemnt).focus();
                        }
                    }
                    else if (TxOrderType.val() == "2") {
                        var Item = undefined; //LstPurchaseOrders.find(x => x.ORNO == OrdenID.toUpperCase() && x.ITEM == Item.toUpperCase() && x.PONO == Position);


                        LstPurchaseOrders.forEach(function (x) {
                            if (x.ORNO == OrdenID.toUpperCase() && x.PONO == Position) {
                                Item = x
                            }
                        });

                        if (Item != undefined) {
                            $('#lblsItemDesc').show(500);
                            $('#lblUnidSt').html(Item.STUN);
                            $('#lblUnidDsca').html(Item.DSCA);
                            $('#lblUnidDsca').show();
                            //$("#Contenido_DdUnit option[value='" + Item.STUN.trim() + "']").attr("selected", true);
                            $("#Contenido_DdUnit").val(Item.STUN.trim());
                            console.log("Si existe el la posicion con respecto al item y a la orden.");
                            if (Item.TERM == "1") {
                                //txQuantity.attr('disabled', false);
                                txItem.attr('disabled', true);
                                console.log("El registro tiene Fecha vigente");
                                lblError.html("");
                                //txItem.focus();
                                $('#txItem').val(Item.ITEM);

                                ConsultarSumatoria130(OrdenID, Position, Item.SEQNR);

                                if (Item.KLTC == "1") {
                                    txLot.attr('disabled', false);
                                    txLot.focus();
                                }
                                else {
                                    txLot.attr('disabled', true);
                                    //DdUnis.focus();
                                }
                                ConsultarFactoresporItem();
                                DdUnis.attr('disabled', true);
                                txQuantity.attr('disabled', false);
                            }
                            else {

                                btnEnviar.attr('disabled', true);

                                //txLot.attr('disabled', true);
                                txItem.attr('disabled', true);
                                DdUnis.attr('disabled', true);
                                txQuantity.attr('disabled', true);

                                //txLot.val("");
                                txItem.val("");
                                txQuantity.val("");

                                var MyElemnt = "#" + e.currentTarget.id;
                                $(MyElemnt).focus();
                                lblError.html('<%= lblDateError%>');
                                btnEnviar.attr('disabled', true);
                            }
                        } else {
                            $('#lblsItemDesc').hide(500);
                            lblError.html('<%= lblPositionError%>');

                            btnEnviar.attr('disabled', true);

                            //txLot.attr('disabled', true);
                            txItem.attr('disabled', true);
                            DdUnis.attr('disabled', true);
                            txQuantity.attr('disabled', true);

                            //txLot.val("");
                            txItem.val("");
                            txQuantity.val("");

                            var MyElemnt = "#" + e.currentTarget.id;
                            $(MyElemnt).focus();
                        }
                    }
                    else if (TxOrderType.val() == "22") {
                        var Item = undefined; // LstTransferOrder.find(x => x.ORNO == OrdenID.toUpperCase() && x.ITEM == Item.toUpperCase() && x.PONO == Position);

                        LstTransferOrder.forEach(function (x) {
                            if (x.ORNO == OrdenID.toUpperCase() && x.PONO == Position) {
                                Item = x
                            }
                        });

                        if (Item != undefined) {
                            $('#lblsItemDesc').show(500);
                            $('#lblUnidSt').html(Item.STUN);
                            $('#lblUnidDsca').html(Item.DSCA);
                            $('#lblUnidDsca').show();
                            $("#Contenido_DdUnit").val(Item.STUN.trim());
                            console.log("Si existe el la posicion con respecto al item y a la orden.");
                            if (Item.TERM == "1") {
                                //txQuantity.attr('disabled', false);
                                txItem.attr('disabled', true);
                                console.log("El registro tiene Fecha vigente");
                                lblError.html("");
                                //txItem.focus();
                                $('#txItem').val(Item.ITEM);
                                ConsultarSumatoria130(OrdenID, Position, Item.SEQNR);
                                if (Item.KLTC == "1") {
                                    txLot.attr('disabled', false);
                                    txLot.focus();
                                    if (Item.LSEL != "") {
                                        txLot.val(Item.LSEL);
                                        txLot.attr('disabled', true);
                                    }


                                }
                                else {
                                    txLot.attr('disabled', true);
                                    //DdUnis.focus();
                                }
                                ConsultarFactoresporItem();
                                DdUnis.attr('disabled', true);
                                txQuantity.attr('disabled', false);
                                var mycant = parseFloat(Item.OQUA);
                                if (SumatoriaDevuelta == mycant) {
                                    txQuantity.attr('disabled', true);
                                    btnEnviar.attr('disabled', true);
                                    $('#ChkfinalReceipt').attr('checked', true);
                                }
                            }
                            else {
                                $('#lblUnidSt').html("");
                                btnEnviar.attr('disabled', true);

                                txLot.attr('disabled', true);
                                txItem.attr('disabled', true);
                                DdUnis.attr('disabled', true);
                                txQuantity.attr('disabled', true);

                                txLot.val("");
                                txItem.val("");
                                txQuantity.val("");

                                var MyElemnt = "#" + e.currentTarget.id;
                                $(MyElemnt).focus();
                                lblError.html('<%= lblDateError%>');
                            }
                        } else {
                            $('#lblsItemDesc').hide(500);
                            lblError.html('<%= lblPositionError%>');

                            btnEnviar.attr('disabled', true);

                            //txLot.attr('disabled', true);
                            txLot.attr('disabled', true);
                            txItem.attr('disabled', true);
                            DdUnis.attr('disabled', true);
                            txQuantity.attr('disabled', true);

                            //txLot.val("");
                            txItem.val("");
                            txQuantity.val("");

                            var MyElemnt = "#" + e.currentTarget.id;
                            $(MyElemnt).focus();
                        }
                    }
                }
                else {

                    txLot.attr('disabled', true);
                    DdUnis.attr('disabled', true);
                    txQuantity.attr('disabled', true);

                    txItem.val("");
                    txLot.val("");
                    DdUnis.val("");
                    txQuantity.val("");
                }
            });

            $('#ChkfinalReceipt').click(function () {

                if (TxOrderType.val() == "2") {
                    if ($('#ChkfinalReceipt')[0].checked) {
                        $('#ShowModal').click();
                    }
                }
            }
            );

            $('#ShowModal').click(function () {
                var msg1 = "Confirm final receipt for PO " + $("#txOrderID").val() + " position " + $("#DdPosition").val() + "?";
                $("#label1").html(msg1);

            });

            txLot.bind("change paste keyup", function (e) {

                var Position = txPosition.val().trim();
                var MyItem = txItem.val().trim().toUpperCase();
                var OrdenID = txOrderID.val().trim();

                if (TxOrderType.val() == "1") {
                    var Item = undefined;
                    LstSalesOrder.forEach(function (x) {
                        if (x.ORNO == OrdenID.toUpperCase() && x.ITEM == MyItem && x.PONO == Position) {
                            Item = x
                        }
                    });

                }

                else if (TxOrderType.val() == "22") {
                    var Item = undefined;
                    LstTransferOrder.forEach(function (x) {
                        if (x.ORNO == OrdenID.toUpperCase() && x.ITEM == MyItem && x.PONO == Position) {
                            Item = x
                        }
                    });

                }

                //                else {
                //                    Data = "{'ITEM':'" + $('#txItem').val().trim() + "','CLOT':'" + $('#txLot').val().trim() + "'}";
                //                    sendAjax("ValidarLote", Data, ValidarLoteSucces, false);
                //                }
            });

        });


        $('#DdPosition').bind("change", function (e) {

            txPosition.val($('#DdPosition').val());
            txPosition.change();
        });


        function addOption() {

            var options = [];
            var unique = [];

            $('.rowTable').remove();
            $('#DdPosition option').remove(0);
            $('#DdPosition').append("<option value='0'>Select Position</option>");

            if (TxOrderType.val() == "1") {
                LstSalesOrder.forEach(function (x) {
                    if (x.ORNO.toUpperCase() == txOrderID.val().trim().toUpperCase()) {
                        optionValue = x.PONO.trim();
                        options.push(optionValue);
                    }
                });
            }
            else if (TxOrderType.val() == "2") {

                LstPurchaseOrders.forEach(function (x) {
                    if (x.ORNO.toUpperCase() == txOrderID.val().trim().toUpperCase()) {
                        optionValue = x.PONO.trim();
                        options.push(optionValue);
                    }
                });

            }
            else if (TxOrderType.val() == "22") {

                LstTransferOrder.forEach(function (x) {
                    if (x.ORNO.toUpperCase() == txOrderID.val().trim().toUpperCase()) {
                        optionValue = x.PONO.trim();
                        options.push(optionValue);
                    }
                });
            }



            unique = options.filter(onlyUnique);

            function onlyUnique(value, index, self) {
                return self.indexOf(value) === index;
            }

            unique.forEach(function (x) {
                optionValueX = x;
                $('#DdPosition').append("<option value=" + optionValueX + ">" + optionValueX + "</option>");

            });

            unique.forEach(function (x) {
                CurrentOption = x;
                PonoAnt = "";

                if (TxOrderType.val() == "1") {

                    LstSalesOrder.forEach(function (x) {
                        if (x.ORNO.toUpperCase() == txOrderID.val().trim().toUpperCase() && x.PONO.toUpperCase().trim() == CurrentOption.trim() && PonoAnt.trim() != x.PONO.toUpperCase().trim()) {
                            $('#DetailBody').append("<tr class='rowTable'><td>" + x.ORNO.trim() + "</rd><td>" + x.PONO.trim() + "</td><td>" + x.ITEM.trim() + "</td><td>" + x.DSCA.trim() + "</td><td>" + x.PRDT.trim() + "</td><td>" + x.QSTR.trim() + "</td><td>" + x.STUN.trim() + "</td><td>" + x.CWAR.trim() + "</td></tr>");
                            PonoAnt = x.PONO.toUpperCase().trim();
                        }
                    });
                }
                else if (TxOrderType.val() == "2") {

                    LstPurchaseOrders.forEach(function (x) {
                        if (x.ORNO.toUpperCase() == txOrderID.val().trim().toUpperCase() && x.PONO.toUpperCase().trim() == CurrentOption.trim() && PonoAnt.trim() != x.PONO.toUpperCase().trim()) {
                            $('#DetailBody').append("<tr class='rowTable'><td>" + x.ORNO.trim() + "</rd><td>" + x.PONO.trim() + "</td><td>" + x.ITEM.trim() + "</td><td>" + x.DSCA.trim() + "</td><td>" + x.PRDT.trim() + "</td><td>" + x.QSTR.trim() + "</td><td>" + x.STUN.trim() + "</td><td>" + x.CWAR.trim() + "</td></tr>");
                            PonoAnt = x.PONO.toUpperCase().trim();
                        }
                    });

                }
                else if (TxOrderType.val() == "22") {



                    LstTransferOrder.forEach(function (x) {
                        if (x.ORNO.toUpperCase() == txOrderID.val().trim().toUpperCase() && x.PONO.toUpperCase().trim() == CurrentOption.trim() && PonoAnt.trim() != x.PONO.toUpperCase().trim()) {
                            $('#DetailBody').append("<tr class='rowTable'><td>" + x.ORNO.trim() + "</rd><td>" + x.PONO.trim() + "</td><td>" + x.ITEM.trim() + "</td><td>" + x.DSCA.trim() + "</td><td>" + x.PRDT.trim() + "</td><td>" + x.QSTR.trim() + "</td><td>" + x.STUN.trim() + "</td><td>" + x.CWAR.trim() + "</td></tr>");
                            PonoAnt = x.PONO.toUpperCase().trim();
                        }
                    });
                }




            });

            options = [];
            unique = []
        }

        function limpiarPosition() {
            $('#DdPosition option').remove(0);
            $('#DdPosition').append("<option value='0'>Select Position</option>");
            $('.rowTable').remove();
        }
    </script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"
        integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1"
        crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"
        integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM"
        crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.1.1.min.js"></script>
</asp:Content>
