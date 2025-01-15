using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x0200028F RID: 655
	internal static class UriUtils
	{
		// Token: 0x06001667 RID: 5735 RVA: 0x0004E180 File Offset: 0x0004C380
		internal static bool UriInvariantInsensitiveIsBaseOf(Uri baseUri, Uri uri)
		{
			Uri uri2 = UriUtils.CreateBaseComparableUri(baseUri);
			Uri uri3 = UriUtils.CreateBaseComparableUri(uri);
			return uri2.IsBaseOf(uri3);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0004E1A4 File Offset: 0x0004C3A4
		internal static List<CustomQueryOptionToken> ParseQueryOptions(Uri uri)
		{
			List<CustomQueryOptionToken> list = new List<CustomQueryOptionToken>();
			string text = uri.Query.Replace('+', ' ');
			int num;
			if (text != null)
			{
				if (text.Length > 0 && text.get_Chars(0) == '?')
				{
					text = text.Substring(1);
				}
				num = text.Length;
			}
			else
			{
				num = 0;
			}
			for (int i = 0; i < num; i++)
			{
				int num2 = i;
				int num3 = -1;
				while (i < num)
				{
					char c = text.get_Chars(i);
					if (c == '=')
					{
						if (num3 < 0)
						{
							num3 = i;
						}
					}
					else if (c == '&')
					{
						break;
					}
					i++;
				}
				string text2 = null;
				string text3;
				if (num3 >= 0)
				{
					text2 = text.Substring(num2, num3 - num2);
					text3 = text.Substring(num3 + 1, i - num3 - 1);
				}
				else
				{
					text3 = text.Substring(num2, i - num2);
				}
				text2 = ((text2 == null) ? null : Uri.UnescapeDataString(text2).Trim());
				text3 = ((text3 == null) ? null : Uri.UnescapeDataString(text3).Trim());
				list.Add(new CustomQueryOptionToken(text2, text3));
				if (i == num - 1 && text.get_Chars(i) == '&')
				{
					list.Add(new CustomQueryOptionToken(null, string.Empty));
				}
			}
			return list;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0004E2CC File Offset: 0x0004C4CC
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

		// Token: 0x0600166A RID: 5738 RVA: 0x0004E330 File Offset: 0x0004C530
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

		// Token: 0x0600166B RID: 5739 RVA: 0x0004E3A8 File Offset: 0x0004C5A8
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

		// Token: 0x0600166C RID: 5740 RVA: 0x0004E3E4 File Offset: 0x0004C5E4
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

		// Token: 0x0600166D RID: 5741 RVA: 0x0004E420 File Offset: 0x0004C620
		internal static Uri CreateMockAbsoluteUri(Uri uri = null)
		{
			if (uri == null)
			{
				return UriUtils.BaseMockUri;
			}
			if (uri.IsAbsoluteUri)
			{
				return uri;
			}
			return new Uri(UriUtils.BaseMockUri, uri);
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0004E448 File Offset: 0x0004C648
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

		// Token: 0x040009F9 RID: 2553
		private static readonly Uri BaseMockUri = new Uri("http://host/");
	}
}
