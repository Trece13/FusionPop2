<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="4FinishedCupsDoubleME.aspx.cs" Inherits="whusap.WebPages.Labels.RedesingLabels._4FinishedCupsDoubleME" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css"
        integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p"
        crossorigin="anonymous" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" crossorigin="anonymous">
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

        #myLabel,#myLabel2 {
            width: 100%;
            height: 650px;
            padding: 5px;
            border: 1px solid black;
            border-radius: 12px;
            margin:0;
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

        #codePaid,#codePaid2 {
            display: block;
            margin: auto;
            height: 100%;
            width: 100%;
        }

        #codeMaterial,#codeMaterial2 {
            display: block;
            margin: auto;
            height: 100%;
            width: 100%;
        }

        #codeItem,#codeItem2 {
            display: block;
            margin: auto;
            height: 100%;
            width: 100%;
        }

        #itemDesc,#itemDesc2 {
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

        #divBarcode,#divBarcode2 {
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
            padding: .1rem;
            border-top: 1px solid #dee2e6;
            font-size: 1.5em;
            text-align: left;
            vertical-align: middle;
            padding-left: 1em;
        }
    </style>
    <script type="text/javascript">
        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;

            var mywindow = window.open('', 'PRINT', 'height=400px,width=600px');
            mywindow.document.write('<html><head>');
            mywindow.document.write('</head><body><style> body{ width:5.5in; height:3.4in;} @page {size: 6in 4in landscape; margin: 0;} .table {font-size:13px;} .table td{padding: 0.25rem !important;}</style>');
            mywindow.document.write(document.getElementById(divID).innerHTML);
            mywindow.document.write('<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">');
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
                <button type="button" onclick="printDiv('printSpace')" class="btn btn-link col-12 "><i class="fas fa-print fa-2x" id="btnPrint"></i></button>
            </div>
            <br />
            <div id="printSpace">
                <div id="myLabel" runat="server">
                    <div class="row" style="margin:auto; height:10% !important">
                        <div class="col-6 alingLeft">
                            <label>
                                <strong>
                                    <label id="lblMaterialDesc" runat="server">MATERIAL DESCRIPTION</label></strong>
                            </label>
                        </div>
                        <div class="col-6 alingRight">
                            <img id="codeMaterial" src="~/images/logophoenix_login.jpg" runat="server"/>
                        </div>
                    </div>
                    <br />
                    <div class="col-12 borderTop" id="divBarcode">
                        <img id="codePaid" src="~/images/logophoenix_login.jpg" runat="server"/>
                    </div>
                    <div style="height:50%">
                        <table class="table mw-100" style="height:100%">
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
                <div id="myLabel2">
                    <div class="row" style="margin:auto; height:10% !important">
                        <div class="col-6 alingLeft">
                            <label>
                                <strong>
                                    <label id="lblMaterialDesc2" runat="server">MATERIAL DESCRIPTION</label></strong>
                            </label>
                        </div>
                        <div class="col-6 alingRight">
                            <img id="codeMaterial2" src="~/images/logophoenix_login.jpg" runat="server"/>
                        </div>
                    </div>
                    <br />
                    <div class="col-12 borderTop" id="divBarcode">
                        <img id="codePaid2" src="~/images/logophoenix_login.jpg" runat="server" />
                    </div>
                    <div style="height:50%">
                        <table class="table mw-100" style="height:100%">
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
            </div>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" crossorigin="anonymous"></script>
</body>
</html>
