using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using whusa.Entidades;
using System.Diagnostics;
using whusa.Utilidades;
using System.Reflection;

namespace whusa.DAL
{
    public class tticol182
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
        private static string tabla = owner + ".tticol182" + env;

        public tticol182() 
        {
            //Constructor
        }

        public DataTable Delete182Zero()
        {
            method = MethodBase.GetCurrentMethod();
            DataTable retorno = new DataTable();

            try
            {
                paramList = new Dictionary<string, object>();

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);

                return retorno;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return retorno;
        }


        public DataTable ChangeStat182(ref Ent_tticol182 data, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            DataTable retorno = new DataTable();

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$PICK", data.PICK.Trim());
                paramList.Add(":T$STAT", data.STAT.Trim());
                paramList.Add(":T$LOGN", data.LOGN.Trim());
                paramList.Add(":T$ORNO", data.ORNO.Trim());
                paramList.Add(":T$PONO", data.PONO.Trim());
                paramList.Add(":T$ADVS", data.ADVS.Trim());

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);

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


        public DataTable SelectRecord(ref Ent_tticol182 data, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            DataTable retorno = new DataTable();

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$CWAR", data.CWAR.Trim());

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);

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

        public bool InsertarregistroItticol182(Entidades.Ent_tticol182 Objtticol182)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$OORG", Objtticol182.OORG.Trim() == "" ? "0" : Objtticol182.OORG);
                paramList.Add(":T$ORNO", Objtticol182.ORNO);
                //paramList.Add(":T$OSET", Objtticol182.OSET);
                paramList.Add(":T$PONO", Objtticol182.PONO.Trim() == "" ? "0" : Objtticol182.PONO);
                //paramList.Add(":T$SQNB", Objtticol182.SQNB);
                paramList.Add(":T$ADVS", Objtticol182.ADVS.Trim() == "" ? "0" : Objtticol182.ADVS);

                paramList.Add(":T$ITEM", Objtticol182.ITEM);
                paramList.Add(":T$STAT", Objtticol182.STAT);
                paramList.Add(":T$QTYT", Objtticol182.QTYT);
                paramList.Add(":T$CWAR", Objtticol182.CWAR);
                paramList.Add(":T$UNIT", Objtticol182.UNIT);
                paramList.Add(":T$PRIO", Objtticol182.PRIO.Trim() == "" ? "0" : Objtticol182.PRIO);
                paramList.Add(":T$PAID", Objtticol182.PAID);
                paramList.Add(":T$LOGN", Objtticol182.LOGN);
                paramList.Add(":T$MCNO", Objtticol182.MCNO);
                paramList.Add(":T$PICK", Objtticol182.PICK);
                paramList.Add(":T$LOCA", Objtticol182.LOCA);
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool ActualizarRegistroItticol182(Entidades.Ent_tticol182 Objtticol182)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$QTYT", Objtticol182.QTYT);
                paramList.Add(":T$CWAR", Objtticol182.CWAR);
                paramList.Add(":T$PAID", Objtticol182.PAID);
                paramList.Add(":T$PICK", Objtticol182.PICK);
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public DataTable SelectTticol182(ref Ent_tticol182 data, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            DataTable retorno = new DataTable();

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$STAT", data.STAT);
                paramList.Add(":T$STAB", 3);
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool ActualizarStatTticol182(ref Ent_tticol182 data, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$ORNO", data.ORNO);
                paramList.Add(":T$PONO", data.PONO);
                paramList.Add(":T$ADVS", data.ADVS);
                paramList.Add(":T$PICK", data.PICK);
                paramList.Add(":T$STAT", data.STAT);
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
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
