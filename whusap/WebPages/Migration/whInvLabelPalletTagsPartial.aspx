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

        function fun_AllowOnlyAmountAndDot(txt) {
            if (event.keyCode > 47 && event.keyCode < 58) {
                return true;
            }
            else {
                event.keyCode = 0;
                alert("Only Numbers allowed.");
                return false;
            }
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label Text="" runat="server" ID="lblInfo" Style="color: Black; font-size: medium;" />
            <table border="0">
                <tr>
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
                <tr runat="server">
                    <td>
                        <asp:Label runat="server" ID="lblMsgMcnoActive"></asp:Label>
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
                            <asp:TextBox runat="server" ID="txtQuantity" onkeypress="return fun_AllowOnlyAmountAndDot(this);" onblur="validarCantidad(this)" CssClass="TextBoxBig" ClientIDMode="Static" />
                        </span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Label Text="" runat="server" ID="lblError" Style="color: red; font-size: 15px; font-weight: bold;" ClientIDMode="Static" />
                        <asp:Label Text="" runat="server" ID="lblConfirm" Style="color: green; font-size: 15px; font-weight: bold;" ClientIDMode="Static" />
                        <hr />
                        <asp:Button Text="" runat="server" ID="btnConsultar" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait 20 seconds... '; " OnClick="btnConsultar_Click" CssClass="ButtonsSendSave" Style="height: 30px;" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <hr />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container">
                <iframe id="myLabelFrame" scrolling="no" title="Inline Frame Example" class="col-12" style="height: 450px; overflow: hidden; margin-bottom: 100px;" frameborder="0" src=""></iframe>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div runat="server" id="divTable" clientidmode="Static" visible="false">
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
    </div>
    <table runat="server" id="divBotones" visible="false" style="margin-bottom: 10px; width: 5.8in; text-align: center; font-weight: bold;" border="1" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="2"><a href="#" onclick="javascript:printDiv('divTable')" runat="server" id="linkPrint"></a></td>
            <td runat="server" id="tdBtnExit">
                <asp:Button class="buttonMenu" runat="server" ID="btnSalir" OnClick="btnExit_Click" /></td>
        </tr>
    </table>
</asp:Content>
