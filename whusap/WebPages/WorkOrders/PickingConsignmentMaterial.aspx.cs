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
    public partial class PickingConsignmentMaterial : System.Web.UI.Page
    {
        private static Seguimiento log = new Seguimiento();
        public static string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        public static string thereisnotPalletavailable = mensajes("thereisnotPalletavailable");
        public static string ThequantityassociatetonewpalletisminortooldpalletID = mensajes("ThequantityassociatetonewpalletisminortooldpalletID");
        public static string ThenewpalletIddoesnthaveItemequaltotheoldpalletIditem = mensajes("ThenewpalletIddoesnthaveItemequaltotheoldpalletIditem");
        public static string ThepalletIDDoesntexistorItsinpickingprocess = mensajes("ThepalletIDDoesntexistorItsinpickingprocess");
        public static string ThepalletIDNotContaenthesameItem = mensajes("ThepalletIDNotContaenthesameItem");
           public static string ThePalletNotAllowedHavingTheSameWarehouse = mensajes("ThePalletNotAllowedHavingTheSameWarehouse");
        public static string ThePallethasalreadylocate = mensajes("ThePallethasalreadylocate");
        public static string ThePalletIDdoesnotexistorisnotassociatedtoyouruserornothavepalletsinpickingstatus = mensajes("ThePalletIDdoesnotexistorisnotassociatedtoyouruserornothavepalletsinpickingstatus");
        public static string ThePalletIDdoesnotexist = mensajes("ThePalletIDdoesnotexist");
        public static string NotAalletsAvailablethereNotPallets = mensajes("NotAalletsAvailablethereNotPallets");
        public static string machinesPicking = ConfigurationManager.AppSettings["MachinesPicking"];
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
        private static IntefazDAL_ttccol307 _idaltccol307 = new IntefazDAL_ttccol307();
        public static InterfazDAL_ttdcol137 ITticol137 = new InterfazDAL_ttdcol137();
        public EventArgs Ge = new EventArgs();
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
        public static EntidadPicking loadPage(string CWAR = "")
        {
            bool PrioIs131 = false;
            bool PrioIs022 = false;
            bool PrioIs042 = false;
            int currentPrio = int.MinValue;
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
            MySessionObjPicking.WRH = CWAR;
            HttpContext.Current.Session["MyObjPicking"] = MySessionObjPicking;

            string sentencia = string.Empty;
            EntidadPicking MyObj = new EntidadPicking();

            Ent_ttccol307 MyObj307 = new Ent_ttccol307();
            MyObj307.CWAR = CWAR;
            MyObj307.USRR = string.Empty;
            List<EntidadPicking> LstPallet22 = new List<EntidadPicking>();
            List<EntidadPicking> LstPallet042 = new List<EntidadPicking>();
            List<EntidadPicking> LstPallet131 = new List<EntidadPicking>();
            List<EntidadPicking> LstPallet22PAID = new List<EntidadPicking>();
            List<EntidadPicking> LstPallet042PAID = new List<EntidadPicking>();
            List<EntidadPicking> LstPallet131PAID = new List<EntidadPicking>();


            LstPallet131 = twhcolDAL.ConsultarPalletPicking131With082(CWAR, string.Empty, HttpContext.Current.Session["user"].ToString().Trim());
            LstPallet042 = twhcolDAL.ConsultarPalletPicking042With082(CWAR, string.Empty, HttpContext.Current.Session["user"].ToString().Trim());
            LstPallet22 = twhcolDAL.ConsultarPalletPicking22With082(CWAR, string.Empty, HttpContext.Current.Session["user"].ToString().Trim());

            if (LstPallet131.Count == 0 && LstPallet042.Count == 0 && LstPallet22.Count == 0)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('" + NotAalletsAvailablethereNotPallets + "')", true);
            }
            else
            {

                List<int> prios = new List<int>();
                int prio131 = LstPallet131.Count > 0 ? Convert.ToInt32(LstPallet131[0].PRIO.ToString()) : int.MaxValue;
                int prio022 = LstPallet22.Count > 0 ? Convert.ToInt32(LstPallet22[0].PRIO.ToString()) : int.MaxValue;
                int prio042 = LstPallet042.Count > 0 ? Convert.ToInt32(LstPallet042[0].PRIO.ToString()) : int.MaxValue;

                if (prio131 != int.MinValue || prio022 != int.MinValue || prio042 != int.MinValue)
                {
                    prios.Add(prio131);
                    prios.Add(prio022);
                    prios.Add(prio042);
                    prios.Sort();
                    currentPrio = prios[0];
                }

                if (currentPrio != int.MinValue)
                {
                    PrioIs131 = prio131 == currentPrio ? true : false;
                    PrioIs022 = prio022 == currentPrio ? true : false;
                    PrioIs042 = prio042 == currentPrio ? true : false;
                }
            }

            if (LstPallet131.Count > 0 && PrioIs131 == true)
            {
                MyObj.PALLETID = LstPallet131[0].PALLETID.ToString();
                EntidadPicking MyObjLst131 = LstPallet131[0];

                bool mcnopick = machinesPicking.Contains(LstPallet131[0].MCNO.Trim()) == true ? MyObj.MCNOPICK = true : MyObj.MCNOPICK = false;

                if (MyObjLst131.STAT.Trim() == "6" || MyObjLst131.STAT.Trim() == "3")
                {
                    bool res = twhcolDAL.InsertarTccol307140(HttpContext.Current.Session["user"].ToString().Trim(), "1", MyObjLst131.PICK.ToString(), CWAR, "7", "0", "0");
                }
                else
                {
                    if (MyObjLst131.STAT.Trim() == "10")
                    {
                        Ent_tticol082 myObj082 = new Ent_tticol082();
                        myObj082.STAT = "3";
                        myObj082.PRIO = LstPallet131[0].PRIO;
                        _idaltwhcol122.UpdateTtico082(myObj082);
                        loadPage();
                        return MyObj;
                    }
                }
            }

            if (LstPallet042.Count > 0 && PrioIs042 == true)
            {
                MyObj.PALLETID = LstPallet042[0].PALLETID.ToString();
                EntidadPicking MyObjLst042 = LstPallet042[0];

                bool mcnopick = machinesPicking.Contains(MyObjLst042.MCNO.Trim()) == true ? MyObj.MCNOPICK = true : MyObj.MCNOPICK = false;

                if (MyObjLst042.STAT.Trim() == "7" || MyObjLst042.STAT.Trim() == "8")
                {

                    bool res = twhcolDAL.InsertarTccol307140(HttpContext.Current.Session["user"].ToString().Trim(), "1", MyObjLst042.PICK.ToString(), CWAR, "7", "0", "0");
                }
                else
                {
                    if (MyObjLst042.STAT.Trim() == "12")
                    {
                        Ent_tticol082 myObj082 = new Ent_tticol082();
                        myObj082.STAT = "3";
                        myObj082.PRIO = LstPallet042[0].PRIO;
                        _idaltwhcol122.UpdateTtico082(myObj082);
                        loadPage();
                        return MyObj;
                    }
                }
            }

            if (LstPallet22.Count > 0 && PrioIs022 == true)
            {
                MyObj.PALLETID = LstPallet22[0].PALLETID.ToString();
                EntidadPicking MyObjLst022 = LstPallet22[0];

                bool mcnopick = machinesPicking.Contains(MyObjLst022.MCNO.Trim()) == true ? MyObj.MCNOPICK = true : MyObj.MCNOPICK = false;

                if (MyObjLst022.STAT.Trim() == "7" || MyObjLst022.STAT.Trim() == "8")
                {
                    bool res = twhcolDAL.InsertarTccol307140(HttpContext.Current.Session["user"].ToString().Trim(), "1", MyObjLst022.PICK.ToString(), CWAR, "7", "0", "0");
                }
                else
                {
                    if (MyObjLst022.STAT.Trim() == "12")
                    {
                        Ent_tticol082 myObj082 = new Ent_tticol082();
                        myObj082.STAT = "3";
                        myObj082.PRIO = LstPallet22[0].PRIO;
                        _idaltwhcol122.UpdateTtico082(myObj082);
                        loadPage();
                        return MyObj;
                    }
                }
            }



            if (LstPallet22.Count > 0 && PrioIs022 == true)
            {
                MyObj = LstPallet22[0];
                bool mcnopick = machinesPicking.Contains(MyObj.MCNO.Trim()) == true ? MyObj.MCNOPICK = true : MyObj.MCNOPICK = false;
                HttpContext.Current.Session["MyObjPicking"] = MyObj;
                HttpContext.Current.Session["flag022"] = 0;
                HttpContext.Current.Session["flag131"] = 0;
                HttpContext.Current.Session["flag042"] = 1;

                Ent_ttccol307 tccol307 = new Ent_ttccol307();
                tccol307.PAID = MyObj.PICK;
                tccol307.CWAR = MyObj.WRH;
                HttpContext.Current.Session["PICKUSING"] = MyObj.PICK;
                HttpContext.Current.Session["CWARUSING"] = MyObj.WRH;
                tccol307.STAT_AUX = "2";
                _idaltccol307.ActualizarTccol307(tccol307);

            }
            else if (LstPallet042.Count > 0 && PrioIs042 == true)
            {
                MyObj = LstPallet042[0];
                bool mcnopick = machinesPicking.Contains(MyObj.MCNO.Trim()) == true ? MyObj.MCNOPICK = true : MyObj.MCNOPICK = false;                
                ADVS = MyObj.ADVS.ToString();
                HttpContext.Current.Session["MyObjPicking"] = MyObj;
                HttpContext.Current.Session["flag022"] = 0;
                HttpContext.Current.Session["flag131"] = 0;
                HttpContext.Current.Session["flag042"] = 1;

                Ent_ttccol307 tccol307 = new Ent_ttccol307();
                tccol307.PAID = MyObj.PICK;
                tccol307.CWAR = MyObj.WRH;
                HttpContext.Current.Session["PICKUSING"] = MyObj.PICK;
                HttpContext.Current.Session["CWARUSING"] = MyObj.WRH;
                tccol307.STAT_AUX = "2";
                _idaltccol307.ActualizarTccol307(tccol307);
            }
            else if (LstPallet131.Count > 0 && PrioIs131 == true)
            {

                MyObj = LstPallet131[0];
                bool mcnopick = machinesPicking.Contains(MyObj.MCNO.Trim()) == true ? MyObj.MCNOPICK = true : MyObj.MCNOPICK = false;
                HttpContext.Current.Session["MyObjPicking"] = MyObj;
                HttpContext.Current.Session["flag022"] = 0;
                HttpContext.Current.Session["flag131"] = 1;
                HttpContext.Current.Session["flag042"] = 0;

                Ent_ttccol307 tccol307 = new Ent_ttccol307();
                tccol307.PAID = MyObj.PICK;
                tccol307.CWAR = MyObj.WRH;
                HttpContext.Current.Session["PICKUSING"] = MyObj.PICK;
                HttpContext.Current.Session["CWARUSING"] = MyObj.WRH;
                tccol307.STAT_AUX = "2";
                _idaltccol307.ActualizarTccol307(tccol307);
            }

            if ((LstPallet22.Count == 0) && (LstPallet042.Count == 0) && (LstPallet131.Count == 0))
            {
                MyObj.error = true;
                MyObj.errorMsg = thereisnotPalletavailable;
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "script","ShowCurrentOptions()", true);
            }

            return MyObj;
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
        public static string loadPicksPending(string CWAR)
        {
            Ent_ttccol307 MyObj307 = new Ent_ttccol307();
            MyObj307.PAID = String.Empty;
            MyObj307.CWAR = CWAR;
            MyObj307.USRR = string.Empty;
            if (HttpContext.Current.Session["PICKUSING"] != null && HttpContext.Current.Session["CWARUSING"] != null)
            {
                Ent_ttccol307 tccol307 = new Ent_ttccol307();
                tccol307.PAID = HttpContext.Current.Session["PICKUSING"].ToString();
                tccol307.CWAR = HttpContext.Current.Session["CWARUSING"].ToString();
                tccol307.STAT_AUX = "1";
                _idaltccol307.ActualizarTccol307(tccol307);

            }
            DataTable DTttccol307 = _idaltccol307.ConsultarPendientesTccol307("'1','2'", CWAR);
            HttpContext.Current.Session["PICKUSING"] = null;
            HttpContext.Current.Session["CWARUSING"] = null;

            return JsonConvert.SerializeObject(DTttccol307);
        }

        [WebMethod]
        public static EntidadPicking ClickPickingPending(string PICK = "", string CWAR = "")
        {
            log.escribirError("los paramentros de entrada son pick=" + PICK + " Y CWAR= " + CWAR, "seguimiento picking", "seguimiento picking", "seguimiento picking");
            bool PickIntUse = false;
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
            MySessionObjPicking.WRH = CWAR;
            MySessionObjPicking.PICK = PICK;
            HttpContext.Current.Session["MyObjPicking"] = MySessionObjPicking;

            DataTable DTttccol307 = _idaltccol307.ConsultarPendientesTccol307("'2'", CWAR);
            if (DTttccol307.Rows.Count > 0)
            {
                log.escribirError("Entro en DTttccol307.Rows.Count" + CWAR, "seguimiento picking", "seguimiento picking", "seguimiento picking");

                foreach (DataRow item in DTttccol307.Rows)
                {
                    if (item["T$PAID"].ToString() == PICK && item["T$CWAR"].ToString() == CWAR)
                    {
                        log.escribirError("Entro en (item[T$PAID].ToString() == PICK && item[T$CWAR].ToString() == CWAR)", "seguimiento picking", "seguimiento picking", "seguimiento picking");
                        MySessionObjPicking.error = true;
                        return MySessionObjPicking;
                    }
                }
            }

            Ent_ttccol307 tccol307 = new Ent_ttccol307();
            tccol307.PAID = PICK;
            tccol307.CWAR = CWAR;
            HttpContext.Current.Session["PICKUSING"] = PICK;
            HttpContext.Current.Session["CWARUSING"] = CWAR;
            log.escribirError("Las variables de session son: " + HttpContext.Current.Session["PICKUSING"].ToString() + " y " + HttpContext.Current.Session["CWARUSING"].ToString(), "seguimiento picking", "seguimiento picking", "seguimiento picking");
            tccol307.STAT_AUX = "2";
            _idaltccol307.ActualizarTccol307(tccol307);
            return getPendingPicket(PICK, CWAR);
        }

        [WebMethod]
        public static string GetAllsPAlletsPending()
        {
            //JC 101021 No mostrar la lista de pallets pendientes cuando se termina el picking
            HttpContext.Current.Session["STATPICKING"] = null;
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
            DataTable Dt082Pickied = twhcolDAL.ConsultarTticol082porStat("", "'1','2'", MySessionObjPicking.PICK.Trim(), MySessionObjPicking.WRH);
            if (Dt082Pickied.Rows.Count > 0)
            {
                HttpContext.Current.Session["DtPalletsPick"] = Dt082Pickied;
                //JC 101021  No mostrar la lista de pallets pendientes cuando se termina el picking
                HttpContext.Current.Session["STATPICKING"] = Dt082Pickied.Rows[0]["T$STAT"].ToString();
            }
            return JsonConvert.SerializeObject(Dt082Pickied);
        }

        [WebMethod]
        public static DataTable GetAllsPAlletsPendingDT()
        {
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
            DataTable Dt082Pickied = twhcolDAL.ConsultarTticol082porStat("", "'1','2'", MySessionObjPicking.PICK.Trim(), MySessionObjPicking.WRH);
            return Dt082Pickied;
        }

        public static EntidadPicking getPendingPicket(string PICK, string CWAR)
        {
            log.escribirError(" Entro en :  getPendingPicket con: " + PICK + " y " + CWAR, "seguimiento picking", "seguimiento picking", "seguimiento picking");
            //JC 101021 Evitar que despliegue la lista de pallets sugeridos si ya se termino el pick
            HttpContext.Current.Session["STATPICKING"] = null;

            EntidadPicking MyObj = new EntidadPicking();
            DataTable Dt082Pickied = twhcolDAL.ConsultarTticol082porStat("", "1", PICK, CWAR);

            foreach (DataRow row in Dt082Pickied.Rows)
            {
                log.escribirError(" entro en foreach (DataRow row in Dt082Pickied.Rows)", "seguimiento picking", "seguimiento picking", "seguimiento picking");
                List<EntidadPicking> LstPallet22PAID = twhcolDAL.ConsultarPalletPicking22PAID(row["T$PAID"].ToString().Trim(), string.Empty, HttpContext.Current.Session["user"].ToString().Trim(), "1", CWAR, PICK);
                List<EntidadPicking> LstPallet042PAID = twhcolDAL.ConsultarPalletPicking042PAID(row["T$PAID"].ToString().Trim(), string.Empty, HttpContext.Current.Session["user"].ToString().Trim(), "1", CWAR, PICK);
                List<EntidadPicking> LstPallet131PAID = twhcolDAL.ConsultarPalletPicking131PAID(row["T$PAID"].ToString().Trim(), CWAR, string.Empty, HttpContext.Current.Session["user"].ToString().Trim(), "5", PICK);
                if (LstPallet22PAID.Count > 0)
                {
                    MyObj = LstPallet22PAID[0];
                    bool mcnopick = machinesPicking.Contains(MyObj.MCNO.Trim()) == true ? MyObj.MCNOPICK = true : MyObj.MCNOPICK = false;
                    HttpContext.Current.Session["MyObjPicking"] = MyObj;
                    HttpContext.Current.Session["flag022"] = 1;
                    HttpContext.Current.Session["flag131"] = 0;
                    HttpContext.Current.Session["flag042"] = 0;
                    //JC 101021 Tomar el estado del picking para desplegar lista de pallets sugeridos
                    HttpContext.Current.Session["STATPICKING"] = Dt082Pickied.Rows[0]["T$STAT"].ToString();
                    return MyObj;
                }
                else if (LstPallet042PAID.Count > 0)
                {
                    MyObj = LstPallet042PAID[0];
                    bool mcnopick = machinesPicking.Contains(MyObj.MCNO.Trim()) == true ? MyObj.MCNOPICK = true : MyObj.MCNOPICK = false;
                    HttpContext.Current.Session["MyObjPicking"] = MyObj;
                    HttpContext.Current.Session["flag042"] = 1;
                    HttpContext.Current.Session["flag022"] = 0;
                    HttpContext.Current.Session["flag131"] = 0;
                    //JC 101021 Tomar el estado del picking para desplegar lista de pallets sugeridos
                    HttpContext.Current.Session["STATPICKING"] = Dt082Pickied.Rows[0]["T$STAT"].ToString();
                    return MyObj;
                }
                else if (LstPallet131PAID.Count > 0)
                {
                    MyObj = LstPallet131PAID[0];
                    bool mcnopick = machinesPicking.Contains(MyObj.MCNO.Trim()) == true ? MyObj.MCNOPICK = true : MyObj.MCNOPICK = false;
                    HttpContext.Current.Session["MyObjPicking"] = MyObj;
                    HttpContext.Current.Session["flag131"] = 1;
                    HttpContext.Current.Session["flag022"] = 0;
                    HttpContext.Current.Session["flag042"] = 0;
                    //JC 101021 Tomar el estado del picking para desplegar lista de pallets sugeridos
                    HttpContext.Current.Session["STATPICKING"] = Dt082Pickied.Rows[0]["T$STAT"].ToString();
                    return MyObj;
                }

            }
            return MyObj;
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
        public static string ShowCurrentOptionsItem()
        {
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
            List<EntidadPicking> LstPallet22 = new List<EntidadPicking>();
            List<EntidadPicking> LstPallet042 = new List<EntidadPicking>();
            List<EntidadPicking> LstPallet131 = new List<EntidadPicking>();
            List<EntidadPicking> LstReturn = new List<EntidadPicking>();
            //JC 101021  No mostrar la tabla de pallets sugeridos si ya es el último pick
            if (HttpContext.Current.Session["STATPICKING"] != null)
            {
                LstPallet131 = twhcolDAL.ConsultarPalletPicking131ItemQty(MySessionObjPicking.ITEM, MySessionObjPicking.QTY, MySessionObjPicking.PRIO, HttpContext.Current.Session["user"].ToString().Trim(), MySessionObjPicking.ORNO, MySessionObjPicking.PONO, MySessionObjPicking.ADVS);
                if (LstPallet131.Count > 0)
                {
                    LstReturn = LstPallet131;
                }

                LstPallet042 = twhcolDAL.ConsultarPalletPicking042ItemQty(MySessionObjPicking.ITEM, MySessionObjPicking.QTY, MySessionObjPicking.PRIO, HttpContext.Current.Session["user"].ToString().Trim(), MySessionObjPicking.ORNO, MySessionObjPicking.PONO, MySessionObjPicking.ADVS);
                if (LstPallet042.Count > 0)
                {
                    LstReturn = LstPallet042;
                }

                LstPallet22 = twhcolDAL.ConsultarPalletPicking22ItemQty(MySessionObjPicking.ITEM, MySessionObjPicking.QTY, MySessionObjPicking.PRIO, HttpContext.Current.Session["user"].ToString().Trim(), MySessionObjPicking.ORNO, MySessionObjPicking.PONO, MySessionObjPicking.ADVS);
                if (LstPallet22.Count > 0)
                {
                    LstReturn = LstPallet22;
                }
            }
            return JsonConvert.SerializeObject(LstReturn);
        }

        [WebMethod]
        public static string VerificarPalletID(string PAID_NEW, string PAID_OLD, string selectOptionPallet = "false")
        {
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
            EntidadPicking ObjPicking = new EntidadPicking();

            DataTable DTPalletID = twhcolDAL.VerificarPalletID(PAID_NEW);
            if (DTPalletID.Rows.Count > 0)
            {
                if (DTPalletID.Rows[0]["ITEM"].ToString().Trim() == MySessionObjPicking.ITEM.Trim())
                {
                    if (Convert.ToDecimal(DTPalletID.Rows[0]["QTYT"].ToString()) >= Convert.ToDecimal(MySessionObjPicking.QTY.Trim()))
                    {
                        ObjPicking.error = false;
                        ObjPicking.PALLETID = PAID_NEW;
                        ObjPicking.LOT = DTPalletID.Rows[0]["LOT"].ToString();
                        ObjPicking.ITEM = DTPalletID.Rows[0]["ITEM"].ToString();
                        ObjPicking.DESCRIPTION = DTPalletID.Rows[0]["DSCA"].ToString();
                        ObjPicking.WRH = DTPalletID.Rows[0]["CWAT"].ToString();
                        ObjPicking.DESCWRH = DTPalletID.Rows[0]["DESCAW"].ToString();
                        ObjPicking.LOCA = DTPalletID.Rows[0]["ACLO"].ToString();
                        ObjPicking.QTYT = DTPalletID.Rows[0]["QTYT"].ToString();
                        ObjPicking.QTY = MySessionObjPicking.QTYT.ToString();
                        ObjPicking.UN = DTPalletID.Rows[0]["UNIT"].ToString();
                        ObjPicking.ALLO = DTPalletID.Rows[0]["ALLO"].ToString();
                        ObjPicking.CNPK = DTPalletID.Rows[0]["CNPK"].ToString();
                        ObjPicking.PICK = MySessionObjPicking.PICK;
                        ObjPicking.ORNO = MySessionObjPicking.ORNO;
                        ObjPicking.PONO = MySessionObjPicking.PONO;
                        ObjPicking.ADVS = MySessionObjPicking.ADVS;
                        string QTYTT = ObjPicking.QTY.ToString();
                        string Tbl = DTPalletID.Rows[0]["TBL"].ToString().Trim();
                        switch (Tbl)
                        {
                            case "ticol022":
                                HttpContext.Current.Session["flag022"] = 1;
                                HttpContext.Current.Session["flag042"] = 0;
                                HttpContext.Current.Session["flag131"] = 0;
                                //JC 190721 twhcolDAL.ActualizarCantidades222(PAID_OLD);
                                twhcolDAL.ActualizarCantidades222_OLD(PAID_OLD, QTYTT);
                                twhcolDAL.ActualizarCantidades222(PAID_NEW);
                                twhcolDAL.ActCausalTICOL022(PAID_OLD, 12);
                                twhcolDAL.ActCausalTICOL022(PAID_NEW, 8);
                                _idaltwhcol122.UpdateTbl082ByPaid(PAID_NEW, PAID_OLD, ObjPicking);
                                MySessionObjPicking.PALLETID = PAID_NEW;
                                HttpContext.Current.Session["MyObjPicking"] = ObjPicking;
                                break;

                            case "ticol042":
                                HttpContext.Current.Session["flag042"] = 1;
                                HttpContext.Current.Session["flag131"] = 0;
                                HttpContext.Current.Session["flag022"] = 0;
                                //JC 190721 twhcolDAL.ActualizarCantidades242(PAID_OLD);
                                twhcolDAL.ActualizarCantidades242_OLD(PAID_OLD, QTYTT);
                                twhcolDAL.ActualizarCantidades242(PAID_NEW);
                                twhcolDAL.ActCausalTICOL042(PAID_OLD, 12);
                                twhcolDAL.ActCausalTICOL042(PAID_NEW, 8);
                                _idaltwhcol122.UpdateTbl082ByPaid(PAID_NEW, PAID_OLD, ObjPicking);
                                MySessionObjPicking.PALLETID = PAID_NEW;
                                HttpContext.Current.Session["MyObjPicking"] = ObjPicking;
                                break;

                            case "whcol131":
                                HttpContext.Current.Session["flag131"] = 1;
                                HttpContext.Current.Session["flag022"] = 0;
                                HttpContext.Current.Session["flag042"] = 0;
                                twhcolDAL.ActualizarCantidades131(PAID_OLD);
                                twhcolDAL.ActualizarCantidades131(PAID_NEW, false);
                                twhcolDAL.ActCausalcol131140(PAID_OLD, 10);
                                twhcolDAL.ActCausalcol131140(PAID_NEW, 6);
                                _idaltwhcol122.UpdateTbl082ByPaid(PAID_NEW, PAID_OLD, ObjPicking);
                                MySessionObjPicking.PALLETID = PAID_NEW;
                                HttpContext.Current.Session["MyObjPicking"] = ObjPicking;
                                break;
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
        public static string VerificarExistenciaPalletID(string PAID_NEW)
        {
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
            EntidadPicking ObjPicking = new EntidadPicking();

            DataTable DTPalletID = twhcolDAL.VerificarPalletID(PAID_NEW);
            if (DTPalletID.Rows.Count > 0)
            {
                if (DTPalletID.Rows[0]["ITEM"].ToString().Trim() == MySessionObjPicking.ITEM.ToString().Trim())
                    //JC 250921 Validar si el pallet nuevo está en la misma bodega
                    //ObjPicking.error = false;
                    //return JsonConvert.SerializeObject(ObjPicking);
                    if (DTPalletID.Rows[0]["CWAT"].ToString().Trim() == MySessionObjPicking.WRH.ToString().Trim())
                    {
                        ObjPicking.error = false; 
                        return JsonConvert.SerializeObject(ObjPicking);
                    }
                    else
                    {
                        ObjPicking.error = true;
                        ObjPicking.errorMsg = ThePalletNotAllowedHavingTheSameWarehouse;
                        return JsonConvert.SerializeObject(ObjPicking);
                        
                    }
                else
                {
                    ObjPicking.error = true;
                    ObjPicking.errorMsg = ThepalletIDNotContaenthesameItem;
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
        public static void Actualizar307()
        {
            Ent_ttccol307 MyObj307 = new Ent_ttccol307();
            if (HttpContext.Current.Session["PICKUSING"] != null && HttpContext.Current.Session["CWARUSING"] != null)
            {
                Ent_ttccol307 tccol307 = new Ent_ttccol307();
                tccol307.PAID = HttpContext.Current.Session["PICKUSING"].ToString();
                tccol307.CWAR = HttpContext.Current.Session["CWARUSING"].ToString();
                tccol307.STAT_AUX = "1";
                _idaltccol307.ActualizarTccol307(tccol307);

            }
            HttpContext.Current.Session["PICKUSING"] = null;
            HttpContext.Current.Session["CWARUSING"] = null;

        }
        [WebMethod]
        public static void Eliminar307()
        {
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
            string sentencia1 = string.Empty;
            twhcolDAL.EliminarTccol307140(MySessionObjPicking.PICK.Trim(), MySessionObjPicking.WRH.Trim(), ref sentencia1);
        }

        [WebMethod]
        public static string Click_confirPKG(string QTYT)
        {
            try
            {
                EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
                string PAID = MySessionObjPicking.PALLETID;
                string LOCA = MySessionObjPicking.LOCA;
                string OORG = MySessionObjPicking.OORG;
                string ORNO = MySessionObjPicking.ORNO;
                string PONO = MySessionObjPicking.PONO;
                //string QTYT = MySessionObjPicking.QTY;
                //JC 250921 Cambio para que tome la cantidad nueva del pallet
                //string QTYT_OLD = MySessionObjPicking.QTY;
                string QTYT_OLD = MySessionObjPicking.QTYT;
                string CUNI = MySessionObjPicking.UN;
                string CWAR = MySessionObjPicking.WRH;
                string CLOT = MySessionObjPicking.LOT;
                string ADVSP = MySessionObjPicking.ADVS;

                string sentencia = string.Empty;
                string sentencia1 = string.Empty;

                decimal qtyt = ConvertToDecimal(QTYT.ToString().Trim());
                decimal qtyt_old = ConvertToDecimal(QTYT_OLD.ToString().Trim());
                decimal qtyt_act = qtyt_old - qtyt;
                string qtytS = ConvertToDecimal(QTYT.ToString().Trim()).ToString().ToString();
                int cnpk = Convert.ToInt32(MySessionObjPicking.CNPK.Trim() == "" ? "2" : MySessionObjPicking.CNPK.Trim());
                String Location = LOCA;

                //Generar Ramdom
                Random generator = new Random();
                int t = generator.Next(1, 1000000);
                string maximo = string.Format("{0:0000000000}", t);


                if (Convert.ToInt32(HttpContext.Current.Session["flag022"].ToString().Trim()) == 1)
                {
                    Ent_tticol022 MyObj = new Ent_tticol022();

                    int res = twhcolDAL.actRegtticol082140(HttpContext.Current.Session["user"].ToString().Trim(), PAID.Trim().ToUpper(), Location.ToUpper(), 2, maximo, OORG, ORNO, "", PONO, qtytS, ADVSP, sentencia);
                    twhcolDAL.ingRegTticol092140(MySessionObjPicking.PICK.Trim(), MySessionObjPicking.PALLETID.Trim(), PAID.Trim().ToUpper(), 10, HttpContext.Current.Session["user"].ToString().Trim());                    
                    MyObj.urpt = " " + HttpContext.Current.Session["user"].ToString().Trim() + " " +
                        "\n- User: " + HttpContext.Current.Session["user"].ToString().Trim() + ",\n- Pallet: " + PAID.Trim().ToUpper() + "\n- Location: " + Location.ToUpper() + "\n- stat: " + "2" + "\n- maximo: " + maximo + "\n- OORG: " + OORG + "\n- ORNO: " + ORNO + "\n- PONO: " + PONO + "\n- qtytS: " + qtytS + "\n- ADVSP: " + ADVSP +
                        "\n" + sentencia + "\n" +
                        "\n" + sentencia1;

                    if (cnpk != 1)
                    {
                        twhcolDAL.updatetticol222Quantity(PAID.Trim(), Convert.ToDecimal(QTYT), Convert.ToDecimal(QTYT_OLD));
                        DataTable DTPallet = _idaltwhcol130.VerificarPalletID(ref PAID);
                        qtyaG = DTPallet.Rows[0]["QTYT"].ToString();
                        MyObj.qtyaG = Convert.ToDecimal(qtyaG);
                        DataTable dtAllo = twhcolDAL.getAllotticol222(PAID.Trim());
                        //if (Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString()) != 0)
                        //{
                        //JC 111021 Ajustar el estado en funcion de las cantidades
                            //_idaltticol125.updataPalletStatus022(PAID, qtyaG == "0" ? "11" : "7");
                              _idaltticol125.updataPalletStatus022(PAID, (qtyaG == "0" && Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString()) == 0) ? "11" : (qtyaG == "0" && Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString()) >= 0) ? "8" : "7");                       
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

                            //MyObj.pdno = ORNO;
                            MyObj.pdno = MySessionObjPicking.LOT.Trim();
                            MyObj.sqnb = ORNO + "-" + SecuenciaPallet;
                            MyObj.proc = 2;
                            MyObj.logn = HttpContext.Current.Session["user"].ToString().Trim();
                            MyObj.mitm = MySessionObjPicking.ITEM.Trim();
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

                            if (validateSave > 0)
                            {
                                twhcolDAL.actRegtticol082140(HttpContext.Current.Session["user"].ToString().Trim(), PAID.Trim().ToUpper(), Location.ToUpper(), 2, maximo, OORG, ORNO, "", PONO, qtytS, ADVSP, sentencia, true, MyObj.sqnb, MySessionObjPicking.PICK);

                            }

                            if (validateSave > 0 && Convert.ToDecimal(qtyaG) != 0)
                            {
                                HttpContext.Current.Session["codeMaterial"] = MyObj.mitm;
                                HttpContext.Current.Session["codePaid"] = PAID;
                                HttpContext.Current.Session["codePaid2"] = MyObj.sqnb;
                                HttpContext.Current.Session["Lot"] = MyObj.pdno; ;
                                HttpContext.Current.Session["Quantity"] = MyObj.qtd1;
                                HttpContext.Current.Session["Quantity2"] = MyObj.qtyaG; 
                                HttpContext.Current.Session["Date"] = MyObj.date;
                                HttpContext.Current.Session["Pallet"] = MyObj.sqnb;
                                HttpContext.Current.Session["Machine"] = "";
                                HttpContext.Current.Session["Operator"] = MyObj.logn;
                                HttpContext.Current.Session["Reprint"] = "no";
                                HttpContext.Current.Session["AutoPrint"] = "yes";

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
                        _idaltticol125.updataPalletStatus022(PAID, "11");
                        twhcolDAL.updatetticol222Quantity(PAID.Trim(), qtyt_old, qtyt_old);
                    }
                    return JsonConvert.SerializeObject(MyObj);

                }
                else if (Convert.ToInt32(HttpContext.Current.Session["flag042"].ToString().Trim()) == 1)
                {
                    Ent_tticol042 MyObj = new Ent_tticol042();
                    int res = twhcolDAL.actRegtticol082140(HttpContext.Current.Session["user"].ToString().Trim(), PAID.Trim().ToUpper(), Location.ToUpper(), 2, maximo, OORG, ORNO, "", PONO, qtytS, ADVSP, sentencia);
                    twhcolDAL.ingRegTticol092140(MySessionObjPicking.PICK.Trim(), MySessionObjPicking.PALLETID.Trim(), PAID.Trim().ToUpper(), 10, HttpContext.Current.Session["user"].ToString().Trim());                    
                    //bool res1 = twhcolDAL.EliminarTccol307140(PAID.Trim(), ref sentencia1);
                    MyObj.urpt = " " + HttpContext.Current.Session["user"].ToString().Trim() + " " + res + " " +
                        "\n- User: " + HttpContext.Current.Session["user"].ToString().Trim() + ",\n- Pallet: " + PAID.Trim().ToUpper() + "\n- Location: " + Location.ToUpper() + "\n- stat: " + "2" + "\n- maximo: " + maximo + "\n- OORG: " + OORG + "\n- ORNO: " + ORNO + "\n- PONO: " + PONO + "\n- qtytS: " + qtytS + "\n- ADVSP: " + ADVSP +
                        "\n" + sentencia + "\n" +
                        "\n" + sentencia1;
                    if (cnpk != 1)
                    {
                        twhcolDAL.updatetticol242Quantity(PAID.Trim(), Convert.ToDecimal(QTYT), Convert.ToDecimal(QTYT_OLD));
                        DataTable DTPallet = _idaltwhcol130.VerificarPalletID(ref PAID);
                        qtyaG = DTPallet.Rows[0]["QTYT"].ToString();
                        MyObj.qtyaG = Convert.ToDecimal(qtyaG);
                        DataTable dtAllo = twhcolDAL.getAllotticol242(PAID.Trim());
                        //if (Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString()) != 0)
                        //{
                        //JC 111021 Ajustar el estado en funcion de las cantidades
                            //_idaltticol125.updataPalletStatus042(PAID, qtyaG == "0" ? "11" : "7");
                              _idaltticol125.updataPalletStatus042(PAID, (qtyaG == "0" && Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString()) == 0) ? "11" : (qtyaG == "0" && Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString()) >= 0) ? "8" : "7");                       
                        //}
                        string strError = string.Empty;
                        string SecuenciaPallet = "C001";
                        int consecutivo = 0;

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


                            //MyObj.pdno = ORNO;
                            MyObj.pdno = MySessionObjPicking.LOT.Trim(); ;
                            MyObj.sqnb = ORNO + "-" + SecuenciaPallet;
                            MyObj.proc = 2;
                            MyObj.logn = HttpContext.Current.Session["user"].ToString().Trim();
                            MyObj.mitm = MySessionObjPicking.ITEM.Trim();
                            MyObj.qtdl = Convert.ToDouble(qtyt.ToString());
                            MyObj.cuni = CUNI;//CUNI;
                            MyObj.log1 = "NONE";
                            MyObj.qtd1 = Convert.ToDecimal(qtyt.ToString());
                            MyObj.pro1 = 2;
                            MyObj.log2 = "NONE";
                            MyObj.qtd2 = Convert.ToDecimal(qtyt.ToString());
                            MyObj.pro2 = 2;
                            //MyObj.loca = LOCA.Trim() == "" ? "" : LOCA;
                            MyObj.loca =  " ";
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

                            if (validateSave > 0)
                            {
                                twhcolDAL.actRegtticol082140(HttpContext.Current.Session["user"].ToString().Trim(), PAID.Trim().ToUpper(), Location.ToUpper(), 2, maximo, OORG, ORNO, "", PONO, qtytS, ADVSP, sentencia, true, MyObj.sqnb, MySessionObjPicking.PRIO);

                            }

                            if (validateSave > 0 && Convert.ToDecimal(qtyaG) != 0)
                            {
                                HttpContext.Current.Session["codeMaterial"] = MyObj.mitm;
                                HttpContext.Current.Session["codePaid"] = MyObj.sqnb;
                                HttpContext.Current.Session["codePaid2"] = PAID;
                                HttpContext.Current.Session["Lot"] = MyObj.pdno; ;
                                HttpContext.Current.Session["Quantity"] = MyObj.qtd1;
                                HttpContext.Current.Session["Quantity2"] = MyObj.qtyaG;
                                HttpContext.Current.Session["Date"] = MyObj.date;
                                HttpContext.Current.Session["Pallet"] = MyObj.sqnb;
                                HttpContext.Current.Session["Machine"] = "";
                                HttpContext.Current.Session["Operator"] = MyObj.logn;
                                HttpContext.Current.Session["Reprint"] = "no";

                                HttpContext.Current.Session["AutoPrint"] = "yes";

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
                        _idaltticol125.updataPalletStatus042(PAID, "11");
                        twhcolDAL.updatetticol242Quantity(PAID.Trim(), qtyt_old, qtyt_old);
                    }

                    return JsonConvert.SerializeObject(MyObj);


                }

                else if (Convert.ToInt32(HttpContext.Current.Session["flag131"].ToString().Trim()) == 1)
                {
                    errorlog = "-Entro en 131\n";
                    int res = twhcolDAL.actRegtticol082140(HttpContext.Current.Session["user"].ToString().Trim(), PAID.Trim().ToUpper(), Location.ToUpper(), 2, maximo, OORG, ORNO, "", PONO, qtytS, ADVSP, sentencia);
                    //JC 240821 Llevar la trazabilidad de los pallets confirmados
                    twhcolDAL.ingRegTticol092140(MySessionObjPicking.PICK.Trim(), MySessionObjPicking.PALLETID.Trim(), PAID.Trim().ToUpper(), 10, HttpContext.Current.Session["user"].ToString().Trim());
                    Ent_twhcol130131 MyObj = new Ent_twhcol130131();
                    //bool res1 = twhcolDAL.EliminarTccol307140(PAID.Trim().Trim(), ref sentencia1);
                    MyObj.urpt = " " + HttpContext.Current.Session["user"].ToString().Trim() + " " + res + " ";
                    MyObj.urpt = " " + HttpContext.Current.Session["user"].ToString().Trim() + " " + res + " " +
                        "\n- User: " + HttpContext.Current.Session["user"].ToString().Trim() + ",\n- Pallet: " + PAID.Trim().ToUpper() + "\n- Location: " + Location.ToUpper() + "\n- stat: " + "2" + "\n- maximo: " + maximo + "\n- OORG: " + OORG + "\n- ORNO: " + ORNO + "\n- PONO: " + PONO + "\n- qtytS: " + qtytS + "\n- ADVSP: " + ADVSP + "\n";
                    if (cnpk != 1)
                    {

                        twhcolDAL.updatetwhcol131QuantityFirst(PAID.Trim(), qtyt, Convert.ToDecimal(MySessionObjPicking.QTYT));
                        DataTable DTPallet = _idaltwhcol130.VerificarPalletID(ref PAID);
                        qtyaG = DTPallet.Rows[0]["QTYT"].ToString();
                        MyObj.qtyaG = Convert.ToDecimal(qtyaG);
                        DataTable dtAllo = twhcolDAL.getAllotwhcol131(PAID.Trim());
                        //if (Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString()) != 0)
                        //{
                        //JC 111021 Ajustar el estado en funcion de las cantidades
                            //_idaltticol125.updataPalletStatus131(PAID, qtyaG == "0" ? "9" : "3");
                            _idaltticol125.updataPalletStatus131(PAID, (qtyaG == "0" && Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString()) == 0) ? "9" : (qtyaG == "0" && Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString()) >= 0) ? "6" : "3");
                        //}

                        if (Convert.ToDecimal(qtyaG) > 0)
                        {
                            string strMaxSequence = getSequence(PAID, "P");
                            string separator = "-";
                            string newPallet = recursos.GenerateNewPallet(strMaxSequence, separator);
                            string SQNB = PAID.Substring(0, PAID.IndexOf(separator));

                            MyObj.OORG = "2";// Order type escaneada view 
                            MyObj.ORNO = ORNO;
                            MyObj.ITEM = MySessionObjPicking.ITEM.Trim();
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
                            MyObj.LOCA = MySessionObjPicking.LOCA.Trim();
                            //MyObj.ALLO = qtyt.ToString();
                            MyObj.ALLO = "0";
                            MyObj.ALLOAUX = Convert.ToDecimal(dtAllo.Rows[0]["ALLO"].ToString());

                            bool Insertsucces = twhcol130DAL.Insertartwhcol131(MyObj);

                            if (Insertsucces)
                            {
                                twhcolDAL.actRegtticol082140(HttpContext.Current.Session["user"].ToString().Trim(), PAID.Trim().ToUpper(), Location.ToUpper(), 2, maximo, OORG, ORNO, "", PONO, qtytS, ADVSP, sentencia, true, MyObj.PAID, MySessionObjPicking.PRIO);
                                //JC 240821 Llevar la trazabilidad de los pallets confirmados
                                twhcolDAL.ingRegTticol092140(MySessionObjPicking.PICK.Trim(), MySessionObjPicking.PALLETID.Trim(), MyObj.PAID, 10, HttpContext.Current.Session["user"].ToString().Trim());
                            }

                            //if (Insertsucces && qtyt_act != 0)
                            if (Insertsucces && Convert.ToDecimal(qtyaG) != 0)
                            {
                                HttpContext.Current.Session["codeMaterial"] = MyObj.ITEM;
                                HttpContext.Current.Session["codePaid"] = PAID;
                                HttpContext.Current.Session["codePaid2"] = MyObj.PAID;
                                HttpContext.Current.Session["Lot"] = MyObj.CLOT;
                                HttpContext.Current.Session["Quantity"] = MyObj.QTYC;
                                HttpContext.Current.Session["Quantity2"] = MyObj.qtyaG;
                                HttpContext.Current.Session["Date"] = MyObj.DATE;
                                HttpContext.Current.Session["Pallet"] = MyObj.PAID;
                                HttpContext.Current.Session["Machine"] = "";
                                HttpContext.Current.Session["Operator"] = MyObj.LOGN;
                                HttpContext.Current.Session["Reprint"] = "no";
                                HttpContext.Current.Session["AutoPrint"] = "yes";

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
                    }
                    else
                    {
                        _idaltticol125.updataPalletStatus131(PAID, "9");
                        twhcolDAL.updatetwhcol131Quantity(PAID.Trim(), qtyt_old, qtyt_old);
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
        public static EntidadPicking SkipPicking()
        {
            DataTable DtPalletsPick = (DataTable)HttpContext.Current.Session["DtPalletsPick"];
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
            int index = 0;
            int indexCurrent = 0;
            bool NoHallado = true;
            foreach (DataRow row in DtPalletsPick.Rows)
            {
                if (row["T$ADVS"].ToString().Trim() == MySessionObjPicking.ADVS.Trim() && row["T$PAID"].ToString().Trim() == MySessionObjPicking.PALLETID.Trim())
                {
                    indexCurrent = index;
                }
                else
                {
                    index++;
                }
            }

            while (indexCurrent < DtPalletsPick.Rows.Count && NoHallado == true)
            {

                if (DtPalletsPick.Rows[indexCurrent + 1 == DtPalletsPick.Rows.Count ? 0 : indexCurrent + 1]["T$STAT"].ToString().Trim() == "1" && DtPalletsPick.Rows.Count > 1)
                {
                    List<EntidadPicking> LstPallet22PAID = twhcolDAL.ConsultarPalletPicking22PAID((DtPalletsPick.Rows[indexCurrent + 1 <= DtPalletsPick.Rows.Count - 1 ? indexCurrent + 1 : 0]["T$PAID"].ToString()), string.Empty, HttpContext.Current.Session["user"].ToString().Trim(), "1", (DtPalletsPick.Rows[indexCurrent + 1 <= DtPalletsPick.Rows.Count - 1 ? indexCurrent + 1 : 0]["T$CWAR"].ToString()), (DtPalletsPick.Rows[indexCurrent + 1 == DtPalletsPick.Rows.Count - 1 ? indexCurrent + 1 : 0]["T$PICK"].ToString()));
                    List<EntidadPicking> LstPallet42PAID = twhcolDAL.ConsultarPalletPicking042PAID((DtPalletsPick.Rows[indexCurrent + 1 <= DtPalletsPick.Rows.Count - 1 ? indexCurrent + 1 : 0]["T$PAID"].ToString()), string.Empty, HttpContext.Current.Session["user"].ToString().Trim(), "1", (DtPalletsPick.Rows[indexCurrent + 1 <= DtPalletsPick.Rows.Count - 1 ? indexCurrent + 1 : 0]["T$CWAR"].ToString()), (DtPalletsPick.Rows[indexCurrent + 1 == DtPalletsPick.Rows.Count - 1 ? indexCurrent + 1 : 0]["T$PICK"].ToString()));
                    List<EntidadPicking> LstPallet131PAID = twhcolDAL.ConsultarPalletPicking131PAID((DtPalletsPick.Rows[indexCurrent + 1 <= DtPalletsPick.Rows.Count - 1 ? indexCurrent + 1 : 0]["T$PAID"].ToString()), MySessionObjPicking.WRH, string.Empty, HttpContext.Current.Session["user"].ToString().Trim(), "1", (DtPalletsPick.Rows[indexCurrent + 1 <= DtPalletsPick.Rows.Count - 1 ? indexCurrent + 1 : 0]["T$PICK"].ToString()));

                    if (LstPallet22PAID.Count > 0)
                    {
                        HttpContext.Current.Session["MyObjPicking"] = LstPallet22PAID[0];
                        MySessionObjPicking = LstPallet22PAID[0];
                        NoHallado = false;
                        bool mcnopick = machinesPicking.Contains(MySessionObjPicking.MCNO.Trim()) == true ? MySessionObjPicking.MCNOPICK = true : MySessionObjPicking.MCNOPICK = false;
                    }
                    else if (LstPallet42PAID.Count > 0)
                    {
                        HttpContext.Current.Session["MyObjPicking"] = LstPallet42PAID[0];
                        MySessionObjPicking = LstPallet42PAID[0];
                        NoHallado = false;
                        bool mcnopick = machinesPicking.Contains(MySessionObjPicking.MCNO.Trim()) == true ? MySessionObjPicking.MCNOPICK = true : MySessionObjPicking.MCNOPICK = false;
                    }
                    else if (LstPallet131PAID.Count > 0)
                    {
                        HttpContext.Current.Session["MyObjPicking"] = LstPallet131PAID[0];
                        MySessionObjPicking = LstPallet131PAID[0];
                        NoHallado = false;
                        bool mcnopick = machinesPicking.Contains(MySessionObjPicking.MCNO.Trim()) == true ? MySessionObjPicking.MCNOPICK = true : MySessionObjPicking.MCNOPICK = false;
                    }
                    else
                    {
                        indexCurrent++;
                    }
                }
                else
                {
                    int indexCurrentVal = indexCurrent + 1 == DtPalletsPick.Rows.Count ? 0 : indexCurrent + 1;
                    string currentStat = DtPalletsPick.Rows[indexCurrentVal]["T$STAT"].ToString().Trim();
                    if (currentStat == "2")
                    {
                        indexCurrent = indexCurrentVal;
                    }
                    else
                    {

                        indexCurrent++;
                    }
                }

            }
            return MySessionObjPicking;
        }

        [WebMethod]
        public static string Drop(string PAID, bool Consigment = false)
        {
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];

            string res = string.Empty;
            Ent_tticol082 Obj082 = new Ent_tticol082();
            Obj082.STAT = Consigment == false ? "4" : "7";
            Obj082.PAID = PAID.Trim();
            
            int randomNum = new Random().Next(999);
            string ramdomNumStr = string.Empty;

            if (randomNum > 0 && randomNum < 10)
            {
                ramdomNumStr = "00" + randomNum.ToString();
            }
            else if (randomNum > 9 && randomNum < 100)
            {
                ramdomNumStr = "0" + randomNum.ToString();
            }
            else
            {
                ramdomNumStr = randomNum.ToString();
            }
            Obj082.PICK = MySessionObjPicking.PICK.Trim();
            //JC 260921 Ejecutar Drop del pallet unico
            DataTable RespAdvs = _idaltwhcol122.ConsultarPallet_X_Picking(Obj082);
            if (RespAdvs.Rows.Count > 0)
            {
                Obj082.OORG = RespAdvs.Rows[0]["T$OORG"].ToString().Trim();
                Obj082.ORNO = RespAdvs.Rows[0]["T$ORNO"].ToString().Trim();
                Obj082.PONO = RespAdvs.Rows[0]["T$PONO"].ToString().Trim();
                Obj082.ADVS = RespAdvs.Rows[0]["T$ADVS"].ToString().Trim();
                //JC 021021 Enviar la cantidad del pallet por picking
                Obj082.QTYT = RespAdvs.Rows[0]["T$QTYT"].ToString().Trim();
            }
            Obj082.RAND = ramdomNumStr;
            //Obj082.ADVS = MySessionObjPicking.ADVS.Trim();
            if (_idaltwhcol122.UpdateTtico082Stat(Obj082) == true)
            {
                //JC 230721 Envio el nuevo valor del pick listo para imprimir
                //res = UrlBaseBarcode;
                Obj082.PICK = MySessionObjPicking.PICK.Trim() + "-" + ramdomNumStr;
                //JC 021021 Imprimir la cantidad del pick por pallet.
                Obj082.PAID = MySessionObjPicking.PALLETID.Trim();
                res = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Obj082.PICK + "&code=Code128&dpi=96";
            }
            else
            {

            }
            if (GetAllsPAlletsPendingDT().Rows.Count == 0)
            {
                Eliminar307();
            }
            return res;
        }

        [WebMethod]
        public static string DropEndPicking(string PICK, bool Consigment = false)
        {
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
            string res = string.Empty;
            Ent_tticol082 Obj082 = new Ent_tticol082();
            Obj082.STAT = Consigment == false ? "4" : "7";
            Obj082.PICK = PICK.Trim();
            int randomNum = new Random().Next(999);
            string ramdomNumStr = string.Empty;

            if (randomNum > 0 && randomNum < 10)
            {
                ramdomNumStr = "00" + randomNum.ToString();
            }
            else if (randomNum > 9 && randomNum < 100)
            {
                ramdomNumStr = "0" + randomNum.ToString();
            }
            else
            {
                ramdomNumStr = randomNum.ToString();
            }
            Obj082.PICK = MySessionObjPicking.PICK.Trim();
            //JC 250921 Ejecutar Drop del pallet unico
            Obj082.ORNO = MySessionObjPicking.ORNO.Trim();
            Obj082.PONO = MySessionObjPicking.PONO.Trim();
            //Obj082.ADVS = MySessionObjPicking.ADVS.Trim();
            if (_idaltwhcol122.UpdateTtico082StatEndPicking(Obj082, ramdomNumStr) == true)
            {
                //JC 230721 Envio el nuevo valor del pick listo para imprimir
                //res = UrlBaseBarcode;
                Obj082.PICK = MySessionObjPicking.PICK.Trim() + "-" + ramdomNumStr;
                res = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Obj082.PICK + "&code=Code128&dpi=96";
            }
            else
            {

            }
            if (GetAllsPAlletsPendingDT().Rows.Count == 0)
            {
                Eliminar307();
            }
            return res;
        }

        [WebMethod]
        public static bool Click_confirCausal(string PAID, string Causal, string txtPallet, string LOCA)
        {
            EntidadPicking MySessionObjPicking = (EntidadPicking)HttpContext.Current.Session["MyObjPicking"];
            string OORG = MySessionObjPicking.OORG;
            string ORNO = MySessionObjPicking.ORNO;
            string PONO = MySessionObjPicking.PONO;
            string ADVS = MySessionObjPicking.ADVS;
            //JC 250921 No permitir tomar el pallet si ya está asignado al picking
            Ent_tticol082 myObj082PPaid = new Ent_tticol082();
            myObj082PPaid.PAID = PAID;
            myObj082PPaid.PICK = MySessionObjPicking.PICK.Trim();
            DataTable ResPPaid = twhcolDAL.ConsultarPallet_X_Picking(myObj082PPaid);
            if (ResPPaid.Rows.Count > 0)
            {
                string script = "<script type = 'text/javascript'>alert('Pallet not Allowed, cause is into this pick.');</script>";
                errorlog = "Pallet not Allowed, cause is into this pick.";
                return false;
            }

            string sentencia1 = string.Empty;
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
                string strError = string.Empty;
                Random generator = new Random();
                int t = generator.Next(1, 1000000);
                string maximo = string.Format("{0:0000000000}", t);
                Ent_tticol082 myObj082 = new Ent_tticol082();
                myObj082.STAT = "3";
                myObj082.PRIO = MySessionObjPicking.PRIO.ToString();

                if (Convert.ToInt32(HttpContext.Current.Session["flag022"].ToString().Trim()) == 1)
                {
                    stat = 12;
                    twhcolDAL.ingRegTticol092140(maximo, MySessionObjPicking.PALLETID.Trim(), PAID, statCausal, HttpContext.Current.Session["user"].ToString().Trim());
                    twhcolDAL.InsertRegCausalCOL084(MySessionObjPicking.PALLETID.Trim(), HttpContext.Current.Session["user"].ToString().Trim(), statCausal);
                    _idaltticol022.ActualizacionPalletId(MySessionObjPicking.PALLETID.Trim(), stat.ToString(), strError);
                    //if (MySessionObjPicking.LOCA.Trim() != LOCA.Trim())
                    //{
                    twhcolDAL.ActualizarCantidades222(MySessionObjPicking.PALLETID.Trim());
                    Ent_tticol082 Obj082 = new Ent_tticol082();
                    //JC 101021 Evitar que los pickings queden bloqueados, en lugar de eso siempre van a quedar creados
                    //Obj082.STAT = "3";
                    Obj082.STAT = "1";
                    Obj082.PAID = MySessionObjPicking.PALLETID.Trim();
                    //JC 250921 Ejecutar Drop del pallet unico
                    Obj082.PICK = MySessionObjPicking.PICK.Trim();
                    //JC 260921 Ejecutar Drop del pallet unico
                    DataTable RespAdvs = _idaltwhcol122.ConsultarPallet_X_Picking(Obj082);
                    if (RespAdvs.Rows.Count > 0)
                    {
                        Obj082.OORG = RespAdvs.Rows[0]["T$OORG"].ToString().Trim();
                        Obj082.ORNO = RespAdvs.Rows[0]["T$ORNO"].ToString().Trim();
                        Obj082.PONO = RespAdvs.Rows[0]["T$PONO"].ToString().Trim();
                        Obj082.ADVS = RespAdvs.Rows[0]["T$ADVS"].ToString().Trim();
                    }
                    _idaltwhcol122.UpdateTtico082Stat_CambioPallet(Obj082);
                    //}
                    return true;

                }
                else if (Convert.ToInt32(HttpContext.Current.Session["flag042"].ToString().Trim()) == 1)
                {

                    stat = 12;
                    twhcolDAL.ingRegTticol092140(maximo, MySessionObjPicking.PALLETID.Trim(), PAID, statCausal, HttpContext.Current.Session["user"].ToString().Trim());
                    twhcolDAL.InsertRegCausalCOL084(MySessionObjPicking.PALLETID.Trim(), HttpContext.Current.Session["user"].ToString().Trim(), statCausal);
                    //JC 190721 _idaltticol042.ActualizacionPalletId(PAID, stat.ToString(), strError);
                    _idaltticol042.ActualizacionPalletId(MySessionObjPicking.PALLETID.Trim(), stat.ToString(), strError);
                    //if (MySessionObjPicking.LOCA.Trim() != LOCA.Trim())
                    //{
                    twhcolDAL.ActualizarCantidades242(MySessionObjPicking.PALLETID.Trim());
                    Ent_tticol082 Obj082 = new Ent_tticol082();
                    //JC 101021 Evitar que los pickings queden bloqueados, en lugar de eso siempre van a quedar creados
                    //Obj082.STAT = "3";
                    Obj082.STAT = "1";
                    Obj082.PAID = MySessionObjPicking.PALLETID.Trim();
                    //JC 250921 Ejecutar Drop del pallet unico
                    Obj082.PICK = MySessionObjPicking.PICK.Trim();
                    //JC 260921 Ejecutar Drop del pallet unico
                    DataTable RespAdvs = _idaltwhcol122.ConsultarPallet_X_Picking(Obj082);
                    if (RespAdvs.Rows.Count > 0)
                    {
                        Obj082.OORG = RespAdvs.Rows[0]["T$OORG"].ToString().Trim();
                        Obj082.ORNO = RespAdvs.Rows[0]["T$ORNO"].ToString().Trim();
                        Obj082.PONO = RespAdvs.Rows[0]["T$PONO"].ToString().Trim();
                        Obj082.ADVS = RespAdvs.Rows[0]["T$ADVS"].ToString().Trim();
                    }
                    _idaltwhcol122.UpdateTtico082Stat_CambioPallet(Obj082);
                    //}
                    return true;

                }

                else if (Convert.ToInt32(HttpContext.Current.Session["flag131"].ToString().Trim()) == 1)
                {
                    stat = 10;
                    twhcolDAL.ingRegTticol092140(maximo, MySessionObjPicking.PALLETID.Trim(), PAID, statCausal, HttpContext.Current.Session["user"].ToString().Trim());
                    twhcolDAL.InsertRegCausalCOL084(MySessionObjPicking.PALLETID.Trim(), HttpContext.Current.Session["user"].ToString().Trim(), statCausal);
                    _idaltticol125.updataPalletStatus131(MySessionObjPicking.PALLETID.Trim(), stat.ToString());
                    //if (MySessionObjPicking.LOCA.Trim() != LOCA.Trim())
                    //{
                    twhcolDAL.ActualizarCantidades131(MySessionObjPicking.PALLETID.Trim(), false);
                    Ent_tticol082 Obj082 = new Ent_tticol082();
                    //JC 101021 Evitar que los pickings queden bloqueados, en lugar de eso siempre van a quedar creados
                    //Obj082.STAT = "3";
                    Obj082.STAT = "1";
                    Obj082.PAID = MySessionObjPicking.PALLETID.Trim();
                    Obj082.PICK = MySessionObjPicking.PICK.Trim();
                    //JC 260921 Ejecutar Drop del pallet unico
                    DataTable RespAdvs = _idaltwhcol122.ConsultarPallet_X_Picking(Obj082);
                    if (RespAdvs.Rows.Count > 0)
                    {
                        Obj082.OORG = RespAdvs.Rows[0]["T$OORG"].ToString().Trim();
                        Obj082.ORNO = RespAdvs.Rows[0]["T$ORNO"].ToString().Trim();
                        Obj082.PONO = RespAdvs.Rows[0]["T$PONO"].ToString().Trim();
                        Obj082.ADVS = RespAdvs.Rows[0]["T$ADVS"].ToString().Trim();
                    }
                    _idaltwhcol122.UpdateTtico082Stat_CambioPallet(Obj082);
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

