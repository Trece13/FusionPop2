using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using whusa.Interfases;
using System.Data;
using Newtonsoft.Json;
using whusa.Entidades;
using System.Threading;
using System.Globalization;
using System.Configuration;
using whusa.Utilidades;
using System.Web.Configuration;

namespace whusap.WebPages.WorkOrders.NewPages
{
    public partial class DropPickedMaterialOnTunnel : System.Web.UI.Page
    {
        public static string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        private static string globalMessages = "GlobalMessages";

        public static string Thedropprocessissuccess = mensajes("Thedropprocessissuccess");
        public static string Thedropprocessisnotsuccess = mensajes("Thedropprocessisnotsuccess");
        public static string ThePalletIDDoesntexist = mensajes("ThePalletIDDoesntexist");
        public static string PalletIDnotvalidfordropprocess = mensajes("PalletIDnotvalidfordropprocess");


        public static IntefazDAL_tticol082 Itticol082 = new IntefazDAL_tticol082();
        public static InterfazDAL_twhcol130 Itwhcol130 = new InterfazDAL_twhcol130();

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    user = HttpContext.Current.Session["user"].ToString().Trim(),
                    refcntd = 0,
                    refcntu = 0
                };

                List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                datalog.Add(data);

                _idalttccol301.insertarRegistro(ref datalog, ref strError);

            }
        }

        [WebMethod]
        public static string ClickDropTagPick(string PickID)
        {
            DataTable TableItticol082 = Itticol082.ConsultarPalletIDTticol082(PickID);
            string ObjRetorno = string.Empty;

            if (ExistenciaData(TableItticol082))
            {
                DataRow myObjDt = TableItticol082.Rows[0];
                Ent_tticol082 MyObj = new Ent_tticol082
                {
                    TBL = myObjDt["TBL"].ToString(),
                    PAID = myObjDt["PAID"].ToString(),
                    QTYT = myObjDt["QTYT"].ToString(),
                    UNIT = myObjDt["UNIT"].ToString(),
                    ITEM = myObjDt["ITEM"].ToString(),
                    DSCA = myObjDt["DSCA"].ToString(),
                    PRIO = myObjDt["PRIO"].ToString(),
                    ADVS = myObjDt["ADVS"].ToString(),
                    PONO = myObjDt["PONO"].ToString(),
                    ORNO = myObjDt["ORNO"].ToString(),
                    QTYC = myObjDt["QTYA"].ToString(),
                    PICK = myObjDt["PICK"].ToString(),
                    PICK_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + myObjDt["PICK"].ToString() + "&code=Code128&dpi=96   "
                };
                bool ActalizacionExitosa = false;
                switch (MyObj.TBL)
                {
                    case "ticol022":
                        //Itwhcol130.Eliminartccol307(MyObj.PAID, "");
                        Itticol082.Actualizartticol082(MyObj);
                        //if (MyObj.QTYC.Trim() == "0") { ActalizacionExitosa = Itticol082.Actualizartticol022(MyObj); } else { ActalizacionExitosa = false; }
                        break;
                     case "whcol131":
                        //Itwhcol130.Eliminartccol307(MyObj.PAID, "");
                        Itticol082.Actualizartticol082(MyObj);
                        //if (MyObj.QTYC.Trim() == "0") { ActalizacionExitosa = Itticol082.Actualizartwhcol131(MyObj); } else { ActalizacionExitosa = false; }

                        break;
                    case "whcol130":
                        //Itwhcol130.Eliminartccol307(MyObj.PAID, "");
                        Itticol082.Actualizartticol082(MyObj);
                        //if (MyObj.QTYC.Trim() == "0") { ActalizacionExitosa = Itticol082.Actualizartwhcol130(MyObj); } else { ActalizacionExitosa = false; }
                        break;
                    case "ticol042":
                        //Itwhcol130.Eliminartccol307(MyObj.PAID, "");
                        Itticol082.Actualizartticol082(MyObj);
                        //if (MyObj.QTYC.Trim() == "0") { ActalizacionExitosa = Itticol082.Actualizartticol042(MyObj); } else { ActalizacionExitosa = false; }
                        break;
                }
                if (ActalizacionExitosa)
                {
                    MyObj.Error = false;
                    MyObj.TipeMsgJs = "alert";
                    MyObj.SuccessMsg = Thedropprocessissuccess;
                    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                }
                else if (MyObj.QTYC.Trim() != "0")
                {
                    MyObj.Error = false;
                    MyObj.TipeMsgJs = "alert";
                    MyObj.SuccessMsg = Thedropprocessissuccess;
                    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                }
                else
                {
                    MyObj.Error = true;
                    MyObj.TipeMsgJs = "alert";
                    MyObj.ErrorMsg = Thedropprocessisnotsuccess;
                    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                }
            }
            else
            {
                Ent_tticol082 MyObj = new Ent_tticol082
                {
                    Error = true,
                    TipeMsgJs = "alert",
                    ErrorMsg = ThePalletIDDoesntexist
                };
                ObjRetorno = JsonConvert.SerializeObject(MyObj);
            }
            return ObjRetorno;
        }

        [WebMethod]
        public static string SearchPickID(string PickID)
        {
            Ent_tticol082 MyObj = new Ent_tticol082();
            DataTable TableItticol082 = Itticol082.ConsultarPalletIDTticol082(PickID);
            string ObjRetorno = string.Empty;
            bool PalletAsignado = false;

            if (ExistenciaData(TableItticol082) && PalletAsignado == false)
            {
                //foreach (DataRow dr1 in TableItticol082.Rows)
                //{
                  foreach (DataRow myObjDt in TableItticol082.Rows)
                        //DataRow myObjDt = TableItticol082.Rows[0];
                  {
                    MyObj.TBL = myObjDt["TBL"].ToString();
                    //MyObj.PAID = myObjDt["PAID"].ToString().Trim() + "," + MyObj.PAID.Trim();
                    MyObj.PAID = myObjDt.ItemArray[2].ToString().Trim() + "," + MyObj.PAID.Trim();
                    MyObj.QTYT = myObjDt["QTYT"].ToString();
                    MyObj.UNIT = myObjDt["UNIT"].ToString();
                    MyObj.ITEM = myObjDt["ITEM"].ToString();
                    MyObj.DSCA = myObjDt["DSCA"].ToString();
                    MyObj.MCNO = myObjDt["MCNO"].ToString();
                    MyObj.DSCAM = myObjDt["DSCAM"].ToString();
                    MyObj.ORNO = myObjDt["ORNO"].ToString();
                    MyObj.STAT = myObjDt["STAT"].ToString();
                    MyObj.STAP = myObjDt["STAP"].ToString();
                    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                }
            }
            else if (PalletAsignado == false)
            {
                MyObj.Error = true;
                MyObj.TipeMsgJs = "alert";
                MyObj.ErrorMsg = ThePalletIDDoesntexist;
                ObjRetorno = JsonConvert.SerializeObject(MyObj);
            }
            return ObjRetorno;
        }

        public static bool ExistenciaData(DataTable Data)
        {
            bool ContieneDatos = false;
            if (Data.Rows.Count > 0)
            {
                ContieneDatos = true;
            }
            return ContieneDatos;
        }

        protected static string mensajes(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("DropPickedMaterialOnTunnel.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }

    }
}