using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using whusa;
using Newtonsoft.Json;
using System.Data;
using System.Web.Services;
using whusa.Interfases;
using System.Globalization;
using System.Threading;
using System.Configuration;
using whusa.Entidades;
using whusa.Utilidades;
using System.Web.UI.WebControls.WebParts;
using whusa.DAL;
using System.Web.Configuration;

namespace whusap.WebPages.WorkOrders
{
    public partial class PickingUpdate : System.Web.UI.Page
    {
        private static Seguimiento log = new Seguimiento();
        public static string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        public static string ThepalletIDDoesntexistorItsinpickingprocess = mensajes("ThepalletIDDoesntexistorItsinpickingprocess");
        public static string errorlog = string.Empty;
        public static string ADVS = string.Empty;
        public object GObject = new object();
        private static InterfazDAL_twhcol130 _idaltwhcol130 = new InterfazDAL_twhcol130();
        private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
        private static InterfazDAL_tticol042 _idaltticol042 = new InterfazDAL_tticol042();
        public static InterfazDAL_twhcol122 twhcolDAL = new InterfazDAL_twhcol122();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        private static InterfazDAL_tticol125 _idaltticol125 = new InterfazDAL_tticol125();
        private static InterfazDAL_twhcol122 _idaltwhcol122 = new InterfazDAL_twhcol122();
        private static InterfazDAL_tticol182 _idaltticol182 = new InterfazDAL_tticol182();
        public static IntefazDAL_tticol082 Itticol082 = new IntefazDAL_tticol082();
        private static Mensajes _mensajesForm = new Mensajes();
        public static whusa.Utilidades.Recursos recursos = new whusa.Utilidades.Recursos();
        string sentencia1 = string.Empty;
        string formName = string.Empty;
        string _idioma = string.Empty;
        private static string globalMessages = "GlobalMessages";
        public static string qtyaG = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            HttpContext.Current.Session["MyObjPicking"] = null;
            HttpContext.Current.Session["MyObjPicking182"] = null;


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
                if (HttpContext.Current.Session["MyObjPicking"] == null)
                {
                    HttpContext.Current.Session["MyObjPicking"] = new EntidadPicking();
                }
                try
                {
                    _idioma = Session["ddlIdioma"].ToString();
                }
                catch (Exception)
                {
                    _idioma = "INGLES";
                }

                string strTitulo = mensajes("encabezado");
                control.Text = strTitulo;

                string strError = string.Empty;
                Ent_ttccol301 data = new Ent_ttccol301()
                {
                    user = HttpContext.Current.Session["user"].ToString().Trim(),
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
        public static string GetWarehouse(bool consigment = false)
        {
            List<string> lstWarehouse = new List<string>();
            string TPYW = "1";
            if (consigment == true) { TPYW = "21"; } else { TPYW = "1"; }
            DataTable dtWarehouse = _idaltwhcol130.GetWarehouse(HttpContext.Current.Session["user"].ToString(), TPYW);
            if (dtWarehouse.Rows.Count > 0)
            {
                foreach (DataRow dtRow in dtWarehouse.Rows)
                {
                    lstWarehouse.Add(dtRow["T$CWAR"].ToString());
                }
            }
            return JsonConvert.SerializeObject(lstWarehouse); ;
        }

        [WebMethod]
        public static string loadPicks182(string CWAR)
        {
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
            string strError = string.Empty;
            Ent_tticol182 MyObj182 = new Ent_tticol182();
            MyObj182.CWAR = CWAR;

            DataTable DTtticol182 = _idaltticol182.SelectRecord(ref MyObj182, ref strError);
            int index = 0;
            if (DTtticol182.Rows.Count > 0)
            {
                foreach (DataRow row in DTtticol182.Rows)
                {
                    if (row["T$STAT"].ToString().Trim() == "1" || (row["T$STAT"].ToString().Trim() == "5" && row["T$LOGN"].ToString().Trim() == HttpContext.Current.Session["user"].ToString().Trim()))
                    {
                        MySessionObjPicking.OORG = row["T$OORG"].ToString();
                        MySessionObjPicking.ORNO = row["T$ORNO"].ToString();
                        MySessionObjPicking.PONO = row["T$PONO"].ToString();
                        MySessionObjPicking.ADVS = row["T$ADVS"].ToString();
                        MySessionObjPicking.ITEM = row["T$ITEM"].ToString();
                        MySessionObjPicking.QTY = row["T$QTYT"].ToString();
                        MySessionObjPicking.UN = row["T$UNIT"].ToString();
                        MySessionObjPicking.WRH = row["T$CWAR"].ToString();
                        MySessionObjPicking.MCNO = row["T$MCNO"].ToString();
                        MySessionObjPicking.PRIO = row["T$PRIO"].ToString();
                        MySessionObjPicking.PICK = row["T$PICK"].ToString();
                        MySessionObjPicking.DESCRIPTION = row["T$DSCA"].ToString();
                        //MySessionObjPicking.PALLETID = row["T$PAID"].ToString();
                        //MySessionObjPicking.LOCA = row["T$LOCA"].ToString();
                        MySessionObjPicking.STAT = row["T$STAT"].ToString();
                        MyObj182.PICK = MySessionObjPicking.PICK;
                        MyObj182.STAT = "5";
                        MyObj182.LOGN = HttpContext.Current.Session["user"].ToString().Trim();
                        MyObj182.ORNO = MySessionObjPicking.ORNO;
                        MyObj182.PONO = MySessionObjPicking.PONO;
                        MyObj182.ADVS = MySessionObjPicking.ADVS;
                        _idaltticol182.ChangeStat182(ref MyObj182, ref strError);
                        MySessionObjPicking.error = false;
                        break;
                    }
                    index++;
                }
                HttpContext.Current.Session["MyObjPicking"] = MySessionObjPicking;
            }
            else
            {
                HttpContext.Current.Session["MyObjPicking182"] = null;
                HttpContext.Current.Session["MyObjPicking"] = new EntidadPicking();
                MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
                MySessionObjPicking.error =  true;
                MySessionObjPicking.errorMsg = "No Picks Availables For This Warehouse.";
            }
            
            return JsonConvert.SerializeObject(MySessionObjPicking);
        }

        [WebMethod]
        public static string ShowCurrentOptionsItem()
        {
            List<EntidadPicking> LstPallet22 = new List<EntidadPicking>();
            List<EntidadPicking> LstPallet042 = new List<EntidadPicking>();
            List<EntidadPicking> LstPallet131 = new List<EntidadPicking>();
            List<EntidadPicking> LstReturn = new List<EntidadPicking>();
            if (HttpContext.Current.Session["MyObjPicking"] != " ")
            {
                EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
                //JC 101021  No mostrar la tabla de pallets sugeridos si ya es el último pick.
                LstPallet131 = twhcolDAL.ConsultarPalletPicking131Item(MySessionObjPicking.ITEM, MySessionObjPicking.QTY, MySessionObjPicking.PRIO, HttpContext.Current.Session["user"].ToString().Trim(), MySessionObjPicking.ORNO, MySessionObjPicking.PONO, MySessionObjPicking.ADVS);
                if (LstPallet131.Count > 0)
                {
                    LstReturn = LstPallet131;
                }

                LstPallet042 = twhcolDAL.ConsultarPalletPicking042Item(MySessionObjPicking.ITEM, MySessionObjPicking.QTY, MySessionObjPicking.PRIO, HttpContext.Current.Session["user"].ToString().Trim(), MySessionObjPicking.ORNO, MySessionObjPicking.PONO, MySessionObjPicking.ADVS);
                if (LstPallet042.Count > 0)
                {
                    LstReturn = LstPallet042;
                }

                LstPallet22 = twhcolDAL.ConsultarPalletPicking22Item(MySessionObjPicking.ITEM, MySessionObjPicking.QTY, MySessionObjPicking.PRIO, HttpContext.Current.Session["user"].ToString().Trim(), MySessionObjPicking.ORNO, MySessionObjPicking.PONO, MySessionObjPicking.ADVS);
                if (LstPallet22.Count > 0)
                {
                    LstReturn = LstPallet22;
                }               
            }
            else
            {
                EntidadPicking ObjPicking182 = new EntidadPicking();
                ObjPicking182.error = true;
                ObjPicking182.errorMsg = "No More Picks Availables for this Warehouse.";
                EntidadPicking MySessionObjPicking = null;
                return JsonConvert.SerializeObject(ObjPicking182);
            }
            return JsonConvert.SerializeObject(LstReturn);
        }

        public static string LiteralStatus(string tbl, string stat)
        {
            string statRes = string.Empty;
            string[] array131 = new string[] { "", "Received", "Picked", "Located", "Announced", "Adjusted", "Allocated", "Picking", "Dropped", "Delivered", "Blocked", "Rejected" };
            string[] array122 = new string[] { "", "Deleted", "Announced", "Rejected", "Wrapped", "Received", "Picked", "Located", "Allocated", "Picking", "Dropped", "Delivered", "Blocked", "To Delete", "Adjusted", "No" };
            if (tbl == "ticol022" || tbl == "ticol042")
            {
                statRes = array122[Convert.ToInt16(stat)];
            }
            else if (tbl == "whcol131")
            {
                statRes = array131[Convert.ToInt16(stat)];
            }
            return statRes;
        }

        [WebMethod]
        public static string VerificarExistenciaPalletID(string PAID_NEW)
        {
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
            EntidadPicking ObjPicking182 = new EntidadPicking();

            DataTable DTPalletID = twhcolDAL.VerificarPalletID(PAID_NEW);
            if (DTPalletID.Rows.Count > 0)
            {
                ObjPicking182.error = false;
                ObjPicking182.PALLETID = PAID_NEW;
                ObjPicking182.LOT = DTPalletID.Rows[0]["LOT"].ToString();
                ObjPicking182.ITEM = DTPalletID.Rows[0]["ITEM"].ToString();
                ObjPicking182.DESCRIPTION = DTPalletID.Rows[0]["DSCA"].ToString();
                ObjPicking182.WRH = DTPalletID.Rows[0]["CWAT"].ToString();
                ObjPicking182.DESCWRH = DTPalletID.Rows[0]["DESCAW"].ToString();
                ObjPicking182.LOCA = DTPalletID.Rows[0]["ACLO"].ToString();
                ObjPicking182.QTYT = DTPalletID.Rows[0]["QTYT"].ToString();
                ObjPicking182.MCNO = MySessionObjPicking.MCNO;
                //ObjPicking.QTY = MySessionObjPicking.QTYT.ToString();
                ObjPicking182.UN = DTPalletID.Rows[0]["UNIT"].ToString();
                ObjPicking182.ALLO = DTPalletID.Rows[0]["ALLO"].ToString();
                ObjPicking182.CNPK = DTPalletID.Rows[0]["CNPK"].ToString();
                //ObjPicking.PICK = MySessionObjPicking.PICK;
                ObjPicking182.ORNO = MySessionObjPicking.ORNO;
                ObjPicking182.PONO = MySessionObjPicking.PONO;
                ObjPicking182.ADVS = MySessionObjPicking.ADVS;
                ObjPicking182.OORG = MySessionObjPicking.OORG;
                ObjPicking182.PICK = MySessionObjPicking.PICK;
                ObjPicking182.PRIO = MySessionObjPicking.PRIO;

                if ((DTPalletID.Rows[0]["TBL"].ToString().Trim() == "ticol022" || DTPalletID.Rows[0]["TBL"].ToString().Trim() == "ticol042") && (DTPalletID.Rows[0]["STAT"].ToString() != "7"))
                {
                    ObjPicking182.error = true;
                    ObjPicking182.errorMsg = "Pallet no allowed " + LiteralStatus(DTPalletID.Rows[0]["TBL"].ToString().Trim(), DTPalletID.Rows[0]["STAT"].ToString());
                    return JsonConvert.SerializeObject(ObjPicking182);
                }
                else if ((DTPalletID.Rows[0]["TBL"].ToString().Trim() == "whcol131") && (DTPalletID.Rows[0]["STAT"].ToString() != "3"))
                {
                    ObjPicking182.error = true;
                    ObjPicking182.errorMsg = "Pallet no allowed " + LiteralStatus(DTPalletID.Rows[0]["TBL"].ToString().Trim(), DTPalletID.Rows[0]["STAT"].ToString());
                    return JsonConvert.SerializeObject(ObjPicking182);
                }



                if (MySessionObjPicking.WRH.Trim() != DTPalletID.Rows[0]["CWAT"].ToString().Trim() ||
                MySessionObjPicking.ITEM.Trim() != DTPalletID.Rows[0]["ITEM"].ToString().Trim())
                {
                    ObjPicking182.error = true;
                    ObjPicking182.errorMsg = ThepalletIDDoesntexistorItsinpickingprocess;
                    return JsonConvert.SerializeObject(ObjPicking182);
                }

                if (DTPalletID.Rows[0]["TBL"].ToString().Trim() == "ticol022")
                {
                    //Ent_ttdcol137 data137 = new Ent_ttdcol137();
                    //data137.Dele = 1;
                    //var validateSaveTicol022 = ITticol137.Actualizarttdcol022Status(data137);
                    HttpContext.Current.Session["flag022"] = 1;
                    HttpContext.Current.Session["flag042"] = 0;
                    HttpContext.Current.Session["flag131"] = 0;
                }
                else if (DTPalletID.Rows[0]["TBL"].ToString().Trim() == "ticol042")
                {
                    //Ent_ttdcol137 data137 = new Ent_ttdcol137();
                    //data137.Dele = 1;
                    //var validateSaveTicol042 = ITticol137.Actualizarttdcol042Status(data137);
                    HttpContext.Current.Session["flag042"] = 1;
                    HttpContext.Current.Session["flag131"] = 0;
                    HttpContext.Current.Session["flag022"] = 0;
                }
                else if (DTPalletID.Rows[0]["TBL"].ToString().Trim() == "whcol131")
                {
                    //int STATUS = 1;
                    //string PALLET = PAID_NEW;
                    //decimal qt = 0; 
                    //var validateSaveWhcol131 = ITticol137.Actualizartwhcol131CantStatus(ref PALLET, ref STATUS, ref qt);
                    HttpContext.Current.Session["flag131"] = 1;
                    HttpContext.Current.Session["flag022"] = 0;
                    HttpContext.Current.Session["flag042"] = 0;
                }
                HttpContext.Current.Session["MyObjPicking182"] = ObjPicking182;
                return JsonConvert.SerializeObject(ObjPicking182);
            }
            else
            {
                // pallet no existe
                ObjPicking182.error = true;
                ObjPicking182.errorMsg = ThepalletIDDoesntexistorItsinpickingprocess;
                return JsonConvert.SerializeObject(ObjPicking182);
            }
        }

        public static decimal ConvertToDecimal(string inputQtyStr)
        {
            decimal inputQty = 0;
            if (inputQtyStr.Contains("."))
            {
                inputQty = Convert.ToDecimal(inputQtyStr);
                if (!inputQty.ToString().Contains("."))
                {
                    inputQty = Convert.ToDecimal(ReplaceDecimal(inputQtyStr));
                }
            }
            else if (inputQtyStr.Contains(","))
            {
                inputQty = Convert.ToDecimal(inputQtyStr);
                if (!inputQty.ToString().Contains(","))
                {
                    inputQty = Convert.ToDecimal(ReplaceDecimal(inputQtyStr));
                }
            }
            else
            {
                inputQty = Convert.ToDecimal(inputQtyStr);
            }
            return inputQty;
        }

        public static string ReplaceDecimal(string number)
        {
            return number.Contains(",") ? number.Replace(",", ".") : number.Replace(".", ",");
        }

        [WebMethod]
        public static string StopPicking()
        {
            string strError = string.Empty;
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
            Ent_tticol182 MyObj182 = new Ent_tticol182();
            MyObj182.STAT = "1";
            MyObj182.ORNO = MySessionObjPicking.ORNO;
            MyObj182.PONO = MySessionObjPicking.PONO;
            MyObj182.ADVS = MySessionObjPicking.ADVS;
            MyObj182.PICK = MySessionObjPicking.PICK;
            _idaltticol182.ChangeStat182(ref MyObj182, ref strError);
            string UrlBase = WebConfigurationManager.AppSettings["UrlBase"].ToString();
            return UrlBase + "/Webpages/Login/Login/whMenuI.aspx";
        }

        [WebMethod]
        public static string BlockPicking()
        {
            string strError = string.Empty;
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
            Ent_tticol182 MyObj182 = new Ent_tticol182();
            MyObj182.STAT = "3";
            MyObj182.ORNO = MySessionObjPicking.ORNO;
            MyObj182.PONO = MySessionObjPicking.PONO;
            MyObj182.ADVS = MySessionObjPicking.ADVS;
            MyObj182.PICK = MySessionObjPicking.PICK;
            _idaltticol182.ChangeStat182(ref MyObj182, ref strError);
            string UrlBase = WebConfigurationManager.AppSettings["UrlBase"].ToString();
            return UrlBase + "/Webpages/Login/Login/whMenuI.aspx";
        }

        [WebMethod]
        public static string Click_confirPKG(string QTYT, string consigment)
        {
            try
            {
                HttpContext.Current.Session["Table"] = "";
                Random generator = new Random();
                int t = generator.Next(1, 1000);
                string maximo = string.Format("{0:0000000000}", t);


                EntidadPicking MyObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
                EntidadPicking MyObjPicking182 = (EntidadPicking)HttpContext.Current.Session["MyObjPicking182"];
                string PAID = MyObjPicking182.PALLETID;
                string LOCA = MyObjPicking182.LOCA;
                string OORG = MyObjPicking182.OORG;
                string ORNO = MyObjPicking182.ORNO;
                string PONO = MyObjPicking182.PONO;
                string QTYT_OLD = MyObjPicking182.QTYT;
                string CUNI = MyObjPicking182.UN;
                string CWAR = MyObjPicking182.WRH;
                string CLOT = MyObjPicking182.LOT;
                string ADVSP = MyObjPicking182.ADVS;


                string sentencia = string.Empty;
                string sentencia1 = string.Empty;
                //JC 121121 Consultar el stock en baan antes de confirmar.
                string ITM = MyObjPicking182.ITEM;
                string LOT = MyObjPicking182.LOT;
                string LOC = MyObjPicking182.LOCA;
                string WRH = MyObjPicking182.WRH;
                string QTY = QTYT;
                //JC 181121 Validar la cantidad en pickings previos
                string StockB, QtyPicked;
                DataTable StockPicked = _idaltwhcol130.ValidarStockPicked(ref ITM, ref LOT, ref LOC, ref WRH, ref QTY);
                if (StockPicked.Rows.Count <= 0)
                {
                    QtyPicked = "0";
                }
                else
                {
                    QtyPicked = StockPicked.Rows[0]["QTY"].ToString();
                }
                String QtyTotal = (Convert.ToDecimal(QtyPicked) + Convert.ToDecimal(QTY)).ToString();
                DataTable StockBaan = _idaltwhcol130.ValidarStockBaan(ref ITM, ref LOT, ref LOC, ref WRH, ref QtyTotal);
                //JC 181121 Validar la cantidad en pickings previos
                //DataTable StockPicked = _idaltwhcol130.ValidarStockPicked(ref ITM, ref LOT, ref LOC, ref WRH, ref QTY);
                if (StockBaan.Rows.Count <= 0)
                {
                    StockB = "0";
                }
                else
                {
                    StockB = StockBaan.Rows[0]["STOCK"].ToString();
                }
                
                if (Convert.ToDecimal(StockB) >= Convert.ToDecimal(QtyTotal))
                {
                    DataTable DTPallet = _idaltwhcol130.VerificarPalletID(ref PAID);
                    qtyaG = DTPallet.Rows[0]["QTYT"].ToString();
                    Decimal CantidadRestante = Convert.ToDecimal(qtyaG) - Convert.ToDecimal(QTYT);
                    Ent_tticol082 obj082 = new Ent_tticol082();
                    obj082.OORG = MyObjPicking182.OORG == "" ? " " : MyObjPicking182.OORG;
                    obj082.ORNO = MyObjPicking182.ORNO == "" ? " " : MyObjPicking182.ORNO;
                    obj082.PONO = MyObjPicking182.PONO == "" ? " " : MyObjPicking182.PONO;
                    obj082.ADVS = MyObjPicking182.ADVS == "" ? " " : MyObjPicking182.ADVS;
                    obj082.ITEM = MyObjPicking182.ITEM;
                    obj082.STAT = consigment.Trim() == "true" ? "7" : "8";
                    obj082.QTYT = QTYT;
                    obj082.CWAR = MyObjPicking182.WRH;
                    obj082.UNIT = MyObjPicking182.UN;
                    obj082.PRIO = MyObjPicking182.PRIO;
                    obj082.PAID = MyObjPicking182.PALLETID;
                    obj082.MCNO = MyObjPicking182.MCNO == "" ? " " : MyObjPicking182.MCNO;
                    obj082.LOGN = HttpContext.Current.Session["user"].ToString().Trim();
                    obj082.PICK = MyObjPicking.PICK.ToString().Trim() + "-" + t.ToString();
                    obj082.LOCA = MyObjPicking182.LOCA.ToString().Trim();


                    Ent_tticol182 obj182Old = new Ent_tticol182();
                    obj182Old.OORG = MyObjPicking.OORG == "" ? " " : MyObjPicking182.OORG;
                    obj182Old.ORNO = MyObjPicking.ORNO == "" ? " " : MyObjPicking182.ORNO;
                    obj182Old.PONO = MyObjPicking.PONO == "" ? " " : MyObjPicking182.PONO;
                    obj182Old.ADVS = MyObjPicking.ADVS == "" ? " " : MyObjPicking182.ADVS;
                    obj182Old.ITEM = MyObjPicking.ITEM;
                    obj182Old.STAT = consigment.Trim() == "true" ? "7" : "4";
                    obj182Old.QTYT = (Convert.ToDecimal(MyObjPicking.QTY) - Convert.ToDecimal(QTYT)).ToString();
                    obj182Old.QTY = (Convert.ToDecimal(MyObjPicking182.QTYT) - Convert.ToDecimal(QTYT)).ToString();
                    obj182Old.CWAR = MyObjPicking.WRH;
                    obj182Old.UNIT = MyObjPicking.UN;
                    obj182Old.PRIO = MyObjPicking.PRIO;
                    obj182Old.PAID = MyObjPicking.PALLETID;
                    obj182Old.PICK = MyObjPicking.PICK;
                    obj182Old.MCNO = MyObjPicking.MCNO == "" ? " " : MyObjPicking182.MCNO;
                    obj182Old.LOGN = HttpContext.Current.Session["user"].ToString().Trim();




                    decimal qtyt = ConvertToDecimal(QTYT.ToString().Trim());
                    decimal qtyt_old = ConvertToDecimal(QTYT_OLD.ToString().Trim());
                    decimal qtyt_act = qtyt_old - qtyt;
                    string qtytS = ConvertToDecimal(QTYT.ToString().Trim()).ToString().ToString();
                    int cnpk = Convert.ToInt32(MyObjPicking182.CNPK.Trim() == "" ? "2" : MyObjPicking182.CNPK.Trim());
                    String Location = LOCA;
                    if (Convert.ToInt32(HttpContext.Current.Session["flag022"].ToString().Trim()) == 1)
                    {
                        HttpContext.Current.Session["Table"] = "ticol022";
                        Ent_tticol022 MyObj = new Ent_tticol022();

                        DataTable dtAllo = twhcolDAL.getAllotticol222(PAID.Trim());
                        DataTable DTPalletnew = _idaltwhcol130.VerificarPalletID(ref PAID);
                        string qtytnew = DTPallet.Rows[0]["QTYT"].ToString();
                        if (Convert.ToDecimal(qtytnew) >= Convert.ToDecimal(QTYT))
                        {
                            twhcolDAL.updatetticol222Quantity(PAID.Trim(), Convert.ToDecimal(QTYT), Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString()) > 0 ? Convert.ToDecimal(QTYT_OLD) : 0);
                        }
                        else
                        {
                            Ent_twhcol130131 MyErrorObj = new Ent_twhcol130131();
                            MyErrorObj.Error = true;
                            _idaltticol182.Delete182Zero();
                            return JsonConvert.SerializeObject(MyErrorObj); ;
                        }
                        MyObj.qtyaG = Convert.ToDecimal(qtyaG);
                        dtAllo = twhcolDAL.getAllotticol222(PAID.Trim());

                        if (cnpk != 1)
                        {

                            //twhcolDAL.updatetticol222Quantity(PAID.Trim(), Convert.ToDecimal(QTYT), Convert.ToDecimal(QTYT_OLD));

                            string strError = string.Empty;

                            if (Convert.ToDecimal(CantidadRestante) > 0)
                            {
                                string strMaxSequence = getSequence(obj082.ORNO + "-P000", "P");
                                string separator = "-";
                                string newPallet = recursos.GenerateNewPallet(strMaxSequence, separator);
                                string SQNB = PAID.Substring(0, PAID.IndexOf(separator));

                                //MyObj.pdno = ORNO;
                                obj082.PAID = newPallet;
                                bool res182 = Itticol082.InsertarregistroItticol082(obj082);
                                bool resUpd182 = _idaltticol182.ActualizarRegistroItticol182(obj182Old);
                                MyObj.pdno = MyObjPicking182.LOT.Trim();
                                MyObj.sqnb = newPallet;
                                MyObj.proc = 2;
                                MyObj.logn = HttpContext.Current.Session["user"].ToString().Trim();
                                MyObj.mitm = MyObjPicking182.ITEM.Trim();
                                MyObj.qtdl = Convert.ToDecimal(qtyt.ToString());
                                MyObj.cuni = CUNI;//CUNI;
                                MyObj.log1 = "NONE";
                                MyObj.qtd1 = Convert.ToInt32(qtyt.ToString());
                                MyObj.pro1 = 2;
                                MyObj.log2 = "NONE";
                                MyObj.qtd2 = Convert.ToInt32(qtyt.ToString());
                                MyObj.pro2 = 2;
                                MyObj.loca = LOCA.Trim();
                                MyObj.norp = 1;
                                MyObj.dele = 11;
                                MyObj.logd = "NONE";
                                MyObj.refcntd = 0;
                                MyObj.refcntu = 0;
                                MyObj.drpt = DateTime.Now;
                                MyObj.urpt = HttpContext.Current.Session["user"].ToString().Trim();
                                MyObj.acqt = 0;
                                MyObj.cwaf = CWAR;//CWAR;
                                MyObj.cwat = CWAR;//CWAR;
                                MyObj.aclo = LOCA;
                                //MyObj.allo = Convert.ToDecimal(qtyt.ToString());
                                MyObj.allo = 0;
                                MyObj.ALLOAUX = Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString());

                                var validateSave = _idaltticol022.insertarRegistroSimple(ref MyObj, ref strError);
                                var validateSaveTicol222 = _idaltticol022.InsertarRegistroTicol222(ref MyObj, ref strError);

                                _idaltticol125.updataPalletStatus022(PAID, (qtyaG == "0" && Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString()) == 0) ? "11" : (qtyaG == "0" && Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString()) >= 0) ? "8" : "7");

                                twhcolDAL.ingRegTticol092140(maximo, PAID, newPallet, 1, HttpContext.Current.Session["user"].ToString().Trim());

                                if (res182 && Convert.ToDecimal(obj182Old.QTY) != 0)
                                {
                                    HttpContext.Current.Session["codeMaterial"] = MyObj.mitm;
                                    HttpContext.Current.Session["codePaid"] = PAID;
                                    HttpContext.Current.Session["codePaid2"] = MyObj.sqnb;
                                    HttpContext.Current.Session["Lot"] = MyObj.pdno; ;
                                    HttpContext.Current.Session["Quantity"] = MyObj.qtd1;
                                    HttpContext.Current.Session["Quantity2"] = CantidadRestante;
                                    HttpContext.Current.Session["Date"] = MyObj.date;
                                    HttpContext.Current.Session["Pallet"] = MyObj.sqnb;
                                    HttpContext.Current.Session["Machine"] = MyObjPicking182.MCNO;
                                    HttpContext.Current.Session["Operator"] = MyObj.logn;
                                    HttpContext.Current.Session["Reprint"] = "no";
                                    HttpContext.Current.Session["AutoPrint"] = "yes";
                                    HttpContext.Current.Session["PickLabel"] = "yes";
                                    HttpContext.Current.Session["Pick"] = obj082.PICK;
                                    HttpContext.Current.Session["PartialLabel"] = "yes";


                                    MyObj.PAID_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.sqnb + "&code=Code128&dpi=96";
                                    MyObj.PAID_OLD_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + PAID + "&code=Code128&dpi=96";
                                    MyObj.ORNO_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + ORNO + "&code=Code128&dpi=96";
                                    MyObj.ITEM_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.mitm + "&code=Code128&dpi=96";
                                    MyObj.CLOT_URL = CLOT.ToString().Trim() != "" ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + CLOT + "&code=Code128&dpi=96" : "";
                                    MyObj.QTYC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + qtyaG + "&code=Code128&dpi=96";
                                    MyObj.QTYC1_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.qtd1 + "&code=Code128&dpi=96";
                                    MyObj.UNIC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.cuni + "&code=Code128&dpi=96";
                                }
                            }
                            else
                            {
                                bool res182 = Itticol082.InsertarregistroItticol082(obj082);
                                bool resUpd182 = _idaltticol182.ActualizarRegistroItticol182(obj182Old);
                                _idaltticol125.updataPalletStatus022(PAID, "11");
                                twhcolDAL.ingRegTticol092140(maximo, PAID, PAID, 1, HttpContext.Current.Session["user"].ToString().Trim());

                                HttpContext.Current.Session["PartialLabel"] = "no";
                                HttpContext.Current.Session["PickLabel"] = "yes";
                                HttpContext.Current.Session["Pick"] = obj082.PICK;
                                HttpContext.Current.Session["Pallet"] = obj082.PAID;
                                HttpContext.Current.Session["Machine"] = MyObjPicking182.MCNO;
                                HttpContext.Current.Session["Quantity"] = obj082.QTYT;
                                HttpContext.Current.Session["Quantity2"] = obj082.QTYT;
                                HttpContext.Current.Session["codePaid"] = obj082.PAID;
                                HttpContext.Current.Session["codePaid2"] = obj082.PAID;

                            }
                        }
                        else
                        {
                            bool res182 = Itticol082.InsertarregistroItticol082(obj082);
                            bool resUpd182 = _idaltticol182.ActualizarRegistroItticol182(obj182Old);
                            _idaltticol125.updataPalletStatus022(PAID, "11");
                            twhcolDAL.ingRegTticol092140(maximo, PAID, PAID, 1, HttpContext.Current.Session["user"].ToString().Trim());
                            HttpContext.Current.Session["PartialLabel"] = "no";
                            HttpContext.Current.Session["PickLabel"] = "yes";
                            HttpContext.Current.Session["Pallet"] = obj082.PAID;
                            HttpContext.Current.Session["Pick"] = obj082.PICK;
                            HttpContext.Current.Session["Machine"] = MyObjPicking182.MCNO;
                            HttpContext.Current.Session["Quantity"] = obj082.QTYT;
                            HttpContext.Current.Session["Quantity2"] = obj082.QTYT;
                            HttpContext.Current.Session["codePaid"] = obj082.PAID;
                            HttpContext.Current.Session["codePaid2"] = obj082.PAID;



                            //twhcolDAL.updatetticol222Quantity(PAID.Trim(), qtyt_old, qtyt_old);
                        }
                        _idaltticol182.Delete182Zero();
                        return JsonConvert.SerializeObject(MyObj);

                    }
                    else if (Convert.ToInt32(HttpContext.Current.Session["flag042"].ToString().Trim()) == 1)
                    {
                        HttpContext.Current.Session["Table"] = "ticol042";
                        Ent_tticol042 MyObj = new Ent_tticol042();
                        DataTable dtAllo = twhcolDAL.getAllotticol242(PAID.Trim());
                        DataTable DTPalletnew = _idaltwhcol130.VerificarPalletID(ref PAID);
                        string qtytnew = DTPallet.Rows[0]["QTYT"].ToString();
                        if (Convert.ToDecimal(qtytnew) >= Convert.ToDecimal(QTYT))
                        {
                            twhcolDAL.updatetticol242Quantity(PAID.Trim(), Convert.ToDecimal(QTYT), Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString()) > 0 ? Convert.ToDecimal(QTYT_OLD) : 0);
                        }
                        else
                        {
                            Ent_twhcol130131 MyErrorObj = new Ent_twhcol130131();
                            MyErrorObj.Error = true;
                            _idaltticol182.Delete182Zero();
                            return JsonConvert.SerializeObject(MyErrorObj); ;
                        }
                        //twhcolDAL.updatetticol242Quantity(PAID.Trim(), Convert.ToDecimal(QTYT), Convert.ToDecimal(QTYT_OLD));
                        MyObj.qtyaG = Convert.ToDecimal(qtyaG);
                        dtAllo = twhcolDAL.getAllotticol242(PAID.Trim());

                        if (cnpk != 1)
                        {

                            string strError = string.Empty;

                            if (CantidadRestante > 0)
                            {
                                string strMaxSequence = getSequence(obj082.ORNO + "-P000", "P");
                                string separator = "-";
                                string newPallet = recursos.GenerateNewPallet(strMaxSequence, separator);
                                string SQNB = PAID.Substring(0, PAID.IndexOf(separator));


                                //MyObj.pdno = ORNO;
                                obj082.PAID = newPallet;
                                bool res182 = Itticol082.InsertarregistroItticol082(obj082);
                                bool resUpd182 = _idaltticol182.ActualizarRegistroItticol182(obj182Old);
                                MyObj.pdno = MyObjPicking182.LOT.Trim(); ;
                                MyObj.sqnb = newPallet;
                                MyObj.proc = 2;
                                MyObj.logn = HttpContext.Current.Session["user"].ToString().Trim();
                                MyObj.mitm = MyObjPicking182.ITEM.Trim();
                                MyObj.qtdl = Convert.ToDouble(qtyt.ToString());
                                MyObj.cuni = CUNI;//CUNI;
                                MyObj.log1 = "NONE";
                                MyObj.qtd1 = Convert.ToDecimal(qtyt.ToString());
                                MyObj.pro1 = 2;
                                MyObj.log2 = "NONE";
                                MyObj.qtd2 = Convert.ToDecimal(qtyt.ToString());
                                MyObj.pro2 = 2;
                                //MyObj.loca = LOCA.Trim() == "" ? "" : LOCA;
                                MyObj.loca = " ";
                                MyObj.norp = 1;
                                MyObj.dele = 9;
                                MyObj.logd = "NONE";
                                MyObj.refcntd = 0;
                                MyObj.refcntu = 0;
                                MyObj.drpt = DateTime.Now;
                                MyObj.urpt = HttpContext.Current.Session["user"].ToString().Trim();
                                MyObj.acqt = 0;
                                MyObj.cwaf = CWAR;//CWAR;
                                MyObj.cwat = CWAR;//CWAR;
                                //MyObj.aclo = LOCA.Trim() == "" ? "" : LOCA;
                                MyObj.aclo = "";
                                MyObj.allo = Convert.ToDecimal(qtyt.ToString());//Convert.ToDecimal(qtyt_act.ToString());
                                MyObj.ALLOAUX = Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString());

                                var validateSave = _idaltticol042.insertarRegistroSimple(ref MyObj, ref strError);
                                var validateSaveTicol242 = _idaltticol042.InsertarRegistroTicol242(ref MyObj, ref strError);

                                _idaltticol125.updataPalletStatus022(PAID, (qtyaG == "0" && Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString()) == 0) ? "11" : (qtyaG == "0" && Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString()) >= 0) ? "8" : "7");
                                twhcolDAL.ingRegTticol092140(maximo, PAID, newPallet, 1, HttpContext.Current.Session["user"].ToString().Trim());

                                if (res182 && Convert.ToDecimal(obj182Old.QTY) != 0)
                                {
                                    HttpContext.Current.Session["codeMaterial"] = MyObj.mitm;
                                    HttpContext.Current.Session["codePaid"] = MyObj.sqnb;
                                    HttpContext.Current.Session["codePaid2"] = PAID;
                                    HttpContext.Current.Session["Lot"] = MyObj.pdno; ;
                                    HttpContext.Current.Session["Quantity"] = MyObj.qtd1;
                                    HttpContext.Current.Session["Quantity2"] = CantidadRestante;
                                    HttpContext.Current.Session["Date"] = MyObj.date;
                                    HttpContext.Current.Session["Pallet"] = MyObj.sqnb;
                                    HttpContext.Current.Session["Machine"] = MyObjPicking182.MCNO;
                                    HttpContext.Current.Session["Operator"] = MyObj.logn;
                                    HttpContext.Current.Session["Reprint"] = "no";
                                    HttpContext.Current.Session["Pick"] = obj082.PICK;
                                    HttpContext.Current.Session["PartialLabel"] = "yes";

                                    HttpContext.Current.Session["AutoPrint"] = "yes";
                                    HttpContext.Current.Session["PickLabel"] = "yes";

                                    MyObj.PAID_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.sqnb + "&code=Code128&dpi=96";
                                    MyObj.PAID_OLD_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + PAID + "&code=Code128&dpi=96";
                                    MyObj.ORNO_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + ORNO + "&code=Code128&dpi=96";
                                    MyObj.ITEM_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.mitm + "&code=Code128&dpi=96";
                                    MyObj.CLOT_URL = CLOT.ToString().Trim() != "" ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + CLOT + "&code=Code128&dpi=96" : "";
                                    MyObj.QTYC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + qtyaG + "&code=Code128&dpi=96";
                                    MyObj.QTYC1_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.qtd1 + "&code=Code128&dpi=96";
                                    MyObj.UNIC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.cuni + "&code=Code128&dpi=96";
                                }

                            }
                            else
                            {
                                bool res182 = Itticol082.InsertarregistroItticol082(obj082);
                                bool resUpd182 = _idaltticol182.ActualizarRegistroItticol182(obj182Old);
                                HttpContext.Current.Session["PartialLabel"] = "no";
                                HttpContext.Current.Session["PickLabel"] = "yes";
                                _idaltticol125.updataPalletStatus042(PAID, "11");
                                twhcolDAL.ingRegTticol092140(maximo, PAID, PAID, 1, HttpContext.Current.Session["user"].ToString().Trim());
                                HttpContext.Current.Session["Pallet"] = obj082.PAID;
                                HttpContext.Current.Session["Pick"] = obj082.PICK;
                                HttpContext.Current.Session["Machine"] = MyObjPicking182.MCNO;
                                HttpContext.Current.Session["Quantity"] = obj082.QTYT;
                                HttpContext.Current.Session["Quantity2"] = obj082.QTYT;
                                HttpContext.Current.Session["codePaid"] = obj082.PAID;
                                HttpContext.Current.Session["codePaid2"] = obj082.PAID;

                            }
                        }
                        else
                        {
                            bool res182 = Itticol082.InsertarregistroItticol082(obj082);
                            bool resUpd182 = _idaltticol182.ActualizarRegistroItticol182(obj182Old);
                            _idaltticol125.updataPalletStatus042(PAID, "11");
                            twhcolDAL.ingRegTticol092140(maximo, PAID, PAID, 1, HttpContext.Current.Session["user"].ToString().Trim());
                            HttpContext.Current.Session["PartialLabel"] = "no";
                            HttpContext.Current.Session["PickLabel"] = "yes";
                            //twhcolDAL.updatetticol242Quantity(PAID.Trim(), qtyt_old, qtyt_old);
                            HttpContext.Current.Session["Pallet"] = obj082.PAID;
                            HttpContext.Current.Session["Pick"] = obj082.PICK;
                            HttpContext.Current.Session["Pick"] = obj082.PICK;
                            HttpContext.Current.Session["Machine"] = MyObjPicking182.MCNO;
                            HttpContext.Current.Session["Quantity"] = obj082.QTYT;
                            HttpContext.Current.Session["Quantity2"] = obj082.QTYT;
                            HttpContext.Current.Session["codePaid"] = obj082.PAID;
                            HttpContext.Current.Session["codePaid2"] = obj082.PAID;

                        }
                        _idaltticol182.Delete182Zero();
                        return JsonConvert.SerializeObject(MyObj);


                    }

                    else if (Convert.ToInt32(HttpContext.Current.Session["flag131"].ToString().Trim()) == 1)
                    {
                        HttpContext.Current.Session["Table"] = "whcol131";
                        Ent_twhcol130131 MyObj = new Ent_twhcol130131();
                        DataTable DTPalletnew = _idaltwhcol130.VerificarPalletID(ref PAID);
                        string qtytnew = DTPallet.Rows[0]["QTYT"].ToString();
                        if (Convert.ToDecimal(qtytnew) >= Convert.ToDecimal(QTYT))
                        {
                            twhcolDAL.updatetwhcol131QuantityFirst(PAID.Trim(), qtyt, Convert.ToDecimal(MyObjPicking.QTYT));
                        }
                        else
                        {
                            Ent_twhcol130131 MyErrorObj = new Ent_twhcol130131();
                            MyErrorObj.Error = true;
                            _idaltticol182.Delete182Zero();
                            return JsonConvert.SerializeObject(MyErrorObj); ;
                        }
                        MyObj.qtyaG = Convert.ToDecimal(qtyaG);
                        DataTable dtAllo = twhcolDAL.getAllotwhcol131(PAID.Trim());
                        if (cnpk != 1)
                        {


                            if (CantidadRestante > 0)
                            {
                                string strMaxSequence = getSequence(obj082.ORNO + "-P000", "P");
                                string separator = "-";
                                string newPallet = recursos.GenerateNewPallet(strMaxSequence, separator);
                                string SQNB = PAID.Substring(0, PAID.IndexOf(separator));

                                obj082.PAID = newPallet;
                                bool res182 = Itticol082.InsertarregistroItticol082(obj082);
                                bool resUpd182 = _idaltticol182.ActualizarRegistroItticol182(obj182Old);

                                MyObj.OORG = "2";// Order type escaneada view 
                                MyObj.ORNO = ORNO;
                                MyObj.ITEM = MyObjPicking182.ITEM.Trim();
                                MyObj.PAID = newPallet;
                                MyObj.PONO = "1";
                                MyObj.SEQN = "1";
                                MyObj.CLOT = CLOT;//CLOT.ToUpper();// lote VIEW
                                MyObj.CWAR = CWAR;//CWAR.ToUpper();
                                MyObj.QTYS = qtyt.ToString();//QTYS;// cantidad escaneada view 
                                MyObj.UNIT = CUNI;//UNIT;//unit escaneada view
                                MyObj.QTYC = qtyt.ToString();//QTYS;//cantidad escaneada view aplicando factor
                                MyObj.QTYA = "";//QTYS;//cantidad escaneada view aplicando factor
                                MyObj.UNIC = CUNI;//UNIT;//unidad view stock
                                MyObj.DATE = DateTime.Now.ToString("dd/MM/yyyy").ToString();//fecha de confirmacion 
                                MyObj.CONF = "1";
                                MyObj.RCNO = " ";//llena baan
                                MyObj.DATR = DateTime.Now.ToString("dd/MM/yyyy").ToString();//llena baan
                                MyObj.LOCA = " ";//LOCA.ToUpper();// enviamos vacio 
                                MyObj.DATL = DateTime.Now.ToString("dd/MM/yyyy").ToString();//llenar con fecha de hoy
                                MyObj.PRNT = "1";// llenar en 1
                                MyObj.DATP = DateTime.Now.ToString("dd/MM/yyyy").ToString();//llena baan
                                MyObj.NPRT = "1";//conteo de reimpresiones 
                                MyObj.LOGN = HttpContext.Current.Session["user"].ToString().Trim();// nombre de ususario de la session
                                MyObj.LOGT = " ";//llena baan
                                MyObj.STAT = "9";// LLENAR EN 3 
                                MyObj.DSCA = " ";
                                MyObj.COTP = " ";
                                MyObj.FIRE = "2";
                                MyObj.PSLIP = " ";
                                MyObj.LOCA = MyObjPicking182.LOCA.Trim();
                                //MyObj.ALLO = qtyt.ToString();
                                MyObj.ALLO = "0";
                                MyObj.ALLOAUX = Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString());
                                string StrError = string.Empty;
                                bool Insertsucces = twhcol130DAL.Insertartwhcol131(MyObj, ref StrError);

                                _idaltticol125.updataPalletStatus131(PAID, "3");
                                twhcolDAL.ingRegTticol092140(maximo, PAID, newPallet, 1, HttpContext.Current.Session["user"].ToString().Trim());

                                if (res182 && Convert.ToDecimal(obj182Old.QTY) != 0)
                                {                                  
                                    HttpContext.Current.Session["codeMaterial"] = MyObj.ITEM;
                                    HttpContext.Current.Session["codePaid"] = PAID;
                                    HttpContext.Current.Session["codePaid2"] = MyObj.PAID;
                                    HttpContext.Current.Session["Lot"] = MyObj.CLOT;
                                    HttpContext.Current.Session["Quantity"] = MyObj.QTYC;
                                    HttpContext.Current.Session["Quantity2"] = CantidadRestante;
                                    HttpContext.Current.Session["Date"] = MyObj.DATE;
                                    HttpContext.Current.Session["Pallet"] = MyObj.PAID;
                                    HttpContext.Current.Session["Machine"] = MyObjPicking182.MCNO;
                                    HttpContext.Current.Session["Operator"] = MyObj.LOGN;
                                    HttpContext.Current.Session["Reprint"] = "no";
                                    HttpContext.Current.Session["AutoPrint"] = "yes";
                                    HttpContext.Current.Session["PickLabel"] = "yes";
                                    HttpContext.Current.Session["Pick"] = obj082.PICK;
                                    HttpContext.Current.Session["PartialLabel"] = "yes";

                                    MyObj.PAID_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.PAID + "&code=Code128&dpi=96";
                                    MyObj.PAID_OLD_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + PAID + "&code=Code128&dpi=96";
                                    MyObj.ORNO_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.ORNO + "&code=Code128&dpi=96";
                                    MyObj.ITEM_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.ITEM + "&code=Code128&dpi=96";
                                    MyObj.CLOT_URL = CLOT.ToString().Trim() != "" ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + CLOT + "&code=Code128&dpi=96" : "";
                                    MyObj.QTYC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + qtyaG + "&code=Code128&dpi=96";
                                    MyObj.QTYC1_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.QTYS + "&code=Code128&dpi=96";
                                    MyObj.UNIC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.UNIT + "&code=Code128&dpi=96";
                                    //JC 240821  Llevar el control del pallet parcial y el pallet completo que se usó

                                }
                            }
                            else
                            {
                                bool res182 = Itticol082.InsertarregistroItticol082(obj082);
                                bool resUpd182 = _idaltticol182.ActualizarRegistroItticol182(obj182Old);
                                HttpContext.Current.Session["PartialLabel"] = "no";
                                HttpContext.Current.Session["PickLabel"] = "yes";
                                _idaltticol125.updataPalletStatus131(PAID, "9");
                                twhcolDAL.ingRegTticol092140(maximo, PAID, PAID, 1, HttpContext.Current.Session["user"].ToString().Trim());
                                HttpContext.Current.Session["Pallet"] = obj082.PAID;
                                HttpContext.Current.Session["Pick"] = obj082.PICK;
                                HttpContext.Current.Session["Machine"] = MyObjPicking182.MCNO;
                                HttpContext.Current.Session["Quantity"] = obj082.QTYT;
                                HttpContext.Current.Session["Quantity2"] = obj082.QTYT;
                                HttpContext.Current.Session["codePaid"] = obj082.PAID;
                                HttpContext.Current.Session["codePaid2"] = obj082.PAID;
                                HttpContext.Current.Session["PartialLabel"] = "no";
                            }
                        }
                        else
                        {
                            bool res182 = Itticol082.InsertarregistroItticol082(obj082);
                            bool resUpd182 = _idaltticol182.ActualizarRegistroItticol182(obj182Old);
                            _idaltticol125.updataPalletStatus131(PAID, "9");
                            twhcolDAL.ingRegTticol092140(maximo, PAID, PAID, 1, HttpContext.Current.Session["user"].ToString().Trim());
                            HttpContext.Current.Session["PartialLabel"] = "no";
                            HttpContext.Current.Session["PickLabel"] = "yes";
                            //twhcolDAL.updatetwhcol131Quantity(PAID.Trim(), qtyt_old, qtyt_old);
                            HttpContext.Current.Session["Pallet"] = obj082.PAID;
                            HttpContext.Current.Session["Pick"] = obj082.PICK;
                            HttpContext.Current.Session["Machine"] = MyObjPicking182.MCNO;
                            HttpContext.Current.Session["Quantity"] = obj082.QTYT;
                            HttpContext.Current.Session["Quantity2"] = obj082.QTYT;
                            HttpContext.Current.Session["codePaid"] = obj082.PAID;
                            HttpContext.Current.Session["codePaid2"] = obj082.PAID;
                            HttpContext.Current.Session["PartialLabel"] = "no";

                        }
                        _idaltticol182.Delete182Zero();
                        return JsonConvert.SerializeObject(MyObj);
                    }
                    else
                    {
                        Ent_twhcol130131 MyErrorObj = new Ent_twhcol130131();
                        MyErrorObj.Error = true;
                        _idaltticol182.Delete182Zero();
                        return JsonConvert.SerializeObject(MyErrorObj); ;
                    }



                }
                else
                {
                    EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
                    //HttpContext.Current.Session["MyObjPicking182"] = null;
                    //HttpContext.Current.Session["MyObjPicking"] = new EntidadPicking();
                    MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
                    MySessionObjPicking.error = true;
                    MySessionObjPicking.errorMsg = "Stock in baan not available.";
                    return JsonConvert.SerializeObject(MySessionObjPicking);
                }
            }
                
            catch (Exception e)
            {
                Ent_twhcol130131 MyErrorObj = new Ent_twhcol130131();
                MyErrorObj.Error = true;
                MyErrorObj.errorMsg = errorlog + e.Message;
                _idaltticol182.Delete182Zero();
                return JsonConvert.SerializeObject(MyErrorObj); ;
            }
        }

        protected static string mensajes(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("Picking.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }

        [WebMethod]
        public static string getusers()
        {
            return "_operator:" + HttpContext.Current.Session["user"].ToString().Trim() + "  Session['user']:" + HttpContext.Current.Session["user"].ToString() + " ,flag022: " + HttpContext.Current.Session["flag022"].ToString() + " ,flag042: " + HttpContext.Current.Session["flag042"] + " ,flag113: " + HttpContext.Current.Session["flag131"];
        }

        [WebMethod]
        public static string BlockPick182(string PAID)
        {
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking182"];
            string strError = string.Empty;

            if (HttpContext.Current.Session["flag131"].ToString().Trim() == "1")
            {
                _idaltticol125.updataPalletStatus131(PAID, "10");
            }
            else if (HttpContext.Current.Session["flag022"].ToString().Trim() == "1")
            {
                _idaltticol125.updataPalletStatus022(PAID, "12");
            }
            else
            {
                _idaltticol125.updataPalletStatus042(PAID, "12");
            }

            return JsonConvert.SerializeObject(MySessionObjPicking);
        }


        public static string getSequence(string PAIDOld, string complement = "")
        {
            string sequence = string.Empty;
            int indexSeparator = PAIDOld.IndexOf("-");
            string SQNB = PAIDOld.Substring(0, indexSeparator);
            string SEC = PAIDOld.Substring(indexSeparator + 1);

            if (complement == "")
            {
                complement = recursos.SeparatorAlphaNumeric(ref SEC);
            }

            DataTable dtMaxSec = _idaltwhcol130.maximaSecuenciaUnion(SQNB + "-" + complement);
            if (dtMaxSec.Rows.Count > 0)
            {
                sequence = dtMaxSec.Rows[0]["sqnb"].ToString().Trim();
            }
            else
            {
                sequence = SQNB + "-" + complement + "001";
            }
            return sequence;
        }

    }
}

