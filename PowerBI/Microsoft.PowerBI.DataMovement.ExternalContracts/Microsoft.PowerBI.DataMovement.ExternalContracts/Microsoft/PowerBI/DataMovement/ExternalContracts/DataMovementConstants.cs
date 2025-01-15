using System;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts
{
	// Token: 0x02000009 RID: 9
	public static class DataMovementConstants
	{
		// Token: 0x04000014 RID: 20
		public const string AESEncryptionAlgorithm = "AES";

		// Token: 0x04000015 RID: 21
		public const string RSA_OAEPEncryptionAlgorithm = "RSA-OAEP";

		// Token: 0x04000016 RID: 22
		public const string NoneEncryptionAlgorithm = "NONE";

		// Token: 0x04000017 RID: 23
		public const string UserIdKey = "username";

		// Token: 0x04000018 RID: 24
		public const string PasswordKey = "password";

		// Token: 0x04000019 RID: 25
		public const string CredentialKey = "key";

		// Token: 0x0400001A RID: 26
		public const string OAuthAccessToken = "AccessToken";

		// Token: 0x0400001B RID: 27
		public const string OAuthExpires = "Expires";

		// Token: 0x0400001C RID: 28
		public const string OAuthRefreshToken = "RefreshToken";

		// Token: 0x0400001D RID: 29
		public const string OAuthPropertyPrefix = "Property-";

		// Token: 0x0400001E RID: 30
		public const string AccessTokenPrefix = "AccessToken:";

		// Token: 0x0400001F RID: 31
		public const string OAuthTokenType = "token_type";

		// Token: 0x04000020 RID: 32
		public const string OAuthOAuth2Nonce = "oAuth2Nonce";

		// Token: 0x04000021 RID: 33
		public const string OAuthRedirectEndpoint = "redirectEndpoint";

		// Token: 0x04000022 RID: 34
		public const string SASTokenCredentialKey = "Token";

		// Token: 0x04000023 RID: 35
		public const string CustomOAuthAppId = "AppId";

		// Token: 0x04000024 RID: 36
		public const string CustomOAuthIdentityType = "IdentityType";

		// Token: 0x04000025 RID: 37
		public const string CustomOAuthSecretUrl = "SecretUrl";

		// Token: 0x04000026 RID: 38
		public const string CustomOAuthDatasourceTenantId = "TenantId";

		// Token: 0x04000027 RID: 39
		public const string CustomOAuthFakeAccessToken = "FakeAccessTokenForUpdateCredentialByRefreshTokenFlow";

		// Token: 0x04000028 RID: 40
		public const string RunAsAdminEffectiveUser = "718E9B80-F20C-42EA-AA38-E45CBEEB6718";

		// Token: 0x04000029 RID: 41
		public const string TenantIdKey = "tenantId";

		// Token: 0x0400002A RID: 42
		public const string ServicePrincipalClientIdKey = "servicePrincipalClientId";

		// Token: 0x0400002B RID: 43
		public const string ServicePrincipalSecretKey = "servicePrincipalSecret";
	}
}
