using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;

namespace whusap.WebPages.Labels.RedesingLabels
{
    public partial class _6InventoryLabel : System.Web.UI.Page
    {
        public static IntefazDAL_transfer Transfers = new IntefazDAL_transfer();
        public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        //Params
        /*
            Session["MaterialDesc"]
            Session["codeMaterial"]
            Session["codePaid"]
            Session["Lot"]
            Session["Quantity"]
            Session["Date"]
            Session["Pallet"]
            Session["Machine"]
            Session["Operator"]
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            CrearLabel();
            try
            {
                lblMaterialDesc.InnerText   = Session["codeMaterial"] != null ?  Transfers.DescripcionItem(Session["codeMaterial"].ToString().Trim()): string.Empty;
                codeMaterial.Src            = Session["codeMaterial"] != null ?  UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Session["codeMaterial"].ToString() + "&code=Code128&dpi=96": string.Empty;
                codePaid.Src                = Session["codePaid"]   != null ?  UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Session["codePaid"].ToString() + "&code=Code128&dpi=96": string.Empty; 
                lblLot.InnerText            = Session["Lot"]        != null ?  Session["Lot"].ToString(): string.Empty;
                lblQuantity.InnerText       = Session["Quantity"]   != null ?  Session["Quantity"].ToString(): string.Empty;
                lblDate.InnerText           = Session["Date"]       != null ?  Session["Date"].ToString(): string.Empty;
                lbPallet.InnerText          = Session["Pallet"]     != null ?  Session["Pallet"].ToString(): string.Empty;
                lblMachine.InnerText        = Session["Machine"]    != null ?  Session["Machine"].ToString(): string.Empty;
                lblOperator.InnerText       = Session["Operator"]   != null ?  Session["Operator"].ToString(): string.Empty;

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
            catch (Exception ex)
            {
                CrearLabel();
            }
        }

        private void CrearLabel()
        {
            lblMaterialDesc.InnerText = string.Empty;
            codeMaterial.Src = string.Empty;
            codePaid.Src = string.Empty;
            lblLot.InnerText = string.Empty;
            lblQuantity.InnerText = string.Empty;
            lblDate.InnerText = string.Empty;
            lbPallet.InnerText = string.Empty;
            lblMachine.InnerText = string.Empty;
            lblOperator.InnerText = string.Empty;
        }

        private void EliminarVariablesSession()
        {
            Session["codeMaterial"] = null;
            Session["codePaid"] = null;
            Session["Lot"] = null;
            Session["Quantity"] = null;
            Session["Date"] = null;
            Session["Pallet"] = null;
            Session["Machine"] = null;
            Session["Operator"] = null;
            Session["Reprint"] = null;
            Session["AutoPrint"] = null;

        }
    }
}