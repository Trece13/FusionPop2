using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using System.Web.Configuration;

namespace whusap.WebPages.Labels.RedesingLabels
{
    public partial class _1RawMaterial : System.Web.UI.Page
    {
        public static IntefazDAL_transfer Transfers = new IntefazDAL_transfer();
        public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        //Params
        /*
            Session["MaterialDesc"]     
            Session["MaterialCode"]     
            Session["codePaid"]         
            Session["Lot"]              
            Session["Quantity"]         
            Session["Origin"]           
            Session["Supplier"]         
            Session["RecibedBy"]        
            Session["RecibedOn"]   
            Session["Reprint"]
            Session["AutoPrint"]
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            CrearLabel();

            lblMaterialDesc.InnerText   = Session["MaterialCode"]   != null ? Transfers.DescripcionItem(Session["MaterialCode"].ToString().Trim()) : string.Empty;
            //lblMaterialCode.InnerText   = Session["MaterialCode"]   != null ? Session["MaterialCode"].ToString() : string.Empty;
            codeMaterial.Src            = Session["MaterialCode"]   != null ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Session["MaterialCode"].ToString().Trim() + "&code=Code128&dpi=96" : string.Empty;
            codePaid.Src                = Session["codePaid"]       != null ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Session["codePaid"].ToString() + "&code=Code128&dpi=96" : string.Empty;
            lblLot.Text                 = Session["Lot"]            != null ? Session["Lot"].ToString()         : string.Empty;
            lblQuantity.Text            = Session["Quantity"]       != null ? Session["Quantity"].ToString().Replace(",",".")    : string.Empty;
            lblOrigin.Text              = Session["Origin"]         != null ? Session["Origin"].ToString()      : string.Empty;
            lblSupplier.Text            = Session["Supplier"]       != null ? Session["Supplier"].ToString()    : string.Empty;
            lblRecibedBy.Text           = Session["RecibedBy"]      != null ? Session["RecibedBy"].ToString()   : string.Empty;
            lblRecibedOn.Text           = Session["RecibedOn"]      != null ? Session["RecibedOn"].ToString()   : string.Empty;

            if (Session["Reprint"] != null)
            {
                if (Session["Reprint"].ToString() == "yes")
                {
                    printButton.Visible = false;
                    lblReprint.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('printSpace');", true);
                    EliminarVariablesSession();
                }
                else
                {
                    lblReprint.Visible = false;
                    if (Session["AutoPrint"] != null)
                    {
                        if (Session["AutoPrint"] == "yes")
                        {
                            printButton.Visible = false;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('printSpace');", true);
                            EliminarVariablesSession();
                        }
                        else
                        {
                            printButton.Visible = true;
                            EliminarVariablesSession();
                        }
                    }
                    else
                    {
                        printButton.Visible = true;
                        EliminarVariablesSession();
                    }
                }
            }
            else
            {
                EliminarVariablesSession();
            }
        }

        private void CrearLabel()
        {
            lblMaterialDesc.InnerText = string.Empty;
            //lblMaterialCode.InnerText = string.Empty;
            codeMaterial.Src = string.Empty;
            codePaid.Src = string.Empty;
            lblLot.Text = string.Empty;
            lblQuantity.Text = string.Empty;
            lblOrigin.Text = string.Empty;
            lblSupplier.Text = string.Empty;
            lblRecibedBy.Text = string.Empty;
            lblRecibedOn.Text = string.Empty;
        }

        private void EliminarVariablesSession()
        {

            Session["MaterialDesc"] = null;
            Session["MaterialCode"] = null;
            Session["codePaid"] = null;
            Session["Lot"] = null;
            Session["Quantity"] = null;
            Session["Origin"] = null;
            Session["Supplier"] = null;
            Session["RecibedBy"] = null;
            Session["RecibedOn"] = null;
            Session["Reprint"] = null;
            Session["AutoPrint"] = null;

        }
    }
}