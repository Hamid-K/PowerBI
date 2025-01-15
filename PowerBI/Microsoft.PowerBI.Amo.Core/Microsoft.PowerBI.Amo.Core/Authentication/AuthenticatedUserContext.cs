using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AnalysisServices.Security;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x020000EF RID: 239
	internal sealed class AuthenticatedUserContext : UserContext
	{
		// Token: 0x06000F3D RID: 3901 RVA: 0x00034614 File Offset: 0x00032814
		public AuthenticatedUserContext(AuthenticationHandle handle)
		{
			this.handle = handle;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00034623 File Offset: 0x00032823
		protected override void ExecuteInUserContextImpl(Action action)
		{
			action();
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0003462B File Offset: 0x0003282B
		protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
		{
			return action();
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00034633 File Offset: 0x00032833
		protected override void UpdateHttpRequestImpl(HttpWebRequest request)
		{
			request.Headers.Add(HttpRequestHeader.Authorization, string.Format("{0} {1}", this.handle.AuthenticationScheme, this.handle.GetAccessToken()));
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00034662 File Offset: 0x00032862
		protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
		{
			request.Headers.Authorization = new AuthenticationHeaderValue(this.handle.AuthenticationScheme, this.handle.GetAccessToken());
		}

		// Token: 0x0400081D RID: 2077
		private AuthenticationHandle handle;
	}
}
