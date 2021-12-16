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
    public partial class _4FinishedCupsDoubleME : System.Web.UI.Page
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
            ClearLabel();
            try
            {
                lblMaterialDesc.InnerHtml = Session["codeMaterial"] != null ? Transfers.DescripcionItem(Session["codeMaterial"].ToString().Trim()) : string.Empty;
                codeMaterial.Src = Session["codeMaterial"] != null ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Session["codeMaterial"].ToString() + "&code=Code128&dpi=96" : string.Empty;
                codePaid.Src = Session["codePaid"] != null ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Session["codePaid"].ToString() + "&code=Code128&dpi=96" : string.Empty;
                lblLot.InnerHtml = Session["Lot"] != null ? Session["Lot"].ToString() : string.Empty;
                lblQuantity.InnerHtml = Session["Quantity2"] != null ? Session["Quantity2"].ToString() : string.Empty;
                lblDate.InnerHtml = Session["Date"] != null ? Session["Date"].ToString() : string.Empty;
                lblPallet.InnerHtml = Session["codePaid"] != null ? Session["codePaid"].ToString() : string.Empty;
                if (HttpContext.Current.Session["Table"].ToString() != null)
                {
                    if (HttpContext.Current.Session["Table"].ToString() != "whcol131")
                    {
                        lblMachine.InnerHtml = Session["Machine"] != null ? Session["Machine"].ToString() : string.Empty;
                    }
                    else
                    {
                        lblMachine.InnerHtml = "";
                    }
                }
                lblOperator.InnerHtml = Session["Operator"] != null ? Session["Operator"].ToString() : string.Empty;
                myLabel.Visible = (Session["Quantity2"] != null ? Convert.ToDecimal(Session["Quantity2"].ToString()) : 0) > 0 ? true : false;

                lblMaterialDesc2.InnerHtml = Session["codeMaterial"] != null ? Transfers.DescripcionItem(Session["codeMaterial"].ToString().Trim()) : string.Empty;
                codeMaterial2.Src = Session["codeMaterial"] != null ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Session["codeMaterial"].ToString() + "&code=Code128&dpi=96" : string.Empty;
                codePaid2.Src = Session["codePaid2"] != null ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Session["codePaid2"].ToString() + "&code=Code128&dpi=96" : string.Empty;
                lblLot2.InnerHtml = Session["Lot"] != null ? Session["Lot"].ToString() : string.Empty;
                lblQuantity2.InnerHtml = Session["Quantity"] != null ? Session["Quantity"].ToString() : string.Empty;
                lblDate2.InnerHtml = Session["Date"] != null ? Session["Date"].ToString() : string.Empty;
                lblPallet2.InnerHtml = Session["codePaid2"] != null ? Session["codePaid2"].ToString() : string.Empty;
                lblMachine2.InnerHtml = Session["Machine"] != null ? Session["Machine"].ToString() : string.Empty;
                lblOperator2.InnerHtml = Session["Operator"] != null ? Session["Operator"].ToString() : string.Empty;
                bcPick.Src = Session["Pick"] != null ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Session["Pick"].ToString() + "&code=Code128&dpi=96" : string.Empty;
                lbMcno.InnerHtml = Session["Machine"] != null ? Session["Machine"].ToString() : string.Empty; ;
                lbPaid.InnerHtml = Session["codePaid2"] != null ? Session["codePaid2"].ToString() : string.Empty;
                lbQtyp.InnerHtml = Session["Quantity"] != null ? Session["Quantity"].ToString() : string.Empty; ;

                if (Session["PickLabel"] != null)
                {
                    if (Session["PickLabel"].ToString() == "yes")
                    {
                        MyEtiquetaDrop.Visible = true;

                    }
                    else
                    {
                        MyEtiquetaDrop.Visible = false;
                        bcPick.Src = "";
                        lbMcno.InnerHtml = "";
                        lbPaid.InnerHtml = "";
                        lbQtyp.InnerHtml = "";
                    }
                }

                if (Session["PartialLabel"] != null)
                {
                    if (Session["PartialLabel"].ToString() == "yes")
                    {
                        myLabel.Visible = true;
                        myLabel2.Visible = false;

                    }
                    else
                    {
                        myLabel.Visible = false;
                        myLabel2.Visible = false;
                    }
                }
                else
                {
                    MyEtiquetaDrop.Visible = false;
                    myLabel.Visible = true;
                    myLabel2.Visible = true;
                }

                printButton.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('printSpace');", true);
                EliminarVariablesSession();

                if (Session["Reprint"] != null)
                {
                    if (Session["Reprint"].ToString() == "yes")
                    {
                        printButton.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('printSpace');", true);
                        EliminarVariablesSession();
                    }
                    else
                    {
                        if (Session["AutoPrint"] != null)
                        {
                            if (Session["AutoPrint"].ToString() == "yes")
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
                ClearLabel();
            }
        }

        private void ClearLabel()
        {
            printButton.Visible = true;
            //lblReprint.Visible = false;
            lblMaterialDesc.InnerHtml = string.Empty;
            codeMaterial.Src = string.Empty;
            codePaid.Src = string.Empty;
            lblLot.InnerHtml = string.Empty;
            lblQuantity.InnerHtml = string.Empty;
            lblDate.InnerHtml = string.Empty;
            lblPallet.InnerHtml = string.Empty;
            lblMachine.InnerHtml = string.Empty;
            lblOperator.InnerHtml = string.Empty;
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