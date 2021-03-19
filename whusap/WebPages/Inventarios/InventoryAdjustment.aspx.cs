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
            divPrint.Visible = true;
            btnSave.Visible = false;
            tblPalletInfo.Visible = false;
        }

        [System.Web.Services.WebMethod()]
        public static string vallidatePalletID(string palletID)
        {
            //TextBox palletIdTextBox = (TextBox)e.Row.Cells[10].FindControl("palletId");
            //
            //quantityToReturn = "2";
            divPrint.Visible = true;
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
            divPrint.Visible = true;

            string retorno = string.Empty;

            if (string.IsNullOrEmpty(txtPalletId.Text.Trim()))
            {
                //minlenght.Enabled = true;
                //minlenght.ErrorMessage = mensajes("Please Fill all the Required  Fields.");
                //minlenght.IsValid = false;

                //return;
            }
            lblError.Text = "";
            lblConfirm.Text = "";
            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;
            obj.paid = txtPalletId.Text.ToUpperInvariant();
            //lblResult.Text = string.Empty;
            DataTable resultado = idal.invGetPalletInfo(ref obj, ref strError);
            DataTable resultadoSerie = idal.invGetPalletInfoSerie(ref strError);

            if (resultado == null || resultado.Rows.Count == 0)
            {
                lblError.Text = PalletIDdoesntexists;
                return;
            }

            //  SELECT 'ticol022' AS TBL,ticol022.T$PDNO AS T$ORNO,ticol022.T$SQNB AS T$PAID,ticol222.T$ACQT AS T$qtyc,
            //ticol022.T$MITM AS T$ITEM, tcibd001.t$dsca AS DSCA, ticol022.T$CUNI AS T$CUNI,ticol222.T$CWAF AS T$CWAR,
            //tcmcs003.t$dsca as DESCW, ticol222.T$ACLO AS T$LOCA

            //  SELECT 'ticol022' AS TBL,ticol022.T$PDNO AS T$ORNO,ticol022.T$SQNB AS T$PAID,ticol222.T$ACQT AS T$qtyc,
            //ticol022.T$MITM AS T$ITEM, tcibd001.t$dsca AS DSCA, ticol022.T$CUNI AS T$CUNI,ticol222.T$CWAF AS T$CWAR,
            //tcmcs003.t$dsca as DESCW, ticol222.T$ACLO AS T$LOCA



            string palletId, item, warehouse, lot, location, quantity, dsca, unit, waredesc;

            if (resultado.Rows.Count == 1)
            {
                if (resultadoSerie.Rows.Count > 0)
                {
                    Session["emno"] = resultadoSerie.Rows[0]["EMNO"].ToString();
                    Session["cdis"] = resultadoSerie.Rows[0]["CDIS"].ToString();
                    lblReason.Text = resultadoSerie.Rows[0]["EMNO"].ToString() + " - " + resultadoSerie.Rows[0]["NAMA"].ToString();
                    lblCost.Text = resultadoSerie.Rows[0]["CDIS"].ToString() + " - " + resultadoSerie.Rows[0]["DSCA"].ToString();
                }
                else
                {
                    lblReason.Text = string.Empty;
                    lblCost.Text = string.Empty;
                }

                DataRow dr = resultado.Rows[0];
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
                generateDropDownReasonCodes();
                generateDropDownCostCenters();

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
                    itemS.Text = dr.ItemArray[1].ToString();
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
                    itemS.Text = dr.ItemArray[1].ToString();
                    dropDownCostCenters.Items.Insert(rowIndex + 1, itemS);
                }
            }




        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            txtAdjustmentQuantity.Text = txtAdjustmentQuantity.Text.Trim() == string.Empty ? "0" : txtAdjustmentQuantity.Text.Trim();
            if (Convert.ToInt32(txtAdjustmentQuantity.Text.Trim()) <= 0)
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
            else{
                if (Convert.ToDecimal(txtAdjustmentQuantity.Text.Trim()) > (Convert.ToDecimal(lblQuantityValue.Text.Trim()) * 2))
                {
                    lblError.Text = "New quantity value doesnt allow";
                    txtPalletId.Enabled = true;
                    btnSave.Visible = true;
                    tblPalletInfo.Visible = true;
                    btnSave.Visible = true;
                    txtAdjustmentQuantity.Text = "";
                    txtAdjustmentQuantity.Focus();
                    return;
                }
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

            obj.PAID = txtPalletId.Text.ToUpperInvariant();
            obj.ITEM = lblItemValue.Text.Trim().ToUpper();
            //obj.LOCA = lblLocationValue.Text.Trim().ToUpper();
            obj.CWAR = lblWarehouseValue.Text;
            obj.LOCA = lblLocationValue.Text;
            obj.CLOT = lblLotValue.Text;
            obj.QTYA = Convert.ToDouble(txtAdjustmentQuantity.Text.Trim()) - Pquantity;
            obj.UNIT = lblUnitValue.Text.Trim();
            obj.LOGN = Session["user"].ToString(); ;
            //obj.CDIS = dropDownReasonCodes.SelectedItem.Value;
            //obj.EMNO = dropDownCostCenters.SelectedItem.Value; 
            obj.CDIS = Session["cdis"].ToString();
            obj.EMNO = Session["emno"].ToString();
            obj.PROC = 2;
            obj.ORNO = " ";
            obj.PONO = 0;
            obj.MESS = " ";
            obj.REFCNTD = 0;
            obj.REFCNTU = 0;
            parameterCollection025.Add(obj);

            var validSave = idal.insertRegistrItemAdjustment(ref parameterCollection025, ref strError);

            if (validSave > 0)
            {
                lblError.Text = "";
                lblConfirm.Text = mensajes("msjsave");
                //divTable.Visible = false;
                txtPalletId.Enabled = true;
                txtPalletId.Text = String.Empty;
                txtAdjustmentQuantity.Text = String.Empty;
                dropDownCostCenters.Items.Clear();
                dropDownReasonCodes.Items.Clear();
                lblCost.Text = string.Empty;
                lblReason.Text = string.Empty;
                btnSend.Visible = true;

                lblitemDesc.Text = Transfers.DescripcionItem(obj.ITEM); ;
                lblWorkOrder.Text = obj.PAID.Substring(0, obj.PAID.IndexOf("-"));
                lblPalletNum.Text =obj.PAID;
                lblInspector.Text =obj.LOGN;
                lblDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                lblShift.Text =Session["shif"].ToString();
                lblQuantityL.Text = lblQuantityValue.Text;
                codeItem.ImageUrl = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" +obj.ITEM.Trim()+ "&code=Code128&dpi=96";
                codePaid.ImageUrl = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + obj.PAID.Trim() + "&code=Code128&dpi=96";

                divPrint.Visible = true;

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

    }
}