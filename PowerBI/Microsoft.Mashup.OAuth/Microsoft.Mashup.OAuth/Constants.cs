using System;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x0200000B RID: 11
	public static class Constants
	{
		// Token: 0x0600004D RID: 77 RVA: 0x000034CC File Offset: 0x000016CC
		internal static char[] QueryDelimiters()
		{
			return Constants.queryDelimiters;
		}

		// Token: 0x04000043 RID: 67
		public static readonly TimeSpan AccessTokenSoonToExpireInterval = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000044 RID: 68
		internal const string AccessDenied = "access_denied";

		// Token: 0x04000045 RID: 69
		internal const string AccessToken = "access_token";

		// Token: 0x04000046 RID: 70
		internal const string AccessType = "access_type";

		// Token: 0x04000047 RID: 71
		internal const string Account = "account";

		// Token: 0x04000048 RID: 72
		internal const string AdfsTokenPath = "/adfs/oauth2/token";

		// Token: 0x04000049 RID: 73
		internal const string AdfsLogoutPath = "/adfs/ls/";

		// Token: 0x0400004A RID: 74
		internal const string AdfsLogoutQuery = "wa=wsignout1.0";

		// Token: 0x0400004B RID: 75
		internal const string AuthorizationCode = "authorization_code";

		// Token: 0x0400004C RID: 76
		internal const string ApprovalPrompt = "approval_prompt";

		// Token: 0x0400004D RID: 77
		internal const string ClientAssertion = "client_assertion";

		// Token: 0x0400004E RID: 78
		internal const string ClientAssertionType = "client_assertion_type";

		// Token: 0x0400004F RID: 79
		internal const string ClientId = "client_id";

		// Token: 0x04000050 RID: 80
		internal const string ClientSecret = "client_secret";

		// Token: 0x04000051 RID: 81
		internal const string DefaultUserAgent = "Microsoft.Data.Mashup (https://go.microsoft.com/fwlink/?LinkID=304225)";

		// Token: 0x04000052 RID: 82
		public const string DefaultScope = ".default";

		// Token: 0x04000053 RID: 83
		public const string FederatedAuthCookieName = "FedAuth";

		// Token: 0x04000054 RID: 84
		internal const string Force = "force";

		// Token: 0x04000055 RID: 85
		internal const string Consent = "consent";

		// Token: 0x04000056 RID: 86
		internal const string Code = "code";

		// Token: 0x04000057 RID: 87
		internal const string Display = "display";

		// Token: 0x04000058 RID: 88
		internal const string ErrorDescription = "error_description";

		// Token: 0x04000059 RID: 89
		internal const string Error = "error";

		// Token: 0x0400005A RID: 90
		internal const string ErrorUri = "error_uri";

		// Token: 0x0400005B RID: 91
		internal const string Expires = "expires";

		// Token: 0x0400005C RID: 92
		internal const string ExpiresIn = "expires_in";

		// Token: 0x0400005D RID: 93
		internal const string GrantType = "grant_type";

		// Token: 0x0400005E RID: 94
		internal const string InstanceUrl = "instance_url";

		// Token: 0x0400005F RID: 95
		internal const string JwtBearerAssertionType = "urn:ietf:params:oauth:client-assertion-type:jwt-bearer";

		// Token: 0x04000060 RID: 96
		internal const string LocaleVerb = "mkt";

		// Token: 0x04000061 RID: 97
		internal const string Login = "login";

		// Token: 0x04000062 RID: 98
		internal const string Next = "next";

		// Token: 0x04000063 RID: 99
		internal const string Nux = "nux";

		// Token: 0x04000064 RID: 100
		internal const string NuxValue = "1";

		// Token: 0x04000065 RID: 101
		internal const string Offline = "offline";

		// Token: 0x04000066 RID: 102
		internal const string PostLogoutRedirectUri = "post_logout_redirect_uri";

		// Token: 0x04000067 RID: 103
		internal const string Prompt = "prompt";

		// Token: 0x04000068 RID: 104
		public const string ProviderType = "ProviderType";

		// Token: 0x04000069 RID: 105
		internal const string RedirectUri = "redirect_uri";

		// Token: 0x0400006A RID: 106
		internal const string RefreshToken = "refresh_token";

		// Token: 0x0400006B RID: 107
		internal const string ResponseType = "response_type";

		// Token: 0x0400006C RID: 108
		internal const string Resource = "resource";

		// Token: 0x0400006D RID: 109
		internal const string Scope = "scope";

		// Token: 0x0400006E RID: 110
		internal const string SelectAccount = "select_account";

		// Token: 0x0400006F RID: 111
		public const string SharePointAAD = "SharePointAAD";

		// Token: 0x04000070 RID: 112
		public const string SharePointFBA = "SharePointFBA";

		// Token: 0x04000071 RID: 113
		public const string SignOutCookieName = "rtFa";

		// Token: 0x04000072 RID: 114
		internal const string State = "state";

		// Token: 0x04000073 RID: 115
		internal const string Token = "token";

		// Token: 0x04000074 RID: 116
		internal const string UserImpersonation = "user_impersonation";

		// Token: 0x04000075 RID: 117
		internal const string XPermissions = "x_permissions";

		// Token: 0x04000076 RID: 118
		internal const string XScope = "x_scope";

		// Token: 0x04000077 RID: 119
		public const string Authorization = "Authorization";

		// Token: 0x04000078 RID: 120
		public const string Bearer = "Bearer";

		// Token: 0x04000079 RID: 121
		public const string GmailKey = "gmail";

		// Token: 0x0400007A RID: 122
		private static readonly char[] queryDelimiters = new char[] { '?', '#' };
	}
}
