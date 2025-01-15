using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.Cache.Items;

namespace Microsoft.Identity.Client.AuthScheme
{
	// Token: 0x020002BF RID: 703
	internal interface IAuthenticationScheme
	{
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001A7F RID: 6783
		TokenType TelemetryTokenType { get; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001A80 RID: 6784
		string AuthorizationHeaderPrefix { get; }

		// Token: 0x06001A81 RID: 6785
		IReadOnlyDictionary<string, string> GetTokenRequestParams();

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001A82 RID: 6786
		string KeyId { get; }

		// Token: 0x06001A83 RID: 6787
		string FormatAccessToken(MsalAccessTokenCacheItem msalAccessTokenCacheItem);

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001A84 RID: 6788
		string AccessTokenType { get; }
	}
}
