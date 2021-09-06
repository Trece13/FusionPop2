<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="2RollStockME.aspx.cs" Inherits="whusap.WebPages.Labels.RedesingLabels._2RollStockME" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css"
        integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p"
        crossorigin="anonymous" />
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
            width: 100%;
            height: 650px;
            padding: 5px;
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
            height: 100%;
            width: 100%;
        }

        #codeMaterial {
            display: block;
            margin: auto;
            height: 100%;
            width: 100%;
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
                <button type="button" onclick="javascript:printDiv('printSpace')" class="btn btn-link col-12 "><i class="fas fa-print fa-2x" id="btnPrint"></i></button>
            </div>
            <br />
            <div id="printSpace">
                <div id="myLabel">
                    <div class="row" style="height: 10%">
                        <div class="col-6 alingLeft">
                            <label>
                                <strong>
                                    <label id="lblMaterialDesc" runat="server">MATERIAL DESCRIPTION</label></strong>
                            </label>
                        </div>
                        <div class="col-6 alingRight">
                            <img src="~/images/logophoenix_login.jpg" runat="server" id="codeMaterial" alt="" />
                        </div>
                    </div>
                    <br />
                    <div class="col-12 borderTop" id="divBarcode" style="height: 30%">
                        <img src="~/images/logophoenix_login.jpg" runat="server" id="codePaid" alt="" />
                    </div>
                    <div style="height: 50%">
                        <table class="table mw-100" style="height: 100%">
                            <tbody>
                                <tr style="height: 20%">
                                    <td colspan="2"><strong>WO Lot</strong>&nbsp;&nbsp;<label id="lblLot" runat="server"></label></td>
                                    <td rowspan="2"><strong>Quantity</strong>&nbsp;&nbsp;<label id="lblQuantity" runat="server"></label></strong><label id="lblUnit"></label></td>
                                </tr>
                                <tr style="height: 20%" colspan="2">
                                    <td><strong>Date</strong>&nbsp;&nbsp;<label id="lblDate" runat="server"></label></td>
                                </tr>
                                <tr style="height: 20%">
                                    <td><strong>Machine</strong>&nbsp;&nbsp;<label id="lblMachine" runat="server"></label></td>
                                    <td><strong>Operator</strong>&nbsp;&nbsp;<label id="lblOperator" runat="server"></label></td>
                                    <td><strong>Winder</strong>&nbsp;&nbsp;<label id="lblWinder" runat="server"></label></td>
                                </tr>
                                <tr style="height: 20%">
                                    <td><strong>Pallet #</strong>&nbsp;&nbsp;<label id="lblPallet" runat="server"></label></td>
                                    <td><strong>Made in Dublin VA</strong></td>
                                    <td><strong>L-R</strong></td>
                                </tr>
                                <tr style="height: 20%">
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <div id="lblReprint" class="col-12 text-right" runat="server">
                                            <label><strong>REPRINT</strong></label>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
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
