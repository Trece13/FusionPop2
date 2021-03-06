﻿<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvLabelPalletTagsPartial.aspx.cs" Inherits="whusap.WebPages.Migration.whInvLabelPalletTagsPartial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        var _idioma = '<%= _idioma %>';

        function validarOrden() {
            if (document.getElementById("txtOrder").value != "") {
                if (document.getElementById("txtOrder").value.length < 9 || document.getElementById("txtOrder").value.length > 9) {
                    alert(_idioma == "INGLES" ? "Please use this format WORKORDER, remember only 9 characters"
                    : "Por favor use el forma de order de trabajo, solo 9 caracteres.");
                    document.getElementById("txtOrder").focus();
                    document.getElementById("txtOrder").value = "";
                    return false;
                }
            }
            else {
                document.getElementById("txtOrder").focus();
                document.getElementById("txtOrder").value = "";
            }
        };

        function validarCantidad(field) {
            debugger;
            var cantidad = field.value;
            var regex = /^-?\d*[0-9]*[,.]?[0-9]*$/;
            var re = new RegExp(regex);
            if (field.value.match(re)) {
            }
            else {
                this.focus();
                field.value = 0;
                alert(_idioma == "INGLES" ? "Only numbers here" : "Solo números");
            }
        }

        function printDiv(divID) {
            debugger;
            //PRINT LOCAL HOUR
            var d = new Date();
            var x = document.getElementById("lblValueDate");
            var h = addZero(d.getHours());
            var m = addZero(d.getMinutes());
            var s = addZero(d.getSeconds());
            //x.innerHTML = d.toUTCString();
            x.innerHTML = d.toLocaleString();

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
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:Label Text="" runat="server" ID="lblInfo" Style="color: Black; font-size: medium;" />
    <table border="0">
        <tr style="display: none">
            <td style="text-align: left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                    <b style="font-size: 11px;">
                        <asp:Label runat="server" ID="lblOrder" /></b></span>
            </td>
            <td style="width: 250px; padding: 5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtOrder" onblur="validarOrden()" CssClass="TextBoxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                    <b style="font-size: 11px;">
                        <asp:Label runat="server" ID="Label1" />Machine:</b></span>
            </td>
            <td style="width: 250px; padding: 5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtMachine" CssClass="TextBoxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                    <b style="font-size: 11px;">
                        <asp:Label runat="server" ID="lblQuantity" /></b></span>
            </td>
            <td style="width: 250px; padding: 5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtQuantity" onblur="validarCantidad(this)" CssClass="TextBoxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <hr />
                <asp:Button Text="" runat="server" ID="btnConsultar" OnClick="btnConsultar_Click" CssClass="ButtonsSendSave" Style="height: 30px;" />
            </td>
        </tr>
    </table>
    <hr />
    <asp:Label Text="" runat="server" ID="lblError" Style="color: red; font-size: 15px; font-weight: bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" Style="color: green; font-size: 15px; font-weight: bold;" ClientIDMode="Static" />
    <div runat="server" id="divTable" visible="false" clientidmode="Static" style = "margin-bottom : 300px">
        <hr />
        <table style="width: 5.8in; height: 3.8in; text-align: center; font-weight: bold;" border="1" cellspacing="0" cellpadding="0">
            <tr>
                <td colspan="2">
                    <asp:Label runat="server" ID="lblDescItem"></asp:Label></td>
                <td>
                    <asp:Label runat="server" ID="lblMadeIn"></asp:Label>
                    <asp:Label runat="server" ID="lblValueMadeIn"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3">
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="imgCodeItem" alt="" hspace="60" vspace="5" style="width: 3in; height: .5in;" /></td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label runat="server" ID="lblItem"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3">
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="imgCodeSqnb" alt="" hspace="60" vspace="5" style="width: 3in; height: .5in;" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblWorkOrder"></asp:Label></td>
                <td>
                    <asp:Label runat="server" ID="lblPalletNumber"></asp:Label></td>
                <td>
                    <asp:Label runat="server" ID="lblInspectorInitial"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblValueWorkOrder"></asp:Label></td>
                <td>
                    <asp:Label runat="server" ID="lblValuePalletNumber"></asp:Label></td>
                <td>
                    <asp:Label runat="server" ID="lblValueInspectorInitial"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblDate"></asp:Label></td>
                <td>
                    <asp:Label runat="server" ID="lblShift"></asp:Label></td>
                <td>
                    <asp:Label runat="server" ID="lblCasePerPallet"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblValueDate" ClientIDMode="Static"></asp:Label></td>
                <td>
                    <asp:Label runat="server" ID="lblValueShift"></asp:Label></td>
                <td>
                    <asp:Label runat="server" ID="lblValueCasePerPallet"></asp:Label></td>
            </tr>
        </table>
        <table runat="server" id="divBotones" visible="false" style="margin-bottom: 10px; width: 5.8in; text-align: center; font-weight: bold;" border="1" cellspacing="0" cellpadding="0">
            <tr>
                <td colspan="2"><a href="#" onclick="javascript:printDiv('divTable')" runat="server" id="linkPrint"></a></td>
                <td runat="server" id="tdBtnExit">
                    <asp:Button class="buttonMenu" runat="server" ID="btnSalir" OnClick="btnExit_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
