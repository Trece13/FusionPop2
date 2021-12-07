using System;
using System.Data;
using System.Data.Sql;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Reflection;
using whusa.Entidades;
using whusa.Utilidades;
using System.Diagnostics;


namespace whusa.DAL
{
    public class tticol119
    {
        private static Utilidades.Seguimiento log = new Seguimiento();
        private static StackTrace stackTrace = new StackTrace();
        private static MethodBase method = MethodBase.GetCurrentMethod();
        private static string metodo = method.Name;
        private static Recursos recursos = new Recursos();

        private static string env = ConfigurationManager.AppSettings["env"].ToString();
        private static string owner = ConfigurationManager.AppSettings["owner"].ToString();

        List<Ent_ParametrosDAL> parametrosIn = new List<Ent_ParametrosDAL>();
        Dictionary<string, object> parametersOut = new Dictionary<string, object>();
        String strSentencia = string.Empty;
        DataTable consulta = new DataTable();
        string tabla = owner + ".tticol119" + env;
        String strSQL = string.Empty;

        public DataTable SelectRegister(ref Ent_tticol119 Obj119, ref string strError)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", Obj119.paid);
            paramList.Add(":T$CWAR", Obj119.cwar);
            paramList.Add(":T$ITEM", Obj119.item);
            paramList.Add(":T$CLOT", Obj119.clot);
            paramList.Add(":DATEI", Obj119.dati);
            paramList.Add(":DATEF", Obj119.datf);

            string strSentenciaS = recursos.readStatement(method.ReflectedType.Name, "SelectRegister", ref owner, ref env, tabla, paramList);
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentenciaS, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error al buscar informacion para imprimir [tticol119]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }
    }
}









