using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa;
using whusa.Entidades;
using whusa.Interfases;

namespace whusap.WebPages.WorkOrders
{

    public partial class reviewDisposition : System.Web.UI.Page
    {
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        public static InterfazDAL_tticol119 ticol119DAL = new InterfazDAL_tticol119();
        public static IntefazDAL_transfer Transfers = new IntefazDAL_transfer();


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string ValidarItem(string ITEM)
        {
            Ent_twhcol130 twhcol130 = new Ent_twhcol130();
            twhcol130.ITEM = ITEM;
            DataTable consulta = twhcol130DAL.ValidarItemOnly(twhcol130);

            tcibd001 tcibd001 = new tcibd001();
            if (consulta.Rows.Count > 0)
            {

                foreach (DataRow row in consulta.Rows)
                {
                    tcibd001.ITEM = row["ITEM"].ToString().Trim();
                    tcibd001.KLTC = row["KLTC"].ToString().Trim();
                    tcibd001.CUNI = row["CUNI"].ToString().Trim();
                }

            }
            return JsonConvert.SerializeObject(tcibd001);
        }

        [WebMethod]
        public static bool ValidarLote(string ITEM, string CLOT)
        {
            bool retorno = false;
            Ent_twhcol130 twhcol130 = new Ent_twhcol130();
            twhcol130.ITEM = ITEM;
            twhcol130.CLOT = CLOT;
            DataTable DtLote = twhcol130DAL.ValidarLote(twhcol130);
            if (DtLote.Rows.Count > 0)
            {
                retorno = true;
            }
            return retorno;
        }


        [WebMethod]
        public static bool VerificarTipoWarehouse(string WARE)
        {
            bool retorno = false;
            whusap.WebPages.InvFloor.whInvTransfers.TypeWarehouse MyTypeWarehouse = new whusap.WebPages.InvFloor.whInvTransfers.TypeWarehouse();
            DataTable CurrentTypeWareHouse = Transfers.ConsultarTipoWarehouse(WARE);
            if (CurrentTypeWareHouse.Rows.Count > 0)
            {
                retorno = true;
            }
            return retorno;
        }

        [WebMethod]
        public static bool VerificarPalletID(string PAID)
        {
            bool retorno = false;
            string strError = string.Empty;
            DataTable DTPalletID = twhcol130DAL.VerificarPalletID(ref PAID);
            EntidadPicking ObjPicking = new EntidadPicking();

            if (DTPalletID.Rows.Count > 0)
            {
                retorno = true;
            }


            return retorno;
        }

        [WebMethod]
        public static bool VerificarPalletID(string PAID)
        {
            bool retorno = false;
            string strError = string.Empty;
            DataTable DTPalletID = twhcol130DAL.VerificarPalletID(ref PAID);
            EntidadPicking ObjPicking = new EntidadPicking();

            if (DTPalletID.Rows.Count > 0)
            {
                retorno = true;
            }


            return retorno;
        }

        [WebMethod]
        public static string VerificarPalletID(string ITEM,string WARE,string PAID, string CLOT, string DATEI, string DATEF)
        {
            string strError = string.Empty;

            List<Ent_tticol119> lst119 = new List<Ent_tticol119>();
            Ent_tticol119 MyObj119  = new Ent_tticol119();
            MyObj119.item = ITEM;
            MyObj119.cwar = WARE;
            MyObj119.paid = PAID;
            MyObj119.clot = CLOT;
            MyObj119.dati = DATEI;
            MyObj119.datf = DATEF;

            DataTable DT119 = ticol119DAL.SelectRegister(MyObj119,ref strError);
            if (DT119.Rows.Count > 0)
            {
                foreach (DataRow row in DT119.Rows)
                {
                    Ent_tticol119 MyObj119lst = new Ent_tticol119();

                    MyObj119lst.item = row["ITEM"].ToString();
                    MyObj119lst.cwar = row["CWAR"].ToString();
                    MyObj119lst.clot = row["CLOT"].ToString();
                    MyObj119lst.qtyr = Convert.ToInt32(row["QTYR"].ToString());
                    MyObj119lst.cdis = row["CDIS"].ToString();
                    MyObj119lst.obse = row["OBSE"].ToString();
                    MyObj119lst.logr = row["LOGR"].ToString();
                    MyObj119lst.datr = row["DATR"].ToString();
                    MyObj119lst.disp = Convert.ToInt32(row["DISP"].ToString());
                    MyObj119lst.stoc = row["STOC"].ToString();
                    MyObj119lst.ritm = row["RITM"].ToString();
                    MyObj119lst.proc = Convert.ToInt32(row["PROC"].ToString());
                    MyObj119lst.mess = row["MESS"].ToString();
                    MyObj119lst.suno = row["SUNO"].ToString();
                    MyObj119lst.refcntd = Convert.ToInt32(row["REFCNTD"].ToString());
                    MyObj119lst.refcntu = Convert.ToInt32(row["REFCNTU"].ToString());
                    lst119.Add(MyObj119lst);

                }
                
            }
            return  JsonConvert.SerializeObject(lst119);
        }
        
    }
}


