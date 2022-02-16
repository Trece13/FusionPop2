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
using System.Text;
using whusa.Utilidades;

namespace whusap.WebPages.WorkOrders
{
    public partial class RegisterPalletsSalesOrdersNew : System.Web.UI.Page
    {

        public static int kltc = 0;
        public static string RequestUrlAuthority = string.Empty;
        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        private static string globalMessages = "GlobalMessages";

        public static string SalesOrdercodedoesntexist = mensajes("SalesOrdercodedoesntexist");
        public static string Paidcodedoesntexist = mensajes("Paidcodedoesntexist");

        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
        private static InterfazDAL_tticol042 _idaltticol042 = new InterfazDAL_tticol042();
        public static InterfazDAL_ttcibd001 ITtcibd001 = new InterfazDAL_ttcibd001();
        public static InterfazDAL_tticol125 ITticol125 = new InterfazDAL_tticol125();
        public static InterfazDAL_ttdcol137 ITticol137 = new InterfazDAL_ttdcol137();
        public static DataTable resultado = new DataTable();
        public static List<StatusTab> LstStatusTab;
        protected void Page_Load(object sender, EventArgs e)
        {

            RequestUrlAuthority = (string)Request.Url.Authority;
            //Remoto
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            //Local
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();

            if (!IsPostBack)
            {

                HttpContext.Current.Session["TABLA"] = "";
                HttpContext.Current.Session["PAID"] = "";
                HttpContext.Current.Session["ORNO"] = "";
                HttpContext.Current.Session["CLOT"] = "";
                HttpContext.Current.Session["CWAR"] = "";
                HttpContext.Current.Session["LOCA"] = "";
                HttpContext.Current.Session["QTYA"] = "";

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
                resultado = ITticol137.List_StatusPallet_OriginTable(ref strError);
                if (resultado.Rows.Count > 0)
                {
                    LstStatusTab = new List<StatusTab>();
                    foreach(DataRow reg in resultado.Rows){

                        StatusTab objStaTab  = new StatusTab();
                        objStaTab.Table = reg["TAB"].ToString();
                        objStaTab.Code = reg["CODE"].ToString();
                        objStaTab.Desc = reg["DESCR"].ToString();
                        LstStatusTab.Add(objStaTab);

                    }
                }
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
        public static string VerificarSalesOrder(string ORDER)
        {
            HttpContext.Current.Session["ORNO"] = "";
            Ent_ttcibd001 ObjTtcibd001 = new Ent_ttcibd001();

            DataTable dtTtcibd001 = ITtcibd001.findSalesOrder(ORDER);
            if (dtTtcibd001.Rows.Count > 0)
            {
                ObjTtcibd001.Error = false;
                ObjTtcibd001.TypeMsgJs = "console";
                ObjTtcibd001.SuccessMsg = "Orden Encontrada";
                HttpContext.Current.Session["ORNO"] = ORDER;
            }
            else
            {
                ObjTtcibd001.Error = true;
                ObjTtcibd001.TypeMsgJs = "label";
                ObjTtcibd001.ErrorMsg = SalesOrdercodedoesntexist;
            }
            return JsonConvert.SerializeObject(ObjTtcibd001);
        }

        [WebMethod]
        public static string VerificarPallet(string PAID)
        {
            HttpContext.Current.Session["TABLA"] = "";
            HttpContext.Current.Session["PAID"] = "";
            HttpContext.Current.Session["CLOT"] = "";
            HttpContext.Current.Session["CWAR"] = "";
            HttpContext.Current.Session["LOCA"] = "";
            HttpContext.Current.Session["QTYA"] = "";
            HttpContext.Current.Session["RFID"] = "";

            string strError = string.Empty;

            Ent_tticol125 Obj_tticol125 = new Ent_tticol125();
            Obj_tticol125.paid = PAID;

            DataTable DtTticol125 = ITticol137.vallidatePalletInfoSalesOrder(ref Obj_tticol125, ref strError);

            if (DtTticol125.Rows.Count > 0)
            {
                Obj_tticol125.Error = false;
                Obj_tticol125.TypeMsgJs = "console";
                Obj_tticol125.SuccessMsg = "Lote Encontrado";
                Obj_tticol125.item = DtTticol125.Rows[0]["T$ITEM"].ToString();
                Obj_tticol125.clot = DtTticol125.Rows[0]["T$CLOT"].ToString();
                Obj_tticol125.cwar = DtTticol125.Rows[0]["T$CWAR"].ToString();
                Obj_tticol125.dsca = DtTticol125.Rows[0]["DSCA"].ToString();
                Obj_tticol125.pdno = DtTticol125.Rows[0]["T$LOCA"].ToString();
                Obj_tticol125.qtya = DtTticol125.Rows[0]["T$QTYC"].ToString();
                Obj_tticol125.stat = DtTticol125.Rows[0]["STAT"].ToString();
                Obj_tticol125.kltc = DtTticol125.Rows[0]["KLTC"].ToString();
                Obj_tticol125.rfid = DtTticol125.Rows[0]["PAIDR"].ToString();
                HttpContext.Current.Session["TABLA"] = DtTticol125.Rows[0]["TBL"].ToString();
                HttpContext.Current.Session["PAID"] = PAID;
                HttpContext.Current.Session["CLOT"] = Obj_tticol125.clot;
                HttpContext.Current.Session["CWAR"] = Obj_tticol125.cwar;
                HttpContext.Current.Session["LOCA"] = Obj_tticol125.pdno;
                HttpContext.Current.Session["QTYA"] = Obj_tticol125.qtya;
                HttpContext.Current.Session["STAT"] = Obj_tticol125.stat;
                HttpContext.Current.Session["RFID"] = Obj_tticol125.rfid;
                HttpContext.Current.Session["User"].ToString();
                Obj_tticol125.statsTab = LstStatusTab.FindAll(e => DtTticol125.Rows[0]["TBL"].ToString().Contains(e.Table));
                
                //JC 121021 Generar la tabla de estados de acuerdo a la tabla
                
            }
            else
            {
                Obj_tticol125.Error = true;
                Obj_tticol125.TypeMsgJs = "label";
                Obj_tticol125.ErrorMsg = Paidcodedoesntexist;

            }

            return JsonConvert.SerializeObject(Obj_tticol125);


        }

        [WebMethod]
        public static void save(string STAT,string CWAR,string LOCA,string QTYA,string LOT, string RFID)
        {
            string TABLA = HttpContext.Current.Session["TABLA"].ToString();
            string PALLET = HttpContext.Current.Session["PAID"].ToString();
            //string ORNO = HttpContext.Current.Session["ORNO"].ToString();
            string CLOT = HttpContext.Current.Session["CLOT"].ToString();

            string strError = string.Empty;
            Ent_ttdcol137 data137;
            data137 = new Ent_ttdcol137();
            data137.Paid = PALLET.ToUpper();
            //data137.Orno = ORNO.ToUpper();
            data137.Clot = CLOT;
            data137.Cwar = CWAR.ToUpper();
            data137.Loca = LOCA.ToUpper();
            data137.Qtya = Convert.ToDecimal(QTYA);
            data137.Dele = Convert.ToInt32(STAT.Trim());
            data137.User = HttpContext.Current.Session["User"].ToString();
            data137.Lot = LOT.ToUpper();

            //var validatesave = ITticol137.insertarDatos(ref data137, ref strError);
            //if (validatesave > 0) 
            //{
                //if (TABLA == "whcol131")
                //{
                //    data137.Dele = 11;
                //}
                //else
                //{
                //    data137.Dele = 11;
                //}
                if (TABLA == "ticol022")
                {
                    var qt = Convert.ToDecimal(QTYA);
                    var validateSaveTicol222 = ITticol137.Actualizarttdcol222Cant(ref PALLET, ref qt);;
                    var validateSaveTicol222N = ITticol137.Actualizarttdcol222(data137);
                    var validateSaveTicol022N = ITticol137.Actualizarttdcol022Pdno(data137);
                    var validateSaveTicol022 = ITticol137.Actualizarttdcol022Status(data137);
                }
                if (TABLA == "ticol042")
                {
                    var qt = Convert.ToDecimal(QTYA);
                    var validateSaveTicol242 = ITticol137.Actualizarttdcol242Cant(ref PALLET, ref qt);
                    var validateSaveTicol242N = ITticol137.Actualizarttdcol242(data137);
                    var validateSaveTicol042N = ITticol137.Actualizarttdcol042Pdno(data137);
                    var validateSaveTicol042 = ITticol137.Actualizarttdcol042Status(data137);
                }
                if (TABLA == "whcol131")
                {
                    var qt = Convert.ToDecimal(QTYA);
                    var STATUS = Convert.ToInt32(STAT.Trim());
                    var validateSaveWhcol131 = ITticol137.Actualizartwhcol131CantStatus(ref PALLET, ref STATUS, ref qt,CWAR,LOCA,LOT);
                }
                //JC 030222 Actualizar el rfid si hay cambio
                if (HttpContext.Current.Session["RFID"].ToString().Trim() != RFID)
                {
                    var validateSaveWhcol133 = ITticol137.Actualizartwhcol133Rfid(ref PALLET, RFID);
                    SrvRfidPop.Service1Client SrvRfidPopWcf = new SrvRfidPop.Service1Client();
                    bool validateSaveWhcol133Ss = SrvRfidPopWcf.Updtwhcol133RfidSS(PALLET, RFID);
                }
                
            //}
        }

        protected static string mensajes(string tipoMensaje)
        {
            string idioma = "INGLES";
            Mensajes _mensajesForm = new Mensajes();
            var retorno = _mensajesForm.readStatement("RegisterPalletsSalesOrders.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = _mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }


    }
}