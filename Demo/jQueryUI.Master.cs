using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demo
{
    public partial class jQueryUI : System.Web.UI.MasterPage
    {
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

        protected void Timeout1_RaisingCallbackEvent(object sender, EventArgs e)
        {
            // when the timeout control's callback is fired, this event will fire
            string x = "";
        }
    }
}