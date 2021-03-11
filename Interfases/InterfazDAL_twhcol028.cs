using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol028
    {
        twhcol028 dal = new twhcol028();

        static InterfazDAL_twhcol028()
        {
        }

        public bool insertRegistertwhcol028(ref Ent_twhcol028 Obj028, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.insertRegistertwhcol028(ref Obj028, ref strError);

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
            return retorno;
        }
    }
}
