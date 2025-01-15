using System;

namespace Microsoft.OData.Client
{
	// Token: 0x020000E4 RID: 228
	internal static class UriUtil
	{
		// Token: 0x060007C4 RID: 1988 RVA: 0x000205D9 File Offset: 0x0001E7D9
		internal static string UriToString(Uri uri)
		{
			if (!uri.IsAbsoluteUri)
			{
				return uri.OriginalString;
			}
			return uri.AbsoluteUri;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x000205F0 File Offset: 0x0001E7F0
		internal static Uri CreateUri(string value, UriKind kind)
		{
			if (value != null)
			{
				return new Uri(value, kind);
			}
			return null;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00020600 File Offset: 0x0001E800
		internal static Uri CreateUri(Uri baseUri, Uri requestUri)
		{
			Util.CheckArgumentNull<Uri>(requestUri, "requestUri");
			if (!baseUri.IsAbsoluteUri)
			{
				return UriUtil.CreateUri(UriUtil.UriToString(baseUri) + "/" + UriUtil.UriToString(requestUri), UriKind.Relative);
			}
			if (requestUri.IsAbsoluteUri)
			{
				return requestUri;
			}
			return UriUtil.AppendBaseUriAndRelativeUri(baseUri, requestUri);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00020650 File Offset: 0x0001E850
		private static Uri AppendBaseUriAndRelativeUri(Uri baseUri, Uri relativeUri)
		{
			string text = UriUtil.UriToString(baseUri);
			string text2 = UriUtil.UriToString(relativeUri);
			if (text.EndsWith("/", StringComparison.Ordinal))
			{
				if (text2.StartsWith("/", StringComparison.Ordinal))
				{
					relativeUri = new Uri(baseUri, UriUtil.CreateUri(text2.TrimStart(UriUtil.ForwardSlash), UriKind.Relative));
				}
				else
				{
					relativeUri = new Uri(baseUri, relativeUri);
				}
			}
			else
			{
				relativeUri = UriUtil.CreateUri(text + "/" + text2.TrimStart(UriUtil.ForwardSlash), UriKind.Absolute);
			}
			return relativeUri;
		}

		// Token: 0x0400036B RID: 875
		internal static readonly char[] ForwardSlash = new char[] { '/' };
	}
}
