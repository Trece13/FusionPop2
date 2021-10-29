using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using whusa.Interfases;
using System.Data;
using Newtonsoft.Json;
using whusa.Entidades;
using whusa.Utilidades;
//JC 121021 Al finalizar de desagsignar eliminar el picking si queda huerfano
using whusa;

namespace whusap.WebPages.WorkOrders
{
    public partial class UnassignPalletsFreePicked : System.Web.UI.Page
    {
        public static Ent_tticol082 MyObj = new Ent_tticol082();
        private static InterfazDAL_twhcol122 _idaltwhcol122 = new InterfazDAL_twhcol122();
        public static IntefazDAL_tticol082 Itticol082 = new IntefazDAL_tticol082();
        public static InterfazDAL_twhcol122 twhcolDAL = new InterfazDAL_twhcol122();
        //JC 121021 Al finalizar de desagsignar eliminar el picking si queda huerfano
        public static IntefazDAL_ttccol307 Ittccol307 = new IntefazDAL_ttccol307();
        public static Ent_tticol082 ObjReturn = new Ent_tticol082();

        private static string globalMessages = "GlobalMessages";

        public static string Thepickedissuccess = mensajes("Thepickedissuccess");
        public static string Thepickedisnotsuccess = mensajes("Thepickedisnotsuccess");
        public static string ThePickIDDoesntexist = mensajes("ThePickIDDoesntexist");
        public static string ThePickIDAlreadyDrop = mensajes("ThePickIDAlreadyDrop");
        public static string PalletIDnotvalidfortaketoMFG = mensajes("PalletIDnotvalidfortaketoMFG");
        public static string PalletIdAlreadyPicked = mensajes("PalletIdAlreadyPicked");
        

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string ConsultarTticol082(string PAID)
        {
            DataTable Lstticol082 = Itticol082.ConsultarPicksTticol182(PAID);
            if (Lstticol082.Rows.Count > 0)
            {
                DataRow ItemRow = Lstticol082.Rows[0];
                ObjReturn.PICK = ItemRow["PICK"].ToString();
                ObjReturn.CWAR = ItemRow["WRH"].ToString();
                ObjReturn.MCNO= ItemRow["MCN"].ToString();
                ObjReturn.QTYC = ItemRow["PALLETS"].ToString();
                ObjReturn.Error = false;
            }
            else
            {
                ObjReturn.Error = true;
                ObjReturn.ErrorMsg = "";
                ObjReturn.TipeMsgJs = "lbl";
            }

            return JsonConvert.SerializeObject(Lstticol082);
        }

        [WebMethod]
        public static string SearchPickID(string PickID)
        {
            List<Ent_tticol082> lsttticol082 = new List<Ent_tticol082>();
            DataTable TableItticol082 = Itticol082.ConsultarPalletID_x_FreePicking(PickID);
            string ObjRetorno = string.Empty;

            if (ExistenciaData(TableItticol082))
            {
                DataRow myObjDt = TableItticol082.Rows[0];
                if (MyObj.STAT == "4")
                {
                    MyObj.Error = true;
                    ObjRetorno = JsonConvert.SerializeObject(MyObj);

                }
                if (myObjDt["STAP"].ToString() == "6")
                {
                    MyObj.Error = true;
                    MyObj.TipeMsgJs = "lbl";
                    MyObj.ErrorMsg = ThePickIDAlreadyDrop;
                    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                }
                else
                {
                    foreach (DataRow row in TableItticol082.Rows)
                    {
                        Ent_tticol082 MyObj2 = new Ent_tticol082();
                        MyObj2.TBL = row["TBL"].ToString();
                        MyObj2.QTYT = row["QTYT"].ToString();
                        MyObj2.UNIT = row["UNIT"].ToString();
                        MyObj2.ITEM = row["ITEM"].ToString();
                        MyObj2.DSCA = row["DSCA"].ToString();
                        MyObj2.MCNO = row["MCNO"].ToString();
                        MyObj2.DSCAM = row["DSCAM"].ToString();
                        MyObj2.ORNO = row["ORNO"].ToString();
                        MyObj2.STAT = row["STAT"].ToString();
                        MyObj2.CWAR = row["CWAR"].ToString();
                        MyObj2.Error = false;
                        lsttticol082.Add(MyObj2);
                        ObjRetorno = JsonConvert.SerializeObject(lsttticol082);
                    }
                }
            }
            else
            {
                MyObj.Error = true;
                MyObj.TipeMsgJs = "lbl";
                MyObj.ErrorMsg = ThePickIDDoesntexist;
                ObjRetorno = JsonConvert.SerializeObject(MyObj);
            }
            return ObjRetorno;
        }

        [WebMethod]
        public static string designar(string PickID)
        {
            List<Ent_tticol082> lsttticol082 = new List<Ent_tticol082>();
            DataTable TableItticol082 = Itticol082.ConsultarPalletID_x_FreePicking(PickID);
            string ObjRetorno = string.Empty;

            if (ExistenciaData(TableItticol082))
            {
                DataRow myObjDt = TableItticol082.Rows[0];
                if (MyObj.STAT == "4")
                {
                    MyObj.Error = true;
                    ObjRetorno = JsonConvert.SerializeObject(MyObj);

                }
                if (myObjDt["STAP"].ToString() == "6")
                {
                    MyObj.Error = true;
                    MyObj.TipeMsgJs = "lbl";
                    MyObj.ErrorMsg = ThePickIDAlreadyDrop;
                    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                }
                else
                {
                    foreach (DataRow row in TableItticol082.Rows)
                    {
                        MyObj.TBL =  row["TBL"].ToString();
                        MyObj.QTYT = row["QTYT"].ToString();
                        MyObj.UNIT = row["UNIT"].ToString();
                        MyObj.ITEM = row["ITEM"].ToString();
                        MyObj.DSCA = row["DSCA"].ToString();
                        MyObj.MCNO = row["MCNO"].ToString();
                        MyObj.DSCAM = row["DSCAM"].ToString();
                        MyObj.ORNO = row["ORNO"].ToString();
                        MyObj.PONO = row["PONO"].ToString();
                        MyObj.ADVS = row["ADVS"].ToString();
                        MyObj.STAT = "3";
                        MyObj.Error = false;
                        MyObj.PICK = PickID.Trim();
                        _idaltwhcol122.UpdateTtico082StatFreeNew(MyObj);
                        twhcolDAL.Delete182(MyObj);
                        lsttticol082.Add(MyObj);
                        ObjRetorno = JsonConvert.SerializeObject(lsttticol082);
                    }
                }
            }
            else
            {
                MyObj.Error = true;
                MyObj.TipeMsgJs = "lbl";
                MyObj.ErrorMsg = ThePickIDDoesntexist;
                ObjRetorno = JsonConvert.SerializeObject(MyObj);
            }
            return ObjRetorno;          
        }

        public static bool ExistenciaData(DataTable Data)
        {
            bool ContieneDatos = false;
            if (Data.Rows.Count > 0)
            {
                ContieneDatos = true;
            }
            return ContieneDatos;
        }

        protected static string mensajes(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("DropPickedMaterialOnTunnelMFG.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }


    }
}