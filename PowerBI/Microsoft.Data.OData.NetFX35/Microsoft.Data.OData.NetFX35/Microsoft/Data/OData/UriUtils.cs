using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000292 RID: 658
	internal static class UriUtils
	{
		// Token: 0x060014F2 RID: 5362 RVA: 0x0004CBD6 File Offset: 0x0004ADD6
		internal static Uri UriToAbsoluteUri(Uri baseUri, Uri relativeUri)
		{
			return new Uri(baseUri, relativeUri);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0004CBE0 File Offset: 0x0004ADE0
		internal static Uri EnsureEscapedRelativeUri(Uri uri)
		{
			string components = uri.GetComponents(int.MinValue, 1);
			if (string.CompareOrdinal(uri.OriginalString, components) == 0)
			{
				return uri;
			}
			return new Uri(components, 2);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0004CC11 File Offset: 0x0004AE11
		internal static string EnsureEscapedFragment(string fragmentString)
		{
			return new Uri(UriUtils.ExampleMetadataAbsoluteUri, fragmentString).Fragment;
		}

		// Token: 0x040008BE RID: 2238
		private static Uri ExampleMetadataAbsoluteUri = new Uri("http://www.example.com/$metadata", 1);
	}
}
