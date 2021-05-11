using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace whusap.WebPages.Labels.RedesingLabels
{
    public partial class _5MRBMaterials : System.Web.UI.Page
    {
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
                lblWorkOrder.InnerText = Session["WorkOrder"].ToString();
                lblReason.InnerText = Session["lblReason"].ToString();
                lblMaterialDesc.InnerText = "THIS PRODUCT IS ON HOLD PENDING DISPOSITION";
                codePaid.Src = Session["codePaid"].ToString();
                lblProductDesc.InnerText = Session["ProductDesc"].ToString();
                lblProductCode.InnerText = Session["ProductCode"].ToString();
                lblDate.InnerText = Session["Date"].ToString();
                lblQuantity.InnerText = Session["Quantity"].ToString();
                lblFinished.InnerText = Session["Finished"].ToString();
                lblPallet.InnerText = Session["Pallet"].ToString();
                lblPrintedBy.InnerText = Session["PrintedBy"].ToString();
                lblMachine.InnerText = Session["Machine"].ToString();
                lblComments.InnerText = Session["Comments"].ToString();
                if (Session["Reprint"].ToString() == "yes")
                {
                    printButton.Visible = false;
                    lblReprint.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('printSpace');", true);
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
    }
}