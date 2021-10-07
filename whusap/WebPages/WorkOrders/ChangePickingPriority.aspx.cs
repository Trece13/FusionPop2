using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using whusa;
using Newtonsoft.Json;
using System.Data;
using System.Web.Services;
using whusa.Interfases;
using System.Globalization;
using System.Threading;
using System.Configuration;
using whusa.Entidades;
using whusa.Utilidades;
using System.Web.UI.WebControls.WebParts;
using whusa.DAL;
using System.Web.Configuration;

namespace whusap.WebPages.WorkOrders.NewPages
{
    public partial class ChangePickingPriority : System.Web.UI.Page
    {
        private static InterfazDAL_twhcol130 _idaltwhcol130 = new InterfazDAL_twhcol130();
        private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
        private static InterfazDAL_tticol042 _idaltticol042 = new InterfazDAL_tticol042();
        public static InterfazDAL_twhcol122 twhcolDAL = new InterfazDAL_twhcol122();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        private static InterfazDAL_tticol125 _idaltticol125 = new InterfazDAL_tticol125();
        private static InterfazDAL_twhcol122 _idaltwhcol122 = new InterfazDAL_twhcol122();
        private static IntefazDAL_ttccol307 _idaltccol307 = new IntefazDAL_ttccol307();
        private static IntefazDAL_tticol082 _idalticol082 = new IntefazDAL_tticol082();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetMachine()
        {
            List<Machine> lstMachine = new List<Machine>();
            DataTable dtMachine = _idalticol082.GetMachine();
            if (dtMachine.Rows.Count > 0)
            {
                foreach (DataRow dtRow in dtMachine.Rows)
                {
                    Machine machine = new Machine();
                    machine.MCNO = dtRow["MCNO"].ToString();
                    machine.DSCA = dtRow["DSCA"].ToString();
                    lstMachine.Add(machine);
                }
            }
            return JsonConvert.SerializeObject(lstMachine); 
        }

        [WebMethod]
        public static string GetPicks(string MCNO)
        {
            Ent_tticol082 MyObj082 = new Ent_tticol082();
            MyObj082.MCNO = MCNO;
            List<Ent_tticol082> lstPicks = new List<Ent_tticol082>();
            DataTable dtMachine = _idalticol082.GetPicks(MyObj082);
            if (dtMachine.Rows.Count > 0)
            {
                foreach (DataRow dtRow in dtMachine.Rows)
                {
                    Ent_tticol082 tticol082 = new Ent_tticol082();
                    tticol082.PICK = dtRow["PICK"].ToString();
                    tticol082.MCNO = dtRow["MCNO"].ToString();
                    tticol082.ORNO = dtRow["ORNO"].ToString();
                    //tticol082.TIME = dtRow["TIME"].ToString();
                    tticol082.PRIO = dtRow["PRIO"].ToString();
                    lstPicks.Add(tticol082);
                }
            }
            return JsonConvert.SerializeObject(lstPicks);
        }

        [WebMethod]
        public static void SavePrio( string PRIO , string PICK )
        {
            string prioUsing307 = string.Empty;
            bool ExistNewPrio =ExistPrio(PRIO);
            if (ExistNewPrio)
            {
               List<Ent_tticol082> lstUpdatePicks = new List<Ent_tticol082>();
               Ent_tticol082 MyObj082Last = new Ent_tticol082();
               DataTable nextsPrios =  getNextPrio(PRIO);
 
               foreach(DataRow item in nextsPrios.Rows){
                   Ent_tticol082 MyObj082 = new Ent_tticol082();
                   MyObj082.PICK = item["PICK"].ToString();
                   MyObj082.PRIO = item["PRIO"].ToString();
                   MyObj082.PAID = item["PAID"].ToString();
                   MyObj082.OLDP = item["PRIO"].ToString();
                   if (lstUpdatePicks.Count == 0)
                   {
                       lstUpdatePicks.Add(MyObj082);
                       MyObj082Last = MyObj082;
                   }
                   else
                   {
                       if (Convert.ToInt32(MyObj082Last.PRIO) + 1 == Convert.ToInt32(MyObj082.PRIO))
                       {
                           lstUpdatePicks.Add(MyObj082);
                           MyObj082Last = MyObj082;
                           
                       }
                       else
                       {
                           break;
                       }
                   }
               }

               if (lstUpdatePicks.Count > 0)
               {
                   for (int i = (lstUpdatePicks.Count - 1); i >= 0; i--)
                   {
                       if (lstUpdatePicks[i].PAID == string.Empty)
                       {
                           if (prioUsing307 == string.Empty)
                           {
                               UpdatePrio((Convert.ToInt32(lstUpdatePicks[i].PRIO) + 1).ToString(), lstUpdatePicks[i].PICK);
                           }
                           else
                           {
                               UpdatePrio((Convert.ToInt32(prioUsing307) + 1).ToString(), lstUpdatePicks[i].PICK);
                               prioUsing307 = string.Empty;
                           }
                       }
                       else
                       {
                           if (prioUsing307 == string.Empty)
                           {
                                prioUsing307 = lstUpdatePicks[i].PRIO;
                           }
                       }
                   }
                   if (prioUsing307 == string.Empty)
                   {
                    UpdatePrio(PRIO, PICK);
                   }
               }
            }
            else
            {
                UpdatePrio(PRIO, PICK);
            }
        }

        public static bool ExistPrio(string PRIO)
        {
            bool ret = false;
            Ent_tticol082 MyObj082 = new Ent_tticol082();
            MyObj082.PRIO = PRIO;
            DataTable dtMachine = _idalticol082.ExistPrio(MyObj082);
            if (dtMachine.Rows.Count > 0)
            {
                ret = true;
            }
            return ret;
        }

        public static bool UpdatePrio(string PRIO,string PICK)
        {
            Ent_tticol082 MyObj082 = new Ent_tticol082();
            MyObj082.PRIO = PRIO;
            MyObj082.PICK = PICK;
            List<Ent_tticol082> lstPicks = new List<Ent_tticol082>();
            return  _idalticol082.UpdatePrio(MyObj082);
        }

        public static DataTable getNextPrio(string PRIO)
        {
            Ent_tticol082 MyObj082 = new Ent_tticol082();
            MyObj082.PRIO = PRIO;
            DataTable dtMachine = _idalticol082.getNextPrio(MyObj082);
            return dtMachine;
        }

        class Machine
        {
            public string MCNO { get; set; }
            public string DSCA { get; set; }
        }
    }
}