using System;

namespace Microsoft.ReportingServices.Hybrid.OAuth
{
	// Token: 0x02000010 RID: 16
	internal static class ServiceConstants
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public static char[] QueryDelimiters()
		{
			return ServiceConstants.queryDelimiters;
		}

		// Token: 0x0400004E RID: 78
		public const string AccessToken = "access_token";

		// Token: 0x0400004F RID: 79
		public const string AuthorizationCode = "authorization_code";

		// Token: 0x04000050 RID: 80
		public const string Bearer = "Bearer";

		// Token: 0x04000051 RID: 81
		public const string ClientId = "client_id";

		// Token: 0x04000052 RID: 82
		public const string ClientSecret = "client_secret";

		// Token: 0x04000053 RID: 83
		public const string State = "state";

		// Token: 0x04000054 RID: 84
		public const string Code = "code";

		// Token: 0x04000055 RID: 85
		public const string ErrorDescription = "error_description";

		// Token: 0x04000056 RID: 86
		public const string Error = "error";

		// Token: 0x04000057 RID: 87
		public const string ExpiresIn = "expires_in";

		// Token: 0x04000058 RID: 88
		public const string ExpiresOn = "expires_on";

		// Token: 0x04000059 RID: 89
		public const string GrantType = "grant_type";

		// Token: 0x0400005A RID: 90
		public const string IdToken = "id_token";

		// Token: 0x0400005B RID: 91
		public const string IdTokenHint = "id_token_hint";

		// Token: 0x0400005C RID: 92
		public const string Login = "login";

		// Token: 0x0400005D RID: 93
		public const string Popup = "popup";

		// Token: 0x0400005E RID: 94
		public const string PostLogoutRedirectUri = "post_logout_redirect_uri";

		// Token: 0x0400005F RID: 95
		public const string Prompt = "prompt";

		// Token: 0x04000060 RID: 96
		public const string RedirectUri = "redirect_uri";

		// Token: 0x04000061 RID: 97
		public const string RefreshToken = "refresh_token";

		// Token: 0x04000062 RID: 98
		public const string ResponseType = "response_type";

		// Token: 0x04000063 RID: 99
		public const string Resource = "resource";

		// Token: 0x04000064 RID: 100
		public const string Scope = "scope";

		// Token: 0x04000065 RID: 101
		public const string TokenType = "token_type";

		// Token: 0x04000066 RID: 102
		public const string Audience = "aud";

		// Token: 0x04000067 RID: 103
		public const string Expiration = "exp";

		// Token: 0x04000068 RID: 104
		public const string FamilyName = "family_name";

		// Token: 0x04000069 RID: 105
		public const string GivenName = "given_name";

		// Token: 0x0400006A RID: 106
		public const string IdentitySpace = "AAD";

		// Token: 0x0400006B RID: 107
		public const string LocaleVerb = "mkt";

		// Token: 0x0400006C RID: 108
		public const string NotBefore = "nbf";

		// Token: 0x0400006D RID: 109
		public const string Nux = "nux";

		// Token: 0x0400006E RID: 110
		public const string NuxValue = "1";

		// Token: 0x0400006F RID: 111
		public const string ObjectId = "oid";

		// Token: 0x04000070 RID: 112
		public const string Subject = "sub";

		// Token: 0x04000071 RID: 113
		public const string TenantId = "tid";

		// Token: 0x04000072 RID: 114
		public const string TokenIssuedAt = "iat";

		// Token: 0x04000073 RID: 115
		public const string TokenIssuer = "iss";

		// Token: 0x04000074 RID: 116
		public const string UniqueName = "unique_name";

		// Token: 0x04000075 RID: 117
		public const string UserPrincipalName = "upn";

		// Token: 0x04000076 RID: 118
		public const string Version = "ver";

		// Token: 0x04000077 RID: 119
		public const string AadAuthTokenKey = "AADAuthToken";

		// Token: 0x04000078 RID: 120
		public static readonly TimeSpan AccessTokenSoonToExpireInterval = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000079 RID: 121
		private static readonly char[] queryDelimiters = new char[] { '?', '#' };
	}
}
