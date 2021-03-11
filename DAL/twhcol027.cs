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
    public class twhcol027
    {
        private static MethodBase method;
        private static Seguimiento log = new Seguimiento();
        private static Recursos recursos = new Recursos();
        private static StackTrace stackTrace = new StackTrace();

        String strSentencia = string.Empty;
        List<Ent_ParametrosDAL> parametrosIn = new List<Ent_ParametrosDAL>();
        Dictionary<string, object> parametersOut = new Dictionary<string, object>();
        Dictionary<string, object> paramList = new Dictionary<string, object>();
        DataTable consulta = new DataTable();

        private static String env = ConfigurationManager.AppSettings["env"].ToString();
        private static String owner = ConfigurationManager.AppSettings["owner"].ToString();
        private static string tabla = owner + ".twhcol027" + env;



        public bool insertRegistertwhcol027(ref Ent_twhcol027 Obj027, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$PAID", Obj027.PAID.Trim().ToUpper());
                paramList.Add(":T$PAID", Obj027.PAID.Trim().ToUpper());
                paramList.Add(":T$CDIS", Obj027.CDIS.Trim().ToUpper());
                paramList.Add(":T$EMNO", Obj027.EMNO.Trim().ToUpper());
                paramList.Add(":T$SITM", Obj027.SITM.Trim().ToUpper());
                paramList.Add(":T$SWAR", Obj027.SWAR.Trim().ToUpper());
                paramList.Add(":T$SLOC", Obj027.SLOC.Trim().ToUpper());
                paramList.Add(":T$SLOT", Obj027.SLOT.Trim().ToUpper());
                paramList.Add(":T$SQTY", Obj027.SQTY.Trim().ToUpper());
                paramList.Add(":T$TITM", Obj027.TITM.Trim().ToUpper());
                paramList.Add(":T$TWAR", Obj027.TWAR.Trim().ToUpper());
                paramList.Add(":T$TLOC", Obj027.TLOC.Trim().ToUpper());
                paramList.Add(":T$TLOT", Obj027.TLOT.Trim().ToUpper());
                paramList.Add(":T$TQTY", Obj027.TQTY.Trim().ToUpper());
                paramList.Add(":T$LOGN", Obj027.LOGN.Trim().ToUpper());
                paramList.Add(":T$DATR", Obj027.DATR.Trim().ToUpper());
                paramList.Add(":T$PROC", Obj027.PROC.Trim().ToUpper());
                paramList.Add(":T$SORN", Obj027.SORN.Trim().ToUpper());
                paramList.Add(":T$SPON", Obj027.SPON.Trim().ToUpper());
                paramList.Add(":T$TORN", Obj027.TORN.Trim().ToUpper());
                paramList.Add(":T$TPON", Obj027.TPON.Trim().ToUpper());
                paramList.Add(":T$MESS", Obj027.MESS.Trim().ToUpper());
                paramList.Add(":T$REFCNTD", Obj027.REFCNTD.Trim().ToUpper());
                paramList.Add(":T$REFCNTU", Obj027.REFCNTD.Trim().ToUpper());


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, null, false);
                return retorno;
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
