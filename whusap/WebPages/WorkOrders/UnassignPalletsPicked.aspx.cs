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

namespace whusap.WebPages.WorkOrders
{
    public partial class UnassignPalletsPicked : System.Web.UI.Page
    {
        public static Ent_tticol082 MyObj = new Ent_tticol082();
        public static IntefazDAL_tticol082 Itticol082 = new IntefazDAL_tticol082();

        private static string globalMessages = "GlobalMessages";

        public static string Thepickedissuccess = mensajes("Thepickedissuccess");
        public static string Thepickedisnotsuccess = mensajes("Thepickedisnotsuccess");
        public static string ThePalletIDDoesntexist = mensajes("ThePalletIDDoesntexist");
        public static string ThePalletIDAlreadyDrop = mensajes("ThePalletIDAlreadyDrop");
        public static string PalletIDnotvalidfortaketoMFG = mensajes("PalletIDnotvalidfortaketoMFG");
        public static string PalletIdAlreadyPicked = mensajes("PalletIdAlreadyPicked");
        

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string ClickDropTagPick(string PickID)
        {
            DataTable TableItticol082 = Itticol082.ConsultarPalletIDOnTunnelTticol083(PickID);
            string ObjRetorno = string.Empty;

            if (ExistenciaData(TableItticol082))
            {
                foreach (DataRow myObjDt in TableItticol082.Rows)
                {
                //DataRow myObjDt = TableItticol082.Rows[0];
                MyObj.TBL = myObjDt["TBL"].ToString();
                MyObj.PAID = myObjDt["PAID"].ToString();
                MyObj.QTYT = myObjDt["QTYT"].ToString();
                MyObj.UNIT = myObjDt["UNIT"].ToString();
                MyObj.ITEM = myObjDt["ITEM"].ToString();
                MyObj.DSCA = myObjDt["DSCA"].ToString();
                MyObj.STAP = myObjDt["STAP"].ToString();

                bool ActalizacionExitosa = false;
                switch (MyObj.TBL)
                {
                    case "ticol022":
                        if (MyObj.STAP.ToString().Trim() == "11")
                        {
                            MyObj.Error = true;
                            MyObj.TipeMsgJs = "alert";
                            //MyObj.ErrorMsg = PalletIdAlreadyPicked;

                            //ObjRetorno = JsonConvert.SerializeObject(MyObj);
                            //return ObjRetorno;
                        }
                        ActalizacionExitosa = Itticol082.Actualizartticol022MFG(MyObj);
                        Itticol082.Actualizartticol082MFG(MyObj);                        
                        break;
                    case "whcol130":
                        if (MyObj.STAP.ToString().Trim() == "9")
                        {
                            MyObj.Error = true;
                            MyObj.TipeMsgJs = "alert";
                            //MyObj.ErrorMsg = PalletIdAlreadyPicked;

                            //ObjRetorno = JsonConvert.SerializeObject(MyObj);
                            //return ObjRetorno;
                        }
                        ActalizacionExitosa = Itticol082.Actualizartwhcol130MFG(MyObj);
                        Itticol082.Actualizartticol082MFG(MyObj);                        
                        break;
                    case "whcol131":
                        if (MyObj.STAP.ToString().Trim() == "9")
                        {
                            MyObj.Error = true;
                            MyObj.TipeMsgJs = "alert";
                            //MyObj.ErrorMsg = PalletIdAlreadyPicked;

                            //ObjRetorno = JsonConvert.SerializeObject(MyObj);
                            //return ObjRetorno;
                        }
                        ActalizacionExitosa = Itticol082.Actualizartwhcol131MFG(MyObj);
                        Itticol082.Actualizartticol082MFG(MyObj);                        

                        break;
                    case "ticol042":
                        if (MyObj.STAP.ToString().Trim() == "11")
                        {
                            MyObj.Error = true;
                            MyObj.TipeMsgJs = "alert";
                            //MyObj.ErrorMsg = PalletIdAlreadyPicked;

                            //ObjRetorno = JsonConvert.SerializeObject(MyObj);
                            //return ObjRetorno;
                        }
                        ActalizacionExitosa = Itticol082.Actualizartticol042MFG(MyObj);
                        Itticol082.Actualizartticol082MFG(MyObj);                        
                        break;
                }
                if (ActalizacionExitosa)
                {
                    MyObj.Error = false;
                    MyObj.TipeMsgJs = "alert";
                    MyObj.SuccessMsg = Thepickedissuccess;
                    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                }
                else
                {
                    MyObj.Error = true;
                    MyObj.TipeMsgJs = "alert";
                    MyObj.ErrorMsg = Thepickedisnotsuccess;
                    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                }
            }
        }
            else
            {
                MyObj.Error = true;
                MyObj.TipeMsgJs = "alert";
                MyObj.ErrorMsg = ThePalletIDDoesntexist;

                ObjRetorno = JsonConvert.SerializeObject(MyObj);
            }

            return ObjRetorno;
        }

        [WebMethod]
        public static string SearchPickID(string PickID)
        {
            DataTable TableItticol082 = Itticol082.ConsultarPalletID_x_Picking(PickID);
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
                    MyObj.ErrorMsg = ThePalletIDAlreadyDrop;
                    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                }
                else
                {
                    MyObj.TBL = myObjDt["TBL"].ToString();
                    MyObj.PAID = myObjDt["PAID"].ToString();
                    MyObj.QTYT = myObjDt["QTYT"].ToString();
                    MyObj.UNIT = myObjDt["UNIT"].ToString();
                    MyObj.ITEM = myObjDt["ITEM"].ToString();
                    MyObj.DSCA = myObjDt["DSCA"].ToString();
                    MyObj.MCNO = myObjDt["MCNO"].ToString();
                    MyObj.DSCAM = myObjDt["DSCAM"].ToString();
                    MyObj.ORNO = myObjDt["ORNO"].ToString();
                    MyObj.STAT = myObjDt["STAT"].ToString();
                    MyObj.Error = false;
                    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                }
                //if (VerificarStatPortabla(MyObj.TBL, MyObj.STAT))
                //{
                //    MyObj.Error = false;

                //    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                //}
                //else
                //{
                //    MyObj.Error = true;
                //    MyObj.TipeMsgJs = "lbl";
                //    MyObj.ErrorMsg = PalletIDnotvalidfortaketoMFG;
                //    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                //}

            }
            else
            {
                MyObj.Error = true;
                MyObj.TipeMsgJs = "lbl";
                MyObj.ErrorMsg = ThePalletIDDoesntexist;
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

        public static bool VerificarStatPortabla(string tabla, string estado)
        {
            bool retorno = false;
            switch (tabla)
            {
                case "ticol022":
                    if (estado == "11") { retorno = true; } else { retorno = false; };
                    break;
                case "ticol042":
                    if (estado == "11") { retorno = true; } else { retorno = false; };

                    break;
                case "whcol130":
                    if (estado == "9") { retorno = true; } else { retorno = false; };
                    break;
                case "whcol131":
                    if (estado == "9") { retorno = true; } else { retorno = false; };

                    break;
            }
            return retorno;
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