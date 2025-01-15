using System;

namespace Microsoft.Identity.Client.AuthScheme
{
	// Token: 0x020002BE RID: 702
	internal class AuthSchemeHelper
	{
		// Token: 0x06001A7D RID: 6781 RVA: 0x000566AC File Offset: 0x000548AC
		public static bool StoreTokenTypeInCacheKey(string tokenType)
		{
			return !string.Equals(tokenType, "bearer", StringComparison.OrdinalIgnoreCase) && !string.Equals(tokenType, "ssh-cert", StringComparison.OrdinalIgnoreCase);
		}
	}
}
