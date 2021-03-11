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
    public class twhcol028
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



        public bool insertRegistertwhcol028(ref Ent_twhcol028 Obj028, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$PAID", Obj028.PAID.Trim().ToUpper());
                paramList.Add(":T$PAID", Obj028.PAID.Trim().ToUpper());
                paramList.Add(":T$CDIS", Obj028.CDIS.Trim().ToUpper());
                paramList.Add(":T$EMNO", Obj028.EMNO.Trim().ToUpper());
                paramList.Add(":T$SITM", Obj028.SITM.Trim().ToUpper());
                paramList.Add(":T$SWAR", Obj028.SWAR.Trim().ToUpper());
                paramList.Add(":T$SLOC", Obj028.SLOC.Trim().ToUpper());
                paramList.Add(":T$SLOT", Obj028.SLOT.Trim().ToUpper());
                paramList.Add(":T$SQTY", Obj028.SQTY.Trim().ToUpper());
                paramList.Add(":T$TITM", Obj028.TITM.Trim().ToUpper());
                paramList.Add(":T$TWAR", Obj028.TWAR.Trim().ToUpper());
                paramList.Add(":T$TLOC", Obj028.TLOC.Trim().ToUpper());
                paramList.Add(":T$TLOT", Obj028.TLOT.Trim().ToUpper());
                paramList.Add(":T$TQTY", Obj028.TQTY.Trim().ToUpper());
                paramList.Add(":T$LOGN", Obj028.LOGN.Trim().ToUpper());
                paramList.Add(":T$DATR", Obj028.DATR.Trim().ToUpper());
                paramList.Add(":T$PROC", Obj028.PROC.Trim().ToUpper());
                paramList.Add(":T$SORN", Obj028.SORN.Trim().ToUpper());
                paramList.Add(":T$SPON", Obj028.SPON.Trim().ToUpper());
                paramList.Add(":T$TORN", Obj028.TORN.Trim().ToUpper());
                paramList.Add(":T$TPON", Obj028.TPON.Trim().ToUpper());
                paramList.Add(":T$MESS", Obj028.MESS.Trim().ToUpper());
                paramList.Add(":T$REFCNTD", Obj028.REFCNTD.Trim().ToUpper());
                paramList.Add(":T$REFCNTU", Obj028.REFCNTD.Trim().ToUpper());


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
