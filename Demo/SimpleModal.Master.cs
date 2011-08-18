using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demo
{
    public partial class SimpleModal : System.Web.UI.MasterPage
    {
        // this could be used to disable the control from a content page
        public bool TimeoutControlEnabled
        {
            get { return Timeout1.Enabled; }
            set { Timeout1.Enabled = value; }
        }

        // can be used to access the clientid from content pages
        public string TimeoutControlClientId
        {
            get { return Timeout1.ClientID; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}