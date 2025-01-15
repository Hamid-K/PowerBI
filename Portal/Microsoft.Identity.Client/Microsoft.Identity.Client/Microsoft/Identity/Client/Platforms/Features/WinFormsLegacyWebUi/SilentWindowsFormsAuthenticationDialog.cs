using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi
{
	// Token: 0x020001AC RID: 428
	[ComVisible(true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal partial class SilentWindowsFormsAuthenticationDialog : WindowsFormsWebAuthenticationDialogBase
	{
		// Token: 0x06001358 RID: 4952 RVA: 0x000411F8 File Offset: 0x0003F3F8
		public SilentWindowsFormsAuthenticationDialog(object ownerWindow)
			: base(ownerWindow)
		{
			this.SuppressBrowserSubDialogs();
			base.WebBrowser.DocumentCompleted += this.DocumentCompletedHandler;
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x00041229 File Offset: 0x0003F429
		// (set) Token: 0x0600135A RID: 4954 RVA: 0x00041231 File Offset: 0x0003F431
		public int NavigationWaitMiliSecs { get; set; }

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600135B RID: 4955 RVA: 0x0004123C File Offset: 0x0003F43C
		// (remove) Token: 0x0600135C RID: 4956 RVA: 0x00041274 File Offset: 0x0003F474
		internal event SilentWindowsFormsAuthenticationDialog.SilentWebUIDoneEventHandler Done;

		// Token: 0x0600135D RID: 4957 RVA: 0x000412A9 File Offset: 0x0003F4A9
		public void CloseBrowser()
		{
			this.SignalDone(null);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x000412B2 File Offset: 0x0003F4B2
		private void SuppressBrowserSubDialogs()
		{
			((NativeWrapper.IWebBrowser2)base.WebBrowser.ActiveXInstance).Silent = true;
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x000412CA File Offset: 0x0003F4CA
		protected override void WebBrowserBeforeNavigateHandler(object sender, WebBrowserBeforeNavigateEventArgs e)
		{
			if (this.timer == null)
			{
				this.timer = SilentWindowsFormsAuthenticationDialog.CreateStartedTimer(delegate
				{
					if (DateTime.Now > this.navigationExpiry)
					{
						this.OnUserInteractionRequired();
					}
				}, this.NavigationWaitMiliSecs);
			}
			this.navigationExpiry = DateTime.MaxValue;
			base.WebBrowserBeforeNavigateHandler(sender, e);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00041304 File Offset: 0x0003F504
		private static Timer CreateStartedTimer(Action onTickAction, int interval)
		{
			Timer timer = new Timer();
			timer.Interval = interval;
			timer.Tick += delegate(object _, EventArgs _)
			{
				onTickAction();
			};
			timer.Start();
			return timer;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00041344 File Offset: 0x0003F544
		private void SignalDone(Exception exception = null)
		{
			if (!this.doneSignaled)
			{
				this.timer.Stop();
				SilentWebUIDoneEventArgs silentWebUIDoneEventArgs = new SilentWebUIDoneEventArgs(exception);
				SilentWindowsFormsAuthenticationDialog.SilentWebUIDoneEventHandler done = this.Done;
				if (done != null)
				{
					done(this, silentWebUIDoneEventArgs);
				}
				this.doneSignaled = true;
			}
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00041388 File Offset: 0x0003F588
		private void DocumentCompletedHandler(object sender, WebBrowserDocumentCompletedEventArgs args)
		{
			this.navigationExpiry = DateTime.Now.AddMilliseconds((double)this.NavigationWaitMiliSecs);
			if (this.HasLoginPage())
			{
				this.OnUserInteractionRequired();
			}
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x000413BD File Offset: 0x0003F5BD
		private void OnUserInteractionRequired()
		{
			this.SignalDone(new MsalUiRequiredException("no_prompt_failed", "One of two conditions was encountered: 1. The Prompt.Never flag was passed, but the constraint could not be honored, because user interaction was required. 2. An error occurred during a silent web authentication that prevented the HTTP authentication flow from completing in a short enough time frame. "));
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x000413D4 File Offset: 0x0003F5D4
		protected override void OnClosingUrl()
		{
			this.SignalDone(null);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x000413DD File Offset: 0x0003F5DD
		protected override void OnNavigationCanceled(int statusCode)
		{
			this.SignalDone(base.CreateExceptionForAuthenticationUiFailed(statusCode));
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x000413EC File Offset: 0x0003F5EC
		private bool HasLoginPage()
		{
			HtmlDocument document = base.WebBrowser.Document;
			HtmlElement htmlElement = null;
			if (null != document)
			{
				htmlElement = (from HtmlElement element in document.GetElementsByTagName("INPUT")
					where string.Compare(element.GetAttribute("type"), "password", StringComparison.Ordinal) == 0 && element.Enabled && element.OffsetRectangle.Height > 0 && element.OffsetRectangle.Width > 0
					select element).FirstOrDefault<HtmlElement>();
			}
			return htmlElement != null;
		}

		// Token: 0x040007EA RID: 2026
		private bool doneSignaled;

		// Token: 0x040007EB RID: 2027
		private DateTime navigationExpiry = DateTime.MaxValue;

		// Token: 0x0200042D RID: 1069
		// (Invoke) Token: 0x06001F10 RID: 7952
		internal delegate void SilentWebUIDoneEventHandler(object sender, SilentWebUIDoneEventArgs args);
	}
}
