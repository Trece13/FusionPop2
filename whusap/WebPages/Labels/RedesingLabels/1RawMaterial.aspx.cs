using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace whusap.WebPages.Labels.RedesingLabels
{
    public partial class _1RawMaterial : System.Web.UI.Page
    {
        //Params
        /*
            Session["MaterialDesc"]     
            Session["MaterialCode"]     
            Session["codePaid"]         
            Session["Lot"]              
            Session["Quantity"]         
            Session["Origin"]           
            Session["Supplier"]         
            Session["RecibedBy"]        
            Session["RecibedOn"]   
            Session["Reprint"]
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            CrearLabel();
            try
            {
                lblMaterialDesc.InnerText = Session["MaterialDesc"].ToString();
                lblMaterialCode.InnerText = Session["MaterialCode"].ToString();
                codePaid.Src = Session["codePaid"].ToString();
                lblLot.Text = Session["Lot"].ToString();
                lblQuantity.Text = Session["Quantity"].ToString();
                lblOrigin.Text = Session["Origin"].ToString();
                lblSupplier.Text = Session["Supplier"].ToString();
                lblRecibedBy.Text = Session["RecibedBy"].ToString();
                lblRecibedOn.Text = Session["RecibedOn"].ToString();
                if (Session["Reprint"].ToString() == "yes")
                {
                    printButton.Visible = false;
                    lblReprint.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('printSpace');", true);
                }
                else
                {
                    printButton.Visible = true;
                    lblReprint.Visible = false;
                }
            }
            catch (Exception ex)
            {
                CrearLabel();
            }
        }

        private void CrearLabel()
        {
            lblMaterialDesc.InnerText = string.Empty;
            lblMaterialCode.InnerText = string.Empty;
            codePaid.Src = string.Empty;
            lblLot.Text = string.Empty;
            lblQuantity.Text = string.Empty;
            lblOrigin.Text = string.Empty;
            lblSupplier.Text = string.Empty;
            lblRecibedBy.Text = string.Empty;
            lblRecibedOn.Text = string.Empty;
        }
    }
}