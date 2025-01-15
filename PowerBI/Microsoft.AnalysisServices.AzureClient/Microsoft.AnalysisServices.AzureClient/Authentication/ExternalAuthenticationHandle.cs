using System;

namespace Microsoft.AnalysisServices.AzureClient.Authentication
{
	// Token: 0x0200001D RID: 29
	internal sealed class ExternalAuthenticationHandle : AuthenticationHandle
	{
		// Token: 0x060000CC RID: 204 RVA: 0x000040F2 File Offset: 0x000022F2
		public ExternalAuthenticationHandle(string accessToken, string authenticationScheme)
			: base(AuthenticationEndpoint.Unknown, null, null)
		{
			this.accessToken = accessToken;
			this.authenticationScheme = authenticationScheme;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000410B File Offset: 0x0000230B
		public override string Principal
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004112 File Offset: 0x00002312
		public override string AuthenticationScheme
		{
			get
			{
				return this.authenticationScheme;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000411A File Offset: 0x0000231A
		public override string GetAccessToken()
		{
			return this.accessToken;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004122 File Offset: 0x00002322
		public override long GetRefreshByTimeAsFileTime()
		{
			return 0L;
		}

		// Token: 0x04000075 RID: 117
		private readonly string accessToken;

		// Token: 0x04000076 RID: 118
		private readonly string authenticationScheme;
	}
}
