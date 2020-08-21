using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;


namespace whusa.Interfases
{
    public class InterfazDAL_ttccol303
    {
        ttccol303 dal = new ttccol303();

        public InterfazDAL_ttccol303()
        {
        }

        public DataTable listaRegistrosMenu_Param(ref Ent_ttccol300 ParametrosIn, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistrosMenu_Param(ref ParametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
