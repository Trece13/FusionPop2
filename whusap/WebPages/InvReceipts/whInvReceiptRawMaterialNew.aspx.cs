﻿using System;
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
//using whusa.Interfases;
using System.Globalization;
using System.Threading;
using whusa.Utilidades;
using System.Web.Configuration;
using System.Reflection;
using System.Diagnostics;

namespace whusap.WebPages.InvReceipts
{
    public partial class whInvReceiptRawMaterialNew : System.Web.UI.Page
    {
        private static MethodBase method;
        private static Seguimiento log = new Seguimiento();
        private static Recursos recursos = new Recursos();
        private static StackTrace stackTrace = new StackTrace();

        public static bool qtylabelsrec = Convert.ToBoolean(WebConfigurationManager.AppSettings["qtylabelsrec"].ToString().ToLower());
        public static int CiclePrintBegin = 0;
        public static int CiclePrintEnd = 1;
        public static int CurrentPackageCicles = 0;
        public static int CurrentIndependentCicles = 0;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            //ListasOdenCompra();
            LstUnidadesMedidaJSON = ConsultaUnidadesMedida();
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
        public static string InsertarReseiptRawMaterial(string OORG, string ORNO, string ITEM, string PONO, string LOT, decimal QUANTITY, string STUN, string CUNI, string CWAR, string FIRE, string PSLIP, string CICLE = "1", bool INIT = false)
        {
            try
            {
                log.escribirError("he entrado al try", "hola", "InsertarReseiptRawMaterial", "InsertarReseiptRawMaterial");

                int Labels = Convert.ToInt32(CICLE) - 1;
                int FinalPaid = Convert.ToInt32(CICLE);
                int Packages = 10;
                int PackageCicles = Labels / Packages;
                int IndependentCicles = Labels % Packages;

                if (INIT == true)
                {
                    PAIDS = new List<string>();
                    CurrentPackageCicles = 0;
                    CurrentIndependentCicles = 0;
                    QUANTITYAUX_COMPLETADA = 0;
                    CiclePrintBegin = 0;
                    MyInsert.Clear();
                    HttpContext.Current.Session["QuantityFinal"] = null;
                    HttpContext.Current.Session["MyConvertionFactor"] = null;
                    HttpContext.Current.Session["DTOrdencompra"] = null;
                    HttpContext.Current.Session["OrdenImportacion"] = null;
                    HttpContext.Current.Session["Dttwhwmd200"] = null;
                    HttpContext.Current.Session["PRIORIDAD"] = null;
                    HttpContext.Current.Session["LOCAL"] = null;
                    CiclePrintEnd = Convert.ToInt32((Convert.ToInt32(CICLE) < 1) ? 1 : ((Convert.ToInt32(CICLE) > 1000) ? 1000 : Convert.ToInt32(CICLE)));
                    PSLIP = PSLIP.Trim() == string.Empty ? " " : PSLIP.Trim();
                }


                //string PSLIP = string.Empty; 
                decimal QUANTITYAUX = QUANTITY;

                string Retrono = "El Registro no se ha insertado";
                Factor MyConvertionFactor = new Factor { };

                if (CUNI != STUN)
                {
                    if (HttpContext.Current.Session["MyConvertionFactor"] == null)
                    {
                        MyConvertionFactor = FactorConversion(ITEM, STUN, CUNI);
                        HttpContext.Current.Session["MyConvertionFactor"] = MyConvertionFactor;
                        QUANTITYAUX = (MyConvertionFactor.Tipo == "Div") ? Convert.ToDecimal((QUANTITY * MyConvertionFactor.FactorB) / MyConvertionFactor.FactorD) : Convert.ToDecimal((QUANTITY * MyConvertionFactor.FactorD) / MyConvertionFactor.FactorB);
                        QUANTITYAUX = Decimal.Round(QUANTITYAUX, 4);
                    }
                    else
                    {
                        MyConvertionFactor = (Factor)HttpContext.Current.Session["MyConvertionFactor"];
                        QUANTITYAUX = (MyConvertionFactor.Tipo == "Div") ? Convert.ToDecimal((QUANTITY * MyConvertionFactor.FactorB) / MyConvertionFactor.FactorD) : Convert.ToDecimal((QUANTITY * MyConvertionFactor.FactorD) / MyConvertionFactor.FactorB);
                        QUANTITYAUX = Decimal.Round(QUANTITYAUX, 4);
                    }
                }

                log.escribirError("verificaremos unidades", "hola", "InsertarReseiptRawMaterial", "InsertarReseiptRawMaterial");
                if (MyConvertionFactor.FactorD != null || CUNI == STUN)
                {
                    log.escribirError("entramos a: (MyConvertionFactor.FactorD != null || CUNI == STUN)", "hola", "InsertarReseiptRawMaterial", "InsertarReseiptRawMaterial");
                    if (OORG == "2")
                    {
                        log.escribirError("entramos a: if (OORG == '2')", "hola", "InsertarReseiptRawMaterial", "InsertarReseiptRawMaterial");
                        DataTable DTOrdencompra = HttpContext.Current.Session["DTOrdencompra"] == null ? ConsultaOrdencompra(ORNO, PONO, QUANTITYAUX, ITEM, "") : (DataTable)HttpContext.Current.Session["DTOrdencompra"];
                        if (DTOrdencompra.Rows.Count > 0)
                        {
                            log.escribirError("entramos a: if (DTOrdencompra.Rows.Count > 0)", "hola", "InsertarReseiptRawMaterial", "InsertarReseiptRawMaterial");
                            if (HttpContext.Current.Session["DTOrdencompra"] == null) { HttpContext.Current.Session["DTOrdencompra"] = DTOrdencompra; }
                            bool OrdenImportacion = HttpContext.Current.Session["OrdenImportacion"] == null ? false : (bool)HttpContext.Current.Session["OrdenImportacion"];
                            if (HttpContext.Current.Session["OrdenImportacion"] == null)
                            {
                                OrdenImportacion = twhcol130DAL.ConsultaOrdenImportacion(DTOrdencompra.Rows[0]["T$COTP"].ToString()).Rows.Count > 0 ? true : false;
                                HttpContext.Current.Session["OrdenImportacion"] = OrdenImportacion;
                            }
                            int consecutivoPalletID = 0;
                            DataTable DTPalletContinue = twhcol130DAL.PaidMayorwhcol130(ORNO);
                            string SecuenciaPallet = "001";
                            if (DTPalletContinue.Rows.Count > 0)
                            {
                                log.escribirError("entramos a: if (DTPalletContinue.Rows.Count > 0)", "hola", "InsertarReseiptRawMaterial", "InsertarReseiptRawMaterial");
                                foreach (DataRow item in DTPalletContinue.Rows)
                                {
                                    log.escribirError("entramos a: "+ item["T$PAID"].ToString().Trim(), "hola", "InsertarReseiptRawMaterial", "InsertarReseiptRawMaterial");
                                    consecutivoPalletID = Convert.ToInt32(item["T$PAID"].ToString().Trim().Substring(10, 3)) + 1;
                                    if (consecutivoPalletID.ToString().Length == 1)
                                    {
                                        SecuenciaPallet = "00" + consecutivoPalletID;
                                    }
                                    if (consecutivoPalletID.ToString().Length == 2)
                                    {
                                        SecuenciaPallet = "0" + consecutivoPalletID;
                                    }
                                    if (consecutivoPalletID.ToString().Length == 3)
                                    {
                                        SecuenciaPallet = consecutivoPalletID.ToString();
                                    }
                                }

                            }
                            string LOCAL = string.Empty;
                            string PRIORIDAD = string.Empty;
                            Ent_twhcol130131 MyObjError = new Ent_twhcol130131();
                            try
                            {
                                log.escribirError("Entro al segundo try", "Hola", "InsertarReseiptRawMaterial", "InsertarReseiptRawMaterial");
                                string strError = string.Empty;
                                Ent_twhwmd200 OBJ200 = new Ent_twhwmd200 { cwar = DTOrdencompra.Rows[0]["T$CWAR"].ToString().Trim() };

                                DataTable Dttwhwmd200 = HttpContext.Current.Session["Dttwhwmd200"] == null ? twhwmd200.listaRegistro_ObtieneAlmacenLocation(ref OBJ200, ref strError) : (DataTable)HttpContext.Current.Session["Dttwhwmd200"];
                                if (HttpContext.Current.Session["Dttwhwmd200"] == null) { HttpContext.Current.Session["Dttwhwmd200"] = Dttwhwmd200; };

                                if (Dttwhwmd200.Rows.Count > 0)
                                {
                                    log.escribirError("Entro al: Dttwhwmd200.Rows.Count > 0)", "Hola", "InsertarReseiptRawMaterial", "InsertarReseiptRawMaterial");
                                    if (Dttwhwmd200.Rows[0]["LOC"].ToString().Trim() == "1")
                                    {
                                        PRIORIDAD = HttpContext.Current.Session["PRIORIDAD"] == null ? twhcol130DAL.ConsultarPrioridadNativa(DTOrdencompra.Rows[0]["T$CWAR"].ToString().Trim()).Rows[0]["T$PRIO"].ToString().Trim() : HttpContext.Current.Session["PRIORIDAD"].ToString();
                                        LOCAL = HttpContext.Current.Session["LOCAL"] == null ? twhcol130DAL.ConsultarLocationNativa(DTOrdencompra.Rows[0]["T$CWAR"].ToString().Trim(), PRIORIDAD).Rows[0]["T$LOCA"].ToString().Trim() : HttpContext.Current.Session["LOCAL"].ToString();

                                        if (HttpContext.Current.Session["PRIORIDAD"] == null) { HttpContext.Current.Session["PRIORIDAD"] = PRIORIDAD; };
                                        if (HttpContext.Current.Session["LOCAL"] == null) { HttpContext.Current.Session["LOCAL"] = LOCAL; };
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                log.escribirError(ex.Message, "error ", "InsertarReseiptRawMaterial", "InsertarReseiptRawMaterial");

                                MyObjError.error = true;
                                MyObjError.errorMsg = Priorinboundnotfound;
                                return JsonConvert.SerializeObject(MyObjError);
                            }
                            Ent_twhcol130131 MyObj = new Ent_twhcol130131();
                            MyObj.OORG = OORG;// Order type escaneada view 
                            MyObj.ORNO = DTOrdencompra.Rows[0]["T$ORNO"].ToString();
                            MyObj.ITEM = DTOrdencompra.Rows[0]["T$ITEM"].ToString();
                            MyObj.PAID = DTOrdencompra.Rows[0]["T$ORNO"].ToString() + "-" + SecuenciaPallet;
                            MyObj.PONO = DTOrdencompra.Rows[0]["T$PONO"].ToString();
                            MyObj.SEQN = DTOrdencompra.Rows[0]["T$SQNBR"].ToString();
                            MyObj.CLOT = LOT;// lote VIEW
                            MyObj.CWAR = DTOrdencompra.Rows[0]["T$CWAR"].ToString();
                            MyObj.QTYS = (QUANTITY / CiclePrintEnd).ToString("0.0000");// cantidad escaneada view 
                            MyObj.UNIT = STUN;//unit escaneada view
                            MyObj.QTYC = (QUANTITYAUX / CiclePrintEnd).ToString("0.0000");//cantidad escaneada view aplicando factor
                            MyObj.UNIC = CUNI;//unidad view stock
                            MyObj.DATE = DateTime.Now.ToString("dd/MM/yyyy").ToString();//fecha de confirmacion 
                            MyObj.CONF = "1";
                            MyObj.RCNO = " ";//llena baan
                            MyObj.DATR = "01/01/70";//llena baan
                            MyObj.LOCA = LOCAL;// enviamos vacio
                            MyObj.DATL = DateTime.Now.ToString("dd/MM/yyyy").ToString();//llenar con fecha de hoy
                            MyObj.PRNT = "1";// llenar en 1
                            MyObj.DATP = DateTime.Now.ToString("dd/MM/yyyy").ToString();//llena baan
                            MyObj.NPRT = "1";//conteo de reimpresiones 
                            MyObj.LOGN = HttpContext.Current.Session["user"].ToString();// nombre de ususario de la session
                            MyObj.LOGT = " ";//llena baan
                            MyObj.STAT = "0";// LLENAR EN 1  +
                            MyObj.ALLO = "0";
                            MyObj.DSCA = DTOrdencompra.Rows[0]["DSCA"].ToString();
                            MyObj.COTP = DTOrdencompra.Rows[0]["T$COTP"].ToString();
                            MyObj.FIRE = FIRE;
                            MyObj.NAMA = DTOrdencompra.Rows[0]["T$NAMA"].ToString();
                            MyObj.PSLIP = PSLIP.ToUpper();
                            MyObj.PAID_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + DTOrdencompra.Rows[0]["T$ORNO"].ToString() + "-" + SecuenciaPallet + "&code=Code128&dpi=96";
                            MyObj.ORNO_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + DTOrdencompra.Rows[0]["T$ORNO"].ToString() + "&code=Code128&dpi=96";
                            MyObj.ITEM_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + DTOrdencompra.Rows[0]["T$ITEM"].ToString().Trim().ToUpper() + "&code=Code128&dpi=96";
                            MyObj.CLOT_URL = LOT.ToString().Trim() != "" ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + LOT.ToString().Trim().ToUpper() + "&code=Code128&dpi=96" : "";
                            MyObj.QTYC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + (QUANTITYAUX / CiclePrintEnd).ToString("0.0000").Trim().ToUpper() + "&code=Code128&dpi=96";
                            MyObj.UNIC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + STUN.ToString().Trim().ToUpper() + "&code=Code128&dpi=96";
                            string StrInsertMultiple = string.Empty;
                            if (Labels > 10)
                            {
                                if (CurrentPackageCicles == PackageCicles && CurrentIndependentCicles == IndependentCicles)
                                {
                                    PAIDS.Add(UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.PAID + "&code=Code128&dpi=96");
                                    //QUANTITYAUX_COMPLETADA = (Labels*(QUANTITYAUX_COMPLETADA)) + Convert.ToDecimal(MyObj.QTYC);
                                    QUANTITYAUX_COMPLETADA = (Labels * (Convert.ToDecimal(MyObj.QTYC))) + Convert.ToDecimal(MyObj.QTYC);
                                    if (CiclePrintBegin == CiclePrintEnd - 1)
                                    {
                                        decimal QTYCOMPLETADA, QTYFINAL = 0;
                                        decimal LIMITE = 0.05m;
                                        decimal QTYLIMITE = 0;
                                        QTYLIMITE = Convert.ToDecimal(DTOrdencompra.Rows[0]["T$QSTR"].ToString());
                                        if (QUANTITY <= QTYLIMITE)
                                        {
                                            QTYCOMPLETADA = Convert.ToDecimal(DTOrdencompra.Rows[0]["T$QSTK"].ToString()) - QUANTITYAUX_COMPLETADA;
                                            if (QTYCOMPLETADA < LIMITE)
                                            {
                                                QTYFINAL = Convert.ToDecimal(MyObj.QTYC) + QTYCOMPLETADA;
                                                MyObj.QTYC = Math.Abs(Convert.ToDecimal(QTYFINAL.ToString())).ToString();
                                                MyObj.QTYCFinal = Math.Abs(Convert.ToDecimal(QTYFINAL.ToString())).ToString();
                                                HttpContext.Current.Session["QuantityFinal"] = MyObj.QTYCFinal;
                                            }
                                        }
                                    }
                                    StrInsertMultiple += twhcol130DAL.InsertarReseiptRawMaterialComplementMultiInsert(MyObj);
                                    CiclePrintBegin++;
                                }

                                if (CurrentPackageCicles == PackageCicles)
                                {
                                    while (CurrentIndependentCicles < IndependentCicles)
                                    {
                                        StrInsertMultiple += twhcol130DAL.InsertarReseiptRawMaterialComplementMultiInsert(MyObj);
                                        PAIDS.Add(UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.PAID + "&code=Code128&dpi=96");
                                        SecuenciaPallet = (Convert.ToInt32(SecuenciaPallet) + 1).ToString();
                                        MyObj.PAID = MyObj.ORNO + "-" + (SecuenciaPallet.Trim().Length == 1 ? "00" + SecuenciaPallet.Trim() : SecuenciaPallet.Trim().Length == 2 ? "0" + SecuenciaPallet.Trim() : SecuenciaPallet.Trim().Length == 3 ? SecuenciaPallet.Trim() : SecuenciaPallet.Trim());
                                        CiclePrintBegin++;
                                        CurrentIndependentCicles++;
                                    }
                                }

                                if (CurrentPackageCicles < PackageCicles)
                                {
                                    for (int i = 0; i < Packages; i++)
                                    {
                                        StrInsertMultiple += twhcol130DAL.InsertarReseiptRawMaterialComplementMultiInsert(MyObj);
                                        PAIDS.Add(UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.PAID + "&code=Code128&dpi=96");
                                        SecuenciaPallet = (Convert.ToInt32(SecuenciaPallet) + 1).ToString();
                                        MyObj.PAID = MyObj.ORNO + "-" + (SecuenciaPallet.Trim().Length == 1 ? "00" + SecuenciaPallet.Trim() : SecuenciaPallet.Trim().Length == 2 ? "0" + SecuenciaPallet.Trim() : SecuenciaPallet.Trim().Length == 3 ? SecuenciaPallet.Trim() : SecuenciaPallet.Trim());
                                        CiclePrintBegin++;
                                    }
                                    CurrentPackageCicles++;
                                }
                            }
                            else
                            {
                                if (Labels + 1 > 1)
                                {
                                    for (int i = 0; i <= Labels; i++)
                                    {
                                        StrInsertMultiple += twhcol130DAL.InsertarReseiptRawMaterialComplementMultiInsert(MyObj);
                                        PAIDS.Add(UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.PAID + "&code=Code128&dpi=96");
                                        SecuenciaPallet = (Convert.ToInt32(SecuenciaPallet) + 1).ToString();
                                        MyObj.PAID = MyObj.ORNO + "-" + (SecuenciaPallet.Trim().Length == 1 ? "00" + SecuenciaPallet.Trim() : SecuenciaPallet.Trim().Length == 2 ? "0" + SecuenciaPallet.Trim() : SecuenciaPallet.Trim().Length == 3 ? SecuenciaPallet.Trim() : SecuenciaPallet.Trim());
                                        QUANTITYAUX_COMPLETADA = (Labels * (Convert.ToDecimal(MyObj.QTYC))) + Convert.ToDecimal(MyObj.QTYC);
                                        if (CiclePrintBegin == CiclePrintEnd - 1)
                                        {
                                            decimal QTYCOMPLETADA, QTYFINAL = 0;
                                            decimal LIMITE = 0.05m;
                                            decimal QTYLIMITE = 0;
                                            QTYLIMITE = Convert.ToDecimal(DTOrdencompra.Rows[0]["T$QSTR"].ToString());
                                            if (QUANTITY <= QTYLIMITE)
                                            {
                                                QTYCOMPLETADA = Convert.ToDecimal(DTOrdencompra.Rows[0]["T$QSTK"].ToString()) - QUANTITYAUX_COMPLETADA;
                                                if (QTYCOMPLETADA < LIMITE)
                                                {
                                                    QTYFINAL = Convert.ToDecimal(MyObj.QTYC) + QTYCOMPLETADA;
                                                    //MyObj.QTYC = Math.Abs(Convert.ToDecimal(QTYFINAL.ToString())).ToString();
                                                    MyObj.QTYCFinal = Math.Abs(Convert.ToDecimal(QTYFINAL.ToString())).ToString();
                                                    HttpContext.Current.Session["QuantityFinal"] = MyObj.QTYCFinal;
                                                }
                                            }
                                        }
                                        CiclePrintBegin++;
                                    }
                                }
                                else if (Labels + 1 == 1)
                                {
                                    StrInsertMultiple += twhcol130DAL.InsertarReseiptRawMaterialComplementMultiInsert(MyObj);
                                    PAIDS.Add(UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.PAID + "&code=Code128&dpi=96");
                                    SecuenciaPallet = (Convert.ToInt32(SecuenciaPallet) + 1).ToString();
                                    MyObj.PAID = MyObj.ORNO + "-" + (SecuenciaPallet.Trim().Length == 1 ? "00" + SecuenciaPallet.Trim() : SecuenciaPallet.Trim().Length == 2 ? "0" + SecuenciaPallet.Trim() : SecuenciaPallet.Trim().Length == 3 ? SecuenciaPallet.Trim() : SecuenciaPallet.Trim());
                                    CiclePrintBegin++;
                                }
                            }

                            if (OrdenImportacion)
                            {
                                DataTable ConsultaPresupuestoImportacion = twhcol130DAL.ConsultaPresupuestoImportacion(ORNO);
                                if (ConsultaPresupuestoImportacion.Rows.Count > 0 && ConsultaPresupuestoImportacion.Rows[0]["pres"].ToString().Trim() == "3")
                                {
                                    //JC 02092022 Insertar de manera masiva los registros de la importación
                                    //bool Insertsucces = twhcol130DAL.InsertarReseiptRawMaterial(MyObj);

                                    //if (Insertsucces)
                                    //{
                                    //    MyObj.QTYCFinal = HttpContext.Current.Session["QuantityFinal"] != null ? HttpContext.Current.Session["QuantityFinal"].ToString() : "";
                                    //    Retrono = JsonConvert.SerializeObject(MyObj);
                                    //}
                                    //else
                                    //{
                                    //    MyObj.error = true;
                                    //    MyObj.errorMsg = "la insercion fue: " + Insertsucces.ToString();
                                    //    Retrono = JsonConvert.SerializeObject(MyObj);
                                    //}
                                    bool Insertsucces = twhcol130DAL.MultiInsert(StrInsertMultiple);

                                    if (Insertsucces)
                                    {
                                            
                                        MyInsert.Add(MyObj);

                                        if (CiclePrintBegin < CiclePrintEnd && CiclePrintEnd > 1)
                                        {
                                            InsertarReseiptRawMaterial(OORG, ORNO, ITEM, PONO, LOT, QUANTITY, STUN, CUNI, CWAR, FIRE, PSLIP, CICLE, false);
                                        }
                                        else if (CiclePrintEnd == 1)
                                        {
                                            HttpContext.Current.Session["MaterialDesc"] = DTOrdencompra.Rows[0]["DSCA"].ToString();
                                            HttpContext.Current.Session["MaterialCode"] = DTOrdencompra.Rows[0]["T$ITEM"].ToString();
                                            HttpContext.Current.Session["codePaid"] = DTOrdencompra.Rows[0]["T$ORNO"].ToString() + "-" + SecuenciaPallet;
                                            HttpContext.Current.Session["Lot"] = LOT;
                                            HttpContext.Current.Session["Quantity"] = MyObj.QTYC + " " + CUNI;
                                            HttpContext.Current.Session["Origin"] = LOT;
                                            HttpContext.Current.Session["Supplier"] = DTOrdencompra.Rows[0]["T$NAMA"].ToString();
                                            HttpContext.Current.Session["RecibedBy"] = _operator;
                                            HttpContext.Current.Session["RecibedOn"] = DateTime.Now.ToString();
                                            HttpContext.Current.Session["Reprint"] = "no";
                                            HttpContext.Current.Session["AutoPrint"] = "yes";
                                        }
                                        MyObj.PAIDS = PAIDS;
                                        MyObj.QTYCFinal = HttpContext.Current.Session["QuantityFinal"] != null ? HttpContext.Current.Session["QuantityFinal"].ToString() : "";
                                        Retrono = JsonConvert.SerializeObject(MyObj);

                                    }
                                    else
                                    {
                                        MyObj.error = true;
                                        MyObj.errorMsg = "la insercion fue: " + Insertsucces.ToString();
                                        Retrono = JsonConvert.SerializeObject(MyObj);
                                    }
                                }
                                else
                                {
                                    MyObj.error = true;
                                    MyObj.errorMsg = ImportPOBudgetisnotclosedPOcannotberecived;
                                    Retrono = JsonConvert.SerializeObject(MyObj);
                                }
                            }
                            else
                            {

                                bool Insertsucces = twhcol130DAL.MultiInsert(StrInsertMultiple);

                                if (Insertsucces)
                                {

                                    MyInsert.Add(MyObj);

                                    if (CiclePrintBegin < CiclePrintEnd && CiclePrintEnd > 1)
                                    {
                                        InsertarReseiptRawMaterial(OORG, ORNO, ITEM, PONO, LOT, QUANTITY, STUN, CUNI, CWAR, FIRE, PSLIP, CICLE, false);
                                    }
                                    else if (CiclePrintEnd == 1)
                                    {
                                        HttpContext.Current.Session["MaterialDesc"] = DTOrdencompra.Rows[0]["DSCA"].ToString();
                                        HttpContext.Current.Session["MaterialCode"] = DTOrdencompra.Rows[0]["T$ITEM"].ToString();
                                        HttpContext.Current.Session["codePaid"] = DTOrdencompra.Rows[0]["T$ORNO"].ToString() + "-" + SecuenciaPallet;
                                        HttpContext.Current.Session["Lot"] = LOT;
                                        HttpContext.Current.Session["Quantity"] = MyObj.QTYC + " " + CUNI;
                                        HttpContext.Current.Session["Origin"] = LOT;
                                        HttpContext.Current.Session["Supplier"] = DTOrdencompra.Rows[0]["T$NAMA"].ToString();
                                        HttpContext.Current.Session["RecibedBy"] = _operator;
                                        HttpContext.Current.Session["RecibedOn"] = DateTime.Now.ToString();
                                        HttpContext.Current.Session["Reprint"] = "no";
                                        HttpContext.Current.Session["AutoPrint"] = "yes";
                                    }
                                    MyObj.PAIDS = PAIDS;
                                    MyObj.QTYCFinal = HttpContext.Current.Session["QuantityFinal"] != null ? HttpContext.Current.Session["QuantityFinal"].ToString() : "";
                                    Retrono = JsonConvert.SerializeObject(MyObj);

                                }
                                else
                                {
                                    MyObj.error = true;
                                    MyObj.errorMsg = "la insercion fue: " + Insertsucces.ToString();
                                    Retrono = JsonConvert.SerializeObject(MyObj);
                                }
                            }

                        }
                    }
                    else
                    {
                        log.escribirError("No entramos a: if (OORG == '2')", "hola", "InsertarReseiptRawMaterial", "InsertarReseiptRawMaterial");
                        DataTable DTNOOrdencompra = ConsultaNOOrdencompra(ORNO, PONO, OORG, QUANTITY, ITEM, LOT);
                        bool ExistenciaLot = ValidarLote(ITEM, LOT);
                        if (DTNOOrdencompra.Rows.Count > 0 && (ExistenciaLot == true || DTNOOrdencompra.Rows[0]["KLTC"].ToString().Trim() != "1"))
                        {
                            int consecutivoPalletID = 0;
                            DataTable DTPalletContinue = twhcol130DAL.PaidMayorwhcol130(ORNO);
                            string SecuenciaPallet = "001";
                            if (DTPalletContinue.Rows.Count > 0)
                            {
                                foreach (DataRow item in DTPalletContinue.Rows)
                                {
                                    consecutivoPalletID = Convert.ToInt32(item["T$PAID"].ToString().Trim().Substring(10, 3)) + 1;
                                    if (consecutivoPalletID.ToString().Length == 1)
                                    {
                                        SecuenciaPallet = "00" + consecutivoPalletID;
                                    }
                                    if (consecutivoPalletID.ToString().Length == 2)
                                    {
                                        SecuenciaPallet = "0" + consecutivoPalletID;
                                    }
                                    if (consecutivoPalletID.ToString().Length == 3)
                                    {
                                        SecuenciaPallet = consecutivoPalletID.ToString();
                                    }
                                }

                            }

                            string LOCAL = string.Empty;
                            string PRIORIDAD = string.Empty;
                            Ent_twhcol130131 MyObjError = new Ent_twhcol130131();
                            try
                            {
                                PRIORIDAD = twhcol130DAL.ConsultarPrioridadNativa(DTNOOrdencompra.Rows[0]["T$CWAR"].ToString().Trim()).Rows[0]["T$PRIO"].ToString().Trim();
                                LOCAL = twhcol130DAL.ConsultarLocationNativa(DTNOOrdencompra.Rows[0]["T$CWAR"].ToString().Trim()).Rows[0]["T$LOCA"].ToString().Trim();
                            }
                            catch (Exception ex)
                            {
                                //LOCAL = " ";
                                MyObjError.error = true;

                                MyObjError.errorMsg = Priorinboundnotfound;
                                return JsonConvert.SerializeObject(MyObjError);
                            }
                            Ent_twhcol130131 MyObj = new Ent_twhcol130131
                            {
                                OORG = OORG,// Order type escaneada view 
                                ORNO = DTNOOrdencompra.Rows[0]["T$ORNO"].ToString(),
                                ITEM = DTNOOrdencompra.Rows[0]["T$ITEM"].ToString(),
                                PAID = DTNOOrdencompra.Rows[0]["T$ORNO"].ToString() + "-" + SecuenciaPallet,
                                PONO = DTNOOrdencompra.Rows[0]["T$PONO"].ToString(),
                                SEQN = DTNOOrdencompra.Rows[0]["T$SEQN"].ToString(),
                                CLOT = LOT,// lote VIEW
                                CWAR = DTNOOrdencompra.Rows[0]["T$CWAR"].ToString(),
                                QTYS = QUANTITY.ToString("0.0000"),// cantidad escaneada view 
                                UNIT = STUN,//unidad view stock
                                QTYC = QUANTITYAUX.ToString("0.0000"),//cantidad escaneada view aplicando factor
                                UNIC = CUNI,//unit escaneada view
                                DATE = DateTime.Now.ToString("dd/MM/yyyy").ToString(),//fecha de confirmacion 
                                CONF = "1",
                                RCNO = " ",//llena baan
                                DATR = "01/01/70",//llena baan
                                LOCA = LOCAL,// enviamos vacio
                                DATL = DateTime.Now.ToString("dd/MM/yyyy").ToString(),//llenar con fecha de hoy
                                PRNT = "1",// llenar en 1
                                DATP = DateTime.Now.ToString("dd/MM/yyyy").ToString(),//llena baan
                                NPRT = "1",//conteo de reimpresiones 
                                LOGN = HttpContext.Current.Session["user"].ToString(),// nombre de ususario de la session
                                LOGT = " ",//llena baan
                                STAT = "0",// LLENAR EN 1   
                                DSCA = DTNOOrdencompra.Rows[0]["T$DSCA"].ToString(),
                                ALLO = "0",
                                FIRE = FIRE,
                                PAID_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + DTNOOrdencompra.Rows[0]["T$ORNO"].ToString() + "-" + SecuenciaPallet + "&code=Code128&dpi=96",
                                ORNO_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + DTNOOrdencompra.Rows[0]["T$ORNO"].ToString() + "&code=Code128&dpi=96",
                                ITEM_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + DTNOOrdencompra.Rows[0]["T$ITEM"].ToString().Trim().ToUpper() + "&code=Code128&dpi=96",
                                CLOT_URL = LOT.ToString().Trim() != "" ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + LOT.ToString().Trim().ToUpper() + "&code=Code128&dpi=96" : "",
                                QTYC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + QUANTITYAUX.ToString("0.0000").Trim().ToUpper() + "&code=Code128&dpi=96",
                                UNIC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + STUN.ToString().Trim().ToUpper() + "&code=Code128&dpi=96"

                            };
                            bool Insertsucces = twhcol130DAL.InsertarReseiptRawMaterial(MyObj);
                            if (Insertsucces)
                            {
                                MyObj.QTYCFinal = HttpContext.Current.Session["QuantityFinal"] != null ? HttpContext.Current.Session["QuantityFinal"].ToString() : "";
                                Retrono = JsonConvert.SerializeObject(MyObj);
                            }
                            else
                            {
                                MyObj.error = true;
                                MyObj.errorMsg = "la insercion fue:" + Insertsucces.ToString();
                                Retrono = JsonConvert.SerializeObject(MyObj);
                            }
                        }
                        else
                        {
                            Ent_twhcol130131 MyObj = new Ent_twhcol130131
                            {
                                error = true,

                                errorMsg = Thelotdoesnotcorrespondtotheorder
                            };

                            Retrono = JsonConvert.SerializeObject(MyObj);
                        }
                    }
                }
                return Retrono;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
                string strError = "Error al intentar inseretar";
                log.escribirError(ex.Message,"Error al intentar inseretar","Error al intentar inseretar", "Error al intentar inseretar");
            }
        }

        public string ConsultaUnidadesMedida()
        {
            DataTable DTUnidades = twhcol130DAL.ConsultaUnidadesMedida();

            DdUnit.DataSource = DTUnidades;
            DdUnit.DataTextField = "DSCA";
            DdUnit.DataValueField = "CUNI";
            DdUnit.Items.Add("-- Select Unit --");
            DdUnit.DataBind();


            List<Unidad> LstUnidadesMedida = new List<Unidad>();

            foreach (DataRow IUnidad in DTUnidades.Rows)
            {
                Unidad unidad = new Unidad();
                unidad.CUNI = IUnidad["CUNI"].ToString();
                unidad.DSCA = IUnidad["DSCA"].ToString();

                LstUnidadesMedida.Add(unidad);
            }

            return JsonConvert.SerializeObject(LstUnidadesMedida);
        }

        //public void ListasOdenCompra()
        //{
        //    List<DataTable> LstDataTable = twhcol130DAL.ListasOrderType();

        //    DataTable DtSalesOrder = LstDataTable[0];
        //    DataTable DtListaTransferOrder = LstDataTable[1];
        //    DataTable DtListaPurchaseOrders = LstDataTable[2];

        //    List<twhinh210> LstSalesOrder = new List<twhinh210>();
        //    List<twhinh210> LstTransferOrder = new List<twhinh210>();
        //    List<twhinh210> LstPurchaseOrders = new List<twhinh210>();


        //    foreach (DataRow row in DtSalesOrder.Rows)
        //    {
        //        twhinh210 ttwhinh210 = new twhinh210();
        //        ttwhinh210.TERM = row["TERM"].ToString().Trim();
        //        ttwhinh210.ORNO = row["ORNO"].ToString().Trim();
        //        ttwhinh210.ITEM = row["ITEM"].ToString().Trim();
        //        ttwhinh210.KLTC = row["KLTC"].ToString().Trim();
        //        ttwhinh210.PONO = row["PONO"].ToString().Trim();
        //        ttwhinh210.STUN = row["STUN"].ToString().Trim();
        //        ttwhinh210.CUNI = row["CUNI"].ToString().Trim();
        //        ttwhinh210.CWAR = row["CWAR"].ToString().Trim();
        //        ttwhinh210.OQUA = row["QSTR"].ToString().Trim();
        //        ttwhinh210.LSEL = row["LSEL"].ToString().Trim();
        //        ttwhinh210.LSEL = row["CLOT"].ToString().Trim();
        //        //ttwhinh210.RTQP = row["RTQP"].ToString().Trim();
        //        ttwhinh210.DSCA = row["DSCA"].ToString().Trim().Replace("\"", string.Empty).Replace("'", "");
        //        ttwhinh210.PRDT = row["PRDT"].ToString().Trim();
        //        ttwhinh210.QSTR = row["QSTR"].ToString().Trim();

        //        LstSalesOrder.Add(ttwhinh210);
        //    }

        //    foreach (DataRow row in DtListaTransferOrder.Rows)
        //    {
        //        twhinh210 ttwhinh210 = new twhinh210();

        //        ttwhinh210.TERM = row["TERM"].ToString().Trim();
        //        ttwhinh210.ORNO = row["ORNO"].ToString().Trim();
        //        ttwhinh210.ITEM = row["ITEM"].ToString().Trim();
        //        ttwhinh210.KLTC = row["KLTC"].ToString().Trim();
        //        ttwhinh210.PONO = row["PONO"].ToString().Trim();
        //        ttwhinh210.STUN = row["STUN"].ToString().Trim();
        //        ttwhinh210.CUNI = row["CUNI"].ToString().Trim();
        //        ttwhinh210.CWAR = row["CWAR"].ToString().Trim();
        //        ttwhinh210.OQUA = row["QSTR"].ToString().Trim();
        //        ttwhinh210.LSEL = row["LSEL"].ToString().Trim();
        //        ttwhinh210.LSEL = row["CLOT"].ToString().Trim();
        //        ttwhinh210.DSCA = row["DSCA"].ToString().Trim().Replace("\"", string.Empty).Replace("'", "");
        //        ttwhinh210.PRDT = row["PRDT"].ToString().Trim();
        //        ttwhinh210.QSTR = row["QSTR"].ToString().Trim();
        //        LstTransferOrder.Add(ttwhinh210);
        //    }

        //    foreach (DataRow row in DtListaPurchaseOrders.Rows)
        //    {

        //        twhinh210 ttwhinh210 = new twhinh210();
        //        ttwhinh210.OQUA = row["OQUA"].ToString().Trim();
        //        ttwhinh210.TERM = AsignadorTerm(row["RTDP"].ToString().Trim(), row["RTDM"].ToString().Trim(), row["TERM"].ToString().Trim(), Convert.ToDateTime(row["PRDT"]));
        //        ttwhinh210.ORNO = row["ORNO"].ToString().Trim();
        //        ttwhinh210.ITEM = row["ITEM"].ToString().Trim();
        //        ttwhinh210.KLTC = row["KLTC"].ToString().Trim();
        //        ttwhinh210.PONO = row["PONO"].ToString().Trim();
        //        ttwhinh210.STUN = row["STUN"].ToString().Trim();
        //        ttwhinh210.CUNI = row["CUNI"].ToString().Trim();
        //        ttwhinh210.CWAR = row["CWAR"].ToString().Trim();
        //        ttwhinh210.RTQP = row["RTQP"].ToString().Trim();
        //        ttwhinh210.SEQNR = row["SEQNR"].ToString().Trim();
        //        ttwhinh210.DSCA = row["DSCA"].ToString().Trim().Replace("\"", string.Empty).Replace("'", "");
        //        ttwhinh210.PRDT = row["PRDT"].ToString().Trim();
        //        ttwhinh210.QSTR = row["QSTR"].ToString().Trim();



        //        LstPurchaseOrders.Add(ttwhinh210);
        //    }

        //    LstSalesOrderJSON = JsonConvert.SerializeObject(LstSalesOrder);
        //    LstTransferOrderJSON = JsonConvert.SerializeObject(LstTransferOrder);
        //    LstPurchaseOrdersJSON = JsonConvert.SerializeObject(LstPurchaseOrders);

        //}


        public static string AsignadorTerm(string RTDP, string RTDM, string TERM, DateTime PRDT)
        {
            string retorno = "1";
            DateTime FechaRegistro = PRDT;
            DateTime FechaMaxima = new DateTime();
            DateTime FechaMinima = new DateTime();
            DateTime FechaActual = DateTime.Now;
            double DiaMax = 0;
            double DiaMin = 0;
            if (TERM == "0")
            {
                if (RTDP == "0" || RTDP == "null" || RTDP == null)
                {

                    DiaMin = Convert.ToDouble(ToleranciaMinimaDias.ToString().Trim());
                    FechaMinima = FechaActual.AddDays(-DiaMin); //JC150722  Así toma de manera correcta las fechas para la recepción
                }
                //JC150722  Así toma de manera correcta las fechas para la recepción
                else
                {
                    FechaMinima = FechaActual.AddDays(-Convert.ToDouble(RTDM));
                }

                if (RTDM == "0" || RTDM == "null" || RTDM == null)
                {
                    DiaMax = Convert.ToDouble(ToleranciaMaximaDias.ToString().Trim());
                    FechaMaxima = FechaActual.AddDays(DiaMax); //JC150722  Así toma de manera correcta las fechas para la recepción
                }
                //JC150722  Así toma de manera correcta las fechas para la recepción
                else
                {
                    FechaMaxima = FechaActual.AddDays(Convert.ToDouble(RTDP));
                }

                //FechaMaxima = FechaActual.AddDays(DiaMax);
                //FechaMinima = FechaActual.AddDays(-DiaMin);

                if (FechaMaxima >= FechaRegistro && FechaRegistro >= FechaMinima)
                {
                    retorno = "1";
                }
                else
                {
                    retorno = "0";
                }

            }
            return retorno;
        }

        [WebMethod]
        public static string ListasOdenCompra(string ORNO, string TYPE_ORNO)
        {
            List<string> listJson = new List<string>();
            List<DataTable> LstDataTable = twhcol130DAL.ListasOrderType(ORNO, TYPE_ORNO);

            DataTable DtSalesOrder = LstDataTable[0];
            DataTable DtListaTransferOrder = LstDataTable[1];
            DataTable DtListaPurchaseOrders = LstDataTable[2];

            List<twhinh210> LstSalesOrder = new List<twhinh210>();
            List<twhinh210> LstTransferOrder = new List<twhinh210>();
            List<twhinh210> LstPurchaseOrders = new List<twhinh210>();

            if (DtSalesOrder.Rows.Count > 0)
            {
                foreach (DataRow row in DtSalesOrder.Rows)
                {
                    twhinh210 ttwhinh210 = new twhinh210();
                    ttwhinh210.TERM = row["TERM"].ToString().Trim();
                    ttwhinh210.ORNO = row["ORNO"].ToString().Trim();
                    ttwhinh210.ITEM = row["ITEM"].ToString().Trim();
                    ttwhinh210.KLTC = row["KLTC"].ToString().Trim();
                    ttwhinh210.PONO = row["PONO"].ToString().Trim();
                    ttwhinh210.STUN = row["STUN"].ToString().Trim();
                    ttwhinh210.CUNI = row["CUNI"].ToString().Trim();
                    ttwhinh210.CWAR = row["CWAR"].ToString().Trim();
                    ttwhinh210.OQUA = row["QSTR"].ToString().Trim();
                    ttwhinh210.LSEL = row["LSEL"].ToString().Trim();
                    ttwhinh210.LSEL = row["CLOT"].ToString().Trim();
                    //ttwhinh210.RTQP = row["RTQP"].ToString().Trim();
                    ttwhinh210.DSCA = row["DSCA"].ToString().Trim().Replace("\"", string.Empty).Replace("'", "");
                    ttwhinh210.PRDT = row["PRDT"].ToString().Trim();
                    ttwhinh210.QSTR = row["QSTR"].ToString().Trim();

                    LstSalesOrder.Add(ttwhinh210);
                }
            }
            if (DtListaTransferOrder.Rows.Count > 0)
            {
                foreach (DataRow row in DtListaTransferOrder.Rows)
                {
                    twhinh210 ttwhinh210 = new twhinh210();

                    ttwhinh210.TERM = row["TERM"].ToString().Trim();
                    ttwhinh210.ORNO = row["ORNO"].ToString().Trim();
                    ttwhinh210.ITEM = row["ITEM"].ToString().Trim();
                    ttwhinh210.KLTC = row["KLTC"].ToString().Trim();
                    ttwhinh210.PONO = row["PONO"].ToString().Trim();
                    ttwhinh210.STUN = row["STUN"].ToString().Trim();
                    ttwhinh210.CUNI = row["CUNI"].ToString().Trim();
                    ttwhinh210.CWAR = row["CWAR"].ToString().Trim();
                    ttwhinh210.OQUA = row["QSTR"].ToString().Trim();
                    ttwhinh210.LSEL = row["LSEL"].ToString().Trim();
                    ttwhinh210.LSEL = row["CLOT"].ToString().Trim();
                    ttwhinh210.DSCA = row["DSCA"].ToString().Trim().Replace("\"", string.Empty).Replace("'", "");
                    ttwhinh210.PRDT = row["PRDT"].ToString().Trim();
                    ttwhinh210.QSTR = row["QSTR"].ToString().Trim();
                    LstTransferOrder.Add(ttwhinh210);
                }
            }
            if (DtListaPurchaseOrders.Rows.Count > 0)
            {
                foreach (DataRow row in DtListaPurchaseOrders.Rows)
                {

                    twhinh210 ttwhinh210 = new twhinh210();
                    ttwhinh210.OQUA = row["OQUA"].ToString().Trim();
                    ttwhinh210.TERM = AsignadorTerm(row["RTDP"].ToString().Trim(), row["RTDM"].ToString().Trim(), row["TERM"].ToString().Trim(), Convert.ToDateTime(row["PRDT"]));
                    ttwhinh210.ORNO = row["ORNO"].ToString().Trim();
                    ttwhinh210.ITEM = row["ITEM"].ToString().Trim();
                    ttwhinh210.KLTC = row["KLTC"].ToString().Trim();
                    ttwhinh210.PONO = row["PONO"].ToString().Trim();
                    ttwhinh210.STUN = row["STUN"].ToString().Trim();
                    ttwhinh210.CUNI = row["CUNI"].ToString().Trim();
                    ttwhinh210.CWAR = row["CWAR"].ToString().Trim();
                    ttwhinh210.RTQP = row["RTQP"].ToString().Trim();
                    ttwhinh210.SEQNR = row["SEQNR"].ToString().Trim();
                    ttwhinh210.DSCA = row["DSCA"].ToString().Trim().Replace("\"", string.Empty).Replace("'", "");
                    ttwhinh210.PRDT = row["PRDT"].ToString().Trim();
                    ttwhinh210.QSTR = row["QSTR"].ToString().Trim();



                    LstPurchaseOrders.Add(ttwhinh210);
                }
            }

            //HttpContext.Current.Session["LstSalesOrderJSON"] = JsonConvert.SerializeObject(LstSalesOrder);
            //HttpContext.Current.Session["LstTransferOrderJSON"] = JsonConvert.SerializeObject(LstTransferOrder);
            //HttpContext.Current.Session["LstPurchaseOrdersJSON"] = JsonConvert.SerializeObject(LstPurchaseOrders);
            listJson.Add(JsonConvert.SerializeObject(LstSalesOrder));
            listJson.Add(JsonConvert.SerializeObject(LstTransferOrder));
            listJson.Add(JsonConvert.SerializeObject(LstPurchaseOrders));

            return JsonConvert.SerializeObject(listJson);
        }



        [WebMethod]
        public static string ValidarOrderID(string ORNO)
        {
            Ent_twhcol130 twhcol130 = new Ent_twhcol130();
            twhcol130.ORNO = ORNO;
            DataTable consulta = twhcol130DAL.ValidarOrderID(twhcol130);
            twhinh210 twhinh210 = new twhinh210();
            if (consulta.Rows.Count > 0)
            {
                foreach (DataRow row in consulta.Rows)
                {
                    twhinh210.ORNO = row["ORNO"].ToString().Trim();
                    twhinh210.STUN = row["STUN"].ToString().Trim();

                }
            }
            return JsonConvert.SerializeObject(twhinh210);
        }

        [WebMethod]
        public static string ValidarItem(string ITEM, string ORNO)
        {
            Ent_twhcol130 twhcol130 = new Ent_twhcol130();
            twhcol130.ITEM = ITEM;
            twhcol130.ORNO = ORNO;
            DataTable consulta = twhcol130DAL.ValidarItem(twhcol130);

            tcibd001 tcibd001 = new tcibd001();
            if (consulta.Rows.Count > 0)
            {

                foreach (DataRow row in consulta.Rows)
                {
                    tcibd001.ITEM = row["ITEM"].ToString().Trim();
                    tcibd001.KLTC = row["KLTC"].ToString().Trim();
                    tcibd001.CUNI = row["CUNI"].ToString().Trim();
                }

            }
            return JsonConvert.SerializeObject(tcibd001);
        }


        [WebMethod]
        public static bool ValidarLote(string ITEM, string CLOT)
        {
            bool retorno = false;
            Ent_twhcol130 twhcol130 = new Ent_twhcol130();
            twhcol130.ITEM = ITEM;
            twhcol130.CLOT = CLOT;
            DataTable DtLote = twhcol130DAL.ValidarLote(twhcol130);
            if (DtLote.Rows.Count > 0)
            {
                retorno = true;
            }
            return retorno;
        }

        [WebMethod]
        public static string ConsultafactoresporItem(string ITEM)
        {
            List<Factor> lstFatoresItemError = new List<Factor>();

            Factor MyobjetNew = new Factor
            {
                Error = true,

                ErrorMsg = "No tiene factor de Conversion"
            };

            lstFatoresItemError.Add(MyobjetNew);

            string retorno = JsonConvert.SerializeObject(lstFatoresItemError);
            DataTable DTFactoresItem = twhcol130DAL.ConsultafactoresporItem(ITEM);
            List<Factor> lstFatoresItem = new List<Factor>();

            if (DTFactoresItem.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTFactoresItem.Rows)
                {

                    Factor Myobjet = new Factor
                    {

                        BASU = MyRow["BASU"].ToString(),
                        UNIT = MyRow["UNIT"].ToString(),
                        POTENCIA = MyRow["POTENCIA"].ToString(),
                        FACTOR = MyRow["FACTOR"].ToString()
                    };

                    lstFatoresItem.Add(Myobjet);
                }
                retorno = JsonConvert.SerializeObject(lstFatoresItem);
            }
            return retorno;
        }

        [WebMethod]
        public static string ConsultarSumatoriaCantidades130(string ORNO, string PONO, string SEQNR)
        {
            return twhcol130DAL.ConsultarSumatoriaCantidades130(ORNO, PONO, SEQNR);
        }

        [WebMethod]
        public static string ConsultarSumatoriaCantidades130(string ORNO, string PONO)
        {
            return twhcol130DAL.ConsultarSumatoriaCantidades130(ORNO, PONO);
        }

        public static Factor FactorConversion(string ITEM, string STUN, string CUNI)
        {
            Factor MyFactor = new Factor
            {
                MsgError = "No Tiene Factor",
                FactorD = null,
                Tipo = string.Empty
            };

            DataTable DtFactor = new DataTable();
            DataTable ConvercionDiv = twhcol130DAL.FactorConvercionMul(ITEM, CUNI, STUN);
            DataTable ConvercionMul = twhcol130DAL.FactorConvercionDiv(ITEM, CUNI, STUN);

            if (ConvercionDiv.Rows.Count > 0)
            {
                MyFactor.MsgError = string.Empty;
                MyFactor.Tipo = "Div";
                MyFactor.FactorD = (decimal?)ConvercionDiv.Rows[0]["FACTOR"];
                MyFactor.FactorB = (decimal?)ConvercionDiv.Rows[0]["POTENCIA"];
            }
            else if (ConvercionMul.Rows.Count > 0)
            {
                MyFactor.MsgError = string.Empty;
                MyFactor.Tipo = "Mul";
                MyFactor.FactorD = (decimal?)ConvercionMul.Rows[0]["FACTOR"];
                MyFactor.FactorB = (decimal?)ConvercionMul.Rows[0]["POTENCIA"];
            }
            else if (ConvercionDiv.Rows.Count == 0 && ConvercionMul.Rows.Count == 0)
            {
                MyFactor = FactorConversion(string.Empty, STUN, CUNI);
                return MyFactor;
            }

            return MyFactor;

        }

        public static DataTable ConsultaNOOrdencompra(string ORNO, string PONO, string OORG, decimal CANT, string ITEM, string CLOT)
        {
            DataTable consulta = new DataTable();
            consulta = twhcol130DAL.ConsultaNOOrdencompra(ORNO, PONO, OORG, CANT, ITEM, CLOT);
            return consulta;
        }

        public static DataTable ConsultaOrdencompra(string ORNO, string PONO, decimal CANT, string ITEM, string CLOT)
        {
            DataTable consulta = new DataTable();
            consulta = twhcol130DAL.ConsultaOrdencompra(ORNO, PONO, CANT, ITEM, CLOT);
            return consulta;
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
            var retorno = mensajesForm.readStatement("whInvReceiptRawMaterialNew.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }

    }
}