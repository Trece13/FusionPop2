<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="DropPickedMaterialOnTunnel.aspx.cs" Inherits="whusap.WebPages.WorkOrders.NewPages.DropPickedMaterialOnTunnel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <style>
        #lblConsigment {
            margin-left: 20px;
        }
        .InputIncorrecto {
            border-bottom: solid 3px red;
            color: Red;
        }

            .InputIncorrecto:focus {
                color: red;
            }

        .InputCorrecto {
            border-bottom: solid 3px green;
            color: green;
        }

            .InputCorrecto:focus {
                color: green;
            }

        #DetallePallet {
            display: none;
        }

        #LblEtiqueta {
            display: none;
        }

        #diseño {
            width: 6in;
            height: 4in;
            border: solid 1px black;
        }

        .datoEti {
            justify-content: center;
        }


        #centrado {
            margin-top: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <form id="form1" class="container">
        <div class="row">
            <div class="col-6">
                <div class="row">
                    <div class="col-6">
                        <label for="txPickID">Picking ID</label>
                    </div>
                    <div class="col-6">
                        <div class="form-check float-right">
                            <input type="checkbox" class="form-check-input" id="chkConsigment">
                            <label class="form-check-label" for="chkConsigment" id="lblConsigment">Consigment</label>
                        </div>
                    </div>
                </div>

                <input type="text" class="form-control form-control-lg col-12" id="txPickID"
                placeholder="Pick ID" onkeyup="SearchPickIDTimer()" />
            </div>
        </div>
        <div id="divForm" class="col-6">
            <div id="DetallePallet">
                <div class="form-group row ">
                    <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="LblMachined">
                        Machine
                    </label>
                    <div class="col-sm-3">
                        <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="LblMachineval">
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <label class="col-sm-12 col-form-label-lg" for="txCustomer" id="LblMachinedes">
                        </label>
                    </div>
                </div>
                <div class="form-group row ">
                    <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="lblQuantity">
                        Pallet(s)
                    </label>
                    <div class="col-sm-3">
                        <label class="col-sm-12 col-form-label-lg" id="LblPalletsVal">
                        </label>
                    </div>
                    <%--                <div class="col-sm-3">
                    <label class="col-sm-12 col-form-label-lg" id="LblQuantityUnit">
                    </label>
                </div>--%>
                </div>
                <%--            <div class="form-group row ">
                <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="LblWorkOrderd">
                WorkOrder
                </label>
                <div class="col-sm-3">
                    <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="LblWorkOrderval">
                    </label>
                </div>
            </div>
                --%>
                <div class="form-group row" id="divQueryAction">
                    <div class="col-sm-2">
                    </div>
                    <div class="col-sm-4">
                        <input type="button" class="btn btn-primary btn-lg col-sm-7 " id="btnDropTagPick"
                            value="Drop Pick ID" onclick="ClickDropTagPick()" />
                    </div>
                </div>
            </div>
        </div>
        <div id="LblEtiqueta">
            <div id="MyEtiquetaDrop" style="width: 6in; height: 4in; border: solid 1px;">
                <table style="margin: auto">
                    <tr>
                        <td>
                            <label style="font-size: 30px">
                                Pick ID</label>
                        </td>
                        <td colspan="4">
                            <img src="~/images/logophoenix_login.jpg" runat="server" id="LblPickIdVal" alt="" hspace="60"
                                vspace="5" style="width: 3in; height: 1in; margin: 0px !important" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-size: 30px">
                                Machine</label>
                        </td>
                        <td style="text-align: center;">
                            <label style="font-size: 30px" id="lblMachine">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-size: 30px">
                                Pallet(s)
                            </label>
                        </td>
                        <td style="text-align: center;">
                            <label style="font-size: 30px" id="LblPalletsValET">
                            </label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <label id="lblMsg">
        </label>
    </form>
    <script>


        var timer;
        function printDiv(divID) {

            var monthNames = [
                "1", "2", "3",
                "4", "5", "6", "7",
                "8", "9", "10",
                "11", "12"
            ];

            //PRINT LOCAL HOUR
            var d = new Date();
            var LbdDate = $("#LblDate");
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


            var mywindow = window.open('', 'PRINT', 'height=400,width=600');

            mywindow.document.write('<html><head><title>' + document.title + '</title>');
            mywindow.document.write('</head><body >');
            //mywindow.document.write('<h1>' + document.title + '</h1>');
            mywindow.document.write(document.getElementById(divID).innerHTML);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/

            mywindow.print();
            mywindow.close();

            return true;
        };

        function addZero(i) {
            if (i < 10) {
                i = "0" + i;
            }
            return i;
        };
        function stoper() {
            clearTimeout(timer);
        }

        function SearchPickIDTimer() {
            stoper();
            timer = setTimeout("SearchPickID()", 2000);
        }

        $(function () {
            IniciarComponentes();
        })

        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "DropPickedMaterialOnTunnel.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: asyncMode != undefined ? asyncMode : true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }

        function IniciarComponentes() {

            txPickID = $('#txPickID');
            lblItemID = $('#lblItemID');
            lblItemDesc = $('#lblItemDesc');
            LblQuantityVal = $('#LblQuantityVal');
            LblQuantityUnit = $('#LblQuantityUnit');
            LblWorkOrderd = $('#LblWorkOrderd');
            LblWorkOrderval = $('#LblWorkOrderval');
            LblMachined = $('#LblMachined');
            LblMachineval = $('#LblMachineval');
            LblMachinedes = $('#LblMachinedes');
            btnDropTagPick = $('#btnDropTagPick');
            DetallePallet = $('#DetallePallet');
            LblEtiqueta = $('#LblEtiqueta');
            lblWorkOrder = $('#lblWorkOrder');
            lblMachine = $('#lblMachine');
            LblPalletsVal = $('#LblPalletsVal');
            LblPalletsValET = $('#LblPalletsValET');
            LblPickIdVal = $('#LblPickId');
            var chkConsigment = document.getElementById("chkConsigment");
        }

        var SearchPickID = function () {
            //var Data = "{'key':'" + value + "'}";
            var Data = chkConsigment.checked == true ? "{'PickID':'" + $('#txPickID').val().toUpperCase() + "','consigment':'true'}" : "{'PickID':'" + $('#txPickID').val().toUpperCase() + "','consigment':'false'}";
            
            WebMethod = "SearchPickID";
            sendAjax(WebMethod, Data, SearchPickIDSuccess, false);
        }

        var ClickDropTagPick = function () {
            //var Data = "{'key':'" + value + "'}";
            var Data = "{'PickID':'" + $('#txPickID').val().toUpperCase() + "'}";
            WebMethod = "ClickDropTagPick";
            sendAjax(WebMethod, Data, ClickDropTagPickSuccess, false);
        }

        function SearchPickIDSuccess(r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                txPickID.addClass("InputIncorrecto");
                txPickID.removeClass("InputCorrecto");
                //                lblDecCustomer.html("");
                //                if (MyObj.TipeMsgJs == "alert") {
                //                    alert(MyObj.ErrorMsg);
                //                }
                //                else if (MyObj.TipeMsgJs == "lbl") {
                ////                    $('#lblMsg').html(MyObj.ErrorMsg);
                //                }
                if (MyObj.TipeMsgJs == "alert") {
                    alert(MyObj.ErrorMsg);
                }
                DetallePallet.hide();
            }
            else {

                txPickID.addClass("InputCorrecto");
                txPickID.removeClass("InputIncorrecto");
                //                lblDecCustomer.html(MyObj.NAMA);
                //                $('#lblMsg').html("");


                lblItemID.html(MyObj.ITEM);
                lblItemDesc.html(MyObj.DSCA);
                LblQuantityVal.html(MyObj.QTYT);
                LblQuantityUnit.html(MyObj.UNIT);
                lblWorkOrder.html(MyObj.ORNO);
                lblMachine.html(MyObj.MCNO);
                LblWorkOrderval.html(MyObj.ORNO);
                LblMachineval.html(MyObj.MCNO);
                LblMachinedes.html(MyObj.DSCAM);
                LblPalletsVal.html(MyObj.PAID);
                LblPalletsValET.html(MyObj.PAID);
                $("#Contenido_LblPickIdVal").attr("src", MyObj.PICK_URL);
                DetallePallet.show();
            }
        }

        function ClickDropTagPickSuccess(r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                if (MyObj.TipeMsgJs == "alert") {
                    alert(MyObj.ErrorMsg);
                }
            }
            else {
                txPickID.removeClass("InputIncorrecto");
                txPickID.removeClass("InputCorrecto");
                DetallePallet.hide();
                txPickID.val("");
                lblItemID.html("");
                lblItemDesc.html("");
                LblQuantityVal.html("");
                LblQuantityUnit.html("");
                LblWorkOrderval.html("");
                LblMachineval.html("");
                LblMachinedes.html("");
                LblPalletsVal.html("");
                LblPalletsValET.html("");
                LblPickIdVal.html("");
                lblItemDesc

                if (MyObj.TipeMsgJs == "alert") {
                    alert(MyObj.SuccessMsg);
                }

                printDiv('LblEtiqueta');

            }
        }
    </script>
</asp:Content>

