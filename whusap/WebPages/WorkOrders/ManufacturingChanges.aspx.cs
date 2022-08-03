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
    public partial class ManufacturingChanges : System.Web.UI.Page
    {
        public static IntefazDAL_transfer Transfers = new IntefazDAL_transfer();
        public static InterfazDAL_twhcol130 ITwhcol130 = new InterfazDAL_twhcol130();
        public static InterfazDAL_twhcol030 Itwhcol030 = new InterfazDAL_twhcol030();
        public string strError;
        protected void Page_Load(object sender, EventArgs e)
        {

            Ent_ttccol301 data = new Ent_ttccol301()
            {
                user = HttpContext.Current.Session["user"].ToString(),
                come = "Material Request Transfer",
                refcntd = 0,
                refcntu = 0
            };

            List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
            datalog.Add(data);

            new InterfazDAL_ttccol301().insertarRegistro(ref datalog, ref strError);
        }

        [WebMethod]
        public static string VerificarWarehouse(string WARE)
        {
            Warehouse MyWarehouse = new Warehouse();
            DataTable CurrentWareHouse = Transfers.ConsultarTipoWarehouse(WARE);
            if (CurrentWareHouse.Rows.Count > 0)
            {
                MyWarehouse.CWAR = WARE;
                MyWarehouse.DSCA = CurrentWareHouse.Rows[0]["DSCA"].ToString();
            }
            else
            {
                MyWarehouse.Error = true;
                MyWarehouse.MsgError = "The Warehouse don't exist";

            }

            return JsonConvert.SerializeObject(MyWarehouse);
        }

        [WebMethod]
        public static string VerifyItem(string ITEM, string ROW, string CWAR)
        {
            Ent_twhcol130 twhcol130 = new Ent_twhcol130();
            twhcol130.ITEM = ITEM;
            twhcol130.CWAR = CWAR;
            DataTable CurrentItem = ITwhcol130.ValidarItemOnly215(twhcol130);
            if (CurrentItem.Rows.Count > 0)
            {
                twhcol130.ITEM = CurrentItem.Rows[0]["ITEM"].ToString();
                twhcol130.UNIT = CurrentItem.Rows[0]["CUNI"].ToString();
                twhcol130.DSCA = CurrentItem.Rows[0]["DSCA"].ToString();
                twhcol130.QTYS = Convert.ToInt32(CurrentItem.Rows[0]["QSTR"].ToString());
                twhcol130.Row = ROW;
            }
            else
            {
                twhcol130.Error = true;
                twhcol130.MsgError = "The Item don't exist";
            }
            return JsonConvert.SerializeObject(twhcol130);
        }

        [WebMethod]
        public static string Save(string CWOR, string CWDE, string ITEM, string OQTY, string UNIT, string RQTY)
        {
            string strError = string.Empty;
            Ent_ttwhcol030 twhcol130 = new Ent_ttwhcol030();
            twhcol130.CWOR = CWOR.ToUpper();
            twhcol130.CWDE = CWDE.ToUpper();
            twhcol130.ITEM = ITEM.ToUpper();
            twhcol130.QTDL = RQTY.ToUpper();
            twhcol130.RCNO = "";
            twhcol130.CUNI = UNIT;
            twhcol130.USER = HttpContext.Current.Session["user"].ToString();
            if(Itwhcol030.InsertTwhcol030(twhcol130,ref strError)){
                twhcol130.Error = false;
                twhcol130.Msg = "Save successfull";
            }
            else{
                twhcol130.Error = true;
                twhcol130.Msg = "Save error";
            }
            return JsonConvert.SerializeObject(twhcol130); ;
        }


        public class Warehouse
        {
            public string CWAR { get; set; }
            public string SLOC { get; set; }
            public string DSCA { get; set; }

            public bool Error { get; set; }

            public string MsgError { get; set; }
        }
    }
}