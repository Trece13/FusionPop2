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
    public partial class _5MRBMaterials : System.Web.UI.Page
    {
        public static IntefazDAL_transfer Transfers = new IntefazDAL_transfer();
        public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        //Params
        /*
            Session["WorkOrder"]
            Session["lblReason"]
            Session["codePaid"]
            Session["ProductDesc"]
            Session["ProductCode"]
            Session["Date"]
            Session["Quantity"]
            Session["Finished"]
            Session["Pallet"]
            Session["PrintedBy"]
            Session["Machine"]
            Session["Comments"]
            Session["Reprint"]
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            CrearLabel();
            try
            {
                lblWorkOrder.InnerText      =  Session["WorkOrder"]     != null ? Session["WorkOrder"].ToString(): string.Empty;
                lblReason.InnerText         =  Session["lblReason"]     != null ? Session["lblReason"].ToString(): string.Empty;
                lblMaterialDesc.InnerText   =  "THIS PRODUCT IS ON HOLD PENDING DISPOSITION";
                codePaid.Src                =  Session["codePaid"]      != null ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Session["codePaid"].ToString() + "&code=Code128&dpi=96" : string.Empty;
                lblProductDesc.InnerText    =  Session["ProductCode"]   != null ? Transfers.DescripcionItem(Session["ProductCode"].ToString()) : string.Empty;
                lblProductCode.InnerText    =  Session["ProductCode"]   != null ? Session["ProductCode"].ToString(): string.Empty;
                lblDate.InnerText           =  Session["Date"]          != null ? Session["Date"].ToString(): string.Empty;
                lblQuantity.InnerText       =  Session["Quantity"]      != null ? Session["Quantity"].ToString(): string.Empty;
                lblFinished.InnerText       =  Session["Finished"]      != null ? Session["Finished"].ToString(): string.Empty;
                lblPallet.InnerText         =  Session["Pallet"]        != null ? Session["Pallet"].ToString(): string.Empty;
                lblPrintedBy.InnerText      =  Session["PrintedBy"]     != null ? Session["PrintedBy"].ToString(): string.Empty;
                lblMachine.InnerText        =  Session["Machine"]       != null ? Session["Machine"].ToString(): string.Empty;
                lblComments.InnerText       =  Session["Comments"]      != null ? Session["Comments"].ToString(): string.Empty;

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
                                lblReprint.Visible = false;
                                EliminarVariablesSession();
                            }
                        }
                        else
                        {
                            printButton.Visible = true;
                            lblReprint.Visible = false;
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
            lblWorkOrder.InnerText = string.Empty;
            lblReason.InnerText = string.Empty;
            lblMaterialDesc.InnerText = "THIS PRODUCT IS ON HOLD PENDING DISPOSITION";
            codePaid.Src = string.Empty;
            lblProductDesc.InnerText = string.Empty;
            lblProductCode.InnerText = string.Empty;
            lblDate.InnerText = string.Empty;
            lblQuantity.InnerText = string.Empty;
            lblFinished.InnerText = string.Empty;
            lblPallet.InnerText = string.Empty;
            lblPrintedBy.InnerText = string.Empty;
            lblMachine.InnerText = string.Empty;
            lblComments.InnerText = string.Empty;
        }

        private void EliminarVariablesSession()
        {
            Session["WorkOrder"] = null;
            Session["lblReason"] = null;
            Session["codePaid"] = null;
            Session["ProductCode"]= null;
            Session["ProductCode"]= null;
            Session["Date"]       = null;
            Session["Quantity"]   = null;
            Session["Finished"]   = null;
            Session["Pallet"]     = null;
            Session["PrintedBy"]  = null;
            Session["Machine"]    = null;
            Session["Comments"]   = null;
            Session["Reprint"] = null;
            Session["AutoPrint"] = null;

        }
    }
}