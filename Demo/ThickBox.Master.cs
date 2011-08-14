using System;

namespace Demo
{
    public partial class ThickBox : System.Web.UI.MasterPage
    {
        // this could be used to disable the control from a content page
        public bool TimeoutControlEnabled
        {
            get { return Timeout1.Enabled; }
            set { Timeout1.Enabled = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}