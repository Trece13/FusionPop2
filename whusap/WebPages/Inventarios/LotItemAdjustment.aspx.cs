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

        private static  Ent_twhcol130131 MyObj = new Ent_twhcol130131();
        private static InterfazDAL_ttwhcol016 dal016 = new InterfazDAL_ttwhcol016();
        private static InterfazDAL_twhltc100 dal100 = new InterfazDAL_twhltc100();
        private static InterfazDAL_tticol100 dalticol100 = new InterfazDAL_tticol100();
        private static InterfazDAL_twhcol130 _idaltwhcol130 = new InterfazDAL_twhcol130();
        private static IntefazDAL_transfer dalTransfer = new IntefazDAL_transfer();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            generateDropDownReasonCodes();
            generateDropDownCostCenters();
            var ctrlName = Request.Params[Page.postEventSourceID];
            var args = Request.Params[Page.postEventArgumentID];
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
        }

        [WebMethod]
        public static string verifyPallet(string PAID){
            DataTable DTPallet = _idaltwhcol130.VerificarPalletID(ref PAID);

            if (DTPallet.Rows.Count > 0 )
            {
                var MyObjDT = DTPallet.Rows[0];
                MyObj.ITEM = MyObjDT["ITEM"].ToString();
                MyObj.CLOT = MyObjDT["LOT"].ToString();
                MyObj.QTYA = MyObjDT["QTYT"].ToString();
                MyObj.UNIT = MyObjDT["UNIT"].ToString();
                MyObj.CWAR = MyObjDT["CWAT"].ToString();
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
            //DataTable DTPallet = _idaltwhcol130.VerificarPalletID(ref PAID);
            Ent_ttwhcol016 obj016 = new Ent_ttwhcol016();
            obj016.item = ITEM.ToUpper().Trim();
            dal016.TakeMaterialInv_verificaItem_Param(ref obj016, ref strError);

            return JsonConvert.SerializeObject("MyObj"); ;
        }

        [WebMethod]
        public static string verifyLot(string ITEM,string LOT)
        {
            Ent_twhltc100 data100 = new Ent_twhltc100() { item = ITEM, clot = LOT };
            var validaLote = dal100.listaRegistro_Clot(ref data100, ref strError);

            return JsonConvert.SerializeObject("MyObj");
        }


        [WebMethod]
        public static string verifyWarehouse(string CWAR)
        {
            Ent_tticol100 myObj100 = new Ent_tticol100();
            myObj100.cwar = CWAR.Trim().ToUpper();
            DataTable DTWare = dalticol100.selecthandletwhwmd200(ref myObj100, ref strError);

            return JsonConvert.SerializeObject("MyObj");
        }

        [WebMethod]
        public static string verifyLoca(string LOCA)
        {
            DataTable DTLoca = dalTransfer.ConsultarWarehouse(LOCA);

            return JsonConvert.SerializeObject("MyObj");
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