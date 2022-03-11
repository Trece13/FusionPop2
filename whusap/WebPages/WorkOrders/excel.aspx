<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="excel.aspx.cs" Inherits="whusap.WebPages.WorkOrders.excel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style>
        .fileUpload {
            position: relative;
            overflow: hidden;
            margin: 10px;
        }

            .fileUpload input.upload {
                position: absolute;
                top: 0;
                right: 0;
                margin: 0;
                padding: 0;
                font-size: 20px;
                cursor: pointer;
                opacity: 0;
                filter: alpha(opacity=0);
            }

        #tableErrors {
            display: none;
            margin-bottom: 200px;
        }
    </style>
    <div class="col-sm-10 form-inline">
        <label class="col-sm-2 col-form-label-lg" for="txIWrh">
            Warehouse</label>
        <div class="col-2 p-0">
            <input type="text" class="col-10 p-0" id="txWrh" placeholder="Warehouse">            
        </div>
        <div class="col-3 p-0">
            <asp:Button  class="btn btn-primary btn-lg" ID="btnUpdate" runat="server" Text="Clean Pallets POP" />
        </div>
        <div class="form-group row" for="txIWrh">
            <label id="lblError"></label>
        </div>
    </div>
    <div class="container">
        <input id="uploadFile" placeholder="File Name here" disabled="disabled" class="col-12 p-0" style="border-radius: 5px; height: 44px" />
        <br />
        <div class="fileUpload btn btn-primary col-12 m-0">
            <span id="titleLoad">Select .cvs</span>
            <input id="file" type="file" class="upload col-12" />
        </div>
        <br>
        <br>
        <table class="table" id="tableErrors">
            <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Pallet ID whit error</th>
                </tr>
            </thead>
            <tbody id="tbody">
            </tbody>
        </table>
    </div>
    <script>
        btnUpdate = $('#Contenido_btnUpdate');

        dot = "."
        var totalreg = 0;
        document.getElementById("file").onInput = function () {
            document.getElementById("uploadFile").value = this.value;
        };

        function EventoAjax(Method, Data, MethodSuccess) {
            $.ajax({
                type: "POST",
                url: "excel.aspx/" + Method.trim(),
                data: Data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: MethodSuccess
            })
        };

        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "excel.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        };

        function parseCSV(text) {
            // Obtenemos las lineas del texto
            let lines = text.replace(/\r/g, '').split('\n');
            //return lines.map(function(line) {
            //    // Por cada linea obtenemos los valores
            //    let values = line.split(',');
            return lines;
            //});
        }

        function reverseMatrix(matrix) {
            let output = [];
            // Por cada fila
            matrix.forEach(function (values, row) {
                // Vemos los valores y su posicion
                values.forEach(function (value, col) {
                    // Si la posición aún no fue creada
                    if (output[col] === undefined) output[col] = [];
                    output[col][row] = value;
                });
            });
            return output;
        }
        function loader() {
            var strload = "Loading";
            if (dot == "...") {
                dot = ".";
            }
            else {
                dot += "."
            }
            $('#titleLoad').html(strload + dot)


        }
        function readFile(evt) {
            refreshIntervalId = setInterval(loader, 200);
            dataSend = '';
            let file = evt.target.files[0];
            let reader = new FileReader();
            reader.onload = function (e) {
                // Cuando el archivo se terminó de cargar
                let lines = parseCSV(e.target.result);
                //let data = reverseMatrix(lines);
                let data = lines;
                var ciclebegin = 1;
                var cicles = 5;
                var rowsParcial = parseInt(data.length / cicles)
                var resto = data.length - rowsParcial * cicles;
                totalreg = data.length
                for (let i = 0; i < data.length; i++) {
                    if (i > 0) {
                        dataSend += data[i] + ";";
                    }
                }
                document.getElementById('file').value = "";
                EventoAjax("Receipt_Data", "{'Data':'" + dataSend + "'}", resp)
                console.log(data);
            };
            // Leemos el contenido del archivo seleccionado
            reader.readAsBinaryString(file);
        }


        document.getElementById('file').addEventListener('change', readFile, false);
        function resp(r) {
            
            clearInterval(refreshIntervalId);
            $('#titleLoad').html("Select .cvs")
            if (r.d == "") {
                $('#tableErrors').fadeOut(100);
                $("#tbody tr").remove();
                document.getElementById("uploadFile").value = "";
                Swal.fire(
                'Good job!',
                'All records have been saved successfully!',
                'success'
                )


            }
            else {
                $("#tbody tr").remove();
                let errorsNum = 0;
                let errors = r.d.split(';')
                for (var i = 0; i < errors.length; i++) {
                    if (errors[i].trim() != "") {
                        errorsNum++;
                        $('#tbody').append('<tr id=""><td>' + i + '</td><td style="color:red">' + errors[i] + '</td></tr>');
                    }
                }
                $('#tableErrors').fadeIn(100);
                document.getElementById("uploadFile").value = "";
                Swal.fire(
                'Warning!',
                'Some records were not saved! saved: not saved:' + errorsNum,
                'warning'
                )
            }
        }
        //JC 280122 Limpiar los datos de cantidad y estado para la bodega seleccionada
        btnUpdate.bind('click', function () {
            Click_Update();
        });

        var Click_Update = function () {
            var Data = "{'WARE':'" + $('#txWrh').val().trim() + "'}";
            sendAjax("Click_Update", Data, SuccesClick_Update, true)
            //EventoAjax("Click_Update", Data, SuccesClick_Update)
       };

        var SuccesClick_Update = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                alert("Data not Updated");
            }
            if (MyObj.Error == false) {
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.SuccessMsg);
                alert("Data Updated");
                }
            };

        function ImprimirMensaje(type, msg) {
            switch (type) {
                case "alert":
                    alert(msg);
                    break;
                case "console":
                    console.log(msg);
                    break;
                case "label":
                    $('#lblError').html(msg);
                    break;
            }
        };
    </script>
</asp:Content>
