﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;

namespace whusap.WebPages.Labels.RedesingLabels
{
    public partial class _3Regrinds : System.Web.UI.Page
    {
        public static IntefazDAL_transfer Transfers = new IntefazDAL_transfer();
        public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        //Params
        /*
            Session["MaterialDesc"]     
            Session["Material"]         
            Session["codePaid"]         
            Session["Lot"]              
            Session["Quantity"]         
            Session["Date"]             
            Session["Machine"]          
            Session["Operator"]         
            Session["Pallet"]           
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            CrearLabel();
            try
            {
                lblMaterialDesc.InnerHtml   = Session["Material"]  != null  ?  Transfers.DescripcionItem(Session["Material"].ToString().Trim()): string.Empty;
                codeMaterial.Src            = Session["Material"]   != null  ?  UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Session["Material"].ToString() + "&code=Code128&dpi=96": string.Empty;
                codePaid.Src                = Session["codePaid"]   != null  ?  UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Session["codePaid"].ToString() + "&code=Code128&dpi=96": string.Empty;
                lblLot.InnerHtml            = Session["Lot"]        != null  ?  Session["Lot"].ToString(): string.Empty;
                lblQuantity.InnerHtml       = Session["Quantity"]   != null  ?  Session["Quantity"].ToString(): string.Empty;
                lblDate.InnerHtml           = Session["Date"]       != null  ?  Session["Date"].ToString(): string.Empty;
                lblMachine.InnerHtml        = Session["Machine"]    != null  ?  Session["Machine"].ToString(): string.Empty;
                lblOperator.InnerHtml       = Session["Operator"]   != null  ?  Session["Operator"].ToString(): string.Empty;
                lblPallet.InnerHtml         = Session["Pallet"]     != null  ?  Session["Pallet"].ToString(): string.Empty;
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
            lblMaterialDesc.InnerHtml = string.Empty;
            codeMaterial.Src = string.Empty;
            codePaid.Src = string.Empty;
            lblLot.InnerHtml = string.Empty;
            lblQuantity.InnerHtml = string.Empty;
            lblDate.InnerHtml = string.Empty;
            lblMachine.InnerHtml = string.Empty;
            lblOperator.InnerHtml = string.Empty;
            lblPallet.InnerHtml = string.Empty;
        }

        private void EliminarVariablesSession()
        {
            Session["Material"] = null;
            Session["Material"] = null;
            Session["codePaid"] = null;
            Session["Lot"]      = null;
            Session["Quantity"] = null;
            Session["Date"]     = null;
            Session["Machine"]  = null;
            Session["Operator"] = null;
            Session["Pallet"]   = null;
            Session["Reprint"] = null;
            Session["AutoPrint"] = null;

        }
    }
}