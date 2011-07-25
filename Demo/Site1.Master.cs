using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demo
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        // this could be used to disable the control from a content page
        public bool TimeoutControlEnabled
        {
            get { return Timeout1.Enabled; }
            set { Timeout1.Enabled = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // no longer required as these are now the default values of the control.
            //Timeout1.TimeoutMinutes = HttpContext.Current.Session.Timeout;
            //Timeout1.AboutToTimeoutMinutes = Timeout1.TimeoutMinutes - 1;
        }
    }
}