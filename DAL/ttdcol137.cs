using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Utilidades;
using System.Diagnostics;
using System.Reflection;
using whusa.Entidades;
using System.Data;
using System.Configuration;
using whusap.Entidades;

namespace whusa.DAL
{
    public class ttdcol137
    {
        private static Seguimiento log = new Seguimiento();
        private static StackTrace stackTrace = new StackTrace();
        private static MethodBase method;
        private static Recursos recursos = new Recursos();

        List<Ent_ParametrosDAL> parametrosIn = new List<Ent_ParametrosDAL>();
        Dictionary<string, object> paramList = new Dictionary<string, object>();
        Dictionary<string, object> parametersOut = new Dictionary<string, object>();
        String strSentencia = string.Empty;
        DataTable consulta = new DataTable();

        private static String env = ConfigurationManager.AppSettings["env"].ToString();
        private static String owner = ConfigurationManager.AppSettings["owner"].ToString();
        private static string tabla = owner + ".ttdcol137" + env;

        public int insertarDatos(ref Ent_ttdcol137 parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            paramList = new Dictionary<string, object>();

            paramList.Add(":T$PAID", parametros.Paid.Trim().ToUpper().ToString());
            paramList.Add(":T$ORNO", parametros.Orno.ToUpper().ToString());
            paramList.Add(":T$CLOT", parametros.Clot.ToString());
            paramList.Add(":T$CWAR", parametros.Cwar.ToString());
            paramList.Add(":T$LOCA", parametros.Loca.ToString());
            paramList.Add(":T$QTYA", parametros.Qtya.ToString());
            paramList.Add(":T$USER", parametros.User.ToUpper().ToString());
            paramList.Add(":T$REFCNTD", "0");
            paramList.Add(":T$REFCNTU", "0");

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                return Convert.ToInt32(retorno);
            }
            catch (Exception ex)
            {
                strError = "Error when inserting data [ttdcol137]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return Convert.ToInt32(retorno);
        }

        public DataTable vallidatePalletInfoSalesOrder(ref Ent_tticol125 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.paid.Trim().ToUpperInvariant());
            string paid = ParametrosIn.paid.Trim().ToUpperInvariant();
            string tableName = string.Empty;
            strSentencia = recursos.readStatement(method.ReflectedType.Name, "vallidatePalletInfoSalesOrder", ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol125]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }
        //JC 121021 Traer el estado del pallet de acuerdo a la tabla
        public DataTable List_StatusPallet_OriginTable(ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the get data for [Pallet Status]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        public bool Actualizarttdcol222Cant(ref string pallet, ref decimal qty)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", pallet.ToUpper());
                paramList.Add(":QTYA", qty);

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizarttdcol022Status(Ent_ttdcol137 MyObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.Paid.ToUpper());
                paramList.Add(":DELE", MyObj.Dele);

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizarttdcol242Cant(ref string pallet, ref decimal qty)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", pallet.ToUpper());
                paramList.Add(":QTYA", qty);

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizarttdcol042Status(Ent_ttdcol137 MyObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.Paid.ToUpper());
                paramList.Add(":DELE", MyObj.Dele);

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizartwhcol131CantStatus(ref string pallet, ref int status, ref decimal qty,string CWAR,string LOCA)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", pallet.ToUpper());
                paramList.Add(":QTYA", qty);
                paramList.Add(":STAT", status);
                paramList.Add(":CWAR", CWAR.ToUpper());
                paramList.Add(":LOCA", LOCA.ToUpper());

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }
   
        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_ttdcol137 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PAID", DbType.String, parametros.Paid);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ORNO", DbType.String, parametros.Orno);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.Clot);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAR", DbType.String, parametros.Cwar);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOCA", DbType.String, parametros.Loca);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTYA", DbType.String, parametros.Qtya);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$USER", DbType.String, parametros.User);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.Int64, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.Int64, parametros.refcntu);

                if (blnUsarPRetorno)
                {
                    Ent_ParametrosDAL pDal = new Ent_ParametrosDAL();
                    pDal.Name = "@p_Int_Resultado";
                    pDal.Type = DbType.Int32;
                    pDal.ParDirection = ParameterDirection.Output;
                    parameterCollection.Add(pDal);
                }
            }
            catch (Exception ex)
            {
                strError = "Error when creating parameters [137]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return parameterCollection;
        }


        public bool Actualizarttdcol242(Ent_ttdcol137 data137)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", data137.Paid.ToUpper());
                paramList.Add(":CWAT", data137.Cwar.ToUpper());
                paramList.Add(":LOCA", data137.Loca.ToUpper());

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizarttdcol222(Ent_ttdcol137 data137)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", data137.Paid.ToUpper());
                paramList.Add(":CWAT", data137.Cwar.ToUpper());
                paramList.Add(":LOCA", data137.Loca.ToUpper());
                paramList.Add(":LOT", data137.Lot.ToUpper());

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }
    }
}
