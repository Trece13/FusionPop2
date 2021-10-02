<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="3RegrindsDoubleME.aspx.cs" Inherits="whusap.WebPages.Labels.RedesingLabels._3RegrindsDoubleME" %>

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

        #LblDate, #LblDate2 {
            font-size: 14px !important;
        }

        #LblReprintInd,
        #LblReprint, #LblReprint2 {
            display: none;
        }

        tr {
            text-align: center;
        }

        th {
            text-align: center;
        }

        #myLabel, #myLabel2 {
            width: 8.5in;
            height: 6in;
            padding: 0px;
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

        #codePaid, #codePaid2 {
            display: block;
            margin: auto;
            height: 100px;
            width: 500px;
        }

        #codeMaterial, #codeMaterial2 {
            display: block;
            margin: auto;
            height: 50px;
            width: 100%;
        }

        .borderTop {
            border-top: solid 1px gray;
        }

        #printContainer {
            margin-bottom: 100px;
        }

        #divBarcode, #divBarcode2 {
            height: 140px;
            padding: inherit;
        }

        .table td,
        .table th {
            padding: .1rem;
            border-top: 1px solid #dee2e6;
            font-size: 1.7em;
            text-align: left;
            vertical-align: middle;
            padding-left: 1em;
        }

        @page {
            size: landscape;
            margin: 3px;
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
                <button type="button" onclick="javascript:printDiv('printSpace')" class="btn btn-link col-12 ">
                    <img src="images\printer.svg" height="30px" width="30px" /></button>

            </div>
            <br />
            <div id="printSpace">
                <div id="myLabel">
                    <div class="row">
                        <div class="col-6 alingLeft">
                            <label>
                                <strong>
                                    <label id="lblMaterialDesc" runat="server">MATERIAL DESCRIPTION</label></strong>
                            </label>
                        </div>
                        <div class="col-6 alingRight" style="height: 100%">
                            <img src="~/images/logophoenix_login.jpg" id="codeMaterial" runat="server" />
                        </div>
                    </div>
                    <div class="col-12 borderTop" id="divBarcode" style="height: 30% !important">
                        <img src="~/images/logophoenix_login.jpg" id="codePaid" runat="server" />
                    </div>
                    <div>
                        <table class="table mw-100" style="height: 15%">
                            <tbody>
                                <tr>
                                    <td><strong>WO Lot</strong>&nbsp;&nbsp;<label id="lblLot" runat="server"></label></td>
                                    <td rowspan="2" colspan="2"><strong>Quantity</strong>&nbsp;&nbsp;<label id="lblQuantity" runat="server"></label></td>
                                </tr>
                                <tr>
                                    <td><strong>Date</strong>&nbsp;&nbsp;<label id="lblDate" runat="server"></label></td>
                                </tr>
                                <tr>
                                    <td><strong>Machine</strong>&nbsp;&nbsp;<label id="lblMachine" runat="server"></label></td>
                                    <td><strong>Operator</strong>&nbsp;&nbsp;<label id="lblOperator" runat="server"></label></td>
                                    <td rowspan="2"></td>
                                </tr>
                                <tr>
                                    <td><strong>Pallet #</strong>&nbsp;&nbsp;<label id="lblPallet" runat="server"></label></td>
                                    <td><strong>Made in Dublin VA</strong></td>
                                </tr>
                                <tr>
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
                <div id="myLabel2">
                    <div class="row">
                        <div class="col-6 alingLeft">
                            <label>
                                <strong>
                                    <label id="lblMaterialDesc2" runat="server">MATERIAL DESCRIPTION</label></strong>
                            </label>
                        </div>
                        <div class="col-6 alingRight">
                            <img src="~/images/logophoenix_login.jpg" id="codeMaterial2" runat="server" />
                        </div>
                    </div>
                    <div class="col-12 borderTop" id="divBarcode2" style="height: 30% !important">
                        <img src="~/images/logophoenix_login.jpg" id="codePaid2" runat="server" />
                    </div>
                    <div style="height: 10%">
                        <table class="table mw-100">
                            <tbody>
                                <tr>
                                    <td><strong>WO Lot</strong>&nbsp;&nbsp;<label id="lblLot2" runat="server"></label></td>
                                    <td rowspan="2" colspan="2"><strong>Quantity</strong>&nbsp;&nbsp;<label id="lblQuantity2" runat="server"></label></td>
                                </tr>
                                <tr>
                                    <td><strong>Date</strong>&nbsp;&nbsp;<label id="lblDate2" runat="server"></label></td>
                                </tr>
                                <tr>
                                    <td><strong>Machine</strong>&nbsp;&nbsp;<label id="lblMachine2" runat="server"></label></td>
                                    <td><strong>Operator</strong>&nbsp;&nbsp;<label id="lblOperator2" runat="server"></label></td>
                                    <td rowspan="2"></td>
                                </tr>
                                <tr>
                                    <td><strong>Pallet #</strong>&nbsp;&nbsp;<label id="lblPallet2" runat="server"></label></td>
                                    <td><strong>Made in Dublin VA</strong></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <div id="lblReprint2" class="col-12 text-right" runat="server">
                                            <label><strong></strong></label>
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
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var now = new Date();
            document.getElementById('lblDate').innerHTML = now.getMonth() + 1 + "/" + now.getDate() + "/" + now.getFullYear() + " " + now.getHours() + ":" + now.getUTCMinutes();
            document.getElementById('lblDate2').innerHTML = now.getMonth() + 1 + "/" + now.getDate() + "/" + now.getFullYear() + " " + now.getHours() + ":" + now.getUTCMinutes();
        });
    </script>
    <script src="styles/jquery-3.2.1.slim.min.js"></script>
    <script src="styles/popper.min.js"></script>
    <script src="styles/bootstrap4.min.js"></script>
</body>
</html>
