using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.AuthScheme;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Extensibility
{
	// Token: 0x02000299 RID: 665
	internal class ExternalBoundTokenScheme : IAuthenticationScheme
	{
		// Token: 0x06001937 RID: 6455 RVA: 0x00052D67 File Offset: 0x00050F67
		public ExternalBoundTokenScheme(string keyId, string expectedTokenTypeFromEsts = "Bearer")
		{
			this._keyId = keyId;
			this._tokenType = expectedTokenTypeFromEsts;
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x00052D7D File Offset: 0x00050F7D
		public TokenType TelemetryTokenType
		{
			get
			{
				return TokenType.External;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x00052D80 File Offset: 0x00050F80
		public string AuthorizationHeaderPrefix
		{
			get
			{
				return this._tokenType;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x00052D88 File Offset: 0x00050F88
		public string KeyId
		{
			get
			{
				return this._keyId;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x00052D90 File Offset: 0x00050F90
		public string AccessTokenType
		{
			get
			{
				return this._tokenType;
			}
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00052D98 File Offset: 0x00050F98
		public string FormatAccessToken(MsalAccessTokenCacheItem msalAccessTokenCacheItem)
		{
			return msalAccessTokenCacheItem.Secret;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00052DA0 File Offset: 0x00050FA0
		public IReadOnlyDictionary<string, string> GetTokenRequestParams()
		{
			return CollectionHelpers.GetEmptyDictionary<string, string>();
		}

		// Token: 0x04000B54 RID: 2900
		private readonly string _keyId;

		// Token: 0x04000B55 RID: 2901
		private readonly string _tokenType;
	}
}
