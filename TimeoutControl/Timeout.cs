using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AjaxControls
{
    [ToolboxData("<{0}:Timeout runat=server></{0}:Timeout>")]
    public class Timeout : WebControl, INamingContainer, IScriptControl, ICallbackEventHandler
    {
        #region Properties
        [Category("Behavior")]
        [Description("Idle minutes until the user's session will time out")]
        public float TimeoutMinutes
        {
            get { return ViewState["TimeoutMinutes"] as float? ?? HttpContext.Current.Session.Timeout; }
            set { ViewState["TimeoutMinutes"] = value; }
        }

        [Category("Behavior")]
        [Description("Idle minutes until the user is informed that their session is about to time out")]
        public float AboutToTimeoutMinutes
        {
            get { return ViewState["AboutToTimeoutMinutes"] as float? ?? this.TimeoutMinutes - 1; }
            set { ViewState["AboutToTimeoutMinutes"] = value; }
        }

        [Category("Behavior")]
        [UrlProperty]
        [Description("URL to redirect the user, in the event of `session timeout")]
        public string TimeoutUrl
        {
            get { return ViewState["TimeoutUrl"] as string ?? String.Empty; }
            set { ViewState["TimeoutUrl"] = value; }
        }

        [Description("Span Id to display countdown timer in.")]
        [Category("Behavior")]
        public string CountDownSpanId
        {
            get { return ViewState["CountDownSpanId"] as string ?? String.Empty; }
            set { ViewState["CountDownSpanId"] = value; }
        }
        #endregion

        private ScriptManager scriptManager;

        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate Template { get; set; }

        protected override void CreateChildControls()
        {
            if (this.Enabled)
            {
                Controls.Clear();
                if (Template != null) Template.InstantiateIn(this);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!this.DesignMode && this.Enabled)
            {
                // Make sure ScriptManager exists
                scriptManager = ScriptManager.GetCurrent(Page);

                if (scriptManager == null)
                    throw new HttpException("A ScriptManager control must exist on the page.");

                // Register as client control.
                scriptManager.RegisterScriptControl(this);

                this.Style.Add("display", "none");

                string cbReference = Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData", "context");
                string callbackScript = "function CallServer(arg,context){" + cbReference + ";}";

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallServer", callbackScript, true);

                base.OnPreRender(e);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.DesignMode && this.Enabled)
            {
                scriptManager.RegisterScriptDescriptors(this);
                base.Render(writer);
            }
        }

        public event EventHandler RaisingCallbackEvent;

        public void RaiseCallbackEvent(String eventArgument)
        {
            // All we're doing here is resetting session, but we'll expose the event for subscribers.
            var e = RaisingCallbackEvent;
            if (e != null)
                e(this, null);
        }

        public String GetCallbackResult()
        {
            // return an emtpy string.  We're not really interested in the return value.
            return String.Empty;
        }

        #region IScriptControl Members

        IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
        {
            if (this.Enabled)
            {
                ScriptControlDescriptor descriptor = new ScriptControlDescriptor("AjaxControls.Timeout", this.ClientID);

                descriptor.AddProperty("timeoutMinutes", this.TimeoutMinutes);
                descriptor.AddProperty("aboutToTimeoutMinutes", this.AboutToTimeoutMinutes);
                descriptor.AddProperty("timeoutUrl", Page.ResolveClientUrl(this.TimeoutUrl));
                descriptor.AddProperty("countDownSpanId", this.CountDownSpanId);
                descriptor.AddProperty("clientId", this.ClientID);
                return new ScriptDescriptor[] { descriptor };
            }
            else
                return null;
        }

        IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
        {
            if (this.Enabled)
            {
                ScriptReference reference = new ScriptReference();

                if (Page != null)
                    reference.Path = Page.ClientScript.GetWebResourceUrl(this.GetType(), "AjaxControls.Timeout.js");

                return new ScriptReference[] { reference };
            }
            else return null;
        }
        #endregion
    }
}