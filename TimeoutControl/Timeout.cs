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
    public class Timeout : WebControl, INamingContainer, IScriptControl
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
        [Description("URL to redirect the user, in the event of session timeout")]
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

        [Description("Controls whether the embedded javascript resource is sent to the client.  If false, you must provide the javascript by some other means.")]
        [Category("Behavior")]
        [DefaultValue(true)]
        public bool UseEmbeddedJavascript
        {
            get { return ViewState["UseEmbeddedJavascript"] as bool? ?? true; }
            set { ViewState["UseEmbeddedJavascript"] = value; }
        }

        [Category("Behavior")]
        [UrlProperty]
        [Description("URL to Timeout javascript file (if not using the embedded copy).  *Can be left blank if the timeout script is already available on the page (static link, combined into another file, etc).")]
        public string JavascriptUrl
        {
            get { return ViewState["JavascriptUrl"] as string ?? String.Empty; }
            set { ViewState["JavascriptUrl"] = value; }
        }

        [UrlProperty]
        public string SessionRefreshUrl
        {
            get { return ViewState["SessionRefreshUrl"] as string ?? string.Empty; }
            set { ViewState["SessionRefreshUrl"] = value; }
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
                descriptor.AddProperty("sessionRefreshURL", Page.ResolveClientUrl(this.SessionRefreshUrl));
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
                {
                    if (this.UseEmbeddedJavascript)
                        reference.Path = Page.ClientScript.GetWebResourceUrl(this.GetType(), "AjaxControls.Timeout.js");
                    else if (!String.IsNullOrEmpty(this.JavascriptUrl))
                        reference.Path = this.JavascriptUrl;
                    else return null;
                }
                return new ScriptReference[] { reference };
            }
            else return null;
        }
        #endregion
    }
}