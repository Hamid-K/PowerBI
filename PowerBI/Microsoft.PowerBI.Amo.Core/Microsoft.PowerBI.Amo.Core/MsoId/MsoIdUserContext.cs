using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AnalysisServices.Security;

namespace Microsoft.AnalysisServices.MsoId
{
	// Token: 0x02000122 RID: 290
	internal sealed class MsoIdUserContext : UserContext
	{
		// Token: 0x0600104F RID: 4175 RVA: 0x00038E3D File Offset: 0x0003703D
		public MsoIdUserContext()
		{
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00038E45 File Offset: 0x00037045
		public MsoIdUserContext(string userName, string password)
		{
			this.userName = userName;
			this.password = password;
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x00038E5B File Offset: 0x0003705B
		// (set) Token: 0x06001052 RID: 4178 RVA: 0x00038E63 File Offset: 0x00037063
		public bool CacheAccessToken { get; set; }

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x00038E6C File Offset: 0x0003706C
		// (set) Token: 0x06001054 RID: 4180 RVA: 0x00038E74 File Offset: 0x00037074
		public MsoIdAuthenticationExceptionHandler OnAuthenticationException { get; set; }

		// Token: 0x06001055 RID: 4181 RVA: 0x00038E7D File Offset: 0x0003707D
		protected override void ExecuteInUserContextImpl(Action action)
		{
			action();
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00038E85 File Offset: 0x00037085
		protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
		{
			return action();
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00038E90 File Offset: 0x00037090
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

		// Token: 0x06001058 RID: 4184 RVA: 0x00038F0C File Offset: 0x0003710C
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

		// Token: 0x06001059 RID: 4185 RVA: 0x00038F80 File Offset: 0x00037180
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

		// Token: 0x04000A37 RID: 2615
		private const string HttpAuthenticationMsoIDSchemeName = "MsoID";

		// Token: 0x04000A38 RID: 2616
		private string userName;

		// Token: 0x04000A39 RID: 2617
		private string password;

		// Token: 0x04000A3A RID: 2618
		private string accessToken;
	}
}
