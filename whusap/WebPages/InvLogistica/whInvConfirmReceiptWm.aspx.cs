using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Entidades;
using whusa.Interfases;
using whusa.Utilidades;

namespace whusap.WebPages.InvLogistica
{
    public partial class whInvConfirmReceiptWm : System.Web.UI.Page
    {
        #region Propiedades
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        private static InterfazDAL_tticol025 _idaltticol025 = new InterfazDAL_tticol025();
        private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
        private static InterfazDAL_ttisfc001 _idalttisfc001 = new InterfazDAL_ttisfc001();

        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        private static string _operator;
        public static string _idioma;
        private static string strError;
        private static string formName;
        private static string globalMessages = "GlobalMessages";
        #endregion

        #region Eventos
        //
        protected void Page_Load(object sender, EventArgs e)
        {
            // Cambiar cultura para manejo de separador decimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            base.InitializeCulture();

            if (!IsPostBack)
            {
                formName = Request.Url.AbsoluteUri.Split('/').Last();
                if (formName.Contains('?'))
                {
                    formName = formName.Split('?')[0];
                }

                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");

                if (HttpContext.Current.Session["user"] == null)
                {
                    Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                }

                _operator = HttpContext.Current.Session["user"].ToString();

                try
                {
                    _idioma = HttpContext.Current.Session["ddlIdioma"].ToString();
                }
                catch (Exception ex)
                {
                    _idioma = "INGLES";
                }

                CargarIdioma();


                string strTitulo = mensajes("encabezado");
                control.Text = strTitulo;

                Ent_ttccol301 data = new Ent_ttccol301()
                {
                    user = HttpContext.Current.Session["user"].ToString(),
                    come = mensajes("encabezado"),
                    refcntd = 0,
                    refcntu = 0
                };

                List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                datalog.Add(data);

                _idalttccol301.insertarRegistro(ref datalog, ref strError);
            }
        }

        [WebMethod]
        public static string ConsultPallet(string txtNumeroOrden)
        {
            ReturnEntity ReturnObj = new ReturnEntity(); 
            //JC 091221 Se Mueven estas variables al entorno locar y no dejarlas publicas
            HttpContext.Current.Session["CWAR"] = "";
            HttpContext.Current.Session["LOCA"] = "";
            string SLOC, LOCA, CWAR;


            if (txtNumeroOrden.Trim() != String.Empty)
            {
                string numeroOrdenPallet = txtNumeroOrden.Trim().ToUpper();
                string numeroOrden = numeroOrdenPallet.Substring(0, 9);
                var pro1 = true;

                var consultaOrden = _idalttisfc001.findByOrderNumberConfirmRecep(ref numeroOrden, ref numeroOrdenPallet, ref pro1, ref strError);

                if (consultaOrden.Rows.Count < 1)
                {
                    pro1 = false;
                    consultaOrden = _idalttisfc001.findByOrderNumberConfirmRecep(ref numeroOrden, ref numeroOrdenPallet, ref pro1, ref strError);
                }

                if (consultaOrden.Rows.Count > 0)
                {
                    var DELE = consultaOrden.Rows[0]["DELE"].ToString();
                    var QTDL = consultaOrden.Rows[0]["QTDL"].ToString();
                    var OSTA = consultaOrden.Rows[0]["OSTA"].ToString();
                    SLOC = consultaOrden.Rows[0]["SLOC"].ToString();
                    LOCA = consultaOrden.Rows[0]["LOCA"].ToString();
                    CWAR = consultaOrden.Rows[0]["CWAR"].ToString();
                    //JC 091221 Crear una variable de sesion para almacenar la bodega
                    HttpContext.Current.Session["CWAR"] = CWAR;
                    HttpContext.Current.Session["LOCA"] = LOCA;
                    //Validamos si el parametro de validar producción esta activo
                    var consultaPrdVl = _idalttisfc001.findProdValidation_Parameter(ref strError);
                    var PRVL = consultaPrdVl.Rows[0]["PRVL"].ToString();
                    if (PRVL == "1")
                    {
                        if (DELE != "4")
                        {
                            ReturnObj.Msg = mensajes("palletwrapped");
                            ReturnObj.Error = true;
                            return JsonConvert.SerializeObject(ReturnObj);
                        }
                    }
                    if (DELE == "3")
                    {
                        ReturnObj.Msg = mensajes("palletstatus");
                        ReturnObj.Error = true;
                        return JsonConvert.SerializeObject(ReturnObj);
                    }
                    if (QTDL == "0")
                    {
                        ReturnObj.Msg = mensajes("palletannounced");
                        ReturnObj.Error = true;
                        return JsonConvert.SerializeObject(ReturnObj);
                    }
                    else if (!pro1)
                    {
                        ReturnObj.Msg = mensajes("palletconfirmed");
                        ReturnObj.Error = true;
                        return JsonConvert.SerializeObject(ReturnObj);
                    }

                    if (OSTA == "5" || OSTA == "7")
                    {
                    }
                    else
                    {
                        ReturnObj.Msg = mensajes("ordernotactive");
                        ReturnObj.Error = true;
                        return JsonConvert.SerializeObject(ReturnObj);
                    }

                    ReturnObj.PAID = txtNumeroOrden;
                    ReturnObj.ORNO = numeroOrden;
                    ReturnObj.MITM = consultaOrden.Rows[0]["MITM"].ToString();
                    ReturnObj.DSCA = consultaOrden.Rows[0]["DSCA"].ToString();
                    ReturnObj.CWAR = consultaOrden.Rows[0]["CWAR"].ToString();
                    ReturnObj.DCWAR = consultaOrden.Rows[0]["DSCACWAR"].ToString();
                    ReturnObj.QRDR = consultaOrden.Rows[0]["QRDR"].ToString();
                    ReturnObj.TOTQTYENT = consultaOrden.Rows[0]["TOTQTYENT"].ToString();
                    ReturnObj.QTYPEND = consultaOrden.Rows[0]["QTYPEND"].ToString();
                    ReturnObj.QTDL = float.Parse(consultaOrden.Rows[0]["QTDL"].ToString());
                    ReturnObj.CUNI = consultaOrden.Rows[0]["CUNI"].ToString();
                    ReturnObj.Msg = String.Empty;
                    ReturnObj.Error = false;
                    HttpContext.Current.Session["MyObj"] = ReturnObj;
                    return JsonConvert.SerializeObject(ReturnObj);
                }
                else
                {
                    ReturnObj.Msg = mensajes("palletannounced");
                    ReturnObj.Error = true;
                    return JsonConvert.SerializeObject(ReturnObj);
                }
            }
            else
            {
                ReturnObj.Msg = mensajes("numberempty");
                ReturnObj.Error = true;
                return JsonConvert.SerializeObject(ReturnObj);
            }
        }

        [WebMethod]
        public static string Save()
        {
            ReturnEntity ReturnObj  = (ReturnEntity)HttpContext.Current.Session["MyObj"];

            InterfazDAL_ttisfc001 _idalttisfc001 = new InterfazDAL_ttisfc001();
            //JC 091221 Grabar las variables de sesion en variables tipo string

            string CWARG = "";
            string LOCAG = "";
            CWARG = HttpContext.Current.Session["CWAR"].ToString();
            LOCAG = HttpContext.Current.Session["LOCA"].ToString();

            string sqnb = ReturnObj.PAID;
            string pdno = ReturnObj.ORNO;
            var pro1 = "2";

            var consultadata = _idalttisfc001.findByPdnoSqnbAndPro1(ref pdno, ref sqnb, ref pro1, ref strError);

            if (consultadata.Rows.Count > 0)
            {
                var STRQTY = ReturnObj.QTDL;
                var QTYPEND = ReturnObj.QRDR;

                if (STRQTY > float.Parse(QTYPEND))
                {
                    ReturnObj.Msg = String.Format(mensajes("confirmedquantity"), STRQTY, QTYPEND);
                    ReturnObj.Error = true;
                    return JsonConvert.SerializeObject(ReturnObj);
                }

                if (STRQTY < 0)
                {
                    ReturnObj.Msg = mensajes("quantitynegative");
                    ReturnObj.Error = true;
                    return JsonConvert.SerializeObject(ReturnObj);
                }

                if (STRQTY == 0)
                {
                    ReturnObj.Msg = mensajes("quantityzero");
                    ReturnObj.Error = true;
                    return JsonConvert.SerializeObject(ReturnObj);
                }

                var consecutivo = _idaltticol025.consultarConsecutivoRegistro(ref pdno, ref strError);

                consecutivo = consecutivo + 1;

                string item = ReturnObj.MITM; // + "|" + lblValueArticulo.Text.Split('|')[1].Trim().ToUpper();
                string dsca = ReturnObj.DSCA;
                float qtdl = STRQTY;
                string cuni = ReturnObj.CUNI;

                Ent_tticol025 data025 = new Ent_tticol025()
                {
                    pdno = pdno,
                    sqnb = consecutivo,
                    mitm = item.Trim().ToUpper(),
                    dsca = dsca,
                    qtdl = qtdl,
                    cuni = cuni,
                    mess = " ",
                    user = HttpContext.Current.Session["user"].ToString(),
                    refcntd = 0,
                    refcntu = 0
                };

                var validSave = _idaltticol025.insertarRegistro(ref data025, ref strError);

                if (validSave > 0)
                {

                    Ent_tticol022 data022 = new Ent_tticol022()
                    {
                        log1 = HttpContext.Current.Session["user"].ToString(),
                        qtd1 = Convert.ToInt32(data025.qtdl),
                        pdno = pdno,
                        sqnb = sqnb
                    };

                    var validateUpdate = _idaltticol022.actualizaRegistroConfirmReceipt(ref data022, ref strError);

                    if (validateUpdate)
                    {
                        ReturnObj.Msg = mensajes("msjsave");
                        ReturnObj.Error = false;
                        _idaltticol022.ActualizarCantidadAlmacenRegistroTicol222(HttpContext.Current.Session["user"].ToString(), data022.qtd1, LOCAG, CWARG, data022.sqnb);
                        return JsonConvert.SerializeObject(ReturnObj);
                    }
                    else
                    {
                        ReturnObj.Msg = mensajes("errorupdt");
                        ReturnObj.Error = true;
                        return JsonConvert.SerializeObject(ReturnObj);
                    }
                }
                else
                {
                    ReturnObj.Msg = mensajes("errorsave");
                    ReturnObj.Error = true;
                    return JsonConvert.SerializeObject(ReturnObj);
                }
            }
            else
            {
                ReturnObj.Msg = mensajes("orderconfirmed");
                ReturnObj.Error = true;
                return JsonConvert.SerializeObject(ReturnObj);
            }
        }

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            //lblNumeroOrden.Text = _textoLabels.readStatement(formName, _idioma, "lblNumeroOrden");
            //btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            //lblOrden.Text = _textoLabels.readStatement(formName, _idioma, "lblOrden");
            //lblArticulo.Text = _textoLabels.readStatement(formName, _idioma, "lblArticulo");
            //lblWareHouse.Text = _textoLabels.readStatement(formName, _idioma, "lblWareHouse");
            //lblTotal.Text = _textoLabels.readStatement(formName, _idioma, "lblTotal");
            //lblDelivered.Text = _textoLabels.readStatement(formName, _idioma, "lblDelivered");
            //lblToReceive.Text = _textoLabels.readStatement(formName, _idioma, "lblToReceive");
            //lblConfirmed.Text = _textoLabels.readStatement(formName, _idioma, "lblConfirmed");
            //lblUnit.Text = _textoLabels.readStatement(formName, _idioma, "lblUnit");
            //btnGuardar.Text = _textoLabels.readStatement(formName, _idioma, "btnGuardar");
        }

        protected static string mensajes(string tipoMensaje)
        {
            var retorno = _mensajesForm.readStatement(formName, _idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = _mensajesForm.readStatement(globalMessages, _idioma, ref tipoMensaje);
            }

            return retorno;
        }

        #endregion

        public class ReturnEntity
        {
            public string PAID { get; set; }
            public string MITM { get; set; }
            public string DSCA { get; set; }
            public string CWAR { get; set; }
            public string DCWAR { get; set; }
            public string QRDR { get; set; }
            public string TOTQTYENT { get; set; }
            public string QTYPEND { get; set; }
            public string CUNI { get; set; }
            public float QTDL { get; set; }
            public bool Error { get; set; }
            public string Msg { get; set; }

            public string ORNO { get; set; }
        }
    }
}