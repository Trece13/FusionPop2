﻿<%@ Page Title="" Language="C#" MasterPageFile="../../MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="whInvMrbRejection.aspx.cs" Inherits="whusap.WebPages.Migration.whInvMrbRejection"
    EnableViewStateMac="false" Theme="Cuadriculas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="styles/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="styles/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="styles/font-awesome.min.css">
    <script type="text/javascript">
        var idioma = '<%= _idioma %>';
        //         function validaPaid(args) {
        //              var parametrosEnviar = "{ 'palletID': '" + args.value + "'}";
        //           
        //             $.ajax({
        //                 type: "POST",
        //                 url: "whInvMrbRejection.aspx/vallidatePallet",
        //                 data: parametrosEnviar,
        //                 contentType: "application/json; charset=utf-8",
        //                 dataType: "json",
        //                 success: function (msg) {
        //                     if (msg.d != "") {
        //                         var objControl = document.getElementById(args.id);
        //                         objControl.value = "";
        //                         alert(msg.d);
        //                         return false;
        //                     } else { 
        //                     }
        //                 },
        //                 error: function (msg) {
        //                     alert("This is msg " + msg.d);
        //                     return false;
        //                 }
        //             });
        //         }

        function printDivAnnounce(divID) {

            var monthNames = [
               "1", "2", "3",
               "4", "5", "6", "7",
               "8", "9", "10",
               "11", "12"
            ];

            //PRINT LOCAL HOUR
            var d = new Date();
            var LbdDate = $("#Contenido_lblFechaDate");
            LbdDate.text(

               monthNames[d.getMonth()] +
               "/" +
               d.getDate() +
               "/" +
               d.getFullYear() +
               " - " +
               addZero(d.getHours()) +
               ":" +
               addZero(d.getMinutes()) +
               ":" +
               addZero(d.getSeconds())

               );
            //Get the HTML of div
            var divElements = document.getElementById(divID).innerHTML;
            //Get the HTML of whole page
            var oldPage = document.body.innerHTML;
            //Reset the page's HTML with div's HTML only
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            //Print Page
            window.print();
            //Restore orignal HTML
            document.body.innerHTML = oldPage;
        };
        function printDivLocate(divID) {
            //PRINT LOCAL HOUR

            var monthNames = [
               "1", "2", "3",
               "4", "5", "6", "7",
               "8", "9", "10",
               "11", "12"
            ];

            //PRINT LOCAL HOUR
            var d = new Date();
            var LbdDate = $("#lblDate");
            LbdDate.html(
               monthNames[d.getMonth()] +
               "/" +
               d.getDate() +
               "/" +
               d.getFullYear() +
               " " +
               d.getHours() +
               ":" +
               d.getMinutes() +
               ":" +
               d.getSeconds()
               );

            //            var x = document.getElementById("lblDate");
            //            var h = addZero(d.getHours());
            //            var m = addZero(d.getMinutes());
            //            var s = addZero(d.getSeconds());
            //            //x.innerHTML = d.toUTCString();
            //            x.innerHTML = d.toLocaleString();

            //Get the HTML of div
            var divElements = document.getElementById(divID).innerHTML;
            //Get the HTML of whole page
            var oldPage = document.body.innerHTML;
            //Reset the page's HTML with div's HTML only
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            //Print Page
            window.print();
            //Restore orignal HTML
            document.body.innerHTML = oldPage;
        }
        function addZero(i) {
            if (i < 10) {
                i = "0" + i;

            }
            return i;
        };

        function printDivDelivered(divID) {

            var monthNames = [
               "1", "2", "3",
               "4", "5", "6", "7",
               "8", "9", "10",
               "11", "12"
            ];

            //PRINT LOCAL HOUR
            var d = new Date();
            var LbdDate = $("#Contenido_lblFecha");
            LbdDate.text(

               monthNames[d.getMonth()] +
               "/" +
               d.getDate() +
               "/" +
               d.getFullYear() +
               " - " +
               addZero(d.getHours()) +
               ":" +
               addZero(d.getMinutes()) +
               ":" +
               addZero(d.getSeconds())

               );
            //Get the HTML of div
            var divElements = document.getElementById(divID).innerHTML;
            //Get the HTML of whole page
            var oldPage = document.body.innerHTML;
            //Reset the page's HTML with div's HTML only
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            //Print Page
            window.print();
            //Restore orignal HTML
            document.body.innerHTML = oldPage;
        };
        function validarFormularioAnnounce() {
            debugger
            var validate = true;
            var mensaje = "";
            var dataSave = false;

            for (var i = 0; i < numeroRegistros; i++) {

                var txtShift = $('#txtShift-' + i).val().toUpperCase();
                var txtComments = $('#txtExactReason-' + i).val();

                if (txtShift.trim() != "") {
                    if (txtShift == 'A' || txtShift == 'B' || txtShift == 'C' || txtShift == 'D') {
                    } else {
                        validate = false;
                        mensaje += idioma == "INGLES" ? "-A valid value to field Shift - Row " + (i + 1) + "\n" : "-Un valor valido para el campo Cambio - Fila " + (i + 1) + "\n";
                    }

                    if (txtComments.trim() == "") {
                        validate = false;
                        mensaje += idioma == "INGLES" ? "-Exact reason - Row " + (i + 1) + "\n" : "-Razón exacta- Fila" + (i + 1) + "\n";
                    }
                } else {
                    if (txtComments.trim() != "") {
                        if (txtShift.trim() == "") {
                            validate = false;
                            mensaje += idioma == "INGLES" ? "-Shift- Row " + (i + 1) + "\n" : "-Cambio - Fila " + (i + 1) + "\n";
                        } else {
                            if (txtShift == 'A' || txtShift == 'B' || txtShift == 'C' || txtShift == 'D') {
                            } else {
                                validate = false;
                                mensaje += idioma == "INGLES" ? "-A valid value to field Shift - Row " + (i + 1) + "\n" : "-Un valor valido para el campo Cambio - Fila " + (i + 1) + "\n";
                            }
                        }
                    }
                }

                if (txtShift.trim() != "" && txtComments.trim() != "") {
                    dataSave = true;
                }
            }

            debugger;
            if (!validate) {
                alert((idioma == "INGLES" ? "Please, enter this fields:\n" : "Por favor ingrese los siguientes campos:\n") + mensaje);
            } else if (!dataSave) {
                validate = false;
                alert(idioma == "INGLES" ? "Please, enter information to save" : "Por favor ingrese información para guardar");
            }

            return validate;

        }
        function validarShift(field) {
            var valor = field.value.toUpperCase();
            if (valor == 'A' || valor == 'B' || valor == 'C' || valor == 'D') {
            }
            else {
                alert(idioma == "INGLES" ? "Values must be A, B C or D" : "El valor debe ser A, B, C o D");
                this.focus();
                field.value = '';
            }
        }
        function validarCantidadQty(field) {
            var cantidad = field.value;
            var quant = $('#hdfQuantity').val();
            var unidad = document.getElementById("txtUnit");
            unidad = unidad.value;
            if (unidad != "KG" && unidad != "LB") {
                var regex = /^-?\d*[0-9]*$/;
            }
            else {
                var regex = /^-?\d*[0-9]*[.]?[0-9]*$/;
            }
            var re = new RegExp(regex);
            if (field.value.match(re)) {
                if ((cantidad < 0)) {
                    alert(idioma == "INGLES" ? 'Quantity can not be minor or equals zero' : 'Cantidad no puede ser menor o igual a cero');
                    this.focus();
                    field.value = 0;
                } else {
                    if (parseFloat(cantidad) > parseFloat(quant)) {
                        alert(idioma == "INGLES" ? 'Quantity higher than Quantities delivered to WO' : 'Cantidad ingresada es mas grande que la cantidad disponible para la orden');
                        this.focus();
                        field.value = 0;
                    }
                }
            }
            else {
                this.focus();
                field.value = 0;
                alert(idioma == "INGLES" ? "Only numbers here, no decimals allowed" : "Solo número en este campo, no se permiten decimales");
            }
        };
        function validarFormularioDelivered() {
            debugger
            var validate = true;
            var mensaje = "";

            var slItem = $('#slItems').val();
            var txtQty = $('#txtQty').val();
            var slLot = $('#slLot').val();
            var txtShift = $('#txtShift').val();
            var txtComments = $('#txtExactReasons').val();

            if (slItem.trim() == "") {
                validate = false;
                mensaje += idioma == "INGLES" ? "-Item\n" : "-Articulo\n";
            }

            if (txtQty.trim() == "") {
                validate = false;
                mensaje += idioma == "INGLES" ? "-Quantity\n" : "-Cantidad\n";
            }

            if (slLot == null) {
                validate = false;
                mensaje += idioma == "INGLES" ? "-Lot\n" : "-Lote\n";
            }

            if (txtShift.trim() == "") {
                validate = false;
                mensaje += idioma == "INGLES" ? "-Shift\n" : "-Cambio\n";
            }

            if (txtComments.trim() == "") {
                validate = false;
                mensaje += idioma == "INGLES" ? "-Exact Reason\n" : "-Razón exacta\n";
            }

            debugger;
            if (!validate) {
                alert((idioma == "INGLES" ? "Please, enter this fields:\n" : "Por favor ingrese los siguientes campos:\n") + mensaje);
            }

            return validate;
        };
        //JC 02122021 Ajustar la validacion con la cantidad del pallet.
        //function validarCantidad(field, stk, CantidadDevuelta) {
        function validarCantidad(field, stk, stockpallet) {
            //var cantidad = parseInt(field.value);  //JC 060921 Evitar pasarlos valores con decimales a entero
            var cantidad = parseFloat(field.value);
            var stock = stk;
            var unidad = document.getElementById("unit");
            unidad = unidad.textContent;
            if (unidad != "KG" && unidad != "LB") {
                var regex = /^-?\d*[0-9]*$/;
            }
            else {
                var regex = /^-?\d*[0-9]*[.]?[0-9]*$/;
            }
            var re = new RegExp(regex);
            if (field.value.match(re)) {
                //JC 02122021 Ajustar la validacion con la cantidad del pallet.
                //if (((cantidad + CantidadDevuelta) > stock)) {
                if (cantidad > stock) {
                    alert(idioma == "INGLES" ? 'Quantity is greater that stock by item, warehouse and lot, only have ' + (stock) : 'Cantidad mayor a la cantidad existente por item, almacen y lote, solo hay ' + (stock));
                    this.focus();
                    field.value = 0;
                }
                //JC 02122021 Ajustar la validacion con la cantidad del pallet.
                //if (cantidad > CantidadDevuelta) {
                if (cantidad > stockpallet) {
                    //alert(idioma == "INGLES" ? 'Quantity is greater that quantity by pallet, only have ' + (CantidadDevuelta) : 'Cantidad mayor a la cantidad del pallet, solo hay ' + (CantidadDevuelta));
                    alert(idioma == "INGLES" ? 'Quantity is greater that quantity by pallet, only have ' + (stockpallet) : 'Cantidad mayor a la cantidad del pallet, solo hay ' + (stockpallet));
                    this.focus();
                    field.value = 0;
                }
            }
            else {
                this.focus();
                field.value = 0;
                alert(idioma == "INGLES" ? "Only numbers here, no decimals allowed" : "Solo número en este campo, no se permiten decimales");
            }
        };
        function setReason(field) {
            //var selectedText = slReasons.options[slReasons.selectedIndex].innerHTML;
            //document.getElementById("slReasons").value = selectedText;
            var as = document.getElementById("slReasons").value;
            var e = document.getElementById("slReasons");
            var strreason = e.options[e.selectedIndex].text;
            document.getElementById("lblReasonDesc").value = strreason;
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True" EnablePageMethods="true">
    </asp:ScriptManager>
    <br />
    <table border="0">
        <tr>
            <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblMrbWarehouse" Text="MRB Warehouse" runat="server" /></strong>
            </td>
            <td style="width: 250px; padding: 5px;">
                <asp:DropDownList runat="server" ID="dropDownWarehouse" CssClass="TextBoxBig"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    ControlToValidate="dropDownWarehouse"
                    Display="Static"
                    ForeColor="red"
                    Font-Names="arial"
                    Font-Size="12pt"
                    ErrorMessage="Please choose MRB Warehouse"
                    runat="server" />

            </td>
            <td style="text-align: left;">
                <span></span>
                <asp:RequiredFieldValidator ID="RequiredField" runat="server" ErrorMessage="MRB Warrhouse is required"
                    ControlToValidate="dropDownWarehouse" SetFocusOnError="False" CssClass="errorMsg"
                    Enabled="false"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="OrderError" runat="server" ErrorMessage="Order doesn't exist or don´t have items"
                    SetFocusOnError="True" CssClass="errorMsg"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblPalletId" Text="Pallet ID" runat="server" /></strong>
            </td>
            <td style="width: 250px; padding: 5px;">
                <asp:TextBox ID="txtPalletId" class="form-control form-control-lg" runat="server"
                    CausesValidation="True" MaxLength="20" CssClass="TextBoxBig" TabIndex="1" ToolTip="Enter pallet Id"></asp:TextBox>
            </td>
            <td style="text-align: left;">
                <span>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPalletId"
                        ErrorMessage="Pallet ID can Have More than 12 But less than 21 Characters." ValidationExpression="^[a-zA-Z0-9'@&#-.\/\s]{12,20}$"
                        CssClass="errorMsg" SetFocusOnError="True" Display="Dynamic" />
                </span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Pallet Id is required"
                    ControlToValidate="txtPalletId" SetFocusOnError="False" CssClass="errorMsg"
                    Enabled="false"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Pallet Id doesn't exist"
                    SetFocusOnError="True" CssClass="errorMsg"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblWOrder" Text="Work Order" runat="server" /></strong>
            </td>
            <td style="width: 250px; padding: 5px;">
                <asp:TextBox ID="TxtOrder" class="form-control form-control-lg" runat="server"
                    CausesValidation="True" MaxLength="20" CssClass="TextBoxBig" TabIndex="1"
                    ToolTip="Enter Work Order" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <hr />
                <asp:Button ID="btnSend" runat="server" Text="Query" OnClick="btnSend_Click" CssClass="ButtonsSendSave" />
                <asp:Label Text="" runat="server" ID="lblError" Style="color: red; font-size: 15px; font-weight: bold;" ClientIDMode="Static" />
                <asp:Label Text="" runat="server" ID="lblErrorAnnounce" Style="color: red; font-size: 15px; font-weight: bold;" ClientIDMode="Static" />
                <asp:Label Text="" runat="server" ID="lblConfirmAnnounce" Style="color: green; font-size: 15px; font-weight: bold;" ClientIDMode="Static" />
                <asp:Label Text="" runat="server" ID="lblErrorLocated" Style="color: red; font-size: 15px; font-weight: bold;" ClientIDMode="Static" />
                <asp:Label Text="" runat="server" ID="lblConfirmLocated" Style="color: green; font-size: 15px; font-weight: bold;" ClientIDMode="Static" />
                <asp:Label Text="" runat="server" ID="lblErrorDelivered" Style="color: red; font-size: 15px; font-weight: bold;" ClientIDMode="Static" />
                <asp:Label Text="" runat="server" ID="lblConfirmDelivered" Style="color: green; font-size: 15px; font-weight: bold;" ClientIDMode="Static" />
            </td>
        </tr>
    </table>


    <%-- Logic for Announced status --%>
    <div runat="server" id="divBtnGuardarAnnouce" visible="false">
        <hr />
        <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click_announce" OnClientClick="return validarFormularioAnnounce();" CssClass="ButtonsSendSave" />
    </div>
    <div runat="server" id="divTableAnnounce" clientidmode="Static"></div>
    <table runat="server" id="divBotonesAnnounce" style="margin-bottom: 10px; text-align: center; font-weight: bold;" cellspacing="0" cellpadding="0" visible="false">
        <tr>
            <td>
                <button class="btn btn-primary btn-lg" type="button" onclick="javascript:printDivAnnounce('divLabelAnnounce')">Print</button></td>
            <td>
                <asp:Button class="btn btn-primary btn-lg" runat="server" ID="btnSalirAnnounce" OnClick="btnExit_Click" Style="margin-left: 5px;"
                    AutoPostBack="true" /></td>
        </tr>
    </table>
    <div runat="server" id="divLabelAnnounce" clientidmode="Static" visible="false">

        <table style="font-size: small; font-weight: bold; text-align: center; width: 5.8in; height: 3.8in" border="1" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblDmtNumber"></asp:Label></td>
                <td>
                    <asp:Label runat="server" ID="LblSqnb"></asp:Label></td>
                <td>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="imgCBSqnb" alt=""
                        hspace="60" vspace="5" style="width: 2in; height: .5in; margin: 0px;" /></td>
            </tr>
            <tr>
                <td colspan="3">
                    <img src="~/images/logophoenix_login.jpg" runat="server"
                        id="imgCBMitm" alt="" hspace="60" vspace="5"
                        style="width: 4in; height: .5in; margin: 0px;" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblDescription"></asp:Label></td>
                <td colspan="2">
                    <asp:Label runat="server" ID="lblDsca"></asp:Label></td>
            </tr>
            <tr>
                <td style="border-bottom: 0px;">
                    <asp:Label runat="server" ID="lblTitleMachine" />-<asp:Label
                        runat="server" ID="lblDescMachine" /></td>
                <td>
                    <asp:Label runat="server" ID="lblRejected"></asp:Label>
                    -
                    <asp:Label ID="lblValueQuantity" runat="server"></asp:Label>
                    &nbsp;<asp:Label ID="lblQtdl" runat="server"></asp:Label>
                </td>
                <td>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="imgBCQtdl" alt=""
                        hspace="60" vspace="5" style="width: 2in; height: .5in; margin: 0px;" /></td>
            </tr>
            <tr>
                <td style="border-top: 0px;">
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="Label1" />
                    -
                    <asp:Label ID="lblPdno" runat="server" />
                </td>
                <td>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="imgBCPdno" alt=""
                        hspace="60" vspace="5" style="width: 2in; height: .5in; margin: 0px;" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPrintedBy" runat="server" />:
                    <asp:Label ID="lblValuePrintedBy" runat="server" />
                </td>
                <td colspan="2">
                    <asp:Label ID="lblFecha" runat="server" />&nbsp
                    <asp:Label ID="lblFechaDate" runat="server" />&nbsp
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblReason" runat="server" /></td>
                <td colspan="2">
                    <asp:Label ID="lblValueReason" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="3" rowspan="2">
                    <asp:Label ID="lblComments" runat="server" />: 
                    <asp:Label ID="lblValueComments" runat="server" />
                </td>
            </tr>
        </table>
        <hr />
    </div>

    <%-- end Logic for Announced status --%>

    <%-- Logic for located status --%>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server" id="divBtnGuardarLocated" visible="false">
                <hr />
                <asp:Button ID="btnGuardarLocated" runat="server" OnClick="btnGuardar_Click_located" 
                    CssClass="ButtonsSendSave" />
                <input type="text" id="txSloc" runat="server" style="display: none" />
            </div>
            <div runat="server" id="divTableLocated" visible="false">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <table runat="server" id="divBotonesLocated" style="margin-bottom: 10px; text-align: center; font-weight: bold;"
        cellspacing="0" cellpadding="0" visible="false">
        <tr>
            <td>
                <button class="btn btn-primary btn-lg" type="button" onclick="javascript:printDivLocate('divLabel')">
                    Print</button>
            </td>
            <td>
                <asp:Button class="btn btn-primary btn-lg" runat="server" ID="btnSalirLocate" OnClick="btnExit_Click" Style="margin-left: 5px;"
                    AutoPostBack="true" />
            </td>
        </tr>
    </table>
    <div runat="server" id="divLabel" clientidmode="Static" style="zoom: 80%; margin-bottom: 70px"
        visible="false">
        <hr />
        <table style="width: 5.8in; height: 3.8in; text-align: center; font-weight: bold;"
            border="1" cellspacing="0" cellpadding="0">
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblDefectiveMaterial" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="imgPalletId" alt=""
                        hspace="60" vspace="5" style="width: 3in; height: .5in;" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="imgCodeItem" alt=""
                        hspace="60" vspace="5" style="width: 3in; height: .5in;" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblDescDescription"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:Label runat="server" ID="lblValueDescription"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Dispositon - Review
                </td>
                <td>
                    <asp:Label ID="lblDescRejectQty" runat="server" />
                </td>
                <td>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="imgQty" alt="" hspace="60"
                        vspace="5" style="width: 1in; height: .3in;" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblDescPrintedBy"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:Label runat="server" ID="lblDate" ClientIDMode="static"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblValueLot"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lblDescReason"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lblValueReasonLocated"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label runat="server" ID="lblCommentsLocated"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <input type="text" id="lblReasonDesc" name="lblReasonDesc" style='display: none;' />
    <asp:HiddenField runat="server" ID="hdfDescItem" />


    <%-- end Logic for located status --%>

    <%-- Logic for Delivered status --%>
    <div runat="server" id="divBtnGuardarDelivered" visible="false">
        <hr />
        <asp:Button ID="btnGuardarDelivered" runat="server" OnClick="btnGuardar_Click_Delivered" OnClientClick="return validarFormularioDelivered();" CssClass="ButtonsSendSave" />
    </div>
    <div runat="server" id="divTableDelivered" style="overflow-y: scroll; width: 150%;" visible="false">
        <hr />
        <table class='table table-bordered' style='max-width: 1200px; font-size: 13px; border: 3px solid; border-style: outset; text-align: center;'>
            <tr style='font-weight: bold; background-color: lightgray;'>
                <td>
                    <asp:Label ID="lblOrder" runat="server" /></td>
                <td colspan="8">
                    <asp:Label ID="lblValueOrder" runat="server" /></td>
            </tr>
            <tr style='font-weight: bold; background-color: white;'>
                <td>
                    <asp:Label ID="lblItem" runat="server" /></td>
                <td>
                    <asp:Label ID="lblDescriptionDelivered" runat="server" /></td>
                <td>
                    <asp:Label ID="lblQty" runat="server" /></td>
                <td>
                    <asp:Label ID="lblUnit" runat="server" /></td>
                <td>
                    <asp:Label ID="lblLot" runat="server" /></td>
                <td>
                    <asp:Label ID="lblShift" runat="server" /></td>
                <td>
                    <asp:Label ID="lblReasonDelivered" runat="server" /></td>
                <td>
                    <asp:Label ID="lblRejectedType" runat="server" /></td>
                <td>
                    <asp:Label ID="lblExactReason" runat="server" /></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox runat="server" ID="slItems" CssClass="TextBox" Style="width: 150px;" ClientIDMode="Static" ReadOnly="true" /></td>
                <td>
                    <asp:TextBox runat="server" ID="txtDescription" CssClass="TextBox" Style="width: 150px;" ClientIDMode="Static" ReadOnly="true" /></td>
                <td>
                    <asp:TextBox runat="server" TextMode="Number" step="any" ID="txtQty" CssClass="TextBox" Style="width: 40px;" ClientIDMode="Static" onblur="validarCantidadQty(this);" /></td>
                <td>
                    <asp:TextBox runat="server" ID="txtUnit" CssClass="TextBox" Style="width: 40px;" ClientIDMode="Static" ReadOnly="true" /></td>
                <td>
                    <asp:TextBox runat="server" ID="slLot" CssClass="TextBox" Style="width: 100px;" ClientIDMode="Static" ReadOnly="true" />
                    <asp:HiddenField runat="server" ID="txtkltc" ClientIDMode="Static" />
                    <asp:HiddenField runat="server" ID="txtpono" ClientIDMode="Static" />
                </td>

                <td>
                    <asp:TextBox runat="server" ID="txtShift" CssClass="TextBox" Style="width: 25px;" ClientIDMode="Static" MaxLength="1" onblur="validarShift(this);" /></td>
                <td>
                    <asp:DropDownList runat="server" ID="slReason" CssClass="TextBox" Style="width: 100px;" ClientIDMode="Static"></asp:DropDownList></td>
                <td>
                    <asp:DropDownList runat="server" ID="slRejectType" CssClass="TextBox" Style="width: 100px;" ClientIDMode="Static"></asp:DropDownList></td>
                <td>
                    <textarea rows="3" runat="server" id="txtExactReasons" cssclass="TextBox" style="width: 150px;" clientidmode="Static"></textarea></td>
            </tr>
        </table>

    </div>
    <table runat="server" id="divBotonesDelivered" style="margin-bottom: 10px; text-align: center; font-weight: bold;" cellspacing="0" cellpadding="0" visible="false">
        <tr>
            <td>
                <button class="btn btn-primary btn-lg" type="button" onclick="javascript:printDivDelivered('divLabelDelivered')">Print</button></td>
            <td>
                <asp:Button class="btn btn-primary btn-lg" runat="server" ID="btnSalirDelivered" OnClick="btnExit_Click" Style="margin-left: 5px;"
                    AutoPostBack="true" /></td>
        </tr>
    </table>
    <div runat="server" id="divLabelDelivered" clientidmode="Static" visible="false">

        <table style="font-size: small; font-weight: bold; text-align: center; width: 5.8in; height: 3.8in" border="1" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblDmtNumberDelivered"></asp:Label></td>
                <td>
                    <asp:Label runat="server" ID="LblSqnbDElivered"></asp:Label></td>
                <td>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="imgCBSqnbDelivered" alt=""
                        hspace="60" vspace="5" style="width: 2in; height: .5in; margin: 0px;" /></td>
            </tr>
            <tr>
                <td colspan="3">
                    <img src="~/images/logophoenix_login.jpg" runat="server"
                        id="imgCBMitmDelivered" alt="" hspace="60" vspace="5"
                        style="width: 4in; height: .5in; margin: 0px;" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="Label1Delivered"></asp:Label></td>
                <td colspan="2">
                    <asp:Label runat="server" ID="lblDscaDelivered"></asp:Label></td>
            </tr>
            <tr>
                <td style="border-bottom: 0px;">
                    <asp:Label Text="" runat="server" ID="lblDescLot" />
                    <br />
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="imgBCClot" alt=""
                        hspace="60" vspace="5" style="width: 2in; height: .5in; margin: 0px;" /></td>
                <td>
                    <asp:Label runat="server" ID="lblRejectedDelivered"></asp:Label>
                    &nbsp;<asp:Label ID="lblValueQuantityDelivered" runat="server"></asp:Label>
                    &nbsp;<asp:Label ID="lblQtdlDelivered" runat="server"></asp:Label>
                </td>
                <td>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="imgBCQtdlDelivered" alt=""
                        hspace="60" vspace="5" style="width: 2in; height: .5in; margin: 0px;" /></td>
            </tr>
            <tr>
                <td style="border-top: 0px;">
                    <asp:Label ID="Label10Delivered" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="Label11Delivered" />
                    -
                    <asp:Label ID="lblPdnoDelivered" runat="server" />
                </td>
                <td>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="imgBCPdnoDelivered" alt=""
                        hspace="60" vspace="5" style="width: 2in; height: .5in; margin: 0px;" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPrintedByDelivered" runat="server" />:
                    <asp:Label ID="lblValuePrintedByDelivered" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblFechaDelivered" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblMachinetitle" runat="server" />:
                    <asp:Label ID="lblMachine" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3Delivered" runat="server" /></td>
                <td colspan="2">
                    <asp:Label ID="lblValueReasonDelivered" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="3" rowspan="2">
                    <asp:Label ID="lblCommentsDelivered" runat="server" />: 
                    <asp:Label ID="lblValueCommentsDelivered" runat="server" />
                </td>
            </tr>
        </table>
        <hr />
    </div>
    <asp:HiddenField runat="server" ID="hdfQuantity" ClientIDMode="Static" />
    <%-- end Logic for Delivered status --%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container">
                <iframe id="myLabelFrame" scrolling="no" title="Inline Frame Example" class="col-12" style="height: 1000px; overflow: hidden; margin-bottom: 100px;" frameborder="0" src=""></iframe>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
