﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using whusa.Entidades;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol122
    {
        twhcol122 dal = new twhcol122();

        public InterfazDAL_twhcol122()
        {
            //Constructor
        }

        public bool insertarRegistro(ref Ent_twhcol122 parametrosIn, ref string strError)
        {
            //int retorno = -1;
            //try
            //{
                return dal.insertarRegistro(ref parametrosIn, ref strError);
                
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(strError += "\nPila: " + ex.Message);
            //}
        }

        public DataTable validarRegistroByPalledId(ref string palletId, ref string uniqueId, ref string bodegaori, ref string bodegades, string strError) 
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validarRegistroByPalletId(ref palletId, ref uniqueId, ref bodegaori, ref bodegades, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable validarRegistroByUniqueId(ref string uniqueId, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validarRegistroByUniqueId(ref uniqueId, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable buscarbodegasuid(ref string uniqueId, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.buscarbodegasuid(ref uniqueId, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        //inicio

        public List<EntidadPicking> ConsultarPalletPicking22(string PAID, string UrlBaseBarcode, string USER)
        {
            List<EntidadPicking> Lstwhcol122 = new List<EntidadPicking>();
            DataTable DTwhcol122 = dal.ConsultarPalletPicking22(PAID, USER);
            if (DTwhcol122.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcol122.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {

                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        CNPK = MyRow["CNPK"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        ROWN = MyRow["ROWN"].ToString(),
                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        //OSET = MyRow["OSET"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        //SQNB = MyRow["SQNB"].ToString(),
                        ADVS = MyRow["ADVS"].ToString(),
                        QTYT = MyRow["QTYT"].ToString(),

                    };

                    Lstwhcol122.Add(MyObjet);
                }

            }
            return Lstwhcol122;
        }

        public List<EntidadPicking> ConsultarPalletPicking042(string PAID, string UrlBaseBarcode, string USER)
        {
            List<EntidadPicking> Lstwhcol042 = new List<EntidadPicking>();
            DataTable DTwhcolo42 = dal.ConsultarPalletPicking042(PAID, USER);
            if (DTwhcolo42.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcolo42.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {


                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        CNPK = MyRow["CNPK"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        ROWN = MyRow["ROWN"].ToString(),
                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        //OSET = MyRow["OSET"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        //SQNB = MyRow["SQNB"].ToString(),
                        ADVS = MyRow["ADVS"].ToString(),

                    };

                    Lstwhcol042.Add(MyObjet);
                }

            }
            return Lstwhcol042;
        }

        
        public List<EntidadPicking> ConsultarPalletPicking131(string PAID, string UrlBaseBarcode, string USER)
        {
            List<EntidadPicking> Lstwhcol131 = new List<EntidadPicking>();
            DataTable DTwhcolo131 = dal.ConsultarPalletPicking131(PAID, USER);
            if (DTwhcolo131.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcolo131.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {


                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        CNPK = MyRow["CNPK"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        ROWN = MyRow["ROWN"].ToString(),
                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        //OSET = MyRow["OSET"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        //SQNB = MyRow["SQNB"].ToString(),
                        ADVS = MyRow["ADVS"].ToString(),

                    };

                    Lstwhcol131.Add(MyObjet);
                }

            }
            return Lstwhcol131;
        }
        //registros textbox ok

        public int IngRegistrott307140(string USER, int SQNB, string PDNO, int REFCNTD, int REFCNTU)
        {
           
            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.IngRegistrott307140(USER, SQNB, PDNO, REFCNTD, REFCNTU);
                return DTwhcolo131;
               
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }

        public DataTable ConsultarTt307140(Ent_ttccol307 Objttccol307 )
        {

                return dal.ConsultarTt307140(Objttccol307);
        }
        //actRegtticol022140
        public int actRegtticol022140(string PDNO)
        {

            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.actRegtticol022140(PDNO);
                return DTwhcolo131;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }

        
        public int actRegtticol042140(string PDNO)
        {

            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.actRegtticol042140(PDNO);
                return DTwhcolo131;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }


        public int actRegtticol131140(string PDNO)
        {

            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.actRegtticol131140(PDNO);
                return DTwhcolo131;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }



        public int actRegtticol082140(string user, string pallet, string Location, int stat, string t, string OORG, string ORNO, string OSET, string PONO, string SQNB, string ADVS, string sentencia, bool invertPallet = false, string newPallet = "", string PRIO = "")
        {

            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.actRegtticol082140(user, pallet, Location, stat, t, OORG, ORNO, OSET, PONO, SQNB, ADVS, sentencia, invertPallet, newPallet, PRIO);
                return DTwhcolo131;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }

        public int actRegtticol082140Paid(string user, string pallet, string Location, int stat, string prio)
        {

            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.actRegtticol082140Paid( user,  pallet,  Location,  stat,  prio);
                return DTwhcolo131;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }

        public int UpdateTtico082(Ent_tticol082 myObj82)
        {

            string strError = string.Empty;
            try
            {
                int updateTticol082 = dal.UpdateTtico082(myObj82);
                return updateTticol082;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }

        public bool UpdateTbl082ByPaid(string PAID_NEW, string PAID_OLD, string QTYT)
        {

            string strError = string.Empty;
            try
            {
                bool updateTticol082 = dal.UpdateTbl082ByPaid(PAID_NEW, PAID_OLD, QTYT);
                return updateTticol082;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }
       
         public int InsertRegCausalCOL084(string pallet,string user, int statCausal)
        {

            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.InsertRegCausalCOL084(pallet, user, statCausal);
                return DTwhcolo131;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }

      

         public int ingRegTticol092140(string maximo, string pallet, string txtpallet, int causal, string _operator)
         {

             string strError = string.Empty;
             try
             {
                 int DTwhcolo131 = dal.ingRegTticol092140(maximo, pallet, txtpallet,  causal,  _operator);
                 return DTwhcolo131;

             }
             catch (Exception ex)
             {
                 throw new Exception(strError += "\nError: " + ex.Message);
             }
         }
      
         public int ActCausalTICOL022(string pallet,int  stat )
        {

            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.ActCausalTICOL022(pallet, stat);
                return DTwhcolo131;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }

         public int ActCausalTICOL042(string pallet, int stat)
         {

             string strError = string.Empty;
             try
             {
                 int DTwhcolo131 = dal.ActCausalTICOL042(pallet, stat);
                 return DTwhcolo131;

             }
             catch (Exception ex)
             {
                 throw new Exception(strError += "\nError: " + ex.Message);
             }
         }

       
        
         public int ActCausalcol131140(string pallet, int stat)
         {

             string strError = string.Empty;
             try
             {
                 int DTwhcolo131 = dal.ActCausalcol131140(pallet, stat);
                 return DTwhcolo131;

             }
             catch (Exception ex)
             {
                 throw new Exception(strError += "\nError: " + ex.Message);
             }
         }


        //public DataTable invLabelRegrind_listaRegistrosOrdenParam(ref Ent_tticol011 Parametros, ref string strError)
        public DataTable invLabelRegrind_listaRegistrosOrdenParam()
        {
            
            DataTable retorno;
            try
            {
                retorno = dal.invLabelRegrind_listaRegistrosOrdenParam();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable VerificarLocate(string CWAR, string LOCA)
        {
            return dal.VerificarLocate(CWAR, LOCA);
        }

        public DataTable VerificarPalletID(string PAID)
        {
            return dal.VerificarPalletID(PAID);
        }
        //fin

        //public void IngRegistrott307140(string _operator, int p, string pallet, int p_2, int p_3)
        //{
        //    throw new NotImplementedException();
        //}

        public bool InsertarTccol307140(string USER, string STAT, string PAID, string CWAR, string PROC, string REFCNTD, string REFCNTU)
        {
            return dal.InsertarTccol307140(USER, STAT, PAID, CWAR, PROC, REFCNTD, REFCNTU);

        }

        public bool EliminarTccol307140(string PAID, string CWAR, ref string sentencia)
        {
            return dal.EliminarTccol307140(PAID,CWAR,ref sentencia);

        }

        public List<EntidadPicking> ConsultarPalletPicking22PAID(string PAID, string UrlBaseBarcode, string USER, string STAT, string CWAR,string PICK)
        {
            List<EntidadPicking> Lstwhcol122 = new List<EntidadPicking>();
            DataTable DTwhcol122 = dal.ConsultarPalletPicking22PAID(PAID, USER, STAT, CWAR, PICK);
            if (DTwhcol122.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcol122.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {

                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        ROWN = MyRow["ROWN"].ToString(),
                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        //QTYT = MyRow["QTYT"].ToString(),
                        //OSET = MyRow["OSET"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        //SQNB = MyRow["SQNB"].ToString(),
                        ADVS = MyRow["ADVS"].ToString(),
                        QTYT = MyRow["QTYT"].ToString(),
                        CNPK = MyRow["CNPK"].ToString(),
                        QTYOK = MyRow["QTYOK"].ToString(),
                        CWAROK = MyRow["CWAROK"].ToString(),
                        PICK = MyRow["PICK"].ToString(),
                        MCNO = MyRow["MCNO"].ToString(),
                        SLOC = MyRow["SLOC"].ToString()
                    };

                    Lstwhcol122.Add(MyObjet);
                }

            }
            return Lstwhcol122;
        }

        public List<EntidadPicking> ConsultarPalletPicking042PAID(string PAID, string UrlBaseBarcode, string USER, string STAT, string CWAR,string PICK)
        {
            List<EntidadPicking> Lstwhcol042 = new List<EntidadPicking>();
            DataTable DTwhcolo42 = dal.ConsultarPalletPicking042PAID(PAID, USER, STAT, CWAR, PICK);
            if (DTwhcolo42.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcolo42.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {


                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        ROWN = MyRow["ROWN"].ToString(),
                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        //OSET = MyRow["OSET"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        //SQNB = MyRow["SQNB"].ToString(),
                        ADVS = MyRow["ADVS"].ToString(),
                        QTYT = MyRow["QTYT"].ToString(),
                        CNPK = MyRow["CNPK"].ToString(),
                        QTYOK = MyRow["QTYOK"].ToString(),
                        CWAROK = MyRow["CWAROK"].ToString(),
                        PICK = MyRow["PICK"].ToString(),
                        MCNO = MyRow["MCNO"].ToString(),
                        SLOC = MyRow["SLOC"].ToString()


                    };

                    Lstwhcol042.Add(MyObjet);
                }

            }
            return Lstwhcol042;
        }


        public List<EntidadPicking> ConsultarPalletPicking131PAID(string PAID, string CWAR, string UrlBaseBarcode, string USER, string STAT, string PICK)
        {
            List<EntidadPicking> Lstwhcol131 = new List<EntidadPicking>();
            DataTable DTwhcolo131 = dal.ConsultarPalletPicking131PAID(PAID, CWAR, USER, STAT, PICK);
            if (DTwhcolo131.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcolo131.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {


                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        QTYT = MyRow["QTYT"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        ROWN = MyRow["ROWN"].ToString(),
                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        //OSET = MyRow["OSET"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        //SQNB = MyRow["SQNB"].ToString(),
                        ADVS = MyRow["ADVS"].ToString(),
                        //QTYT = MyRow["QTYT"].ToString(),
                        CNPK = MyRow["CNPK"].ToString(),
                        QTYOK = MyRow["QTYOK"].ToString(),
                        CWAROK = MyRow["CWAROK"].ToString(),
                        PICK = MyRow["PICK"].ToString(),
                        MCNO = MyRow["MCNO"].ToString(),
                        SLOC = MyRow["SLOC"].ToString()


                    };

                    Lstwhcol131.Add(MyObjet);
                }

            }
            return Lstwhcol131;
        }

        public bool Actualizar307(string PAID_NEW, string PAID_OLD)
        {
             return dal.Actualizar307(PAID_NEW, PAID_OLD);
        }

        public int ActCausalcol130140(string pallet, int stat)
        {
            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.ActCausalcol130140(pallet, stat);
                return DTwhcolo131;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }

        public List<EntidadPicking> ConsultarPalletPicking131With082(string CWAR, string p, string _operator)
        {
            List<EntidadPicking> Lstwhcol131 = new List<EntidadPicking>();
            DataTable DTwhcolo131 = dal.ConsultarPalletPicking131With082(CWAR, _operator);
            if (DTwhcolo131.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcolo131.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {
                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        ROWN = MyRow["ROWN"].ToString(),
                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        //OSET = MyRow["OSET"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        //SQNB = MyRow["SQNB"].ToString(),
                        ADVS = MyRow["ADVS"].ToString(),
                        CNPK = MyRow["CNPK"].ToString(),
                        QTYT = MyRow["QTYT"].ToString(),
                        STAT = MyRow["STAT"].ToString(),
                        SLOC = MyRow["SLOC"].ToString(),
                        MCNO = MyRow["MCNO"].ToString(),
                        PICK = MyRow["PICK"].ToString()
                    };

                    Lstwhcol131.Add(MyObjet);
                }

            }
            return Lstwhcol131;
        }

        public List<EntidadPicking> ConsultarPalletPicking042With082(string CWAR, string p, string _operator)
        {
            List<EntidadPicking> Lstwhcol042 = new List<EntidadPicking>();
            DataTable DTwhcolo42 = dal.ConsultarPalletPicking042With082(CWAR, _operator);
            if (DTwhcolo42.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcolo42.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {


                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTYT = MyRow["QTY"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        ROWN = MyRow["ROWN"].ToString(),
                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        //OSET = MyRow["OSET"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        //SQNB = MyRow["SQNB"].ToString(),
                        ADVS = MyRow["ADVS"].ToString(),
                        CNPK = MyRow["CNPK"].ToString(),
                        STAT = MyRow["STAT"].ToString(),
                        PICK = MyRow["PICK"].ToString(),
                        MCNO = MyRow["MCNO"].ToString(),
                        SLOC = MyRow["SLOC"].ToString()

                    };

                    Lstwhcol042.Add(MyObjet);
                }

            }
            return Lstwhcol042;
        }

        public List<EntidadPicking> ConsultarPalletPicking22With082(string CWAR, string p, string _operator)
        {
            List<EntidadPicking> Lstwhcol122 = new List<EntidadPicking>();
            DataTable DTwhcol122 = dal.ConsultarPalletPicking22With082(CWAR, _operator);
            if (DTwhcol122.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcol122.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {

                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTYT = MyRow["QTY"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        ROWN = MyRow["ROWN"].ToString(),
                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        //OSET = MyRow["OSET"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        //SQNB = MyRow["SQNB"].ToString(),
                        ADVS = MyRow["ADVS"].ToString(),
                        //QTYT = MyRow["QTYT"].ToString(),
                        CNPK = MyRow["CNPK"].ToString(),
                        STAT = MyRow["STAT"].ToString(),
                        PICK = MyRow["PICK"].ToString(),
                        MCNO = MyRow["MCNO"].ToString(),
                        SLOC = MyRow["SLOC"].ToString()
                    };

                    Lstwhcol122.Add(MyObjet);
                }

            }
            return Lstwhcol122;
        }

        public List<EntidadPicking> ConsultarPalletPicking131ItemQty(string Item, string Cant,string Prio, string _operator, string orno, string pono, string advs)
        {
            List<EntidadPicking> Lstwhcol131 = new List<EntidadPicking>();
            DataTable DTwhcolo131 = dal.ConsultarPalletPicking131ItemQty(Item, Cant, Prio, _operator, orno, pono, advs);
            if (DTwhcolo131.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcolo131.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {


                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        //ROWN = MyRow["ROWN"].ToString(),
                        //OORG = MyRow["OORG"].ToString(),
                        //ORNO = MyRow["ORNO"].ToString(),
                        //OSET = MyRow["OSET"].ToString(),
                        //PONO = MyRow["PONO"].ToString(),
                        //SQNB = MyRow["SQNB"].ToString(),
                        //ADVS = MyRow["ADVS"].ToString(),
                        STAT = MyRow["STAT"].ToString(),
                        MCNO = MyRow["MCNO"].ToString()

                    };

                    Lstwhcol131.Add(MyObjet);
                }

            }
            return Lstwhcol131;
        }

        public List<EntidadPicking> ConsultarPalletPicking042ItemQty(string Item, string Cant, string Prio, string _operator, string orno, string pono, string advs)
        {
            List<EntidadPicking> Lstwhcol042 = new List<EntidadPicking>();
            DataTable DTwhcolo42 = dal.ConsultarPalletPicking042ItemQty(Item, Cant, Prio,_operator, orno, pono, advs);
            if (DTwhcolo42.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcolo42.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {


                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        MCNO = MyRow["MCNO"].ToString()
                        //ROWN = MyRow["ROWN"].ToString(),
                        //OORG = MyRow["OORG"].ToString(),
                        //ORNO = MyRow["ORNO"].ToString(),
                        ////OSET = MyRow["OSET"].ToString(),
                        //PONO = MyRow["PONO"].ToString(),
                        //SQNB = MyRow["SQNB"].ToString(),
                        //ADVS = MyRow["ADVS"].ToString(),

                    };

                    Lstwhcol042.Add(MyObjet);
                }

            }
            return Lstwhcol042;
        }

        public List<EntidadPicking> ConsultarPalletPicking22ItemQty(string Item, string Cant, string Prio, string _operator, string orno, string pono, string advs)
        {
            List<EntidadPicking> Lstwhcol122 = new List<EntidadPicking>();
            DataTable DTwhcol122 = dal.ConsultarPalletPicking22ItemQty(Item, Cant, Prio, _operator, orno, pono, advs);
            if (DTwhcol122.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcol122.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {

                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        MCNO = MyRow["MCNO"].ToString()
                        //ROWN = MyRow["ROWN"].ToString(),
                        //OORG = MyRow["OORG"].ToString(),
                        //ORNO = MyRow["ORNO"].ToString(),
                        //OSET = MyRow["OSET"].ToString(),
                        //PONO = MyRow["PONO"].ToString(),
                        //SQNB = MyRow["SQNB"].ToString(),
                        //ADVS = MyRow["ADVS"].ToString(),
                        //QTYT = MyRow["QTYT"].ToString(),

                    };

                    Lstwhcol122.Add(MyObjet);
                }

            }
            return Lstwhcol122;
        }

        public DataTable ConsultarTt307140Proc(Ent_ttccol307 MyObj307)
        {
            return dal.ConsultarTt307140(MyObj307);
        }

        public bool Actualizar307Proc(string PAID_NEW, string PAID_OLD)
        {
            throw new NotImplementedException();
        }

        public bool Actualizar307Proc(string PAID_NEW, string PAID_OLD, string _operator)
        {
            return dal.Actualizar307proc(PAID_NEW, PAID_OLD, _operator);
        }

        public void updatetticol222Quantity(string pallet, decimal qtyt_act, decimal qtyt_old)
        {
            dal.updatetticol222Quantity(pallet, qtyt_act, qtyt_old);
        }

        public void updatetticol242Quantity(string pallet, decimal sqnb_act, decimal qtyt_old)
        {
            dal.updatetticol242Quantity(pallet, sqnb_act, qtyt_old);

        }

        public void updatetwhcol131Quantity(string pallet, decimal sqnb_act, decimal qtyt_old)
        {
            dal.updatetwhcol131Quantity(pallet, sqnb_act,qtyt_old);

        }

        public DataTable ConsultarTticol082porStat(string _operator, string stat, string PICK = "", string CWAR = "")
        {
            return dal.ConsultarTticol082porStat(_operator, stat, PICK, CWAR);
        }

        public bool ActualizarCantidades222(string PAID)
        {
            return dal.ActualizarCantidades222(PAID);
        }

        public bool ActualizarCantidades242(string PAID)
        {
            return dal.ActualizarCantidades242(PAID);
        }

        public bool ActualizarCantidades131(string PAID,bool OLD = true )
        {
            return dal.ActualizarCantidades131(PAID,OLD);
        }

        public void updatetwhcol131QuantityFirst(string pallet, decimal sqnb_act, decimal qtyt_old)
        {
            dal.updatetwhcol131QuantityFirst(pallet, sqnb_act, qtyt_old);
        }

        public void updatetticol242QuantityFist(string pallet, decimal sqnb_act, decimal qtyt_old)
        {
            dal.updatetticol242QuantityFist(pallet, sqnb_act, qtyt_old);

        }
        public void updatetticol222QuantityFirst(string pallet, decimal sqnb_act, decimal qtyt_old)
        {
            dal.updatetticol222QuantityFirst(pallet, sqnb_act, qtyt_old);
        }

        public List<EntidadPicking> ConsultarPalletPickingGlobal(string CWAR, string p, string _operator)
        {
            List<EntidadPicking> lstGlobal = new List<EntidadPicking>();
            DataTable DtGlobal = dal.ConsultarPalletPickingGlobal(CWAR, _operator);
            if (DtGlobal.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DtGlobal.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {

                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        ROWN = MyRow["ROWN"].ToString(),
                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        //OSET = MyRow["OSET"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        //SQNB = MyRow["SQNB"].ToString(),
                        ADVS = MyRow["ADVS"].ToString(),
                        //QTYT = MyRow["QTYT"].ToString(),
                        CNPK = MyRow["CNPK"].ToString(),
                        STAT = MyRow["STAT"].ToString()
                    };

                    lstGlobal.Add(MyObjet);
                }

            }
            return lstGlobal;
        }

        public bool UpdateTtico082Stat(Ent_tticol082 Obj082)
        {
            string strError = string.Empty;
            try
            {
                return dal.UpdateTtico082Stat(Obj082);

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }

        public bool UpdateTtico082StatEndPicking(Ent_tticol082 Obj082, String Random)
        {
            string strError = string.Empty;
            try
            {
                return dal.UpdateTtico082StatEndPicking(Obj082, Random);

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }

        public DataTable VerificarPalletIDItem(string PAID_NEW, string ITEM)
        {
            return dal.VerificarPalletIDItem(PAID_NEW, ITEM);
        }

        public DataTable getAllotwhcol131(string pallet)
        {
            return dal.getAllotwhcol131(pallet);
        }

        public DataTable getAllotticol242(string pallet)
        {
            return dal.getAllotticol242(pallet);
        }

        public DataTable getAllotticol222(string pallet)
        {
            return dal.getAllotticol222(pallet);
        }
    }
}