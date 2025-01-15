using System;
using Microsoft.ReportingServices.Hybrid.OAuth;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000222 RID: 546
	internal sealed class AADCatalogCache : IAadCache
	{
		// Token: 0x060013BF RID: 5055 RVA: 0x0004A126 File Offset: 0x00048326
		public AADCatalogCache(Security securityStorage)
		{
			this.m_securityStorage = securityStorage;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0004A138 File Offset: 0x00048338
		public string GetAuthorizationCodeFromCache()
		{
			ServiceToken serviceTokenFromCatalog = this.m_securityStorage.GetServiceTokenFromCatalog();
			if (serviceTokenFromCatalog == null)
			{
				return null;
			}
			return serviceTokenFromCatalog.IdToken;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0004A15C File Offset: 0x0004835C
		public ServiceToken GetServiceTokenFromCache()
		{
			return this.m_securityStorage.GetServiceTokenFromCatalog();
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0004A169 File Offset: 0x00048369
		public string GetSessionState()
		{
			return this.m_sessionState;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0004A171 File Offset: 0x00048371
		public void RemoveServiceTokenFromCache()
		{
			this.m_securityStorage.SetServiceToken(null);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void SaveAuthorizationCodeInCache(string value)
		{
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0004A17F File Offset: 0x0004837F
		public void SaveServiceTokenInCache(ServiceToken value)
		{
			this.m_securityStorage.SetServiceToken(value);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0004A18D File Offset: 0x0004838D
		public void SetSessionState(string value)
		{
			this.m_sessionState = value;
		}

		// Token: 0x040006F3 RID: 1779
		private readonly Security m_securityStorage;

		// Token: 0x040006F4 RID: 1780
		private string m_sessionState;
	}
}
