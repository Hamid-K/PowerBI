using System;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000763 RID: 1891
	internal static class ODataUriNormalizer
	{
		// Token: 0x060037AC RID: 14252 RVA: 0x000B228C File Offset: 0x000B048C
		public static Uri Normalize(Uri uri)
		{
			if (!uri.IsAbsoluteUri)
			{
				return uri;
			}
			UriBuilder uriBuilder = new UriBuilder(uri);
			if (uriBuilder.Path != null)
			{
				string text = uriBuilder.Path;
				while (text.Contains("//"))
				{
					text = text.Replace("//", "/");
				}
				uriBuilder.Path = text.ToString();
			}
			string text2 = uriBuilder.Uri.OriginalString;
			if (!string.IsNullOrEmpty(uriBuilder.Query) && text2[text2.Length - 1] == '/')
			{
				text2 = text2.TrimEnd(new char[] { '/' });
			}
			return new Uri(text2);
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x000B2328 File Offset: 0x000B0528
		public static Uri NormalizeServiceDocumentUri(Uri serviceLocation)
		{
			Uri uri = ODataUriNormalizer.Normalize(serviceLocation);
			if (!uri.AbsolutePath.EndsWith("/", StringComparison.Ordinal))
			{
				return new Uri(uri.ToString() + "/");
			}
			return uri;
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000B2366 File Offset: 0x000B0566
		public static Uri NormalizeServiceRoot(Uri serviceRoot)
		{
			return new Uri(new UriBuilder(ODataUriNormalizer.NormalizeServiceDocumentUri(serviceRoot))
			{
				Query = null
			}.Uri.ToString());
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000B238C File Offset: 0x000B058C
		public static bool TryTerminateWithSlash(Uri uri, out Uri newUri)
		{
			UriBuilder uriBuilder = new UriBuilder(uri);
			uriBuilder.Query = null;
			uriBuilder.Fragment = null;
			if (!uriBuilder.Path.EndsWith("/", StringComparison.Ordinal))
			{
				UriBuilder uriBuilder2 = uriBuilder;
				uriBuilder2.Path += "/";
			}
			newUri = uriBuilder.Uri;
			return true;
		}

		// Token: 0x04001CCE RID: 7374
		private const string SinglePathSeparator = "/";

		// Token: 0x04001CCF RID: 7375
		private const string DoublePathSeparator = "//";
	}
}
