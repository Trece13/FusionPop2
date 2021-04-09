<%@ Page Title="" Language="C#" MasterPageFile="../../MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="InventoryAdjustment.aspx.cs" Inherits="whusap.WebPages.Inventarios.InventoryAdjustment"
    EnableViewStateMac="false" Theme="Cuadriculas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
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

        .codePaid {
            display: block;
            margin: auto;
            height: 121px;
            width: 438px;
        }

        .codeItem {
            display: block;
            margin: auto;
            height: 50px;
            width: 150px;
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
            --display: none;
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

        @keyframes spin {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
        }
    </style>
    <script type="text/javascript">
        function replaceDecimal(val) {

        }

        function validaPaid(val) {
            //var parametrosEnviar = "{ 'valor':'" + args.value + "', 'quantityToReturn': '" + quantityToReturn + "'}";
            // var quantityToReturn = $("#Contenido_grdRecords_toReturn_" + rowIndex).val();
            var parametrosEnviar = "{ 'palletID': '" + val + "'}";
            //alert(" pId " + args.value + " toReturn " + quantityToReturn);
            var objSend = document.getElementById('<%=btnSend.ClientID %>');
            objSend.disabled = true;
            $.ajax({
                type: "POST",
                url: "InventoryAdjustment.aspx/vallidatePalletID",
                data: parametrosEnviar,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != "") {
                        var objControl = document.getElementById('<%=txtPalletId.ClientID %>');
                        objControl.value = "";
                        alert(msg.d);
                        return false;
                    } else { objSend.disabled = false; }
                },
                error: function (msg) {
                    alert("This is msg " + msg.d);
                    return false;
                }
            });
        }
        function validarAdjustQty(qty) {

            if (qty <= 0) {

                alert("Adjustment quantity: cannot be zero (0)");
                // $("#txtAdjustmentQuantity").val('');
                $("#txtAdjustmentQuantity").focus();
                return false;
            }

            if ($("#Contenido_lblUnitValue1").html().trim().toUpperCase() == "UN") {
                $("#txtAdjustmentQuantity").val($("#txtAdjustmentQuantity").val().replace(".", "").replace(",", ""));
                $("#txtAdjustmentQuantity").focus();
                return false;
            }
            //qtyExting = $("#Contenido_lblQuantityValue").text();
            //if (parseInt(qty) > parseInt(qtyExting)) {

            //     alert("Adjustment quantity shuould be less than existing Qty");
            //    // $("#txtAdjustmentQuantity").val('');
            //    $("#txtAdjustmentQuantity").focus();
            //    return false;
        }


    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True" EnablePageMethods="true">
    </asp:ScriptManager>
    <br />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table border="0">

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
                                ErrorMessage="Pallet Id lenght between 12 and 13 characteres." ValidationExpression="^[a-zA-Z0-9'@&#-.\/\s]{12,20}$"
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
                    <td colspan="2" style="text-align: center;">
                        <hr />
                        <asp:Button ID="btnSend" runat="server" Text="Query" OnClick="btnSend_Click" CssClass="ButtonsSendSave" />
                    </td>
                </tr>
            </table>
            <div class="style3">
                <br />
                <asp:Table ID="tblPalletInfo" runat="server" Height="123px" Width="567px">
                    <asp:TableRow ID="TableRow1" runat="server">
                        <asp:TableCell ID="TableCell1" runat="server" Style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                            <strong>
                                <asp:Label class="" ID="lblPalletId1" Text="Pallet ID" runat="server" Style="font-size: 13px;" /></strong>
                        </asp:TableCell><asp:TableCell ID="TableCell2" runat="server">
                            <strong>
                                <asp:Label class="" ID="lblPalletId1Value" Text="Item" runat="server" /></strong>
                        </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow2" runat="server">
                        <asp:TableCell ID="TableCell3" runat="server" Style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                            <strong>
                                <asp:Label class="" ID="lblItem" Text="Item" runat="server" Style="font-size: 13px;" /></strong>
                        </asp:TableCell><asp:TableCell ID="TableCell4" runat="server">
                            <strong>
                                <asp:Label class="" ID="lblItemValue" Text="Lot" runat="server" />
                                <asp:Label class="" ID="lblItemDescValue" Text="Item Desc" runat="server" Style="padding-left: 15px;" />
                            </strong>
                        </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow3" runat="server">
                        <asp:TableCell ID="TableCell5" runat="server" Style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                            <strong>
                                <asp:Label class="" ID="lblWarehouse" Text="Warehouse" runat="server" Style="font-size: 13px;" /></strong>
                        </asp:TableCell><asp:TableCell ID="TableCell6" runat="server">
                            <strong>
                                <asp:Label class="" ID="lblWarehouseValue" Text="warehouse" runat="server" />
                                <asp:Label class="" ID="lblWarehouseDescValue" Text="warehouse Description" runat="server" Style="padding-left: 15px;" />
                            </strong>
                        </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow5" runat="server">
                        <asp:TableCell ID="TableCell9" runat="server" Style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                            <strong>
                                <asp:Label class="" ID="lblLocation" Text="Location" runat="server" Style="font-size: 13px;" /></strong>
                        </asp:TableCell><asp:TableCell ID="TableCell10" runat="server">
                            <strong>
                                <asp:Label class="" ID="lblLocationValue" Text="Location" runat="server" /></strong>
                        </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow4" runat="server">
                        <asp:TableCell ID="TableCell7" runat="server" Style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                            <strong>
                                <asp:Label class="" ID="lblLot" Text="Lot" runat="server" /></strong>
                        </asp:TableCell><asp:TableCell ID="TableCell8" runat="server">
                            <strong>
                                <asp:Label class="" ID="lblLotValue" Text="Lot" runat="server" Style="font-size: 13px;" /></strong>
                        </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow6" runat="server">
                        <asp:TableCell ID="TableCell11" runat="server" Style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                            <strong>
                                <asp:Label class="" ID="lblQuantity" Text="Quantity" runat="server" Style="font-size: 13px;" /></strong>
                        </asp:TableCell><asp:TableCell ID="TableCell12" runat="server">
                            <strong>
                                <asp:Label class="" ID="lblQuantityValue" Text="Qty" runat="server" />
                                <asp:Label class="" ID="lblUnitValue" Text="Unit" runat="server" Style="padding-left: 15px;" />
                            </strong>
                        </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow7" runat="server">
                        <asp:TableCell ID="TableCell13" runat="server" Style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                                <b style="font-size: 11px;">
                                    <asp:Label runat="server" ID="lblAdjustmentQuantity" Text="New Quantity" Style="font-size: 13px;" /></b></span>
                        </asp:TableCell><asp:TableCell ID="TableCell14" runat="server">
                            <span style="vertical-align: middle;">
                                <asp:TextBox runat="server" ID="txtAdjustmentQuantity"  CssClass="TextBoxBig" onInput="validarAdjustQty(this.value);" ClientIDMode="Static" Style="width: 80%" value="" />
                                <asp:Label class="" ID="lblUnitValue1" Text="Unit" runat="server" Style="padding-left: 15px;" />
                            </span>
                        </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow8" runat="server">
                        <asp:TableCell ID="TableCell15" runat="server" Style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                                <b style="font-size: 11px;">
                                    <asp:Label runat="server" ID="lblReasonCode" Text="Reason Code" Style="font-size: 13px;" value="" /></b></span>
                        </asp:TableCell><asp:TableCell ID="TableCell16" runat="server" Style="padding-top: 5px;">
                            <span style="vertical-align: middle;">
                                <asp:DropDownList runat="server" ID="dropDownReasonCodes" CssClass="TextBoxBig" Visible="false"></asp:DropDownList>
                                <strong>
                                    <asp:Label class="" ID="lblReason" Text="Item" runat="server" /></strong>
                            </span>
                        </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow9" runat="server" Style="padding-top: 15px;">
                        <asp:TableCell ID="TableCell17" runat="server" Style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                                <b style="font-size: 11px;">
                                    <asp:Label runat="server" ID="lblCostCenter" Text="Cost Center" Style="font-size: 13px;" value="" /></b></span>
                        </asp:TableCell><asp:TableCell ID="TableCell18" runat="server" Style="padding-top: 5px;">
                            <span style="vertical-align: middle;">
                                <asp:DropDownList runat="server" ID="dropDownCostCenters" CssClass="TextBoxBig" Visible="false"></asp:DropDownList>
                                <strong>
                                    <asp:Label class="" ID="lblCost" Text="Item" runat="server" /></strong>
                            </span>
                        </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow10" runat="server">
                        <asp:TableCell ID="TableCell19" runat="server" Style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="ButtonsSendSave" />
                        </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow11" runat="server">
                        <asp:TableCell ID="TableCell20" runat="server" Style="text-align: left;">
                           
                        </asp:TableCell></asp:TableRow></asp:Table><table border="0">

                    <%--<tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <strong>
                    <asp:Label class="" ID="lblPalletId1" Text="Pallet ID" runat="server" /></strong>
            </td>
             <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblPalletId1Value" Text="Item" runat="server" /></strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <strong>
                    <asp:Label class="" ID="lblItem" Text="Item" runat="server" /></strong>
            </td>
             <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblItemValue" Text="Lot" runat="server" />
                     <asp:Label class="" ID="lblItemDescValue" Text="Item Desc" runat="server" />
                    </strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <strong>
                    <asp:Label class="" ID="lblWarehouse" Text="Warehouse" runat="server" /></strong>
            </td>
             <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblWarehouseValue" Text="warehouse" runat="server" />
                     <asp:Label class="" ID="lblWarehouseDescValue" Text="warehouse Description" runat="server" />
                    </strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <strong>
                    <asp:Label class="" ID="lblLot" Text="Lot" runat="server" /></strong>
            </td>
             <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblLotValue" Text="Pallet ID" runat="server" /></strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <strong>
                    <asp:Label class="" ID="lblLocation" Text="Location" runat="server" /></strong>
            </td>
             <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblLocationValue" Text="Pallet ID" runat="server" /></strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <strong>
                    <asp:Label class="" ID="lblQuantity" Text="Quantity" runat="server" /></strong>
            </td>
             <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblQuantityValue" Text="Qty" runat="server" />
                     <asp:Label class="" ID="lblUnitValue" Text="Unit" runat="server" />
                    </strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                <asp:Label runat="server" ID="lblAdjustmentQuantity" text = "Adjustment Quantity" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtAdjustmentQuantity" CssClass="TextBoxBig" onblur="validarAdjustQty(this.value);" ClientIDMode="Static" />
                     <asp:Label class="" ID="Label1" Text="Unit" runat="server" />
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                <asp:Label runat="server" ID="lblReasonCode" text="Reason Code"/></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:DropDownList runat="server" ID="dropDownReasonCodes" CssClass="TextBoxBig"></asp:DropDownList>
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                <asp:Label runat="server" ID="lblCostCenter" text="Cost Center" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:DropDownList runat="server" ID="dropDownCostCenters" CssClass="TextBoxBig"></asp:DropDownList>
                </span>
            </td>
        </tr>--%>
                    <%--<tr>
            <td colspan=2>
              <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="ButtonsSendSave" />
            </td>
        </tr>--%>
                </table>

            </div>
            <div></div>
            <div id="divPrint" runat="server" visible="false">
                <div id="printContainer">
                    <div id="printSpace">
                        <div id="myLabel" class="container">
                            <div class="row">
                                <div class="col-6 alingLeft">
                                    <strong>
                                        <asp:Label ID="lblitemDesc" class="h4" runat="server" Text="Label"></asp:Label></strong></div><div class="col-6 alingRight">
                                    <asp:Image ID="codeItem" CssClass="codeItem" runat="server" />
                                </div>
                            </div>
                            <br />
                            <div class="col-12 borderTop">
                                <br />
                                <asp:Image ID="codePaid" CssClass="codePaid" runat="server" />
                            </div>
                            <br />
                            <div>
                                <table class="table">
                                    <tbody>
                                        <tr>
                                            <td id="">
                                                <strong>WO Lot</strong>&nbsp;&nbsp;<asp:Label ID="lblWorkOrder" runat="server" Text="Label"></asp:Label></td><td id="" rowspan="2" colspan="2" class="h3">
                                                <strong>Quantity</strong>&nbsp;&nbsp;<asp:Label ID="lblQuantityL" runat="server" Text="Label"></asp:Label></td></tr><tr>
                                            <td id="">
                                                <strong>Date</strong>&nbsp;&nbsp;<asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label></td></tr><tr>
                                            <td id="">
                                                <strong>Machine</strong>&nbsp;&nbsp;<asp:Label ID="lblMachine" runat="server" Text="Label"></asp:Label></td><td id="">
                                                <strong>Operator</strong>&nbsp;&nbsp;<asp:Label ID="lblInspector" runat="server" Text="Label"></asp:Label></td><td id="">
                                                <strong></strong>&nbsp;&nbsp; </td></tr><tr>
                                            <td id="">
                                                <strong>Pallet #</strong>&nbsp;&nbsp;<asp:Label ID="lblPalletNum" runat="server" Text="Label"></asp:Label></td><td id="">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Text="Made in Dublin VA"></asp:Label></strong></td><td id="" style="width: 151px;">
                                                <strong>&nbsp;&nbsp;</strong> </td></tr></tbody></table></div></div></div><br /><div id="printButton" class="container">
                        <button type="button" onclick="printLabel()" class="btn btn-link col-12"><i class="fas fa-print" id="btnPrint"></i></button>
                    </div>
                </div>
            </div>
             <asp:Label Text="" runat="server" ID="lblError" Style="color: red; font-size: 15px; font-weight: bold;" ClientIDMode="Static" />
                            <asp:Label Text="" runat="server" ID="lblConfirm" Style="color: green; font-size: 15px; font-weight: bold;" ClientIDMode="Static" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <!--<asp:Button ID="btnGetObj" runat="server" Text="Save" CssClass="ButtonsSendSave" OnClientClick="getObj(); return false;" />-->
    <!--<button id="btnGetObj" type="button">getObj</button>-->



    <script>
        function printDiv(divID) {

            //            //Get the HTML of div
            //            var divElements = document.getElementById(divID).innerHTML;
            //            //Get the HTML of whole page
            //            var oldPage = document.body.innerHTML;
            //            //Reset the page's HTML with div's HTML only
            //            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body></html>";
            //            //Print Page
            //            window.print();
            //            //Restore orignal HTML
            //            document.body.innerHTML = oldPage;
            //            window.close();
            //            return true;

            var mywindow = window.open('', 'PRINT', 'height=400px,width=600px');
            mywindow.document.write('<html><head>');
            mywindow.document.write('<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl" crossorigin="anonymous">');
            mywindow.document.write('<style>*{font-size: small !important;}#MyEtiqueta label {font-size: 15px;}'
                                    + '#LblDate {font-size: 14px !important; }'
                                    +'#LblReprintInd,#LblReprint {display: none;}'
                                    +'.isValid {border-bottom: solid; border-color: green;}'
                                    +'.isNotValid {border-bottom: solid;border-color: red;}'
                                    +'.fa-check {color: green;}'
                                    +'.fa-times {color: red;}'
                                    +'#checkItem,#checkLot,#checkWarehouse,#checkLoca,#checkPaid {display: none;}'
                                    +'#exItem,#exLot,#exWarehouse,#exLoca,#exPaid {display: none;}'
                                    +'#loadItem,#loadLot,#loadWarehouse,#loadLoca,#loadPaid {display: none;}'
                                    +'tr {text-align: center;}'
                                    +'th {text-align: center;}'
                                    +'#myLabel {width: 6in;height: 4in;padding: 20px;border: 1px solid black;border-radius: 12px;}'
                                    +'.alingRight {text-align: right;}'
                                    +'.alingLeft {text-align: left;}'
                                    +'#printButton {width: 6in;}'
                                    +'.codePaid {display: block;margin: auto;height: 121px;width: 438px;}'
                                    +'.codeItem {display: block;margin: auto;height: 50px;width: 150px;}'
                                    +'#itemDesc {vertical-align: middle;font-size: 21px;}'
                                    +'.divDesc {text-align: center;}'
                                    +'.borderTop {border-top: solid 1px gray;}'
                                    +'#printContainer {margin-bottom: 100px;--display: none;}'
                                    +'#editTable {display: none;}'
                                    +'#lblError {color: red;font-size: 13px;}'
                                    +'.load {width: 10px;height: 10px;align-content: center;animation-name: spin;animation-duration: 5000ms;animation-iteration-count: infinite;animation-timing-function: linear;}'
                                    +'#saveSection {display: none;}'
                                    +'.notBorderBottom {border-bottom: none;}</style>');

            mywindow.document.write('</head><body >');
            mywindow.document.write(document.getElementById(divID).innerHTML);
            mywindow.document.write('</body></html>');
            mywindow.focus(); // necessary for IE >= 10*/
            mywindow.print();
            mywindow.document.close(); // necessary for IE >= 10


            return true;
        };

        var printLabel = function () {
            printDiv("printSpace");
        }

        var getObj = function () {
            var Data = "{}";
            sendAjax("InventoryAdjustment.aspx/GetObj", Data, getObjSuccess);
        }

        var getObjSuccess = function (res) {
            alert(res.d);
        }

        var IdentificarControles = function () {
            //var btnGetObj = document.getElementById("Contenido_btnGetObj"); 
            //btnGetObj.addEventListener("click", getObj, false);
        }

        $(function () {
            IdentificarControles();
        });

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
    </script>
</asp:Content>
