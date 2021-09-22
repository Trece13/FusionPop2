<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whBarcodePassword.aspx.cs" Inherits="whusap.WebPages.Login.whBarcodePassword" Theme="Cuadriculas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <link href="<%= String.Concat(ConfigurationManager.AppSettings["UrlBase"], "/Styles/Site.css") %> " rel="stylesheet" type="text/css" />
    <style type="text/css">
        #Cuerpo {
            background-color: white !important;
        }

        .style7 {
            width: 266px;
        }

        .style8 {
            width: 306px;
        }
    </style>
    <style type="text/css" media="print">
        @page {
            size: auto; /* auto is the initial value */
            margin: 0; /* this affects the margin in the printer settings */
        }

        @media print {
            a[href]:after {
                content: none !important;
            }
        }
    </style>
    <script type="text/javascript">
        function printContent(printpage) {
            
            var mywindow = window.open('', 'PRINT', 'height=400px,width=600px');
            mywindow.document.write('<html><head>');
            mywindow.document.write('</head><body>');
            mywindow.document.write(document.getElementById('uno').innerHTML);
            mywindow.document.write('<link rel="stylesheet" href="styles/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">');
            mywindow.document.write('<link rel="stylesheet" href="styleLabel.css">');
            mywindow.document.write('</body></html>');
            mywindow.focus(); // necessary for IE >= 10*/
            setTimeout(function () {
                mywindow.print();
            }, 3000);
            mywindow.document.close(); // necessary for IE >= 10
        }
        var _idioma = '<%= _idioma %>';
        $(document).ready(function () {
            $('#Password').val('');
            $('#UserName').val('');

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <%--<div style="border-style:solid; border-width: 0px; background-color:#ffffff; height: 1px; margin-right:auto;" />--%>
    <div runat="server" id="ContentMainMenu" visible="true">
        <table style="width: 100%;">

            <tr>
                <td class="style7">&nbsp;</td>
                <td class="style8" align="center">

                    <p style="font-family: tahoma; font-size: 14px">
                    </p>
                    <p style="font-family: tahoma; font-size: 14px">
                        <asp:Label ID="lblErrorMessage1" runat="server" Visible="false" Style="color: Red;" />
                    </p>
                    <fieldset class="login">
                        <legend align="left">
                            <asp:Label ID="lblIngreso" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:DropDownList runat="server" ID="ddlIdioma1" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_OnSelectedIndexChanged">
                                <asp:ListItem Text="English" Value="INGLES" />
                                <asp:ListItem Text="Español" Value="ESPAÑOL" />
                            </asp:DropDownList></legend>
                        <p>
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" AutoPostBack="true"
                                Font-Size="X-Large">Username:</asp:Label>
                            <asp:TextBox ID="UserName" runat="server" CssClass="TextBox form-control form-control-sm"
                                Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                ValidationGroup="UserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p align="center">
                            <asp:Label ID="LblUserNameExist" Visible="false" runat="server"></asp:Label>
                        </p>

                        <p align="center">
                            <asp:Label ID="LblUserNameDontExist" Visible="false" runat="server" ForeColor="Red"></asp:Label>
                        </p>
                        <br />
                        <span class="failureNotification">
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        </span>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="failureNotification"
                            ValidationGroup="UserValidationGroup" />
                    </fieldset>
                    <p class="submitButton">
                        <asp:Button ID="btnValidate" CssClass="ButtonsSendSave" Width="60px"
                            Height="20px" runat="server" Text="Query" Visible="true"
                            ValidationGroup="UserValidationGroup" OnClick="btnVerificate_Click" />
                    </p>
                    <p class="submitButton">
                        <asp:Button ID="btnGeneratePassword" CssClass="ButtonsSendSave" Width="120px"
                            Height="20px" runat="server" Text="Query" Visible="false"
                            ValidationGroup="UserValidationGroup" OnClick="btnGeneratePassword_Click" />

                    </p>
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="uno" align="center">

                                <p align="center">
                                    <img src="" runat="server" id="imgUniqueUserName" alt="" />
                                </p>
                                <p align="center">
                                    <img src="" runat="server" id="imgUniquePassword" alt=""/>
                                </p>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="style7">&nbsp;</td>
            </tr>
        </table>
    </div>
    <%-- <asp:ScriptManager runat="server"></asp:ScriptManager>--%>
</asp:Content>
