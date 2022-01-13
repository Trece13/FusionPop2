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
    public class tticol042
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
        private static string tabla = owner + ".tticol042" + env;
        private static string tabla2 = owner + ".tticol242" + env;

        public int insertarRegistro(ref List<Ent_tticol042> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);

                foreach (Ent_tticol042 reg in parametros)
                {
                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = "Error when inserting data [tticol042]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }

            return Convert.ToInt32(retorno);

        }


        public int insertarRegistroSimple(ref Ent_tticol042 parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);

                parametrosIn = AdicionaParametrosComunes(parametros);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);

                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }

            return Convert.ToInt32(retorno);
        }

        public int insertarRegistroSimpleD(ref Ent_tticol042 parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            paramList = new Dictionary<string, object>();

              paramList.Add(":T$PDNO", parametros.pdno.Trim().ToUpper());
              paramList.Add(":T$SQNB", parametros.sqnb.Trim().ToUpper());
              paramList.Add(":T$PROC", parametros.proc);
              paramList.Add(":T$LOGN", parametros.logn.Trim().ToUpper());
              paramList.Add(":T$MITM",  parametros.mitm.Trim().ToUpper());
              paramList.Add(":T$PONO", parametros.pono); 
              paramList.Add(":T$QTDL", parametros.qtdl); 
              paramList.Add(":T$CUNI", parametros.cuni.Trim().ToUpper());  
              paramList.Add(":T$LOG1", parametros.log1.Trim().ToUpper()); 
              paramList.Add(":T$QTD1", parametros.qtd1); 
              paramList.Add(":T$PRO1", parametros.pro1); 
              paramList.Add(":T$LOG2", parametros.log2.Trim().ToUpper());
              paramList.Add(":T$QTD2", parametros.qtd2); 
              paramList.Add(":T$PRO2", parametros.pro2);
              paramList.Add(":T$LOCA", parametros.loca.Trim().ToUpper() == string.Empty ? " " : parametros.loca.Trim().ToUpper()); 
              paramList.Add(":T$NORP", parametros.norp); 
              paramList.Add(":T$DELE", parametros.dele); 
              paramList.Add(":T$LOGD", parametros.logd.Trim().ToUpper()); 
          
            try
            {
                strSentencia = recursos.readStatement("tticol042", method.Name, ref owner, ref env, tabla2, paramList);
                log.escribirError("Sentencia SQL: " + strSentencia, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                parametrosIn = AdicionaParametrosComunes(parametros);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, null, false);

                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;
                log.escribirError(strError + Console.Out.NewLine + ex.Message + "Sentencia SQL: " + strSentencia, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return Convert.ToInt32(retorno);
        }

        public int InsertarRegistroTicol242(ref Ent_tticol042 parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            paramList = new Dictionary<string, object>();

            paramList.Add(":T$PDNO", parametros.pdno.ToUpper());
            paramList.Add(":T$SQNB", parametros.sqnb.Trim().ToUpper());
            paramList.Add(":T$URPT", parametros.urpt.ToUpper());
            paramList.Add(":T$ACQT", parametros.acqt.ToString());
            paramList.Add(":T$CWAF", parametros.cwaf.ToUpper().Trim());
            paramList.Add(":T$CWAT", parametros.cwat.ToUpper().Trim() == string.Empty ? " " : parametros.cwat.ToUpper().Trim());
            paramList.Add(":T$ACLO", parametros.aclo.ToUpper().Trim() == string.Empty ? " " : parametros.aclo.ToUpper().Trim());
            paramList.Add(":T$ALLO", parametros.allo.ToString());

            try
            {
                strSentencia = recursos.readStatement("tticol042",method.Name, ref owner, ref env, tabla2, paramList);
                log.escribirError("Sentencia SQL: " + strSentencia, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                parametrosIn = AdicionaParametrosComunes(parametros);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, null, false);

                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;
                log.escribirError(strError + Console.Out.NewLine + ex.Message + "Sentencia SQL: " + strSentencia, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return Convert.ToInt32(retorno);
        }

        public int wrapRegrind_ActualizaRegistro(ref List<Ent_tticol042> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strCondicion = string.Empty;

            try
            {
                foreach (Ent_tticol042 reg in parametros)
                {
                    paramList = new Dictionary<string, object>();
                    paramList.Add(":T$SQNB", reg.sqnb);
                    paramList.Add(":T$DELE", reg.dele);

                    strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                    if (!string.IsNullOrEmpty(strError))
                        break;
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = "Error updating data [tticol042]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return Convert.ToInt32(retorno);
        }

        public int actualizaRegistro_ConfirmedRegrind(ref List<Ent_tticol042> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strCondicion = string.Empty;

            try
            {
                foreach (Ent_tticol042 reg in parametros)
                {
                    paramList = new Dictionary<string, object>();
                    paramList.Add(":T$LOG1", reg.log1);
                    log.escribirError("My query 042: " + reg.qtd1.ToString().Trim(), method.Module.Name, method.Name, method.ReflectedType.Name);
                    paramList.Add(":T$QTD1", reg.qtd1.ToString().Trim().Contains(",") == true ? reg.qtd1.ToString().Trim().Replace(",", ".") : reg.qtd1.ToString().Trim().Replace(".", ","));
                    paramList.Add(":T$PRO1", reg.pro1);
                    paramList.Add(":T$SQNB", reg.sqnb);
                    paramList.Add(":T$DELE", reg.dele);

                    strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                    log.escribirError("My query 042: "+strSentencia, method.Module.Name, method.Name, method.ReflectedType.Name);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                    if (!string.IsNullOrEmpty(strError))
                        break;
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = "Error updating data [tticol042]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return Convert.ToInt32(retorno);
        }

        public int actualizaRegistro_LocationRegrind(ref List<Ent_tticol042> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strCondicion = string.Empty;

            try
            {
                foreach (Ent_tticol042 reg in parametros)
                {
                    paramList = new Dictionary<string, object>();
                    paramList.Add(":T$LOG2", reg.log2);
                    paramList.Add(":T$QTD2", reg.qtd2.ToString().Trim().Contains(",") == true ? reg.qtd2.ToString().Trim().Replace(",", ".") : reg.qtd2.ToString().Trim().Replace(",", "."));
                    paramList.Add(":T$PRO2", reg.pro2);
                    paramList.Add(":T$LOCA", reg.loca);
                    paramList.Add(":T$SQNB", reg.sqnb);
                    paramList.Add(":T$DELE", reg.dele);

                    strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                    if (!string.IsNullOrEmpty(strError))
                        break;
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = "Error updating data [tticol042]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return Convert.ToInt32(retorno);
        }

        public bool ActualizarUbicacionTicol242(string PDNO, string SQNB, string ACLO, string CWAR)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;
            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$PDNO", PDNO.ToUpper());
                paramList.Add(":T$SQNB", SQNB.ToUpper());
                paramList.Add(":T$ACLO", ACLO.ToUpper().Trim() == string.Empty ? " " : ACLO.ToUpper().Trim());
                paramList.Add(":T$CWAT", CWAR.ToUpper().Trim());

                strSentencia = recursos.readStatement("tticol042", method.Name, ref owner, ref env, tabla2, paramList);

                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);

                return retorno;
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return retorno;
        }


        public int ActualizaRegistro_ReprintRegrind(ref List<Ent_tticol042> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strCondicion = string.Empty;

            try
            {
                foreach (Ent_tticol042 reg in parametros)
                {
                    paramList = new Dictionary<string, object>();
                    paramList.Add(":T$NORP", reg.norp);
                    paramList.Add(":T$SQNB", reg.sqnb);

                    strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                    if (!string.IsNullOrEmpty(strError))
                        break;
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = "Error updating data [tticol042]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return Convert.ToInt32(retorno);
        }

        public DataTable listaCantidadRegrind(ref Ent_tticol042 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PDNO", ParametrosIn.pdno);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "-1"; }
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol042]. Try again or contact your administrator";
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;
                throw ex;
            }
        }

        //JC 1512221 Buscar el maximo consecutivo en todas las tablas
        public DataTable listaCantidadRegrind_Disposicion(ref Ent_tticol042 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PDNO", ParametrosIn.pdno);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "-1"; }
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol042-ticol022-whcol131]. Try again or contact your administrator";
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;
                throw ex;
            }
        }

        public DataTable listaRegistroXSQNB(ref Ent_tticol042 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            string strSecuencia = Parametros.sqnb.Trim().ToUpperInvariant();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", strSecuencia);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Not item's found for Sequence "; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol042]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

        public DataTable listaRegistroXSQNB_ConfirmedRegrind(ref Ent_tticol042 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            string strSecuencia = Parametros.sqnb.Trim().ToUpperInvariant();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", strSecuencia);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Regrind sequence doesn't exist, process cannot continue"; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol042]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

        public DataTable listaRegistroXSQNB_FindLocation(ref Ent_tticol042 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            string strSecuencia = Parametros.sqnb.Trim().ToUpperInvariant();
            string strLocation = Parametros.loca.Trim().ToUpperInvariant();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", strSecuencia);
            paramList.Add(":T$LOCA", strLocation);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Location in not a valida location. Process cannot continue"; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol042]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

        public DataTable listaRegistroXSQNB_LocatedRegrind(ref Ent_tticol042 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            string strSecuencia = Parametros.sqnb.Trim().ToUpperInvariant();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", strSecuencia);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Regrind sequence doesn't exist, process cannot continue"; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol042]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

        public DataTable ListaRegistro_ReprintRegrind(ref Ent_tticol042 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            string strSecuencia = Parametros.sqnb.Trim().ToUpperInvariant();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", strSecuencia);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Not item's found for Sequence "; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol042]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_tticol042 parametros, bool blnUsarPRetorno = false)
        {
            string param = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PDNO", DbType.String, parametros.pdno);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SQNB", DbType.String, parametros.sqnb);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PROC", DbType.String, parametros.proc);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGN", DbType.String, parametros.logn);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MITM", DbType.String, parametros.mitm);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PONO", DbType.String, parametros.pono);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTDL", DbType.Decimal, Convert.ToDecimal(parametros.qtdl).ToString("#.##0,0000"));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CUNI", DbType.String, parametros.cuni);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOG1", DbType.String, parametros.log1);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTD1", DbType.Decimal, Convert.ToDecimal(parametros.qtd1).ToString("#.##0,0000"));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PRO1", DbType.String, parametros.pro1);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOG2", DbType.String, parametros.log2);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTD2", DbType.Decimal, Convert.ToDecimal(parametros.qtd2).ToString("#.##0,0000"));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PRO2", DbType.String, parametros.pro2);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOCA", DbType.String, parametros.loca);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$NORP", DbType.String, parametros.norp);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DELE", DbType.String, parametros.dele);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGD", DbType.String, parametros.logd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.String, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.String, parametros.refcntu);

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
                throw (ex);
            }
            return parameterCollection;
        }

        public bool insertarRegistroTticon242(ref List<Ent_tticol042> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            
            try
            {
                

                foreach (Ent_tticol042 reg in parametros)
                {
                    paramList = new Dictionary<string, object>();

                    paramList.Add(":T$PDNO", reg.pdno);
                    paramList.Add(":T$SQNB", reg.sqnb);
                    //JC 240821 SAlvar los kilos con coma para que no falle en el local
                    paramList.Add(":T$ACQT", reg.qtdl);
                    //paramList.Add(":T$ACQT", reg.kilos.Replace(".",","));
                    paramList.Add(":T$CWAF", reg.cwaf);
                    //JC 060921 Traer la bodega correcta definida para bodega mrb por bodega regrind ticol008
                    //paramList.Add(":T$CWAT", reg.cwaf);
                    paramList.Add(":T$CWAT", reg.cwat);
                    paramList.Add(":T$URPT", reg.urpt);
                    paramList.Add(":T$ACLO", reg.aclo.Trim() == string.Empty ? " " : reg.aclo.Trim());
                    paramList.Add(":T$ALLO", reg.allo.ToString().Trim() == decimal.MinValue.ToString().Trim() ? "0" : reg.allo.ToString());

                    strSentencia = recursos.readStatement("tticol242", method.Name, ref owner, ref env, tabla2, paramList);
                    log.escribirError("aclo: " + reg.acqt + " Sentencia SQL 242 : " + strSentencia, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, null, false);
                }
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;
                log.escribirError(" Sentencia SQL 242 : " + strSentencia, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return retorno;
        }

        public bool ActualizarCantidadAlmacenRegistroTicol242(string _operator, double ACQT, string ACLO, string CWAR, string PAID)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;
            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$CWAR", CWAR.ToUpper().Trim());
                paramList.Add(":T$ACQT", ACQT.ToString().Contains(",") == true ? ACQT.ToString().Replace(",", ".") : ACQT.ToString().Replace(".", ","));
                paramList.Add(":T$ACLO", ACLO.ToUpper().Trim() == string.Empty ? " " : ACLO.ToUpper().Trim());
                paramList.Add(":T$SQNB", PAID.ToUpper());


                strSentencia = recursos.readStatement("tticol242", method.Name, ref owner, ref env, tabla, paramList);

                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;
                log.escribirError(" Sentencia SQL 242 : " + strSentencia, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);

            }

            return retorno;
        }

        public bool ActualizarRegistroTticon242(ref List<Ent_tticol042> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {


                foreach (Ent_tticol042 reg in parametros)
                {
                    paramList = new Dictionary<string, object>();

                    paramList.Add(":T$PDNO", reg.pdno);
                    paramList.Add(":T$SQNB", reg.sqnb);

                    paramList.Add(":T$URPT", reg.urpt);
                    paramList.Add(":T$DRPT", reg.drpt);

                    strSentencia = recursos.readStatement("tticol242", method.Name, ref owner, ref env, tabla2, paramList);

                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, null, false);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return retorno;
        }

        public DataTable SecuenciaMayor042(string id)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = "";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", id);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Not item's found for Sequence "; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol042]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable ConsultarPorPalletID(ref string PDNO, ref string strError)
        {

            consulta.Rows.Clear();

            method = MethodBase.GetCurrentMethod();



            paramList = new Dictionary<string, object>();

            paramList.Add("p1", PDNO.ToUpper());



            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);



            try
            {

                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);

                if (consulta.Rows.Count < 1) { strError = "Pallet Id not Confirmed."; }

            }

            catch (Exception ex)
            {

                strError = "Error to the search sequence [tticol042]. Try again or contact your administrator \n ";

                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);

            }

            return consulta;

        }

        public bool ActualizacionPalletId(string PAID, string STAT, string strError)
        {
            bool consulta = false;
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();

            paramList.Add("p1", PAID.Trim().ToUpper());
            paramList.Add(":STAT", STAT.Trim());



            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {

                consulta = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);

            }

            catch (Exception ex)
            {

                strError = "Error to update status (to delete) [tticol022]. Try again or contact your administrator \n ";

                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);

                Console.WriteLine(ex);

            }

            return consulta;

        }

            public DataTable SecuenciaMayor042RT(string id)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = "";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", id);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Not item's found for Sequence "; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol042]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

            public DataTable selectTticol000(ref string strError)
            {
                method = MethodBase.GetCurrentMethod();
                //string strSecuencia = Parametros.sqnb.Trim().ToUpperInvariant();

                paramList = new Dictionary<string, object>();
                //paramList.Add(":T$SQNB", strSecuencia);

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                try
                {
                    consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                    
                }
                catch (Exception ex)
                {
                    //strError = "Error to the search sequence [tticol042]. Try again or contact your administrator \n " + strSentencia;
                    log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                }
                return consulta;
            }

            //JC 060921 Traer la bodega de regrind definida por bodega MRB
            public DataTable Warehouse_Regrind(ref string cwar, ref string strError)
            {
                method = MethodBase.GetCurrentMethod();
                //string strSecuencia = Parametros.sqnb.Trim().ToUpperInvariant();

                paramList = new Dictionary<string, object>();
                paramList.Add(":T$CWAR", cwar.ToUpper());

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                try
                {
                    consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);

                }
                catch (Exception ex)
                {
                    //strError = "Error to the search sequence [tticol042]. Try again or contact your administrator \n " + strSentencia;
                    log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                }
                return consulta;
            }

            public bool ActualizarCantidadRegistroTicol242(decimal ACQT, string SQNB)
            {
                method = MethodBase.GetCurrentMethod();
                bool retorno = false;
                string strError = string.Empty;
                try
                {
                    paramList = new Dictionary<string, object>();
                    paramList.Add(":T$ACQT", ACQT);
                    paramList.Add(":T$SQNB", SQNB.ToUpper());


                    strSentencia = recursos.readStatement("tticol242", method.Name, ref owner, ref env, tabla2, paramList);

                    //retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, null, false);

                    //return retorno;
                }

                catch (Exception ex)
                {
                    strError = ex.InnerException != null ?
                        ex.Message + " (" + ex.InnerException + ")" :
                        ex.Message;

                }

                return retorno;
            }

            public DataTable SelectRegister(ref string PAID, ref string strError)
            {
                Dictionary<string, object> paramList = new Dictionary<string, object>();
                paramList.Add(":T$SQNB", PAID);

                string strSentenciaS = recursos.readStatement(method.ReflectedType.Name, "SelectRegister", ref owner, ref env, tabla, paramList);
                try
                {
                    consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentenciaS, ref parametersOut, null, true);
                }
                catch (Exception ex)
                {
                    strError = "Error al buscar informacion para imprimir [tticol042]. Try again or contact your administrator \n " + strSentencia;
                    log.escribirError(strError + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                }
                return consulta;
            }

            public DataTable TakeMaterialInv_verificaBodega_Param(string CWAR, ref string strError)
            {
                method = MethodBase.GetCurrentMethod();
                paramList = new Dictionary<string, object>();
                paramList.Add("p1", CWAR.Trim().ToUpperInvariant());

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                try
                {
                    consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                    if (consulta.Rows.Count < 1) { strError = "Wharehouse code doesn´t exist. Cannot continue"; }
                    return consulta;
                }
                catch (Exception ex)
                {
                    log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
                }
                return consulta;
            }

            public DataTable listaRegistro_ObtieneAlmacenLocation(string CWAR, ref string strError)
            {
                method = MethodBase.GetCurrentMethod();

                paramList = new Dictionary<string, object>();
                paramList.Add(":T$CWAR", CWAR.Trim());

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                try
                {
                    consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                    if (consulta.Rows.Count < 1) { strError = "Warehouse incorrect, please check."; }
                }
                catch (Exception ex)
                {
                    strError = "Error to the search sequence [twhwmd200]. Try again or contact your administrator \n ";
                    log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                }
                return consulta;

            }

            public DataTable listaRegistro_ObtieneLocation(string CWAR, string LOCA, ref string strError)
            {
                method = MethodBase.GetCurrentMethod();

                paramList = new Dictionary<string, object>();
                paramList.Add(":T$CWAR", CWAR.Trim());
                paramList.Add(":T$LOCA", LOCA.Trim());

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                try
                {
                    consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                    if (consulta.Rows.Count < 1) { strError = "Warehouse incorrect, please check."; }
                }
                catch (Exception ex)
                {
                    strError = "Error to the search sequence [twhwmd200]. Try again or contact your administrator \n ";
                    log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                }
                return consulta;

            }


            public DataTable invLabel_registroImprimir_Param(ref Ent_tticol042 Parametros, ref string strError)
            {
                method = MethodBase.GetCurrentMethod();
                string strSentenciaS = string.Empty;
                string strOrden = Parametros.pdno.Trim().ToUpperInvariant();
                string strSecuencia = Parametros.sqnb.Trim().ToUpperInvariant(); ;

                paramList = new Dictionary<string, object>();
                paramList.Add("p1", strOrden);
                paramList.Add("p2", strSecuencia);

                strSentenciaS = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                try
                {
                    consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentenciaS, ref parametersOut, null, true);
                }
                catch (Exception ex)
                {
                    strError = "Error to search information to print [tticol022]. Try again or contact your administrator \n " + strSentencia;
                    log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                }
                return consulta;
            }

            public bool UpdateMasive(Ent_tticol042 MyObj042)
            {
                method = MethodBase.GetCurrentMethod();
                bool retorno = false;
                string strError = string.Empty;
                try
                {
                    paramList = new Dictionary<string, object>();
                    paramList.Add(":T$PDNO", MyObj042.pdno);
                    paramList.Add(":T$MITM", MyObj042.mitm);
                    paramList.Add(":T$DELE", MyObj042.dele);
                    paramList.Add(":T$SQNB", MyObj042.sqnb.ToUpper());


                    strSentencia = recursos.readStatement("tticol042", method.Name, ref owner, ref env, tabla2, paramList);

                    //retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, null, false);

                    //return retorno;
                }

                catch (Exception ex)
                {
                    strError = ex.InnerException != null ?
                        ex.Message + " (" + ex.InnerException + ")" :
                        ex.Message;

                }

                return retorno;
            }

            public bool UpdateMasive242(Ent_tticol042 MyObj042)
            {
                method = MethodBase.GetCurrentMethod();
                bool retorno = false;
                string strError = string.Empty;
                try
                {
                    paramList = new Dictionary<string, object>();
                    paramList.Add(":T$CWAT", MyObj042.cwat);
                    paramList.Add(":T$ACLO", MyObj042.aclo);
                    paramList.Add(":T$ACQT", MyObj042.acqt);
                    paramList.Add(":T$SQNB", MyObj042.sqnb.ToUpper());


                    strSentencia = recursos.readStatement("tticol042", method.Name, ref owner, ref env, tabla2, paramList);

                    //retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, null, false);

                    //return retorno;
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


