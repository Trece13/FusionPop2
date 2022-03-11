using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa;
using whusa.Entidades;
using whusa.Interfases;
using Newtonsoft.Json;

namespace whusap.WebPages.WorkOrders
{
    public partial class excel : System.Web.UI.Page
    {
        public static InterfazDAL_twhcol130 ITwhcol130 = new InterfazDAL_twhcol130();
        public static InterfazDAL_tticol022 ITticol022 = new InterfazDAL_tticol022();
        public static InterfazDAL_tticol042 ITticol042 = new InterfazDAL_tticol042();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string Receipt_Data(string Data)
        {
            string fails = string.Empty;
            try
            {
                //bool result = true;
                string[] DataColArray = new string[] { };
                string[] DataRowArray = Data.Split(';');
                Ent_twhcol130 MyObj131 = new Ent_twhcol130();
                Ent_tticol022 MyObj022 = new Ent_tticol022();
                Ent_tticol042 MyObj042 = new Ent_tticol042();
                for (int i = 0; i < DataRowArray.Length; i++)
                {
                    if (DataRowArray[i] != "")
                    {

                        DataColArray = DataRowArray[i].Split('|');
                        string Paid = DataColArray[3];
                        DataTable DTPalletID = ITwhcol130.VerificarPalletID(ref Paid);
                        if (DTPalletID.Rows.Count > 0)
                        {
                            if (DTPalletID.Rows[0]["TBL"].ToString() == "ticol042")
                            {
                                if (DataColArray.Length == 8 && (DataColArray[7] != "" || DataColArray[6] != ""))
                                {
                                    MyObj042.pdno = DataColArray[1].ToString();
                                    MyObj042.mitm = DataColArray[2].ToString();
                                    MyObj042.sqnb = DataColArray[3].ToString();
                                    MyObj042.cwat = DataColArray[4].ToString();
                                    MyObj042.aclo = DataColArray[5].ToString() == "" ? " " : DataColArray[5].ToString();
                                    MyObj042.acqt = Convert.ToDouble(DataColArray[6]);
                                    MyObj042.dele = Convert.ToInt32(DataColArray[7]);
                                    if (ITticol042.UpdateMasive(MyObj042))
                                    {
                                        ITticol042.UpdateMasive242(MyObj042);
                                    }
                                    else
                                    {
                                        fails += MyObj042.sqnb + ";";
                                    }
                                }
                                else
                                {
                                    fails += Paid + ";";
                                }
                            }
                            else if (DTPalletID.Rows[0]["TBL"].ToString() == "ticol022")
                            {

                                if (DataColArray.Length == 8 && (DataColArray[7] != "" || DataColArray[6] != ""))
                                {
                                    MyObj022.pdno = DataColArray[1].ToString();
                                    MyObj022.mitm = DataColArray[2].ToString();
                                    MyObj022.sqnb = DataColArray[3].ToString();
                                    MyObj022.cwat = DataColArray[4].ToString();
                                    MyObj022.aclo = DataColArray[5].ToString() == "" ? " " : DataColArray[5].ToString();
                                    MyObj022.acqt = Convert.ToDecimal(DataColArray[6]);
                                    MyObj022.dele = Convert.ToInt32(DataColArray[7]);
                                    if (ITticol022.UpdateMasive(MyObj022))
                                    {
                                        ITticol022.UpdateMasive222(MyObj022);
                                    }
                                    else
                                    {
                                        fails += MyObj022.sqnb + ";";
                                    }
                                }
                                else
                                {
                                    fails += Paid + ";";
                                }

                            }
                            else if (DTPalletID.Rows[0]["TBL"].ToString() == "whcol131")
                            {
                                if (DataColArray.Length == 8 && (DataColArray[7] != "" || DataColArray[6] != ""))
                                {

                                    MyObj131.OORG = Convert.ToInt32(DataColArray[0].Contains(",") ? (DataColArray[0].Substring(0, DataColArray[0].IndexOf(",")).ToString()) : ((DataColArray[0].ToString())));
                                    MyObj131.ORNO = DataColArray[1].ToString();
                                    MyObj131.ITEM = DataColArray[2].ToString();
                                    MyObj131.PAID = DataColArray[3].ToString();
                                    MyObj131.CWAA = DataColArray[4].ToString();
                                    MyObj131.LOAA = DataColArray[5].ToString() == "" ? " " : DataColArray[5].ToString();
                                    MyObj131.QTYA = Convert.ToDouble(DataColArray[6]);
                                    MyObj131.STAT = Convert.ToInt32(DataColArray[7]);
                                    if (!ITwhcol130.UpdateMasive(MyObj131))
                                    {
                                        fails += MyObj131.PAID + ";";
                                    }
                                }
                                else
                                {
                                    fails += Paid + ";";
                                }
                            }

                        }
                        else
                        {
                            fails += Paid + ";";
                        }

                    }
                }


                //return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return fails;
        }

        [WebMethod]
        public static string Click_Update(string WARE)
        {
            string strError = string.Empty;

            Ent_twhcol130 MyObj131 = new Ent_twhcol130();
            Ent_tticol022 MyObj022 = new Ent_tticol022();
            Ent_tticol042 MyObj042 = new Ent_tticol042();
            MyObj042.Error = true;
            MyObj042.cwat = WARE;
            MyObj042.acqt = Convert.ToInt32(0);
            MyObj042.dele = Convert.ToInt32(11);
            if (ITticol042.UpdateMasiveStatus(MyObj042))
            {
                ITticol042.UpdateMasive242WrhQty(MyObj042);
                MyObj042.Error = false;
            }

            MyObj022.cwat = WARE;
            MyObj022.acqt = Convert.ToInt32(0);
            MyObj022.dele = Convert.ToInt32(11);
            if (ITticol022.UpdateMasiveStatus(MyObj022))
            {
                ITticol022.UpdateMasive222WrhQty(MyObj022);
                MyObj042.Error = false;
            }

            MyObj131.CWAA = WARE;
            MyObj131.QTYA = Convert.ToInt32(0);
            MyObj131.STAT = Convert.ToInt32(9);
            if (ITwhcol130.UpdateMasiveStaQty(MyObj131))
            {
                MyObj042.Error = false;
                
            }

            if (MyObj042.Error == false)
            {
                MyObj042.Error = false;
                MyObj042.TypeMsgJs = "label";
                MyObj042.ErrorMsg = "Data Updated";
            }
            else
            {
                MyObj042.Error = true;
                MyObj042.TypeMsgJs = "label";
                MyObj042.ErrorMsg = "No Data Updated";
            }

            return JsonConvert.SerializeObject(MyObj042);
        }

    }
}


