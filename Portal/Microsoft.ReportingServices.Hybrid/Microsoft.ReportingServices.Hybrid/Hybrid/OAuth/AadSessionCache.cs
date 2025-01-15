using System;
using System.Web;
using System.Web.SessionState;

namespace Microsoft.ReportingServices.Hybrid.OAuth
{
	// Token: 0x0200000A RID: 10
	internal sealed class AadSessionCache : IAadCache
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002AEC File Offset: 0x00000CEC
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002AF4 File Offset: 0x00000CF4
		private string ResourceId { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002AFD File Offset: 0x00000CFD
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002B05 File Offset: 0x00000D05
		private string ServiceTokenName { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002B0E File Offset: 0x00000D0E
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002B16 File Offset: 0x00000D16
		private string AuthorizationTokenName { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002B1F File Offset: 0x00000D1F
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002B27 File Offset: 0x00000D27
		private string SessionStateName { get; set; }

		// Token: 0x0600002C RID: 44 RVA: 0x00002B30 File Offset: 0x00000D30
		public AadSessionCache(string resourceId)
		{
			this.SessionState = HttpContext.Current.Session;
			this.ResourceId = resourceId;
			this.ServiceTokenName = "AzureAdCache#ServiceToken#" + this.ResourceId;
			this.AuthorizationTokenName = "AzureAdCache#AuthorizationCode#" + this.ResourceId;
			this.SessionStateName = "AzureAdCache#AadOAuthState#" + this.ResourceId;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002B9C File Offset: 0x00000D9C
		public void SaveServiceTokenInCache(ServiceToken value)
		{
			this.SessionState[this.ServiceTokenName] = value;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public ServiceToken GetServiceTokenFromCache()
		{
			return this.SessionState[this.ServiceTokenName] as ServiceToken;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public void RemoveServiceTokenFromCache()
		{
			this.SessionState.Remove(this.ServiceTokenName);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002BDB File Offset: 0x00000DDB
		public void SaveAuthorizationCodeInCache(string value)
		{
			this.SessionState[this.AuthorizationTokenName] = value;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002BEF File Offset: 0x00000DEF
		public string GetAuthorizationCodeFromCache()
		{
			return this.SessionState[this.AuthorizationTokenName] as string;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002C07 File Offset: 0x00000E07
		public void SetSessionState(string value)
		{
			this.SessionState[this.SessionStateName] = value;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002C1B File Offset: 0x00000E1B
		public string GetSessionState()
		{
			return this.SessionState[this.SessionStateName] as string;
		}

		// Token: 0x04000044 RID: 68
		private const string SessionInfoPrefix = "AzureAdCache#";

		// Token: 0x04000045 RID: 69
		private const string ServiceTokenPrefix = "ServiceToken#";

		// Token: 0x04000046 RID: 70
		private const string AuthroizationCodePrefix = "AuthorizationCode#";

		// Token: 0x04000047 RID: 71
		private const string AadAuthStatePrefix = "AadOAuthState#";

		// Token: 0x0400004C RID: 76
		private readonly HttpSessionState SessionState;
	}
}
