using System;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.AzureClient.Authentication;

namespace Microsoft.AnalysisServices.AzureClient
{
	// Token: 0x02000011 RID: 17
	[Guid("1FCDEF9E-8540-40C6-AE40-298BEB006486")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public class TokenHolder : ITokenHolder
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000027B8 File Offset: 0x000009B8
		internal TokenHolder(AuthenticationHandle handle)
		{
			this.handle = handle;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000027C7 File Offset: 0x000009C7
		public string TenantId
		{
			get
			{
				return this.handle.Tenant;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000027D4 File Offset: 0x000009D4
		public string IdentityProvider
		{
			get
			{
				return this.handle.Provider;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000027E1 File Offset: 0x000009E1
		public string AuthenticationScheme
		{
			get
			{
				return this.handle.AuthenticationScheme;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027EE File Offset: 0x000009EE
		public string GetValidAccessToken()
		{
			return this.handle.GetAccessToken();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027FB File Offset: 0x000009FB
		public long GetReAcquireOn()
		{
			return this.handle.GetRefreshByTimeAsFileTime();
		}

		// Token: 0x0400002E RID: 46
		private AuthenticationHandle handle;
	}
}
