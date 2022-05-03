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
using System.Threading;
using System.IO;
using whusa.Utilidades;
using System.Web.Configuration;

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialDevolReprintLabel : System.Web.UI.Page
    {
        #region Propiedades
            string strError = string.Empty;
            DataTable resultado = new DataTable();

            //Manejo idioma
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string formName;
            private static string globalMessages = "GlobalMessages";
            public static string _idioma;
            public static string valueprint;
            public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
            {
                txtWorkOrder.Focus();
                this.SetFocus(Page.Form.UniqueID);



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

                    string strTitulo = mensajes("encabezado");

                    // Si la llamada no proviene de otro formulario
                    if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                    Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                    if (control != null) { control.Text = strTitulo; }
                    Page.Form.DefaultButton = btnSend.UniqueID;

                    if (Session["resultado"] != null)
                    {
                        resultado = (DataTable)Session["resultado"];
                        grdRecords.DataSource = resultado;
                        grdRecords.DataBind();
                        if (PreviousPage != null)
                        {
                            if (PreviousPage.IsValid)
                            {
                                txtWorkOrder.Text = ((TextBox)Page.PreviousPage.FindControl(txtWorkOrder.UniqueID)).Text;
                            }
                        }
                        else if (Session["WorkOrder"] != null)
                        {
                            txtWorkOrder.Text = Session["WorkOrder"].ToString();
                        }
                        txtWorkOrder.ReadOnly = true;
                        lblOrder.Text = _idioma == "INGLES" ? "Order:" : "Orden:" + txtWorkOrder.Text;
                        btnSend.Visible = false;
                    }

                    Ent_ttccol301 data = new Ent_ttccol301()
                    {
                        user = HttpContext.Current.Session["user"].ToString(),
                        come = strTitulo,
                        refcntd = 0,
                        refcntu = 0
                    };

                    List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                    datalog.Add(data);

                    new InterfazDAL_ttccol301.insertarRegistro(ref datalog, ref strError);
                }

            }

        protected void btnSend_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrEmpty(txtWorkOrder.Text.Trim()))
                {
                    //minlenght.Enabled = true;
                    //minlenght.ErrorMessage = mensajes("workrequired");
                    //minlenght.IsValid = false;

                    return;
                }

                InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
                Ent_tticol125 obj = new Ent_tticol125();
                string strError = string.Empty;

                txtWorkOrder.Text = txtWorkOrder.Text.ToUpperInvariant();
                obj.pdno = txtWorkOrder.Text.ToUpperInvariant();
                resultado = idal.listaRegistrosOrden_Param(ref obj, ref strError, true); //DataTable resultado = TABLA; // 

                string findIn = string.Empty;
                // Validar si el numero de orden trae registros
                if (strError != string.Empty || resultado.Rows.Count < 1)
                {
                    strError = string.Empty;
                    // Si no encuentra registros en la principal busca en historico
                    resultado = idal.listaRegistrosOrden_ParamHis(ref obj, ref strError);
                    if (strError != string.Empty || resultado.Rows.Count < 1)
                    {
                        OrderError.IsValid = false;
                        txtWorkOrder.Focus();
                        grdRecords.DataSource = "";
                        grdRecords.DataBind();
                        return;
                    }
                    findIn = _idioma == "INGLES" ? " [ Find in  History ]" : " [ Buscar en Historial ]";
                    Session["update"] = 1;
                }

                lblOrder.Text = _idioma == "INGLES" ? "Order: " : "Orden: " + obj.pdno + findIn;
                grdRecords.DataSource = resultado;
                grdRecords.DataBind();

                if (Session["resultado"] == null) { Session["resultado"] = resultado; }

            }

        protected void grdRecords_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
                Ent_tticol125 obj = new Ent_tticol125();
                string strError = string.Empty;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //string prin = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["T$PRIN"].ToString();
                    string paid = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["T$PAID"].ToString();
                    //JC 190821 Actualiza el contador de impresiones cada vez que ingresen
                    if (IsPostBack)
                    {
                        idal.updateFieldPrint(ref paid, ref strError);
                        if (strError != string.Empty)
                        {
                            OrderError.IsValid = false;
                        }
                    }

                    var printed = idal.queryFieldPrint(ref paid, ref strError);
                    string prin = printed.Rows[0]["T$PRIN"].ToString().Trim();
                    valueprint = prin;
                    //obj.prin = idal.Rows[0].["T$PRIN"]

                    // ((Button)e.Row.Cells[7].FindControl("btnPrint")).OnClientClick = "printTag(" + FilaSerializada.Trim() + ")";
                    //((Button)e.Row.Cells[7].FindControl("btnPrint")).Text = (prin.Trim().Equals("1")
                    //    ? _idioma == "INGLES" ? "Print" : "Imprimir" : _idioma == "INGLES" ? "Reprint" : "Reimprimir");

                    //Cambia el estado del campo print en la tabla ticol125
                    //JC 190821 Actualiza el contador de impresiones cada vez que ingresen
                    //if (prin.Trim().Equals("2"))
                    //{
                    //idal.updateFieldPrint(ref paid, ref strError);
                    //if (strError != string.Empty)
                    //{
                    //    OrderError.IsValid = false;
                    //}
                    //}
                    //JC 190821 Actualiza el contador de impresiones cada vez que ingresen
                    if (Convert.ToInt16(valueprint) > 1)
                    {
                        ((Button)e.Row.Cells[7].FindControl("btnPrint")).Text = (_idioma == "INGLES" ? "Reprint" : "Reimprimir");
                    }
                    else
                    {
                        ((Button)e.Row.Cells[7].FindControl("btnPrint")).Text = (_idioma == "INGLES" ? "Print" : "Imprimir");
                    }
                }
            }

        protected void grdRecords_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                if (e.CommandName == "btnPrint_Click")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = grdRecords.Rows[index];
                    DataRow reg;
                    if (row.DataItem == null)
                    {
                        if (Session["resultado"] != null)
                        {
                            resultado = (DataTable)Session["resultado"];
                            grdRecords.DataSource = resultado;
                            resultado.Rows[0]["T$PRIN"] = valueprint;
                            grdRecords.DataBind();
                            reg = resultado.Rows[index];
                            Session["FilaImprimir"] = reg;
                            //JC 190821 Evitar que la primera vez imprima reprint en la etiqueta
                            if (Convert.ToInt16(resultado.Rows[0]["T$PRIN"].ToString()) <= 1)
                            {
                                Session["Reprint"] = "no";
                            }
                            else
                            {
                                Session["Reprint"] = "yes";
                            }
                            Session["MaterialDesc"] = reg["DESCRIPCION"].ToString().Trim();
                            Session["MaterialCode"] = reg["T$ITEM"].ToString().Trim();
                            Session["codePaid"] = reg["T$PAID"].ToString().Trim();
                            //JC 240821 Ajuste para que tome el lote correcto cuando sea la tabla whcol131
                            //Session["Lot"] = reg["T$PDNO"].ToString().Trim();
                            Session["Lot"] = reg["T$CLOT"].ToString().Trim();
                            Session["Quantity"] = reg["T$REQT"].ToString().Trim() + " " + reg["UNIDAD"].ToString().Trim();
                            Session["Origin"] = reg["T$PDNO"].ToString().Trim();
                            Session["Supplier"] = reg["T$USER"].ToString().Trim();
                            Session["RecibedBy"] = Session["User"].ToString();
                            Session["RecibedOn"] = DateTime.Now.ToString();

                            StringBuilder script = new StringBuilder();
                            if (HttpContext.Current.Session["navigator"].ToString() == "EDG")
                            {
                                script.Append("ventanaImp = window.open('../Labels/RedesingLabels/1RawMaterialME.aspx', ");
                            }
                            else
                            {
                                script.Append("ventanaImp = window.open('../Labels/RedesingLabels/1RawMaterial.aspx', ");
                            }
                            
                            script.Append("'ventanaImp', 'menubar=0,resizable=0,width=700,height=450');");
                            script.Append("ventanaImp.moveTo(30, 0);");
                            //script.Append("ventanaImp = window.open('../Labels/whInvPrintLabel.aspx', ");
                            //script.Append("'ventanaImp', 'menubar=0,resizable=0,width=580,height=450');");
                            //script.Append("ventanaImp.moveTo(30, 0);");
                            //script.Append("setTimeout (ventanaImp.close(), 20000);");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", script.ToString(), true);
                        }
                    }
                }
            }

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            //lblWorkOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
            btnSend.Text = _textoLabels.readStatement(formName, _idioma, "btnSend");
            //minlenght.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "regularWorkOrder");
            RequiredField.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "requiredWorkOrder");
            OrderError.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "customWorkOrder");
            grdRecords.Columns[0].HeaderText = _textoLabels.readStatement(formName, _idioma, "headPosition");
            grdRecords.Columns[1].HeaderText = _textoLabels.readStatement(formName, _idioma, "headItem");
            grdRecords.Columns[2].HeaderText = _textoLabels.readStatement(formName, _idioma, "headDescription");
            grdRecords.Columns[3].HeaderText = _textoLabels.readStatement(formName, _idioma, "headWarehouse");
            grdRecords.Columns[4].HeaderText = _textoLabels.readStatement(formName, _idioma, "headUnit");
            grdRecords.Columns[5].HeaderText = _textoLabels.readStatement(formName, _idioma, "headReturnQty");
            grdRecords.Columns[6].HeaderText = _textoLabels.readStatement(formName, _idioma, "headLot");
            grdRecords.Columns[7].HeaderText = _textoLabels.readStatement(formName, _idioma, "headPallet");
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

        #endregion
    }
}