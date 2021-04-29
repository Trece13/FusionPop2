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
           Session["lblLot"]       
           Session["lblQuantity"]  
           Session["lblDate"]      
           Session["lblPallet"]    
           Session["lblMachine"]   
           Session["lblOperator"]  
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearLabel();
            lblMaterialDesc.InnerHtml = Session["MaterialDesc"].ToString();
            codeMaterial.Src = Session["codeMaterial"].ToString();
            codePaid.Src = Session["codePaid"].ToString();
            lblLot.InnerHtml = Session["lblLot"].ToString();
            lblQuantity.InnerHtml = Session["lblQuantity"].ToString();
            lblDate.InnerHtml = Session["lblDate"].ToString();
            lblPallet.InnerHtml = Session["lblPallet"].ToString();
            lblMachine.InnerHtml = Session["lblMachine"].ToString();
            lblOperator.InnerHtml = Session["lblOperator"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('printSpace');", true);
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