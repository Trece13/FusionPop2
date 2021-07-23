using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Threading;
using System.Globalization;
using whusa.Entidades;
using whusa.Utilidades;
using whusa.Interfases;
using whusa;

namespace whusap
{
    public class Global : System.Web.HttpApplication
    {
        private static IntefazDAL_ttccol307 _idaltccol307 = new IntefazDAL_ttccol307();
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            //base.InitializeCulture();
        }

        void Application_End(object sender, EventArgs e)
        {

        }

        void Application_Error(object sender, EventArgs e)
        {

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
        
        }

    }
}
