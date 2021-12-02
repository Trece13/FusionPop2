<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="5MRBMaterialsDoubleME.aspx.cs" Inherits="whusap.WebPages.Labels.RedesingLabels._5MRBMaterialsDoubleME" %>

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
            height: 5.5in;
            padding: 5px;
            border: 0px solid black;
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

        #table {
            font-size: 10px;
        }

        .table td,
        .table th {
            padding: .1rem;
            vertical-align: top;
            border-top: 1px solid #dee2e6;
            font-size: 1.5em;
            text-align: left;
        }

        .alingCenter {
            text-align: center;
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
                    <div class="row" style="height: 10%;">
                        <div class="col-4 alingLeft">
                            <strong>WO / PO&nbsp;</strong><label id="lblWorkOrder" runat="server"></label>
                        </div>
                        <div class="col-8 alingLeft">
                            <strong>&nbsp;</strong><label id="lblReason" runat="server"></label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 alingCenter">
                            <strong>
                                <label id="lblMaterialDesc" runat="server"></label>
                            </strong>
                        </div>
                    </div>
                    <div class="col-12 borderTop" id="divBarcode" style="height: 25%">
                        <img src="~/images/logophoenix_login.jpg" runat="server" id="codePaid" alt="" />
                    </div>
                    <div class="col-12 m-0 justify-content-center" style="height: 50%">
                        <table class="table col-auto p-5 mw-100" style="height: 100%">
                            <tbody>
                                <tr class="" style="height: 20% !important">
                                    <td class="col-12">
                                        <div class="row">
                                            <div class="col-12 alingCenter">
                                                <strong>
                                                    <label id="lblProductDesc" runat="server">XXXXXXXXXXXXXXXXXXXXXXXXX</label>-<label id="lblProductCode" runat="server">XXXXXXXXXXX</label></strong>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="" style="height: 20% !important">
                                    <td class="col-12">
                                        <div class="row">
                                            <div class="col-6 pl-4"><strong>Date</strong>&nbsp;&nbsp;<label id="lblDate" runat="server"></label></div>
                                            <div class="col-6"><strong>Quantity</strong>&nbsp;&nbsp;<label id="lblQuantity" runat="server"></label></div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="" style="height: 20% !important">
                                    <td class="col-12">
                                        <div class="row">
                                            <div class="col-7 pl-4"><strong>Finished/WIP PID</strong>&nbsp;&nbsp;<label id="lblFinished" runat="server"></label></div>
                                            <div class="col-5"><strong>Pallet #</strong>&nbsp;&nbsp;<label id="lblPallet" runat="server"></label></div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="" style="height: 20% !important">
                                    <td class="col-12">
                                        <div class="row">
                                            <div class="col-6 pl-4"><strong>Printed By</strong>&nbsp;&nbsp;<label id="lblPrintedBy" runat="server"></label></div>
                                            <div class="col-6"><strong>Machine</strong>&nbsp;&nbsp;<label id="lblMachine" runat="server"></label></div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="" style="height: 20% !important">
                                    <td class="col-10">
                                        <div class="row">
                                            <div class="col-12 pl-4"><strong>Comments</strong>&nbsp;&nbsp;<label id="lblComments" runat="server"></label></div>
                                        </div>
                                    </td>
                                    <td class="col-2">
                                        <div id="lblReprint" class="col-12 text-right" runat="server">
                                            <label><strong>REPRINT</strong></label>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <br />
                <div id="myLabel2">
                    <div class="row" style="height: 10%;">
                        <div class="col-4 alingLeft">
                            <strong>WO / PO&nbsp;</strong><label id="lblWorkOrder2" runat="server"></label>
                        </div>
                        <div class="col-8 alingLeft">
                            <strong>Reason&nbsp;</strong><label id="lblReason2" runat="server"></label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 alingCenter">
                            <strong>
                                <label id="lblMaterialDesc2" runat="server">THIS PRODUCT IS ON HOLD PENDING DISPOSITION</label></strong>
                        </div>
                    </div>
                    <div class="col-12 borderTop" id="divBarcode2" style="height: 25%">
                        <img src="~/images/logophoenix_login.jpg" runat="server" id="codePaid2" alt="" />
                    </div>
                    <div class="col-12 m-0 justify-content-center" style="height: 50%">
                        <table class="table col-auto p-5 mw-100" style="height: 100%">
                            <tbody>
                                <tr class="" style="height: 20% !important">
                                    <td class="col-12">
                                        <div class="row">
                                            <div class="col-12 alingCenter">
                                                <strong>
                                                    <label id="lblProductDesc2" runat="server">XXXXXXXXXXXXXXXXXXXXXXXXX</label>-<label id="lblProductCode2" runat="server">XXXXXXXXXXX</label></strong>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="" style="height: 20% !important">
                                    <td class="col-12">
                                        <div class="row">
                                            <div class="col-6 pl-4"><strong>Date</strong>&nbsp;&nbsp;<label id="lblDate2" runat="server"></label></div>
                                            <div class="col-6"><strong>Quantity</strong>&nbsp;&nbsp;<label id="lblQuantity2" runat="server"></label></div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="" style="height: 20% !important">
                                    <td class="col-12">
                                        <div class="row">
                                            <div class="col-7 pl-4"><strong>Finished/WIP PID</strong>&nbsp;&nbsp;<label id="lblFinished2" runat="server"></label></div>
                                            <div class="col-5"><strong>Pallet #</strong>&nbsp;&nbsp;<label id="lblPallet2" runat="server"></label></div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="" style="height: 20% !important">
                                    <td class="col-12">
                                        <div class="row">
                                            <div class="col-6 pl-4"><strong>Printed By</strong>&nbsp;&nbsp;<label id="lblPrintedBy2" runat="server"></label></div>
                                            <div class="col-6"><strong>Machine</strong>&nbsp;&nbsp;<label id="lblMachine2" runat="server"></label></div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="" style="height: 20% !important">
                                    <td class="col-10">
                                        <div class="row">
                                            <div class="col-12 pl-4"><strong>Comments</strong>&nbsp;&nbsp;<label id="lblComments2" runat="server"></label></div>
                                        </div>
                                    </td>
                                    <td class="col-2">
                                        <div id="lblReprint2" class="col-12 text-right" runat="server">
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
