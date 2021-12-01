using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_ttwhcol016
    {
        ttwhcol016 dal = new ttwhcol016();

        public InterfazDAL_ttwhcol016()
        {
        }

        public int insertarRegistro(ref List<Ent_ttwhcol016> parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarRegistro(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int ActualizarSerie_Consecutivo(ref Ent_ttwhcol016 parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.ActualizarSerie_Consecutivo(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int DesactivarSerie_Consecutivo(ref Ent_ttwhcol016 parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.DesactivarSerie_Consecutivo(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int TakeMaterialInv_verificaConsLabel_Param(ref Ent_ttwhcol016 Parametros, ref string strError)
        {
            int retorno = -1;
            //DataTable retorno;
            try
            {
                retorno = dal.TakeMaterialInv_verificaConsLabel_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        //JC 020821 Retornar consecutivo y serie
        public DataTable VerificaCons_Serie_Label(ref Ent_ttwhcol016 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.VerificaCons_Serie_Label(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable TakeMaterialInv_listaConsLabel_Param(ref Ent_ttwhcol016 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.TakeMaterialInv_listaConsLabel_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public int actualizarContadores(ref Ent_ttwhcol016 Parametros, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.actualizarContadores(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += ex.Message);
            }
        }

        public DataTable TakeMaterialInv_verificaBodega_Param(ref Ent_ttwhcol016 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.TakeMaterialInv_verificaBodega_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable TakeMaterialInv_verificaItem_Param(ref Ent_ttwhcol016 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.TakeMaterialInv_verificaItem_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable GetItemsStat(ref Ent_ttwhcol016 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.GetItemsStat(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable TakeMaterialInv_verificaZona_Param(ref Ent_ttwhcol016 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.TakeMaterialInv_verificaZona_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable TakeMaterialInv_verificaLote_Param(ref Ent_ttwhcol016 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.TakeMaterialInv_verificaLote_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

    }
}
