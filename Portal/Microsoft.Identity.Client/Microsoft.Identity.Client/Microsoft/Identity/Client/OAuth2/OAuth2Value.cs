using System;
using System.Collections.Generic;

namespace Microsoft.Identity.Client.OAuth2
{
	// Token: 0x02000210 RID: 528
	internal static class OAuth2Value
	{
		// Token: 0x0400095A RID: 2394
		public const string CodeChallengeMethodValue = "S256";

		// Token: 0x0400095B RID: 2395
		public const string ScopeOpenId = "openid";

		// Token: 0x0400095C RID: 2396
		public const string ScopeOfflineAccess = "offline_access";

		// Token: 0x0400095D RID: 2397
		public const string ScopeProfile = "profile";

		// Token: 0x0400095E RID: 2398
		public static readonly HashSet<string> ReservedScopes = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "openid", "profile", "offline_access" };
	}
}
