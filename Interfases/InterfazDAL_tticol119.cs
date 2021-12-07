using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol119
    {
        tticol119 dal = new tticol119();
        
        static InterfazDAL_tticol119()
        { 
        }

        public DataTable SelectRegister(Ent_tticol119 MyObj119, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.SelectRegister(ref MyObj119, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
