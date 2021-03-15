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
    public partial class LotItemAdjustment : System.Web.UI.Page
    {
        public static string strError = string.Empty;
        public static string _idioma;
        private static string formName;
        private static string globalMessages = "GlobalMessages";
        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        public static whusa.Utilidades.Recursos recursos = new whusa.Utilidades.Recursos();

        private static  Ent_twhcol130131 MyObj = new Ent_twhcol130131();
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


                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                if (control != null) { control.Text = strTitulo; }
            }
        }

        [WebMethod]
        public static string verifyPallet(string PAID){
            DataTable DTPallet = _idaltwhcol130.VerificarPalletID(ref PAID);

            if (DTPallet.Rows.Count > 0 )
            {
                var MyObjDT = DTPallet.Rows[0];
                MyObj.Error = false;
                HttpContext.Current.Session["TBL"] = MyObjDT["TBL"].ToString().Trim();
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
            }
            else
            {
                MyObj.Error = true;
                MyObj.errorMsg = "Pallet ID doesn´t exist";
                MyObj.TipeMsgJs = "Label";
                
            }

            return JsonConvert.SerializeObject(MyObj);
        }

        [WebMethod]
        public static string verifyItem(string ITEM)
        {
            Ent_ttwhcol016 obj016 = new Ent_ttwhcol016();
            obj016.item = ITEM.ToUpper().Trim();
            DataTable DTItemt = dal016.TakeMaterialInv_verificaItem_Param(ref obj016, ref strError);

            if (DTItemt.Rows.Count > 0)
            {
                var MyObjDT = DTItemt.Rows[0];

                MyObj.KTLC = MyObjDT["LOTE"].ToString();
                MyObj.ITEM = MyObjDT["t$item"].ToString();
                MyObj.DSCA = MyObjDT["DESCRIPCION"].ToString();
                MyObj.UNIT = MyObjDT["UNIDAD"].ToString();
                MyObj.Error = false;
                MyObj.errorMsg = string.Empty;
                MyObj.TipeMsgJs = string.Empty;
            }
            else
            {
                MyObj.Error = true;
                MyObj.errorMsg = "Item code doesn´t exist";
                MyObj.TipeMsgJs = "Label";

            }

            return JsonConvert.SerializeObject(MyObj); ;
        }

        [WebMethod]
        public static string verifyLot(string ITEM,string LOT)
        {
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
                MyObj.errorMsg = "Lot code doesn´t exist";
                MyObj.TipeMsgJs = "Label";

            }

            return JsonConvert.SerializeObject(MyObj);
        }


        [WebMethod]
        public static string verifyWarehouse(string CWAR)
        {
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
                MyObj.errorMsg = "Warehouse code doesn´t exist";
                MyObj.TipeMsgJs = "Label";

            }

            return JsonConvert.SerializeObject(MyObj);
        }

        [WebMethod]
        public static string verifyLoca(string LOCA)
        {
            DataTable DTLoca = dalTransfer.ConsultarWarehouse(LOCA);

            if (DTLoca.Rows.Count > 0)
            {
                var MyObjDT = DTLoca.Rows[0];
                MyObj.Error = false;
                MyObj.errorMsg = string.Empty;
                MyObj.TipeMsgJs = string.Empty;
            }
            else
            {
                MyObj.Error = true;
                MyObj.errorMsg = "Location code doesn´t exist";
                MyObj.TipeMsgJs = "Label";

            }

            return JsonConvert.SerializeObject(MyObj);
        }

        [WebMethod]
        public static string Save(Ent_twhcol028 twhcol028)
        {
            //twhcol028.EMNO =
            bool Res = _idaltwhcol028.insertRegistertwhcol028(ref twhcol028, ref strError);

            if (Res)
            {
                
                saveOriginTable(twhcol028);
                string strMaxSequence = getSequence(twhcol028.PAID);
                //string strNewSequence = currentSequience(strOldSequence);
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
            else
            {
                twhcol028.Error = true;
                twhcol028.ErrorMsg = "No se inserto correctamente";
                twhcol028.TypeMsgJs = "Label";

            }

            return JsonConvert.SerializeObject(twhcol028);
        }

        private static bool saveNewPalletOriginTable( ref Ent_twhcol028 twhcol028, string MaxSequence)
        {

            bool res = false;
            string separator = "-";
            twhcol028.PAID = recursos.GenerateNewPallet(MaxSequence, separator);
            string SQNB = twhcol028.PAID.Substring(0, twhcol028.PAID.IndexOf(separator));
            switch (HttpContext.Current.Session["TBL"].ToString())
            {
                case "ticol022":
                    Ent_tticol022 obj022 = new Ent_tticol022();
                    List<Ent_tticol022> list022 = new List<Ent_tticol022>();
                    obj022.pdno = string.IsNullOrEmpty(twhcol028.TLOT) ? " " : twhcol028.TLOT.ToUpper().Trim();
                    obj022.sqnb = twhcol028.PAID;
                    obj022.proc = 1;
                    obj022.logn = HttpContext.Current.Session["user"].ToString();
                    obj022.mitm = twhcol028.TITM;
                    obj022.qtdl = Convert.ToDecimal(twhcol028.TQTY);
                    obj022.cuni = "kg";
                    obj022.log1 = "NONE";
                    obj022.qtd1 = Convert.ToInt32(twhcol028.TQTY);
                    obj022.pro1 = 1;
                    obj022.log2 = "NONE";
                    obj022.qtd2 = Convert.ToInt32(twhcol028.TQTY);
                    obj022.pro2 = 2;
                    obj022.loca = " ";
                    obj022.norp = 1;
                    obj022.dele = 2;
                    obj022.logd = "NONE";
                    obj022.refcntd = 0;
                    obj022.refcntu = 0;
                    obj022.drpt = DateTime.Now;
                    obj022.urpt = HttpContext.Current.Session["user"].ToString();
                    obj022.acqt = Convert.ToDecimal(twhcol028.TQTY);
                    obj022.cwaf = twhcol028.TWAR;
                    obj022.cwat = twhcol028.TWAR;
                    obj022.aclo = "";
                    obj022.allo = 0;
                    list022.Add(obj022);
                    bool insert022 = Convert.ToBoolean(_idaltticol022.insertarRegistroSimple(ref obj022, ref strError));
                    bool insert222 = Convert.ToBoolean(_idaltticol022.InsertarRegistroTicol222(ref obj022, ref strError));
                    res = (insert222 == true && insert022 == true) ? true : false; 
                    break;
                case "ticol042":
                    Ent_tticol042 obj042 = new Ent_tticol042();
                    List<Ent_tticol042> list042 = new List<Ent_tticol042>();
                    obj042.pdno = string.IsNullOrEmpty(twhcol028.TLOT) ? " " : twhcol028.TLOT.ToUpper().Trim();
                    obj042.sqnb = twhcol028.PAID;
                    obj042.proc = 1;
                    obj042.logn = HttpContext.Current.Session["user"].ToString();
                    obj042.mitm = twhcol028.TITM;
                    obj042.qtdl = Convert.ToDouble(twhcol028.TQTY);
                    obj042.cuni = "kg";
                    obj042.log1 = "NONE";
                    obj042.qtd1 = Convert.ToInt32(twhcol028.TQTY);
                    obj042.pro1 = 1;
                    obj042.log2 = "NONE";
                    obj042.qtd2 = Convert.ToInt32(twhcol028.TQTY);
                    obj042.pro2 = 2;
                    obj042.loca = " ";
                    obj042.norp = 1;
                    obj042.dele = 2;
                    obj042.logd = "NONE";
                    obj042.refcntd = 0;
                    obj042.refcntu = 0;
                    obj042.drpt = DateTime.Now;
                    obj042.urpt = HttpContext.Current.Session["user"].ToString();
                    obj042.acqt = Convert.ToDouble(twhcol028.TQTY);
                    obj042.cwaf = twhcol028.TWAR;
                    obj042.cwat = twhcol028.TWAR;
                    obj042.aclo = "";
                    obj042.allo = 0;
                    list042.Add(obj042);
                    bool insert042 = Convert.ToBoolean(_idaltticol042.insertarRegistroSimple(ref obj042, ref strError));
                    bool insert242 = Convert.ToBoolean(_idaltticol042.InsertarRegistroTicol242(ref obj042, ref strError));
                    res = (insert242 == true && insert042 == true) ? true : false; 
                    break;
                case "whcol131":
                    Ent_twhcol130131 obj131 = new Ent_twhcol130131();
                    Ent_twhcol130131 MyObj131 = new Ent_twhcol130131();
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
                    MyObj131.UNIT = "KG";
                    MyObj131.QTYC = twhcol028.TQTY;//cantidad escaneada view aplicando factor
                    MyObj131.UNIC = "KG";
                    MyObj131.DATE = DateTime.Now.ToString("dd/MM/yyyy").ToString();//fecha de confirmacion 
                    MyObj131.CONF = "1";
                    MyObj131.RCNO = " ";//llena baan
                    MyObj131.DATR = "01/01/70";//llena baan
                    MyObj131.LOCA = " ";// enviamos vacio
                    MyObj131.DATL = DateTime.Now.ToString("dd/MM/yyyy").ToString();//llenar con fecha de hoy
                    MyObj131.PRNT = "1";// llenar en 1
                    MyObj131.DATP = DateTime.Now.ToString("dd/MM/yyyy").ToString();//llena baan
                    MyObj131.NPRT = "1";//conteo de reimpresiones 
                    MyObj131.LOGN = HttpContext.Current.Session["user"].ToString();// nombre de ususario de la session
                    MyObj131.LOGT = " ";//llena baan
                    MyObj131.STAT = "1";// LLENAR EN 1  
                    MyObj131.DSCA = "0";
                    MyObj131.COTP = "0";
                    MyObj131.FIRE = "1";
                    MyObj131.PSLIP = " ";
                    MyObj131.ALLO = "0";
                    res = _idaltwhcol130.Insertartwhcol131(MyObj131);
                    break;

            }
            return res;
        }

        private static string currentSequience(string strOldSequence)
        {
            string strNewSequence = string.Empty;
            int newSequence = Convert.ToInt32(strOldSequence)+1;
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
            bool ActalizacionExitosa = false;
            Ent_tticol082 MyObj82 = new Ent_tticol082();
            MyObj82.PAID = twhcol028.PAID;
            MyObj82.QTYC = "0";
            switch (HttpContext.Current.Session["TBL"].ToString())
            {
                case "ticol022":
                    ActalizacionExitosa = Itticol082.Actualizartticol222Cant(MyObj82);
                    break;
                case "ticol042":
                    ActalizacionExitosa = Itticol082.Actualizartticol242Cant(MyObj82);
                    break;
                case "whcol131":
                    ActalizacionExitosa = Itticol082.Actualizartwhcol131Cant(MyObj82);
                    break;
                case "whcol130":
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
            string SEC = PAIDOld.Substring(indexSeparator + 1);
            string complement = recursos.SeparatorAlphaNumeric(ref SEC);
            DataTable dtMaxSec = _idaltwhcol130.maximaSecuenciaUnion(SQNB+"-"+complement);
            if (dtMaxSec.Rows.Count > 0)
            {
                sequence = dtMaxSec.Rows[0]["sqnb"].ToString().Trim();
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
                itemS.Text = _idioma == "INGLES" ? "-- Select an option -- " : " -- Seleccione --";
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
                    itemS.Text = dr.ItemArray[1].ToString();
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