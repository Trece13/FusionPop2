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
using Newtonsoft.Json;
using System.Web.Configuration;
using whusa;

namespace whusap.WebPages.Inventarios
{
    public partial class InventoryAdjustment : System.Web.UI.Page
    {
        #region Propiedades
        string strError = string.Empty;
        string Aplicacion = "WEBAPP";
        private static string globalMessages = "GlobalMessages";
        //Manejo idioma
        public static string PalletIDdoesntexists = mensajesStatic("PalletIDdoesntexists");
        public static string PalletIDstatusdoesntallowadjustment = mensajesStatic("PalletIDstatusdoesntallowadjustment");
        public static string Adjustmentquantitycannotbezero = mensajesStatic("Adjustmentquantitycannotbezero");
        public static string AdjustmentquantityshouldbelessthanexistingQty = mensajesStatic("AdjustmentquantityshouldbelessthanexistingQty");
        public static string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        public static IntefazDAL_transfer Transfers = new IntefazDAL_transfer();
        public static Ent_twhcol025 obj = new Ent_twhcol025();
        public static whusa.Utilidades.Recursos recursos = new whusa.Utilidades.Recursos();
        public static IntefazDAL_tticol082 Itticol082 = new IntefazDAL_tticol082();
        private static InterfazDAL_twhcol130 _idaltwhcol130 = new InterfazDAL_twhcol130();
        public static InterfazDAL_twhcol122 twhcolDAL = new InterfazDAL_twhcol122();
        private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
        private static InterfazDAL_tticol042 _idaltticol042 = new InterfazDAL_tticol042();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();

        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        private static string formName;
        public static string _idioma;
        public static double Pquantity;
        public string emno;
        public string cdis;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            txtPalletId.Focus();
            Page.Form.DefaultButton = btnSend.UniqueID;

            var ctrlName = Request.Params[Page.postEventSourceID];
            var args = Request.Params[Page.postEventArgumentID];
            //Page.Form.Unload += new EventHandler(Form_Unload);

            //HandleCustomPostbackEvent(ctrlName, args);

            if (!IsPostBack)
            {
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
            //generateDropDownReasonCodes();
            //generateDropDownCostCenters();
            divPrint.Visible = false;
            btnSave.Visible = false;
            tblPalletInfo.Visible = false;
        }

        [System.Web.Services.WebMethod()]
        public static string vallidatePalletID(string palletID)
        {
            //TextBox palletIdTextBox = (TextBox)e.Row.Cells[10].FindControl("palletId");
            //
            //quantityToReturn = "2";
            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;
            string retorno = string.Empty;
            //TextBox quantity = (TextBox)e.Row.Cells[10].FindControl("palletId");

            obj.paid = palletID.Trim().ToUpperInvariant();
            //obj.reqt = Convert.ToInt32(fila.GetValue(8).ToString().Trim());


            decimal palletQuantity = 0;
            decimal status;
            string tableName = string.Empty;

            DataTable resultado = idal.vallidatePalletID(ref obj, ref strError);

            if (resultado.Rows.Count < 1) { retorno = PalletIDdoesntexists; }
            else
            {
                foreach (DataRow dr in resultado.Rows)
                {
                    status = Convert.ToDecimal(dr.ItemArray[8].ToString());
                    palletQuantity = Convert.ToDecimal(dr.ItemArray[3].ToString());
                    tableName = dr.ItemArray[0].ToString();

                    if (((tableName == "whcol131") || (tableName == "whcol130")) && (status != 3))
                    {
                        retorno = PalletIDstatusdoesntallowadjustment;
                        break;
                    }
                    else if (((tableName == "ticol022") || (tableName == "ticol042")) && (status != 7))
                    {
                        retorno = PalletIDstatusdoesntallowadjustment;
                        break;
                    }
                }
            }
            return retorno;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {

            lblError.Text = "";
            lblConfirm.Text = "";
            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;
            obj.paid = txtPalletId.Text.ToUpperInvariant();

            HttpContext.Current.Session["flag022"] = 0;
            HttpContext.Current.Session["flag042"] = 0;
            HttpContext.Current.Session["flag131"] = 0;
            divPrint.Visible = false;

            string retorno = string.Empty;


            decimal palletQuantity = 0;
            decimal status;
            string tableName = string.Empty;

            DataTable resultadoPaid = idal.vallidatePalletID(ref obj, ref strError);

            if (resultadoPaid.Rows.Count < 1) { retorno = PalletIDdoesntexists; }
            else
            {
                foreach (DataRow dr in resultadoPaid.Rows)
                {
                    status = Convert.ToDecimal(dr.ItemArray[8].ToString());
                    palletQuantity = Convert.ToDecimal(dr.ItemArray[3].ToString());
                    tableName = dr.ItemArray[0].ToString();

                    if (((tableName == "whcol131") || (tableName == "whcol130")) && (status != 3))
                    {
                        lblError.Text = PalletIDstatusdoesntallowadjustment;
                        return;
                    }
                    else if (((tableName == "ticol022") || (tableName == "ticol042")) && (status != 7))
                    {
                        lblError.Text = PalletIDstatusdoesntallowadjustment;
                        return;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtPalletId.Text.Trim()))
            {
                //minlenght.Enabled = true;
                //minlenght.ErrorMessage = mensajes("Please Fill all the Required  Fields.");
                //minlenght.IsValid = false;

                //return;
            }
            DataTable resultado = idal.invGetPalletInfo(ref obj, ref strError);
            DataTable resultadoSerie = idal.invGetPalletInfoSerie(ref strError);

            if (resultado == null || resultado.Rows.Count == 0)
            {
                lblError.Text = PalletIDdoesntexists;
                return;
            }

            string palletId, item, warehouse, lot, location, quantity, dsca, unit, waredesc, machine, tbl, pono;

            if (resultado.Rows.Count == 1)
            {
                if (resultadoSerie.Rows.Count > 0)
                {
                    Session["emno"] = resultadoSerie.Rows[0]["EMNO"].ToString();
                    Session["cdis"] = resultadoSerie.Rows[0]["CDIS"].ToString();
                    lblReason.Text = resultadoSerie.Rows[0]["CDIS"].ToString() + " - " + resultadoSerie.Rows[0]["DSCA"].ToString();
                    lblCost.Text = resultadoSerie.Rows[0]["EMNO"].ToString() + " - " + resultadoSerie.Rows[0]["NAMA"].ToString();
                }
                else
                {
                    lblReason.Text = string.Empty;
                    lblCost.Text = string.Empty;
                }

                DataRow dr = resultado.Rows[0];
                tbl = dr.ItemArray[0].ToString();
                if (tbl.Trim() == "ticol022")
                {
                    HttpContext.Current.Session["flag022"] = 1;
                    HttpContext.Current.Session["flag131"] = 0;
                    HttpContext.Current.Session["flag042"] = 0;
                }
                else if (tbl.Trim() == "whcol131")
                {
                    HttpContext.Current.Session["flag022"] = 0;
                    HttpContext.Current.Session["flag131"] = 1;
                    HttpContext.Current.Session["flag042"] = 0;
                }
                else if (tbl.Trim() == "ticol042")
                {
                    HttpContext.Current.Session["flag022"] = 0;
                    HttpContext.Current.Session["flag131"] = 0;
                    HttpContext.Current.Session["flag042"] = 1;
                }

                palletId = dr.ItemArray[2].ToString();
                quantity = dr.ItemArray[3].ToString();
                item = dr.ItemArray[4].ToString();
                dsca = dr.ItemArray[5].ToString();
                unit = dr.ItemArray[6].ToString();
                warehouse = dr.ItemArray[7].ToString();
                waredesc = dr.ItemArray[8].ToString();
                lot = dr.ItemArray[10].ToString();
                location = dr.ItemArray[9].ToString();
                Pquantity = Convert.ToDouble(quantity);
                machine = dr.ItemArray[11].ToString();
                pono = dr.ItemArray[12].ToString();
                HttpContext.Current.Session["pono"] = pono;

                lblPalletId1Value.Text = palletId;
                lblItemValue.Text = item;
                lblItemDescValue.Text = dsca;
                lblWarehouseValue.Text = warehouse;
                lblWarehouseDescValue.Text = waredesc;
                lblLotValue.Text = lot;
                lblLocationValue.Text = location;
                lblQuantityValue.Text = quantity;
                lblUnitValue.Text = unit;
                lblUnitValue1.Text = unit;
                tblPalletInfo.Visible = true;
                btnSend.Visible = false;
                btnSave.Visible = true;
                lblMachine.Text = machine;
                generateDropDownReasonCodes();
                generateDropDownCostCenters();
                dropDownCostCenters.SelectedValue = Session["emno"].ToString();
                dropDownReasonCodes.SelectedValue = Session["cdis"].ToString();
                if (Session["emno"].ToString() == "DO3062" && Session["cdis"].ToString() == "CS1004")
                {
                    dropDownCostCenters.Visible = true;
                    dropDownReasonCodes.Visible = true;
                    lblReason.Visible = false;
                    lblCost.Visible = false;
                }
                else
                {
                    dropDownCostCenters.Visible = false;
                    dropDownReasonCodes.Visible = false;
                    lblReason.Visible = true;
                    lblCost.Visible = true;
                }
            }
        }

        [WebMethod]
        public static string GetObj()
        {
            return JsonConvert.SerializeObject(obj);
        }

        protected void OptionList_value(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            if (ddl.SelectedIndex == 0)
            {
                return;
            }
            else
            {
                return;
            }
        }

        protected void grdRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string prin = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["T$PRIN"].ToString();
                // ((Button)e.Row.Cells[7].FindControl("btnPrint")).OnClientClick = "printTag(" + FilaSerializada.Trim() + ")";
                ((Label)e.Row.Cells[8].FindControl("prin")).Text = prin;
            }
        }

        protected void Form_Unload(object sender, EventArgs e)
        {
            Session["FilaImprimir"] = null;
            Session["resultado"] = null;
            Session["WorkOrder"] = null;
        }

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblPalletId.Text = _textoLabels.readStatement(formName, _idioma, "lblPalletId");
            btnSend.Text = _textoLabels.readStatement(formName, _idioma, "btnSend");
            //btnSave.Text = _textoLabels.readStatement(formName, _idioma, "btnSave");
            ////  minlenght.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "regularWorkOrder");
            // // RequiredFieldPallet.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "RequiredFieldPallet");
            // // PalletError.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "customWorkOrder");
            //  grdRecords.Columns[0].HeaderText = _textoLabels.readStatement(formName, _idioma, "headPosition");
            //  grdRecords.Columns[1].HeaderText = _textoLabels.readStatement(formName, _idioma, "headItem");
            //  grdRecords.Columns[2].HeaderText = _textoLabels.readStatement(formName, _idioma, "headDescription");
            //  grdRecords.Columns[3].HeaderText = _textoLabels.readStatement(formName, _idioma, "headWarehouse");
            //  grdRecords.Columns[4].HeaderText = _textoLabels.readStatement(formName, _idioma, "headLot");
            //  grdRecords.Columns[5].HeaderText = _textoLabels.readStatement(formName, _idioma, "headPalletID");
            //  grdRecords.Columns[6].HeaderText = _textoLabels.readStatement(formName, _idioma, "headReturnQty");
            //  grdRecords.Columns[7].HeaderText = _textoLabels.readStatement(formName, _idioma, "headUnit");
            //  grdRecords.Columns[8].HeaderText = _textoLabels.readStatement(formName, _idioma, "headConfirmed");
            //grdRecords.Columns[9].HeaderText = _textoLabels.readStatement(formName, _idioma, "headConfirmed");
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
                    itemS.Text = dr.ItemArray[0].ToString() + "-" + dr.ItemArray[1].ToString();
                    dropDownReasonCodes.Items.Insert(rowIndex + 1, itemS);
                }
            }
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
                    itemS.Text = dr.ItemArray[0].ToString() + "-" + dr.ItemArray[1].ToString();
                    dropDownCostCenters.Items.Insert(rowIndex + 1, itemS);
                }
            }




        }
        #endregion
        public string ReplaceDecimal(string number)
        {
            return number.Contains(",") ? number.Replace(",", ".") : number.Replace(".", ",");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            txtAdjustmentQuantity.Text = txtAdjustmentQuantity.Text.Trim() == string.Empty ? "0" : txtAdjustmentQuantity.Text.Trim();
            string inputQtyStr = txtAdjustmentQuantity.Text;
            decimal inputQty;

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

            if (inputQty < 0)
            {
                lblError.Text = "";
                lblError.Text = Adjustmentquantitycannotbezero;
                txtPalletId.Enabled = true;
                txtPalletId.Text = String.Empty;
                btnSend.Visible = true;
                tblPalletInfo.Visible = true;
                btnSave.Visible = true;
                txtAdjustmentQuantity.Text = "";
                txtAdjustmentQuantity.Focus();
                return;
            }
            else
            {
                //if (lblQuantityValue.Text.Trim() != "0")
                //{
                    if (inputQty > (Convert.ToDecimal(lblQuantityValue.Text) * 2))
                    {
                        lblError.Text = "New quantity not allowed, máximum double”";
                        txtPalletId.Enabled = true;
                        btnSave.Visible = true;
                        tblPalletInfo.Visible = true;
                        btnSave.Visible = true;
                        txtAdjustmentQuantity.Text = "";
                        txtAdjustmentQuantity.Focus();
                        return;
                    }
                //}

            }


            //if (Convert.ToInt32(txtAdjustmentQuantity.Text.Trim()) > Convert.ToInt32(lblQuantityValue.Text.Trim()))
            //{
            //    lblError.Text = AdjustmentquantityshouldbelessthanexistingQty;
            //    txtPalletId.Enabled = true;
            //    txtPalletId.Text = String.Empty;
            //    btnSend.Visible = true;
            //    return;
            //}
            InterfazDAL_twhcol025 idal = new InterfazDAL_twhcol025();
            string strError = string.Empty;
            List<Ent_twhcol025> parameterCollection025 = new List<Ent_twhcol025>();
            //T$PAID, T$ITEM, T$LOCA, T$CLOT, T$QTYA, T$UNIT, T$DATE, T$LOGN, T$EMNO, T$PROC, T$REFCNTD,T$REFCNTU
            string PAID = txtPalletId.Text.ToUpperInvariant().Trim();
            int consecutivoPalletID = 0;
            string strMaxSequence = getSequence(PAID, "A");
            string separator = "-";
            string newPallet = recursos.GenerateNewPallet(strMaxSequence, separator);
            string SQNB = PAID.Substring(0, PAID.IndexOf(separator));

            Ent_tticol082 MyObj82 = new Ent_tticol082();
            MyObj82.PAID = PAID;
            MyObj82.QTYC = "0";

            obj.PAID = PAID;
            obj.ITEM = lblItemValue.Text.Trim().ToUpper();
            //obj.LOCA = lblLocationValue.Text.Trim().ToUpper();
            obj.CWAR = lblWarehouseValue.Text;
            obj.LOCA = lblLocationValue.Text;
            obj.CLOT = lblLotValue.Text;
            obj.QTYA = Convert.ToDouble(inputQty) - Pquantity;
            obj.UNIT = lblUnitValue.Text.Trim();
            obj.LOGN = Session["user"].ToString(); ;
            //obj.CDIS = dropDownReasonCodes.SelectedItem.Value;
            //obj.EMNO = dropDownCostCenters.SelectedItem.Value; 
            //JC 280921 Evitar que tome los valores por defecto de la variable de session
            //obj.CDIS = Session["cdis"].ToString();
            //obj.EMNO = Session["emno"].ToString();
            obj.CDIS = dropDownReasonCodes.SelectedValue;
            obj.EMNO = dropDownCostCenters.SelectedValue;
            obj.PROC = 2;
            obj.ORNO = " ";
            obj.PONO = Convert.ToInt32(HttpContext.Current.Session["pono"].ToString());
            obj.MESS = " ";
            obj.REFCNTD = 0;
            obj.REFCNTU = 0;
            parameterCollection025.Add(obj);

            var validSave = idal.insertRegistrItemAdjustment(ref parameterCollection025, ref strError);

            if (validSave > 0)
            {
                if (Convert.ToInt32(HttpContext.Current.Session["flag022"].ToString().Trim()) == 1)
                {
                    twhcolDAL.ActCausalTICOL022(PAID, 14);

                    Ent_tticol022 MyObj = new Ent_tticol022();
                    MyObj.pdno = obj.CLOT;
                    MyObj.sqnb = newPallet;
                    MyObj.proc = 2;
                    MyObj.logn = HttpContext.Current.Session["user"].ToString().Trim();
                    MyObj.mitm = obj.ITEM.Trim();
                    MyObj.qtdl = Convert.ToDecimal(inputQty);
                    MyObj.cuni = obj.UNIT;//CUNI;
                    MyObj.log1 = "NONE";
                    MyObj.qtd1 = Convert.ToInt32(inputQty);
                    MyObj.pro1 = 2;
                    MyObj.log2 = "NONE";
                    MyObj.qtd2 = Convert.ToInt32(inputQty);
                    MyObj.pro2 = 2;
                    MyObj.loca = obj.LOCA;
                    MyObj.norp = 1;
                    MyObj.dele = 7;
                    MyObj.logd = "NONE";
                    MyObj.refcntd = 0;
                    MyObj.refcntu = 0;
                    MyObj.drpt = DateTime.Now;
                    MyObj.urpt = HttpContext.Current.Session["user"].ToString().Trim();
                    MyObj.acqt = Convert.ToDecimal(inputQty);
                    MyObj.cwaf = obj.CWAR;//CWAR;
                    MyObj.cwat = obj.CWAR;//CWAR;
                    MyObj.aclo = obj.LOCA;
                    MyObj.allo = 0;// Convert.ToDecimal(txtAdjustmentQuantity.Text.Trim()); ;

                    var validateSave = _idaltticol022.insertarRegistroSimple(ref MyObj, ref strError);
                    var validateSaveTicol222 = _idaltticol022.InsertarRegistroTicol222(ref MyObj, ref strError);
                    var ActalizacionExitosa = Itticol082.Actualizartticol222Cant(MyObj82);

                }
                else if (Convert.ToInt32(HttpContext.Current.Session["flag042"].ToString().Trim()) == 1)
                {
                    twhcolDAL.ActCausalTICOL042(PAID, 14);

                    Ent_tticol042 MyObj = new Ent_tticol042();
                    MyObj.pdno = obj.CLOT;
                    MyObj.sqnb = newPallet;
                    MyObj.proc = 2;
                    MyObj.logn = HttpContext.Current.Session["user"].ToString().Trim();
                    MyObj.mitm = obj.ITEM.Trim();
                    MyObj.qtdl = Convert.ToDouble(inputQty);
                    MyObj.cuni = obj.UNIT;//CUNI;
                    MyObj.log1 = "NONE";
                    MyObj.qtd1 = Convert.ToDecimal(inputQty);
                    MyObj.pro1 = 2;
                    MyObj.log2 = "NONE";
                    MyObj.qtd2 = Convert.ToDecimal(inputQty);
                    MyObj.pro2 = 2;
                    MyObj.loca = obj.LOCA;
                    MyObj.norp = 1;
                    MyObj.dele = 7;
                    MyObj.logd = "NONE";
                    MyObj.refcntd = 0;
                    MyObj.refcntu = 0;
                    MyObj.drpt = DateTime.Now;
                    MyObj.urpt = HttpContext.Current.Session["user"].ToString().Trim();
                    MyObj.acqt = Convert.ToDouble(inputQty);
                    MyObj.cwaf = obj.CWAR;//CWAR;
                    MyObj.cwat = obj.CWAR;//CWAR;
                    MyObj.aclo = obj.LOCA;
                    MyObj.allo = 0;// Convert.ToDecimal(txtAdjustmentQuantity.Text.Trim());//Convert.ToDecimal(qtyt_act.ToString());


                    var validateSave = _idaltticol042.insertarRegistroSimple(ref MyObj, ref strError);
                    var validateSaveTicol242 = _idaltticol042.InsertarRegistroTicol242(ref MyObj, ref strError);
                    var ActalizacionExitosa = Itticol082.Actualizartticol242Cant(MyObj82);

                }
                else if (Convert.ToInt32(HttpContext.Current.Session["flag131"].ToString().Trim()) == 1)
                {
                    twhcolDAL.ActCausalcol131140(PAID, 5);
                    Ent_twhcol130131 MyObj = new Ent_twhcol130131();
                    MyObj.OORG = "2";// Order type escaneada view 
                    MyObj.ORNO = newPallet.Substring(0, newPallet.IndexOf("-"));
                    MyObj.ITEM = obj.ITEM.Trim();
                    MyObj.PAID = newPallet;
                    MyObj.PONO = HttpContext.Current.Session["pono"].ToString();
                    MyObj.SEQN = "1";
                    MyObj.CLOT = obj.CLOT;//CLOT.ToUpper();// lote VIEW
                    MyObj.CWAR = obj.CWAR;//CWAR.ToUpper();
                    MyObj.QTYS = inputQty.ToString();//QTYS;// cantidad escaneada view 
                    MyObj.UNIT = obj.UNIT;//UNIT;//unit escaneada view
                    MyObj.QTYC = inputQty.ToString();//QTYS;//cantidad escaneada view aplicando factor
                    MyObj.QTYA = inputQty.ToString();//QTYS;//cantidad escaneada view aplicando factor
                    MyObj.UNIC = obj.UNIT;//UNIT;//unidad view stock
                    MyObj.DATE = DateTime.Now.ToString("dd/MM/yyyy").ToString();//fecha de confirmacion 
                    MyObj.CONF = "1";
                    MyObj.RCNO = " ";//llena baan
                    MyObj.DATR = DateTime.Now.ToString("dd/MM/yyyy").ToString();//llena baan
                    MyObj.LOCA = obj.LOCA;//LOCA.ToUpper();// enviamos vacio 
                    MyObj.DATL = DateTime.Now.ToString("dd/MM/yyyy").ToString();//llenar con fecha de hoy
                    MyObj.PRNT = "1";// llenar en 1
                    MyObj.DATP = DateTime.Now.ToString("dd/MM/yyyy").ToString();//llena baan
                    MyObj.NPRT = "1";//conteo de reimpresiones 
                    MyObj.LOGN = HttpContext.Current.Session["user"].ToString().Trim();// nombre de ususario de la session
                    MyObj.LOGT = " ";//llena baan
                    MyObj.STAT = "3";// LLENAR EN 3 
                    MyObj.DSCA = " ";
                    MyObj.COTP = " ";
                    MyObj.FIRE = "2";
                    MyObj.PSLIP = " ";
                    MyObj.ALLO = "0"; //txtAdjustmentQuantity.Text.Trim();


                    bool Insertsucces = twhcol130DAL.Insertartwhcol131(MyObj);
                    var ActalizacionExitosa = Itticol082.Actualizartwhcol131Cant(MyObj82);
                }

                lblError.Text = "";
                lblConfirm.Text = mensajes("msjsave");
                //divTable.Visible = false;
                lblitemDesc.Text = Transfers.DescripcionItem(obj.ITEM); ;
                lblWorkOrder.Text = obj.PAID.Substring(0, obj.PAID.IndexOf("-"));
                lblPalletNum.Text = newPallet.Substring(obj.PAID.IndexOf("-") + 1);
                lblInspector.Text = obj.LOGN;
                lblDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                //lblShift.Text =Session["shif"].ToString();
                lblQuantityL.Text = txtAdjustmentQuantity.Text.Trim() + "  " + lblUnitValue.Text.Trim();
                codeItem.ImageUrl = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + obj.ITEM.Trim() + "&code=Code128&dpi=96";
                codePaid.ImageUrl = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + newPallet.Trim() + "&code=Code128&dpi=96";

                divPrint.Visible = true;

                txtPalletId.Enabled = true;
                txtPalletId.Text = String.Empty;
                txtAdjustmentQuantity.Text = String.Empty;
                dropDownCostCenters.Items.Clear();
                dropDownReasonCodes.Items.Clear();
                lblCost.Text = string.Empty;
                lblReason.Text = string.Empty;
                btnSend.Visible = true;

            }
            else
            {
                lblConfirm.Text = "";
                lblError.Text = mensajes("errorsave");
                return;
            }
        }

        protected static string mensajesStatic(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("InventoryAdjustment.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
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
                sequence = SQNB + "-" + complement + "000";
            }

            return sequence;
        }

    }
}