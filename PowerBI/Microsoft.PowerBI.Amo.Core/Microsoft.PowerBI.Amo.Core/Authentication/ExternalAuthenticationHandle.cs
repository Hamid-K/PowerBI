using System;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x020000F9 RID: 249
	internal sealed class ExternalAuthenticationHandle : AuthenticationHandle
	{
		// Token: 0x06000F98 RID: 3992 RVA: 0x0003593E File Offset: 0x00033B3E
		public ExternalAuthenticationHandle(string accessToken, string authenticationScheme)
			: base(AuthenticationEndpoint.Unknown, null, null)
		{
			this.accessToken = accessToken;
			this.authenticationScheme = authenticationScheme;
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00035957 File Offset: 0x00033B57
		public override string Principal
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x0003595E File Offset: 0x00033B5E
		public override string AuthenticationScheme
		{
			get
			{
				return this.authenticationScheme;
			}
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00035966 File Offset: 0x00033B66
		public override string GetAccessToken()
		{
			return this.accessToken;
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0003596E File Offset: 0x00033B6E
		public override long GetRefreshByTimeAsFileTime()
		{
			return 0L;
		}

		// Token: 0x0400085E RID: 2142
		private readonly string accessToken;

		// Token: 0x0400085F RID: 2143
		private readonly string authenticationScheme;
	}
}
