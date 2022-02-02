using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol042
    {
        tticol042 dal = new tticol042();

        static InterfazDAL_tticol042()
        {
        }

        public int insertarRegistro(ref List<Ent_tticol042> parametros, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarRegistro(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int wrapRegrind_ActualizaRegistro(ref List<Ent_tticol042> parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.wrapRegrind_ActualizaRegistro(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int insertarRegistroSimple(ref Ent_tticol042 parametros, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarRegistroSimple(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int insertarRegistroSimpleD(ref Ent_tticol042 parametros, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarRegistroSimpleD(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int InsertarRegistroTicol242(ref Ent_tticol042 data042, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.InsertarRegistroTicol242(ref data042, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int actualizaRegistro_ConfirmedRegrind(ref List<Ent_tticol042> parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.actualizaRegistro_ConfirmedRegrind(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int actualizaRegistro_LocationRegrind(ref List<Ent_tticol042> parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.actualizaRegistro_LocationRegrind(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int ActualizaRegistro_ReprintRegrind(ref List<Ent_tticol042> parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.ActualizaRegistro_ReprintRegrind(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool ActualizarUbicacionTicol242(string PDNO, string SQNB, string ACLO, string CWAR)
        {
            string strError = string.Empty;
            try
            {
                bool retorno = dal.ActualizarUbicacionTicol242(PDNO, SQNB, ACLO, CWAR);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }


        public DataTable listaCantidadRegrind(ref Ent_tticol042 ParametrosIn, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaCantidadRegrind(ref ParametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        //JC 15122021 Buscar el maximo consecutivo en todas las tablas
        public DataTable listaCantidadRegrind_Disposicion(ref Ent_tticol042 ParametrosIn, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaCantidadRegrind_Disposicion(ref ParametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistroXSQNB(ref Ent_tticol042 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistroXSQNB(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistroXSQNB_ConfirmedRegrind(ref Ent_tticol042 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistroXSQNB_ConfirmedRegrind(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistroXSQNB_FindLocation(ref Ent_tticol042 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistroXSQNB_FindLocation(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistroXSQNB_LocatedRegrind(ref Ent_tticol042 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistroXSQNB_LocatedRegrind(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable ListaRegistro_ReprintRegrind(ref Ent_tticol042 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.ListaRegistro_ReprintRegrind(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public bool insertarRegistroTticon242(ref List<Ent_tticol042> parameterCollectionRegrind, ref string strError)
        {
            bool retorno = false;
            //try
            //{
            retorno = dal.insertarRegistroTticon242(ref parameterCollectionRegrind, ref strError);
            return retorno;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(strError += "\nPila: " + ex.Message);
            //}
        }

        public bool ActualizarRegistroTticon242(ref List<Ent_tticol042> parameterCollectionRegrind, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.ActualizarRegistroTticon242(ref parameterCollectionRegrind, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool ActualizarCantidadAlmacenRegistroTicol242(string _operator, double ACQT, string ACLO, string CWAR, string PAID)
        {
            string strError = string.Empty;
            try
            {
                bool retorno = dal.ActualizarCantidadAlmacenRegistroTicol242(_operator, ACQT, ACLO, CWAR, PAID);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable SecuenciaMayor(string id)
        {
            string strError = "";
            DataTable retorno;
            try
            {
                retorno = dal.SecuenciaMayor042(id);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable ConsultaPorPalletID(ref string PDNO, ref string strError)
        {

            DataTable retorno = new DataTable();

            try
            {

                retorno = dal.ConsultarPorPalletID(ref PDNO, ref strError);

                return retorno;

            }

            catch (Exception ex)
            {

                throw new Exception(strError += "\nPila: " + ex.Message);

            }

        }

        public bool ActualizacionPalletId(string PAID, string STAT, string strError)
        {

            return dal.ActualizacionPalletId(PAID, STAT, strError);

        }

        public DataTable SecuenciaMayorRT(string id)
        {
            string strError = "";
            DataTable retorno;
            try
            {
                retorno = dal.SecuenciaMayor042RT(id);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }


        public DataTable selectTticol000(ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.selectTticol000(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        //JC 060921 Traer la bodega de regrind 
        public DataTable Warehouse_Regrind(ref string cwar, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.Warehouse_Regrind(ref cwar, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public bool ActualizarCantidadRegistroTicol242(decimal ACQT, string SQNB)
        {
            string strError = string.Empty;
            try
            {
                bool retorno = dal.ActualizarCantidadRegistroTicol242(ACQT, SQNB);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable SelectRegister(string PAID, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.SelectRegister(ref PAID, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable TakeMaterialInv_verificaBodega_Param(string CWAR, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.TakeMaterialInv_verificaBodega_Param(CWAR, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistro_ObtieneAlmacenLocation(string CWAR, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistro_ObtieneAlmacenLocation(CWAR, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistro_ObtieneLocation(string CWAR, string LOCA, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistro_ObtieneLocation(CWAR, LOCA, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }


        public DataTable invLabel_registroImprimir_Param(ref Ent_tticol042 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.invLabel_registroImprimir_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public bool UpdateMasive(Ent_tticol042 MyObj042)
        {
            string strError = string.Empty;
            try
            {
                bool retorno = dal.UpdateMasive(MyObj042);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool UpdateMasive242(Ent_tticol042 MyObj042)
        {
            string strError = string.Empty;
            try
            {
                bool retorno = dal.UpdateMasive242(MyObj042);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        //JC Actualizar de manera masiva los datos del estado y cantidad de la bodega a limpiar
        public bool UpdateMasiveStatus(Ent_tticol042 MyObj042)
        {
            string strError = string.Empty;
            try
            {
                bool retorno = dal.UpdateMasiveStatus(MyObj042);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool UpdateMasive242WrhQty(Ent_tticol042 MyObj042)
        {
            string strError = string.Empty;
            try
            {
                bool retorno = dal.UpdateMasive242WrhQty(MyObj042);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

    }
}