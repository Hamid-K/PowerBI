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
		// Token: 0x06001100 RID: 4352 RVA: 0x0003B0E4 File Offset: 0x000392E4
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

		// Token: 0x06001101 RID: 4353 RVA: 0x0003B11A File Offset: 0x0003931A
		public static UserContext GetCurrent(bool applyCredentials = false, bool applyPrincipal = false)
		{
			return new WindowsIdentityUserContext(WindowsIdentity.GetCurrent(), applyCredentials ? CredentialCache.DefaultCredentials : null, applyPrincipal);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0003B132 File Offset: 0x00039332
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

		// Token: 0x06001103 RID: 4355 RVA: 0x0003B158 File Offset: 0x00039358
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

		// Token: 0x06001104 RID: 4356 RVA: 0x0003B1F0 File Offset: 0x000393F0
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

		// Token: 0x06001105 RID: 4357 RVA: 0x0003B28C File Offset: 0x0003948C
		protected override void UpdateHttpRequestImpl(HttpWebRequest request)
		{
			if (this.credentials != null)
			{
				request.Credentials = this.credentials;
				request.ConnectionGroupName = this.sid;
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0003B2AE File Offset: 0x000394AE
		protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
		{
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0003B2B0 File Offset: 0x000394B0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.identity.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000B4A RID: 2890
		private WindowsIdentity identity;

		// Token: 0x04000B4B RID: 2891
		private ICredentials credentials;

		// Token: 0x04000B4C RID: 2892
		private string sid;

		// Token: 0x04000B4D RID: 2893
		private bool applyPrincipal;
	}
}
