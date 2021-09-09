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
    public partial class _5MRBMaterialsDouble : System.Web.UI.Page
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
                //JC 130821 En la etiqueta con cantidad restante no debe generar leyenda de motivo 
                //lblReason.InnerText         =  Session["lblReason"]     != null ? Session["lblReason"].ToString(): string.Empty;
                //lblMaterialDesc.InnerText   =  "THIS PRODUCT IS ON HOLD PENDING DISPOSITION";
                lblReason.InnerText         =  "";
                lblMaterialDesc.InnerText   =  "";
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

                lblWorkOrder2.InnerText      = Session["WorkOrder2"] != null ? Session["WorkOrder2"].ToString() : string.Empty;
                lblReason2.InnerText         = Session["lblReason2"] != null ? Session["lblReason2"].ToString() : string.Empty;
                lblMaterialDesc2.InnerText   = "THIS PRODUCT IS ON HOLD PENDING DISPOSITION";
                codePaid2.Src                = Session["codePaid2"] != null ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Session["codePaid2"].ToString() + "&code=Code128&dpi=96" : string.Empty;
                lblProductDesc2.InnerText    = Session["ProductCode2"] != null ? Transfers.DescripcionItem(Session["ProductCode"].ToString()) : string.Empty;
                lblProductCode2.InnerText    = Session["ProductCode2"] != null ? Session["ProductCode2"].ToString() : string.Empty;
                lblDate2.InnerText           = Session["Date2"] != null ? Session["Date2"].ToString() : string.Empty;
                lblQuantity2.InnerText       = Session["Quantity2"] != null ? Session["Quantity2"].ToString() : string.Empty;
                lblFinished2.InnerText       = Session["Finished2"] != null ? Session["Finished2"].ToString() : string.Empty;
                lblPallet2.InnerText         = Session["Pallet2"] != null ? Session["Pallet2"].ToString() : string.Empty;
                lblPrintedBy2.InnerText      = Session["PrintedBy2"] != null ? Session["PrintedBy2"].ToString() : string.Empty;
                lblMachine2.InnerText        = Session["Machine2"] != null ? Session["Machine2"].ToString() : string.Empty;
                lblComments2.InnerText       = Session["Comments2"] != null ? Session["Comments2"].ToString() : string.Empty;


                if (Session["Reprint"] != null)
                {
                    if (Session["Reprint"].ToString() == "yes")
                    {
                        printButton.Visible = false;
                        lblReprint.Visible = true;
                        lblReprint2.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('printSpace');", true);
                        EliminarVariablesSession();
                    }
                    else
                    {
                        lblReprint.Visible = false;
                        lblReprint2.Visible = false;
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