using System;

namespace Microsoft.AnalysisServices.Extensions
{
	// Token: 0x0200014E RID: 334
	internal static class UriExtensions
	{
		// Token: 0x06001169 RID: 4457 RVA: 0x0003D0F5 File Offset: 0x0003B2F5
		public static Uri RemovePathSuffix(this Uri uri, string suffix)
		{
			return new UriBuilder(uri)
			{
				Path = uri.AbsolutePath.Remove(uri.AbsolutePath.Length - suffix.Length)
			}.Uri;
		}
	}
}
