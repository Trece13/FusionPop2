<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvCosts.aspx.cs" Inherits="whusap.WebPages.Migration.whInvCosts" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        var _idioma = '<%= _idioma %>';
        var numeroRegistros = <%= LstTable.Count %>;
        function validarCantidad(field, factor, cantm, cantr) {
            debugger;
            var dividendo = field.value;
            var residuo = 0;
            var cantmx = cantm;
            var canrg = cantr;
            var totalcant = parseFloat(dividendo) + parseFloat(cantr)
            var total = cantmx - totalcant
            var regex = /^-?\d*[0-9]*[,.]?[0-9]*$/;
            var re = new RegExp(regex);
            if (field.value.match(re)) {
                residuo = dividendo % factor;
                if (total < 0) {
                    alert(_idioma == "INGLES" ? "Quantity exceed the quanity by shift" : "Cantidad excede la cantidad para cambio");
                    this.focus();
                    field.value = 0;
                }
                if ((dividendo % factor) != 0) {
                    alert(_idioma == "INGLES" ? "Is not an order quantity multiple" : "No es una cantidad multiple de la orden");
                }
            }
            else {
                this.focus();
                field.value = 0;
                alert(_idioma == "INGLES" ? "Only numbers here" : "Solo números.");
            }
        };

        function validarCantidadLimiteArticuloMaquina(field, cant_max, cant_reg,cant_proc) {
            if(parseFloat(cant_max) >= parseFloat(field.value) + parseFloat(cant_reg) + parseFloat(cant_proc)){
                
            }
            else{

                alert(_idioma == "INGLES" ? "Requested Quantity higher than: "+cant_max+" quantities allowed for this material" : "Requested Quantity higher than quantities allowed for this material");
                this.focus();
                field.value = 0;
            }
//            debugger;
//            var dividendo = field.value;
//            var residuo = 0;
//            var cantmx = cantm;
//            var canrg = cantr;
//            var totalcant = parseFloat(dividendo) + parseFloat(cantr)
//            var total = cantmx - totalcant
//            var regex = /^-?\d*[0-9]*[,.]?[0-9]*$/;
//            var re = new RegExp(regex);
//            if (field.value.match(re)) {
//                residuo = dividendo % factor;
//                if (total < 0) {
//                    alert(_idioma == "INGLES" ? "Quantity exceed the quanity by shift" : "Cantidad excede la cantidad para cambio");
//                    this.focus();
//                    field.value = 0;
//                }
//                if ((dividendo % factor) != 0) {
//                    alert(_idioma == "INGLES" ? "Is not an order quantity multiple" : "No es una cantidad multiple de la orden");
//                }
//            }
//            else {
//                this.focus();
//                field.value = 0;
//                alert(_idioma == "INGLES" ? "Only numbers here" : "Solo números.");
//            }
        };

        function validarFormulario() {
            debugger
            var validate = true;
            var dataSave = false;
            var mensaje = "";

            for (var i = 0; i < numeroRegistros; i++) {
                var txtQuantity = $('#txtQuantity-'+i).val();
                
                if (txtQuantity.trim() != "" && parseFloat(txtQuantity.trim()) <= 0) { 
                    validate = false;
                    mensaje += _idioma == "INGLES" ? "-Valid quantity - row " + i +"\n" : "-Cantidad valida - fila " + (i + 1) +"\n";
                }else
                {
                    if (txtQuantity.trim() != "") {
                        dataSave = true;
                    }
                } 
            }

            debugger;
            if (!validate) {
                alert((_idioma == "INGLES" ? "Please, enter this fields:\n" : "Por favor ingrese los siguientes campos:\n") + mensaje);
            }
            else if (!dataSave) {
                validate = false;
                alert(_idioma == "INGLES" ? "Please, enter information to save" : "Por favor ingrese información para guardar");
            }

            return validate;
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table border="0">
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblOrder" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtOrder" CssClass="TextBoxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <hr />  
                <asp:Button Text="" runat="server" ID="btnConsultar" OnClick="btnConsultar_Click" CssClass="ButtonsSendSave" style="height:30px;" />
            </td>
        </tr>
    </table>

     <div runat="server" id="divBtnGuardar" visible="false">
        <hr />
        <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" OnClientClick="return validarFormulario();" CssClass="ButtonsSendSave"/>
    </div>

    <div runat="server" id="divTable">
        
        

    </div>


    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />

</asp:Content>