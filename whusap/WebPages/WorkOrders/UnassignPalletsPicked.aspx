<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="UnassignPalletsPicked.aspx.cs" Inherits="whusap.WebPages.WorkOrders.UnassignPalletsPicked" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="styles/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="styles/jquery.dataTables.min.js"></script>
    <style>
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

        #lblMsg {
            display: none;
            font-size: 14px;
            color: Red;
        }

        #txPickID {
            width: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <form id="form1" class="container">
        <div id="divForm">
            <div class="form-group row ">
                <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="lblpickID">
                    Pick ID
                </label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="txPickID"
                        placeholder="Pick ID" onkeyup="SearchPickIDTimer()" />
                </div>
            </div>
            <div id="divTables" class="col-7">
                <div id="divTableItem" class="col-12">
                </div>
                <br />
            </div>
            <div id="DetallePallet">
                <div class="form-group row ">
                    <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="lblItem">
                        Item
                    </label>
                    <div class="col-sm-3">
                        <label class="col-sm-12 col-form-label-lg" for="txCustomer" id="lblItemID">
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <label class="col-sm-12 col-form-label-lg" for="txCustomer" id="lblItemDesc">
                        </label>
                    </div>
                </div>
                <div class="form-group row ">
                    <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="lblQuantity">
                        Quantity
                    </label>
                    <div class="col-sm-3">
                        <label class="col-sm-12 col-form-label-lg" id="LblQuantityVal">
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <label class="col-sm-12 col-form-label-lg" id="LblQuantityUnit">
                        </label>
                    </div>
                </div>
                <div class="form-group row ">
                    <label class="col-sm-2 col-form-label-lg" id="Label1">
                        Work Order
                    </label>
                    <div class="col-sm-3">
                        <label class="col-sm-12 col-form-label-lg" id="lblWorkOrder">
                        </label>
                    </div>
                </div>
                <div class="form-group row ">
                    <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="Label4">
                        Machine
                    </label>
                    <div class="col-sm-3">
                        <label class="col-sm-12 col-form-label-lg" id="lblMachine">
                        </label>
                    </div>
                    <div class="col-sm-3">
                        <label class="col-sm-12 col-form-label-lg" id="lblDMachine">
                        </label>
                    </div>
                </div>
                <div class="form-group row" id="divQueryAction">
                    <div class="col-sm-2">
                    </div>
                    <div class="col-sm-4">
                        <input type="button" class="btn btn-primary btn-lg col-sm-7 " id="btnDropTagPick"
                            value="Pick Material" onclick="ClickDropTagPick()" />
                    </div>
                </div>
            </div>
        </div>
        <br />
        <label id="lblMsg">
        </label>
    </form>
    <script>
        var timer;
        var ajaxSearchPickID = null;

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
                url: "UnassignPalletsPicked.aspx/" + WebMethod,
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
            btnDropTagPick = $('#btnDropTagPick');
            DetallePallet = $('#DetallePallet');

            lblWorkOrder = $('#lblWorkOrder');
            lblMachine = $('#lblMachine');
            lblDMachine = $('#lblDMachine');
            lblMsg = $('#lblMsg');
        }

        var SearchPickID = function () {
            //var Data = "{'key':'" + value + "'}";
            var Data = "{'PickID':'" + $('#txPickID').val().toUpperCase() + "'}";
            WebMethod = "SearchPickID";
            sendAjax(WebMethod, Data, SearchPickIDSuccess, false);
        }

        var designar = function (pick) {
            var Data = "{'PickID':'" + $('#txPickID').val().toUpperCase() + "'}";
            WebMethod = "designar";
            sendAjax(WebMethod, Data, designarSuccess, false);
        }

        var designarSuccess = function () {
            alert("wwwwww");
        }

        function SearchPickIDSuccess(response) {
            //divTableItem.innerHTML = '';
            var bodyRows = "";
            myObj = JSON.parse(response.d);
            if (myObj.length > 0) {
                for (var i = 0; i < myObj.length; i++) {
                    bodyRows += "<tr id='rowNum" + i + "'><td>" + myObj[i].PAID + "</td><td>" + myObj[i].LOCA + "</td><td>" + myObj[i].ITEM + "</td><td>" + myObj[i].DSCA + "</td><td>" + myObj[i].QTYT + "</td><td>" + myObj[i].UNIT + "</td></tr>";
                }
                var tableOptions = "<table id ='tblItems' class='table animate__animated animate__fadeIn' style='width:100% display:none'>" +
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
                                            bodyRows +
                                           "<tr><button type='button' class='btn btn-primary' onclick='designar(" + $('#txPickID').val().toUpperCase() + ")'>Designar</button></tr>" +
                                           "</tbody>" +
                                        "</table>";


                $("#divTableItem").append(tableOptions);
            }
            else {
                alert(myObj.ErrorMsg);
            }
            //},
            //failure: function (response) {
            //    alert(response.d);
            //}
            //});
        }

        var ClickDropTagPick = function () {
            //var Data = "{'key':'" + value + "'}";
            var Data = "{'PickID':'" + $('#txPickID').val() + "'}";
            WebMethod = "ClickDropTagPick";
            sendAjax(WebMethod, Data, ClickDropTagPickSuccess, false);
        }

        function ClickDropTagPickSuccess(r) {
            //FlblMsg.hide(500);
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                if (MyObj.TipeMsgJs == "alert") {
                    alert(MyObj.ErrorMsg);
                }
            }
            else {
                txPickID.removeClass("InputIncorrecto");
                txPickID.removeClass("InputCorrecto");
                txPickID.val("");
                lblItemID.html("");
                lblItemDesc.html("");
                LblQuantityVal.html("");
                LblQuantityUnit.html("");

                if (MyObj.TipeMsgJs == "alert") {
                    alert(MyObj.SuccessMsg);
                }
                DetallePallet.hide();
            }
        }
    </script>
</asp:Content>
