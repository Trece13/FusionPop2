using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa;
using whusa.Interfases;
using Newtonsoft.Json;
using System.Data;
using System.Configuration;
using whusa.Entidades;
using whusa.Interfases;
using System.Globalization;
using System.Threading;
using whusa.Utilidades;
using System.Web.Configuration;
using System.Reflection;
using System.Diagnostics;
using System.Net;
namespace whusap.WebPages.InvReceipts
{
    public partial class RfidPop : System.Web.UI.Page
    {
        private static MethodBase method;
        private static Seguimiento log = new Seguimiento();
        private static Recursos recursos = new Recursos();
        private static StackTrace stackTrace = new StackTrace();
        public static List<Ent_twhcol130131> MyInsert = new List<Ent_twhcol130131>();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        public static InterfazDAL_twhwmd200 twhwmd200 = new InterfazDAL_twhwmd200();
        public static string LstSalesOrderJSON = string.Empty;
        public static string LstTransferOrderJSON = string.Empty;
        public static string LstPurchaseOrdersJSON = string.Empty;
        public string LstUnidadesMedidaJSON = string.Empty;
        public static string RequestUrlAuthority = string.Empty;
        private static LabelsText _textoLabels = new LabelsText();
        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        public string lblQuantityError = string.Empty;
        public string lblConvertionError = string.Empty;
        public string lblOrderError = string.Empty;
        public string lblItemError = string.Empty;
        public string lblDateError = string.Empty;
        public string lblPositionError = string.Empty;
        public static string Priorinboundnotfound = mensajes("Priorinboundnotfound");
        public static string ImportPOBudgetisnotclosedPOcannotberecived = mensajes("ImportPOBudgetisnotclosedPOcannotberecived");
        public static string Thelotdoesnotcorrespondtotheorder = mensajes("Thelotdoesnotcorrespondtotheorder");
        private static string globalMessages = "GlobalMessages";
        public static string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        public static int ToleranciaMaximaDias = Convert.ToInt32(WebConfigurationManager.AppSettings["ToleranciaMaximaDias"].ToString());
        public static int ToleranciaMinimaDias = Convert.ToInt32(WebConfigurationManager.AppSettings["ToleranciaMinimaDias"].ToString());
        private static Mensajes _mensajesForm = new Mensajes();
        public static decimal QUANTITYAUX_COMPLETADA;
        public static List<string> PAIDS;
        private static SrvRfidPop.Service1Client ServiceRfidPop = new SrvRfidPop.Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            //ListasOdenCompra();
            RequestUrlAuthority = (string)Request.Url.Authority;


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
                if (Session["user"] == null)
                {
                    Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                }

                _operator = Session["user"].ToString();

                try
                {
                    _idioma = Session["ddlIdioma"].ToString();
                }
                catch (Exception)
                {
                    _idioma = "INGLES";
                }
                CargarIdioma();

                string strTitulo = mensajes("encabezado");
                control.Text = strTitulo;
                string strError = string.Empty;

                Ent_ttccol301 data = new Ent_ttccol301()
                {
                    user = HttpContext.Current.Session["user"].ToString(),
                    come = strTitulo,
                    refcntd = 0,
                    refcntu = 0
                };

                List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                datalog.Add(data);

                _idalttccol301.insertarRegistro(ref datalog, ref strError);
            }
        }

        [WebMethod]
        public static string ValidarPalletID(string PAID)
        {
            Ent_twhcol130 twhcol130 = new Ent_twhcol130();
            twhcol130.PAID = PAID;
            DataTable dt130 = twhcol130DAL.VerificarPalletID130(twhcol130);
            HttpContext.Current.Session["dt130"] = dt130;
            HttpContext.Current.Session["ObjTwhcol130"] = twhcol130;
            return JsonConvert.SerializeObject(dt130);
        }


        [WebMethod]
        public static string ValidarRfis(string RFID)
        {
            Ent_twhcol130 twhcol130 = (Ent_twhcol130)HttpContext.Current.Session["ObjTwhcol130"];
            HttpContext.Current.Session["RFID"] = RFID.Trim().ToUpper();

            string strMsg = string.Empty;
            ////JC 020322 Validar si el web service esta arriba
            //string ws = WebConfigurationManager.AppSettings["ws"].ToString();
            if (twhcol130.PAID.ToUpper().Trim().Contains("-RT"))
            {
                return "";
            }

            var respuesta = true;
            if (respuesta != false)
            {
                DataTable dt133WhitPaid = ServiceRfidPop.SelectWhcol133OPaidAssing(RFID, "VA Dock");
                DataTable dt133 = ServiceRfidPop.SelectWhcol133Oss(RFID, "VA Dock");
                HttpContext.Current.Session["dt133"] = dt133;
                if (dt133.Rows.Count <= 0)
                {
                    strMsg = mensajes("RfidNotRegisterRealview");
                    //HttpContext.Current.Session["dt133"] = dt133;
                }
                if (dt133WhitPaid.Rows.Count > 0)
                {
                    strMsg = mensajes("RfidAlreadyLinkedPallet");
                    //HttpContext.Current.Session["dt133"] = dt133WhitPaid;
                }
                return strMsg;
            } //JC 020322 Mensaje cuando el web service no esté disponible.
            else
            {
                strMsg = mensajes("Rfidnotalive");
                return strMsg;
            }            
        }

        [WebMethod]
        public static bool Save()
        {
            DataTable dt130 = (DataTable)HttpContext.Current.Session["dt130"];
            DataTable dt133 = (DataTable)HttpContext.Current.Session["dt133"];
            Ent_twhcol130 MyObj131 = new Ent_twhcol130();
            MyObj131.FIRE = "1";
            MyObj131.PAID = dt130.Rows[0]["T$PAID"].ToString();

            bool bl1 = false;
            bool bl2 = false;
            bool bl3 = false;

            if (dt130.Rows[0]["T$PAID"].ToString().ToUpper().Contains("-RT"))
            {
                bl1 = ServiceRfidPop.Insert133ss(dt130.Rows[0]["T$PAID"].ToString(), HttpContext.Current.Session["RFID"].ToString(), "VA Dock 11", dt130.Rows[0]["T$ORNO"].ToString(), "", _operator, "1", "", "");
                DataTable dt133ss = ServiceRfidPop.SelectWhcol133Oss(HttpContext.Current.Session["RFID"].ToString(), "VA Dock 11");            
                bl2 = ServiceRfidPop.ProWhcol133Ora(dt130.Rows[0]["T$PAID"].ToString(), dt133ss.Rows[0]["RFID"].ToString(), dt133ss.Rows[0]["EVNT"].ToString(), dt130.Rows[0]["T$ORNO"].ToString(), _operator, "Si");
                bl3 = twhcol130DAL.Actfirecol130140(MyObj131);                
            }
            else
            {
                bl1 = ServiceRfidPop.Update133ss(dt130.Rows[0]["T$PAID"].ToString(), dt133.Rows[0]["RFID"].ToString(), "VA Dock", dt130.Rows[0]["T$ORNO"].ToString(), "", "", "", "", "");
                bl2 = ServiceRfidPop.ProWhcol133Ora(dt130.Rows[0]["T$PAID"].ToString(), dt133.Rows[0]["RFID"].ToString(), dt133.Rows[0]["EVNT"].ToString(), dt130.Rows[0]["T$ORNO"].ToString(), _operator, "Si");
                bl3 = twhcol130DAL.Actfirecol130140(MyObj131);
            }
            
            if (bl1 && bl2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CargarIdioma()
        {
            lblQuantityError = _textoLabels.readStatement(formName, _idioma, "lblQuantityError");
            lblConvertionError = _textoLabels.readStatement(formName, _idioma, "lblConvertionError");
            lblOrderError = _textoLabels.readStatement(formName, _idioma, "lblOrderError");
            lblItemError = _textoLabels.readStatement(formName, _idioma, "lblItemError");
            lblDateError = _textoLabels.readStatement(formName, _idioma, "lblDateError");
            lblPositionError = _textoLabels.readStatement(formName, _idioma, "lblPositionError");
        }

        protected static string mensajes(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("RfidPop.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }
        //JC 020322 Función para determinar si el web service está disponible
        private static bool WebSiteIsAvailable(string Url)
        {
            string Message = string.Empty;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Url);

            // Set the credentials to the current user account
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Do nothing; we're only testing to see if we can get the response
                }
            }
            catch (WebException ex)
            {
                Message += ((Message.Length > 0) ? "\n" : "") + ex.Message;
            }

            return (Message.Length == 0);
        }
    }
}