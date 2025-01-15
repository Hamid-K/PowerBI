using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs;
using Microsoft.Identity.Client.UI;

namespace Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi
{
	// Token: 0x020001B2 RID: 434
	[ComVisible(true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public partial class WindowsFormsWebAuthenticationDialog : WindowsFormsWebAuthenticationDialogBase
	{
		// Token: 0x06001387 RID: 4999 RVA: 0x00041634 File Offset: 0x0003F834
		public WindowsFormsWebAuthenticationDialog(object ownerWindow, EmbeddedWebViewOptions embeddedWebViewOptions)
			: base(ownerWindow)
		{
			base.Shown += this.FormShownHandler;
			this._embeddedWebViewOptions = embeddedWebViewOptions ?? EmbeddedWebViewOptions.GetDefaultOptions();
			if (string.IsNullOrEmpty(this._embeddedWebViewOptions.Title))
			{
				base.WebBrowser.DocumentTitleChanged += this.WebBrowserDocumentTitleChangedHandler;
			}
			else
			{
				this.Text = this._embeddedWebViewOptions.Title;
			}
			base.WebBrowser.ObjectForScripting = this;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x000416B2 File Offset: 0x0003F8B2
		protected override void OnAuthenticate(CancellationToken cancellationToken)
		{
			this._zoomed = false;
			this._statusCode = 0;
			this.ShowBrowser(cancellationToken);
			base.OnAuthenticate(cancellationToken);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x000416D0 File Offset: 0x0003F8D0
		public void ShowBrowser(CancellationToken cancellationToken)
		{
			DialogResult uiResult = DialogResult.None;
			using (cancellationToken.Register(new Action(this.CloseIfOpen)))
			{
				base.InvokeHandlingOwnerWindow(delegate
				{
					uiResult = this.ShowDialog(this.ownerWindow);
				});
				cancellationToken.ThrowIfCancellationRequested();
			}
			DialogResult uiResult2 = uiResult;
			if (uiResult2 == DialogResult.OK)
			{
				return;
			}
			if (uiResult2 == DialogResult.Cancel)
			{
				base.Result = AuthorizationResult.FromStatus(AuthorizationStatus.UserCancel);
				return;
			}
			throw base.CreateExceptionForAuthenticationUiFailed(this._statusCode);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00041768 File Offset: 0x0003F968
		private void CloseIfOpen()
		{
			if (Application.OpenForms.OfType<WindowsFormsWebAuthenticationDialog>().Any<WindowsFormsWebAuthenticationDialog>())
			{
				base.InvokeOnly(new Action(base.Close));
			}
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0004178D File Offset: 0x0003F98D
		protected override void WebBrowserBeforeNavigateHandler(object sender, WebBrowserBeforeNavigateEventArgs e)
		{
			this.SetBrowserZoom();
			base.WebBrowserBeforeNavigateHandler(sender, e);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0004179D File Offset: 0x0003F99D
		protected override void OnClosingUrl()
		{
			base.DialogResult = DialogResult.OK;
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x000417A6 File Offset: 0x0003F9A6
		protected override void OnNavigationCanceled(int inputStatusCode)
		{
			this._statusCode = inputStatusCode;
			base.DialogResult = ((inputStatusCode == 0) ? DialogResult.Cancel : DialogResult.Abort);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x000417BC File Offset: 0x0003F9BC
		private void SetBrowserZoom()
		{
			int zoomPercent = WindowsDpiHelper.ZoomPercent;
			if (WindowsDpiHelper.IsProcessDPIAware() && 100 != zoomPercent && !this._zoomed)
			{
				this.SetBrowserControlZoom(zoomPercent - 1);
				this.SetBrowserControlZoom(zoomPercent);
				this._zoomed = true;
			}
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x000417FC File Offset: 0x0003F9FC
		private void SetBrowserControlZoom(int zoomPercent)
		{
			NativeWrapper.IOleCommandTarget oleCommandTarget = ((NativeWrapper.IWebBrowser2)base.WebBrowser.ActiveXInstance).Document as NativeWrapper.IOleCommandTarget;
			if (oleCommandTarget != null)
			{
				object[] array = new object[] { zoomPercent };
				Marshal.ThrowExceptionForHR(oleCommandTarget.Exec(IntPtr.Zero, 63, 2, array, IntPtr.Zero));
			}
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00041850 File Offset: 0x0003FA50
		private void FormShownHandler(object sender, EventArgs e)
		{
			if (base.Owner == null)
			{
				base.Activate();
			}
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00041860 File Offset: 0x0003FA60
		private void WebBrowserDocumentTitleChangedHandler(object sender, EventArgs e)
		{
			this.Text = base.WebBrowser.DocumentTitle;
		}

		// Token: 0x040007FF RID: 2047
		private int _statusCode;

		// Token: 0x04000800 RID: 2048
		private bool _zoomed;

		// Token: 0x04000801 RID: 2049
		private readonly EmbeddedWebViewOptions _embeddedWebViewOptions;
	}
}
