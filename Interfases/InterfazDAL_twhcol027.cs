using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol027
    {
        twhcol027 dal = new twhcol027();

        static InterfazDAL_twhcol027()
        {
        }

        public bool insertRegistertwhcol027(ref Ent_twhcol027 Obj027, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.insertRegistertwhcol027(ref Obj027, ref strError);
                
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
            return retorno;
        }
    }
}
