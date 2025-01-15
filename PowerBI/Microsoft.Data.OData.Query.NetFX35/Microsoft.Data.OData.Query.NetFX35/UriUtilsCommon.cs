using System;

namespace Microsoft.Data.Experimental.OData
{
	// Token: 0x02000017 RID: 23
	internal static class UriUtilsCommon
	{
		// Token: 0x06000066 RID: 102 RVA: 0x0000375F File Offset: 0x0000195F
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
