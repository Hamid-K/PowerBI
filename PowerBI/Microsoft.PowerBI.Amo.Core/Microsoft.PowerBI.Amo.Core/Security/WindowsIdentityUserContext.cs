using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;

namespace Microsoft.AnalysisServices.Security
{
	// Token: 0x02000155 RID: 341
	internal sealed class WindowsIdentityUserContext : UserContext
	{
		// Token: 0x0600118F RID: 4495 RVA: 0x0003DA74 File Offset: 0x0003BC74
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

		// Token: 0x06001190 RID: 4496 RVA: 0x0003DAAA File Offset: 0x0003BCAA
		public static UserContext GetCurrent(bool applyCredentials = false, bool applyPrincipal = false)
		{
			return new WindowsIdentityUserContext(WindowsIdentity.GetCurrent(), applyCredentials ? CredentialCache.DefaultCredentials : null, applyPrincipal);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0003DAC2 File Offset: 0x0003BCC2
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

		// Token: 0x06001192 RID: 4498 RVA: 0x0003DAE8 File Offset: 0x0003BCE8
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

		// Token: 0x06001193 RID: 4499 RVA: 0x0003DB80 File Offset: 0x0003BD80
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

		// Token: 0x06001194 RID: 4500 RVA: 0x0003DC1C File Offset: 0x0003BE1C
		protected override void UpdateHttpRequestImpl(HttpWebRequest request)
		{
			if (this.credentials != null)
			{
				request.Credentials = this.credentials;
				request.ConnectionGroupName = this.sid;
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0003DC3E File Offset: 0x0003BE3E
		protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
		{
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0003DC40 File Offset: 0x0003BE40
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.identity.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000B03 RID: 2819
		private WindowsIdentity identity;

		// Token: 0x04000B04 RID: 2820
		private ICredentials credentials;

		// Token: 0x04000B05 RID: 2821
		private string sid;

		// Token: 0x04000B06 RID: 2822
		private bool applyPrincipal;
	}
}
