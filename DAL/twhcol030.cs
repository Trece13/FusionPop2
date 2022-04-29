using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using whusa.Entidades;
using whusa.Utilidades;

namespace whusa.DAL
{
    public class twhcol030
    {
        private static Seguimiento log = new Seguimiento();
        private static StackTrace stackTrace = new StackTrace();
        private static MethodBase method;
        private static Recursos recursos = new Recursos();

        List<Ent_ParametrosDAL> parametrosIn = new List<Ent_ParametrosDAL>();
        Dictionary<string, object> paramList = new Dictionary<string, object>();
        Dictionary<string, object> parametersOut = new Dictionary<string, object>();
        String strSentencia = string.Empty;

        private static String env = ConfigurationManager.AppSettings["env"].ToString();
        private static String owner = ConfigurationManager.AppSettings["owner"].ToString();
        private static string tabla = owner + ".twhcol030" + env;

        public bool InsertTwhcol030(Entidades.Ent_ttwhcol030 ObjTwhcol030, ref string strError)
        {
            bool retorno = false;
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CWOR", ObjTwhcol030.CWOR);
            paramList.Add(":T$CWDE", ObjTwhcol030.CWDE);
            paramList.Add(":T$ITEM", ObjTwhcol030.ITEM);
            paramList.Add(":T$QTDL", ObjTwhcol030.QTDL);
            paramList.Add(":T$CUNI", ObjTwhcol030.CUNI);
            paramList.Add(":T$RCNO", ObjTwhcol030.RCNO);
            paramList.Add(":T$DATE", ObjTwhcol030.DATE);
            //paramList.Add(":T$MESS", ObjTwhcol030.MESS);
            paramList.Add(":T$USER", ObjTwhcol030.USER);
            //paramList.Add(":T$REFCNTD", "0");
            //paramList.Add(":T$REFCNTU", "0");
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
            }

            catch (Exception ex)
            {
                strError = "Error: " + ex.Message + " " + strSentencia + " ";
            }
            return retorno;
        }
    }
}
