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
        protected void Page_Load(object sender, EventArgs e)
        {
            CrearLabel();
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