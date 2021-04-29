using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace whusap.WebPages.Labels.RedesingLabels
{
    public partial class _6Inventory_Label : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CrearLabel();
            lblMaterialDesc.InnerText = Session["MaterialDesc"].ToString();
            codeMaterial.Src = Session["codeMaterial"].ToString();
            codePaid.Src = Session["codePaid"].ToString();
            lblLot.InnerText = Session["Lot"].ToString();
            lblQuantity.InnerText = Session["Quantity"].ToString();
            lblDate.InnerText = Session["Date"].ToString();
            lbPallet.InnerText = Session["Pallet"].ToString();
            lblMachine.InnerText = Session["Machine"].ToString();
            lblOperator.InnerText = Session["Operator"].ToString();

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
    }
}