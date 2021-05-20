<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="whInvReprintMrbRejection.aspx.cs" Inherits="whusap.WebPages.Migration.whInvReprintMrbRejection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style>
        #LblUnitOC
        {
            font-size:14px;
        }
        #lblError
        {
            font-size:14px;
            color:Red;
        }
        
        label
        {
            font-size:14px;
        }
    </style>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txPaid">
            Paid</label>
        <div class="col-sm-4">
            <input type="text" class="form-control form-control-lg" id="txPaid" placeholder="Paid">
        </div>
    </div>
    <div class="form-group row">
        <input id="btnPrint" type="button" class="btn btn-primary btn-lg" value="Print" />
    </div>
    <div class="form-group row">
        <label id="lblError">
        </label>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container">
                <iframe id="myLabelFrame" scrolling="no" title="Inline Frame Example" class ="col-12" style="height: 470px; overflow: hidden; margin-bottom: 100px;" frameborder="0" src=""></iframe>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
        
        var timer;
        function stoper() {

            clearTimeout(timer);
        }


        function IniciarComponentes() {

            txPaid = $('#txPaid');

            btnPrint = $('#btnPrint');

        };

        IniciarComponentes();


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
        }
        var SuccesClick_Print = function (r) {
            MyObject = JSON.parse(r.d);

            if (MyObject.Error == false) {
                //Etiqueta Sin orden de compra

                $('#txPaid').val("");

                myLabelFrame = document.getElementById('myLabelFrame'); 
                myLabelFrame.src = '../Labels/RedesingLabels/' + MyObject.SuccessMsg;

            }
            else {
                alert(MyObject.ErrorMsg);
                $('#txPaid').val("");
            }

        }


        var Click_Print = function () {

            var Data = "{'PAID':'" + $('#txPaid').val().trim()+"'}";
            sendAjax("Click_Print", Data, SuccesClick_Print);

        }

        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "whInvReprintMrbRejection.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }

        btnPrint.bind('click', function () {
            Click_Print();
        });

    </script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"
        integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1"
        crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"
        integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM"
        crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.1.1.min.js"></script>
</asp:Content>
