using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000248 RID: 584
	internal static class UriUtilsCommon
	{
		// Token: 0x060011C6 RID: 4550 RVA: 0x00043602 File Offset: 0x00041802
		internal static string UriToString(Uri uri)
		{
			if (!uri.IsAbsoluteUri)
			{
				return uri.OriginalString;
			}
			return uri.AbsoluteUri;
		}
	}
}
