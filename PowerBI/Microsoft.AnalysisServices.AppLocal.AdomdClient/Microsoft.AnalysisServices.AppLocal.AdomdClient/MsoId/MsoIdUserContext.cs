using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AnalysisServices.AdomdClient.Security;

namespace Microsoft.AnalysisServices.AdomdClient.MsoId
{
	// Token: 0x0200012D RID: 301
	internal sealed class MsoIdUserContext : UserContext
	{
		// Token: 0x06000FC1 RID: 4033 RVA: 0x00036539 File Offset: 0x00034739
		public MsoIdUserContext()
		{
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00036541 File Offset: 0x00034741
		public MsoIdUserContext(string userName, string password)
		{
			this.userName = userName;
			this.password = password;
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00036557 File Offset: 0x00034757
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x0003655F File Offset: 0x0003475F
		public bool CacheAccessToken { get; set; }

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00036568 File Offset: 0x00034768
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x00036570 File Offset: 0x00034770
		public MsoIdAuthenticationExceptionHandler OnAuthenticationException { get; set; }

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00036579 File Offset: 0x00034779
		protected override void ExecuteInUserContextImpl(Action action)
		{
			action();
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00036581 File Offset: 0x00034781
		protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
		{
			return action();
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0003658C File Offset: 0x0003478C
		protected override void UpdateHttpRequestImpl(HttpWebRequest request)
		{
			try
			{
				if (string.IsNullOrEmpty(this.accessToken) || !this.CacheAccessToken)
				{
					this.accessToken = MsoIdAuthenticationProvider.Authenticate(this.userName, this.password);
				}
				request.Headers.Add(HttpRequestHeader.Authorization, string.Format("{0} {1}", "MsoID", this.accessToken));
			}
			catch (MsoIdAuthenticationException ex)
			{
				if (!this.ShouldIgnoreError(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00036608 File Offset: 0x00034808
		protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
		{
			try
			{
				if (string.IsNullOrEmpty(this.accessToken) || !this.CacheAccessToken)
				{
					this.accessToken = MsoIdAuthenticationProvider.Authenticate(this.userName, this.password);
				}
				request.Headers.Authorization = new AuthenticationHeaderValue("MsoID", this.accessToken);
			}
			catch (MsoIdAuthenticationException ex)
			{
				if (!this.ShouldIgnoreError(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0003667C File Offset: 0x0003487C
		private bool ShouldIgnoreError(MsoIdAuthenticationException e)
		{
			if (this.OnAuthenticationException == null)
			{
				return false;
			}
			bool flag;
			this.OnAuthenticationException(e, out flag);
			return flag;
		}

		// Token: 0x04000A7E RID: 2686
		private const string HttpAuthenticationMsoIDSchemeName = "MsoID";

		// Token: 0x04000A7F RID: 2687
		private string userName;

		// Token: 0x04000A80 RID: 2688
		private string password;

		// Token: 0x04000A81 RID: 2689
		private string accessToken;
	}
}
