using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.Cache.Items;

namespace Microsoft.Identity.Client.AuthScheme.SSHCertificates
{
	// Token: 0x020002C1 RID: 705
	internal class SSHCertAuthenticationScheme : IAuthenticationScheme
	{
		// Token: 0x06001A85 RID: 6789 RVA: 0x000566D7 File Offset: 0x000548D7
		public SSHCertAuthenticationScheme(string keyId, string jwk)
		{
			if (string.IsNullOrEmpty(keyId))
			{
				throw new ArgumentNullException("keyId");
			}
			if (string.IsNullOrEmpty(jwk))
			{
				throw new ArgumentNullException("jwk");
			}
			this.KeyId = keyId;
			this._jwk = jwk;
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x00056713 File Offset: 0x00054913
		public TokenType TelemetryTokenType
		{
			get
			{
				return TokenType.SshCert;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x00056716 File Offset: 0x00054916
		public string AuthorizationHeaderPrefix
		{
			get
			{
				throw new MsalClientException("ssh_cert_used_as_http_header", "MSAL was configured to request SSH certificates from AAD, and these cannot be used as an HTTP authentication header. Developers are responsible for transporting the SSH certificates to the target machines. ");
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x00056727 File Offset: 0x00054927
		public string AccessTokenType
		{
			get
			{
				return "ssh-cert";
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x0005672E File Offset: 0x0005492E
		public string KeyId { get; }

		// Token: 0x06001A8A RID: 6794 RVA: 0x00056736 File Offset: 0x00054936
		public string FormatAccessToken(MsalAccessTokenCacheItem msalAccessTokenCacheItem)
		{
			return msalAccessTokenCacheItem.Secret;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x0005673E File Offset: 0x0005493E
		public IReadOnlyDictionary<string, string> GetTokenRequestParams()
		{
			return new Dictionary<string, string>
			{
				{ "token_type", "ssh-cert" },
				{ "req_cnf", this._jwk }
			};
		}

		// Token: 0x04000BF7 RID: 3063
		internal const string SSHCertTokenType = "ssh-cert";

		// Token: 0x04000BF8 RID: 3064
		private readonly string _jwk;
	}
}
