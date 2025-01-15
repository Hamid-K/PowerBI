using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000B2 RID: 178
	internal static class UriUtils
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x0001388B File Offset: 0x00011A8B
		internal static Uri UriToAbsoluteUri(Uri baseUri, Uri relativeUri)
		{
			return new Uri(baseUri, relativeUri);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00013894 File Offset: 0x00011A94
		internal static Uri EnsureEscapedRelativeUri(Uri uri)
		{
			string components = uri.GetComponents(int.MinValue, 1);
			if (string.CompareOrdinal(uri.OriginalString, components) == 0)
			{
				return uri;
			}
			return new Uri(components, 2);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000138C5 File Offset: 0x00011AC5
		internal static string EnsureEscapedFragment(string fragmentString)
		{
			return "#" + Uri.EscapeDataString(fragmentString.Substring(1));
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000138DD File Offset: 0x00011ADD
		internal static string UriToString(Uri uri)
		{
			if (!uri.IsAbsoluteUri)
			{
				return uri.OriginalString;
			}
			return uri.AbsoluteUri;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000138F4 File Offset: 0x00011AF4
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

		// Token: 0x060006E4 RID: 1764 RVA: 0x00013938 File Offset: 0x00011B38
		internal static bool UriInvariantInsensitiveIsBaseOf(Uri baseUri, Uri uri)
		{
			Uri uri2 = UriUtils.CreateBaseComparableUri(baseUri);
			Uri uri3 = UriUtils.CreateBaseComparableUri(uri);
			return uri2.IsBaseOf(uri3);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001395C File Offset: 0x00011B5C
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

		// Token: 0x060006E6 RID: 1766 RVA: 0x000139C0 File Offset: 0x00011BC0
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

		// Token: 0x060006E7 RID: 1767 RVA: 0x00013A38 File Offset: 0x00011C38
		internal static bool TryUriStringToDate(string text, out Date targetValue)
		{
			targetValue = default(Date);
			bool flag;
			try
			{
				targetValue = PlatformHelper.ConvertStringToDate(text);
				flag = true;
			}
			catch (FormatException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00013A74 File Offset: 0x00011C74
		internal static bool TryUriStringToTimeOfDay(string text, out TimeOfDay targetValue)
		{
			targetValue = default(TimeOfDay);
			bool flag;
			try
			{
				targetValue = PlatformHelper.ConvertStringToTimeOfDay(text);
				flag = true;
			}
			catch (FormatException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00013AB0 File Offset: 0x00011CB0
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

		// Token: 0x060006EA RID: 1770 RVA: 0x00013AE4 File Offset: 0x00011CE4
		private static Uri CreateBaseComparableUri(Uri uri)
		{
			uri = new Uri(UriUtils.UriToString(uri).ToUpper(CultureInfo.InvariantCulture), 0);
			return new UriBuilder(uri)
			{
				Host = "h",
				Port = 80,
				Scheme = "http"
			}.Uri;
		}
	}
}
