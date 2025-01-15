using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;

namespace Microsoft.AnalysisServices.AdomdClient.Security
{
	// Token: 0x0200015F RID: 351
	internal sealed class WindowsIdentityUserContext : UserContext
	{
		// Token: 0x060010F3 RID: 4339 RVA: 0x0003ADB4 File Offset: 0x00038FB4
		public WindowsIdentityUserContext(WindowsIdentity identity, ICredentials credentials = null, bool applyPrincipal = false)
		{
			this.identity = identity;
			this.credentials = credentials;
			if (credentials != null)
			{
				this.sid = this.identity.User.ToString();
			}
			this.applyPrincipal = applyPrincipal;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0003ADEA File Offset: 0x00038FEA
		public static UserContext GetCurrent(bool applyCredentials = false, bool applyPrincipal = false)
		{
			return new WindowsIdentityUserContext(WindowsIdentity.GetCurrent(), applyCredentials ? CredentialCache.DefaultCredentials : null, applyPrincipal);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0003AE02 File Offset: 0x00039002
		public override bool TryGetCredentials(out ICredentials credentials, out string groupName)
		{
			if (this.credentials != null)
			{
				credentials = this.credentials;
				groupName = this.sid;
				return true;
			}
			return base.TryGetCredentials(out credentials, out groupName);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0003AE28 File Offset: 0x00039028
		protected override void ExecuteInUserContextImpl(Action action)
		{
			if (this.applyPrincipal)
			{
				IPrincipal currentPrincipal = Thread.CurrentPrincipal;
				try
				{
					Thread.CurrentPrincipal = new WindowsPrincipal(this.identity);
					using (this.identity.Impersonate())
					{
						action();
						return;
					}
				}
				finally
				{
					Thread.CurrentPrincipal = currentPrincipal;
				}
			}
			using (this.identity.Impersonate())
			{
				action();
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0003AEC0 File Offset: 0x000390C0
		protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
		{
			if (this.applyPrincipal)
			{
				IPrincipal currentPrincipal = Thread.CurrentPrincipal;
				try
				{
					Thread.CurrentPrincipal = new WindowsPrincipal(this.identity);
					using (this.identity.Impersonate())
					{
						return action();
					}
				}
				finally
				{
					Thread.CurrentPrincipal = currentPrincipal;
				}
			}
			TResult tresult;
			using (this.identity.Impersonate())
			{
				tresult = action();
			}
			return tresult;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0003AF5C File Offset: 0x0003915C
		protected override void UpdateHttpRequestImpl(HttpWebRequest request)
		{
			if (this.credentials != null)
			{
				request.Credentials = this.credentials;
				request.ConnectionGroupName = this.sid;
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0003AF7E File Offset: 0x0003917E
		protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
		{
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0003AF80 File Offset: 0x00039180
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.identity.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000B3D RID: 2877
		private WindowsIdentity identity;

		// Token: 0x04000B3E RID: 2878
		private ICredentials credentials;

		// Token: 0x04000B3F RID: 2879
		private string sid;

		// Token: 0x04000B40 RID: 2880
		private bool applyPrincipal;
	}
}
