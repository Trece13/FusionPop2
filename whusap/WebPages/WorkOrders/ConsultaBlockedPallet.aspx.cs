using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using whusa.Entidades;
using System.Data;
using whusa.Interfases;
using Newtonsoft.Json;
using System.Configuration;

namespace whusap.WebPages.WorkOrders
{
    public partial class ConsultaBlockedPallet : System.Web.UI.Page
    {
        public static IntefazDAL_tticol082 Itticol082 = new IntefazDAL_tticol082();
        public int CicloPaginacion = 0;
        public int CicloActualizacion = 0;
        private static string _operator;
        protected void Page_Load(object sender, EventArgs e)
        {
            CicloPaginacion = Convert.ToInt32(ConfigurationManager.AppSettings["CicloPaginacion"].ToString());
            CicloActualizacion = Convert.ToInt32(ConfigurationManager.AppSettings["CicloActualizacion"].ToString());
            if (Session["user"] == null)
            {
                if (Request.QueryString["Valor1"] == null || Request.QueryString["Valor1"] == "")
                {
                    Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                }
                else
                {
                    _operator = Request.QueryString["Valor1"];
                    Session["user"] = _operator;
                    Session["logok"] = "OKYes";
                    //txtNumeroOrden.Enabled = false;
                }
            }
            else
            {
                _operator = Session["user"].ToString();
            }

        }

        [WebMethod]
        public static string ClickQuery()
        {
            Console.WriteLine("Entro en ClickQuery...");
            string strError = string.Empty;
            DataTable ListaRegistroCustomer = Itticol082.ConsultarRegistrosBloquedos(_operator);
            if (strError == string.Empty)
            {
                return JsonConvert.SerializeObject(ListaRegistroCustomer);
            }
            else
            {
                return strError;
            }

        }
    }
}