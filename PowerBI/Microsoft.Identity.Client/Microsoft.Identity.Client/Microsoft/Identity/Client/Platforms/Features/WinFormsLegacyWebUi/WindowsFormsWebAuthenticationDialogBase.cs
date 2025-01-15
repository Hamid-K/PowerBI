using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs;
using Microsoft.Identity.Client.UI;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi
{
	// Token: 0x020001B3 RID: 435
	[ComVisible(true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract partial class WindowsFormsWebAuthenticationDialogBase : Form
	{
		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x00041873 File Offset: 0x0003FA73
		// (set) Token: 0x06001393 RID: 5011 RVA: 0x0004187B File Offset: 0x0003FA7B
		internal RequestContext RequestContext { get; set; }

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x00041884 File Offset: 0x0003FA84
		// (set) Token: 0x06001395 RID: 5013 RVA: 0x0004188C File Offset: 0x0003FA8C
		protected IWin32Window ownerWindow { get; set; }

		// Token: 0x06001396 RID: 5014 RVA: 0x00041898 File Offset: 0x0003FA98
		protected WindowsFormsWebAuthenticationDialogBase(object ownerWindow)
		{
			if (WindowsFormsWebAuthenticationDialogBase.NativeMethods.SetQueryNetSessionCount(WindowsFormsWebAuthenticationDialogBase.NativeMethods.SessionOp.SESSION_QUERY) == 0)
			{
				WindowsFormsWebAuthenticationDialogBase.NativeMethods.SetQueryNetSessionCount(WindowsFormsWebAuthenticationDialogBase.NativeMethods.SessionOp.SESSION_INCREMENT);
			}
			if (ownerWindow == null)
			{
				this.ownerWindow = null;
			}
			else
			{
				IWin32Window win32Window = ownerWindow as IWin32Window;
				if (win32Window != null)
				{
					this.ownerWindow = win32Window;
				}
				else
				{
					if (!(ownerWindow is IntPtr))
					{
						throw new MsalException("invalid_owner_window_type", "Invalid owner window type. Expected types are IWin32Window or IntPtr (for window handle).");
					}
					IntPtr intPtr = (IntPtr)ownerWindow;
					this.ownerWindow = new Win32Window(intPtr);
				}
			}
			this._webBrowser = new CustomWebBrowser();
			this._webBrowser.PreviewKeyDown += this.WebBrowser_PreviewKeyDown;
			this.InitializeComponent();
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x0004192E File Offset: 0x0003FB2E
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x00041936 File Offset: 0x0003FB36
		internal AuthorizationResult Result { get; set; }

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x0004193F File Offset: 0x0003FB3F
		public WebBrowser WebBrowser
		{
			get
			{
				return this._webBrowser;
			}
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00041947 File Offset: 0x0003FB47
		private void WebBrowser_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Back)
			{
				this._key = Keys.Back;
			}
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0004195C File Offset: 0x0003FB5C
		protected virtual void WebBrowserBeforeNavigateHandler(object sender, WebBrowserBeforeNavigateEventArgs e)
		{
			if (base.DialogResult == DialogResult.OK)
			{
				e.Cancel = true;
				return;
			}
			if (this._webBrowser.IsDisposed)
			{
				e.Cancel = true;
				return;
			}
			if (this._key == Keys.Back)
			{
				this._key = Keys.None;
				e.Cancel = true;
			}
			if (string.IsNullOrEmpty(e.Url))
			{
				this.RequestContext.Logger.Verbose(() => "[Legacy WebView] URL in BeforeNavigate is null or empty.");
				e.Cancel = true;
				return;
			}
			Uri uri = new Uri(e.Url);
			e.Cancel = this.CheckForClosingUrl(uri, e.PostData);
			if (uri.Scheme.Equals("browser", StringComparison.OrdinalIgnoreCase))
			{
				Process.Start(uri.AbsoluteUri.Replace("browser://", "https://"));
				e.Cancel = true;
			}
			if (!e.Cancel)
			{
				string urlDecode = CoreHelpers.UrlDecode(e.Url);
				this.RequestContext.Logger.VerbosePii(() => string.Format(CultureInfo.InvariantCulture, "[Legacy WebView] Navigating to '{0}'.", urlDecode), () => string.Empty);
			}
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00041A98 File Offset: 0x0003FC98
		private void WebBrowserNavigatedHandler(object sender, WebBrowserNavigatedEventArgs e)
		{
			if (this.CheckForClosingUrl(e.Url, null))
			{
				return;
			}
			string urlDecode = CoreHelpers.UrlDecode(e.Url.ToString());
			this.RequestContext.Logger.VerbosePii(() => string.Format(CultureInfo.InvariantCulture, "[Legacy WebView] Navigated to '{0}'.", urlDecode), () => string.Empty);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00041B0C File Offset: 0x0003FD0C
		protected virtual void WebBrowserNavigateErrorHandler(object sender, WebBrowserNavigateErrorEventArgs e)
		{
			if (base.DialogResult == DialogResult.OK)
			{
				e.Cancel = true;
				return;
			}
			if (this._webBrowser.IsDisposed)
			{
				e.Cancel = true;
				return;
			}
			if (this._webBrowser.ActiveXInstance != e.WebBrowserActiveXInstance)
			{
				return;
			}
			if (e.StatusCode >= 300 && e.StatusCode < 400)
			{
				return;
			}
			e.Cancel = true;
			this.StopWebBrowser();
			this.OnNavigationCanceled(e.StatusCode);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00041B88 File Offset: 0x0003FD88
		private bool CheckForClosingUrl(Uri url, byte[] postData = null)
		{
			bool flag = false;
			if (url.Authority.Equals(this._desiredCallbackUri.Authority, StringComparison.OrdinalIgnoreCase) && url.AbsolutePath.Equals(this._desiredCallbackUri.AbsolutePath))
			{
				this.RequestContext.Logger.Info("[Legacy WebView] Redirect URI was reached. Stopping WebView navigation...");
				this.Result = AuthorizationResult.FromPostData(postData);
				flag = true;
			}
			if (!flag && !EmbeddedUiCommon.IsAllowedIeOrEdgeAuthorizationRedirect(url))
			{
				this.RequestContext.Logger.Error(string.Format(CultureInfo.InvariantCulture, "[Legacy WebView] Redirection to non-HTTPS uri: {0} - WebView1 will fail...", url));
				this.Result = AuthorizationResult.FromStatus(AuthorizationStatus.ErrorHttp, "non_https_redirect_failed", "Non-HTTPS URL redirect is not supported in a web view. This error happens when the authorization flow, which collects user credentials, gets redirected to a page that is not supported, for example if the redirect occurs over http. This error does not trigger for the final redirect, which can be http://localhost, but for intermediary redirects.Mitigation: This usually happens when using a federated directory which is not setup correctly. ");
				flag = true;
			}
			if (flag)
			{
				this.StopWebBrowser();
				this.OnClosingUrl();
			}
			return flag;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x00041C3E File Offset: 0x0003FE3E
		private void StopWebBrowser()
		{
			this.InvokeHandlingOwnerWindow(delegate
			{
				if (this._webBrowser.IsDisposed || !this._webBrowser.IsBusy)
				{
					return;
				}
				this.RequestContext.Logger.Verbose(() => string.Format(CultureInfo.InvariantCulture, "[Legacy WebView] WebBrowser state: IsBusy: {0}, ReadyState: {1}, Created: {2}, Disposing: {3}, IsDisposed: {4}, IsOffline: {5}", new object[]
				{
					this._webBrowser.IsBusy,
					this._webBrowser.ReadyState,
					this._webBrowser.Created,
					this._webBrowser.Disposing,
					this._webBrowser.IsDisposed,
					this._webBrowser.IsOffline
				}));
				this._webBrowser.Stop();
				this.RequestContext.Logger.Verbose(() => string.Format(CultureInfo.InvariantCulture, "[Legacy WebView] WebBrowser state (after Stop): IsBusy: {0}, ReadyState: {1}, Created: {2}, Disposing: {3}, IsDisposed: {4}, IsOffline: {5}", new object[]
				{
					this._webBrowser.IsBusy,
					this._webBrowser.ReadyState,
					this._webBrowser.Created,
					this._webBrowser.Disposing,
					this._webBrowser.IsDisposed,
					this._webBrowser.IsOffline
				}));
			});
		}

		// Token: 0x060013A0 RID: 5024
		protected abstract void OnClosingUrl();

		// Token: 0x060013A1 RID: 5025
		protected abstract void OnNavigationCanceled(int statusCode);

		// Token: 0x060013A2 RID: 5026 RVA: 0x00041C54 File Offset: 0x0003FE54
		internal AuthorizationResult AuthenticateAAD(Uri requestUri, Uri callbackUri, CancellationToken cancellationToken)
		{
			this._desiredCallbackUri = callbackUri;
			this.Result = null;
			this._webBrowser.BeforeNavigate += this.WebBrowserBeforeNavigateHandler;
			this._webBrowser.Navigated += this.WebBrowserNavigatedHandler;
			this._webBrowser.NavigateError += this.WebBrowserNavigateErrorHandler;
			if (this.RequestContext.ServiceBundle.Config.IsWebviewSsoPolicyEnabled)
			{
				IEnumerable<KeyValuePair<string, string>> ssoPolicyHeaders = this.RequestContext.ServiceBundle.Config.BrokerCreatorFunc(null, this.RequestContext.ServiceBundle.Config, this.RequestContext.Logger).GetSsoPolicyHeaders();
				string text = "";
				foreach (KeyValuePair<string, string> keyValuePair in ssoPolicyHeaders)
				{
					text = string.Concat(new string[]
					{
						text,
						keyValuePair.Key,
						":",
						keyValuePair.Value,
						Environment.NewLine
					});
				}
				this._webBrowser.Navigate(requestUri, null, null, text);
			}
			else
			{
				this._webBrowser.Navigate(requestUri);
			}
			this.OnAuthenticate(cancellationToken);
			return this.Result;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00041DA4 File Offset: 0x0003FFA4
		protected virtual void OnAuthenticate(CancellationToken cancellationToken)
		{
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00041DA8 File Offset: 0x0003FFA8
		protected void InvokeHandlingOwnerWindow(Action action)
		{
			if (this.ownerWindow != null)
			{
				Control control = this.ownerWindow as Control;
				if (control != null)
				{
					control.Invoke(action);
					return;
				}
			}
			action();
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00041DDB File Offset: 0x0003FFDB
		protected void InvokeOnly(Action action)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(action);
				return;
			}
			action();
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00041E1C File Offset: 0x0004001C
		protected MsalClientException CreateExceptionForAuthenticationUiFailed(int statusCode)
		{
			string text;
			if (WindowsFormsWebAuthenticationDialogBase.NavigateErrorStatus.Messages.TryGetValue(statusCode, out text))
			{
				string text2 = "The browser based authentication dialog failed to complete. Reason: {0}";
				string text3 = string.Format(CultureInfo.InvariantCulture, text2, text);
				return new MsalClientException("authentication_ui_failed", text3);
			}
			string text4 = "The browser based authentication dialog failed to complete for an unknown reason. StatusCode: {0}";
			string text5 = string.Format(CultureInfo.InvariantCulture, text4, statusCode);
			return new MsalClientException("authentication_ui_failed", text5);
		}

		// Token: 0x04000803 RID: 2051
		private const int UIWidth = 566;

		// Token: 0x04000804 RID: 2052
		private static readonly NavigateErrorStatus NavigateErrorStatus = new NavigateErrorStatus();

		// Token: 0x04000805 RID: 2053
		private readonly CustomWebBrowser _webBrowser;

		// Token: 0x04000806 RID: 2054
		private Uri _desiredCallbackUri;

		// Token: 0x04000807 RID: 2055
		private Keys _key;

		// Token: 0x04000809 RID: 2057
		private Panel _webBrowserPanel;

		// Token: 0x02000441 RID: 1089
		internal static class NativeMethods
		{
			// Token: 0x06001F7A RID: 8058
			[DllImport("IEFRAME.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
			internal static extern int SetQueryNetSessionCount(WindowsFormsWebAuthenticationDialogBase.NativeMethods.SessionOp sessionOp);

			// Token: 0x0200054B RID: 1355
			internal enum SessionOp
			{
				// Token: 0x040017AE RID: 6062
				SESSION_QUERY,
				// Token: 0x040017AF RID: 6063
				SESSION_INCREMENT,
				// Token: 0x040017B0 RID: 6064
				SESSION_DECREMENT
			}
		}
	}
}
