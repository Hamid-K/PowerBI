using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Core
{
	// Token: 0x0200029C RID: 668
	internal static class UriUtils
	{
		// Token: 0x060016FA RID: 5882 RVA: 0x0004ECDC File Offset: 0x0004CEDC
		internal static Uri UriToAbsoluteUri(Uri baseUri, Uri relativeUri)
		{
			return new Uri(baseUri, relativeUri);
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0004ECE8 File Offset: 0x0004CEE8
		internal static Uri CreateUriAsEntryOrFeedId(string value, UriKind kind, bool swallowEmpty = true)
		{
			if ((swallowEmpty && value == string.Empty) || value == null)
			{
				return null;
			}
			Uri uri;
			try
			{
				uri = new Uri(value, kind);
			}
			catch (FormatException)
			{
				throw new ODataException(Strings.ODataUriUtils_InvalidUriFormatForEntryIdOrFeedId(value));
			}
			return uri;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0004ED34 File Offset: 0x0004CF34
		[SuppressMessage("DataWeb.Usage", "AC0010", Justification = "Usage of OriginalString is safe in this context")]
		internal static Uri EnsureEscapedRelativeUri(Uri uri)
		{
			string components = uri.GetComponents(int.MinValue, 1);
			if (string.CompareOrdinal(uri.OriginalString, components) == 0)
			{
				return uri;
			}
			return new Uri(components, 2);
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0004ED65 File Offset: 0x0004CF65
		[SuppressMessage("DataWeb.Usage", "AC0018:SystemUriEscapeDataStringRule", Justification = "Values passed to this method are to appear in Uri fragments.")]
		internal static string EnsureEscapedFragment(string fragmentString)
		{
			return '#' + Uri.EscapeDataString(fragmentString.Substring(1));
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0004ED7F File Offset: 0x0004CF7F
		[SuppressMessage("DataWeb.Usage", "AC0010", Justification = "Usage of OriginalString is safe in this context")]
		internal static string UriToString(Uri uri)
		{
			if (!uri.IsAbsoluteUri)
			{
				return uri.OriginalString;
			}
			return uri.AbsoluteUri;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0004ED98 File Offset: 0x0004CF98
		internal static Uri EnsureTaillingSlash(Uri uri)
		{
			if (uri == null)
			{
				return null;
			}
			string text = UriUtils.UriToString(uri);
			if (text.get_Chars(text.Length - 1) != '/')
			{
				return new Uri(text + "/", 0);
			}
			return uri;
		}
	}
}
