<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="4FinishedCups.aspx.cs" Inherits="whusap.WebPages.Labels.RedesingLabels._4FinishedCups" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
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

        #myLabel {
            width: 6in;
            height: 3in;
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
            height: 75px;
            width: 500px;
        }

        #codeMaterial {
            display: block;
            margin: auto;
            height: 70px;
            width: 220px;
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
            height: 140px;
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
            font-size: 1.05em;
            text-align: left;
            vertical-align: middle;
            padding-left: 1em;
        }
        @page{
            size:landscape;
            margin:3px;
        }
    </style>
    <script type="text/javascript">
        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html>";
            document.body.innerHTML += "<head>";
            document.body.innerHTML += "</head>";
            document.body.innerHTML += "<body>";
            document.body.innerHTML += divElements;
            document.body.innerHTML += "</body>";

            window.print();
            document.body.innerHTML = oldPage;
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
                <div id="myLabel">
                    <div class="row">
                        <div class="col-6 alingLeft">
                            <label>
                                <strong>
                                    <label id="lblMaterialDesc" style="font-size: 1.1em;" runat="server">MATERIAL DESCRIPTION</label></strong>
                            </label>
                        </div>
                        <div class="col-6 alingRight">
                            <img id="codeMaterial" src="~/images/logophoenix_login.jpg" runat="server" />
                        </div>
                    </div>
                    <br />
                    <div class="col-12 borderTop" id="divBarcode">
                        <img id="codePaid" src="~/images/logophoenix_login.jpg" runat="server" />
                    </div>
                    <div>
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
                    <div id="lblReprint" class="col-12 text-right" runat="server">
                        <label><strong>REPRINT</strong></label>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="styles/jquery-3.2.1.slim.min.js"></script>
    <script src="styles/popper.min.js"></script>
    <script src="styles/bootstrap4.min.js"></script>
</body>
</html>
