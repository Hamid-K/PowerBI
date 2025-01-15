using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.UI;

namespace Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi
{
	// Token: 0x020001AA RID: 426
	internal class SilentWebUI : WebUI, IDisposable
	{
		// Token: 0x06001349 RID: 4937 RVA: 0x00040ED6 File Offset: 0x0003F0D6
		public SilentWebUI(CoreUIParent parent, RequestContext requestContext)
		{
			base.OwnerWindow = ((parent != null) ? parent.OwnerWindow : null);
			base.SynchronizationContext = ((parent != null) ? parent.SynchronizationContext : null);
			base.RequestContext = requestContext;
			this._threadInitializedEvent = new ManualResetEvent(false);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00040F15 File Offset: 0x0003F115
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00040F24 File Offset: 0x0003F124
		~SilentWebUI()
		{
			this.Dispose(false);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00040F54 File Offset: 0x0003F154
		private void WaitForCompletionOrTimeout(Thread uiThread)
		{
			long num = 10000L;
			long ticks = DateTime.Now.Ticks;
			if (this._threadInitializedEvent.WaitOne((int)num))
			{
				long num2 = (DateTime.Now.Ticks - ticks) / 10000L;
				num -= num2;
				if (!uiThread.Join((num > 0L) ? ((int)num) : 0))
				{
					base.RequestContext.Logger.Info("Silent login thread did not complete on time.");
					this._formsSyncContext.Post(delegate(object _)
					{
						this._dialog.CloseBrowser();
					}, null);
				}
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00040FDF File Offset: 0x0003F1DF
		private Thread StartUIThread()
		{
			Thread thread = new Thread(delegate
			{
				try
				{
					this._formsSyncContext = new WindowsFormsSynchronizationContext();
					this._dialog = new SilentWindowsFormsAuthenticationDialog(base.OwnerWindow)
					{
						NavigationWaitMiliSecs = 250,
						RequestContext = base.RequestContext
					};
					this._dialog.Done += this.UIDoneHandler;
					this._threadInitializedEvent.Set();
					this._dialog.AuthenticateAAD(base.RequestUri, base.CallbackUri, CancellationToken.None);
					Application.Run();
					this._result = this._dialog.Result;
				}
				catch (Exception ex)
				{
					base.RequestContext.Logger.ErrorPii(ex);
					this._uiException = ex;
				}
			});
			thread.SetApartmentState(ApartmentState.STA);
			thread.IsBackground = true;
			thread.Start();
			return thread;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00041008 File Offset: 0x0003F208
		protected override AuthorizationResult OnAuthenticate(CancellationToken cancellationToken)
		{
			if (null == base.CallbackUri)
			{
				throw new InvalidOperationException("CallbackUri cannot be null");
			}
			Thread thread = this.StartUIThread();
			this.WaitForCompletionOrTimeout(thread);
			this.Cleanup();
			this.ThrowIfTransferredException();
			if (this._result == null)
			{
				throw new MsalUiRequiredException("no_prompt_failed", "One of two conditions was encountered: 1. The Prompt.Never flag was passed, but the constraint could not be honored, because user interaction was required. 2. An error occurred during a silent web authentication that prevented the HTTP authentication flow from completing in a short enough time frame. ");
			}
			return this._result;
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00041068 File Offset: 0x0003F268
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					if (this._threadInitializedEvent != null)
					{
						this._threadInitializedEvent.Dispose();
						this._threadInitializedEvent = null;
					}
					if (this._formsSyncContext != null)
					{
						this._formsSyncContext.Dispose();
						this._formsSyncContext = null;
					}
				}
				this._disposed = true;
			}
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x000410BB File Offset: 0x0003F2BB
		private void Cleanup()
		{
			this._threadInitializedEvent.Dispose();
			this._threadInitializedEvent = null;
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x000410CF File Offset: 0x0003F2CF
		private void ThrowIfTransferredException()
		{
			if (this._uiException != null)
			{
				throw this._uiException;
			}
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x000410E0 File Offset: 0x0003F2E0
		private void UIDoneHandler(object sender, SilentWebUIDoneEventArgs e)
		{
			if (this._uiException == null)
			{
				this._uiException = e.TransferredException;
			}
			((SilentWindowsFormsAuthenticationDialog)sender).Dispose();
			Application.ExitThread();
		}

		// Token: 0x040007E1 RID: 2017
		private const int NavigationWaitMiliSecs = 250;

		// Token: 0x040007E2 RID: 2018
		private const int NavigationOverallTimeout = 10000;

		// Token: 0x040007E3 RID: 2019
		private SilentWindowsFormsAuthenticationDialog _dialog;

		// Token: 0x040007E4 RID: 2020
		private bool _disposed;

		// Token: 0x040007E5 RID: 2021
		private WindowsFormsSynchronizationContext _formsSyncContext;

		// Token: 0x040007E6 RID: 2022
		private AuthorizationResult _result;

		// Token: 0x040007E7 RID: 2023
		private ManualResetEvent _threadInitializedEvent;

		// Token: 0x040007E8 RID: 2024
		private Exception _uiException;
	}
}
