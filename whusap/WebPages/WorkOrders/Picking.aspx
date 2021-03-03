<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="Picking.aspx.cs" Inherits="whusap.WebPages.WorkOrders.Picking" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="http://code.jquery.com/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="http://cdn.datatables.net/1.10.2/js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/limonte-sweetalert2/7.33.1/sweetalert2.css"
        integrity="sha512-3QG6i4RNIYVKJ4nysdP4qo87uoO+vmEzGcEgN68abTpg2usKfuwvaYU+sk08z8k09a0vwflzwyR6agXZ+wgfLA=="
        crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/limonte-sweetalert2/7.33.1/sweetalert2.min.js"
        integrity="sha512-aDa+VOyQu6doCaYbMFcBBZ1z5zro7l/aur7DgYpt7KzNS9bjuQeowEX0JyTTeBTcRd0wwN7dfg5OThSKIWYj3A=="
        crossorigin="anonymous"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style type="text/css">
        .swal2-popup {   
            width: 850px !important;
        }  

        #MyEtiqueta {
            font-size: 14px;
        }

        #MyEtiqueta2 label {
            font-size: 15px;
        }

        #LblDated { 
            font-size: 14px !important;
        }

        #LblReprintInd, #LblReprint {
            display: none;
        }

        .colorButton {
            background-color: #C0C0C0;
            background-position: center;
            font-size: 24px;
            height: 100%;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            padding: 5px 5px 5px 5px;
        }

        .hidebutton {
            display: none;
        }

        #LblError {
            color: Red;
            font-size: 14px;
        }

        .colorButton2 {
            background-color: #3399ff;
            background-position: center;
            font-size: 24px;
            height: 100%;
            width: 70%;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            text-align: left;
            padding: 5px 5px 5px 5px;
            color: white;
        }

        .table2 {
            font-family: arial, sans-serif;
            border-collapse: collapse;
            width: 70%;
            font-size: 16px;
        }

        td, th {
            text-align: left;
            padding: 8px;
        }

        .hidetable {
            display: none;
        }

        #Contenido_lblQuantityOld {
            display: none;
        }
    </style>
    <script>
        function printDiv(divID) {

            var monthNames = [
                "1", "2", "3",
                "4", "5", "6", "7",
                "8", "9", "10",
                "11", "12"
            ];

            //PRINT LOCAL HOUR
            var d = new Date();
            var dateNow = (monthNames[d.getMonth()] +
                "/" +
                d.getDate() +
                "/" +
                d.getFullYear() +
                " " +
                d.getHours() +
                ":" +
                d.getMinutes() +
                ":" +
                d.getSeconds());
            var LbdDate = $("#LblDate");
            LbdDate.html(dateNow);

            var LbdDate2 = $("#LblDate2");
            LbdDate2.html(dateNow);

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

            var mywindow = window.open('', 'PRINT');

            mywindow.document.write('<html><head><title>' + document.title + '</title>');
            mywindow.document.write('</head><body >');
            //mywindow.document.write('<h1>' + document.title + '</h1>');
            mywindow.document.write(document.getElementById(divID).innerHTML);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/

            mywindow.print();

            return true;
        };
    </script>
    <form id="form1" class="container">
        <div class="form-group row">
            <input id="getusers1" type="button" class="btn btn-primary btn-lg" onclick="getusers()"
                value="Get user session" style="display:none"/>

            <label class="col-sm-2 col-form-label-lg hidebutton" for="txQuantity">
                Pallet ID</label>
            <div class="col-sm-4">
                <input type="text" class="hidebutton" id="txPalletID" placeholder="Pallet ID" data-method="ValidarQuantity">
            </div>
        </div>
        <br />
        <div id="blocked">
            <div class="row">
                <div id="MyEtiqueta2" class="col-6">
                    <table class="table2">
                        <tr>
                            <asp:Label ID="lblCNPK" runat="server" CssClass=""></asp:Label>
                        </tr>
                        <tr>
                            ADVS:
                            <asp:Label ID="lblDetail" runat="server" CssClass=""></asp:Label>
                            <asp:Label ID="lblADVS" runat="server" CssClass=""></asp:Label>

                        </tr>
                        <tr>
                            <td class="">
                                <label class="" id="Label1">
                                    Pallet ID</label>
                            </td>
                            <td>
                                <asp:Label ID="lblPalletID" runat="server" CssClass=""></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPalletID" MaxLength="20" Style="text-transform: uppercase;" runat="server"
                                    CssClass="form-control" onkeyup="validarPallet();" Font-Size="Medium"></asp:TextBox>
                                <asp:Button ID="Reload" runat="server" Text="Next Picking" OnClick="Reload_Click"
                                    class="btn btn-primary btn-lg mt-3" />
                            </td>
                        </tr>
                        <tr>
                            <td class="">
                                <label class="">
                                    Item</label>
                            </td>
                            <td class="">
                                <asp:Label ID="lblItemID" runat="server" CssClass=""></asp:Label>
                            </td>
                            <td class="">
                                <asp:Label ID="lblItemDesc" runat="server" CssClass=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="">
                                <label class="">
                                    Lot</label>
                            </td>
                            <td class="">
                                <asp:Label ID="LblLotId" runat="server" CssClass=""></asp:Label>
                            </td>
                            <td class="">
                                <asp:Label ID="LblLotIdDesc" runat="server" CssClass=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="">
                                <label class="">
                                    Warehouse</label>
                            </td>
                            <td class="">
                                <asp:Label ID="lblWarehouse" runat="server" CssClass=""></asp:Label>
                            </td>
                            <td class="">
                                <asp:Label ID="lblWareDescr" runat="server" CssClass=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="">
                                <label class="">
                                    Location</label>
                            </td>
                            <td class="">
                                <asp:Label ID="lbllocation" runat="server" CssClass=""></asp:Label>
                            </td>
                            <td class="">
                                <%--<asp:TextBox ID="txtlocation" MaxLength="20" Style="text-transform: uppercase;" runat="server"
                        CssClass="form-control" onkeyup="validarLocation();" Font-Size="Medium"></asp:TextBox>--%>
                                <%--<asp:TextBox ID="txtlocation" MaxLength="20" runat="server"
                         Font-Size="Medium"></asp:TextBox>--%>
                                <input type="text" id="txtlocation" />
                            </td>
                        </tr>
                        <tr>
                            <td class="">
                                <label class="">
                                    Quantity</label>
                            </td>
                            <td class="">
                                <%--<asp:Label ID="lblQuantity" runat="server" CssClass=""></asp:Label>--%>
                                <asp:TextBox ID="lblQuantity" CssClass="" runat="server" type="number"></asp:TextBox>
                                <asp:Label ID="lblQuantityAux" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblQuantityOld" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblQuantityDesc" runat="server" CssClass=""></asp:Label>
                            </td>
                        </tr>
                        <tr id="HideReason">
                            <td class="">
                                <label class="">
                                    Reason</label>
                            </td>
                            <td>
                                <%-- <asp:DropDownList ID="listRegrind" runat="server"  Width="100%" height="100%" CssClass="DropDownList"    AutoPostBack="false" Font-Size="Larger" OnSelectedIndexChanged="listRegrind_SelectedIndexChanged">
                    </asp:DropDownList>--%>
                                <select class="form-control form-control-lg" id="listCausal" tabindex="1">
                                    <option value="0">Select Reason</option>
                                    <option value="1">Wrong Lot</option>
                                    <option value="2">Aisle Blocked</option>
                                    <option value="3">Wrong Location</option>
                                </select>
                            </td>
                            <td>
                                <input id="bntChange" type="button" class="btn btn-primary btn-lg" onclick="IngresarCausales()"
                                    value="CHANGE" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input id="btnconfirPKG" type="button" class="btn btn-primary btn-lg" onclick="ShowCurrentTime()"
                                    value="Confirm" />
                            </td>
                            <td></td>
                            <td>
                                <%--<input id="btnNotPKG" type="button" class="btn btn-primary btn-lg ml-20"
                        onclick="ShowCurrentOptions()" value="Pallet Can't be picked" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <label id="LblError">
                                </label>
                            </td>
                        </tr>
                    </table>

                </div>
                <div id="divTable" class="col-6">
                </div>

            </div>

            <div class="hidetable">
                <div class="">
                    <table border="1">
                        <tr>
                            <th>OORG
                            </th>
                            <th>ORNO
                            </th>
                            <%--<th>
                        OSET
                    </th>--%>
                            <th>PONO
                            </th>
                            <th>SQNB
                            </th>
                            <th>ADVS
                            </th>
                        </tr>
                        <tr>
                            <td class="">
                                <asp:Label ID="lblOORG" runat="server" CssClass=""></asp:Label>
                            </td>
                            <td class="">
                                <asp:Label ID="lblORNO" runat="server" CssClass=""></asp:Label>
                            </td>
                            <%--<td class="">
                        <asp:Label ID="lblOSET" runat="server" CssClass=""></asp:Label>
                    </td>--%>
                            <td class="">
                                <asp:Label ID="lblPONO" runat="server" CssClass=""></asp:Label>
                            </td>
                            <td class="">
                                <asp:Label ID="lblSQNB" runat="server" CssClass=""></asp:Label>
                            </td>
                            <td class=""></td>
                        </tr>
                    </table>
                </div>

                <div class="">
                    <table border="1">
                        <tr>
                            <th>OORG
                            </th>
                            <th>ORNO
                            </th>
                            <%--<th>
                        OSET
                    </th>--%>
                            <th>PONO
                            </th>
                            <th>SQNB
                            </th>
                            <th>ADVS
                            </th>
                        </tr>
                        <tr>
                            <td class="">
                                <asp:Label ID="lblOORGAUX" runat="server" CssClass=""></asp:Label>
                            </td>
                            <td class="">
                                <asp:Label ID="lblORNOAUX" runat="server" CssClass=""></asp:Label>
                            </td>
                            <%--<td class="">
                        <asp:Label ID="lblOSET" runat="server" CssClass=""></asp:Label>
                    </td>--%>
                            <td class="">
                                <asp:Label ID="lblPONOAUX" runat="server" CssClass=""></asp:Label>
                            </td>
                            <td class="">
                                <asp:Label ID="lblSQNBAUX" runat="server" CssClass=""></asp:Label>
                            </td>
                            <td class=""></td>
                        </tr>
                    </table>
                </div>
            </div>

        </div>

    </form>
    <div id="MyEtiqueta" style="display: none">
        <table style="margin: auto">
            <tr>
                <td>
                    <%--<label style="font-size: 11px">
                        Pallet ID</label>--%>
                </td>
                <td colspan="4">
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBPalletNO" alt="" hspace="60"
                        vspace="5" style="width: 4in; height: .5in; margin: 0px !important" />
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 11px">
                        ITEM</label>
                </td>
                <td colspan="4">
                    <label id="lblItemID" style="display: none; font-size: 11px">
                    </label>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBItem" alt="" hspace="60"
                        vspace="5" style="width: 4in; height: .5in; margin: 0px !important" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td align="center">
                    <label id="lblItemDesc" style="font-size: 14px">
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 11px">
                        QUANTITY</label>
                </td>
                <td colspan="4">

                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBQuantity" alt="" hspace="60"
                        vspace="5" style="width: 1in; height: .5in; margin: 0px !important" />
                    <label id="LblQuantity" style="display: none; font-size: 11px">
                    </label>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td align="center">
                    <label id="LblUnit" style="font-size: 11px">
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 11px">
                        LOT</label>
                </td>
                <td>
                    <label id="LblLotId" style="display: none; font-size: 11px">
                    </label>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBLot" alt="" hspace="60"
                        vspace="5" style="width: 4in; height: .5in; margin: 0px !important" />
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 9px">
                        RECEIPT DATE</label>
                </td>
                <td>
                    <label id="LblDate" style="font-size: 9px">
                    </label>
                </td>
                <!--<td>
                    <label>
                        REPRINT:</label>
                </td>
                <td>
                    <label id="LblReprint">
                    </label>
                </td>-->
            </tr>
        </table>
        <br />
        <br />
        <table style="margin: auto">
            <tr>
                <td>
                    <%--<label style="font-size: 11px">
                        Pallet ID</label>--%>
                </td>
                <td colspan="4">
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBPalletNO2" alt="" hspace="60"
                        vspace="5" style="width: 4in; height: .5in; margin: 0px !important" />
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 11px">
                        ITEM</label>
                </td>
                <td colspan="4">
                    <label id="lblItemID" style="display: none; font-size: 11px">
                    </label>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBItem2" alt="" hspace="60"
                        vspace="5" style="width: 4in; height: .5in; margin: 0px !important" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td align="center">
                    <label id="lblItemDesc2" style="font-size: 14px">
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 11px">
                        QUANTITY</label>
                </td>
                <td colspan="4">

                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBQuantity2" alt="" hspace="60"
                        vspace="5" style="width: 1in; height: .5in; margin: 0px !important" />
                    <label id="LblQuantity2" style="display: none; font-size: 11px">
                    </label>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td align="center">
                    <label id="LblUnit2" style="font-size: 11px">
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 11px">
                        LOT</label>
                </td>
                <td>
                    <label id="LblLotId2" style="display: none; font-size: 11px">
                    </label>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBLot2" alt="" hspace="60"
                        vspace="5" style="width: 4in; height: .5in; margin: 0px !important" />
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 9px">
                        RECEIPT DATE</label>
                </td>
                <td>
                    <label id="LblDate2" style="font-size: 9px">
                    </label>
                </td>
                <!--<td>
                    <label>
                        REPRINT:</label>
                </td>
                <td>
                    <label id="LblReprint">
                    </label>
                </td>-->
            </tr>
        </table>
    </div>
    <script type="text/javascript">


        function EventoAjax(Method, Data, MethodSuccess) {
            $.ajax({
                type: "POST",
                url: "Picking.aspx/" + Method.trim(),
                data: Data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: MethodSuccess
            })
        };

        var flagPallet = 0;
        var flaglocation = 0;

        function IdentificarControles() {


            MyEtiquetaOC = $('#MyEtiquetaOC');
            MyEtiqueta = $('#MyEtiqueta');

            //Formulario

            txPalletID = $('#txPalletID');
            listCausal = $('#listCausal');
            btnEnviar = $('#btnEnviar');


            lblItemID = $('#lblItemID');
            lblItemDesc = $('#lblItemDesc');

            LblQuantity = $('#LblQuantity');
            LblUnit = $('#LblUnit');

            LblLotId = $('#LblLotId');
            LblDate = $('#LblDate');
            LblReprint = $('#LblReprint');
            LblReprintInd = $('#LblReprintInd');
        }
        function imprimir(r) {
            //console.log(r.d)
        }

        function getusers() {
            EventoAjax("getusers", '{}', imprimir)
        }

        function formatDate(date) {
            var monthNames = [
                "1", "2", "3",
                "4", "5", "6", "7",
                "8", "9", "10",
                "11", "12"
            ];

            var day = date.getDate();
            var monthIndex = date.getMonth();
            var year = date.getFullYear();

            var hours = date.getHours();
            var minutes = date.getMinutes();
            var seconds = date.getSeconds();

            return ' ' + monthNames[monthIndex] + '/' + day + '/' + year + ' ' + hours + ':' + minutes + ':' + seconds + ' ';
        }

        var FuncitionSuccesReprint = function (r) {
            //console.log(r.d);
            MyEtiqueta.hide('slow');

            var MyObject = JSON.parse(r.d);
            if (MyObject.MyError == undefined) {

                lblItemID.html(MyObject.ITEM);
                lblItemDesc.html(MyObject.DSCA);

                LblQuantity.html(MyObject.QTYC);
                LblUnit.html(MyObject.UNIC);

                LblLotId.html(MyObject.CLOT);
                LblDate.html(" " + formatDate(new Date()) + " ");
                LblReprint.html(MyObject.NPRT);


                if (parseInt(MyObject.NPRT, 10) > 1) {
                    LblReprintInd.show();
                    LblReprint.show();
                }

                MyEtiqueta.show('slow');
                alert("Pickup Process Succesfully");

            }
            else if (MyObject.MyError != undefined) {
                MyEtiqueta.hide('slow');
                alert(MyObject.MyError);
            }

        }

        var FuncitionSuccesQuery = function (r) {
            var MyObject = JSON.parse(r.d);

            if (MyObject.error == false) {
                MyEtiqueta.show('slow');
                lblPalletID.html(MyObject.PAID);
                lblWarehouse.html(MyObject.CWAR);
                lblItemID.html(MyObject.ITEM);
                LblLotId.html(MyObject.CLOT);
            }
            else {
                alert(MyObject.errorMsg);
            }

        }

        $(document).keypress(
         function validar(e) {
             var keycode = (e.keyCode ? e.keyCode : e.which);

             var _txt1 = document.getElementById("<%=lblPalletID.ClientID %>").innerHTML.toString();
             var _txt2 = $('#<%=txtPalletID.ClientID%>').val().toString();
             var result = _txt1.trim() == _txt2.trim() ? 1 : 2;

             if (keycode == '13') {
                 if (result === 1) {
                     var flagPallet = 1;
                     document.getElementById("txtlocation").disabled = false;
                     document.getElementById("txtlocation").focus();
                     return false;
                 }
                 else {
                     alert('Pallet Id not equal to the selected pallet');
                     document.getElementById("txtlocation").value = "";
                     document.getElementById("txtlocation").disabled = true;
                     //JUANC
                     return false;
                 }
             }
         });
        //        var PalletIDSucces = function (r) {
        //            if (r.d) {
        //                alert(r.d);

        //            }
        //            else if (!r.d) {
        //                alert(r.d);
        //            }
        //        }
        //         public static bool VerificarLocate(string CWAR,string LOCA)
        //        public static bool VerificarPalletID(string PAID)

        function pulsar2(e) {
            //tecla = (document.all) ? e.keyCode : e.which;
            var _txt3 = document.getElementById("<%=lbllocation.ClientID %>").innerHTML.toString();
            var _txt4 = $('#txtlocation').val().toString();

            var result = _txt3.trim() == _txt4.trim() ? 1 : 2;

            if (tecla == 13)
                //alert("presion la tecla");
                validar2(result);
            return false;

        }

        function validar2(num) {

            //            if (num === 1) {

            //                HideReason.style.display = "none";
            //                document.getElementById("bntChange").disabled = true;
            //                document.getElementById("txtlocation").disabled = true;
            //                document.getElementById('btnconfirPKG').disabled = false;

            //                return false;
            //            }
            //            else {
            //                document.getElementById('btnconfirPKG').disabled = true;
            //                Method = "VerificarLocate"
            //                Data = "{'CWAR':'" + $('#Contenido_lblWarehouse').html().trim() + "','LOCA':'" + $('#txtlocation').val().trim() + "'}";
            //                EventoAjax(Method, Data, PalletIDSuccess)
            //                //JUAN C
            //                return false;
            //            }
        }

        var PalletIDSuccess = function (r) {
            var MyObj = JSON.parse(r.d);

            if (MyObj.error == false) {


                HideReason.style.display = "";
                $('#LblError').html("");
                $('#Contenido_lblPalletID').html(MyObj.PALLETID.toString())
                $('#Contenido_LblLotId').html(MyObj.LOT.toString())
                $('#Contenido_lblWarehouse').html(MyObj.WRH.toString())
                $('#Contenido_lblWareDescr').html(MyObj.DESCWRH.toString())
                $('#Contenido_lbllocation').html(MyObj.LOCA.toString())
                $('#Contenido_lblQuantityAux').html(MyObj.QTY.toString())
                $('#Contenido_lblQuantityDesc').html(MyObj.UN.toString())

                document.getElementById("bntChange").disabled = true;
            }
            else if (MyObj.error == true) {
                HideReason.style.display = "none";
                $('#LblError').html(MyObj.errorMsg);
                document.getElementById("bntChange").disabled = true;
            }
        }

        function ShowCurrentTime() {
            if ($("#Contenido_lblCNPK").html() != "1") {
                dataS = "{'PAID_OLD':'" + $('#Contenido_lblPalletID').html() + "','PAID':'" + $("#<%=txtPalletID.ClientID%>")[0].value.toUpperCase() + "', 'LOCA':'" + $('#txtlocation').val().toUpperCase() + "','OORG':'" + document.getElementById("<%=lblOORG.ClientID %>").innerHTML.toString() + "','ORNO':'" + document.getElementById("<%=lblORNO.ClientID %>").innerHTML.toString() + "','PONO':'" + document.getElementById("<%=lblPONO.ClientID %>").innerHTML.toString() + "' ,'QTYT':'" + ($("#Contenido_lblQuantity").val() == undefined ? $("#Contenido_lblQuantityAux").html() : $("#Contenido_lblQuantity").val()) + "' ,'QTYT_OLD':'" + document.getElementById("<%=lblQuantityAux.ClientID %>").innerHTML.toString() + "','CUNI':'" + $('#Contenido_lblQuantityDesc').html() + "', 'CWAR':'" + $('#Contenido_lblWarehouse').html() + "', 'CLOT':'" + $('#Contenido_LblLotId').html() + "', 'ADVSP':'" + $('#Contenido_lblADVS').html().trim() + "'}";
            }
            else {
                dataS = "{'PAID_OLD':'" + $('#Contenido_lblPalletID').html() + "','PAID':'" + $("#<%=txtPalletID.ClientID%>")[0].value.toUpperCase() + "', 'LOCA':'" + $('#txtlocation').val().toUpperCase() + "','OORG':'" + document.getElementById("<%=lblOORG.ClientID %>").innerHTML.toString() + "','ORNO':'" + document.getElementById("<%=lblORNO.ClientID %>").innerHTML.toString() + "','PONO':'" + document.getElementById("<%=lblPONO.ClientID %>").innerHTML.toString() + "' ,'QTYT':'" + ($("#Contenido_lblQuantity").val() == undefined ? $("#Contenido_lblQuantityAux").html() : $("#Contenido_lblQuantity").val()) + "' ,'QTYT_OLD':'" + document.getElementById("<%=lblQuantityAux.ClientID%>").innerHTML.toString() + "','CUNI':'" + $('#Contenido_lblQuantityDesc').html() + "', 'CWAR':'" + $('#Contenido_lblWarehouse').html() + "', 'CLOT':'" + $('#Contenido_LblLotId').html() + "', 'ADVSP':'" + $('#Contenido_lblADVS').html().trim() + "'}";
            }

            //"'CUNI':'" + $('#Contenido_lblQuantityDesc').html() + "', 'LOCA':'" + $('#Contenido_lbllocation').html() + "', 'CWAR':'" + $('#Contenido_lblWarehouse').html() + "', 'CLOT':'" + $('#Contenido_LblLotId').html() + "'"
            $.ajax({
                type: "POST",
                url: "Picking.aspx/Click_confirPKG",
                data: dataS,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var myObj = JSON.parse(response.d)
                    if (myObj.Error == false) {

                        ////window.location = "/WebPages/Login/whMenuI.aspx";
                        var newCant = parseFloat($("#Contenido_lblQuantity").val());
                        var oldCant = parseFloat($("#Contenido_lblQuantityAux").html());

                        $('#Contenido_CBPalletNO').attr("src", myObj.PAID_OLD_URL);
                        $('#Contenido_CBItem').attr("src", myObj.ITEM_URL);
                        $('#lblItemDesc').html($('#Contenido_lblItemDesc').html());
                        $('#Contenido_CBQuantity').attr("src", myObj.QTYC_URL);
                        $('#LblUnit').html($('#Contenido_lblQuantityDesc').html());

                        $('#Contenido_CBLot').attr("src", myObj.CLOT_URL);
                        if (myObj.CLOT_URL != undefined) {
                            myObj.CLOT_URL.trim() == "" ? $('#Contenido_CBLot').hide() : $('#Contenido_CBLot').show();
                        }
                        //$('#LblDate').html();

                        $('#Contenido_CBPalletNO2').attr("src", myObj.PAID_URL);
                        $('#Contenido_CBItem2').attr("src", myObj.ITEM_URL);
                        $('#lblItemDesc2').html($('#Contenido_lblItemDesc').html());
                        $('#Contenido_CBQuantity2').attr("src", myObj.QTYC1_URL);
                        $('#LblUnit2').html($('#Contenido_lblQuantityDesc').html());

                        $('#Contenido_CBLot2').attr("src", myObj.CLOT_URL);
                        if (myObj.CLOT_URL != undefined) {
                            myObj.CLOT_URL.trim() == "" ? $('#Contenido_CBLot2').hide() : $('#Contenido_CBLot2').show();
                        }
                        //$('#LblDate2').html();
                        var newCant = parseFloat($("#Contenido_lblQuantity").val());
                        var oldCant = parseFloat($("#Contenido_lblQuantityAux").html());
                        alert("Information saved successfully");
                        //console.log(myObj.urpt);

                        if (myObj.qtyaG > 0) {
                            printDiv("MyEtiqueta");
                        }




                        $('#Contenido_lblPalletID').html("");
                        $('#Contenido_lblItemID').html("");
                        $('#Contenido_LblLotId').html("");
                        $('#Contenido_lblWarehouse').html("");
                        $('#Contenido_lbllocation').html("");
                        $('#Contenido_lblQuantity').html("");
                        $('#Contenido_lblItemDesc').html("");
                        $('#Contenido_lblWareDescr').html("");
                        $('#Contenido_lblQuantityDesc').html("");
                        $('#Contenido_txtPalletID').val("");
                        $('#txtlocation').val("");

                    }
                    else {
                        alert(myObj.errorMsg);
                        $('#Contenido_lblPalletID').html("");
                        $('#Contenido_lblItemID').html("");
                        $('#Contenido_LblLotId').html("");
                        $('#Contenido_lblWarehouse').html("");
                        $('#Contenido_lbllocation').html("");
                        $('#Contenido_lblQuantity').html("");
                        $('#Contenido_lblItemDesc').html("");
                        $('#Contenido_lblWareDescr').html("");
                        $('#Contenido_lblQuantityDesc').html("");
                        $('#Contenido_txtPalletID').val("");
                        $('#txtlocation').val("");

                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        function clearForm() {
            $('#Contenido_lblPalletID').html("");
            $('#Contenido_lblItemID').html("");
            $('#Contenido_LblLotId').html("");
            $('#Contenido_lblWarehouse').html("");
            $('#Contenido_lbllocation').html("");
            $('#Contenido_lblQuantity').html("");
            $('#Contenido_lblItemDesc').html("");
            $('#Contenido_lblWareDescr').html("");
            $('#Contenido_lblQuantityDesc').html("");
            $('#Contenido_txtPalletID').val("");
            $('#txtlocation').val("");
        }

        function OnSuccess(response) {
            alert(response.d);
        }

        function ShowCurrentOptions() {
            var bodyRows = ""
            //var tableOptions = "<table class='table' style='width:100%'>" +
            //                                    "<thead>" +
            //                                      "<tr>" +
            //                                        "<th scope='col'>Pallet</th>" +
            //                                        "<th scope='col'>Location</th>" +
            //                                        "<th scope='col'>Item</th>" +
            //                                        "<th scope='col'>Description</th>" +
            //                                        "<th scope='col'>Quantity</th>" +
            //                                        "<th scope='col'>Unit</th>" +
            //                                    "</tr>" +
            //                                   "</thead>" +
            //                                   "<tbody>" +
            //                                   bodyRows
            //"</tbody>" +
            //                        "</table>";


            //$("#divTable").append(tableOptions);
            $.ajax({
                type: "POST",

                url: "Picking.aspx/ShowCurrentOptions",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    myObj = JSON.parse(response.d);
                    window.localStorage.setItem('MyPalletList', JSON.stringify(myObj));
                    for (var i = 0; i < myObj.length; i++) {
                        bodyRows += "<tr onClick='selectNewPallet(this)' id='rowNum" + i + "'><td>" + myObj[i].PALLETID + "</td><td>" + myObj[i].LOCA + "</td><td>" + myObj[i].ITEM + "</td><td>" + myObj[i].DESCRIPTION + "</td><td>" + myObj[i].QTY + "</td><td>" + myObj[i].UN + "</td></tr>";
                    }
                    var tableOptions = "<table class='table' style='width:100%'>" +
                                                "<thead>" +
                                                  "<tr>" +
                                                    "<th scope='col'>Pallet</th>" +
                                                    "<th scope='col'>Location</th>" +
                                                    "<th scope='col'>Item</th>" +
                                                    "<th scope='col'>Description</th>" +
                                                    "<th scope='col'>Quantity</th>" +
                                                    "<th scope='col'>Unit</th>" +
                                                "</tr>" +
                                               "</thead>" +
                                               "<tbody>" +
                                               bodyRows
                    "</tbody>" +
                                            "</table>";


                    $("#divTable").append(tableOptions);
                    //                    Swal.fire({
                    //                        title: '<strong>Options</strong>',
                    //                        icon: 'info',
                    //                        html: tableOptions,
                    //                        showCloseButton: false,
                    //                        showCancelButton: false,
                    //                        focusConfirm: false
                    //                    });
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {
            alert(response.d);
        }

        function IngresarCausales() {
            $.ajax({
                type: "POST",
                url: "Picking.aspx/Click_confirCausal",
                data: "{'PAID':'" + document.getElementById("<%=lblPalletID.ClientID %>").innerHTML.toString() + "','Causal':'" + document.getElementById("listCausal").value + "' ,'txtPallet':'" + $("#<%=txtPalletID.ClientID%>")[0].value + "','OORG':'" + document.getElementById("<%=lblOORG.ClientID %>").innerHTML.toString() + "','ORNO':'" + document.getElementById("<%=lblORNO.ClientID %>").innerHTML.toString() + "','PONO':'" + document.getElementById("<%=lblPONO.ClientID %>").innerHTML.toString() + "' ,'SQNB':'" + document.getElementById("<%=lblSQNB.ClientID %>").innerHTML.toString() + "','ADVS':'" + document.getElementById("<%=lblADVS.ClientID %>").innerHTML.toString() + "' ,'LOCA':'" + document.getElementById("<%=lbllocation.ClientID %>").innerHTML.toString() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d == true) {
                        $('#txtlocation').removeAttr('disabled');
                        alert("Reason saved");
                        //window.location = "/WebPages/Login/whMenuI.aspx";
                        //                        if ($('#txtlocation').val().trim().toUpperCase() != $('#Contenido_lbllocation').html().trim().toUpperCase() && $('#Contenido_txtPalletID').val().trim().toUpperCase() == $('#Contenido_lblPalletID').html().trim().toUpperCase()) {
                        //                            document.getElementById('btnconfirPKG').disabled = false;
                        //                        }

                        if ($('#Contenido_txtPalletID').val().trim().toUpperCase() != $('#Contenido_lblPalletID').html().trim().toUpperCase()) {
                            $("#btnNotPKG").show();
                        }
                    }
                    else {
                        $('#txtlocation').attr('disabled', 'disabled');
                        $("#btnNotPKG").hide();
                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {
            alert("Information not saved");
            window.location.href = "/WebPages/Login/whMenuI.aspx";
        }


        function delayTimer() {
            var timer;
            return function (fun, time) {
                clearTimeout(timer);
                timer = setTimeout(fun, time);
            };
        }

        var delayFunction = delayTimer();

        // Takes parameters to display
        function validarPallet() {

            var _txt1 = document.getElementById("<%=lblPalletID.ClientID %>").innerHTML.toString();
            var _txt2 = $('#<%=txtPalletID.ClientID%>').val().toString().toUpperCase();

            if (_txt2.length >= 13) {
                var result = _txt1.trim() == _txt2.trim() ? 1 : 2;

                if (result == 1) {
                    var flagPallet = 1;
                    if ($("#Contenido_lblCNPK").html() != "1") {
                        document.getElementById("txtlocation").disabled = false;
                        document.getElementById("txtlocation").focus();
                        return false;
                    }
                    else {
                        document.getElementById("txtlocation").disabled = false;
                        document.getElementById("txtlocation").focus();
                        $("#btnconfirPKG").disabled = false;
                    }

                }
                else {
                    alert('Pallet Id not equal to the selected pallet');
                    document.getElementById("txtlocation").value = "";
                    document.getElementById("txtlocation").disabled = true;
                    var exist = false;
                    var MyPalletList = JSON.parse(JSON.stringify(window.localStorage.getItem('MyPalletList')));
                    for (var i in JSON.parse(MyPalletList)) {
                        var MyList = JSON.parse(MyPalletList);

                        if (MyList[i]["PALLETID"].trim() == _txt2) {
                            MyObj = MyList[i];
                            exist = true;
                        }
                    }

                    if (exist) {
                        $('#LblError').html("");
                        //                        HideReason.style.display = "";
                        //                        $('#LblError').html("");
                        //                        $('#Contenido_lblPalletID').html(MyObj["PALLETID"].toString())
                        //                        $('#Contenido_LblLotId').html(MyObj["LOT"].toString())
                        //                        $('#Contenido_lblWarehouse').html(MyObj["WRH"].toString())
                        //                        $('#Contenido_lblWareDescr').html(MyObj["DESCWRH"].toString())
                        //                        $('#Contenido_lbllocation').html(MyObj["LOCA"].toString())
                        //                        $('#Contenido_lblQuantity').html(MyObj["QTY"].toString())
                        //                        $('#Contenido_lblQuantityDesc').html(MyObj["UN"].toString())

                        document.getElementById("bntChange").disabled = true;
                        Method = "VerificarPalletID"
                        Data = "{'PAID_NEW':'" + _txt2 + "', 'PAID_OLD':'" + $('#Contenido_lblPalletID').html() + "','selectOptionPallet':'false'}";
                        EventoAjax(Method, Data, PalletIDSuccess)

                    }
                    else {
                        HideReason.style.display = "none";
                        $('#LblError').html("The pallet does not correspond to those available");
                        document.getElementById("bntChange").disabled = true;
                    }
                }
                //                else {
                //                    alert('Pallet Id not equal to the selected pallet');
                //                    document.getElementById("txtlocation").value = "";
                //                    document.getElementById("txtlocation").disabled = true;

                //                    Method = "VerificarPalletID"
                //                    Data = "{'PAID_NEW':'" + _txt2 + "', 'PAID_OLD':'" + $('#Contenido_lblPalletID').html() + "','selectOptionPallet':'false'}";
                //                    EventoAjax(Method, Data, PalletIDSuccess)

                //                    //JUANC
                //                    return false;
                //                }
            }
        }


        function validarLocation() {
            var _txt3 = document.getElementById("<%=lbllocation.ClientID %>").innerHTML.toString();
            var _txt4 = $('#txtlocation').val().toString().toUpperCase();

            if (_txt4.length > 0) {
                var result = _txt3.trim() == _txt4.trim() ? 1 : 2;

                //                validar2(result);
            }
        }

        $('#listCausal').change(function (a) {
            if (a.target.selectedIndex != 0) {
                document.getElementById("txtlocation").disabled = true;
                document.getElementById("bntChange").disabled = false;
                $('#txtlocation').focus();
            }
            else {
                document.getElementById("txtlocation").disabled = true;
                document.getElementById("bntChange").disabled = true;

                document.getElementById("txtlocation").value = "";
            }
        });

        var LocateSuccess = function (r) {
            if (r.d == true) {
                document.getElementById("btnconfirPKG").disabled = false;
            }
            else if (r.d == false) {
                document.getElementById("btnconfirPKG").disabled = true;
            }
        }

        var LocateSuccessD = function (r) {
            if (r.d == true) {
                HideReason.style.display = "";
                document.getElementById("btnconfirPKG").disabled = true;
            }
            else if (r.d == false) {
                document.getElementById("btnconfirPKG").disabled = true;
                HideReason.style.display = "none";
            }
        }



        $('#txtlocation').bind("change paste keyup", function (a) {

            Method = "VerificarLocate"
            Data = "{'CWAR':'" + $('#Contenido_lblWarehouse').html().trim().toUpperCase() + "','LOCA':'" + $('#txtlocation').val().trim().toUpperCase() + "'}";
            //            if($('#Contenido_txtPalletID').val().trim().toUpperCase() == $('#Contenido_lblPalletID').html().trim().toUpperCase() && $('#txtlocation').val().trim().toUpperCase() == $('#Contenido_lbllocation').html().trim().toUpperCase())
            //            {
            //                document.getElementById('btnconfirPKG').disabled = false;
            //            }
            //            else if($('#Contenido_txtPalletID').val().trim().toUpperCase() != $('#Contenido_lblPalletID').html().trim().toUpperCase() && $('#txtlocation').val().trim().toUpperCase() == $('#Contenido_lbllocation').html().trim().toUpperCase())
            //            {
            //            }
            //            else if($('#Contenido_txtPalletID').val().trim().toUpperCase() != $('#Contenido_lblPalletID').html().trim().toUpperCase() && $('#txtlocation').val().trim().toUpperCase() != $('#Contenido_lbllocation').html().trim().toUpperCase())
            //            {
            //                document.getElementById('btnconfirPKG').disabled = false;
            //                EventoAjax(Method, Data, LocateSuccess)
            //            }
            //            else if ($('#Contenido_txtPalletID').val().trim().toUpperCase() == $('#Contenido_lblPalletID').html().trim().toUpperCase() && $('#txtlocation').val().trim().toUpperCase() != $('#Contenido_lbllocation').html().trim().toUpperCase()) 
            //            { 
            //            }  


            if ($('#txtlocation').val().trim().toUpperCase() == $('#Contenido_lbllocation').html().trim().toUpperCase() && $('#Contenido_txtPalletID').val().trim().toUpperCase() == $('#Contenido_lblPalletID').html().trim().toUpperCase()) {

                HideReason.style.display = "none";
                document.getElementById("bntChange").disabled = true;
                document.getElementById("txtlocation").disabled = false;
                document.getElementById('btnconfirPKG').disabled = false;
            }
            else if ($('#txtlocation').val().trim().toUpperCase() != $('#Contenido_lbllocation').html().trim().toUpperCase() && $('#Contenido_txtPalletID').val().trim().toUpperCase() == $('#Contenido_lblPalletID').html().trim().toUpperCase()) {


                document.getElementById("txtlocation").disabled = false;
                document.getElementById('btnconfirPKG').disabled = true;
                EventoAjax(Method, Data, LocateSuccessD)
            }
            else {
                document.getElementById('btnconfirPKG').disabled = false;
                //Method = "VerificarLocate"
                //Data = "{'CWAR':'" + $('#Contenido_lblWarehouse').html().trim().toUpperCase() + "','LOCA':'" + $('#txtlocation').val().trim().toUpperCase() + "'}";
                EventoAjax(Method, Data, LocateSuccess)
                //JUAN C
                return false;
            }
        });

        $(document).ready(function () {
            HideReason.style.display = "none";
            document.getElementById("bntChange").disabled = true;
            document.getElementById("txtlocation").disabled = true;
            //document.getElementById("Contenido_lblCNPK").innerText == "1" ? document.getElementById("Contenido_lblQuantity").disabled = true : document.getElementById("Contenido_lblQuantity").disabled = false;
            document.getElementById("<%=txtPalletID.ClientID %>").focus();
            document.getElementById('btnconfirPKG').disabled = true;
            ShowCurrentOptions();
        });
        var selectNewPalletSuccess = function (r) {
            PalletIDSuccess(r);
        }
        var selectNewPallet = function (currentRow) {
            currentRow = currentRow.cells[0].innerHTML.toString().trim();
            EventoAjax("VerificarPalletID", "{'PAID_NEW':'" + currentRow + "', 'PAID_OLD':'" + $('#Contenido_lblPalletID').html() + "','selectOptionPallet':'true'}", selectNewPalletSuccess);
        }
        $("#Contenido_lblQuantity").bind("change paste keyup",
            function () {
                var newCant = parseFloat($("#Contenido_lblQuantity").val());
                var oldCant = parseFloat($("#Contenido_lblQuantityAux").html());
                if (newCant > 0 && newCant != null) {
                    if (newCant <= oldCant) {

                    }
                    else {
                        $("#Contenido_lblQuantity").val($("#Contenido_lblQuantityOld").html());
                    }
                }
            }
        );

    </script>
</asp:Content>
