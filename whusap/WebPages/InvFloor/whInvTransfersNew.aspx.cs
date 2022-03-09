using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI; 
using System.Web.UI.WebControls;
using whusa;
using whusa.Interfases;
using Newtonsoft.Json;
using System.Data;
using System.Configuration;
using whusa.Entidades;
using whusa.Interfases;
using System.Globalization;
using System.Threading;
using whusa.Utilidades;

namespace whusap.WebPages.InvFloor
{
    public partial class whInvTransfersNew : System.Web.UI.Page
    {
        public static IntefazDAL_transfer Transfers = new IntefazDAL_transfer();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        //JC 171021 Consultar stock en baan antes de tranferir
        private static InterfazDAL_tticol127 _idaltticol127 = new InterfazDAL_tticol127();
        public static InterfazDAL_ttwhcol016 ITtwhcol016 = new InterfazDAL_ttwhcol016();
        public static InterfazDAL_twhwmd200 ITwhwmd200 = new InterfazDAL_twhwmd200();
        public static IntefazDAL_transfer Itransfer = new IntefazDAL_transfer();

        public string lstWarehouses = string.Empty;
        public List<string> lstLocates = new List<string>();
        public static string _operator;
        public static string _idioma;
        public static string strError;
        public static string formName = "NewWhInvTransfers.aspx";


        public static string Thetransferwassuccessful = string.Empty;
        public static string PalletNotLocate = string.Empty;
        public static string PalletNotExist = string.Empty;
        public static string PalletRfidNotExist = string.Empty;
        public static string WarehouseNotExist = string.Empty;
        public static string WarehouseConsigment = string.Empty;
        public static string LocationTransfeCannotEqual = string.Empty;
        public static string LocationTypeMustBulK = string.Empty;
        public static string LocationBlockedTransfers = string.Empty;
        public static string TransferNotUpdated = string.Empty;
        public static string TranferQtynotenough = string.Empty;
        public static string NotInserted = string.Empty;
        public static string TargetLocationNotExist = string.Empty;
        public static string CurrentNotExist = string.Empty;
        public static string PalletIdAlreadyTransferAndPendingToProcess = string.Empty;

        private static Mensajes _mensajesForm = new Mensajes();
        private static string globalMessages = "GlobalMessages";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["user"] == null)
                {
                    Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                }
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                _operator = Session["user"].ToString();
                try
                {
                    _idioma = Session["ddlIdioma"].ToString();
                }
                catch (Exception)
                {
                    _idioma = "INGLES";
                }


                CargarIdioma();

                string strTitulo = mensajes("encabezado");
                control.Text = strTitulo;

                Ent_ttccol301 data = new Ent_ttccol301()
                {
                    user = HttpContext.Current.Session["user"].ToString(),
                    come = strTitulo,
                    refcntd = 0,
                    refcntu = 0
                };

                List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                datalog.Add(data);

                _idalttccol301.insertarRegistro(ref datalog, ref strError);

            }
            lstWarehouses = ListWarehouses();
        }

        [WebMethod]
        public static string VerifyWarehouse(string LOCA)
        {
            Warehouse MyWarehouse = new Warehouse();
            DataTable CurrentWareHouse = Transfers.ConsultarWarehouse(LOCA);
            if (CurrentWareHouse.Rows.Count > 0)
            {
                MyWarehouse.CWAR = CurrentWareHouse.Rows[0]["CWAR"].ToString();
            }
            return JsonConvert.SerializeObject(MyWarehouse);
        }

        [WebMethod]
        public static string VerificarTipoWarehouse(string WARE, string ROW)
        {

            string strError = string.Empty;

            Ent_ttwhcol016 Obj_twhcol016 = new Ent_ttwhcol016();
            Obj_twhcol016.cwar = WARE.ToUpper();
            Obj_twhcol016.Row = ROW;

            Ent_twhwmd200 Obj_twhwmd200 = new Ent_twhwmd200();
            Obj_twhwmd200.cwar = WARE.ToUpper();

            DataTable DtConsigment = ITtwhcol016.UserConsigment(HttpContext.Current.Session["user"].ToString(), WARE.ToUpper());
            DataTable DtTtwhcol016 = ITtwhcol016.TakeMaterialInv_verificaBodega_Param(ref Obj_twhcol016, ref strError);
            DataTable DtTwhwmd200 = ITwhwmd200.listaRegistro_ObtieneAlmacenLocation(ref Obj_twhwmd200, ref strError);

            if (DtTtwhcol016.Rows.Count > 0)
            {
                Obj_twhcol016.Error = false;
                Obj_twhcol016.TypeMsgJs = "console";
                Obj_twhcol016.SuccessMsg = "Warehouse Encontrado";
                

                Obj_twhcol016.TYPW = DtTtwhcol016.Rows[0]["TYPW"].ToString();

                if (Obj_twhcol016.TYPW.ToString() == "21" && DtConsigment.Rows[0]["T$CONS"].ToString().Trim() != "1")
                {
                    Obj_twhcol016.Error = true;
                    Obj_twhcol016.ErrorMsg = WarehouseConsigment;
                    Obj_twhcol016.TypeMsgJs = "lbl";
                }

                if (DtTwhwmd200.Rows.Count > 0)
                {
                    Obj_twhcol016.sloc = DtTwhwmd200.Rows[0]["LOC"].ToString();
                }
                else
                {
                    Obj_twhcol016.sloc = string.Empty;
                }
            }
            else
            {
                Obj_twhcol016.Error = true;
                Obj_twhcol016.TypeMsgJs = "label";
                Obj_twhcol016.ErrorMsg = WarehouseNotExist;
            }
            Obj_twhcol016.cwar.ToUpper();
            return JsonConvert.SerializeObject(Obj_twhcol016);
        }

        
        [WebMethod]
        public static string clickQuery(string PAID, string ROW)
        {

            DataTable Transferencias = Transfers.ConsultarRegistroTransferir(PAID);


            Ent_twhcol020 objWhcol020 = new Ent_twhcol020();

            if (Transferencias.Rows.Count > 0)
            {
                //if (Transferencias.Rows[0]["SLOC"].ToString() == "1")
                //{
                if (Transferencias.Rows[0]["T$LOCA"].ToString().Trim() == string.Empty && Transferencias.Rows[0]["T$CWAR"].ToString().Trim() == string.Empty)
                {
                    objWhcol020.Error = true;
                    objWhcol020.ErrorMsg = PalletNotLocate;
                    objWhcol020.TipeMsgJs = "lbl";
                }
                else
                {
                                        //JC 030222 Validar si el item es de RFID que el pallet este asociado a un RFID
                    //DataTable Rfid = Transfers.ConsultarRegistroTransferirRfid(Transferencias.Rows[0]["T$ITEM"].ToString().Trim());

                    //if (Rfid.Rows[0]["T$RFID"].ToString().Trim() == "1")
                    //{
                    //    DataTable RfidPallet = Transfers.ConsultarRegistroTransferirRfidPallet(PAID);

                    //    if (RfidPallet.Rows.Count > 0)
                    //    {
                    //        objWhcol020.tbl = Transferencias.Rows[0]["TBL"].ToString().Trim();
                    //        objWhcol020.sloc = Transferencias.Rows[0]["SLOC"].ToString();
                    //        objWhcol020.clot = Transferencias.Rows[0]["T$ORNO"].ToString().Trim();
                    //        objWhcol020.sqnb = Transferencias.Rows[0]["T$PAID"].ToString().Trim();
                    //        objWhcol020.mitm = Transferencias.Rows[0]["T$ITEM"].ToString().Trim();
                    //        objWhcol020.dsca = Transfers.DescripcionItem(objWhcol020.mitm);
                    //        objWhcol020.cwor = Transferencias.Rows[0]["T$CWAR"].ToString().Trim();
                    //        objWhcol020.loor = Transferencias.Rows[0]["T$LOCA"].ToString().Trim();
                    //        objWhcol020.qtdl = Convert.ToDouble(Transferencias.Rows[0]["T$QTYC"].ToString().Trim());
                    //        objWhcol020.cuni = Transferencias.Rows[0]["T$CUNI"].ToString().Trim();
                    //        objWhcol020.user = HttpContext.Current.Session["user"].ToString();
                    //    }
                    //    else
                    //    {
                    //        objWhcol020.Error = true;
                    //        objWhcol020.ErrorMsg = PalletRfidNotExist;
                    //        objWhcol020.TipeMsgJs = "lbl";
                    //        objWhcol020.row = ROW;
                    //    }
                    //}
                    //else
                    //{
                        objWhcol020.tbl = Transferencias.Rows[0]["TBL"].ToString().Trim();
                        objWhcol020.sloc = Transferencias.Rows[0]["SLOC"].ToString();
                        objWhcol020.clot = Transferencias.Rows[0]["T$ORNO"].ToString().Trim();
                        objWhcol020.sqnb = Transferencias.Rows[0]["T$PAID"].ToString().Trim();
                        objWhcol020.mitm = Transferencias.Rows[0]["T$ITEM"].ToString().Trim();
                        objWhcol020.dsca = Transfers.DescripcionItem(objWhcol020.mitm);
                        objWhcol020.cwor = Transferencias.Rows[0]["T$CWAR"].ToString().Trim();
                        objWhcol020.loor = Transferencias.Rows[0]["T$LOCA"].ToString().Trim();
                        objWhcol020.qtdl = Convert.ToDouble(Transferencias.Rows[0]["T$QTYC"].ToString().Trim());
                        objWhcol020.cuni = Transferencias.Rows[0]["T$CUNI"].ToString().Trim();
                        objWhcol020.user = HttpContext.Current.Session["user"].ToString();
                        objWhcol020.row = ROW;
                    //}
                }
                //}
            }
            else
            {
                objWhcol020.Error = true;
                objWhcol020.ErrorMsg = PalletNotExist;
                objWhcol020.TipeMsgJs = "lbl";
                objWhcol020.row = ROW;

            }

            return JsonConvert.SerializeObject(objWhcol020);

        }

        [WebMethod]
        public static string VerificarLocation(string CWAR, string LOCA, string ROW)
        {

            string strError = string.Empty;
            Ent_twhwmd200 Obj_twhwmd200 = new Ent_twhwmd200();
            Obj_twhwmd200.cwar = CWAR.ToUpper();
            Obj_twhwmd200.row = ROW;

            DataTable DtTransfer = Itransfer.ConsultarLocation(Obj_twhwmd200.cwar, LOCA.ToUpper());

            if (DtTransfer.Rows.Count > 0)
            {
                if (DtTransfer.Rows[0]["LOCT"].ToString() == "5")
                {
                    if (DtTransfer.Rows[0]["BINB"].ToString() == "2")
                    {
                        Obj_twhwmd200.Error = false;
                        Obj_twhwmd200.TypeMsgJs = "console";
                        Obj_twhwmd200.SuccessMsg = "Location Encontrado";
                    }
                    else
                    {
                        Obj_twhwmd200.Error = true;
                        Obj_twhwmd200.TypeMsgJs = "label";

                        Obj_twhwmd200.ErrorMsg = "Location blocked inbound";
                    }
                }
                else
                {
                    Obj_twhwmd200.Error = true;
                    Obj_twhwmd200.TypeMsgJs = "label";

                    Obj_twhwmd200.ErrorMsg = "Location code doesnt exist";
                }

            }
            else
            {
                Obj_twhwmd200.Error = true;
                Obj_twhwmd200.TypeMsgJs = "label";

                Obj_twhwmd200.ErrorMsg = "Location code doesnt exist";
            }

            return JsonConvert.SerializeObject(Obj_twhwmd200);


        }

        [WebMethod]
        public static string clickTransfer(string PAID, string CurrentWarehouse, string CurrentSloc, string CurrentLocation, string TargetWarehouse, string TargetSloc, string TargetLocation, string row,bool StartCurrentWarehouse = true)
        {
            Ent_twhcol020 objWhcol020 = new Ent_twhcol020();
            objWhcol020.row = row;
            DataTable Transferencias = Transfers.ConsultarRegistroTransferir(PAID);

            bool LocationCurrent = false;
            bool LocationTarget = false;
            if (CurrentWarehouse != TargetWarehouse && CurrentLocation != TargetLocation || CurrentWarehouse == TargetWarehouse && CurrentLocation != TargetLocation || CurrentWarehouse != TargetWarehouse && CurrentLocation == TargetLocation || (CurrentWarehouse == TargetWarehouse && CurrentLocation == TargetLocation && CurrentSloc.Trim() != "1" && TargetSloc != "1"))
            {
                if (CurrentSloc.Trim() != "1")
                {
                    LocationCurrent = true;
                }
                else
                {
                    if (StartCurrentWarehouse == true)
                    {
                        LocationCurrent = true;
                    }
                }


                if (TargetSloc.Trim() != "1")
                {
                    LocationTarget = true;
                }
                else
                {
                    var DtCurrentLocation = Transfers.ConsultarLocation(TargetWarehouse, TargetLocation);
                    if (DtCurrentLocation.Rows.Count > 0)
                    {
                        string LOCT = DtCurrentLocation.Rows[0]["LOCT"].ToString().Trim();
                        string BINB = DtCurrentLocation.Rows[0]["BINB"].ToString().Trim();
                        if (LOCT != "5")
                        {
                            objWhcol020.Error = true;
                            objWhcol020.ErrorMsg = LocationTypeMustBulK;
                            objWhcol020.TipeMsgJs = "lbl";
                            return JsonConvert.SerializeObject(objWhcol020);
                        }
                        else if (BINB != "2")
                        {
                            objWhcol020.Error = true;
                            objWhcol020.ErrorMsg = LocationBlockedTransfers;
                            objWhcol020.TipeMsgJs = "lbl";
                            return JsonConvert.SerializeObject(objWhcol020);
                        }
                        else
                        {
                            LocationTarget = true;
                        }
                    }
                }


                if (LocationCurrent == true)
                {
                    if (LocationTarget == true)
                    {

                        if (Transferencias.Rows.Count > 0)
                        {
                            objWhcol020.tbl = Transferencias.Rows[0]["TBL"].ToString().Trim();
                            objWhcol020.clot = Transferencias.Rows[0]["T$ORNO"].ToString().Trim();
                            objWhcol020.sqnb = Transferencias.Rows[0]["T$PAID"].ToString().Trim();
                            objWhcol020.mitm = Transferencias.Rows[0]["T$ITEM"].ToString();
                            objWhcol020.dsca = Transfers.DescripcionItem(objWhcol020.mitm);
                            objWhcol020.cwor = CurrentWarehouse;
                            objWhcol020.loor = CurrentLocation;
                            objWhcol020.cwde = TargetWarehouse;
                            objWhcol020.lode = TargetLocation;

                            objWhcol020.qtdl = Convert.ToDouble(Transferencias.Rows[0]["T$QTYC"].ToString().Trim());
                            objWhcol020.cuni = Transferencias.Rows[0]["T$CUNI"].ToString().Trim();
                            objWhcol020.user = HttpContext.Current.Session["user"].ToString();




                            DataTable ExistenciaTransfer = Transfers.ConsultaTransferencia(PAID);

                            if (ExistenciaTransfer.Rows.Count > 0)
                            {
                                objWhcol020.Error = true;
                                objWhcol020.ErrorMsg = PalletIdAlreadyTransferAndPendingToProcess;
                                objWhcol020.TipeMsgJs = "lbl";
                            }
                            else
                            {
                                DataTable StockBaan = Transfers.ValidarStockBaan(objWhcol020);
                                if (StockBaan.Rows.Count <= 0)
                                {
                                    objWhcol020.Error = true;
                                    objWhcol020.ErrorMsg = TranferQtynotenough;
                                    objWhcol020.TipeMsgJs = "lbl";

                                }
                                else
                                {
                                    bool TransferenciasI = Transfers.InsertarTransferencia(objWhcol020);

                                    if (TransferenciasI)
                                    {
                                        if (/*TransferenciasU == */true)
                                        {
                                            objWhcol020.Success = true;
                                            objWhcol020.SuccessMsg = Thetransferwassuccessful;
                                            objWhcol020.TipeMsgJs = "lbl";
                                        }
                                        else
                                        {
                                            objWhcol020.Error = true;
                                            objWhcol020.ErrorMsg = TransferNotUpdated;
                                            objWhcol020.TipeMsgJs = "lbl";
                                        }
                                    }
                                    else
                                    {
                                        objWhcol020.Error = true;
                                        objWhcol020.ErrorMsg = NotInserted;
                                        objWhcol020.TipeMsgJs = "lbl";
                                    }

                                }
                            }
                        }
                        else
                        {
                            objWhcol020.Error = true;
                            objWhcol020.ErrorMsg = PalletNotExist;
                            objWhcol020.TipeMsgJs = "lbl";

                        }

                    }
                    else
                    {
                        objWhcol020.Error = true;
                        objWhcol020.ErrorMsg = TargetLocationNotExist;
                        objWhcol020.TipeMsgJs = "lbl";

                    }

                }
                else
                {
                    objWhcol020.Error = true;
                    objWhcol020.ErrorMsg = CurrentNotExist;
                    objWhcol020.TipeMsgJs = "lbl";

                }
            }
            else
            {
                objWhcol020.Error = true;
                objWhcol020.ErrorMsg = LocationTransfeCannotEqual;
                objWhcol020.TipeMsgJs = "lbl";
            }

            return JsonConvert.SerializeObject(objWhcol020);

        }

        public string ListWarehouses()
        {
            DataTable DTWarehouses = Transfers.ListWarehouses();
            List<Warehouse> lstWarehouses = new List<Warehouse>();

            if (DTWarehouses.Rows.Count > 0)
            {

                foreach (DataRow item in DTWarehouses.Rows)
                {
                    Warehouse MyWareHouse = new Warehouse();
                    MyWareHouse.CWAR = item["CWAR"].ToString();
                    MyWareHouse.SLOC = item["SLOC"].ToString();
                    lstWarehouses.Add(MyWareHouse);
                }

            }

            return JsonConvert.SerializeObject(lstWarehouses);

        }

        protected void CargarIdioma()
        {
            Thetransferwassuccessful = mensajes("Thetransferwassuccessful");
            TranferQtynotenough = mensajes("TranferQtynotenough");
            PalletNotLocate = mensajes("PalletNotLocate");
            PalletNotExist = mensajes("PalletNotExist");
            PalletRfidNotExist = mensajes("PalletRfidNotExist");
            WarehouseNotExist = mensajes("WarehouseNotExist");
            WarehouseConsigment = mensajes("WarehouseConsigment");
            LocationTransfeCannotEqual = mensajes("LocationTransfeCannotEqual");
            LocationTypeMustBulK = mensajes("LocationTypeMustBulK");
            LocationBlockedTransfers = mensajes("LocationBlockedTransfers");
            TransferNotUpdated = mensajes("TransferNotUpdated");
            NotInserted = mensajes("NotInserted");
            TargetLocationNotExist = mensajes("TargetLocationNotExist");
            CurrentNotExist = mensajes("CurrentNotExist");
            PalletIdAlreadyTransferAndPendingToProcess = mensajes("PalletIdAlreadyTransferAndPendingToProcess");
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

        public class Warehouse
        {
            public string CWAR { get; set; }
            public string SLOC { get; set; }
        }

        public class TypeWarehouse
        {
            public string TYPW { get; set; }
            public bool Error { get; set; }
            public string ErrorMsg { get; set; }
            public string TipeMsgJs { get; set; }
        }
    }
}