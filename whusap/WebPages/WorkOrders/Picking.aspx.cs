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
    public partial class Picking : System.Web.UI.Page
    {

        public static string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        public static string thereisnotPalletavailable = mensajes("thereisnotPalletavailable");
        public static string ThequantityassociatetonewpalletisminortooldpalletID = mensajes("ThequantityassociatetonewpalletisminortooldpalletID");
        public static string ThenewpalletIddoesnthaveItemequaltotheoldpalletIditem = mensajes("ThenewpalletIddoesnthaveItemequaltotheoldpalletIditem");
        public static string ThepalletIDDoesntexistorItsinpickingprocess = mensajes("ThepalletIDDoesntexistorItsinpickingprocess");
        public static string ThePallethasalreadylocate = mensajes("ThePallethasalreadylocate");
        public static string ThePalletIDdoesnotexistorisnotassociatedtoyouruserornothavepalletsinpickingstatus = mensajes("ThePalletIDdoesnotexistorisnotassociatedtoyouruserornothavepalletsinpickingstatus");
        public static string ThePalletIDdoesnotexist = mensajes("ThePalletIDdoesnotexist");
        public static string NotAalletsAvailablethereNotPallets = mensajes("NotAalletsAvailablethereNotPallets");
        public static string errorlog = string.Empty;
        public static string ADVS = string.Empty;
        public object GObject = new object();
        public EventArgs Ge = new EventArgs();
        private static InterfazDAL_twhcol130 _idaltwhcol130 = new InterfazDAL_twhcol130();
        private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
        private static InterfazDAL_tticol042 _idaltticol042 = new InterfazDAL_tticol042();
        public static InterfazDAL_twhcol122 twhcolDAL = new InterfazDAL_twhcol122();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        private static InterfazDAL_tticol125 _idaltticol125 = new InterfazDAL_tticol125();
        string formName = string.Empty;
        //public static string _operator = string.Empty;
        string _idioma = string.Empty;
        private static Mensajes _mensajesForm = new Mensajes();
        private static string globalMessages = "GlobalMessages";
        //public static int flag022, flag042, flag131, flag307;
        DataTable resultado = new DataTable();
        public static string descombo = "";
        public static string qtyaG = "0";

        protected void Page_Load(object sender, EventArgs e)
        {

            HttpContext.Current.Session["CNPK"] = "";
            GObject = sender;
            Ge = e;
            string sentencia = string.Empty;
            //_operator = Session["user"].ToString();
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('"+HttpContext.Current.Session["user"].ToString().Trim()+"')", true);
            string PAID = "";
            string mensaje = Clic_Pick(PAID);
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
            EntidadPicking MyObj = new EntidadPicking();
            Ent_ttccol307 MyObj307 = new Ent_ttccol307();
            MyObj307.PAID = PAID;
            MyObj307.USRR = Session["user"].ToString();
            DataTable DTttccol307 = twhcolDAL.ConsultarTt307140(MyObj307);
            List<EntidadPicking> LstPallet22 = new List<EntidadPicking>();
            List<EntidadPicking> LstPallet042 = new List<EntidadPicking>();
            List<EntidadPicking> LstPallet131 = new List<EntidadPicking>();

            DataTable DT082STAT = twhcolDAL.ConsultarTticol082porStat(Session["user"].ToString().Trim(), 5);
            DataTable DT082STATPENDING = twhcolDAL.ConsultarTticol082porStat(Session["user"].ToString().Trim(), 2);

            if (DT082STAT.Rows.Count > 0)
            {
                limpiarControles();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('User has a picking pending')", true);
                //return;
            }
            if (DT082STATPENDING.Rows.Count > 0 )
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('User has this pallet " + DT082STATPENDING.Rows[0]["T$PAID"].ToString().Trim() + " pending to Drop');clearForm();", true);
                return;
            }

            if (DTttccol307.Rows.Count < 1)
            {

                //LstPallet131 = twhcolDAL.ConsultarPalletPicking131(PAID, string.Empty, _operator);
                LstPallet131 = twhcolDAL.ConsultarPalletPicking131With082(PAID, string.Empty, HttpContext.Current.Session["user"].ToString().Trim());
                if (LstPallet131.Count > 0)
                {
                    MyObj.PALLETID = LstPallet131[0].PALLETID.ToString();
                    Session["originalPallet"] = MyObj.PALLETID;
                    bool res = twhcolDAL.InsertarTccol307140(HttpContext.Current.Session["user"].ToString().Trim(), "1", MyObj.PALLETID.ToString(), "7", "0", "0");
                    if (res == false)
                    {
                        Page_Load(sender, e);
                    }
                }


                //LstPallet042 = twhcolDAL.ConsultarPalletPicking042(PAID, string.Empty, _operator);
                LstPallet042 = twhcolDAL.ConsultarPalletPicking042With082(PAID, string.Empty, HttpContext.Current.Session["user"].ToString().Trim());
                if (LstPallet042.Count > 0)
                {
                    MyObj.PALLETID = LstPallet042[0].PALLETID.ToString();
                    Session["originalPallet"] = MyObj.PALLETID;
                    bool res = twhcolDAL.InsertarTccol307140(HttpContext.Current.Session["user"].ToString().Trim(), "1", MyObj.PALLETID.ToString(), "7", "0", "0");
                    if (res == false)
                    {
                        Page_Load(sender, e);
                    }
                }


                //LstPallet22 = twhcolDAL.ConsultarPalletPicking22(PAID, string.Empty, _operator);
                LstPallet22 = twhcolDAL.ConsultarPalletPicking22With082(PAID, string.Empty, HttpContext.Current.Session["user"].ToString().Trim());
                if (LstPallet22.Count > 0)
                {
                    MyObj.PALLETID = LstPallet22[0].PALLETID.ToString();
                    Session["originalPallet"] = MyObj.PALLETID;
                    bool res = twhcolDAL.InsertarTccol307140(HttpContext.Current.Session["user"].ToString().Trim(), "1", MyObj.PALLETID.ToString(), "7", "0", "0");
                    if (res == false)
                    {
                        Page_Load(sender, e); 
                    }
                }

                if (LstPallet131.Count == 0 && LstPallet042.Count == 0 && LstPallet22.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('" + NotAalletsAvailablethereNotPallets + "')", true);
                }
            }



            Random generator = new Random();
            int t = generator.Next(1, 1000000);
            string maximo = string.Format("{0:0000000000}", t);

            limpiarControles();
            if (DTttccol307.Rows.Count > 0)
            {
                List<EntidadPicking> LstPallet22PAID = twhcolDAL.ConsultarPalletPicking22PAID(DTttccol307.Rows[0]["T$PAID"].ToString().Trim(), string.Empty, HttpContext.Current.Session["user"].ToString().Trim());
                List<EntidadPicking> LstPallet042PAID = twhcolDAL.ConsultarPalletPicking042PAID(DTttccol307.Rows[0]["T$PAID"].ToString().Trim(), string.Empty, HttpContext.Current.Session["user"].ToString().Trim());
                List<EntidadPicking> LstPallet131PAID = twhcolDAL.ConsultarPalletPicking131PAID(DTttccol307.Rows[0]["T$PAID"].ToString().Trim(), string.Empty, HttpContext.Current.Session["user"].ToString().Trim());
                if (LstPallet22PAID.Count > 0)
                {
                    MyObj = LstPallet22PAID[0];
                    HttpContext.Current.Session["CNPK"] = MyObj.CNPK.ToString().Trim();
                    lblCNPK.Text = MyObj.CNPK.ToString().Trim();
                    Session["originalPallet"] = MyObj.PALLETID.ToString();
                    lblPalletID.Text = MyObj.PALLETID.ToString();
                    lblItemID.Text = MyObj.ITEM.ToString();
                    HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                    lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                    LblLotId.Text = MyObj.LOT.ToString();
                    lblWarehouse.Text = MyObj.WRH.ToString();
                    lblWareDescr.Text = MyObj.DESCWRH.ToString();
                    lbllocation.Text = MyObj.LOCA.ToString();
                    lblQuantity.Text = MyObj.QTY.ToString();
                    lblQuantityAux.Text = MyObj.QTY.ToString();
                    lblQuantityOld.Text = MyObj.QTY.ToString();
                    if (MyObj.CNPK.ToString().Trim() == "1") { lblQuantity.Visible = false; } else { lblQuantityAux.Visible = true; };
                    //lblQuantity.Visible = false; 
                    //HttpContext.Current.Session["QTY"] = MyObj.QTYT.ToString();
                    HttpContext.Current.Session["QTY"] = MyObj.QTY.ToString();
                    HttpContext.Current.Session["PRIO"] = MyObj.PRIO.ToString();
                    lblQuantityDesc.Text = MyObj.UN.ToString();
                    lblOORG.Text = MyObj.OORG.ToString();
                    lblORNO.Text = MyObj.ORNO.ToString();
                    //lblOSET.Text = MyObj.OSET.ToString();
                    lblPONO.Text = MyObj.PONO.ToString();
                    lblSQNB.Text = MyObj.SQNB.ToString();
                    lblOORGAUX.Text = MyObj.OORG.ToString();
                    lblORNOAUX.Text = MyObj.ORNO.ToString();
                    lblPONOAUX.Text = MyObj.PONO.ToString();
                    lblSQNBAUX.Text = MyObj.SQNB.ToString();
                    lblADVS.Text = MyObj.ADVS.ToString();// + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                    lblDetail.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                    ADVS = MyObj.ADVS.ToString();
                    HttpContext.Current.Session["flag022"] = 1;
                    HttpContext.Current.Session["flag131"] = 0;
                    HttpContext.Current.Session["flag042"] = 0;
                }
                else if (LstPallet042PAID.Count > 0)
                {
                    MyObj = LstPallet042PAID[0];
                    lblCNPK.Text = MyObj.CNPK.ToString().Trim();
                    HttpContext.Current.Session["CNPK"] = MyObj.CNPK.ToString().Trim();
                    Session["originalPallet"] = MyObj.PALLETID.ToString();
                    lblPalletID.Text = MyObj.PALLETID.ToString();
                    lblItemID.Text = MyObj.ITEM.ToString();
                    HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                    lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                    LblLotId.Text = MyObj.LOT.ToString();
                    lblWarehouse.Text = MyObj.WRH.ToString();
                    lblWareDescr.Text = MyObj.DESCWRH.ToString();
                    lbllocation.Text = MyObj.LOCA.ToString();
                    lblQuantity.Text = MyObj.QTY.ToString();
                    lblQuantityAux.Text = MyObj.QTY.ToString();
                    lblQuantityOld.Text = MyObj.QTY.ToString();
                    if (MyObj.CNPK.ToString().Trim() == "1") { lblQuantity.Visible = false; } else { lblQuantityAux.Visible = true; };
                    //HttpContext.Current.Session["QTY"] = MyObj.QTYT.ToString();
                    HttpContext.Current.Session["QTY"] = MyObj.QTY.ToString();
                    HttpContext.Current.Session["PRIO"] = MyObj.PRIO.ToString();
                    lblQuantityDesc.Text = MyObj.UN.ToString();
                    lblOORG.Text = MyObj.OORG.ToString();
                    lblORNO.Text = MyObj.ORNO.ToString();
                    //lblOSET.Text = MyObj.OSET.ToString();
                    lblPONO.Text = MyObj.PONO.ToString();
                    //lblSQNB.Text = MyObj.SQNB.ToString();
                    lblOORGAUX.Text = MyObj.OORG.ToString();
                    lblORNOAUX.Text = MyObj.ORNO.ToString();
                    lblPONOAUX.Text = MyObj.PONO.ToString();
                    //lblSQNBAUX.Text = MyObj.SQNB.ToString();
                    lblADVS.Text = MyObj.ADVS.ToString();// +" PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                    lblDetail.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                    ADVS = MyObj.ADVS.ToString();
                    HttpContext.Current.Session["flag022"] = 0;
                    HttpContext.Current.Session["flag131"] = 0;
                    HttpContext.Current.Session["flag042"] = 1;
                }
                else if (LstPallet131PAID.Count > 0)
                {
                    MyObj = LstPallet131PAID[0];
                    lblCNPK.Text = MyObj.CNPK.ToString().Trim();
                    HttpContext.Current.Session["CNPK"] = MyObj.CNPK.ToString().Trim();
                    Session["originalPallet"] = MyObj.PALLETID.ToString();
                    lblPalletID.Text = MyObj.PALLETID.ToString();
                    lblItemID.Text = MyObj.ITEM.ToString();
                    HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                    lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                    LblLotId.Text = MyObj.LOT.ToString();
                    lblWarehouse.Text = MyObj.WRH.ToString();
                    lblWareDescr.Text = MyObj.DESCWRH.ToString();
                    lbllocation.Text = MyObj.LOCA.ToString();
                    lblQuantity.Text = MyObj.QTYT.ToString();
                    lblQuantityAux.Text = MyObj.QTY.ToString();
                    lblQuantityOld.Text = MyObj.QTY.ToString();
                    if (MyObj.CNPK.ToString().Trim() == "1") { lblQuantity.Visible = false; } else { lblQuantityAux.Visible = true; };
                    //HttpContext.Current.Session["QTY"] = MyObj.QTY.ToString();
                    HttpContext.Current.Session["QTY"] = MyObj.QTY.ToString();
                    HttpContext.Current.Session["PRIO"] = MyObj.PRIO.ToString();
                    lblQuantityDesc.Text = MyObj.UN.ToString();
                    lblOORG.Text = MyObj.OORG.ToString();
                    lblORNO.Text = MyObj.ORNO.ToString();
                    //lblOSET.Text = MyObj.OSET.ToString();
                    lblPONO.Text = MyObj.PONO.ToString();
                    //lblSQNB.Text = MyObj.SQNB.ToString();
                    lblADVS.Text = MyObj.ADVS.ToString();// +" PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                    lblDetail.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                    ADVS = MyObj.ADVS.ToString();
                    HttpContext.Current.Session["flag022"] = 0;
                    HttpContext.Current.Session["flag131"] = 1;
                    HttpContext.Current.Session["flag042"] = 0;
                }
            }
            else if (LstPallet22.Count > 0)
            {
                MyObj = LstPallet22[0];
                lblCNPK.Text = MyObj.CNPK.ToString().Trim();
                HttpContext.Current.Session["CNPK"] = MyObj.CNPK.ToString().Trim();
                Session["originalPallet"] = MyObj.PALLETID.ToString();
                lblPalletID.Text = MyObj.PALLETID.ToString();
                lblItemID.Text = MyObj.ITEM.ToString();
                HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                LblLotId.Text = MyObj.LOT.ToString();
                lblWarehouse.Text = MyObj.WRH.ToString();
                lblWareDescr.Text = MyObj.DESCWRH.ToString();
                lbllocation.Text = MyObj.LOCA.ToString();
                lblQuantity.Text = MyObj.QTY.ToString();
                lblQuantityAux.Text = MyObj.QTY.ToString();
                lblQuantityOld.Text = MyObj.QTY.ToString();
                if (MyObj.CNPK.ToString().Trim() == "1") { lblQuantity.Visible = false; } else { lblQuantityAux.Visible = true; };
                HttpContext.Current.Session["QTY"] = MyObj.QTY.ToString();
                HttpContext.Current.Session["PRIO"] = MyObj.PRIO.ToString();
                lblQuantityDesc.Text = MyObj.UN.ToString();
                lblOORG.Text = MyObj.OORG.ToString();
                lblORNO.Text = MyObj.ORNO.ToString();
                //lblOSET.Text = MyObj.OSET.ToString();
                lblPONO.Text = MyObj.PONO.ToString();
                lblSQNB.Text = MyObj.SQNB.ToString();
                lblOORGAUX.Text = MyObj.OORG.ToString();
                lblORNOAUX.Text = MyObj.ORNO.ToString();
                lblPONOAUX.Text = MyObj.PONO.ToString();
                //lblSQNBAUX.Text = MyObj.SQNB.ToString();
                lblADVS.Text = MyObj.ADVS.ToString();// +" PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                lblDetail.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                ADVS = MyObj.ADVS.ToString();
                HttpContext.Current.Session["flag022"] = 1;
                HttpContext.Current.Session["flag131"] = 0;
                HttpContext.Current.Session["flag042"] = 0;

                //twhcolDAL.actRegtticol082140(_operator, " ", " ", 5, " ", MyObj.OORG.ToString(), MyObj.ORNO.ToString(), MyObj.OSET.ToString(), MyObj.PONO.ToString(), MyObj.SQNB.ToString(), MyObj.ADVS.ToString());
                twhcolDAL.actRegtticol082140(HttpContext.Current.Session["user"].ToString().Trim(), MyObj.PALLETID.ToString(), MyObj.LOCA.ToString(), 5, " ", MyObj.OORG.ToString(), MyObj.ORNO.ToString(), "", MyObj.PONO.ToString(), MyObj.QTY.ToString(), MyObj.ADVS.ToString(),ref sentencia);
            }
            else if (LstPallet042.Count > 0)
            {
                MyObj = LstPallet042[0];
                lblCNPK.Text = MyObj.CNPK.ToString().Trim();
                HttpContext.Current.Session["CNPK"] = MyObj.CNPK.ToString().Trim();
                Session["originalPallet"] = MyObj.PALLETID.ToString();
                lblPalletID.Text = MyObj.PALLETID.ToString();
                lblItemID.Text = MyObj.ITEM.ToString();
                HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                LblLotId.Text = MyObj.LOT.ToString();
                lblWarehouse.Text = MyObj.WRH.ToString();
                lblWareDescr.Text = MyObj.DESCWRH.ToString();
                lbllocation.Text = MyObj.LOCA.ToString();
                lblQuantity.Text = MyObj.QTY.ToString();
                lblQuantityAux.Text = MyObj.QTY.ToString();
                lblQuantityOld.Text = MyObj.QTY.ToString();
                if (MyObj.CNPK.ToString().Trim() == "1") { lblQuantity.Visible = false; } else { lblQuantityAux.Visible = true; };
                HttpContext.Current.Session["QTY"] = MyObj.QTY.ToString();
                HttpContext.Current.Session["PRIO"] = MyObj.PRIO.ToString();
                lblQuantityDesc.Text = MyObj.UN.ToString();
                lblOORG.Text = MyObj.OORG.ToString();
                lblORNO.Text = MyObj.ORNO.ToString();
                //lblOSET.Text = MyObj.OSET.ToString();
                lblPONO.Text = MyObj.PONO.ToString();
                lblSQNB.Text = MyObj.SQNB.ToString();
                lblOORGAUX.Text = MyObj.OORG.ToString();
                lblORNOAUX.Text = MyObj.ORNO.ToString();
                lblPONOAUX.Text = MyObj.PONO.ToString();
                //lblSQNBAUX.Text = MyObj.SQNB.ToString();
                lblADVS.Text = MyObj.ADVS.ToString();// +" PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                lblDetail.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                ADVS = MyObj.ADVS.ToString();
                HttpContext.Current.Session["flag022"] = 0;
                HttpContext.Current.Session["flag131"] = 0;
                HttpContext.Current.Session["flag042"] = 1;
                //twhcolDAL.actRegtticol082140(_operator, " ", " ", 5, " ", MyObj.OORG.ToString(), MyObj.ORNO.ToString(), MyObj.OSET.ToString(), MyObj.PONO.ToString(), MyObj.SQNB.ToString(), MyObj.ADVS.ToString());
                twhcolDAL.actRegtticol082140(HttpContext.Current.Session["user"].ToString().Trim(), MyObj.PALLETID.ToString(), MyObj.LOCA.ToString(), 5, " ", MyObj.OORG.ToString(), MyObj.ORNO.ToString(), "", MyObj.PONO.ToString(), MyObj.QTY.ToString(), MyObj.ADVS.ToString(),ref sentencia);

                ////                twhcolDAL.actRegtticol082140(_operator, MyObj.PALLETID.ToString(), MyObj.LOCA.ToString(), 5, maximo, MyObj.OORG.ToString(), MyObj.ORNO.ToString(), MyObj.OSET.ToString(), MyObj.PONO.ToString(), MyObj.SQNB.ToString(), MyObj.ADVS.ToString());
                //bool res = twhcolDAL.InsertarTccol307140(_operator, "1", MyObj.PALLETID.ToString(), "7", "0", "0");
                //if (res == false)
                //{
                //    Page_Load(sender, e);
                //}
            }
            else if (LstPallet131.Count > 0)
            {

                MyObj = LstPallet131[0];
                lblCNPK.Text = MyObj.CNPK.ToString().Trim();
                HttpContext.Current.Session["CNPK"] = MyObj.CNPK.ToString().Trim();
                Session["originalPallet"] = MyObj.PALLETID.ToString();
                lblPalletID.Text = MyObj.PALLETID.ToString();
                lblItemID.Text = MyObj.ITEM.ToString();
                HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                LblLotId.Text = MyObj.LOT.ToString();
                lblWarehouse.Text = MyObj.WRH.ToString();
                lblWareDescr.Text = MyObj.DESCWRH.ToString();
                lbllocation.Text = MyObj.LOCA.ToString();
                lblQuantity.Text = MyObj.QTY.ToString();
                lblQuantityAux.Text = MyObj.QTYT.ToString();
                lblQuantityOld.Text = MyObj.QTYT.ToString();
                if (MyObj.CNPK.ToString().Trim() == "1") { lblQuantity.Visible = false; } else { lblQuantityAux.Visible = true; };
                HttpContext.Current.Session["QTY"] = MyObj.QTY.ToString();
                HttpContext.Current.Session["PRIO"] = MyObj.PRIO.ToString();
                lblQuantityDesc.Text = MyObj.UN.ToString();
                lblOORG.Text = MyObj.OORG.ToString();
                lblORNO.Text = MyObj.ORNO.ToString();
                //lblOSET.Text = MyObj.OSET.ToString();
                lblPONO.Text = MyObj.PONO.ToString();
                lblSQNB.Text = MyObj.SQNB.ToString();
                lblOORGAUX.Text = MyObj.OORG.ToString();
                lblORNOAUX.Text = MyObj.ORNO.ToString();
                lblPONOAUX.Text = MyObj.PONO.ToString();
                lblSQNBAUX.Text = MyObj.SQNB.ToString();
                lblADVS.Text = MyObj.ADVS.ToString();// +" PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                lblDetail.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                ADVS = MyObj.ADVS.ToString();
                HttpContext.Current.Session["flag022"] = 0;
                HttpContext.Current.Session["flag131"] = 1;
                HttpContext.Current.Session["flag042"] = 0;
                //twhcolDAL.actRegtticol082140(_operator, " ", " ", 5, " ", MyObj.OORG.ToString(), MyObj.ORNO.ToString(), MyObj.OSET.ToString(), MyObj.PONO.ToString(), MyObj.SQNB.ToString(), MyObj.ADVS.ToString());
                twhcolDAL.actRegtticol082140(HttpContext.Current.Session["user"].ToString().Trim(), MyObj.PALLETID.ToString(), MyObj.LOCA.ToString(), 5, " ", MyObj.OORG.ToString(), MyObj.ORNO.ToString(), "", MyObj.PONO.ToString(), MyObj.QTY.ToString(), MyObj.ADVS.ToString(),ref sentencia);

                ////twhcolDAL.actRegtticol082140(_operator, MyObj.PALLETID.ToString(), MyObj.LOCA.ToString(), 5, maximo, MyObj.OORG.ToString(), MyObj.ORNO.ToString(), MyObj.OSET.ToString(), MyObj.PONO.ToString(), MyObj.SQNB.ToString(), MyObj.ADVS.ToString());
                //bool res = twhcolDAL.InsertarTccol307140(_operator, "1", MyObj.PALLETID.ToString(), "7", "0", "0");
                //if (res == false)
                //{
                //    Page_Load(sender, e);
                //}
            }
            if ((LstPallet22.Count == 0) && (LstPallet042.Count == 0) && (LstPallet131.Count == 0) && (DTttccol307.Rows.Count == 0))
            {
                mensaje = thereisnotPalletavailable;
                //Response.Write("<script language=javascript>alert('" + mensaje + "');window.location = '/WebPages/Login/whMenuI.aspx';</script>");

            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "script","ShowCurrentOptions()", true);
            }

            if (HttpContext.Current.Session["CNPK"].ToString().Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "$('#btnconfirPKG').hide(500)", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('additional data not defined')", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "$('#btnconfirPKG').show(500)", true);
            }

        }

        public void limpiarControles()
        {
            lblPalletID.Text = "";
            lblItemID.Text = "";
            lblItemDesc.Text = "";
            LblLotId.Text = "";
            lblWarehouse.Text = "";
            lblWareDescr.Text = "";
            lbllocation.Text = "";
            lblQuantity.Text = "";
            lblQuantityAux.Text = "";
            lblQuantityOld.Text = "";
            lblQuantityDesc.Text = "";
            lblOORG.Text = "";
            lblORNO.Text = "";
            //lblOSET.Text = "";
            lblPONO.Text = "";
            lblSQNB.Text = "";
            lblADVS.Text = "";
            lblCNPK.Text = "";
            lblOORGAUX.Text = "";
            lblORNOAUX.Text = "";
            lblPONOAUX.Text = "";
            lblSQNBAUX.Text = "";
        }
        [WebMethod]
        public static bool VerificarLocate(string CWAR, string LOCA)
        {
            DataTable DTLote = twhcolDAL.VerificarLocate(CWAR, LOCA);
            if (DTLote.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [WebMethod]
        public static string VerificarPalletID(string PAID_NEW, string PAID_OLD, string selectOptionPallet = "false")
        {



            EntidadPicking ObjPicking = new EntidadPicking();

            DataTable DTPalletID = twhcolDAL.VerificarPalletID(PAID_NEW);
            if (DTPalletID.Rows.Count > 0)
            {


                if (DTPalletID.Rows[0]["ITEM"].ToString().Trim() == HttpContext.Current.Session["ITEM"].ToString().Trim())
                {
                    if (Convert.ToDecimal(DTPalletID.Rows[0]["QTYT"].ToString()) >= Convert.ToDecimal(HttpContext.Current.Session["QTY"].ToString()))
                    {
                        ObjPicking.error = false;
                        ObjPicking.PALLETID = PAID_NEW;
                        ObjPicking.LOT = DTPalletID.Rows[0]["LOT"].ToString();
                        ObjPicking.WRH = DTPalletID.Rows[0]["CWAT"].ToString();
                        ObjPicking.DESCWRH = DTPalletID.Rows[0]["DESCAW"].ToString();
                        ObjPicking.LOCA = DTPalletID.Rows[0]["ACLO"].ToString();
                        ObjPicking.QTY = DTPalletID.Rows[0]["QTYT"].ToString();
                        ObjPicking.UN = DTPalletID.Rows[0]["UNIT"].ToString();
                        ObjPicking.ALLO = DTPalletID.Rows[0]["ALLO"].ToString();
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "$('#LblQuantity').html(" + ObjPicking.QTY + ") ", true);
                        bool ret = false;
                        //ret = twhcolDAL.Actualizar307(PAID_NEW, PAID_OLD);
                        ret = twhcolDAL.Actualizar307Proc(PAID_NEW, PAID_OLD, HttpContext.Current.Session["user"].ToString().Trim());

                        if (ret)
                        {
                            twhcolDAL.actRegtticol082140Paid(HttpContext.Current.Session["user"].ToString().Trim(), PAID_NEW, ObjPicking.LOCA, 5, HttpContext.Current.Session["PRIO"].ToString());
                            Ent_ttccol307 MyObj307 = new Ent_ttccol307();
                            MyObj307.PAID = PAID_NEW;
                            MyObj307.USRR = HttpContext.Current.Session["user"].ToString().Trim();
                            DataTable DTttccol307 = twhcolDAL.ConsultarTt307140(MyObj307);
                            if (DTttccol307.Rows.Count > 0)
                            {
                                string PAID = "";
                                string Tbl = DTPalletID.Rows[0]["TBL"].ToString().Trim();
                                switch (Tbl)
                                {
                                    case "ticol022":
                                        HttpContext.Current.Session["flag022"] = 1;
                                        HttpContext.Current.Session["flag042"] = 0;
                                        HttpContext.Current.Session["flag131"] = 0;
                                        twhcolDAL.ActualizarCantidades222(PAID_OLD);
                                        twhcolDAL.ActualizarCantidades222(PAID_NEW);
                                        break;

                                    case "ticol042":
                                        HttpContext.Current.Session["flag042"] = 1;
                                        HttpContext.Current.Session["flag131"] = 0;
                                        HttpContext.Current.Session["flag022"] = 0;
                                        twhcolDAL.ActualizarCantidades242(PAID_OLD);
                                        twhcolDAL.ActualizarCantidades242(PAID_NEW);
                                        break;

                                    case "whcol131":
                                        HttpContext.Current.Session["flag131"] = 1;
                                        HttpContext.Current.Session["flag022"] = 0;
                                        HttpContext.Current.Session["flag042"] = 0;
                                        twhcolDAL.ActualizarCantidades131(PAID_OLD);
                                        twhcolDAL.ActualizarCantidades131(PAID_NEW, false);
                                        break;
                                }

                            }
                        }
                        if (Convert.ToInt32(HttpContext.Current.Session["flag022"].ToString().Trim()) == 1)
                        {
                            twhcolDAL.ActCausalTICOL022(PAID_OLD, 12);
                            twhcolDAL.ActCausalTICOL022(PAID_NEW, 8);
                        }
                        else if (Convert.ToInt32(HttpContext.Current.Session["flag042"].ToString().Trim()) == 1)
                        {
                            twhcolDAL.ActCausalTICOL042(PAID_OLD, 12);
                            twhcolDAL.ActCausalTICOL042(PAID_NEW, 8);

                        }
                        else if (Convert.ToInt32(HttpContext.Current.Session["flag131"].ToString().Trim()) == 1)
                        {
                            twhcolDAL.ActCausalcol131140(PAID_OLD, 10);
                            twhcolDAL.ActCausalcol131140(PAID_NEW, 6);
                        }

                        if (selectOptionPallet == "true")
                        {
                            if (Convert.ToInt32(HttpContext.Current.Session["flag022"].ToString().Trim()) == 1)
                            {
                                twhcolDAL.ActCausalTICOL022(PAID_OLD, 12);
                                twhcolDAL.ActCausalTICOL022(PAID_NEW, 8);
                            }
                            else if (Convert.ToInt32(HttpContext.Current.Session["flag042"].ToString().Trim()) == 1)
                            {
                                twhcolDAL.ActCausalTICOL042(PAID_OLD, 12);
                                twhcolDAL.ActCausalTICOL042(PAID_NEW, 8);

                            }
                            else if (Convert.ToInt32(HttpContext.Current.Session["flag131"].ToString().Trim()) == 1)
                            {
                                twhcolDAL.ActCausalcol131140(PAID_OLD, 10);
                                twhcolDAL.ActCausalcol131140(PAID_NEW, 6);
                            }
                        }

                        return JsonConvert.SerializeObject(ObjPicking);
                    }
                    else
                    {
                        // cantidadd de pallet new menor a pallet old
                        ObjPicking.error = true;
                        ObjPicking.errorMsg = ThequantityassociatetonewpalletisminortooldpalletID;
                        return JsonConvert.SerializeObject(ObjPicking);
                    }
                }
                else
                {
                    //pallet new con item diferente
                    ObjPicking.error = true;
                    ObjPicking.errorMsg = ThenewpalletIddoesnthaveItemequaltotheoldpalletIditem;
                    return JsonConvert.SerializeObject(ObjPicking);
                }
            }
            else
            {
                // pallet no existe
                ObjPicking.error = true;
                ObjPicking.errorMsg = ThepalletIDDoesntexistorItsinpickingprocess;
                return JsonConvert.SerializeObject(ObjPicking);
            }
        }

        [WebMethod]
        public static string Click_Query(string PAID)
        {
            HttpContext.Current.Session["flag022"] = 0;
            HttpContext.Current.Session["flag042"] = 0;
            HttpContext.Current.Session["flag131"] = 0;

            try
            {
                EntidadPicking MyObj = new EntidadPicking();

                List<EntidadPicking> LstPallet = twhcolDAL.ConsultarPalletPicking22(PAID, string.Empty, HttpContext.Current.Session["user"].ToString().Trim());
                if (LstPallet.Count > 0)
                {
                    MyObj = LstPallet[0];

                    if (string.IsNullOrEmpty(MyObj.PALLETID) || string.IsNullOrEmpty(MyObj.LOCA))
                    {
                        MyObj.error = true;
                        MyObj.errorMsg = ThePallethasalreadylocate;
                    }
                }
                else
                {
                    MyObj.error = true;
                    MyObj.errorMsg = ThePalletIDdoesnotexistorisnotassociatedtoyouruserornothavepalletsinpickingstatus;
                }

                return JsonConvert.SerializeObject(MyObj);

            }
            catch (Exception e)
            {
                return ThePalletIDdoesnotexist;
            }

        }

        [WebMethod]
        //public static bool Click_confirPKG(string PAID_OLD, string PAID, string LOCA, string OORG, string ORNO, string OSET, string PONO, string SQNB)
        public static string Click_confirPKG(string PAID_OLD, string PAID, string LOCA, string OORG, string ORNO, string PONO, string QTYT, string QTYT_OLD, string CUNI, string CWAR, string CLOT,string ADVSP)
        {
            try
            {
                string sentencia = string.Empty;
                string sentencia1 = string.Empty;
                PAID_OLD = HttpContext.Current.Session["originalPallet"].ToString();
                //QTYT_OLD = HttpContext.Current.Session["QTY"].ToString();
                
                decimal qtyt = Convert.ToDecimal(QTYT.ToString().Trim());
                decimal qtyt_old = Convert.ToDecimal(QTYT_OLD.ToString().Trim());
                decimal qtyt_act = qtyt_old - qtyt;
                string qtyt_acts = (qtyt_old - qtyt).ToString();
                string qtytS = Convert.ToDecimal(QTYT.ToString().Trim()).ToString();
                int cnpk = Convert.ToInt32(HttpContext.Current.Session["CNPK"].ToString());
                PAID_OLD = PAID_OLD.Trim();
                PAID = PAID.Trim();
                String pallet = PAID.Trim();
                String Location = LOCA;

                //Generar Ramdom
                Random generator = new Random();
                int t = generator.Next(1, 1000000);
                string maximo = string.Format("{0:0000000000}", t);


                if (Convert.ToInt32(HttpContext.Current.Session["flag022"].ToString().Trim()) == 1)
                {
                    Ent_tticol022 MyObj = new Ent_tticol022();

                    int res = twhcolDAL.actRegtticol082140(HttpContext.Current.Session["user"].ToString().Trim(), pallet.ToUpper(), Location.ToUpper(), 2, maximo, OORG, ORNO, "", PONO, qtytS, ADVSP,ref sentencia);
                    bool res1 = twhcolDAL.EliminarTccol307140(pallet.Trim(), ref sentencia1);
                    MyObj.urpt = " " + HttpContext.Current.Session["user"].ToString().Trim() +" "+res+" "+res1+
                        "\n- User: " + HttpContext.Current.Session["user"].ToString().Trim() + ",\n- Pallet: " + pallet.ToUpper() + "\n- Location: " + Location.ToUpper() + "\n- stat: " + "2" + "\n- maximo: " + maximo + "\n- OORG: " + OORG + "\n- ORNO: " + ORNO + "\n- PONO: " + PONO + "\n- qtytS: " + qtytS + "\n- ADVSP: " + ADVSP+
                        "\n"+sentencia+"\n"+
                        "\n"+sentencia1;                 

                    if (cnpk != 1)
                    {
                        twhcolDAL.updatetticol222Quantity(pallet, qtyt_act, qtyt_old);
                        DataTable DTPallet = _idaltwhcol130.VerificarPalletID(ref PAID);
                        qtyaG = DTPallet.Rows[0]["QTYT"].ToString();
                        MyObj.qtyaG = Convert.ToDecimal(qtyaG);
                        _idaltticol125.updataPalletStatus022(PAID, qtyaG == "0" ? "9" : "7");
                        //if (qtyt_act==0)
                        //{
                        //    return true;
                        //}


                        string strError = string.Empty;
                        string SecuenciaPallet = "C001";
                        int consecutivo = 0;
                        if (Convert.ToDecimal(qtyaG) > 0)
                        {

                            string id = ORNO;
                            DataTable Dtticol022 = _idaltticol022.SecuenciaMayor(id);
                            if (Dtticol022.Rows.Count > 0)
                            {

                                if (Dtticol022.Rows[0]["T$SQNB"].ToString().Trim() != "")
                                {
                                    consecutivo = Convert.ToInt32(Dtticol022.Rows[0]["T$SQNB"].ToString().Trim().Substring(11, 3)) + 1;
                                }
                                else
                                {
                                    consecutivo = 1;
                                }

                            }
                            else
                            {
                                consecutivo = 0;
                            }

                            if (consecutivo.ToString().Length == 1)
                            {
                                SecuenciaPallet = "C00" + consecutivo.ToString();
                            }
                            if (consecutivo.ToString().Length == 2)
                            {
                                SecuenciaPallet = "C0" + consecutivo.ToString();
                            }
                            if (consecutivo.ToString().Length == 3)
                            {
                                SecuenciaPallet = "C0" + consecutivo.ToString();
                            }


                            MyObj.pdno = ORNO;
                            MyObj.sqnb = ORNO + "-" + SecuenciaPallet;
                            MyObj.proc = 2;
                            MyObj.logn = HttpContext.Current.Session["user"].ToString().Trim();
                            MyObj.mitm = "         " + HttpContext.Current.Session["ITEM"].ToString().Trim();
                            MyObj.qtdl = Convert.ToDecimal(qtyt.ToString());
                            MyObj.cuni = CUNI;//CUNI;
                            MyObj.log1 = "NONE";
                            MyObj.qtd1 = Convert.ToInt32(qtyt.ToString());
                            MyObj.pro1 = 2;
                            MyObj.log2 = "NONE";
                            MyObj.qtd2 = Convert.ToInt32(qtyt.ToString());
                            MyObj.pro2 = 2;
                            MyObj.loca = LOCA;
                            MyObj.norp = 1;
                            MyObj.dele = 9;
                            MyObj.logd = "NONE";
                            MyObj.refcntd = 0;
                            MyObj.refcntu = 0;
                            MyObj.drpt = DateTime.Now;
                            MyObj.urpt = HttpContext.Current.Session["user"].ToString().Trim();
                            MyObj.acqt = Convert.ToDecimal(qtyt.ToString());
                            MyObj.cwaf = CWAR;//CWAR;
                            MyObj.cwat = CWAR;//CWAR;
                            MyObj.aclo = LOCA;
                            MyObj.allo = 0;

                            var validateSave = _idaltticol022.insertarRegistroSimple(ref MyObj, ref strError);
                            var validateSaveTicol222 = _idaltticol022.InsertarRegistroTicol222(ref MyObj, ref strError);

                            if (validateSave > 0 && Convert.ToDecimal(qtyaG) != 0)
                            {
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
                    }
                    else
                    {
                        _idaltticol125.updataPalletStatus022(PAID, "9");
                        twhcolDAL.updatetticol222Quantity(pallet, 0, qtyt_old);
                    }
                    return JsonConvert.SerializeObject(MyObj);

                }
                else if (Convert.ToInt32(HttpContext.Current.Session["flag042"].ToString().Trim()) == 1)
                {
                    Ent_tticol042 MyObj = new Ent_tticol042();
                    int res = twhcolDAL.actRegtticol082140(HttpContext.Current.Session["user"].ToString().Trim(), pallet.ToUpper(), Location.ToUpper(), 2, maximo, OORG, ORNO, "", PONO, qtytS, ADVSP,ref sentencia);
                    bool res1 = twhcolDAL.EliminarTccol307140(pallet.Trim(),ref sentencia1);
                    MyObj.urpt = " " + HttpContext.Current.Session["user"].ToString().Trim() + " " + res + " " + res1 +
                        "\n- User: " + HttpContext.Current.Session["user"].ToString().Trim() + ",\n- Pallet: " + pallet.ToUpper() + "\n- Location: " + Location.ToUpper() + "\n- stat: " + "2" + "\n- maximo: " + maximo + "\n- OORG: " + OORG + "\n- ORNO: " + ORNO + "\n- PONO: " + PONO + "\n- qtytS: " + qtytS + "\n- ADVSP: " + ADVSP +
                        "\n" + sentencia +"\n"+
                        "\n" + sentencia1; 
                    if (cnpk != 1)
                    {
                        twhcolDAL.updatetticol242Quantity(pallet, qtyt_act, qtyt_old);
                        DataTable DTPallet = _idaltwhcol130.VerificarPalletID(ref PAID);
                        qtyaG = DTPallet.Rows[0]["QTYT"].ToString();
                        MyObj.qtyaG = Convert.ToDecimal(qtyaG);
                        _idaltticol125.updataPalletStatus042(PAID, qtyaG == "0" ? "9" : "7");
                        string strError = string.Empty;
                        string SecuenciaPallet = "C001";
                        int consecutivo = 0;
                        //if (qtyt_act == 0)
                        //{
                        //    return true;
                        //}
                        if (Convert.ToDecimal(qtyaG) > 0)
                        {
                            string id = ORNO;
                            DataTable Dtticol042 = _idaltticol042.SecuenciaMayor(id);
                            if (Dtticol042.Rows.Count > 0)
                            {
                                if (Dtticol042.Rows[0]["T$SQNB"].ToString().Trim() != "")
                                {
                                    consecutivo = Convert.ToInt32(Dtticol042.Rows[0]["T$SQNB"].ToString().Trim().Substring(11, 3)) + 1;
                                }
                                else
                                {
                                    consecutivo = 1;
                                }
                            }
                            else
                            {
                                consecutivo = 1;
                            }

                            if (consecutivo.ToString().Length == 1)
                            {
                                SecuenciaPallet = "C00" + consecutivo.ToString();
                            }
                            if (consecutivo.ToString().Length == 2)
                            {
                                SecuenciaPallet = "C0" + consecutivo.ToString();
                            }
                            if (consecutivo.ToString().Length == 3)
                            {
                                SecuenciaPallet = "C0" + consecutivo.ToString();
                            }


                            MyObj.pdno = ORNO;
                            MyObj.sqnb = ORNO + "-" + SecuenciaPallet;
                            MyObj.proc = 2;
                            MyObj.logn = HttpContext.Current.Session["user"].ToString().Trim();
                            MyObj.mitm = "         " + HttpContext.Current.Session["ITEM"].ToString().Trim();
                            MyObj.qtdl = Convert.ToDecimal(qtyt.ToString());
                            MyObj.cuni = CUNI;//CUNI;
                            MyObj.log1 = "NONE";
                            MyObj.qtd1 = Convert.ToDecimal(qtyt.ToString());
                            MyObj.pro1 = 2;
                            MyObj.log2 = "NONE";
                            MyObj.qtd2 = Convert.ToDecimal(qtyt.ToString());
                            MyObj.pro2 = 2;
                            MyObj.loca = LOCA;
                            MyObj.norp = 1;
                            MyObj.dele = 9;
                            MyObj.logd = "NONE";
                            MyObj.refcntd = 0;
                            MyObj.refcntu = 0;
                            MyObj.drpt = DateTime.Now;
                            MyObj.urpt = HttpContext.Current.Session["user"].ToString().Trim();
                            MyObj.acqt = Convert.ToDouble(qtyt_act.ToString());
                            MyObj.cwaf = CWAR;//CWAR;
                            MyObj.cwat = CWAR;//CWAR;
                            MyObj.aclo = LOCA;
                            MyObj.allo = 0;


                            var validateSave = _idaltticol042.insertarRegistroSimple(ref MyObj, ref strError);
                            var validateSaveTicol242 = _idaltticol042.InsertarRegistroTicol242(ref MyObj, ref strError);

                            if (validateSave > 0 && Convert.ToDecimal(qtyaG) != 0)
                            {
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
                    }
                    else
                    {
                        _idaltticol125.updataPalletStatus042(PAID, "9");
                        twhcolDAL.updatetticol242Quantity(pallet, 0, qtyt_old);
                    }

                    return JsonConvert.SerializeObject(MyObj);


                }

                else if (Convert.ToInt32(HttpContext.Current.Session["flag131"].ToString().Trim()) == 1)
                {
                    errorlog = "-Entro en 131\n";
                    int res = twhcolDAL.actRegtticol082140(HttpContext.Current.Session["user"].ToString().Trim(), pallet.ToUpper(), Location.ToUpper(), 2, maximo, OORG, ORNO, "", PONO, qtytS, ADVSP,ref sentencia);
                    Ent_twhcol130131 MyObj = new Ent_twhcol130131();
                    bool res1  = twhcolDAL.EliminarTccol307140(pallet.Trim(),ref sentencia1);
                    MyObj.urpt = " " + HttpContext.Current.Session["user"].ToString().Trim() + " " + res + " " + res1;
                    MyObj.urpt = " " + HttpContext.Current.Session["user"].ToString().Trim() + " " + res + " " + res1 +
                        "\n- User: " + HttpContext.Current.Session["user"].ToString().Trim() + ",\n- Pallet: " + pallet.ToUpper() + "\n- Location: " + Location.ToUpper() + "\n- stat: " + "2" + "\n- maximo: " + maximo + "\n- OORG: " + OORG + "\n- ORNO: " + ORNO + "\n- PONO: " + PONO + "\n- qtytS: " + qtytS + "\n- ADVSP: " + ADVSP +
                        "\n" + sentencia +"\n"+
                        "\n" + sentencia1;
                    if (cnpk != 1)
                    {

                        twhcolDAL.updatetwhcol131Quantity(pallet, qtyt, qtyt);
                        DataTable DTPallet = _idaltwhcol130.VerificarPalletID(ref PAID);
                        qtyaG = DTPallet.Rows[0]["QTYT"].ToString();
                        MyObj.qtyaG = Convert.ToDecimal(qtyaG);
                        _idaltticol125.updataPalletStatus131(PAID, qtyaG == "0" ? "7" : "3");
                        //if (qtyt_act == 0)
                        //{
                        //    return true;
                        //}
                        if (Convert.ToDecimal(qtyaG) > 0)
                        {
                            int consecutivoPalletID = 0;
                            DataTable DTPalletContinue = twhcol130DAL.PaidMayorwhcol130(ORNO, true);
                            errorlog += "-Modifico trablas\n";
                            string SecuenciaPallet = "001";
                            errorlog += "-Realiza conteo de busqueda pallet max: " + DTPalletContinue.Rows.Count + "\n";
                            if (DTPalletContinue.Rows.Count > 0)
                            {
                                errorlog += "-Verifica si la catidad max es mato a 0\n";
                                foreach (DataRow item in DTPalletContinue.Rows)
                                {
                                    errorlog += "-Entro a for each\n";
                                    errorlog += "la cadena a recortar es esta: " + item["T$PAID"].ToString().Trim() + " \n";
                                    errorlog += "la cadena extraida es : " + item["T$PAID"].ToString().Trim().Substring(10, 3) + " \n";

                                    consecutivoPalletID = Convert.ToInt32(item["T$PAID"].ToString().Trim().Substring(11, 3)) + 1;
                                    errorlog += "realiza substring de pallet para tener la secuencia:" + consecutivoPalletID + "\n";
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
                            errorlog += "genero secuencia,";

                            MyObj.OORG = "2";// Order type escaneada view 
                            MyObj.ORNO = ORNO;
                            MyObj.ITEM = "         " + HttpContext.Current.Session["ITEM"].ToString().Trim();
                            MyObj.PAID = ORNO + "-P" + SecuenciaPallet;
                            MyObj.PONO = "1";
                            MyObj.SEQN = "1";
                            MyObj.CLOT = CLOT;//CLOT.ToUpper();// lote VIEW
                            MyObj.CWAR = CWAR;//CWAR.ToUpper();
                            MyObj.QTYS = qtyt.ToString();//QTYS;// cantidad escaneada view 
                            MyObj.UNIT = CUNI;//UNIT;//unit escaneada view
                            MyObj.QTYC = qtyt.ToString();//QTYS;//cantidad escaneada view aplicando factor
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
                            MyObj.STAT = "7";// LLENAR EN 3 
                            MyObj.DSCA = " ";
                            MyObj.COTP = " ";
                            MyObj.FIRE = "2";
                            MyObj.PSLIP = " ";
                            MyObj.ALLO = "0";

                            bool Insertsucces = twhcol130DAL.Insertartwhcol131(MyObj);

                            if (Insertsucces && qtyt_act != 0)
                            {
                                MyObj.PAID_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.PAID + "&code=Code128&dpi=96";
                                MyObj.PAID_OLD_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + PAID + "&code=Code128&dpi=96";
                                MyObj.ORNO_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.ORNO + "&code=Code128&dpi=96";
                                MyObj.ITEM_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.ITEM + "&code=Code128&dpi=96";
                                MyObj.CLOT_URL = CLOT.ToString().Trim() != "" ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + CLOT + "&code=Code128&dpi=96" : "";
                                MyObj.QTYC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + qtyaG + "&code=Code128&dpi=96";
                                MyObj.QTYC1_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.QTYS + "&code=Code128&dpi=96";
                                MyObj.UNIC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyObj.UNIT + "&code=Code128&dpi=96";
                            }
                        }
                    }
                    else
                    {
                        twhcolDAL.updatetwhcol131Quantity(pallet, 0, qtyt_old);
                        _idaltticol125.updataPalletStatus131(PAID, "7");
                    }
                    return JsonConvert.SerializeObject(MyObj);
                }
                else
                {
                    Ent_twhcol130131 MyErrorObj = new Ent_twhcol130131();
                    MyErrorObj.Error = true;
                    return JsonConvert.SerializeObject(MyErrorObj); ;
                }


            }
            catch (Exception e)
            {
                Ent_twhcol130131 MyErrorObj = new Ent_twhcol130131();
                MyErrorObj.Error = true;
                MyErrorObj.errorMsg = errorlog + e.Message;
                return JsonConvert.SerializeObject(MyErrorObj); ;
            }
        }

        [WebMethod]
        //public static bool Click_confirPKG(string PAID_OLD, string PAID, string LOCA, string OORG, string ORNO, string OSET, string PONO, string SQNB)
        public static string ShowCurrentOptions()
        {

            string ITEM = HttpContext.Current.Session["ITEM"].ToString().Trim();
            string QTY = HttpContext.Current.Session["QTY"].ToString().Trim();
            string PRIO = HttpContext.Current.Session["PRIO"].ToString().Trim();

            //string ITEM = "";
            //string QTY  = "";
            //string PRIO = "";

            List<EntidadPicking> LstPallet22 = new List<EntidadPicking>();
            List<EntidadPicking> LstPallet042 = new List<EntidadPicking>();
            List<EntidadPicking> LstPallet131 = new List<EntidadPicking>();
            List<EntidadPicking> LstReturn = new List<EntidadPicking>();
            LstPallet131 = twhcolDAL.ConsultarPalletPicking131ItemQty(ITEM, QTY, PRIO, HttpContext.Current.Session["user"].ToString().Trim());
            if (LstPallet131.Count > 0)
            {
                LstReturn = LstPallet131;
            }

            LstPallet042 = twhcolDAL.ConsultarPalletPicking042ItemQty(ITEM, QTY, PRIO, HttpContext.Current.Session["user"].ToString().Trim());
            if (LstPallet042.Count > 0)
            {
                LstReturn = LstPallet042;
            }

            LstPallet22 = twhcolDAL.ConsultarPalletPicking22ItemQty(ITEM, QTY, PRIO, HttpContext.Current.Session["user"].ToString().Trim());
            if (LstPallet22.Count > 0)
            {
                LstReturn = LstPallet22;
            }
            return JsonConvert.SerializeObject(LstReturn);
        }

        [WebMethod]
        //public static bool Click_confirCausal(string PAID, string Causal, string txtPallet, string LOCA, string OORG, string ORNO, string OSET, string PONO, string SQNB, string ADVS)
        public static bool Click_confirCausal(string PAID, string Causal, string txtPallet, string LOCA, string OORG, string ORNO, string PONO, string SQNB, string ADVS)
        {
            String pallet = PAID;
            String Location = LOCA.ToUpper();
            LOCA = LOCA.ToUpper();
            int stat = 0;
            int statCausal = 0;
            string de = Causal;

            if (de == "1")
            {

                statCausal = 1;
            }
            else if (de == "2")
            {
                statCausal = 2;
            }
            else if (de == "3")
            {

                statCausal = 3;
            }
            try
            {
                Random generator = new Random();
                int t = generator.Next(1, 1000000);
                string maximo = string.Format("{0:0000000000}", t);

                if (Convert.ToInt32(HttpContext.Current.Session["flag022"].ToString().Trim()) == 1)
                {
                    stat = 12;
                    twhcolDAL.ingRegTticol092140(maximo, HttpContext.Current.Session["originalPallet"].ToString().Trim(), txtPallet, statCausal, HttpContext.Current.Session["user"].ToString().Trim());
                    //twhcolDAL.actRegtticol082140(HttpContext.Current.Session["user"].ToString().Trim(), pallet, Location, 2, maximo, OORG, ORNO, OSET, PONO, SQNB, ADVS);
                    twhcolDAL.InsertRegCausalCOL084(pallet, HttpContext.Current.Session["user"].ToString().Trim(), statCausal);
                    //if (HttpContext.Current.Session["originalPallet"].ToString() != PAID.ToString())
                    //{
                    //    twhcolDAL.ActCausalTICOL022(pallet, stat);
                    //}
                    return true;

                }
                else if (Convert.ToInt32(HttpContext.Current.Session["flag042"].ToString().Trim()) == 1)
                {

                    stat = 12;
                    twhcolDAL.ingRegTticol092140(maximo, HttpContext.Current.Session["originalPallet"].ToString().Trim(), txtPallet, statCausal, HttpContext.Current.Session["user"].ToString().Trim());
                    //twhcolDAL.actRegtticol082140(HttpContext.Current.Session["user"].ToString().Trim(), pallet, Location, 2, maximo, OORG, ORNO, OSET, PONO, SQNB, ADVS);
                    twhcolDAL.InsertRegCausalCOL084(pallet, HttpContext.Current.Session["user"].ToString().Trim(), statCausal);
                    //if (HttpContext.Current.Session["originalPallet"].ToString() != PAID.ToString())
                    //{
                    //    twhcolDAL.ActCausalTICOL042(pallet, stat);
                    //}

                    return true;

                }

                else if (Convert.ToInt32(HttpContext.Current.Session["flag131"].ToString().Trim()) == 1)
                {
                    stat = 10;
                    twhcolDAL.ingRegTticol092140(maximo, HttpContext.Current.Session["originalPallet"].ToString().Trim(), txtPallet, statCausal, HttpContext.Current.Session["user"].ToString().Trim());
                    //twhcolDAL.actRegtticol082140(HttpContext.Current.Session["user"].ToString().Trim(), pallet, Location, 2, maximo, OORG, ORNO, OSET, PONO, SQNB, ADVS);
                    twhcolDAL.InsertRegCausalCOL084(pallet, HttpContext.Current.Session["user"].ToString().Trim(), statCausal);
                    //if (HttpContext.Current.Session["originalPallet"].ToString() != PAID.ToString())
                    //{
                    //    twhcolDAL.ActCausalcol131140(pallet, stat);
                    //}
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception e)
            {
                return false;
            }
        }

        public class Error
        {
            public string MyError { get; set; }
        };

        [WebMethod]
        public string Clic_Pick(string PAID)
        {

            try
            {
                string retorno = string.Empty;
                string USRR = HttpContext.Current.Session["user"].ToString().Trim();
                DataTable DTtccol307PalletID = twhcol130DAL.Consultarttccol307(PAID, string.Empty);


                if (DTtccol307PalletID.Rows.Count > 0)
                {
                    retorno = "The ID palled: " + DTtccol307PalletID.Rows[0]["PAID"].ToString().Trim() + " pending to locate by user: " + DTtccol307PalletID.Rows[0]["USRR"].ToString().Trim();
                }

                return retorno;

            }
            catch (Exception e)
            {
                return e.ToString();
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

        protected void Reload_Click(object sender, EventArgs e)
        {
            //HttpContext.Current.Session["flag022"] = 0;
            //HttpContext.Current.Session["flag042"] = 0;
            //HttpContext.Current.Session["flag131"] = 0;
            string PAID = "";
            string mensaje = "";
            //Page_Load(GObject, Ge);

            //Page_Load(sender, e);

        }

        [WebMethod]
        public static string getusers()
        {
            return "_operator:" + HttpContext.Current.Session["user"].ToString().Trim() + "  Session['user']:" + HttpContext.Current.Session["user"].ToString() + " ,flag022: " + HttpContext.Current.Session["flag022"].ToString() + " ,flag042: " + HttpContext.Current.Session["flag042"] + " ,flag113: " + HttpContext.Current.Session["flag131"];
                                                                                                                                                                  
        }                                                                                                                                                         

        protected void Button1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('_operator:" + HttpContext.Current.Session["user"].ToString().Trim() + "  Session['user']:" + Session["user"].ToString() + "')", true);

        }

    }
}