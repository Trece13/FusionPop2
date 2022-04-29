using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using whusa.Entidades;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol030
    {
        twhcol030 dal = new twhcol030();
        public bool InsertTwhcol030(Ent_ttwhcol030 ObjTwhcol030, ref string strError)
        {
            return dal.InsertTwhcol030(ObjTwhcol030, ref strError);
        }
    }
}
