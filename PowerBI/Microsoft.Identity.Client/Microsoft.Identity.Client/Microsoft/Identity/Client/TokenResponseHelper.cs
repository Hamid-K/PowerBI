using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200017D RID: 381
	internal static class TokenResponseHelper
	{
		// Token: 0x06001270 RID: 4720 RVA: 0x0003EC9D File Offset: 0x0003CE9D
		public static string GetTenantId(IdToken idToken, AuthenticationRequestParameters requestParams)
		{
			return Authority.CreateAuthorityWithTenant(requestParams.Authority.AuthorityInfo, (idToken != null) ? idToken.TenantId : null).TenantId;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0003ECC0 File Offset: 0x0003CEC0
		public static string GetUsernameFromIdToken(IdToken idToken)
		{
			if (idToken == null)
			{
				return "Missing from the token response";
			}
			string text;
			if ((text = idToken.PreferredUsername.NullIfWhiteSpace()) == null && (text = idToken.Upn.NullIfWhiteSpace()) == null && (text = idToken.Email.NullIfWhiteSpace()) == null)
			{
				text = idToken.Name.NullIfWhiteSpace() ?? "Missing from the token response";
			}
			return text;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0003ED18 File Offset: 0x0003CF18
		public static string GetHomeAccountId(AuthenticationRequestParameters requestParams, MsalTokenResponse response, IdToken idToken)
		{
			ClientInfo clientInfo = ((response.ClientInfo != null) ? ClientInfo.CreateFromJson(response.ClientInfo) : null);
			string text = ((clientInfo != null) ? clientInfo.ToAccountIdentifier() : null) ?? ((idToken != null) ? idToken.Subject : null);
			if (text == null)
			{
				requestParams.RequestContext.Logger.Info("Cannot determine home account ID - or id token or no client info and no subject ");
			}
			return text;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0003ED6F File Offset: 0x0003CF6F
		public static Dictionary<string, string> GetWamAccountIds(AuthenticationRequestParameters requestParams, MsalTokenResponse response)
		{
			if (!string.IsNullOrEmpty(response.WamAccountId))
			{
				return new Dictionary<string, string> { 
				{
					requestParams.AppConfig.ClientId,
					response.WamAccountId
				} };
			}
			return new Dictionary<string, string>();
		}

		// Token: 0x040006D5 RID: 1749
		internal const string NullPreferredUsernameDisplayLabel = "Missing from the token response";
	}
}
