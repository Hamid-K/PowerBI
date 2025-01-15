using System;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x02000104 RID: 260
	internal sealed class ExternalAuthenticationHandle : AuthenticationHandle
	{
		// Token: 0x06000EFC RID: 3836 RVA: 0x00032CF2 File Offset: 0x00030EF2
		public ExternalAuthenticationHandle(string accessToken, string authenticationScheme)
			: base(AuthenticationEndpoint.Unknown, null, null)
		{
			this.accessToken = accessToken;
			this.authenticationScheme = authenticationScheme;
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x00032D0B File Offset: 0x00030F0B
		public override string Principal
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00032D12 File Offset: 0x00030F12
		public override string AuthenticationScheme
		{
			get
			{
				return this.authenticationScheme;
			}
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00032D1A File Offset: 0x00030F1A
		public override string GetAccessToken()
		{
			return this.accessToken;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00032D22 File Offset: 0x00030F22
		public override long GetRefreshByTimeAsFileTime()
		{
			return 0L;
		}

		// Token: 0x04000898 RID: 2200
		private readonly string accessToken;

		// Token: 0x04000899 RID: 2201
		private readonly string authenticationScheme;
	}
}
