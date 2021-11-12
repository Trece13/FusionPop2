<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="4FinishedCupsDoubleME.aspx.cs" Inherits="whusap.WebPages.Labels.RedesingLabels._4FinishedCupsDoubleME" %>

<!DOCTYPE html>

<html xmlns=http://www.w3.org/1999/xhtml>
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="styles/fontawesome5.1.css" />
    <link rel="stylesheet" href="styles/bootstrap4.css" />
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
        #myLabel, #myLabel2 {
            width: 6in;
            height: 3.5in;
            padding: 0px;
            border-radius: 12px;
            margin: 0;
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
        #codePaid, #codePaid2 {
            display: block;
            margin: auto;
            height: 75px;
            width: 500px;
        }
        #codeMaterial, #codeMaterial2 {
            display: block;
            margin: auto;
            height: 50px;
            width: 220px;
        }
        #codeItem, #codeItem2 {
            display: block;
            margin: auto;
            height: 50px;
            width: 100%;
        }
        #itemDesc, #itemDesc2 {
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
        #divBarcode, #divBarcode2 {
            height: 30%;
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
            padding: 0rem;
            border-top: 1px solid #dee2e6;
            font-size: 2em;
            text-align: left;
            vertical-align: middle;
            padding-left: 0em;
        }
        @page {
            size: landscape;
        }
    </style>
    <script type="text/javascript">
        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            var mywindow = window.open('', 'PRINT', 'height=400px,width=600px');
            mywindow.document.write('<html><head>');
            mywindow.document.write('</head><body style="margin-top: 1px; margin-left: 1px; margin-right: 1px;"><style>@page{size:landscape;}</style>');
            mywindow.document.write(document.getElementById(divID).innerHTML);
            mywindow.document.write('<link rel="stylesheet" href="styles/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">');
            mywindow.document.write('</body></html>');
            setTimeout(function () {
                mywindow.print();
            }, 3000);
            mywindow.document.close(); // necessary for IE >= 10
            //setTimeout(window.close(),15000);
        };
        function addZero(i) {
            if (i < 10) {
                i = "0" + i;
            }
            return i;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="printContainer" class="container">
            <div id="printButton" runat="server">
                               <button type="button" onclick="javascript:printDiv('printSpace')" class="btn btn-link col-12 "><img src="images\printer.svg" height="30px" width="30px"/></button>

            </div>
            <br />
            <div id="printSpace">
                <div id="myLabel" runat="server">
                    <div class="row">
                        <div class="col-6 alingLeft">
                            <label>
                                <strong>
                                    <label id="lblMaterialDesc" runat="server">MATERIAL DESCRIPTION</label></strong>
                            </label>
                        </div>
                        <div class="col-6 alingRight">
                            <img id="codeMaterial" src="~/images/logophoenix_login.jpg" runat="server" />
                        </div>
                    </div>
                    <br />
                    <div class="col-12 borderTop" id="divBarcode1">
                        <img id="codePaid" src="~/images/logophoenix_login.jpg" runat="server" />
                    </div>
                    <div style="height: 15%">
                        <table class="table mw-100">
                            <tbody>
                                <tr>
                                    <td><strong>WO Lot</strong>&nbsp;&nbsp;<label id="lblLot" runat="server"></label></td>
                                    <td><strong>Quantity</strong>&nbsp;&nbsp;<label id="lblQuantity" runat="server"></label></td>
                                </tr>
                                <tr>
                                    <td colspan="2"><strong>Date</strong>&nbsp;&nbsp;<label id="lblDate" runat="server"></label></td>
                                    <td><strong>Pallet #</strong>&nbsp;&nbsp;<label id="lblPallet" runat="server"></label></td>
                                </tr>
                                <tr>
                                    <td><strong>Machine</strong>&nbsp;&nbsp;<label id="lblMachine" runat="server"></label></td>
                                    <td><strong>Made in Dublin VA</strong></td>
                                    <td><strong>Operator</strong>&nbsp;&nbsp;<label id="lblOperator" runat="server"></label></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <br />
                <div id="myLabel2" runat="server">
                    <div class="row">
                        <div class="col-6 alingLeft">
                            <label>
                                <strong>
                                    <label id="lblMaterialDesc2" runat="server">MATERIAL DESCRIPTION</label></strong>
                            </label>
                        </div>
                        <div class="col-6 alingRight">
                            <img id="codeMaterial2" src="~/images/logophoenix_login.jpg" runat="server" />
                        </div>
                    </div>
                    <br />
                    <div class="col-12 borderTop" id="divBarcode2">
                        <img id="codePaid2" src="~/images/logophoenix_login.jpg" runat="server" />
                    </div>
                    <div style="height: 10%">
                        <table class="table mw-100">
                            <tbody>
                                <tr>
                                    <td><strong>WO Lot</strong>&nbsp;&nbsp;<label id="lblLot2" runat="server"></label></td>
                                    <td><strong>Quantity</strong>&nbsp;&nbsp;<label id="lblQuantity2" runat="server"></label></td>
                                </tr>
                                <tr>
                                    <td colspan="2"><strong>Date</strong>&nbsp;&nbsp;<label id="lblDate2" runat="server"></label></td>
                                    <td><strong>Pallet #</strong>&nbsp;&nbsp;<label id="lblPallet2" runat="server"></label></td>
                                </tr>
                                <tr>
                                    <td><strong>Machine</strong>&nbsp;&nbsp;<label id="lblMachine2" runat="server"></label></td>
                                    <td><strong>Made in Dublin VA</strong></td>
                                    <td><strong>Operator</strong>&nbsp;&nbsp;<label id="lblOperator2" runat="server"></label></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            <div id="MyEtiquetaDrop" style="width:6in; height:3in;" runat="server">
                <table style="margin: auto;margin-top:15px;">
                    <tr>
                        <td>
                            <label style="font-size: 30px">
                                Pick ID</label>
                        </td>
                        <td colspan="4">
                            <img src="~/images/logophoenix_login.jpg" runat="server" id="bcPick" alt="" hspace="60"
                                vspace="5" style="width: 3in; height: 1in; margin: 0px !important" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-size: 30px">
                                Machine</label>
                        </td>
                        <td style="text-align: center;">
                            <label style="font-size: 30px" id="lbMcno" runat="server">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-size: 30px">
                                Pallet(s)
                            </label>
                        </td>
                        <td style="text-align: center;">
                            <label style="font-size: 20px" id="lbPaid" runat="server"></label>
                            <label style="font-size: 20px" id="lbSep" runat="server">/</label>
                            <label style="font-size: 20px" id="lbQtyp" runat="server"></label>
                        </td>
                    </tr>
                </table>
            </div>
            </div>
        </div>
    </form>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var now = new Date();
            document.getElementById('lblDate') != null ? document.getElementById('lblDate').innerHTML = now.getMonth() + 1 + "/" + now.getDate() + "/" + now.getFullYear() + " " + now.getHours() + ":" + now.getUTCMinutes() : "";
            document.getElementById('lblDate2') != null ? document.getElementById('lblDate2').innerHTML = now.getMonth() + 1 + "/" + now.getDate() + "/" + now.getFullYear() + " " + now.getHours() + ":" + now.getUTCMinutes() : "";
        });
    </script>
    <script src="styles/jquery-3.2.1.slim.min.js"></script>
    <script src="styles/popper.min.js"></script>
    <script src="styles/bootstrap4.min.js"></script>

</body>
</html>