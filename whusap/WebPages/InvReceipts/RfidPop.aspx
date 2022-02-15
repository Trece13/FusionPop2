<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="RfidPop.aspx.cs" Inherits="whusap.WebPages.InvReceipts.RfidPop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <script type="text/javascript">
        var timer;

        function stoper() {

            clearTimeout(timer);
        }
    </script>
    <link rel="stylesheet" href="styles/bootstrap.min.css">
    <link rel="stylesheet" href="styles/font-awesome.min.css">
    <style type="text/css">
        #lblUnidDsca {
            font-size: small;
        }

        #lblUnidSt {
            font-size: small;
        }

        #loadingIcon {
            display: none;
        }

        #ShowModalMsg {
            display: none;
        }

        .form-group {
            margin-bottom: 1.5rem;
        }

        #lblError {
            color: Red;
            font-size: 24px;
        }

        #LabelQuantityDiv {
            display: none;
        }

        #lblsItemDesc {
            display: none;
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

        #itemDesc {
            vertical-align: middle;
            font-size: 21px;
        }

        #lblError {
            color: red;
            font-size: 13px;
        }
    </style>
    <form id="form1" class="container col-sm-12">
        <div class="row">
            <div class="col-sm-6">
                <div class="form-group row">
                    <label class="col-sm-4" for="txPaid">
                        Pallet ID</label>
                    <div class="col-sm-6">
                        <input type="text" class="form-control" id="txPaid" placeholder="Pallet id" tabindex="1">
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4" for="txOrderID">
                        Rfid</label>
                    <div class="col-sm-6">
                        <input type="text" class="form-control" id="txRfid" placeholder="Rfid" tabindex="1" disabled>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4" for="txItem">
                        Item</label>
                    <div class="col-sm-6">
                        <input type="text" class="form-control" id="txItem" placeholder="Item"
                            data-method="ValidarItem" tabindex="1" readonly>
                    </div>
                </div>
                <div class="form-group row" id="lblsItemDesc">
                    <label class="col-sm-4"></label>
                    <div class="col-sm-6">
                        <label class="col-sm-10" id="lblUnidDsca">-</label>
                        <label class="col-sm-1" id="lblUnidSt">-</label>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4" for="txLot">
                        Lot</label>
                    <div class="col-sm-6">
                        <input type="text" class="form-control" id="txLot" placeholder="Lot" tabindex="1" readonly>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4" for="txQuantity">Quantity</label>
                    <div class="col-sm-6">
                        <input type="text" class="form-control" id="txQuantity" placeholder="Quantity"
                            data-method="ValidarQuantity" tabindex="1" readonly>
                    </div>
                    <label id="lblUnis" for="txQuantity">
                    </label>
                </div>
                <div class="form-group row">
                    <input type="button" class="btn btn-primary btn-lg" id="btnEnviar" value="Confirm" disabled />&nbsp
                </div>
            </div>
        </div>
        <label id="lblError" class="col-sm-12">
        </label>
    </form>
    <!-- Referencias de estilo-->
    <script>


        $(function () {
            function sendAjax(WebMethod, Data, FuncitionSucces) {
                var options = {
                    type: "POST",
                    url: WebMethod,
                    data: Data,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    dataType: "json",
                    success: FuncitionSucces
                };
                $.ajax(options);
            }
            let txPaid = document.getElementById("txPaid");
            let txRfid = document.getElementById("txRfid");
            let txItem = document.getElementById("txItem");
            let lblUnidDsca = document.getElementById("lblUnidDsca");
            let lblUnidSt = document.getElementById("lblUnidSt");
            let txLot = document.getElementById("txLot");
            let txQuantity = document.getElementById("txQuantity");
            let lblUnis = document.getElementById("lblUnis");
            let btnEnviar = document.getElementById("btnEnviar");
            let lblError = document.getElementById("lblError");

            let sendValidarPalletID = function () {
                sendAjax("RfidPop.aspx/ValidarPalletID", "{'PAID':'" + txPaid.value.toUpperCase().trim() + "'}", ValidarPalletIDSucces);
            }
            let sendValidarRfis = function () {
                sendAjax("RfidPop.aspx/ValidarRfis", "{'RFID':'" + txRfid.value.toUpperCase().trim() + "'}", ValidarRfisSucces);
            }
            let Save = function () {
                stoper();
                timer = sendAjax("RfidPop.aspx/Save", '{}', SaveSucces);
            };
            let ValidarPalletID = function () {
                stoper();
                timer = setTimeout(function () { sendValidarPalletID() }, 2000);
            };
            let ValidarRfis = function () {
                stoper();
                timer = setTimeout(function () { sendValidarRfis() }, 2000);
            };

            let SaveSucces = function (r) {
                if (r.d == true) {
                    alert("Process success");
                    CleanForm();
                }
                else {
                    alert("Process fail");
                }
            };
            let ValidarPalletIDSucces = function (r) {
                let myList = JSON.parse(r.d)
                if (myList.length > 0) {
                    if (myList[0]["T$FIRE"].toString().trim() == '2') {

                        if (myList[0]["T$RFID"].toString().trim() == "1") {
                            txItem.value = myList[0]["T$ITEM"].toString().trim();
                            txLot.value = myList[0]["T$CLOT"].toString();
                            txQuantity.value = myList[0]["T$QTYS"].toString();
                            lblUnis.innerHTML = myList[0]["T$UNIT"].toString();
                            txRfid.removeAttribute("disabled");
                        }
                        else {
                            txRfid.value = "";
                            txItem.value = "";
                            lblUnidDsca.innerHTML = "";
                            lblUnidSt.innerHTML = "";
                            txLot.value = "";
                            txQuantity.value = "";
                            lblUnis.innerHTML = "";
                            txRfid.setAttribute('disabled', 'disabled');
                            btnEnviar.setAttribute('disabled', 'disabled');
                            lblError.innerHTML = "item not controlled with Rfid";
                        }
                    }
                    else{
                        lblError.innerHTML = "Pallet already receipt";
                    }
                }
                else {
                    txRfid.value = "";
                    txItem.value = "";
                    lblUnidDsca.innerHTML = "";
                    lblUnidSt.innerHTML = "";
                    txLot.value = "";
                    txQuantity.value = "";
                    lblUnis.innerHTML = "";
                    txRfid.setAttribute('disabled', 'disabled');
                    btnEnviar.setAttribute('disabled', 'disabled');
                    lblError.innerHTML = "Pallet don't exist";
                }

            };
            let ValidarRfisSucces = function (r) {
                if (r.d.trim() != "") {
                    btnEnviar.setAttribute("disabled", 'disabled');
                    lblError.innerHTML = r.d;
                }
                else {
                    btnEnviar.removeAttribute("disabled");
                    lblError.innerHTML = "";
                }

            };
            let CleanForm = function () {
                txPaid.value = "";
                txRfid.value = "";
                txItem.value = "";
                lblUnidDsca.innerHTML = "";
                lblUnidSt.innerHTML = "";
                txLot.value = "";
                txQuantity.value = "";
                lblUnis.innerHTML = "";
                txRfid.setAttribute('disabled', 'disabled');
                btnEnviar.setAttribute('disabled', 'disabled');
                lblError.innerHTML = "";
            };

            txPaid.addEventListener('input', ValidarPalletID, false);
            txRfid.addEventListener('input', ValidarRfis, false);
            btnEnviar.addEventListener('click', Save, false);
        });


    </script>
    <script src="styles/popper.min.js"></script>
    <script src="styles/bootstrap.min.js"></script>
    <script src="styles/jquery-3.1.1.min.js"></script>
</asp:Content>
