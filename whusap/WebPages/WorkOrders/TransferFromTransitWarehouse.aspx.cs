using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using System.Threading;
using System.Web.Services;
using whusa.Entidades;
using whusa;
using System.Data;
using System.Globalization;
using System.Web.Configuration;
using Newtonsoft.Json;
using System.Configuration;
using whusa.Utilidades;

namespace whusap.WebPages.WorkOrders
{
    public partial class TransferFromTransitWarehouse : System.Web.UI.Page
    {
        public static IntefazDAL_transfer Transfers = new IntefazDAL_transfer();
        public static string RequestUrlAuthority = string.Empty;
        //private static LabelsText _textoLabels = new LabelsText();
        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        public static string PCLOT = string.Empty;
        private static string globalMessages = "GlobalMessages";
        public static string PalletIDdoesntexist = mensajes("PalletIDdoesntexist");
        public static string ItemcodeisnotPurchaseType = mensajes("ItemcodeisnotPurchaseType");
        public static string Lotcodedoesntexist = mensajes("Lotcodedoesntexist");
        public static string Warehousecodedoesntexist = mensajes("Warehousecodedoesntexist");
        public static string Locationblockedinbound = mensajes("Locationblockedinbound");
        public static string Locationcodedoesntexist = mensajes("Locationcodedoesntexist");
        public static string RegisteredquantitynotavilableonBaaninventory = mensajes("RegisteredquantitynotavilableonBaaninventory");

        public static string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
        private static InterfazDAL_twhcol019 _idaltwhcol019 = new InterfazDAL_twhcol019();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        public static InterfazDAL_ttcibd001 ITtcibd001 = new InterfazDAL_ttcibd001();
        public static InterfazDAL_tticol125 ITticol125 = new InterfazDAL_tticol125();
        public static InterfazDAL_ttwhcol016 ITtwhcol016 = new InterfazDAL_ttwhcol016();
        public static InterfazDAL_twhwmd200 ITwhwmd200 = new InterfazDAL_twhwmd200();
        public static IntefazDAL_transfer Itransfer = new IntefazDAL_transfer();
        public static InterfazDAL_twhinr140 ITtwhinr140 = new InterfazDAL_twhinr140();
        private static InterfazDAL_twhcol130 _idaltwhcol131 = new InterfazDAL_twhcol130();

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Session["MyPalletTwhcol13"] = null;
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
        public static string VerificarPalletID(string PAID)
        {
            HttpContext.Current.Session["MyPalletTwhcol13"] = null;
            string strError = string.Empty;
            DataTable DTPalletID = twhcol130DAL.VerificarPalletIDTwhcol131(ref PAID);
            Ent_twhcol130131 Obj131 = new Ent_twhcol130131();

            if (DTPalletID.Rows.Count > 0)
            {
                Obj131.MCNO = DTPalletID.Rows[0]["MCNO"].ToString();
                Obj131.DSCA = DTPalletID.Rows[0]["DSCA"].ToString();
                Obj131.KLTC = DTPalletID.Rows[0]["KLTC"].ToString();
                Obj131.SLOC = DTPalletID.Rows[0]["SLOC"].ToString();
                Obj131.DESCAW = DTPalletID.Rows[0]["DESCAW"].ToString();
                Obj131.OORG = DTPalletID.Rows[0]["OORG"].ToString();
                Obj131.ORNO = DTPalletID.Rows[0]["ORNO"].ToString();
                Obj131.ITEM = DTPalletID.Rows[0]["ITEM"].ToString();
                Obj131.PAID = DTPalletID.Rows[0]["PAID"].ToString();
                Obj131.PONO = DTPalletID.Rows[0]["PONO"].ToString();
                Obj131.SEQN = DTPalletID.Rows[0]["SEQN"].ToString();
                Obj131.LOT = DTPalletID.Rows[0]["LOT"].ToString();
                Obj131.CWAR = DTPalletID.Rows[0]["CWAR"].ToString();
                Obj131.QTYS = DTPalletID.Rows[0]["QTYS"].ToString();
                Obj131.UNIT = DTPalletID.Rows[0]["UNIT"].ToString();
                Obj131.QTYC = DTPalletID.Rows[0]["QTYC"].ToString();
                Obj131.UNIC = DTPalletID.Rows[0]["UNIC"].ToString();
                Obj131.DATT = DTPalletID.Rows[0]["DATT"].ToString();
                Obj131.CONF = DTPalletID.Rows[0]["CONF"].ToString();
                Obj131.RCNO = DTPalletID.Rows[0]["RCNO"].ToString();
                Obj131.DATR = DTPalletID.Rows[0]["DATR"].ToString();
                Obj131.LOCA = DTPalletID.Rows[0]["LOCA"].ToString();
                Obj131.DATL = DTPalletID.Rows[0]["DATL"].ToString();
                Obj131.PICK = DTPalletID.Rows[0]["PICK"].ToString();
                Obj131.DATK = DTPalletID.Rows[0]["DATK"].ToString();
                Obj131.PRNT = DTPalletID.Rows[0]["PRNT"].ToString();
                Obj131.DATP = DTPalletID.Rows[0]["DATP"].ToString();
                Obj131.NPRT = DTPalletID.Rows[0]["NPRT"].ToString();
                Obj131.DRPT = DTPalletID.Rows[0]["DRPT"].ToString();
                Obj131.LOGR = DTPalletID.Rows[0]["LOGR"].ToString();
                Obj131.LOGN = DTPalletID.Rows[0]["LOGN"].ToString();
                Obj131.LOGP = DTPalletID.Rows[0]["LOGP"].ToString();
                Obj131.LOGT = DTPalletID.Rows[0]["LOGT"].ToString();
                Obj131.DAFR = DTPalletID.Rows[0]["DAFR"].ToString();
                Obj131.MES1 = DTPalletID.Rows[0]["MES1"].ToString();
                Obj131.DAFL = DTPalletID.Rows[0]["DAFL"].ToString();
                Obj131.MES2 = DTPalletID.Rows[0]["MES2"].ToString();
                Obj131.FIRE = DTPalletID.Rows[0]["FIRE"].ToString();
                Obj131.CWAA = DTPalletID.Rows[0]["CWAA"].ToString();
                Obj131.LOAA = DTPalletID.Rows[0]["LOAA"].ToString();
                Obj131.QTYT = DTPalletID.Rows[0]["QTYT"].ToString();
                Obj131.STAT = DTPalletID.Rows[0]["STAT"].ToString();
                Obj131.PSNO = DTPalletID.Rows[0]["PSNO"].ToString();
                Obj131.ALLO = DTPalletID.Rows[0]["ALLO"].ToString();
                Obj131.REFCNTD = DTPalletID.Rows[0]["REFCNTD"].ToString();
                Obj131.REFCNTU = DTPalletID.Rows[0]["REFCNTU"].ToString();
                Obj131.error = false;

                HttpContext.Current.Session["MyPalletTwhcol13"] = Obj131;
            }
            else
            {
                Obj131.error = true;
                Obj131.typeMsgJs = "label";

                Obj131.errorMsg = PalletIDdoesntexist;
            }


            return JsonConvert.SerializeObject(Obj131);
        }

        public static string VerificarItem(string ITEM, string CLOT)
        {

            Ent_ttcibd001 ObjTtcibd001 = new Ent_ttcibd001();

            DataTable dtTtcibd001 = ITtcibd001.findItem(ITEM);
            if (dtTtcibd001.Rows.Count > 0)
            {
                ObjTtcibd001.item = dtTtcibd001.Rows[0]["ITEM"].ToString();
                ObjTtcibd001.dsca = dtTtcibd001.Rows[0]["DSCA"].ToString();
                ObjTtcibd001.cuni = dtTtcibd001.Rows[0]["CUNI"].ToString();
                ObjTtcibd001.kltc = dtTtcibd001.Rows[0]["KLTC"].ToString();
                ObjTtcibd001.kitm = dtTtcibd001.Rows[0]["KITM"].ToString();

                if (ObjTtcibd001.kltc.Trim() == "1")
                {
                    PCLOT = CLOT;
                }

                if (ObjTtcibd001.kitm.Trim() == "1")
                {
                    ObjTtcibd001.Error = true;
                    ObjTtcibd001.TypeMsgJs = "label";

                    ObjTtcibd001.ErrorMsg = ItemcodeisnotPurchaseType;
                }
                else
                {
                    ObjTtcibd001.Error = false;
                    ObjTtcibd001.TypeMsgJs = "console";
                    ObjTtcibd001.SuccessMsg = "Item Encontrado";
                }
            }
            else
            {
                ObjTtcibd001.Error = true;
                ObjTtcibd001.TypeMsgJs = "label";

                ObjTtcibd001.ErrorMsg = ItemcodeisnotPurchaseType;
            }



            return JsonConvert.SerializeObject(ObjTtcibd001);
        }

        [WebMethod]
        public static string VerificarLote(string ITEM, string CLOT)
        {
            string strError = string.Empty;

            Ent_tticol125 Obj_tticol125 = new Ent_tticol125();
            Obj_tticol125.item = ITEM;
            Obj_tticol125.clot = CLOT;

            DataTable DtTticol125 = ITticol125.listaRegistrosLoteItem_Param(ref Obj_tticol125);

            if (DtTticol125.Rows.Count > 0)
            {
                Obj_tticol125.Error = false;
                Obj_tticol125.TypeMsgJs = "console";
                Obj_tticol125.SuccessMsg = "Lote Encontrado";
            }
            else
            {
                Obj_tticol125.Error = true;
                Obj_tticol125.TypeMsgJs = "label";

                Obj_tticol125.ErrorMsg = Lotcodedoesntexist;

            }

            return JsonConvert.SerializeObject(Obj_tticol125);


        }
        [WebMethod]
        public static string VerificarWarehouse(string CWAR)
        {

            string strError = string.Empty;

            Ent_ttwhcol016 Obj_twhcol016 = new Ent_ttwhcol016();
            Obj_twhcol016.cwar = CWAR;

            Ent_twhwmd200 Obj_twhwmd200 = new Ent_twhwmd200();
            Obj_twhwmd200.cwar = CWAR;

            DataTable DtTtwhcol016 = ITtwhcol016.TakeMaterialInv_verificaBodega_Param(ref Obj_twhcol016, ref strError);
            DataTable DtTwhwmd200 = ITwhwmd200.listaRegistro_ObtieneAlmacenLocation(ref Obj_twhwmd200, ref strError);

            if (DtTtwhcol016.Rows.Count > 0)
            {
                Obj_twhcol016.Error = false;
                Obj_twhcol016.TypeMsgJs = "console";
                Obj_twhcol016.SuccessMsg = "Warehouse Encontrado";

                if (DtTwhwmd200.Rows.Count > 0)
                {
                    Obj_twhcol016.sloc = DtTwhwmd200.Rows[0]["LOC"].ToString();
                }
                else
                {
                    Obj_twhcol016.sloc = string.Empty;
                }
            }
            else
            {
                Obj_twhcol016.Error = true;
                Obj_twhcol016.TypeMsgJs = "label";

                Obj_twhcol016.ErrorMsg = Warehousecodedoesntexist;
            }

            return JsonConvert.SerializeObject(Obj_twhcol016);
        }

        [WebMethod]
        public static string VerificarLocation(string CWAR, string LOCA)
        {


            string strError = string.Empty;
            Ent_twhwmd200 Obj_twhwmd200 = new Ent_twhwmd200();
            Obj_twhwmd200.cwar = CWAR;

            DataTable DtTransfer = Itransfer.ConsultarLocation(Obj_twhwmd200.cwar, LOCA);

            if (DtTransfer.Rows.Count > 0)
            {
                if (DtTransfer.Rows[0]["LOCT"].ToString() == "5")
                {
                    if (DtTransfer.Rows[0]["BINB"].ToString() == "2")
                    {
                        Obj_twhwmd200.Error = false;
                        Obj_twhwmd200.TypeMsgJs = "console";
                        Obj_twhwmd200.SuccessMsg = "Location Encontrado";
                    }
                    else
                    {
                        Obj_twhwmd200.Error = true;
                        Obj_twhwmd200.TypeMsgJs = "label";

                        Obj_twhwmd200.ErrorMsg = Locationblockedinbound;
                    }
                }
                else
                {
                    Obj_twhwmd200.Error = true;
                    Obj_twhwmd200.TypeMsgJs = "label";

                    Obj_twhwmd200.ErrorMsg = Locationcodedoesntexist;
                }

            }
            else
            {
                Obj_twhwmd200.Error = true;
                Obj_twhwmd200.TypeMsgJs = "label";

                Obj_twhwmd200.ErrorMsg = Locationcodedoesntexist;
            }

            return JsonConvert.SerializeObject(Obj_twhwmd200);


        }
        [WebMethod]
        public static string VerificarQuantity(string CWAR, string ITEM, string LOCA = " ", string CLOT = " ")
        {
            string strError = string.Empty;
            DataTable DtTtwhinr140 = ITtwhinr140.consultaPorAlmacenItemUbicacionLote(ref CWAR, ref ITEM, ref LOCA, ref PCLOT, ref strError);
            Ent_twhinr140 ObjTtwhinr140 = new Ent_twhinr140();

            if (DtTtwhinr140.Rows.Count > 0)
            {
                if (Convert.ToInt32(DtTtwhinr140.Rows[0]["STKS"].ToString()) < 1)
                {
                    ObjTtwhinr140.Error = true;
                    ObjTtwhinr140.TypeMsgJs = "label";

                    ObjTtwhinr140.ErrorMsg = RegisteredquantitynotavilableonBaaninventory;
                }
                else
                {
                    ObjTtwhinr140.stks = Convert.ToInt32(DtTtwhinr140.Rows[0]["STKS"].ToString());
                    ObjTtwhinr140.Error = false;
                    ObjTtwhinr140.TypeMsgJs = "label";

                    ObjTtwhinr140.SuccessMsg = RegisteredquantitynotavilableonBaaninventory;
                }

            }
            else
            {
                ObjTtwhinr140.Error = true;
                ObjTtwhinr140.TypeMsgJs = "label";

                ObjTtwhinr140.ErrorMsg = RegisteredquantitynotavilableonBaaninventory;
            }

            return JsonConvert.SerializeObject(ObjTtwhinr140);

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

        [WebMethod]
        public static string Click_Transfer(string QtyReal, string Paids, string TargetCwar, string TargetLoca)
        {
            Ent_twhcol130131 MyObj131Base = (Ent_twhcol130131)HttpContext.Current.Session["MyPalletTwhcol13"];
            MyObj131Base.PAIDS_URLS.Clear();
            MyObj131Base.PAIDS.Clear();
            Ent_twhcol130131 MyObj = (Ent_twhcol130131)MyObj131Base.clone();
            Double qtyReal = Convert.ToDouble(QtyReal);
            Double QtyPallets = Convert.ToDouble(Paids.Trim() == string.Empty ? "1" : Paids.Trim());
            Double Parcials = qtyReal / QtyPallets;
            Decimal QUANTITY = 0;
            int CantPalletsComp = Convert.ToInt32(Parcials);
            double CantParcPallets = Parcials - CantPalletsComp;
            if (CantParcPallets == 0)
            {
                CantParcPallets = 0;
            }
            else
            {
                CantParcPallets = 1;
            }
            int inserts = 0;
            int consecutivoPalletID = 0;
            int QUANTITYAUX_COMPLETADA = 0;
            Factor MyConvertionFactor = new Factor { };

            if (MyObj131Base.UNIT != "PLT")
            {

                MyConvertionFactor = FactorConversion(MyObj131Base.ITEM, MyObj131Base.UNIT, "PLT");
                QUANTITY = (MyConvertionFactor.Tipo == "Div") ? Convert.ToDecimal((QUANTITY * MyConvertionFactor.FactorB) / MyConvertionFactor.FactorD) : Convert.ToDecimal((QUANTITY * MyConvertionFactor.FactorD) / MyConvertionFactor.FactorB);
            }

            for (int i = 0; i < CantPalletsComp; i++)
            {

                DataTable DTPalletContinue = twhcol130DAL.PaidMayorwhcol131(MyObj131Base.ORNO);
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
                MyObj.PAID = MyObj131Base.ORNO + "-" + SecuenciaPallet;
                MyObj.CWAR = TargetCwar;
                MyObj.LOCA = TargetLoca;
                MyObj.QTYS = QtyPallets.ToString();
                MyObj.QTYC = QtyPallets.ToString();
                MyObj.DATE = DateTime.Now.ToString("dd/MM/yyyy").ToString();
                MyObj.DATR = DateTime.Now.ToString("dd/MM/yyyy").ToString(); ;
                MyObj.DATL = DateTime.Now.ToString("dd/MM/yyyy").ToString();
                MyObj.DATP = DateTime.Now.ToString("dd/MM/yyyy").ToString();
                MyObj.LOGN = HttpContext.Current.Session["user"].ToString();
                MyObj.LOGT = " ";
                MyObj.CWAA = TargetCwar;
                MyObj.LOAA = TargetLoca;
                MyObj.QTYA = QtyPallets.ToString();
                MyObj.PAIDS.Add(MyObj.PAID);
                MyObj.PAID_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.PAID + "&code=Code128&dpi=96";
                MyObj.PAIDS_URLS.Add(MyObj.PAID_URL);
                MyObj.ORNO_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.ORNO + "&code=Code128&dpi=96";
                MyObj.ITEM_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.ITEM + "&code=Code128&dpi=96";
                MyObj.CLOT_URL = MyObj.LOT.ToString().Trim() != "" ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.CLOT + "&code=Code128&dpi=96" : "";
                //MyObj.QTYC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.QTYC.ToString("0.0000").Trim().ToUpper() + "&code=Code128&dpi=96";
                MyObj.UNIC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.UNIC.ToString().Trim().ToUpper() + "&code=Code128&dpi=96";

                if (twhcol130DAL.Insertartwhcol131(MyObj))
                {
                    inserts++;
                }
            }
            for (int p = 0; p < CantParcPallets; p++)
            {

                DataTable DTPalletContinue = twhcol130DAL.PaidMayorwhcol131(MyObj131Base.ORNO);
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
                MyObj.PAID = MyObj131Base.ORNO + "-" + SecuenciaPallet;
                MyObj.CWAR = TargetCwar;
                MyObj.LOCA = TargetLoca;
                MyObj.QTYS = (qtyReal - (CantPalletsComp * QtyPallets)).ToString();
                MyObj.QTYC = (qtyReal - (CantPalletsComp * QtyPallets)).ToString();
                MyObj.DATE = DateTime.Now.ToString("dd/MM/yyyy").ToString();
                MyObj.DATR = DateTime.Now.ToString("dd/MM/yyyy").ToString(); ;
                MyObj.DATL = DateTime.Now.ToString("dd/MM/yyyy").ToString();
                MyObj.DATP = DateTime.Now.ToString("dd/MM/yyyy").ToString();
                MyObj.LOGN = HttpContext.Current.Session["user"].ToString();
                MyObj.LOGT = " ";
                MyObj.CWAA = TargetCwar;
                MyObj.LOAA = TargetLoca;
                MyObj.QTYA = (qtyReal - (CantPalletsComp * QtyPallets)).ToString();
                MyObj.PAIDS.Add(MyObj.PAID);
                MyObj.PAID_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.PAID + "&code=Code128&dpi=96";
                MyObj.PAIDS_URLS.Add(MyObj.PAID_URL);
                MyObj.ORNO_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.ORNO + "&code=Code128&dpi=96";
                MyObj.ITEM_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.ITEM + "&code=Code128&dpi=96";
                MyObj.CLOT_URL = MyObj.LOT.ToString().Trim() != "" ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.CLOT + "&code=Code128&dpi=96" : "";
                //MyObj.QTYC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.QTYC.ToString("0.0000").Trim().ToUpper() + "&code=Code128&dpi=96";
                MyObj.UNIC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.UNIC.ToString().Trim().ToUpper() + "&code=Code128&dpi=96";

                if (twhcol130DAL.Insertartwhcol131(MyObj))
                {
                    inserts++;
                }
            }
            if (inserts == CantPalletsComp + CantParcPallets)
            {
                Ent_twhcol020 objWhcol020 = new Ent_twhcol020();
                objWhcol020.tbl = "";
                objWhcol020.clot = "";
                objWhcol020.sqnb = MyObj131Base.PAID;
                objWhcol020.mitm = MyObj131Base.ITEM;
                objWhcol020.dsca = Transfers.DescripcionItem(objWhcol020.mitm);
                objWhcol020.cwor = MyObj131Base.CWAR;
                objWhcol020.loor = MyObj131Base.LOCA;
                objWhcol020.cwde = TargetCwar;
                objWhcol020.lode = TargetLoca;

                objWhcol020.qtdl = Convert.ToDouble(qtyReal);
                objWhcol020.cuni = MyObj131Base.UNIT;
                objWhcol020.user = HttpContext.Current.Session["user"].ToString();

                Transfers.InsertarTransferencia(objWhcol020);
                _idaltwhcol131.Actualizartwhcol131CantEstado(MyObj131Base.PAID, 9, (Convert.ToDecimal(QtyReal) - Convert.ToDecimal(MyObj131Base.QTYS)));
            }
            return JsonConvert.SerializeObject(MyObj);
        }

        protected static string mensajes(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("InventoryTakingByPalletID.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }
    }

}