using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace whusap.WebPages.Labels.RedesingLabels
{
    public partial class _2RollStock : System.Web.UI.Page
    {
        //params
        /*
            Session["MaterialDesc"]
            Session["codeMaterial"]
            Session["codePaid"]
            Session["Lot"]
            Session["Quantity"]
            Session["Date"]
            Session["Machine"]
            Session["Operator"]
            Session["Winder"]
            Session["Pallet"]
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            CrearLabel();
            try
            {
                lblMaterialDesc.InnerHtml = Session["MaterialDesc"].ToString();
                codeMaterial.Src = Session["codeMaterial"].ToString();
                codePaid.Src = Session["codePaid"].ToString();
                lblLot.InnerHtml = Session["Lot"].ToString();
                lblQuantity.InnerHtml = Session["Quantity"].ToString();
                lblDate.InnerHtml = Session["Date"].ToString();
                lblMachine.InnerHtml = Session["Machine"].ToString();
                lblOperator.InnerHtml = Session["Operator"].ToString();
                lblWinder.InnerHtml = Session["Winder"].ToString();
                lblPallet.InnerHtml = Session["Pallet"].ToString();
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
            codeMaterial.Src = string.Empty;
            codePaid.Src = string.Empty;
            lblLot.InnerHtml = string.Empty;
            lblQuantity.InnerHtml = string.Empty;
            lblDate.InnerHtml = string.Empty;
            lblMachine.InnerHtml = string.Empty;
            lblOperator.InnerHtml = string.Empty;
            lblWinder.InnerHtml = string.Empty;
            lblPallet.InnerHtml = string.Empty;
        }
    }
}