<%@ Page aspcompat=true debug=true%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must login firts, before use this session."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
Dim strSQL, Odbcon, objrs
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
	strSQL = "insert into baan.ttccol301" & session("env") & " (t$user,t$fein,t$come,t$refcntd,t$refcntu) values('" & Session("user") & "',sysdate+5/24,'Material Rejected Warehouse',0,0)"
	objrs = Server.CreateObject("ADODB.Recordset")
	objrs.Open (strSQL, Odbcon)
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->

<html>
<link href="css/basic.css" rel="stylesheet" type="text/css">
<head>

<script type="text/javascript">

function setFocus()
{
document.getElementById("item").focus();
};

function CheckLength() {
    var lenght = document.getElementById("orden").value.length;
    var str = document.getElementById("orden").value;
    if (document.getElementById("orden").value != "") {
        if (document.getElementById("orden").value.length < 9 || document.getElementById("orden").value.length > 9) {
            alert("Remember 9 characters")
            document.getElementById("orden").focus();
            document.getElementById("orden").value = "";
            return false;
        }
    }
};

</script>
    <style type="text/css">
        .style2
        {
            color: Black;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-style: normal;
            text-align: Left;
            height: 10px;
        }
        .style2
        {
            color: Black;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-style: normal;
            text-align: Left;
            height: 10px;
        }
        .errorMsg
        {
            color: Black;
            font-weight: bold;
            font-size: medium;
        }
</style>        
    </style>
</head>

<%
Dim stritem, strwarehouse, strlocation, strlot, strlen, strflag, strmsg  

strflag = Request.QueryString("flag")
if strflag = "Y" then
  strmsg = ""
  stritem = ""
  strwarehouse = ""
  strlocation = ""
  strlot = ""
else
  stritem  = ""
  strwarehouse = ""
  strlocation = ""
  strlot = ""
  strmsg = Session("strmsg")
end if

%>
<meta name="viewport" content="width=300, user-scalable=no">
<body onload="setFocus()" bgcolor="#87CEEB">
<form name="frmord" method="post" action="whInvMaterialRejectedW2.aspx">
<TABLE style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H3>Material Rejection in Warehouse</H3>
    </td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<table align="left" class="tableDefault4" border="0" cellspacing="0" 
    cellpadding="0">
<tr>
<td class="style2"><b>User </b>:</td>
<td class="style2" colspan="2"><b><%=Session("user")%></b></td>
</tr>
<tr>
<td class="style2"><b>Name </b>:</td>
<td class="style2" colspan="2"><b><%=Session("username")%></b></td>
</tr>
<tr> 
<td class="style2"><b>Item</b></td>
<td class="style2"><input type="text" id="item" name="item" size="9" style="width: 131px; height: 23px;" value="<%=stritem%>"></td>
</tr>
<tr> 
<td class="style2"><b>Warehouse</b></td>
<td class="style2"><input type="text" id="warehouse" name="warehouse" size="9" style="width: 131px; height: 23px;" value="<%=strwarehouse%>"></td>
</tr>
<tr> 
<td class="style2"><b>Location</b></td>
<td class="style2"><input type="text" id="location" name="location" size="9" style="width: 131px; height: 23px;" value="<%=strlocation%>"></td>
</tr>
<tr> 
<td class="style2"><b>Lot</b></td>
<td class="style2"><input type="text" id="lot" name="lot" size="9" style="width: 131px; height: 23px;" value="<%=strlot%>"></td>
</tr>
<tr> 
<td class="errorMsg" colspan="2"><%=strmsg%></td>
</tr> 
    <td colspan="2" align="center">
      <input type="submit" name="btnLogin" value="  Query  ">
    </td>
</tr>
</table>
</form>
</body>
</html>
