using System;

namespace Microsoft.ReportingServices.Hybrid.OAuth
{
	// Token: 0x0200000D RID: 13
	internal interface IAadCache
	{
		// Token: 0x06000037 RID: 55
		void SaveServiceTokenInCache(ServiceToken value);

		// Token: 0x06000038 RID: 56
		ServiceToken GetServiceTokenFromCache();

		// Token: 0x06000039 RID: 57
		void RemoveServiceTokenFromCache();

		// Token: 0x0600003A RID: 58
		void SaveAuthorizationCodeInCache(string value);

		// Token: 0x0600003B RID: 59
		string GetAuthorizationCodeFromCache();

		// Token: 0x0600003C RID: 60
		void SetSessionState(string value);

		// Token: 0x0600003D RID: 61
		string GetSessionState();
	}
}
