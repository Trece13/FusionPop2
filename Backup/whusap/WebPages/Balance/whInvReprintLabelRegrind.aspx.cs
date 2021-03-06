﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.Threading;
using whusa.Entidades;
using whusa.Interfases;

namespace whusap.WebPages.Balance
{
    public partial class whInvReprintLabelRegrind : System.Web.UI.Page
    {
        string strError = string.Empty;
        string strConteo = string.Empty;
        DataTable resultado = new DataTable();
        DataTable reimpresion = new DataTable();

        protected static InterfazDAL_tticol042 idal042 = new InterfazDAL_tticol042();
        Ent_tticol042 obj042 = new Ent_tticol042();
        protected static InterfazDAL_ttccol301 idal301 = new InterfazDAL_ttccol301();
        Ent_ttccol301 obj301 = new Ent_ttccol301();

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();

            if (!IsPostBack)
            {
                lblError.Visible = false;
                if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                string strTitulo = "Reprint label Regrind";
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                control.Text = strTitulo;
                Page.Form.DefaultButton = btnSend.UniqueID;

                if (lblIngreso.Text == "")
                {
                    lblIngreso.Text = "1";
                }
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            InsertarIngresoPagina();

            lblError.Visible = false;
            lblError.Text = string.Empty;
            obj042 = new Ent_tticol042();

            strError = string.Empty;
            strConteo = string.Empty;
            obj042.sqnb = txtSQNB.Text.Trim().ToUpperInvariant();
            resultado = idal042.ListaRegistro_ReprintRegrind(ref obj042, ref strError);

            if (resultado.Rows.Count < 1)
            {
                lblError.Visible = true;
                lblError.Text = strError + obj042.sqnb.Trim();
                return;
            }

            List<Ent_tticol042> parameterCollection042 = new List<Ent_tticol042>();

            strConteo = resultado.Rows[0]["T$NORP"].ToString();
            obj042.norp = Convert.ToInt32(strConteo) + 1;
            parameterCollection042.Add(obj042);
            
            int retorno = idal042.ActualizaRegistro_ReprintRegrind(ref parameterCollection042, ref strError);
            
            if (!string.IsNullOrEmpty(strError))
            {
                lblError.Visible = true;
                lblError.Text = strError;
                return;
            }

            if (retorno > 0)
            {
                reimpresion = idal042.ListaRegistro_ReprintRegrind(ref obj042, ref strError);

                if (reimpresion.Rows.Count < 1)
                {
                    lblError.Visible = true;
                    lblError.Text = strError + obj042.sqnb.Trim();
                    return;
                }
                DataRow filaImprimir = reimpresion.Rows[0];

                Session["FilaImprimir"] = filaImprimir;
                Session["descItem"] = reimpresion.Rows[0]["T$DSCA"];
                Session["unidad"] = reimpresion.Rows[0]["T$CUNI"];
                Session["machineItem"] = reimpresion.Rows[0]["T$MCNO"];
                Session["strTagid"] = reimpresion.Rows[0]["T$SQNB"];
                Session["reprinted"] = "1";

                StringBuilder script = new StringBuilder();
                script.Append("ventanaImp = window.open('../Labels/whInvLabelRegrind.aspx', ");
                script.Append("'ventanaImp', 'menubar=0,resizable=0,width=580,height=450');");
                script.Append("ventanaImp.moveTo(30, 0);");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", script.ToString(), true);
            }

            txtSQNB.Text = string.Empty;
        }

        void InsertarIngresoPagina()
        {
            if (lblIngreso.Text == "1")
            {
                lblIngreso.Text = "0";

                List<Ent_ttccol301> parameterCollection301 = new List<Ent_ttccol301>();

                obj301.user = Session["user"].ToString();
                obj301.come = "Reprint label Regrind ";
                obj301.refcntd = 0;
                obj301.refcntu = 0;
                parameterCollection301.Add(obj301);

                int retorno2 = idal301.insertarRegistro(ref parameterCollection301, ref strError);

                if (!string.IsNullOrEmpty(strError))
                {
                    lblError.Visible = true;
                    lblError.Text = strError;
                    return;
                }

            }
        }

    }
}