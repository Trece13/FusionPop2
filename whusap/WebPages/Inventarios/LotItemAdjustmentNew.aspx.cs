using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Text;
using whusa.Entidades;
using whusa.Interfases;
using whusa.Utilidades;
using System.Web.Services;
using whusa;
using Newtonsoft.Json;

namespace whusap.WebPages.Inventarios
{
    public partial class LotItemAdjustmentNew : System.Web.UI.Page
    {
        public static string strError = string.Empty;
        public static string _idioma;
        private static string formName;
        private static string globalMessages = "GlobalMessages";
        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        public static whusa.Utilidades.Recursos recursos = new whusa.Utilidades.Recursos();

        private static InterfazDAL_ttwhcol016 dal016 = new InterfazDAL_ttwhcol016();
        private static InterfazDAL_twhltc100 dal100 = new InterfazDAL_twhltc100();
        private static InterfazDAL_tticol100 dalticol100 = new InterfazDAL_tticol100();
        private static InterfazDAL_twhcol130 _idaltwhcol130 = new InterfazDAL_twhcol130();
        private static InterfazDAL_twhcol028 _idaltwhcol028 = new InterfazDAL_twhcol028();
        public static IntefazDAL_tticol082 Itticol082 = new IntefazDAL_tticol082();
        private static IntefazDAL_transfer dalTransfer = new IntefazDAL_transfer();
        public static InterfazDAL_twhcol122 twhcolDAL = new InterfazDAL_twhcol122();
        private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
        private static InterfazDAL_tticol042 _idaltticol042 = new InterfazDAL_tticol042();


        public static string PalletIDdoesntexistorQuantityavailableiszero = string.Empty;
        public static string PalletIDStatusdoesntallowadjustmen = string.Empty;
        public static string PalletIDdoesntexist = string.Empty;
        public static string Itemcodedoesntexist = string.Empty;
        public static string Lotcodedoesntexist = string.Empty;
        public static string Warehousecodedoesntexist = string.Empty;
        public static string Locationblocked = string.Empty;
        public static string Locationcodedoesntexist = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["TBL"] = string.Empty;
            generateDropDownReasonCodes();
            generateDropDownCostCenters();
            var ctrlName = Request.Params[Page.postEventSourceID];
            var args = Request.Params[Page.postEventArgumentID];
            if (!IsPostBack)
            {
                Session["TBL"] = string.Empty;
                formName = Request.Url.AbsoluteUri.Split('/').Last();
                if (formName.Contains('?'))
                {
                    formName = formName.Split('?')[0];
                }

                if (Session["ddlIdioma"] != null)
                {
                    _idioma = Session["ddlIdioma"].ToString();
                }
                else
                {
                    _idioma = "INGLES";
                }

                CargarIdioma();

                String strTitulo = mensajes("encabezado");
                PalletIDdoesntexistorQuantityavailableiszero = mensajes("PalletIDdoesntexistorQuantityavailableiszero");
                PalletIDStatusdoesntallowadjustmen = mensajes("PalletIDStatusdoesntallowadjustment");
                PalletIDdoesntexist = mensajes("PalletIDdoesntexist");
                Itemcodedoesntexist = mensajes("Itemcodedoesntexist");
                Lotcodedoesntexist = mensajes("Lotcodedoesntexist");
                Warehousecodedoesntexist = mensajes("Warehousecodedoesntexist");
                Locationblocked = mensajes("Locationblocked");
                Locationcodedoesntexist = mensajes("Locationcodedoesntexist");

                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                if (control != null) { control.Text = strTitulo; }


                Ent_ttccol301 data = new Ent_ttccol301()
                {
                    user = Session["user"].ToString(),
                    come = strTitulo,
                    refcntd = 0,
                    refcntu = 0
                };

                List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                datalog.Add(data);

                new InterfazDAL_ttccol301().insertarRegistro(ref datalog, ref strError);
            }
        }

        [WebMethod]
        public static string verifyPallet(string PAID)
        {
            Ent_twhcol130131 MyObj = new Ent_twhcol130131();
            DataTable DTPallet = _idaltwhcol130.VerificarPalletID(ref PAID);

            if (DTPallet.Rows.Count > 0)
            {
                var MyObjDT = DTPallet.Rows[0];
                MyObj.Error = false;

                MyObj.TBL = MyObjDT["TBL"].ToString();
                MyObj.ITEM = MyObjDT["ITEM"].ToString();
                MyObj.KTLC = MyObjDT["KLTC"].ToString();
                MyObj.CLOT = MyObjDT["LOT"].ToString();
                MyObj.QTYA = MyObjDT["QTYT"].ToString();
                MyObj.UNIT = MyObjDT["UNIT"].ToString();
                MyObj.CWAR = MyObjDT["CWAT"].ToString();
                MyObj.SLOC = MyObjDT["SLOC"].ToString();
                MyObj.LOCA = MyObjDT["ACLO"].ToString();
                MyObj.DSCA = MyObjDT["DSCA"].ToString();
                MyObj.DSCAW = MyObjDT["DESCAW"].ToString();
                MyObj.STAT = MyObjDT["STAT"].ToString();
                MyObj.MCNO = MyObjDT["MCNO"].ToString();
                MyObj.ORNO = MyObjDT["ORNO"].ToString();

                HttpContext.Current.Session["TBL"] = MyObjDT["TBL"].ToString().Trim();
                HttpContext.Current.Session["MyOriginalPallet"] = MyObj;
                //HttpContext.Current.Session["ORNO"] = MyObjDT["ORNO"].ToString();
                //JC 210122 Guardar el item para busarlo en la tabla de regrind por item
                HttpContext.Current.Session["ITM"] = MyObjDT["ITEM"].ToString().Trim();

                if (MyObj.QTYA.Trim() == "0")
                {
                    MyObj.Error = true;
                    MyObj.errorMsg = PalletIDdoesntexistorQuantityavailableiszero;
                    MyObj.TipeMsgJs = "Label";
                    return JsonConvert.SerializeObject(MyObj);
                }

                if (MyObj.TBL == "whcol131")
                {
                    if (MyObj.STAT != "3")
                    {
                        MyObj.Error = true;
                        MyObj.errorMsg = PalletIDStatusdoesntallowadjustmen;
                        MyObj.TipeMsgJs = "Label";
                    }
                    else
                    {
                        MyObj.Error = false;
                    }
                }
                else
                {
                    if (MyObj.STAT != "7")
                    {
                        MyObj.Error = true;
                        MyObj.errorMsg = PalletIDStatusdoesntallowadjustmen;
                        MyObj.TipeMsgJs = "Label";
                    }
                    else
                    {
                        MyObj.Error = false;
                    }
                }
            }
            else
            {
                MyObj.Error = true;
                MyObj.errorMsg = PalletIDdoesntexist;
                MyObj.TipeMsgJs = "Label";

            }

            return JsonConvert.SerializeObject(MyObj);
        }

        [WebMethod]
        public static string verifyItem(string ITEM)
        {
            Ent_twhcol130131 MyObj = new Ent_twhcol130131();
            Ent_ttwhcol016 obj016 = new Ent_ttwhcol016();
            obj016.item = ITEM.ToUpper().Trim();
            DataTable DTItemt = dal016.TakeMaterialInv_verificaItem_Param(ref obj016, ref strError);

            if (DTItemt.Rows.Count > 0)
            {
                Ent_twhcol130131 MyOriginalPallet = (Ent_twhcol130131)HttpContext.Current.Session["MyOriginalPallet"];
                var MyObjDT = DTItemt.Rows[0];

                if (MyObjDT["UNIDAD"].ToString().Trim() == MyOriginalPallet.UNIT.Trim())
                {
                    MyObj.KTLC = MyObjDT["LOTE"].ToString();
                    MyObj.ITEM = MyObjDT["T$ITEM"].ToString();
                    MyObj.DSCA = MyObjDT["DESCRIPCION"].ToString();
                    MyObj.UNIT = MyObjDT["UNIDAD"].ToString();
                    MyObj.Error = false;
                    MyObj.errorMsg = string.Empty;
                    MyObj.TipeMsgJs = string.Empty;
                }
                else
                {
                    MyObj.Error = true;
                    MyObj.errorMsg = "Item not allowed to adjust, due the unit is different";
                    MyObj.TipeMsgJs = "alert";
                }

            }
            else
            {
                MyObj.Error = true;
                MyObj.errorMsg = Itemcodedoesntexist;
                MyObj.TipeMsgJs = "Label";

            }

            return JsonConvert.SerializeObject(MyObj); ;
        }

        [WebMethod]
        public static string GetItems()
        {
            Ent_ttwhcol016 obj016 = new Ent_ttwhcol016();
            obj016.stat = "RET";
            //JC 210122 Buscar los posibles regrind en la tabla ticol135
            obj016.item = HttpContext.Current.Session["ITM"].ToString();

            int i = 0;
            DataTable DTItemt = dal016.GetItemsStat(ref obj016, ref strError);
            List<Ent_twhcol130131> lstItems = new List<Ent_twhcol130131>();
            if (DTItemt.Rows.Count > 0)
            {
                foreach (DataRow item in DTItemt.Rows)
                {
                    if (i <= 50)
                    {
                        Ent_twhcol130131 MyObj = new Ent_twhcol130131();
                        MyObj.KTLC = item["LOTE"].ToString();
                        MyObj.ITEM = item["T$ITEM"].ToString();
                        MyObj.DSCA = item["DESCRIPCION"].ToString();
                        MyObj.UNIT = item["UNIDAD"].ToString();
                        MyObj.Error = false;
                        MyObj.errorMsg = string.Empty;
                        MyObj.TipeMsgJs = string.Empty;
                        lstItems.Add(MyObj);
                        i++;
                    }
                }
            }
                       
            return JsonConvert.SerializeObject(lstItems); ;
        }

        [WebMethod]
        public static string verifyLot(string ITEM, string LOT)
        {
            Ent_twhcol130131 MyObj = new Ent_twhcol130131();
            Ent_twhltc100 data100 = new Ent_twhltc100() { item = ITEM, clot = LOT };
            var DTLot = dal100.listaRegistro_Clot(ref data100, ref strError);

            if (DTLot.Rows.Count > 0)
            {
                var MyObjDT = DTLot.Rows[0];
                MyObj.Error = false;
                MyObj.errorMsg = string.Empty;
                MyObj.TipeMsgJs = string.Empty;
            }
            else
            {
                MyObj.Error = true;
                MyObj.errorMsg = Lotcodedoesntexist;
                MyObj.TipeMsgJs = "Label";

            }

            return JsonConvert.SerializeObject(MyObj);
        }

        [WebMethod]
        public static string verifyWarehouse(string CWAR)
        {
            Ent_twhcol130131 MyObj = new Ent_twhcol130131();
            Ent_tticol100 myObj100 = new Ent_tticol100();
            myObj100.cwar = CWAR.Trim().ToUpper();
            DataTable DTWare = dalticol100.selecthandletwhwmd200(ref myObj100, ref strError);

            if (DTWare.Rows.Count > 0)
            {
                var MyObjDT = DTWare.Rows[0];
                MyObj.CWAR = MyObjDT["warehosue"].ToString();
                MyObj.SLOC = MyObjDT["handle_locations"].ToString();
                MyObj.Error = false;
                MyObj.errorMsg = string.Empty;
                MyObj.TipeMsgJs = string.Empty;
            }
            else
            {
                MyObj.Error = true;
                MyObj.errorMsg = Warehousecodedoesntexist;
                MyObj.TipeMsgJs = "Label";

            }

            return JsonConvert.SerializeObject(MyObj);
        }

        [WebMethod]
        public static string verifyLoca(string CWAR, string LOCA)
        {
            Ent_twhcol130131 MyObj = new Ent_twhcol130131();
            DataTable DTLoca = dalTransfer.ConsultarLocation(CWAR, LOCA);
            if (DTLoca.Rows.Count > 0)
            {
                var MyObjDT = DTLoca.Rows[0];
                if (MyObjDT["BINB"].ToString().Trim() != "2")
                {
                    MyObj.Error = true;
                    MyObj.errorMsg = Locationblocked;
                    MyObj.TipeMsgJs = "Label";
                }
                else
                {
                    MyObj.Error = false;
                    MyObj.errorMsg = string.Empty;
                    MyObj.TipeMsgJs = string.Empty;
                }
            }
            else
            {
                MyObj.Error = true;
                MyObj.errorMsg = Locationcodedoesntexist;
                MyObj.TipeMsgJs = "Label";
            }
            return JsonConvert.SerializeObject(MyObj);
        }

        [WebMethod]
        public static string Save(Ent_twhcol028 twhcol028)
        {
            //twhcol028.EMNO =
            Ent_twhcol130131 MyOriginalPallet = (Ent_twhcol130131)HttpContext.Current.Session["MyOriginalPallet"];
            twhcol028.LOGN = HttpContext.Current.Session["user"].ToString();
            bool Res = _idaltwhcol028.insertRegistertwhcol028(ref twhcol028, ref strError);
            //JC 210122 Si es un retal hacer un calculo diferente para la cantidad.
            DataTable TipoItem = _idaltwhcol028.GetItemType(ref twhcol028, ref strError);
            DataTable NetwItem = _idaltwhcol028.GetItemNetw(ref twhcol028, ref strError);
            twhcol028.TYPE = TipoItem.Rows[0]["TYPE"].ToString().ToUpper().Trim();
            twhcol028.NETW = NetwItem.Rows[0]["NETW"].ToString().Trim();
            twhcol028.UNRG = TipoItem.Rows[0]["UNIT"].ToString();
            twhcol028.QTRG = Convert.ToString(Math.Round(((Convert.ToDecimal(twhcol028.TQTY) * Convert.ToDecimal(twhcol028.NETW)) / Convert.ToDecimal(2.20462)), 4));
            if (Res)
            {
                if ((twhcol028.TITM == twhcol028.SITM) && (twhcol028.TWAR != twhcol028.SWAR || twhcol028.TLOC != twhcol028.SLOC))
                {

                    bool updateSuccessPallet = UpdatePalletOriginTable(ref twhcol028);
                    if (updateSuccessPallet)
                    {
                        twhcol028.Error = false;
                    }
                    else
                    {
                        twhcol028.Error = true;
                    }
                }
                else
                {
                    saveOriginTable(twhcol028);
                    string strMaxSequence = string.Empty;
                    switch (MyOriginalPallet.TBL)
                    {
                        case "ticol022":
                        case "ticol042":
                            if (twhcol028.KTLC == "1")
                            {
                                if (twhcol028.TLOT.Trim().ToUpper() == twhcol028.SLOT.Trim().ToUpper())
                                {
                                    strMaxSequence = getSequence(twhcol028.SLOT.Trim().ToUpper() + "-A");
                                }
                                else
                                {
                                    //strMaxSequence = getSequence(twhcol028.TLOT.Trim().ToUpper() + "-A");
                                    //JC 210122 Corregir la busqueda si el lote destino no existe o si el item no maneja lote
                                    strMaxSequence = getSequence(twhcol028.PAID.Substring(0, 9).ToUpper() + "-A");
                                                                       
                                }
                            }
                            else if (twhcol028.KTLC != "1")
                            {
                                if (twhcol028.TLOT.Trim().ToUpper() == twhcol028.SLOT.Trim().ToUpper())
                                {
                                    strMaxSequence = getSequence(MyOriginalPallet.ORNO + "-A");
                                }
                                else
                                {
                                    strMaxSequence = getSequence(twhcol028.SLOT.Trim() + "-A");
                                }
                            }
                            break;
                        case "whcol131":
                            strMaxSequence = getSequence(MyOriginalPallet.ORNO + "-A");
                            break;
                    }

                    bool createSuccessNewPaller = saveNewPalletOriginTable(ref twhcol028, strMaxSequence);

                    if (createSuccessNewPaller)
                    {
                        twhcol028.Error = false;
                        twhcol028.ErrorMsg = "Se inserto correctamente";
                        twhcol028.SuccessMsg = "Se inserto ok";
                        twhcol028.TypeMsgJs = "Label";
                    }
                    else
                    {
                        twhcol028.Error = true;
                        twhcol028.ErrorMsg = "No se inserto correctamente el nuevo pallet";
                        twhcol028.TypeMsgJs = "Label";
                    }
                }
            }
            else
            {
                twhcol028.Error = true;
                twhcol028.ErrorMsg = "No se inserto correctamente";
                twhcol028.TypeMsgJs = "Label";

            }

            return JsonConvert.SerializeObject(twhcol028);
        }

        private static bool saveNewPalletOriginTable(ref Ent_twhcol028 twhcol028, string MaxSequence)
        {

            bool res = false;
            string separator = "-";
            twhcol028.PAID = recursos.GenerateNewPallet(MaxSequence, separator);
            string SQNB = twhcol028.PAID.Substring(0, twhcol028.PAID.IndexOf(separator));
            twhcol028.LOGN = HttpContext.Current.Session["user"].ToString();
            twhcol028.DATR = DateTime.Now.ToString("MM/dd/yyyy");
            switch (HttpContext.Current.Session["TBL"].ToString())
            {
                case "ticol022":
                    Ent_tticol022 obj022 = new Ent_tticol022();
                    List<Ent_tticol022> list022 = new List<Ent_tticol022>();
                    twhcol028.WHLOT = twhcol028.TLOT.Trim() == string.Empty ? " " : twhcol028.TLOT.Trim();
                    obj022.pdno = SQNB;
                    obj022.sqnb = twhcol028.PAID;
                    obj022.proc = 1;
                    obj022.logn = twhcol028.LOGN;
                    obj022.mitm = twhcol028.TITM;
                    //JC 210122 Grabar la cantidad Correcta si es un regrind
                    //obj022.cuni = twhcol028.UNIT;
                    //obj022.qtdl = Convert.ToDecimal(twhcol028.TQTY);
                    if (twhcol028.TYPE == "RET")
                    {
                        obj022.cuni = twhcol028.UNRG;
                        obj022.qtdl = Math.Round(((Convert.ToDecimal(twhcol028.TQTY) * Convert.ToDecimal(twhcol028.NETW)) / Convert.ToDecimal(2.20462)), 4);
                        obj022.qtd1 = Convert.ToInt32((Convert.ToInt32(twhcol028.TQTY) * Convert.ToDecimal(twhcol028.NETW)) / Convert.ToDecimal(2.20462));
                        obj022.qtd2 = Convert.ToInt32((Convert.ToInt32(twhcol028.TQTY) * Convert.ToDecimal(twhcol028.NETW)) / Convert.ToDecimal(2.20462));
                        obj022.acqt = Math.Round(((Convert.ToDecimal(twhcol028.TQTY) * Convert.ToDecimal(twhcol028.NETW)) / Convert.ToDecimal(2.20462)), 4);
                    }
                    else
                    {
                        obj022.cuni = twhcol028.UNIT;
                        obj022.qtdl = Convert.ToDecimal(twhcol028.TQTY);
                        obj022.qtd1 = Convert.ToInt32(Convert.ToDouble(twhcol028.TQTY));
                        obj022.qtd2 = Convert.ToInt32(Convert.ToDouble(twhcol028.TQTY));
                        obj022.acqt = Convert.ToDecimal(twhcol028.TQTY);
                    }
                    obj022.log1 = "NONE";
                    //obj022.qtd1 = Convert.ToInt32(Convert.ToDouble(twhcol028.TQTY));
                    obj022.pro1 = 1;
                    obj022.log2 = "NONE";
                    //obj022.qtd2 = Convert.ToInt32(Convert.ToDouble(twhcol028.TQTY));
                    obj022.pro2 = 2;
                    obj022.loca = " ";
                    obj022.norp = 1;
                    obj022.dele = 7;
                    obj022.logd = "NONE";
                    obj022.refcntd = 0;
                    obj022.refcntu = 0;
                    obj022.drpt = DateTime.Now;
                    obj022.urpt = twhcol028.LOGN;
                    //obj022.acqt = Convert.ToDecimal(twhcol028.TQTY);
                    obj022.cwaf = twhcol028.TWAR;
                    obj022.cwat = twhcol028.TWAR;
                    obj022.aclo = twhcol028.SLOC;
                    obj022.allo = 0;
                    list022.Add(obj022);
                    bool insert022 = Convert.ToBoolean(_idaltticol022.insertarRegistroSimple(ref obj022, ref strError));
                    bool insert222 = Convert.ToBoolean(_idaltticol022.InsertarRegistroTicol222(ref obj022, ref strError));
                    res = (insert222 == true && insert022 == true) ? true : false;
                    break;
                case "ticol042":
                    Ent_tticol042 obj042 = new Ent_tticol042();
                    List<Ent_tticol042> list042 = new List<Ent_tticol042>();
                    twhcol028.WHLOT = twhcol028.TLOT;
                    obj042.pdno = SQNB;
                    obj042.sqnb = twhcol028.PAID;
                    obj042.proc = 1;
                    obj042.logn = twhcol028.LOGN;
                    obj042.mitm = twhcol028.TITM;
                    obj042.qtdl = Convert.ToDouble(twhcol028.TQTY);
                    obj042.cuni = twhcol028.UNIT;
                    obj042.log1 = "NONE";
                    obj042.qtd1 = Convert.ToInt32(Convert.ToDouble(twhcol028.TQTY));
                    obj042.pro1 = 1;
                    obj042.log2 = "NONE";
                    obj042.qtd2 = Convert.ToInt32(Convert.ToDouble(twhcol028.TQTY));
                    obj042.pro2 = 2;
                    obj042.loca = " ";
                    obj042.norp = 1;
                    obj042.dele = 7;
                    obj042.logd = "NONE";
                    obj042.refcntd = 0;
                    obj042.refcntu = 0;
                    obj042.drpt = DateTime.Now;
                    obj042.urpt = twhcol028.LOGN;
                    obj042.acqt = Convert.ToDouble(twhcol028.TQTY);
                    obj042.cwaf = twhcol028.TWAR;
                    obj042.cwat = twhcol028.TWAR;
                    obj042.aclo = twhcol028.SLOC;
                    obj042.allo = 0;
                    list042.Add(obj042);
                    bool insert042 = Convert.ToBoolean(_idaltticol042.insertarRegistroSimple(ref obj042, ref strError));
                    bool insert242 = Convert.ToBoolean(_idaltticol042.InsertarRegistroTicol242(ref obj042, ref strError));
                    res = (insert242 == true && insert042 == true) ? true : false;
                    break;
                case "whcol131":
                    Ent_twhcol130131 obj131 = new Ent_twhcol130131();
                    Ent_twhcol130131 MyObj131 = new Ent_twhcol130131();
                    twhcol028.WHLOT = twhcol028.PAID.Substring(0, (twhcol028.PAID.IndexOf("-")));
                    MyObj131.OORG = "4";
                    MyObj131.ORNO = SQNB.Trim().ToUpper();
                    MyObj131.ITEM = twhcol028.TITM;
                    MyObj131.PAID = twhcol028.PAID;
                    MyObj131.PONO = "0";
                    MyObj131.SEQN = "0";
                    MyObj131.CLOT = string.IsNullOrEmpty(twhcol028.TLOT) ? " " : twhcol028.TLOT.ToUpper();
                    MyObj131.CWAR = twhcol028.TWAR;
                    MyObj131.QTYS = twhcol028.TQTY;// cantidad escaneada view
                    MyObj131.QTYA = twhcol028.TQTY;
                    MyObj131.UNIT = twhcol028.UNIT;
                    MyObj131.QTYC = twhcol028.TQTY;//cantidad escaneada view aplicando factor
                    MyObj131.UNIC = twhcol028.UNIT;
                    MyObj131.DATE = DateTime.Now.ToString("dd/MM/yyyy").ToString();//fecha de confirmacion 
                    MyObj131.CONF = "1";
                    MyObj131.RCNO = " ";//llena baan
                    MyObj131.DATR = "01/01/70";//llena baan
                    MyObj131.LOCA = twhcol028.SLOC;// enviamos vacio
                    MyObj131.DATL = DateTime.Now.ToString("dd/MM/yyyy").ToString();//llenar con fecha de hoy
                    MyObj131.PRNT = "1";// llenar en 1
                    MyObj131.DATP = DateTime.Now.ToString("dd/MM/yyyy").ToString();//llena baan
                    MyObj131.NPRT = "1";//conteo de reimpresiones 
                    MyObj131.LOGN = twhcol028.LOGN;// nombre de ususario de la session
                    MyObj131.LOGT = " ";//llena baan
                    MyObj131.STAT = "3";// LLENAR EN 1  
                    MyObj131.DSCA = "0";
                    MyObj131.COTP = "0";
                    MyObj131.FIRE = "1";
                    MyObj131.PSLIP = " ";
                    MyObj131.ALLO = "0";
                    string StrError = string.Empty;
                    res = _idaltwhcol130.Insertartwhcol131(MyObj131, ref StrError);
                    break;

            }
            return res;
        }

        private static bool UpdatePalletOriginTable(ref Ent_twhcol028 twhcol028)
        {
            bool res = false;
            string separator = "-";
            string SQNB = twhcol028.PAID.Substring(0, twhcol028.PAID.IndexOf(separator));
            switch (HttpContext.Current.Session["TBL"].ToString())
            {
                case "ticol022":
                    Ent_tticol022 obj022 = new Ent_tticol022();
                    obj022.sqnb = twhcol028.PAID;
                    obj022.dele = 7;
                    obj022.cwaf = twhcol028.TWAR;
                    obj022.cwat = twhcol028.TWAR;
                    obj022.aclo = twhcol028.TLOC == string.Empty ? " " : twhcol028.TLOC;
                    res = dalticol100.updatetticol222(ref obj022, ref strError);
                    break;
                case "ticol042":
                    Ent_tticol042 obj042 = new Ent_tticol042();
                    obj042.sqnb = twhcol028.PAID;
                    obj042.dele = 7;
                    obj042.cwaf = twhcol028.TWAR;
                    obj042.cwat = twhcol028.TWAR;
                    obj042.aclo = twhcol028.TLOC == string.Empty ? " " : twhcol028.TLOC;;
                    res = dalticol100.updatetticol242(ref obj042, ref strError);
                    break;
                case "whcol131":
                    Ent_twhcol130131 MyObj131 = new Ent_twhcol130131();
                    MyObj131.PAID = twhcol028.PAID;
                    MyObj131.STAT = "3";
                    MyObj131.CWAR = twhcol028.TWAR;
                    MyObj131.LOCA = twhcol028.TLOC == string.Empty ? " " : twhcol028.TLOC;;
                    res = dalticol100.updatetwhcol131(ref MyObj131, ref strError);
                    break;
            }
            return res;
        }
        private static string currentSequience(string strOldSequence)
        {
            string strNewSequence = string.Empty;
            int newSequence = Convert.ToInt32(strOldSequence) + 1;
            if (newSequence <= 9)
            {
                strNewSequence = "00" + newSequence.ToString();
            }
            else if (newSequence > 9 && newSequence <= 99)
            {
                strNewSequence = "0" + newSequence.ToString();
            }
            else if (newSequence > 99)
            {
                strNewSequence = newSequence.ToString();
            }
            return strNewSequence;
        }

        public static bool saveOriginTable(Ent_twhcol028 twhcol028)
        {
            Ent_twhcol130131 MyOriginalPallet = (Ent_twhcol130131)HttpContext.Current.Session["MyOriginalPallet"];
            bool ActalizacionExitosa = false;
            Ent_tticol082 MyObj82 = new Ent_tticol082();
            MyObj82.PAID = twhcol028.PAID;
            MyObj82.STAT = "14";
            MyObj82.QTYC = "0";
            switch (MyOriginalPallet.TBL)
            {
                case "ticol022":
                    twhcolDAL.ActCausalTICOL022(MyObj82.PAID, 14);
                    ActalizacionExitosa = Itticol082.Actualizartticol222Cant(MyObj82);
                    break;
                case "ticol042":
                    twhcolDAL.ActCausalTICOL042(MyObj82.PAID, 14);
                    ActalizacionExitosa = Itticol082.Actualizartticol242Cant(MyObj82);
                    break;
                case "whcol131":
                    twhcolDAL.ActCausalcol131140(MyObj82.PAID, 5);
                    ActalizacionExitosa = Itticol082.Actualizartwhcol131Cant(MyObj82);
                    break;
                case "whcol130":
                    twhcolDAL.ActCausalcol131140(MyObj82.PAID, 5);
                    ActalizacionExitosa = Itticol082.Actualizartwhcol131Cant(MyObj82);
                    break;

            }
            return ActalizacionExitosa;
        }

        public static string getSequence(string PAIDOld)
        {
            string sequence = string.Empty;
            int indexSeparator = PAIDOld.IndexOf("-");
            string SQNB = PAIDOld.Substring(0, indexSeparator);
            string separator = PAIDOld.Substring(indexSeparator, 1);
            string SEC = PAIDOld.Substring(indexSeparator + 1);
            string complement = recursos.SeparatorAlphaNumeric(ref SEC);
            DataTable dtMaxSec = _idaltwhcol130.maximaSecuenciaUnion(SQNB + "-" + complement);
            if (dtMaxSec.Rows.Count > 0)
            {
                sequence = dtMaxSec.Rows[0]["sqnb"].ToString().Trim();
            }
            else
            {
                sequence = SQNB + separator + complement + "000";
            }
            return sequence;
        }

        protected void generateDropDownCostCenters()
        {

            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            DataTable resultado = idal.getCostCenters(ref strError);

            if (resultado.Rows.Count > 0)
            {
                int rowIndex = 0;
                ListItem itemS = null;
                itemS = new ListItem();
                itemS.Text = _idioma == "INGLES" ? "-- Select an Cost Center -- " : " -- Seleccione --";
                itemS.Value = "";
                dropDownCostCenters.Items.Insert(rowIndex, itemS);

                foreach (DataRow dr in resultado.Rows)
                {
                    itemS = new ListItem();
                    rowIndex = (int)resultado.Rows.IndexOf(dr);
                    itemS.Value = dr.ItemArray[0].ToString();
                    itemS.Text = dr.ItemArray[1].ToString();
                    dropDownCostCenters.Items.Insert(rowIndex + 1, itemS);
                }
            }
        }

        protected void generateDropDownReasonCodes()
        {
            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            DataTable resultado = idal.getReasonCodes(ref strError);

            if (resultado.Rows.Count > 0)
            {

                int rowIndex = 0;
                ListItem itemS = null;
                itemS = new ListItem();
                itemS.Text = _idioma == "INGLES" ? "-- Select an option -- " : " -- Seleccione --";
                itemS.Value = "";
                dropDownReasonCodes.Items.Insert(rowIndex, itemS);

                //ListItem itemS = null; 
                foreach (DataRow dr in resultado.Rows)
                {
                    itemS = new ListItem();
                    rowIndex = (int)resultado.Rows.IndexOf(dr);
                    itemS.Value = dr.ItemArray[0].ToString();
                    itemS.Text = dr.ItemArray[0].ToString() + " - " + dr.ItemArray[1].ToString();
                    dropDownReasonCodes.Items.Insert(rowIndex + 1, itemS);
                }
            }
        }

        protected void CargarIdioma()
        {
            //lblPalletId.Text = _textoLabels.readStatement(formName, _idioma, "lblPalletId");
            //btnSend.Text = _textoLabels.readStatement(formName, _idioma, "btnSend");
        }

        protected string mensajes(string tipoMensaje)
        {
            var retorno = _mensajesForm.readStatement(formName, _idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = _mensajesForm.readStatement(globalMessages, _idioma, ref tipoMensaje);
            }

            return retorno;
        }

    }
}