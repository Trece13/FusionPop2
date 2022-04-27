using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using whusa.Utilidades;
using System.Diagnostics;
using whusa.Entidades;
using System.Data;
using System.Configuration;

namespace whusa.DAL
{
    public class tticol100
    {
        private static MethodBase method;
        private static Seguimiento log = new Seguimiento();
        private static Recursos recursos = new Recursos();
        private static StackTrace stackTrace = new StackTrace();
        private static string sloc = string.Empty;
        private static string aloc = string.Empty;

        String strSentencia = string.Empty;
        List<Ent_ParametrosDAL> parametrosIn = new List<Ent_ParametrosDAL>();
        Dictionary<string, object> parametersOut = new Dictionary<string, object>();
        Dictionary<string, object> paramList = new Dictionary<string, object>();
        DataTable consulta = new DataTable();

        private static String env = ConfigurationManager.AppSettings["env"].ToString();
        private static String owner = ConfigurationManager.AppSettings["owner"].ToString();
        private static string tabla = owner + ".tticol100" + env;

        public tticol100()
        {
            //Constructor
        }

        public int insertRecord(ref Ent_tticol100 parametro, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$PDNO", parametro.pdno);
                paramList.Add(":T$PONO", parametro.pono);
                paramList.Add(":T$SEQN", parametro.seqn);
                paramList.Add(":T$MCNO", parametro.mcno);
                paramList.Add(":T$SHIF", parametro.shif.ToUpper());
                paramList.Add(":T$ITEM", parametro.item);
                paramList.Add(":T$QTYR", parametro.qtyr);
                paramList.Add(":T$CDIS", parametro.cdis);
                paramList.Add(":T$REJT", parametro.rejt);
                paramList.Add(":T$CLOT", parametro.clot);
                paramList.Add(":T$OBSE", parametro.obse);
                paramList.Add(":T$LOGR", parametro.logr);
                paramList.Add(":T$DISP", parametro.disp);
                paramList.Add(":T$LOGN", parametro.logn);
                paramList.Add(":T$PROC", parametro.proc);
                paramList.Add(":T$MESS", parametro.mess);
                paramList.Add(":T$REFCNTD", parametro.refcntd);
                paramList.Add(":T$REFCNTU", parametro.refcntu);
                paramList.Add(":T$CWAR", parametro.cwar);
                paramList.Add(":T$PAID", parametro.paid);
                paramList.Add(":T$PAIO", parametro.paio);

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                //parametrosIn = AdicionaParametrosComunes(parametro);
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

        public int ActualUpdateWarehouse_ticol222(ref Ent_tticol100 parametro, ref string strError, ref string tipo)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            var qt = parametro.qtyr;
            var plt = parametro.paid;
            plt.Substring(11, 1);
            try
            {
                paramList = new Dictionary<string, object>();
                if (tipo == "Announced")
                {
                    paramList.Add(":T$CWAT", parametro.cwar.Trim().ToUpper()); 
                }
                if (tipo == "Located")
                {
                    paramList.Add(":T$CWAT", " ");
                }
                if (tipo == "Delivered")
                {
                    paramList.Add(":T$CWAT", " ");
                }
                paramList.Add(":T$SQNB", parametro.paid.Trim().ToUpper());
                paramList.Add(":T$ACLO", " ");
                paramList.Add(":T$ACQT", parametro.qtyr);

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

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

        //JC 060921 Ajustar datos para grabar regrind
        public int ActualUpdateWarehouse_ticol242(ref Ent_tticol100 parametro, ref string strError, ref string tipo)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            var qt = parametro.qtyr;
            var plt = parametro.paid;
            plt.Substring(11, 1);
            try
            {
                paramList = new Dictionary<string, object>();
                if (tipo == "Located")
                {
                    paramList.Add(":T$CWAT", " ");
                }
  
                paramList.Add(":T$SQNB", parametro.paid.Trim().ToUpper());
                paramList.Add(":T$ACLO", " ");
                paramList.Add(":T$ACQT", parametro.qtyr);

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

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

        public int ActualUpdateStockWarehouse_ticol222(ref string tableName, ref string stockw, ref string palletId, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;


            try
            {

                if (tableName == "ticol022")
                {
                    paramList = new Dictionary<string, object>();
                    paramList.Add(":T$SWAR", stockw.Trim().ToUpper());
                    paramList.Add(":T$SQNB", palletId.Trim().ToUpper());

                    //strSentencia = "SELECT  t$cwar warehosue, t$sloc handle_locations FROM   " + owner + ".twhwmd200" + env + "  WHERE   t$cwar = '" + stockw.Trim().ToUpper() + "' ";
                    strSentencia = recursos.readStatement(method.ReflectedType.Name, "selecttwhwmd200", ref owner, ref env, tabla, paramList);
                    consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                    if (consulta.Rows.Count < 1)
                    {
                        strError = "Incorrect location, please verify.";
                    }
                    else
                    {
                        foreach (DataRow row in consulta.Rows)
                        {
                            sloc = row["handle_locations"].ToString();

                        }

                        if (sloc == "1")
                        {   //if sloc = 1  

                            // strSentencia = "SELECT  t$cwar warehouse, t$loca lcation, min(t$prio) priority FROM    " + owner + ".twhwmd300" + env + "  WHERE   t$cwar =  '" + stockw.Trim().ToUpper() + "' AND t$loct = 5 AND rownum = 1 GROUP BY t$cwar, t$loca";
                            //strSentencia = recursos.readStatement(method.ReflectedType.Name, "GetLocationStockWarehouse_whwmd300", ref owner, ref env, tabla, paramList);
                            strSentencia = recursos.readStatement(method.ReflectedType.Name, "selecttwhwmd300", ref owner, ref env, tabla, paramList);
                            consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                            foreach (DataRow row in consulta.Rows)
                            {
                                aloc = row["lcation"].ToString();

                            }

                        }
                        else
                        {
                            aloc = "  ";
                        }
                        aloc = aloc.Trim() == string.Empty ? " " : aloc.Trim();
                        paramList.Add(":ALOC", aloc);
                        //strSentencia = "UPDATE  " + owner + ".tticol222" + env + " SET T$CWAT = '" + stockw.Trim().ToUpper() + "'  ,T$ACLO='" + aloc + "'    WHERE T$SQNB = '" + palletId.Trim().ToUpper() + "'  ";
                        strSentencia = recursos.readStatement(method.ReflectedType.Name, "updatetticol222", ref owner, ref env, tabla, paramList);
                        retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                        return Convert.ToInt32(retorno);


                    }
                }
                else if (tableName == "whcol131")
                {
                    paramList = new Dictionary<string, object>();
                    paramList.Add(":T$SWAR", stockw.Trim().ToUpper());
                    paramList.Add(":T$SQNB", palletId.Trim().ToUpper());
                    //strSentencia = "select  t$cwar warehosue, t$sloc handle_locations from   " + owner + ".twhwmd200" + env + "   where   t$cwar = '" + stockw.Trim().ToUpper() + "'";
                    strSentencia = recursos.readStatement(method.ReflectedType.Name, "selecthandletwhwmd200", ref owner, ref env, tabla, paramList);
                    consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                    if (consulta.Rows.Count < 1)
                    {
                        strError = "Incorrect location, please verify.";
                    }
                    else
                    {
                        foreach (DataRow row in consulta.Rows)
                        {
                            sloc = row["handle_locations"].ToString();

                        }

                        if (sloc == "1")
                        {   //if sloc = 1  

                            //strSentencia = " select  t$cwar warehouse, t$loca lcation, min(t$prio) priority from     " + owner + ".twhwmd300" + env + " where   t$cwar =  '" + stockw.Trim().ToUpper() + "' and t$loct = 5 and rownum = 1 GROUP BY t$cwar,t$loca ";
                            strSentencia = recursos.readStatement(method.ReflectedType.Name, "selectpriowhwmd300", ref owner, ref env, tabla, paramList);
                            consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                            foreach (DataRow row in consulta.Rows)
                            {
                                aloc = row["lcation"].ToString();

                            }

                        }
                        else
                        {
                            aloc = " ";
                        }
                        aloc = aloc.Trim() == string.Empty ? "   " : aloc.Trim();
                        paramList.Add(":ACLO", aloc);
                        //strSentencia = "UPDATE  " + owner + ".twhcol131" + env + " SET T$CWAA  = '" + stockw.Trim().ToUpper() + "'  ,T$LOAA ='" + aloc + "'   WHERE T$PAID = '" + palletId.Trim().ToUpper() + "'  ";
                        strSentencia = recursos.readStatement(method.ReflectedType.Name, "updatetwhcol131", ref owner, ref env, tabla, paramList);
                        retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                        return Convert.ToInt32(retorno);

                    }

                }
                //paramList = new Dictionary<string, object>();
                //paramList.Add(":T$CWAT", parametro.cwar.Trim().ToUpper());
                //paramList.Add(":T$SQNB", parametro.paid.Trim().ToUpper());

                //strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                //retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);

                //return Convert.ToInt32(retorno);
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                   ex.Message + " (" + ex.InnerException + ")" :
                   ex.Message;
            }
            return Convert.ToInt32(retorno);
        }
        public int ActualizaRegistro_ticol022(ref Ent_tticol100 parametro, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$DELE", parametro.dele.Trim().ToUpper());
                paramList.Add(":T$PAID", parametro.paid.Trim().ToUpper());
                paramList.Add(":T$LOGN", parametro.logr.Trim().ToUpper());


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);


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

        //JC 060921 Ajustar datos para grabar y actualizar regrind
        public int ActualizaRegistro_ticol042(ref Ent_tticol100 parametro, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$DELE", parametro.dele.Trim().ToUpper());
                paramList.Add(":T$PAID", parametro.paid.Trim().ToUpper());
                paramList.Add(":T$LOGN", parametro.logr.Trim().ToUpper());

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
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

        public int ActualUpdateWarehouse_whcol131(ref Ent_tticol100 parametro, ref string strError, ref string tipo)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            var qt = parametro.qtyr;
            var plt = parametro.paid;
            plt.Substring(11, 1);

            try
            {
                paramList = new Dictionary<string, object>();
                if (tipo == "Announced")
                {
                    paramList.Add(":T$CWAA", parametro.cwar.Trim().ToUpper());
                    if (qt != 0)
                    {
                        paramList.Add(":T$STAT", "3");
                    }
                    else
                    {
                        paramList.Add(":T$STAT", "11");
                    }
                }
                else
                {
                    if (tipo == "Located")
                    {
                        paramList.Add(":T$CWAA", " ");
                        if (qt != 0)
                        {
                            paramList.Add(":T$STAT", "3");
                        }
                        else
                        {                          
                            paramList.Add(":T$STAT", "11");
                        }
                    }
                }
                paramList.Add(":T$SQNB", parametro.paid.Trim().ToUpper());     
                paramList.Add(":T$QTYA", parametro.qtyr);
                //paramList.Add(":T$STAT", "11");

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

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

        public int ActualizaRegistro_located(ref Ent_tticol100 parametro, ref string updstatus, ref string tableName, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$DELE", updstatus);
                paramList.Add(":T$PAID", parametro.paid.Trim().ToUpper());
                paramList.Add(":T$LOGN", parametro.logr.Trim().ToUpper());
                paramList.Add(":TABLENAME", "T" + tableName.Trim().ToUpper());



                if (tableName == "whcol131")
                {
                    strSentencia = recursos.readStatement(method.ReflectedType.Name, "ActualizaRegistro_located_whcol131", ref owner, ref env, tabla, paramList);
                }
                else
                {
                    strSentencia = recursos.readStatement(method.ReflectedType.Name, "ActualizaRegistro_located", ref owner, ref env, tabla, paramList);
                }

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

        //JC 100921 Actualiza Bodega cuando se retorna todo el pallet
        public int ActualizaRegistrobodegaxtabla(ref Ent_tticol100 parametro, ref string location, ref string tableName, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$CWAT", parametro.cwar.Trim().ToUpper());
                paramList.Add(":T$PAID", parametro.paid.Trim().ToUpper());
                paramList.Add(":T$ACLO", location);
                paramList.Add(":TABLENAME", "T" + tableName.Trim().ToUpper());
                if (tableName == "whcol131")
                {
                    strSentencia = recursos.readStatement(method.ReflectedType.Name, "ActualizaRegistrobodegaxtabla_whcol131", ref owner, ref env, tabla, paramList);
                }
                else
                {
                    strSentencia = recursos.readStatement(method.ReflectedType.Name, "ActualizaRegistrobodegaxtabla", ref owner, ref env, tabla, paramList);
                }
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

        public DataTable findMaxSeqnByPdno(ref string pdno, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PDNO", pdno.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol100]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        public DataTable findMaxSeqnByPdnoPono(ref string pdno, ref string pono, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PDNO", pdno.Trim().ToUpper());
            paramList.Add(":T$PONO", pono.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol100]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        public DataTable findRecordByPdnoSeqnAndPono(ref string pdno, ref string seqn, ref string comparative, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PDNO", pdno.Trim().ToUpper());
            paramList.Add(":T$SEQN", seqn.Trim().ToUpper());
            paramList.Add(":COMPARATIVE", comparative.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol100]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_tticol100 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {

                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PDNO", DbType.String, parametros.pdno.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PONO", DbType.Int32, parametros.pono);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SEQN", DbType.Int32, parametros.seqn);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MCNO", DbType.String, parametros.mcno.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SHIF", DbType.String, parametros.shif.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, parametros.item.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTYR", DbType.Double, parametros.qtyr);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CDIS", DbType.String, parametros.cdis.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REJT", DbType.Int32, parametros.rejt);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.clot.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$OBSE", DbType.String, parametros.obse.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGR", DbType.String, parametros.logr.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DISP", DbType.Int32, parametros.disp);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGN", DbType.String, parametros.logn.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PROC", DbType.Int32, parametros.proc);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MESS", DbType.String, parametros.mess.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.Int32, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.Int32, parametros.refcntu);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PAID", DbType.String, parametros.paid.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAR", DbType.String, parametros.cwar.ToUpper());

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
                strError = "Error when creating parameters [301]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return parameterCollection;
        }

        public DataTable selecthandletwhwmd200(ref Ent_tticol100 parametro, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            DataTable consulta = new DataTable();

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$SWAR", parametro.cwar.Trim().ToUpper());
                //strSentencia = "select  t$cwar warehosue, t$sloc handle_locations from   " + owner + ".twhwmd200" + env + "   where   t$cwar = '" + stockw.Trim().ToUpper() + "'";
                strSentencia = recursos.readStatement(method.ReflectedType.Name, "selecthandletwhwmd200", ref owner, ref env, tabla, paramList);
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                   ex.Message + " (" + ex.InnerException + ")" :
                   ex.Message;
            }
            return consulta;
        }

        public DataTable SelectRegister(string PAID, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            DataTable consulta = new DataTable();

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$PAID", PAID.Trim().ToUpper());
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol100]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public bool updatetticol222(ref Ent_tticol022 parametro, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$SWAR", parametro.cwaf.Trim().ToUpper());
                paramList.Add(":T$SQNB", parametro.sqnb.Trim().ToUpper());
                paramList.Add(":ALOC", parametro.aclo.ToUpper());

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);

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

        //JC 240821 Solo actualiza cantidad en la ticol222 al pallet viejo
        //public bool updatetticol222acqt(ref Ent_tticol022 parametro, ref string strError)
        //{
        //    method = MethodBase.GetCurrentMethod();
        //    bool retorno = false;

        //    try
        //    {
        //        paramList = new Dictionary<string, object>();
        //        paramList.Add(":T$ACQT", parametro.acqt);
        //        paramList.Add(":T$CWAT", parametro.cwat);
        //        paramList.Add(":T$SQNB", parametro.sqnb);

        //        strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

        //        retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);

        //        return retorno;
        //    }
        //    catch (Exception ex)
        //    {
        //        strError = ex.InnerException != null ?
        //           ex.Message + " (" + ex.InnerException + ")" :
        //           ex.Message;
        //    }
        //    return retorno;
        //}

        public bool updatetticol242(ref Ent_tticol042 parametro, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$SWAR", parametro.cwaf.Trim().ToUpper());
                paramList.Add(":T$SQNB", parametro.sqnb.Trim().ToUpper());
                paramList.Add(":ALOC", parametro.aclo.ToUpper());

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);

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

        public bool updatetwhcol131(ref Ent_twhcol130131 parametro, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$SWAR", parametro.CWAR.Trim().ToUpper());
                paramList.Add(":T$SQNB", parametro.PAID.Trim().ToUpper());
                paramList.Add(":ACLO", parametro.LOCA.ToUpper());

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);

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

        public DataTable SearchQtdlSumPaid100(string PAID)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", PAID.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                log.escribirError(Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }
    }
}
