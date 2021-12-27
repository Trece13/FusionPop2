<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="excel.aspx.cs" Inherits="whusap.WebPages.WorkOrders.excel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <input type="file" id="file" class="col-6 border" />
    <label id="lblError" style="color:red;font-size:12px"></label>
    <script>

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

        function readFile(evt) {
            dataSend = '';
            let file = evt.target.files[0];
            let reader = new FileReader();
            reader.onload = function (e) {
                // Cuando el archivo se terminó de cargar
                let lines = parseCSV(e.target.result);
                //let data = reverseMatrix(lines);
                let data = lines;
                for (let i = 0; i < data.length; i++) {
                    if (i > 0) {
                        dataSend += data[i] + ";";
                    }
                }
                EventoAjax("Receipt_Data", "{'Data':'" + dataSend + "'}", resp)
                console.log(data);
            };
            // Leemos el contenido del archivo seleccionado
            reader.readAsBinaryString(file);
        }


        document.getElementById('file').addEventListener('change', readFile, false);
        function resp(r) {
            if (r.d != "") {
                Swal.fire(
                'Good job!',
                'All records have been saved successfully!',
                'success'
                )
            }
            else {
                Swal.fire(
                'Warnong!',
                'Some records were not saved!',
                'warning'
                )

            }
        }

    </script>
</asp:Content>
