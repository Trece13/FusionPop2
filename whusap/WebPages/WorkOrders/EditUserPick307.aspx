<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="EditUserPick307.aspx.cs" Inherits="whusap.WebPages.WorkOrders.EditUserPick307" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="styles/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="styles/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="styles/font-awesome.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style>
        #DivDetalle,#BtnSave,#BtnClean
        {
            display:none;
        }
        
        .Incorrecto
        {
            color: Red;
        }
        .Correcto
        {
            color:Green;
        }
        
        #lblMsg
        {
            font-size:14px;
        }
    </style>
    <script>
        var timer;
        lstEnviar = [];
        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "EditUserPick307.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: asyncMode != undefined ? asyncMode : true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }

        function stoper() {
            clearTimeout(timer);
        }

        function myFunction() {
            stoper();
            timer = setTimeout("EnviarCustomer()", 500);
        }

        var GetPicks = function () {
            for (let i = ddPick.options.length; i > 0; i--) {
                ddPick.remove(i);
            }
            EventoAjax("ConsultarTtccol307", "{'PAID':''}", SucceessGetPicks);
        }

        var SucceessGetPicks = function (r) {
            var MylistPicks = JSON.parse(r.d);
            MylistPicks.forEach(function (e) {
                var option = document.createElement("option");
                option.text = "Pick No: " + e.T$PAID + " Warehouse: " + e.T$CWAR
                option.value = e.T$PAID.trim()
                ddPick.add(option);
            });
            $('#BtnSave').show();
        }

                var SuccessGetWarehouse = function () {

        }

        var SuccessGetPicks = function () {

        }

        $(function () {
            GetPicks();
        });

         var loadPage = function () {
            skip = false;
            EventoAjax("loadPage", "{'CWAR':'" + ddWare.value + "'}", LoadPageSuccess);
        }

        var LoadPageSuccess = function (r) {
            ddWare.value = 0;
            var MyObj = r.d;
            if (MyObj.error == false) {
            }
        }

        var EventoAjax = function (Method, Data, MethodSuccess) {
            $.ajax({
                type: "POST",
                url: "EditUserPick307.aspx/" + Method.trim(),
                data: Data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: MethodSuccess
            })
        };
    </script>
    <form id="form1" class="container">
    <div id="divForm">
        <div class="form-group row ">
            <label class="col-sm-2 col-form-label-lg">
                Pick Id
            </label>
            <select id="ddPick" class="form-control col-6">
                <option value="0" selected>Select Pick Blocked</option>
            </select>
<%--            <div class="col-sm-4">
                <input type="text" class="form-control form-control-lg col-sm-10" id="txPalletId"
                    placeholder="Pallet ID" />
                    <button class="btn btn-lg col-sm-1,5" id="BtnClean" value="Clean"  onclick="LimpiarFormulario(); return false;"><i class="fa fa-trash"></i></button>
            </div>--%>
        </div>
    </div>
    <div id="DivDetalle">
        <div class="form-group row">
            <label class="col-sm-2 col-form-label-lg" for="lblState" id="Label1">
                State
            </label>
<%--            <label class="col-sm-2 col-form-label-lg" id="lblState">
                ----
            </label>--%>
            <div class="col-sm-2">
                <input type="text" class="form-control form-control-lg col-sm-12" id="txtStatus" placeholder="Status" />
            </div>
        </div>
        <div class="form-group row">
            <label class="col-sm-2 col-form-label-lg" for="txPalletId" id="Label3">
                User
            </label>
            <div class="col-sm-2">
                <input type="text" class="form-control form-control-lg col-sm-12" id="txtUser" placeholder="User" />
            </div>
        </div>
    </div>
    <div id="DivBotones">
        <div class="form-group row">
            <div class="col-sm-2">
                
            </div>
            <div class="col-sm-4">
                <input type="button" class="btn btn-primary btn-lg col-sm-7" id="BtnSave" value="Save" />
<%--                <input type="button" class="btn btn-primary btn-lg col-sm-7" id="BtnQuery" value="Query" />--%>
            </div>
        </div>
    </div>
    <br />
    <label id="lblMsg" />
    <br />
    </form>
    <script>

        function iniciarComponentes() {

            //txPalletId = $('#txPalletId');
            var ddPick = $('#ddPick');
            lblState = $('#lblState');
            txtStatus = $('#txtStatus');
            txtUser = $('#txtUser');
            //BtnQuery = $('#BtnQuery');
            BtnSave = $('#BtnSave');
            lblMsg = $('#lblMsg');
            DivDetalle = $('#DivDetalle');
            BtnClean = $('#BtnClean');

        }
        iniciarComponentes();

//        var ConsultarTtccol307 = function () {
//            //var Data = "{'ORNO':'" + OrdenID + "','PONO':'" + Position + "','SEQNR':'" + SEQNR + "'}";
//            var Data = "{'PAID':'" + txPalletId.val().trim() + "'}";
//            WebMethod = "ConsultarTtccol307";
//            sendAjax(WebMethod, Data, ConsultarTtccol307Success, false);
//        }

        var ActualizarUsuarioTtccol307 = function () {
            //var Data = "{'ORNO':'" + OrdenID + "','PONO':'" + Position + "','SEQNR':'" + SEQNR + "'}";
            //var Data = "{'PAID':'" + txPalletId.val() + "','USER':'" + txtUser.val() + "','USERO':'" + MyOBj.USRR + "'}";
            var Data = "{'PICK':'" + ddPick.value + "'}";
            WebMethod = "ActualizarUsuarioTtccol307";
            sendAjax(WebMethod, Data, ActualizarUsuarioTtccol307Success, false);
        }

        //BtnQuery.click(function () { ConsultarTtccol307(); });
        BtnSave.click(function () { ActualizarUsuarioTtccol307() });

        function LimpiarFormulario() {
            lblMsg.html("");
            DivDetalle.hide(1000);
            //BtnQuery.show(500);
            BtnSave.hide(500);
            lblMsg.removeClass("Correcto");
            lblMsg.removeClass("Incorrecto");
            txPalletId.val("");
            BtnClean.hide(1000);
            txPalletId.attr("disabled", false);
        }
//        var ConsultarTtccol307Success = function (res) {
//            MyOBj = JSON.parse(res.d);
//            if (MyOBj.Error) {
//                lblMsg.html(MyOBj.ErrorMsg);
//                DivDetalle.hide(1000);
//                BtnSave.hide(500);
//                BtnQuery.show(500);
//                lblMsg.addClass("Incorrecto");
//                lblMsg.removeClass("Correcto");
//            }
//            else if (!MyOBj.Error) {
//                DivDetalle.show(1000);
//                BtnSave.show(500);
//                BtnQuery.hide(500);
//                lblState.html(MyOBj.STAT);
//                txtUser.val(MyOBj.USRR);
//                txtStatus.val(MyOBj.STAT);
//                lblMsg.addClass("Correcto");
//                lblMsg.removeClass("Incorrecto");
//                BtnClean.show(500);
//                txPalletId.attr("disabled", true);
//            }
//        };
        var ActualizarUsuarioTtccol307Success = function (res) {
            MyOBj = JSON.parse(res.d);
            if (MyOBj.Error) {
                lblMsg.html(MyOBj.ErrorMsg);
                //DivDetalle.show(1000);
                //BtnQuery.hide(500);
                BtnSave.show(500);
                lblMsg.addClass("Incorrecto");
                lblMsg.removeClass("Correcto");
                //BtnClean.show(500);
            }
            else if (!MyOBj.Error) {
                lblMsg.html(MyOBj.SuccessMsg);
                //DivDetalle.hide(1000);
                //BtnQuery.show(500);
                BtnSave.hide(500);
                lblMsg.addClass("Correcto");
                lblMsg.removeClass("Incorrecto");
                //BtnClean.hide(500);
                //txPalletId.attr("disabled", false);
                //txPalletId.val("");
                GetPicks();
            }
        };
    </script>
    </label>
</asp:Content>
