using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using whusa.Entidades;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.Configuration;
using whusa;
using System.Threading;
using System.Configuration;
using System.Globalization;
using whusa.Utilidades;

namespace whusap.WebPages.Migration
{
    public partial class whInvReprintMrbRejection : System.Web.UI.Page
    {
        public static int kltc = 0;
        public static string RequestUrlAuthority = string.Empty;
        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        private static string globalMessages = "GlobalMessages";

        public static string ItemcodeisnotPurchaseType = mensajes("ItemcodeisnotPurchaseType");
        public static string Itemcodedoesntexist = mensajes("Itemcodedoesntexist");
        public static string Lotcodedoesntexist = mensajes("Lotcodedoesntexist");
        public static string Warehousecodedoesntexist = mensajes("Warehousecodedoesntexist");
        public static string Locationblockedinbound = mensajes("Locationblockedinbound");
        public static string Locationcodedoesntexist = mensajes("Locationcodedoesntexist");
        public static string codedoesntexist = mensajes("codedoesntexist");
        public static string RegisteredquantitynotavilableonBaaninventory = mensajes("RegisteredquantitynotavilableonBaaninventory");

        public static string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        public static InterfazDAL_ttcibd001 ITtcibd001 = new InterfazDAL_ttcibd001();
        public static InterfazDAL_tticol125 ITticol125 = new InterfazDAL_tticol125();
        public static InterfazDAL_ttwhcol016 ITtwhcol016 = new InterfazDAL_ttwhcol016();
        public static InterfazDAL_twhwmd200 ITwhwmd200 = new InterfazDAL_twhwmd200();
        public static IntefazDAL_transfer Itransfer = new IntefazDAL_transfer();
        public static InterfazDAL_twhinr140 ITtwhinr140 = new InterfazDAL_twhinr140();
        private static InterfazDAL_tticol100 _idaltticol100 = new InterfazDAL_tticol100();
        private static InterfazDAL_tticol116 _idaltticol116 = new InterfazDAL_tticol116();
        private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();

        protected void Page_Load(object sender, EventArgs e)
        {
            RequestUrlAuthority = (string)Request.Url.Authority;


            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
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

                string strError = string.Empty;

                Ent_ttccol301 data = new Ent_ttccol301()
                {
                    user = HttpContext.Current.Session["user"].ToString(),
                    come = "",
                    refcntd = 0,
                    refcntu = 0
                };

                List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                datalog.Add(data);

                _idalttccol301.insertarRegistro(ref datalog, ref strError);
            }
        }

        [WebMethod]
        public static string Click_Print(string PAID)
        {
            string strError = string.Empty;
            Ent_twhcol130131 MyObj = new Ent_twhcol130131();
            DataTable dt100 = _idaltticol100.SelectRegister(PAID, ref strError);
            DataTable dt116 = _idaltticol116.SelectRegister(PAID, ref strError);
            
            if(dt100.Rows.Count > 0 ){
                HttpContext.Current.Session["WorkOrder"]    = dt100.Rows[0]["T$PDNO"].ToString();
                HttpContext.Current.Session["lblReason"]    = dt100.Rows[0]["T$OBSE"].ToString();
                HttpContext.Current.Session["codePaid"]     = dt100.Rows[0]["T$PAID"].ToString();
                HttpContext.Current.Session["ProductDesc"]  = dt100.Rows[0]["T$ITEM"].ToString();
                HttpContext.Current.Session["ProductCode"]  = dt100.Rows[0]["T$ITEM"].ToString();
                HttpContext.Current.Session["Date"]         = dt100.Rows[0]["T$DATR"].ToString();
                HttpContext.Current.Session["Quantity"]     = dt100.Rows[0]["T$QTYR"].ToString();
                HttpContext.Current.Session["Finished"]     = dt100.Rows[0]["T$LOGR"].ToString();
                HttpContext.Current.Session["Pallet"]       = dt100.Rows[0]["T$PAID"].ToString();
                HttpContext.Current.Session["PrintedBy"]    = dt100.Rows[0]["T$LOGR"].ToString();
                HttpContext.Current.Session["Machine"]      = dt100.Rows[0]["T$MCNO"].ToString();
                HttpContext.Current.Session["Comments"]     = dt100.Rows[0]["T$OBSE"].ToString();
                HttpContext.Current.Session["Reprint"]      = "yes";
                MyObj.Error = false;
                MyObj.SuccessMsg = "5MRBMaterials.aspx";
            }
            else if(dt116.Rows.Count > 0 ){
                HttpContext.Current.Session["MaterialDesc"] = dt116.Rows[0]["T$ITEM"].ToString();
                HttpContext.Current.Session["Material"]     = dt116.Rows[0]["T$ITEM"].ToString();
                HttpContext.Current.Session["codePaid"]     = dt116.Rows[0]["T$PAID"].ToString();
                HttpContext.Current.Session["Lot"]          = dt116.Rows[0]["T$CLOT"].ToString();
                HttpContext.Current.Session["Quantity"]     = dt116.Rows[0]["T$QTYR"].ToString();
                HttpContext.Current.Session["Date"]         = dt116.Rows[0]["T$DATR"].ToString();
                HttpContext.Current.Session["Machine"]      = _idaltticol022.getMachine(dt116.Rows[0]["T$CLOT"].ToString().Trim().ToString(), dt116.Rows[0]["T$ITEM"].ToString().Trim().ToUpper(), ref strError);
                HttpContext.Current.Session["Operator"]     = dt116.Rows[0]["T$LOGR"].ToString();
                HttpContext.Current.Session["Pallet"]       = dt116.Rows[0]["T$PAID"].ToString();
                HttpContext.Current.Session["Reprint"]      = "yes";
                MyObj.Error = false;
                MyObj.SuccessMsg = "3Regrinds.aspx";
            }
            else
            {
                MyObj.Error = true;
                MyObj.ErrorMsg = "Pallet ID Doesn't exist";
            }


            
            //{
                //OORG = "2",// Order type escaneada view 
                //ORNO = "INITIAPOP",
                //ITEM =  ITEM.ToUpper(),
                //PAID = "INITIAPOP" + "-" + SecuenciaPallet,
                //PONO = "1",
                //SEQN = "1",
                //CLOT = CLOT.ToUpper(),// lote VIEW
                //CWAR = CWAR.ToUpper(),
                //QTYS = QTYS,// cantidad escaneada view 
                //QTYA = QTYS,
                //UNIT = UNIT,//unit escaneada view
                //QTYC = QTYS,//cantidad escaneada view aplicando factor
                //UNIC = UNIT,//unidad view stock
                //DATE = DateTime.Now.ToString("dd/MM/yyyy").ToString(),//fecha de confirmacion 
                //CONF = "1",
                //RCNO = " ",//llena baan
                //DATR = DateTime.Now.ToString("dd/MM/yyyy").ToString(),//llena baan
                //LOCA = LOCA.ToUpper(),// enviamos vacio
                //DATL = DateTime.Now.ToString("dd/MM/yyyy").ToString(),//llenar con fecha de hoy
                //PRNT = "1",// llenar en 1
                //DATP = DateTime.Now.ToString("dd/MM/yyyy").ToString(),//llena baan
                //NPRT = "1",//conteo de reimpresiones 
                //LOGN = HttpContext.Current.Session["user"].ToString(),// nombre de ususario de la session
                //LOGT = " ",//llena baan
                //STAT = "3",// LLENAR EN 3 
                //DSCA = " ",
                //COTP = " ",
                //FIRE = "2",
                //PSLIP = " ",
                //ALLO = "0",
                //PAID_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + "INITIAPOP" + "-" + SecuenciaPallet + "&code=Code128&dpi=96",
                //ORNO_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + "DTOrdencompra.Rows[0][].ToString()" + "&code=Code128&dpi=96",
                //ITEM_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + ITEM + "&code=Code128&dpi=96",
                //CLOT_URL = CLOT.ToString().Trim() != "" ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + CLOT + "&code=Code128&dpi=96" : "",
                //QTYC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + QTYS + "&code=Code128&dpi=96",
                //UNIC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + UNIT + "&code=Code128&dpi=96"
            //};

            
            //bool Insertsucces = twhcol130DAL.Insertartwhcol131(MyObj);

            //if (Insertsucces)
            //{
            //    HttpContext.Current.Session["MaterialDesc"] = "XXXXX XXX XXX XX";
            //    HttpContext.Current.Session["MaterialCode"] = MyObj.ITEM;
            //    HttpContext.Current.Session["codePaid"] =  MyObj.PAID ;
            //    HttpContext.Current.Session["Lot"] = MyObj.CLOT;
            //    HttpContext.Current.Session["Quantity"] = MyObj.QTYS + " " + UNIT;
            //    HttpContext.Current.Session["Origin"] = MyObj.CLOT;
            //    HttpContext.Current.Session["Supplier"] = "";
            //    HttpContext.Current.Session["RecibedBy"] = MyObj.LOGN;
            //    HttpContext.Current.Session["RecibedOn"] = DateTime.Now.ToString();
            //    HttpContext.Current.Session["Reprint"] = "no";
            //}
            //else
            //{
            //    MyObj.error = true;
            //    MyObj.TypeMsgJs = "label";
            //    MyObj.errorMsg = "Error insert";
            //}


            return JsonConvert.SerializeObject(MyObj);

        }

        protected static string mensajes(string tipoMensaje)
        {
            string idioma = "INGLES";
            Mensajes _mensajesForm = new Mensajes();
            var retorno = _mensajesForm.readStatement("GeneratePalletIDPurchaseItems.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = _mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }
    }
}