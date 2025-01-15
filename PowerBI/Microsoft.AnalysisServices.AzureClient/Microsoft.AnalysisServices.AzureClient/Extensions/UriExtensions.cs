using System;

namespace Microsoft.AnalysisServices.AzureClient.Extensions
{
	// Token: 0x0200003C RID: 60
	internal static class UriExtensions
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x00008FB9 File Offset: 0x000071B9
		public static Uri RemovePathSuffix(this Uri uri, string suffix)
		{
			return new UriBuilder(uri)
			{
				Path = uri.AbsolutePath.Remove(uri.AbsolutePath.Length - suffix.Length)
			}.Uri;
		}
	}
}
