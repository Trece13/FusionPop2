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
        //JC 121021 Llenar el dropdown con el valor correcto del estado del pallet
        public DataTable List_StatusPallet_OriginTable(ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.List_StatusPallet_OriginTable(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
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

        public bool Actualizartwhcol131CantStatus(ref string pallet, ref int status, ref decimal qty,string CWAR = "",string LOCA = "",string LOT = "")
        {
            return dal.Actualizartwhcol131CantStatus(ref pallet, ref status, ref qty,CWAR,LOCA,LOT);
        }

        //JC 030222 Actualizar el rfid si cambió
        public bool Actualizartwhcol133Rfid(ref string pallet, string RFID)
        {
            return dal.Actualizartwhcol133Rfid(ref pallet, RFID);
        }

        public object Actualizarttdcol222Status(Ent_ttdcol137 data137)
        {
            throw new NotImplementedException();
        }

        public object Actualizarttdcol242(Ent_ttdcol137 data137)
        {
            return dal.Actualizarttdcol242(data137);
        }

        public object Actualizarttdcol222(Ent_ttdcol137 data137)
        {
            return dal.Actualizarttdcol222(data137);
        }

        public object Actualizarttdcol022Pdno(Ent_ttdcol137 data137)
        {
            return dal.Actualizarttdcol022Pdno(data137);
        }

        public object Actualizarttdcol042Pdno(Ent_ttdcol137 data137)
        {
            return dal.Actualizarttdcol042Pdno(data137);
        }
    }
}

