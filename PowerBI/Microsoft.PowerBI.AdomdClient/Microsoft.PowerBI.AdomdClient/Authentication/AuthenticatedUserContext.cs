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
		// Token: 0x06000E9F RID: 3743 RVA: 0x0003193C File Offset: 0x0002FB3C
		public AuthenticatedUserContext(AuthenticationHandle handle)
		{
			this.handle = handle;
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003194B File Offset: 0x0002FB4B
		protected override void ExecuteInUserContextImpl(Action action)
		{
			action();
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00031953 File Offset: 0x0002FB53
		protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
		{
			return action();
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003195B File Offset: 0x0002FB5B
		protected override void UpdateHttpRequestImpl(HttpWebRequest request)
		{
			request.Headers.Add(HttpRequestHeader.Authorization, string.Format("{0} {1}", this.handle.AuthenticationScheme, this.handle.GetAccessToken()));
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0003198A File Offset: 0x0002FB8A
		protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
		{
			request.Headers.Authorization = new AuthenticationHeaderValue(this.handle.AuthenticationScheme, this.handle.GetAccessToken());
		}

		// Token: 0x04000854 RID: 2132
		private AuthenticationHandle handle;
	}
}
