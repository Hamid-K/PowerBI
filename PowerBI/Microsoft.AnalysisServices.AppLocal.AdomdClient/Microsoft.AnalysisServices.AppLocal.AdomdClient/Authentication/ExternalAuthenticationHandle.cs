using System;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x02000104 RID: 260
	internal sealed class ExternalAuthenticationHandle : AuthenticationHandle
	{
		// Token: 0x06000F09 RID: 3849 RVA: 0x00033022 File Offset: 0x00031222
		public ExternalAuthenticationHandle(string accessToken, string authenticationScheme)
			: base(AuthenticationEndpoint.Unknown, null, null)
		{
			this.accessToken = accessToken;
			this.authenticationScheme = authenticationScheme;
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0003303B File Offset: 0x0003123B
		public override string Principal
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x00033042 File Offset: 0x00031242
		public override string AuthenticationScheme
		{
			get
			{
				return this.authenticationScheme;
			}
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0003304A File Offset: 0x0003124A
		public override string GetAccessToken()
		{
			return this.accessToken;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00033052 File Offset: 0x00031252
		public override long GetRefreshByTimeAsFileTime()
		{
			return 0L;
		}

		// Token: 0x040008A5 RID: 2213
		private readonly string accessToken;

		// Token: 0x040008A6 RID: 2214
		private readonly string authenticationScheme;
	}
}
