using System;
using System.Data;
using System.Data.Sql;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using whusa.Entidades;
using whusa.Utilidades;

namespace whusa.DAL
{
    public class ttccol307
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
        private static string tabla = owner + ".ttccol307" + env;

        static ttccol307()
        {

        }


        public bool ActualizarUsuariotccol307(Ent_ttccol307 ObjTtccol307)
        {
            string strError = string.Empty;
            bool ActualizacionExitosa = false;

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":PICK", ObjTtccol307.PAID);
            paramList.Add(":USER", ObjTtccol307.USRR);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                ActualizacionExitosa = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [ttccol303]. Try again or contact your administrator";
                throw ex;
            }

            return ActualizacionExitosa;
        }

        public DataTable ConsultarRegistrotccol307(Ent_ttccol307 ObjTtccol307)
        {
            string strError = string.Empty;
            DataTable Consulta = new DataTable();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":PAID", ObjTtccol307.PAID.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                Consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [ttccol307]. Try again or contact your administrator";
                throw ex;
            }
            return Consulta;
        }

        public bool ActualizarTccol307(Ent_ttccol307 ObjTtccol307)
        {
            string strError = string.Empty;
            bool Resultado = false;
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$USER",ObjTtccol307.USRR_AUX);
            paramList.Add(":T$STAT",ObjTtccol307.STAT_AUX);
            paramList.Add(":T$PAID",ObjTtccol307.PAID_AUX);
            paramList.Add(":T$PROC",ObjTtccol307.PROC_AUX);
            paramList.Add(":T$CWAR",ObjTtccol307.CWAR_AUX);
            paramList.Add(":USER",ObjTtccol307.USRR);
            paramList.Add(":STAT",ObjTtccol307.STAT);
            paramList.Add(":PAID",ObjTtccol307.PAID);
            paramList.Add(":PROC",ObjTtccol307.PROC);
            paramList.Add(":CWAR",ObjTtccol307.CWAR);
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                Resultado = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [ttccol307]. Try again or contact your administrator";
                //throw ex;
            }
            return Resultado;
        }

        public DataTable ConsultarPendientesTccol307(string STAT, string CWAR)
        {
            string strError = string.Empty;
            DataTable Resultado = new DataTable();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$STAT", STAT);
            paramList.Add(":T$CWAR", CWAR);
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                Resultado = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [ttccol307]. Try again or contact your administrator";
                //throw ex;
            }
            return Resultado;
        }
    }
}



