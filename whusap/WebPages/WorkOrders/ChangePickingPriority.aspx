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
                                    <label for="ddMcno">Machine</label>
                                </div>
                            </div>

                            <select id="ddMcno" class="form-control">
                                <option value="0" selected>Select Machine</option>
                            </select>
                        </div>
                    </div>
                    <br>
                </form>
            </div>
        </div>
        <hr />
        <br />
        <div id="divPicketPrio" class="col-6">
            <table id="tblPicketPending" class="table animate__animated animate__fadeInLeft col-12 col-sm-12">
                <thead>
                    <tr>
                        <th scope="col">Pick ID</th>
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
        var ig;
        var EventoAjax = function (Method, Data, MethodSuccess) {
            $.ajax({
                type: "POST",
                url: "ChangePickingPriority.aspx/" + Method.trim(),
                data: Data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: MethodSuccess
            })
        };

        var LoadControls = function () {
            var ddMcno = document.getElementById("ddMcno");
            ddMcno.addEventListener("change", loadPicks);
            var bdPicketPending = document.getElementById("bdPicketPending");
        }

        var GetMachine = function () {
            EventoAjax("GetMachine", "{}", SucceessGetMachine);
        }

        var SucceessGetMachine = function (r) {
            var MylistMachines = JSON.parse(r.d);
            MylistMachines.forEach(function (e) {
                var option = document.createElement("option");
                option.text = e.MCNO + " - " + e.DSCA;
                option.value = e.MCNO
                ddMcno.add(option);
            });
        }

        var loadPicks = function () {
            if (ddMcno.value == "0") {
                if (bdPicketPending.childElementCount > 0) {
                    for (let i = bdPicketPending.childElementCount - 1; i >= 0 ; i--) {
                        bdPicketPending.children[i].remove()
                    }
                }
            }
            else {
                if (bdPicketPending.childElementCount > 0) {
                    for (let i = bdPicketPending.childElementCount - 1; i >= 0 ; i--) {
                        bdPicketPending.children[i].remove()
                    }
                }
                EventoAjax("GetPicks", "{'MCNO':'" + ddMcno.value + "'}", loadPicksSuccess);
            }
        }
        var showButton = function (i) {
            for(var index = 0; index <= ig; index++){
                $('#btnChangePrio' + index).hide(100);
            }
            $('#btnChangePrio' + i).show(100);
        }

        var SavePrio = function (PICK,i) {
            //alert("{'PRIO':'" + $('#inputNum' + i).val() + "','PICK':'" + PICK + "'}");
            EventoAjax("SavePrio", "{'PRIO':'" + $('#inputNum' + i).val() + "','PICK':'" + PICK + "'}", SavePrioSuccess);
        }
        var SavePrioSuccess =  function(){
            loadPicks();
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
                    bodyRows += "<tr row = '" + i + "' onmouseenter=showButton(" + i + ") id='rowNum" + i + "' class = 'animate__animated animate__fadeInLeft'><td>" + item.PICK + "</td><td><input id='inputNum" + i + "' type = 'number' value='" + item.PRIO + "'/></td><td><button class='btn btn-primary btn-sm' type='button' style='display:none; height:26px' id='btnChangePrio" + i + "' onclick= SavePrio('" + item.PICK + "','" + i + "')>Chage</button></td></tr>";
                    ig = i;
                });
            }
            else {
                $('#divPicketPending').hide(100);
            }
            $("#bdPicketPending").append(bodyRows);
        }
        LoadControls();
        GetMachine();
    </script>
</asp:Content>
