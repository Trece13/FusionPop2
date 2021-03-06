﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Utilidades;
using System.Diagnostics;
using System.Reflection;
using whusa.Entidades;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using whusa;

namespace whusa.Interfases
{
    public class tticol074
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
        private static string tabla = "tticol074";


        public int insertarRegistro(ref List<Ent_tticol074> parametros, ref string strError)
        {
            String dato = "";
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);


                foreach (Ent_tticol074 reg in parametros)
                {
                    try
                    {
                        parametrosIn = AdicionaParametrosComunes(reg);
                        retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                    }
                    catch (Exception ex)
                    {
                        /**
                         * Se Adiciona esta parte para controlar los errores en la insesión en este momento 
                         * solo se controla la llave duplicada que esta generando problemas, se toma el 
                         * registro y se actualiza, teniendo en cuenta que eso sucede cuando el dato anterior 
                         * tiene cantidad (0) cero LRM 03/01/2018
                         **/
                        dato = ex.Message.Substring(0, 9).Trim();

                        switch (dato)
                        {
                            case "ORA-00001":
                                //MessageBox.Show("Error attempting to insert duplicate data.");
                                paramList = new Dictionary<string, object>();
                                paramList.Add("p1", "'" + reg.orden + "'");
                                paramList.Add("p2", reg.fecha.ToString("MM/dd/yyyy HH:mm:ss"));
                                paramList.Add("p3", reg.fecha.ToString("MM/dd/yyyy HH:mm:ss"));

                                strSentencia = recursos.readStatement(method.ReflectedType.Name, "actualizarRegistro", ref owner, ref env, tabla, paramList);
                                parametrosIn = AdicionaParametrosComunes(reg);
                                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                                strSentencia = recursos.readStatement(method.ReflectedType.Name, "insertarRegistro", ref owner, ref env, tabla);

                                break;
                            case "ORA-12545":
                                //MessageBox.Show("The database is unavailable.");
                                break;
                            default:
                                //MessageBox.Show("Database error: " + ex.Message.ToString());
                                break;
                        }
                    }
                }
                return Convert.ToInt32(retorno);
            }

            catch (SqlException ex)
            {
                strError = "Error when inserting data [tticol074]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return Convert.ToInt32(retorno);
        }

        public int eliminarRegistro(string orden, DateTime dtStart, DateTime dtEnd, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", orden);
            paramList.Add("p2", dtStart.ToString("MM/dd/yyyy HH:mm"));
            paramList.Add("p3", dtEnd.ToString("MM/dd/yyyy HH:mm"));

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
                return Convert.ToInt32(retorno);
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol074]. Try again or contact your administrator";
                throw ex;
            }
        }

        public DataTable ObtenerConsolidado(string ordenes, DateTime dtStart, DateTime dtEnd, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ordenes);
            paramList.Add("p2", dtStart.ToString("MM/dd/yyyy HH:mm"));
            paramList.Add("p3", dtEnd.ToString("MM/dd/yyyy HH:mm"));

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "-1"; }
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol074]. Try again or contact your administrator";
                throw ex;
            }
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_tticol074 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.Int32, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.Int32, parametros.refcntu);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CHEC", DbType.Int32, parametros.chequeado);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$COMM", DbType.String, parametros.comentario);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWTT", DbType.String, parametros.tarifaHoraria);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DATE", DbType.String, parametros.fecha.ToString("dd/MM/yyyy HH:mm:ss"));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$HREA", DbType.Decimal, parametros.horas);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGN", DbType.String, parametros.usuario);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MESS", DbType.String, parametros.mensaje);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$OPNO", DbType.Int32, parametros.numOperacion);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PDNO", DbType.String, parametros.orden);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PROC", DbType.Int32, parametros.procesado);

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
                strError = "Error when creating parameters [074]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return parameterCollection;
        }
    }
}
