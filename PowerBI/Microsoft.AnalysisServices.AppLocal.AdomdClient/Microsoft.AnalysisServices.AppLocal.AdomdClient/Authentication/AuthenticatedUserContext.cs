using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AnalysisServices.AdomdClient.Security;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x020000FA RID: 250
	internal sealed class AuthenticatedUserContext : UserContext
	{
		// Token: 0x06000EAC RID: 3756 RVA: 0x00031C6C File Offset: 0x0002FE6C
		public AuthenticatedUserContext(AuthenticationHandle handle)
		{
			this.handle = handle;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00031C7B File Offset: 0x0002FE7B
		protected override void ExecuteInUserContextImpl(Action action)
		{
			action();
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00031C83 File Offset: 0x0002FE83
		protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
		{
			return action();
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00031C8B File Offset: 0x0002FE8B
		protected override void UpdateHttpRequestImpl(HttpWebRequest request)
		{
			request.Headers.Add(HttpRequestHeader.Authorization, string.Format("{0} {1}", this.handle.AuthenticationScheme, this.handle.GetAccessToken()));
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x00031CBA File Offset: 0x0002FEBA
		protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
		{
			request.Headers.Authorization = new AuthenticationHeaderValue(this.handle.AuthenticationScheme, this.handle.GetAccessToken());
		}

		// Token: 0x04000861 RID: 2145
		private AuthenticationHandle handle;
	}
}
