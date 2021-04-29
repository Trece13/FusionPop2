using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace whusap.WebPages.Labels.RedesingLabels
{
    public partial class _3Regrinds : System.Web.UI.Page
    {
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
            lblMaterialDesc.InnerHtml = Session["MaterialDesc"].ToString();
            codeMaterial.Src = Session["Material"].ToString();
            codePaid.Src = Session["codePaid"].ToString();
            lblLot.InnerHtml = Session["Lot"].ToString();
            lblQuantity.InnerHtml = Session["Quantity"].ToString();
            lblDate.InnerHtml = Session["Date"].ToString();
            lblMachine.InnerHtml = Session["Machine"].ToString();
            lblOperator.InnerHtml = Session["Operator"].ToString();
            lblPallet.InnerHtml = Session["Pallet"].ToString();
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
    }
}