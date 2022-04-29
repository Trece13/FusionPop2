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
    public partial class DropPickedMaterialOnTunnelC : System.Web.UI.Page
    {
        public static string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        private static string globalMessages = "GlobalMessages";
        //JC 260721 Adicionar Variable para validar el estado del pick y evitar doble asignación de numero aleatorio
        public static string STATPICK = string.Empty;
        public static string Thedropprocessissuccess = mensajes("Thedropprocessissuccess");
        public static string Thedropprocessisnotsuccess = mensajes("Thedropprocessisnotsuccess");
        public static string ThePalletIDDoesntexist = mensajes("ThePalletIDDoesntexist");
        public static string StatusNotAllowed = mensajes("StatusNotAllowed");
        public static string WarehouseNotAllowed = mensajes("WarehouseNotAllowed");
        public static string PalletIDnotvalidfordropprocess = mensajes("PalletIDnotvalidfordropprocess");
        public static string Thepickdoesnothavewarehouses = mensajes("Thepickdoesnothavewarehouses");
        public static string Thepickdoesnothavewarehousesonconsignment = mensajes("Thepickdoesnothavewarehousesonconsignment");


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
            Ent_tticol082 MyObj = new Ent_tticol082();
            //DataTable TableItticol082 = Itticol082.ConsultarPalletIDTticol082(PickID);
            DataTable TableItticol082 = Itticol082.ConsultarPalletIDTticol082PIckAbreb(PickID.Substring(0, PickID.IndexOf('-')));
            string ObjRetorno = string.Empty;
            bool ActalizacionExitosa = false;
            //JC 230721 Adicionar la generación del número aleatorio para asignarsela al pick
            int randomNum = new Random().Next(999);
            string ramdomNumStr = string.Empty;

            if (randomNum > 0 && randomNum < 10)
            {
                ramdomNumStr = "00" + randomNum.ToString();
            }
            else if (randomNum > 9 && randomNum < 100)
            {
                ramdomNumStr = "0" + randomNum.ToString();
            }
            else
            {
                ramdomNumStr = randomNum.ToString();
            }

            if (ExistenciaData(TableItticol082))
            {
                List<string> paids = new List<string>();
                foreach (DataRow myObjDt in TableItticol082.Rows)
                {
                    MyObj.TBL = myObjDt["TBL"].ToString();
                    MyObj.PAID = myObjDt["PAID"].ToString();
                    paids.Add(myObjDt["PAID"].ToString());
                    MyObj.QTYT = myObjDt["QTYT"].ToString();
                    MyObj.UNIT = myObjDt["UNIT"].ToString();
                    MyObj.ITEM = myObjDt["ITEM"].ToString();
                    MyObj.DSCA = myObjDt["DSCA"].ToString();
                    MyObj.PRIO = myObjDt["PRIO"].ToString();
                    MyObj.ADVS = myObjDt["ADVS"].ToString();
                    MyObj.PONO = myObjDt["PONO"].ToString();
                    MyObj.ORNO = myObjDt["ORNO"].ToString();
                    MyObj.QTYC = myObjDt["QTYA"].ToString();
                    MyObj.PICK = myObjDt["PICK"].ToString().Trim();
                    MyObj.TYPW = myObjDt["TYPW"].ToString();
                    MyObj.CWAR = myObjDt["CWAR"].ToString();
                    MyObj.STAT = myObjDt["STAT"].ToString();
                    MyObj.MCNO = HttpContext.Current.Session["MCNO"].ToString();
                    MyObj.PAIDS = paids;
                    STATPICK = MyObj.STAT;
                    //JC 230721 Cambio para que se envíe el dato con el numero aleatorio
                    //MyObj.PICK_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + myObjDt["PICK"] + "&code=Code128&dpi=96";
                    //JC 270721 Quitar restricion de consignacion 
                    //if (HttpContext.Current.Session["consigment"].ToString().ToLower() == "true" && MyObj.TYPW == "21")
                    if (MyObj.TYPW == "21")
                    {
                        //MyObj.STAT  = MyObj.STAT.Trim() == "2" ? "7" : "4";
                        MyObj.STAT = "8";
                        //JC 230721 Cambio para que se envíe el dato con el numero aleatorio
                        //JC 260721 Cambio para que cuando ya tenga el número aleatorio no le genere uno adicional
                        if (STATPICK == "7")
                        {
                            MyObj.RAND = "";
                            Itticol082.Actualizartticol082SinRandom(MyObj);
                            //JC 230721 Cambio para que se envíe el dato con el numero aleatorio
                            MyObj.PICK_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.PICK + "&code=Code128&dpi=96";
                        }
                        else
                        {
                            MyObj.RAND = ramdomNumStr;
                            MyObj.STAT = "4";
                            MyObj.STATW = "8";
                            Itticol082.Actualizartticol082Pick(MyObj);
                            //JC 230721 Cambio para que se envíe el dato con el numero aleatorio
                            MyObj.PICK = MyObj.PICK + "-" + ramdomNumStr;
                            MyObj.PICK_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.PICK + "&code=Code128&dpi=96";
                        }
                    }
                    //JC 270721 Quitar restricion de consignacion 
                    //else if (HttpContext.Current.Session["consigment"].ToString().ToLower() == "false" && MyObj.TYPW == "1")
                    else if (MyObj.TYPW == "1")
                    {
                        MyObj.STAT = "4";
                        //JC 230721 Cambio para que se envíe el dato con el numero aleatorio
                        MyObj.STATW = "8";
                        MyObj.RAND = ramdomNumStr;
                        Itticol082.Actualizartticol082Pick(MyObj);
                        //JC 230721 Cambio para que se envíe el dato con el numero aleatorio
                        MyObj.PICK = MyObj.PICK + "-" + ramdomNumStr;
                        MyObj.PICK_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.PICK + "&code=Code128&dpi=96";
                    }
                }

                if (MyObj.Error != true)
                {
                    ActalizacionExitosa = true;
                }
                if (ActalizacionExitosa)
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
                MyObj.Error = true;
                MyObj.TipeMsgJs = "alert";
                MyObj.ErrorMsg = ThePalletIDDoesntexist;
                ObjRetorno = JsonConvert.SerializeObject(MyObj);
            }
            return ObjRetorno;
        }

        [WebMethod]
        public static string SearchPickID(string PickID, bool consigment = false)
        {
            bool consigmentPallets = false;
            bool noConsigmentPallets = false;
            HttpContext.Current.Session["consigment"] = consigment.ToString();
            Ent_tticol082 MyObj = new Ent_tticol082();
            //DataTable TableItticol082 = Itticol082.ConsultarPalletIDTticol082(PickID);
            DataTable TableItticol082 = Itticol082.ConsultarPalletIDTticol082PIckAbreb(PickID.Substring(0, PickID.IndexOf('-')));
            string ObjRetorno = string.Empty;
            bool PalletAsignado = false;

            if (ExistenciaData(TableItticol082) && PalletAsignado == false)
            {
                //foreach (DataRow dr1 in TableItticol082.Rows)
                //{
                foreach (DataRow myObjDt in TableItticol082.Rows)
                {
                    MyObj.TBL = myObjDt["TBL"].ToString();
                    MyObj.QTYT = myObjDt["QTYT"].ToString();
                    MyObj.UNIT = myObjDt["UNIT"].ToString();
                    MyObj.ITEM = myObjDt["ITEM"].ToString();
                    MyObj.DSCA = myObjDt["DSCA"].ToString();
                    MyObj.MCNO = myObjDt["MCNO"].ToString();
                    MyObj.DSCAM = myObjDt["DSCAM"].ToString();
                    MyObj.ORNO = myObjDt["ORNO"].ToString();
                    MyObj.STAT = myObjDt["STAT"].ToString();
                    MyObj.STAP = myObjDt["STAP"].ToString();
                    MyObj.TYPW = myObjDt["TYPW"].ToString();
                    //JC 270721 Duplicar la sesion para que permita hacer drop de cualquier pick (consignacion y no consignacion
                    //if (MyObj.TYPW == "21" && consigment == true)
                    //{
                    //    MyObj.PAID += myObjDt["PAID"].ToString().Trim() + ",";
                    //}
                    //else if (MyObj.TYPW == "1" && consigment == false)
                    //{
                    //    MyObj.PAID += myObjDt["PAID"].ToString().Trim() + ",";
                    //}
                    MyObj.PAID += myObjDt["PAID"].ToString().Trim() + ",";
                    HttpContext.Current.Session["PAID"] = MyObj.PAID;
                    HttpContext.Current.Session["MCNO"] = MyObj.MCNO;
                    //if (MyObj.STAT == "2")
                    //{
                    //    MyObj.Error = true;
                    //    MyObj.TipeMsgJs = "alert";
                    //    MyObj.ErrorMsg = StatusNotAllowed;
                    //    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                    //}
                    //if (MyObj.TYPW == "1")
                    //{
                    //    MyObj.Error = true;
                    //    MyObj.TipeMsgJs = "alert";
                    //    MyObj.ErrorMsg = WarehouseNotAllowed;
                    //    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                    //}
                    //JC 270721 Duplicar la sesion para que permita hacer drop de cualquier pick (consignacion y no consignacion
                    //if (MyObj.TYPW != "21")
                    //{
                    //    noConsigmentPallets = true;

                    //}
                    //else if (MyObj.TYPW != "1")
                    //{
                    //    consigmentPallets = true;
                    //}
                }
                //if (consigment == true && consigmentPallets == false)
                //{
                //    MyObj.Error = true;
                //    MyObj.TipeMsgJs = "alert";
                //    MyObj.ErrorMsg = Thepickdoesnothavewarehousesonconsignment;
                //    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                //}
                //else if (consigment == false && noConsigmentPallets == false)
                //{
                //    MyObj.Error = true;
                //    MyObj.TipeMsgJs = "alert";
                //    MyObj.ErrorMsg = Thepickdoesnothavewarehouses;
                //    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                //}
                ObjRetorno = JsonConvert.SerializeObject(MyObj);
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