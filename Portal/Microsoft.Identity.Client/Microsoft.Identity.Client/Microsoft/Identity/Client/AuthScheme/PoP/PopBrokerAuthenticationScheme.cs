using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.AuthScheme.PoP
{
	// Token: 0x020002C7 RID: 711
	internal class PopBrokerAuthenticationScheme : IAuthenticationScheme
	{
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x00056B96 File Offset: 0x00054D96
		public TokenType TelemetryTokenType
		{
			get
			{
				return TokenType.Pop;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x00056B99 File Offset: 0x00054D99
		public string AuthorizationHeaderPrefix
		{
			get
			{
				return "PoP";
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x00056BA0 File Offset: 0x00054DA0
		public string KeyId
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x00056BA7 File Offset: 0x00054DA7
		public string AccessTokenType
		{
			get
			{
				return "pop";
			}
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00056BAE File Offset: 0x00054DAE
		public string FormatAccessToken(MsalAccessTokenCacheItem msalAccessTokenCacheItem)
		{
			return msalAccessTokenCacheItem.Secret;
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00056BB6 File Offset: 0x00054DB6
		public IReadOnlyDictionary<string, string> GetTokenRequestParams()
		{
			return CollectionHelpers.GetEmptyDictionary<string, string>();
		}
	}
}
