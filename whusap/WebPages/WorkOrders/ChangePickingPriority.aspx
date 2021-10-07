<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="ChangePickingPriority.aspx.cs" Inherits="whusap.WebPages.WorkOrders.NewPages.ChangePickingPriority" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="styles/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="styles/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="styles/sweetalert2.css"/>
    <script src="styles/sweetalert2.min.js"></script>
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
        <div id="divPicketPrio" class="col-6 p-0" style="display:none">
            <table id="tblPicketPending" class="table animate__animated animate__fadeInLeft col-10 col-sm-12">
                <thead>
                    <tr>
                        <th scope="col" class="col-4">Pick ID</th>
                        <th scope="col" class="col-4">Work Order</th>
                        <th scope="col" class="col-4">Machine</th>
                        <th scope="col" class="col-4">Priority</th>
                        <th scope="col" class="col-4"></th>
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
                $('#divPicketPrio').fadeOut(100);
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
                $('#btnChangePrio' + index).fadeOut(100);
                $('#inputNum' + index).attr("disabled", true);
                $('#inputNum' + index).val("");
            }
            $('#btnChangePrio' + i).fadeIn(100);
            $('#inputNum' + i).attr("disabled", false);
            $('#inputNum' + i).val("");
            $('#inputNum' + i).focus();
        }

        var SavePrio = function (PICK,i) {
            //alert("{'PRIO':'" + $('#inputNum' + i).val() + "','PICK':'" + PICK + "'}");
            if ($('#inputNum' + i).attr('placeholder').trim() != $('#inputNum' + i).val().trim() && $('#inputNum' + i).val().trim() != "" && parseInt($('#inputNum' + i).val().trim()) >= 0) {
                EventoAjax("SavePrio", "{'PRIO':'" + $('#inputNum' + i).val() + "','PICK':'" + PICK + "'}", SavePrioSuccess);
            }
            else {
                $('#inputNum' + i).focus();
            }
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
                MyObjLst.forEach(function (item, i) {
                    bodyRows += "<tr row = '" + i + "' onmouseenter=showButton(" + i + ") id='rowNum" + i + "' class = 'animate__animated animate__fadeInLeft' style='display:none'><td>" + item.PICK + "</td><td>" + item.ORNO + "</td><td>" + item.MCNO + "</td><td><input id='inputNum" + i + "' class='form-control' type = 'number' value='" + item.PRIO + "' placeholder='" + item.PRIO + "' style='width: 100px;' disabled/></td><td><button class='btn btn-success col-12 btn-sm' type='button' style='display:none; height:33px; width: 100px;' id='btnChangePrio" + i + "' onclick= SavePrio('" + item.PICK.trim() + "','" + i + "')>Change</button></td></tr>";
                    ig = i;
                });   
            }
            else {
                $('#divPicketPrio').fadeOut(100);
            }

            $("#bdPicketPending").append(bodyRows);
            for (var index = 0; index <= ig; index++) {
                if (MyObjLst.length > 0) {
                    $('#divPicketPrio').fadeIn(100);
                    $('#rowNum' + index).fadeIn(1000);
                }
            }
        }
        LoadControls();
        GetMachine();
    </script>
</asp:Content>
