using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace whusap.WebPages.Labels.RedesingLabels
{
    public partial class _4FinishedCups : System.Web.UI.Page
    {
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
                lblMaterialDesc.InnerHtml = Session["MaterialDesc"].ToString();
                codeMaterial.Src = Session["codeMaterial"].ToString();
                codePaid.Src = Session["codePaid"].ToString();
                lblLot.InnerHtml = Session["Lot"].ToString();
                lblQuantity.InnerHtml = Session["Quantity"].ToString();
                lblDate.InnerHtml = Session["Date"].ToString();
                lblPallet.InnerHtml = Session["Pallet"].ToString();
                lblMachine.InnerHtml = Session["Machine"].ToString();
                lblOperator.InnerHtml = Session["Operator"].ToString();
                if (Session["Reprint"].ToString() == "yes")
                {
                    printButton.Visible = false;
                    lblReprint.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('printSpace');", true);
                }
            }
            catch (Exception ex)
            {
                ClearLabel();
            }
        }

        private void ClearLabel()
        {
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
    }
}