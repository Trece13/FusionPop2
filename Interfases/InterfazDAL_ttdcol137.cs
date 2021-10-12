using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;
using whusa.Entidades;

namespace whusa.Interfases
{
    public class InterfazDAL_ttdcol137
    {
        ttdcol137 dal = new ttdcol137();

        public int insertarDatos(ref Ent_ttdcol137 parametros, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarDatos(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += ex.Message);
            }
        }

        public DataTable vallidatePalletInfoSalesOrder(ref Ent_tticol125 parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.vallidatePalletInfoSalesOrder(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public bool Actualizarttdcol222Cant(ref string pallet, ref decimal qty)
        {
            return dal.Actualizarttdcol222Cant(ref pallet, ref qty);
        }

        public bool Actualizarttdcol022Status(Ent_ttdcol137 myObj)
        {
            return dal.Actualizarttdcol022Status(myObj);
        }

        public bool Actualizarttdcol242Cant(ref string pallet, ref decimal qty)
        {
            return dal.Actualizarttdcol242Cant(ref pallet, ref qty);
        }

        public bool Actualizarttdcol042Status(Ent_ttdcol137 myObj)
        {
            return dal.Actualizarttdcol042Status(myObj);
        }

        public bool Actualizartwhcol131CantStatus(ref string pallet, ref int status, ref decimal qty,string CWAR = "",string LOCA = "")
        {
            return dal.Actualizartwhcol131CantStatus(ref pallet, ref status, ref qty,CWAR,LOCA);
        }

        public object Actualizarttdcol222Status(Ent_ttdcol137 data137)
        {
            throw new NotImplementedException();
        }

        public object Actualizarttdcol242(Ent_ttdcol137 data137)
        {
            return dal.Actualizarttdco242(data137);
        }

        public object Actualizarttdcol222(Ent_ttdcol137 data137)
        {
            return dal.Actualizarttdcol222(data137);
        }
    }
}

