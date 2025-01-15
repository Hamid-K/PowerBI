using System;

namespace Microsoft.Identity.Client.OAuth2
{
	// Token: 0x0200020A RID: 522
	internal static class OAuth2GrantType
	{
		// Token: 0x04000949 RID: 2377
		public const string AuthorizationCode = "authorization_code";

		// Token: 0x0400094A RID: 2378
		public const string RefreshToken = "refresh_token";

		// Token: 0x0400094B RID: 2379
		public const string ClientCredentials = "client_credentials";

		// Token: 0x0400094C RID: 2380
		public const string Saml11Bearer = "urn:ietf:params:oauth:grant-type:saml1_1-bearer";

		// Token: 0x0400094D RID: 2381
		public const string Saml20Bearer = "urn:ietf:params:oauth:grant-type:saml2-bearer";

		// Token: 0x0400094E RID: 2382
		public const string JwtBearer = "urn:ietf:params:oauth:grant-type:jwt-bearer";

		// Token: 0x0400094F RID: 2383
		public const string Password = "password";

		// Token: 0x04000950 RID: 2384
		public const string DeviceCode = "device_code";
	}
}
