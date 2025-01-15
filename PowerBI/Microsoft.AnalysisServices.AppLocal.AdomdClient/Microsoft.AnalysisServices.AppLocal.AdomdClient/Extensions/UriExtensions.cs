using System;

namespace Microsoft.AnalysisServices.AdomdClient.Extensions
{
	// Token: 0x02000159 RID: 345
	internal static class UriExtensions
	{
		// Token: 0x060010DB RID: 4315 RVA: 0x0003A7F1 File Offset: 0x000389F1
		public static Uri RemovePathSuffix(this Uri uri, string suffix)
		{
			return new UriBuilder(uri)
			{
				Path = uri.AbsolutePath.Remove(uri.AbsolutePath.Length - suffix.Length)
			}.Uri;
		}
	}
}
