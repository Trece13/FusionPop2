using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using whusa.Entidades;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol182
    {
        tticol182 dal = new tticol182();

        public InterfazDAL_tticol182()
        {
            //Constructor
        }

        public DataTable SelectRecord(ref Ent_tticol182 data, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.SelectRecord(ref data, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable ChangeStat182(ref Ent_tticol182 data, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.ChangeStat182(ref data, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable Delete182Zero()
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.Delete182Zero();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception("\nPila: " + ex.Message);
            }
        }
        

        public bool InsertarregistroItticol182(Ent_tticol182 myObj)
        {
            return dal.InsertarregistroItticol182(myObj);
        }

        public bool ActualizarRegistroItticol182(Ent_tticol182 myObj)
        {
            return dal.ActualizarRegistroItticol182(myObj);
        }

        public DataTable SelectTticol182(ref Ent_tticol182 data, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.SelectTticol182(ref data, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool ActualizarStatTticol182(ref Ent_tticol182 data, ref string strError)
        {
            bool retorno  = false;
            try
            {
                retorno = dal.ActualizarStatTticol182(ref data, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
