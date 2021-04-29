<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="1RawMaterial.aspx.cs" Inherits="whusap.WebPages.Labels.RedesingLabels._1RawMaterial" %>
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
            height: 170px;
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
        
        #lblDesc {}
        
        #lblMadein {}
        
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
            height: 186px;
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
    </style>
    <script type="text/javascript">
        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html";
            document.body.innerHTML += "><head";
            document.body.innerHTML += "><title";
            document.body.innerHTML += "></title"
            document.body.innerHTML += "></head";
            document.body.innerHTML += "><body";
            document.body.innerHTML += ">" + divElements;
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
    <div id="printContainer">
        <div id="printSpace">
            <div id="myLabel" class="container">
                <div class="row">
                    <div class="col-6 alingLeft">
                        <label id="lblMaterialDesc" runat="server"><strong>MATERIAL DESCRIPTION</strong></label>
                    </div>
                    <div class="col-6 alingRight">
                        <label id="lblMaterialCode" runat="server"><strong>MATERIAL CODE</strong></label>
                    </div>
                </div>
                <br />
                <div class="col-12 borderTop" id="divBarcode">
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="codePaid" alt="" />
                </div>
                <br />
                <div>
                    <table class="table">
                        <tbody>
                            <tr>
                                <td><strong>LOT</strong>&nbsp;&nbsp;<asp:Label id="lblLot" runat="server"></asp:Label></td>
                                <td><strong>Quantity</strong>&nbsp;&nbsp;<asp:Label id="lblQuantity"  runat="server"></asp:Label></td>
                            </tr>
                        </tbody>
                        <thead>
                            <tr>
                                <td><strong>Origin Lot</strong>&nbsp;&nbsp;<asp:Label id="lblOrigin" runat="server"></asp:Label></td>
                                <td><strong>Supplier</strong>&nbsp;&nbsp;<asp:Label id="lblSupplier" runat="server"></asp:Label></td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td><strong>Received By</strong>&nbsp;&nbsp;<asp:Label id="lblRecibedBy" runat="server"></asp:Label></td>
                                <td><strong>Received On</strong>&nbsp;&nbsp;<asp:Label id="lblRecibedOn" runat="server"></asp:Label></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <br />
        <div id="printButton" class="container" runat="server">
            <button type="button" onclick="" class="btn btn-link col-12"><i class="fas fa-print" id="btnPrint"></i></button>
        </div>
    </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" crossorigin="anonymous"></script>
</body>
</html>
