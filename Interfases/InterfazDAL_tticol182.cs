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

    }
}
