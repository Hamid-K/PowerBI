using System;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000DC RID: 220
	internal static class UriUtils
	{
		// Token: 0x06000A4C RID: 2636 RVA: 0x0001BAE8 File Offset: 0x00019CE8
		internal static Uri UriToAbsoluteUri(Uri baseUri, Uri relativeUri)
		{
			return new Uri(baseUri, relativeUri);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001BAF4 File Offset: 0x00019CF4
		internal static Uri EnsureEscapedRelativeUri(Uri uri)
		{
			string components = uri.GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped);
			if (string.CompareOrdinal(uri.OriginalString, components) == 0)
			{
				return uri;
			}
			return new Uri(components, UriKind.Relative);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001BB25 File Offset: 0x00019D25
		internal static string EnsureEscapedFragment(string fragmentString)
		{
			return "#" + Uri.EscapeDataString(fragmentString.Substring(1));
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001BB3D File Offset: 0x00019D3D
		internal static string UriToString(Uri uri)
		{
			if (!uri.IsAbsoluteUri)
			{
				return uri.OriginalString;
			}
			return uri.AbsoluteUri;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0001BB54 File Offset: 0x00019D54
		internal static Uri StringToUri(string uriString)
		{
			Uri uri = null;
			try
			{
				uri = new Uri(uriString, UriKind.RelativeOrAbsolute);
			}
			catch (FormatException)
			{
				uri = new Uri(uriString, UriKind.Relative);
			}
			return uri;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0001BB8C File Offset: 0x00019D8C
		internal static Uri EnsureTaillingSlash(Uri uri)
		{
			if (uri == null)
			{
				return null;
			}
			string text = UriUtils.UriToString(uri);
			if (text[text.Length - 1] != '/')
			{
				return new Uri(text + "/", UriKind.RelativeOrAbsolute);
			}
			return uri;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0001BBD0 File Offset: 0x00019DD0
		internal static bool UriInvariantInsensitiveIsBaseOf(Uri baseUri, Uri uri)
		{
			Uri uri2 = UriUtils.CreateBaseComparableUri(baseUri);
			Uri uri3 = UriUtils.CreateBaseComparableUri(uri);
			return uri2.IsBaseOf(uri3);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001BBF4 File Offset: 0x00019DF4
		internal static bool TryUriStringToGuid(string text, out Guid targetValue)
		{
			bool flag;
			try
			{
				string text2 = text.Trim();
				if (text2.Length != 36 || text2.IndexOf('-') != 8)
				{
					targetValue = default(Guid);
					flag = false;
				}
				else
				{
					targetValue = XmlConvert.ToGuid(text);
					flag = true;
				}
			}
			catch (FormatException)
			{
				targetValue = default(Guid);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0001BC58 File Offset: 0x00019E58
		internal static bool ConvertUriStringToDateTimeOffset(string text, out DateTimeOffset targetValue)
		{
			targetValue = default(DateTimeOffset);
			bool flag;
			try
			{
				targetValue = PlatformHelper.ConvertStringToDateTimeOffset(text);
				flag = true;
			}
			catch (FormatException ex)
			{
				Match match = PlatformHelper.PotentialDateTimeOffsetValidator.Match(text);
				if (match.Success)
				{
					throw new ODataException(Strings.UriUtils_DateTimeOffsetInvalidFormat(text), ex);
				}
				flag = false;
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				throw new ODataException(Strings.UriUtils_DateTimeOffsetInvalidFormat(text), ex2);
			}
			return flag;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001BCD0 File Offset: 0x00019ED0
		internal static bool TryUriStringToDate(string text, out Date targetValue)
		{
			return PlatformHelper.TryConvertStringToDate(text, out targetValue);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001BCD9 File Offset: 0x00019ED9
		internal static bool TryUriStringToTimeOfDay(string text, out TimeOfDay targetValue)
		{
			return PlatformHelper.TryConvertStringToTimeOfDay(text, out targetValue);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001BCE4 File Offset: 0x00019EE4
		internal static Uri CreateMockAbsoluteUri(Uri uri = null)
		{
			Uri uri2 = new Uri("http://host/");
			if (uri == null)
			{
				return uri2;
			}
			if (uri.IsAbsoluteUri)
			{
				return uri;
			}
			return new Uri(uri2, uri);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001BD18 File Offset: 0x00019F18
		private static Uri CreateBaseComparableUri(Uri uri)
		{
			uri = new Uri(UriUtils.UriToString(uri).ToUpperInvariant(), UriKind.RelativeOrAbsolute);
			return new UriBuilder(uri)
			{
				Host = "h",
				Port = 80,
				Scheme = "http"
			}.Uri;
		}
	}
}
