using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using System.Web.Services;
using whusa.Entidades;
using System.Data;
using whusa;
using Newtonsoft.Json;
using whusa.Utilidades;

namespace whusap.WebPages.WorkOrders
{
    public partial class EditPick : System.Web.UI.Page
    {
        public static IntefazDAL_ttccol307 Ittccol307 = new IntefazDAL_ttccol307();
        public static InterfazDAL_ttccol300 Ittccol300 = new InterfazDAL_ttccol300();
        private static InterfazDAL_tticol182 _idaltticol182 = new InterfazDAL_tticol182();
        public static Ent_ttccol307 ObjReturn = new Ent_ttccol307();

        private static string globalMessages = "GlobalMessages";
        public static string ThepalletIDdoestexist = mensajes("ThepalletIDdoestexist");
        public static string Thecurrentusercannotbethesameastheprevioususer = mensajes("Thecurrentusercannotbethesameastheprevioususer");
        public static string Updtatesuccessfull = mensajes("Updtatesuccessfull");
        public static string Updtatenotsuccessfull = mensajes("Updtatenotsuccessfull");
        public static string Theusertoupdatenotexist = mensajes("Theusertoupdatenotexist");
        public string strError;

        protected void Page_Load(object sender, EventArgs e)
        {
            Ent_ttccol301 data = new Ent_ttccol301()
            {
                user = HttpContext.Current.Session["user"].ToString(),
                come = this.GetType().BaseType.Name,                
                refcntd = 0,
                refcntu = 0
            };

            List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
            datalog.Add(data);

            new InterfazDAL_ttccol301().insertarRegistro(ref datalog, ref strError);
        }

        [WebMethod]
        public static string ConsultarTtccol307(string PAID)
        {
            Ent_tticol182 MyObj182 = new Ent_tticol182();
            MyObj182.STAT = "5";
            string strError = string.Empty;
            DataTable DTtticol182 = _idaltticol182.SelectTticol182(ref MyObj182, ref strError);
            if (DTtticol182.Rows.Count > 0)
            {
                DataRow ItemRow = DTtticol182.Rows[0];
                ObjReturn.PAID = ItemRow["PICK"].ToString();
                ObjReturn.STAT = ItemRow["STAA"].ToString();
                ObjReturn.STAT = ItemRow["STATUS"].ToString();
                ObjReturn.Error = false;
            }
            else
            {
                ObjReturn.Error = true;
                ObjReturn.ErrorMsg = ThepalletIDdoestexist;
                ObjReturn.TypeMsgJs = "lbl";
            }

            return JsonConvert.SerializeObject(DTtticol182);
        }

        [WebMethod]
        public static string ActualizarUsuarioTtccol307(string PICK, string ORNO, string PONO, string ADVS)
        {
            string strError = string.Empty;
            Ent_tticol182 MyObj182 = new Ent_tticol182();
            MyObj182.STAT = "1";
            MyObj182.ORNO = ORNO;
            MyObj182.PONO = PONO;
            MyObj182.ADVS = ADVS;
            MyObj182.PICK = PICK;
            bool ActualizacionExitosa = _idaltticol182.ActualizarStatTticol182(ref MyObj182, ref strError);
                if (ActualizacionExitosa)
                {
                    ObjReturn.Error = false;
                    ObjReturn.SuccessMsg = Updtatesuccessfull;
                    ObjReturn.TypeMsgJs = "lbl";
                }
                else if (!ActualizacionExitosa)
                {
                    ObjReturn.Error = true;
                    ObjReturn.ErrorMsg = Updtatenotsuccessfull;
                    ObjReturn.TypeMsgJs = "lbl";
                }

            return JsonConvert.SerializeObject(ObjReturn);
        }

        private static bool ConsulrarExistenciaUsuario(Ent_ttccol300 ttccol300)
        {
            bool ExisteUsuario = false;
            string StrError = string.Empty;
            DataTable ListaUsuarios = Ittccol300.listaRegistro_Param(ref ttccol300, ref StrError);
            if (ListaUsuarios.Rows.Count > 0) { ExisteUsuario = true; } else { ExisteUsuario = false; };
            return ExisteUsuario;
        }

        protected static string mensajes(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("EditUser.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }

    }
}