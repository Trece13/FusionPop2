<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="ChangePickingPriority.aspx.cs" Inherits="whusap.WebPages.WorkOrders.NewPages.ChangePickingPriority" %>
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
    <div class="container-fluid col-12 animate__animated animate__fadeInLeft" style="margin-bottom: 200px">
        <div class="row" id="formWarehouse">
            <div class="col-6">
                <form>
                    <div class="row">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-6">
                                    <label for="ddMachine">Machine</label>
                                </div>
                            </div>

                            <select id="ddWare" class="form-control">
                                <option value="0" selected>Select Machine</option>
                            </select>
                        </div>
                    </div>
                    <br>
                    <div id="divStartPicking" class="row">
                        <div class="col-6">
                            <button class="btn btn-primary col-12 btn-sm" id="btnStarPicking" type="button">Start Picking</button>
                        </div>
                    </div>
                </form>
            </div>
        </div>
        <hr />
        <br />
        <div id="divPicketPrio" class="col-12">
            <table id="tblPicketPending" class="table animate__animated animate__fadeInLeft" style="width: 100%">
                <thead>
                    <tr>
                        <th scope="col">Pick ID</th>
                        <th scope="col">Work Order</th>
                        <th scope="col">Requested On</th>
                        <th scope="col">Priority</th>
                        <th scope="col"></th>
                    </tr>
                </thead>
                <tbody id="bdPicketPending">
                </tbody>
            </table>
        </div>
    </div>
    <script>
        var LoadControls = function () {

        }

        var loadPicks = function () {
            if (ddWare.value == "0") {
                $("#btnStarPicking").hide(100);
                $('#divPicketPending').hide(100);
                $("#formPicking").hide(100);
                divTableWarehouse.innerHTML = '';
                divTableItem.innerHTML = '';
            }
            else {
                $("#formPicking").hide(100);
                divTableWarehouse.innerHTML = '';
                divTableItem.innerHTML = '';
                EventoAjax("loadPicksPending", "{'CWAR':'" + ddWare.value + "'}", loadPicksSuccess);
            }
        }
        var loadPicksSuccess = function (r) {

            var MyObjLst = JSON.parse(r.d);
            if (bdPicketPending.childElementCount > 0) {
                for (let i = bdPicketPending.childElementCount - 1; i >= 0 ; i--) {
                    bdPicketPending.children[i].remove()
                }
            }
            $("#btnStarPicking").show(100);
            var bodyRows = "";
            if (MyObjLst.length > 0) {
                $('#divPicketPending').show(100);
                MyObjLst.forEach(function (item, i) {

                    if (item.T$STAT == 1) {
                        bodyRows += "<tr onClick='selectPicksPending(this)' row = '" + i + "' id='rowNum" + i + "' class = 'animate__animated animate__fadeInLeft'><td>" + item.T$PAID + "</td><td>" + item.T$CWAR + "</td><td><button class='btn btn-primary col-12 btn-sm' type='button' id='btnPickingPending" + i + "'>Take</button></td>";
                    }
                    else {
                        bodyRows += "<tr onClick='selectPicksPending(this)' row = '" + i + "' id='rowNum" + i + "' class = 'animate__animated animate__fadeInLeft'><td>" + item.T$PAID + "</td><td>" + item.T$CWAR + "</td><td><button class='btn btn-primary col-12 btn-sm' type='button' id='btnPickingPending" + i + "'>Take</button></td>";
                    }
                });
            }
            else {
                $('#divPicketPending').hide(100);
            }
            $("#bdPicketPending").append(bodyRows);
        }
    </script>
</asp:Content>
