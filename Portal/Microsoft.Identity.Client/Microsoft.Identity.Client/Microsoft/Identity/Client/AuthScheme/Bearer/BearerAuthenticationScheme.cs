using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.AuthScheme.Bearer
{
	// Token: 0x020002CA RID: 714
	internal class BearerAuthenticationScheme : IAuthenticationScheme
	{
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x00056CA3 File Offset: 0x00054EA3
		public TokenType TelemetryTokenType
		{
			get
			{
				return TokenType.Bearer;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x00056CA6 File Offset: 0x00054EA6
		public string AuthorizationHeaderPrefix
		{
			get
			{
				return "Bearer";
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x00056CAD File Offset: 0x00054EAD
		public string AccessTokenType
		{
			get
			{
				return "bearer";
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x00056CB4 File Offset: 0x00054EB4
		public string KeyId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00056CB7 File Offset: 0x00054EB7
		public string FormatAccessToken(MsalAccessTokenCacheItem msalAccessTokenCacheItem)
		{
			return msalAccessTokenCacheItem.Secret;
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00056CBF File Offset: 0x00054EBF
		public IReadOnlyDictionary<string, string> GetTokenRequestParams()
		{
			return CollectionHelpers.GetEmptyDictionary<string, string>();
		}

		// Token: 0x04000C2B RID: 3115
		internal const string BearerTokenType = "bearer";
	}
}
