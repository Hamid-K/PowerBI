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
		// Token: 0x06000FB4 RID: 4020 RVA: 0x00036209 File Offset: 0x00034409
		public MsoIdUserContext()
		{
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00036211 File Offset: 0x00034411
		public MsoIdUserContext(string userName, string password)
		{
			this.userName = userName;
			this.password = password;
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x00036227 File Offset: 0x00034427
		// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x0003622F File Offset: 0x0003442F
		public bool CacheAccessToken { get; set; }

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x00036238 File Offset: 0x00034438
		// (set) Token: 0x06000FB9 RID: 4025 RVA: 0x00036240 File Offset: 0x00034440
		public MsoIdAuthenticationExceptionHandler OnAuthenticationException { get; set; }

		// Token: 0x06000FBA RID: 4026 RVA: 0x00036249 File Offset: 0x00034449
		protected override void ExecuteInUserContextImpl(Action action)
		{
			action();
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00036251 File Offset: 0x00034451
		protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
		{
			return action();
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0003625C File Offset: 0x0003445C
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

		// Token: 0x06000FBD RID: 4029 RVA: 0x000362D8 File Offset: 0x000344D8
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

		// Token: 0x06000FBE RID: 4030 RVA: 0x0003634C File Offset: 0x0003454C
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

		// Token: 0x04000A71 RID: 2673
		private const string HttpAuthenticationMsoIDSchemeName = "MsoID";

		// Token: 0x04000A72 RID: 2674
		private string userName;

		// Token: 0x04000A73 RID: 2675
		private string password;

		// Token: 0x04000A74 RID: 2676
		private string accessToken;
	}
}
