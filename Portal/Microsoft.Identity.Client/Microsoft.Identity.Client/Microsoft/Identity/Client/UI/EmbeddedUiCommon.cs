using System;

namespace Microsoft.Identity.Client.UI
{
	// Token: 0x020001DB RID: 475
	internal static class EmbeddedUiCommon
	{
		// Token: 0x0600149A RID: 5274 RVA: 0x00045BC0 File Offset: 0x00043DC0
		public static bool IsAllowedIeOrEdgeAuthorizationRedirect(Uri uri)
		{
			return uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase) || uri.AbsoluteUri.Equals("about:blank", StringComparison.OrdinalIgnoreCase) || uri.Scheme.Equals("javascript", StringComparison.OrdinalIgnoreCase) || uri.Scheme.Equals("res", StringComparison.OrdinalIgnoreCase);
		}
	}
}
