<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="1RawMaterialME.aspx.cs" Inherits="whusap.WebPages.Labels.RedesingLabels._1RawMaterialME" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="styles/fontawesome5.1.css"/>
    <link rel="stylesheet" href="styles/bootstrap4.css"/>
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
            width: 85%;
            height: 440px;
            padding: 0px;
            border-radius: 12px;
        }

        .alingRight {
            text-align: right;
            top: 0px;
            left: 0px;
        }

        .alingLeft {
            text-align: left;
        }

        #printButton {
            width: 6in;
        }

        #codePaid {
            display: block;
            height: 100%;
            width: 740px;
			margin-left: 40px;
        }

        #codeMaterial, #codeMaterial2 {
            display: block;
            margin: auto;
            height: 110%;
            width: 100%
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
            font-size: 1.7em;
            text-align: left;
            vertical-align: middle;
            padding-left: 1em;
        }

        #lblMaterialDesc, #lblMaterialCode{
            font-size: ;
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
                <button type="button" onclick="javascript:printDiv('printSpace')" class="btn btn-link col-12 "><i class="fas fa-print fa-2x" id="btnPrint"></i></button>
            </div>
            <br />
            <div id="printSpace">
                <div id="myLabel">
                    <div class="row" style="height:20%">
                        <div class="col-6 alingLeft">
                            <label id="lblMaterialDesc" runat="server"><strong>MATERIAL DESCRIPTION</strong></label>
                        </div>
                        <div class="col-6 alingRight" style="height:100%">
                            <img src="~/images/logophoenix_login.jpg" runat="server" id="codeMaterial" alt="" />
                        </div>
                    </div>
                    <br />
                    <div class="col-12 borderTop" id="divBarcode">
                        <img src="~/images/logophoenix_login.jpg" runat="server" id="codePaid" alt="" />
                    </div>
                    <div style="height:50%">
                        <table class="table mw-100" style="height:100%">
                            <tbody>
                                <tr>
                                    <td><strong>LOT</strong>&nbsp;&nbsp;<asp:Label ID="lblLot" runat="server"></asp:Label></td>
                                    <td><strong>Quantity</strong>&nbsp;&nbsp;<asp:Label ID="lblQuantity" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><strong>Origin Lot</strong>&nbsp;&nbsp;<asp:Label ID="lblOrigin" runat="server"></asp:Label></td>
                                    <td><strong>Supplier</strong>&nbsp;&nbsp;<asp:Label ID="lblSupplier" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><strong>Received By</strong>&nbsp;&nbsp;<asp:Label ID="lblRecibedBy" runat="server"></asp:Label></td>
                                    <td><strong>Received On</strong>&nbsp;&nbsp;<asp:Label ID="lblRecibedOn" runat="server"></asp:Label></td>
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
