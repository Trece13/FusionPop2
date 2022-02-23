<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvConfirmReceiptWm.aspx.cs" Inherits="whusap.WebPages.InvLogistica.whInvConfirmReceiptWm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style>
        #tblinfo {
            display: none;
        }
    </style>
    <br>
    <form>
        <div class="form-group row">
            <label for="inputEmail3" class="col-sm-1 col-form-label">Pallet ID</label>
            <div class="col-sm-4">
                <input type="text" class="form-control" id="txtNumeroOrden" placeholder="XXX000000000-XXX">
            </div>
            <div class="col-sm-2">
                <input type="button" value="Query" class="btn btn-primary btn" id="btnConsultar" />
            </div>
        </div>
    </form>
    <br />
    <div class="row">
        <label id="lblError" style="color:red"></label>
    </div>
    <div class="row" id="tblinfo">
        <hr>
        <table class="table table-sm table-reflow">
            <thead style="background-color: #009bff; color: white; font-weight: bold;">
                <tr>
                    <th>Production Order:</th>
                    <th>
                        <label id="lblValueOrden"></label>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr style="background-color: lightgray;">
                    <td>Item:</td>
                    <td>
                        <label id="lblValueArticulo"></label>
                    </td>
                </tr>
                <tr>
                    <td>Warehouse:</td>
                    <td>
                        <label id="lblValueWareHouse"></label>
                    </td>
                </tr>
                <tr>
                    <td>Total:</td>
                    <td>
                        <label id="lblValueTotal"></label>
                    </td>
                </tr>
                <tr>
                    <td>Delivered:</td>
                    <td>
                        <label id="lblValueDelivered"></label>
                    </td>
                </tr>
                <tr>
                    <td>To Receive:</td>
                    <td>
                        <label id="lblValueToReceive"></label>
                    </td>
                </tr>
                <tr>
                    <td>Confirmed:</td>
                    <td>
                        <label id="lblValueConfirmed"></label>
                    </td>
                </tr>
                <tr>
                    <td>Unit:</td>
                    <td>
                        <label id="lblValueUnit"></label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="button" class="btn btn-primary btn-sm col-8" value="Save" id ="btnGuardar"/></td>
                    <td></td>
                </tr>
            </tbody>
        </table>
    </div>
    <script>
        paidValid = false;
        paidStr = "";

        let validOrden = function () {
            var ret = true;
            var regex = /^-?\d*[0-9]*[.]?[0-9]*$/;
            var re = new RegExp(regex);
            var numeroOrden = document.getElementById('txtNumeroOrden').value.trim();
            var orden = numeroOrden.substr(9, 1);
            var guion = numeroOrden.substr(9, 1);
            var pallet = numeroOrden.substr(10, 3);
            var idioma = '<%= _idioma %>';
            if (pallet.match(re)) {
                if (numeroOrden != "") {
                    if (numeroOrden.length < 13 || numeroOrden.length > 13) {
                        alert(idioma == "INGLES" ? "Please use this format WORKORDER-PALLETID, remember only 13 characters"
                                    : "Por favor use el formato ORDENTRABAJO-PALLETID, solo 13 caracteres");
                        document.getElementById("txtNumeroOrden").focus();
                        document.getElementById("txtNumeroOrden").value = "";
                        ret = false;
                    }
                    else {
                        if (guion != "-") {
                            alert(idioma == "INGLES" ? "Please use this format WORKORDER-PALLETID, remember 9 characters workorder, simbol minus, 3 characters pallet id"
                                            : "Por favor use el formato ORDERNTRABAJO-PALLETID, recuerde 9 caracteres para la orden de trabajo, simbolo negativo, 3 caracteres para el Pallet ID.");
                            document.getElementById("txtNumeroOrden").focus();
                            document.getElementById("txtNumeroOrden").value = "";
                            ret = false;
                        }
                    }
                }
            }
            else {
                document.getElementById("txtNumeroOrden").focus();
                document.getElementById("txtNumeroOrden").value = "";
                alert(idioma == "INGLES" ? "Only numbers allowed on pallet id"
                                : "Solo se permiten números en el Pallet ID");
                ret = false;
            }
            return ret;
        }

        let consultPallet = function () {
            lblError.innerHTML = "";
            let pallet = document.getElementById("txtNumeroOrden").value;
            paidStr = pallet;
            let Data = "{'txtNumeroOrden':'" + pallet.toUpperCase().trim() + "'}";
            
            if (pallet.trim() != "" && validOrden()) {
                sendAjax("ConsultPallet", Data, consultPalletSuccess, true)
            }
        }

        let consultPalletSuccess = function (r) {
            let myObj = JSON.parse(r.d);
            if (myObj.Error) {
                paidValid = false;
                lblError.innerHTML = myObj.Msg
            }
            else {
                lblError.innerHTML = "";
                paidValid = true;
                console.log(myObj);
                lblValueOrden.innerHTML = myObj.ORNO;
                lblValueArticulo.innerHTML = myObj.MITM + " - " + myObj.DSCA;
                lblValueWareHouse.innerHTML = myObj.CWAR + " - " + myObj.DCWAR;
                lblValueTotal.innerHTML = myObj.QRDR;
                lblValueDelivered.innerHTML = myObj.TOTQTYENT;
                lblValueToReceive.innerHTML = myObj.QTYPEND;
                lblValueConfirmed.innerHTML = myObj.QTDL;
                lblValueUnit.innerHTML = myObj.CUNI;

                $('#tblinfo').fadeIn(300);;
            }

        }

        let save = function () {
            let pallet = document.getElementById("");
            let Data = "{}";
            sendAjax("Save", Data, saveSuccess, true)
        }

        let saveSuccess = function (r) {
            let myObj = JSON.parse(r.d);
            if (myObj.Error) {
                lblError.innerHTML = myObj.Msg;
                $('#tblinfo').fadeOut(200);
                clearTable();
            }
            else{
                lblError.innerHTML = "";
                $('#tblinfo').fadeOut(200);
                clearTable();
                document.getElementById('txtNumeroOrden').value = "";
                alert(myObj.Msg);
            }
                

        }

        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "whInvConfirmReceiptWm.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: asyncMode != undefined ? asyncMode : true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }

        let validateinput = function () {
            if (validateinput) {
                if (paidStr.trim() == document.getElementById("txtNumeroOrden").value.trim()) {

                }
                else {
                    $('#tblinfo').fadeOut(200);
                    clearTable();
                }
            }
        }

        let InitElements = function () {
            txtNumeroOrden = document.getElementById("txtNumeroOrden");
            btnConsultar = document.getElementById("btnConsultar");
            btnGuardar = document.getElementById("btnGuardar");
            lblValueOrden = document.getElementById("lblValueOrden");
            lblValueArticulo = document.getElementById("lblValueArticulo");
            lblValueWareHouse = document.getElementById("lblValueWareHouse");
            lblValueTotal = document.getElementById("lblValueTotal");
            lblValueDelivered = document.getElementById("lblValueDelivered");
            lblValueToReceive = document.getElementById("lblValueToReceive");
            lblValueConfirmed = document.getElementById("lblValueConfirmed");
            lblValueUnit = document.getElementById("lblValueUnit");
            lblError = document.getElementById("lblError");

            btnConsultar.addEventListener('click', consultPallet, false);
            btnGuardar.addEventListener('click', save, false);
            txtNumeroOrden.addEventListener('input', validateinput, false);
            //txtNumeroOrden.addEventListener('blur', validarOrden, false);
            //btnGuardar.addEventListener('click', save, false);
        }

        let clearTable = function () {
            lblValueOrden.innerHTML = "";
            lblValueArticulo.innerHTML = "";
            lblValueWareHouse.innerHTML = "";
            lblValueTotal.innerHTML = "";
            lblValueDelivered.innerHTML = "";
            lblValueToReceive.innerHTML = "";
            lblValueConfirmed.innerHTML = "";
            lblValueUnit.innerHTML = "";
        }
        InitElements();
    </script>
</asp:Content>
