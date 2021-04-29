<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="5MRBMaterials.aspx.cs" Inherits="whusap.WebPages.Labels.RedesingLabels._5MRBMaterials" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
            height: 100px;
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
            vertical-align: top;
            border-top: 1px solid #dee2e6;
            font-size: 12px;
            text-align: left;
        }

        .alingCenter {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="printContainer">
        <div id="printSpace">
            <div id="myLabel" class="container">
                <div class="row">
                    <div class="col-4 alingLeft">
                        <strong>WO&nbsp;</strong><label id="lblWorkOrder" runat="server"></label>
                    </div>
                    <div class="col-8 alingLeft">
                        <strong>Reason&nbsp;</strong><label id="lblReason" runat="server"></label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 alingCenter">
                        <strong><label id="lblMaterialDesc" runat="server">THIS PRODUCT IS ON HOLD PENDING DISPOSITION</label></strong>
                    </div>
                </div>
                <div class="col-12 borderTop" id="divBarcode">
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="codePaid" alt="" />
                </div>
                <div>
                    <table class="table">
                        <tbody>
                            <tr class="row">
                                <td class="col-12">
                                    <div class="row">
                                        <div class="col-12 alingCenter"><strong><label id="lblProductDesc" runat="server">XXXXXXXXXXXXXXXXXXXXXXXXX</label>-<label id="lblProductCode" runat="server">XXXXXXXXXXX</label></strong></div>
                                    </div>
                                </td>
                            </tr>
                            <tr class="row">
                                <td class="col-12">
                                    <div class="row">
                                        <div class="col-6"><strong>Date</strong>&nbsp;&nbsp;<label id="lblDate" runat="server"></label></div>
                                        <div class="col-6"><strong>Quantity</strong>&nbsp;&nbsp;<label id="lblQuantity" runat="server"></label></div>
                                    </div>
                                </td>
                            </tr>
                            <tr class="row">
                                <td class="col-12">
                                    <div class="row">
                                        <div class="col-7"><strong>Finished/WIP PID</strong>&nbsp;&nbsp;<label id="lblFinished" runat="server"></label></div>
                                        <div class="col-5"><strong>Pallet #</strong>&nbsp;&nbsp;<label id="lblPallet" runat="server"></label></div>
                                    </div>
                                </td>
                            </tr>
                            <tr class="row">
                                <td class="col-12">
                                    <div class="row">
                                        <div class="col-6"><strong>Printed By</strong>&nbsp;&nbsp;<label id="lblPrintedBy" runat="server"></label></div>
                                        <div class="col-6"><strong>Machine</strong>&nbsp;&nbsp;<label id="lblMachine" runat="server"></label></div>
                                    </div>
                                </td>
                            </tr>
                            <tr class="row">
                                <td class="col-12">
                                    <div class="row">
                                        <div class="col-12"><strong>Comments</strong>&nbsp;&nbsp;<label id="lblComments" runat="server"></label></div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <br />
        <div id="printButton" class="container">
            <button type="button" onclick="" class="btn btn-link col-12"><i class="fas fa-print" id="btnPrint"></i></button>
        </div>
    </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" crossorigin="anonymous"></script>
</body>
</html>
