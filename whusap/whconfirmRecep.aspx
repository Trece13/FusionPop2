<%@ Page aspcompat=true debug=true%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must authenticate before you can execute this option."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
'if InStr(Session("opciones"),"CONFI")=0 then
'	Response.Redirect("wh" & Session("cia") & "Menu" & Session("envt") & ".aspx?flg=Y")
'End if
Dim strSQL, Odbcon, objrs
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
	strSQL = "insert into baan.ttccol301" & Session("env") & " (t$user,t$fein,t$come,t$refcntd,t$refcntu) values('" & Session("user") & "',sysdate+5/24,'Confirmar Recepcion',0,0)"
	objrs = Server.CreateObject("ADODB.Recordset")
	objrs.Open (strSQL, Odbcon)
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->

<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>

<script type="text/javascript">

    function CheckLength() {
        var regex = /^-?\d*[0-9]*[.]?[0-9]*$/;
        var re = new RegExp(regex);
        var lenght = document.getElementById("txtorden").value.length;
        var str = document.getElementById("txtorden").value;
        var strs = str.substr(9, 1);
        var strp = str.substr(10, 3);
        if (strp.match(re)) {
            if (document.getElementById("txtorden").value != "") {
                if (document.getElementById("txtorden").value.length < 13 || document.getElementById("txtorden").value.length > 13) {
                    alert("Please use this format WORKORDER-PALLETID, remember only 13 characters")
                    document.getElementById("txtorden").focus();
                    document.getElementById("txtorden").value = "";
                    return false;
                }
                else {
                    if (strs != "-") {
                        alert("Please use this format WORKORDER-PALLETID, remember 9 characters workorder, simbol minus, 3 characters pallet id");
                        document.getElementById("txtorden").focus();
                        document.getElementById("txtorden").value = "";
                        return false;
                    }
                }
            }
        }
        else {
            document.getElementById("txtorden").focus();
            document.getElementById("txtorden").value = "";
            alert("Only numbers allowed on pallet id");
        }
    };
</script>
<style type="text/css">
    .errorMsg
    {
        color: Black;
        font-weight: bold;
        font-size: medium;
    }
</style>
</head>
<%
Dim strord, strlen, strflag, strmsg  

strflag = Request.QueryString("flag")
if strflag = "Y" then
  strord = Session("strord")
  strmsg = Session("strmsg")
  strord = ""
else
  strord = ""
  Session("strmsg") = ""
  strmsg  = ""
end if

%>
<meta name="viewport" content="width=300, user-scalable=no">
<body  bgcolor="#87CEEB" >
<form name="frmord" method="post" action="whconfirmRecep2.aspx">
<table>
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H2>Confirm Receipt</H2></td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<table align="left" class="tableDefault4" width="30%" border="0" cellspacing="0" cellpadding="0">
<tr>
<td class="titulog2"><b>User </b>:</td>
<td class="titulog2" colspan="2"><b><%=Session("user")%></b></td>
</tr>
<tr>
<td class="titulog2"><b>Name </b>:</td>
<td class="titulog2" colspan="2"><b><%=Session("username")%></b></td>
</tr>
<tr> 
     <td class="titulog2" colspan="2"><b>Production Order No.</b></td>
     <td><input type="text" id="txtorden" name="txtorden" size="15" onblur="CheckLength();" value=<%=strord%>></td>
</tr>
<tr> 
<td class="errorMsg" colspan="2" rowspan="2"><%=strmsg%></td>
</tr> 
<tr> 
</tr> 
<tr> 
</tr> 
    <td colspan="4" align="center">
      <input type="submit" name="btnLogin" value="  OK  ">
    </td>
</tr>
</table>
</form>
</body>
</html>
