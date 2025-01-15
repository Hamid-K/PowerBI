using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200005A RID: 90
	internal static class UriUtils
	{
		// Token: 0x0600023D RID: 573 RVA: 0x0000C604 File Offset: 0x0000A804
		internal static bool UriInvariantInsensitiveIsBaseOf(Uri baseUri, Uri uri)
		{
			Uri uri2 = UriUtils.CreateBaseComparableUri(baseUri);
			Uri uri3 = UriUtils.CreateBaseComparableUri(uri);
			return UriUtils.IsBaseOf(uri2, uri3);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000C628 File Offset: 0x0000A828
		internal static List<QueryOptionQueryToken> ParseQueryOptions(Uri uri)
		{
			List<QueryOptionQueryToken> list = new List<QueryOptionQueryToken>();
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
				list.Add(new QueryOptionQueryToken(text2, text3));
				if (i == num - 1 && text.get_Chars(i) == '&')
				{
					list.Add(new QueryOptionQueryToken(null, string.Empty));
				}
			}
			return list;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000C750 File Offset: 0x0000A950
		private static Uri CreateBaseComparableUri(Uri uri)
		{
			uri = new Uri(UriUtilsCommon.UriToString(uri).ToUpper(CultureInfo.InvariantCulture), 0);
			return new UriBuilder(uri)
			{
				Host = "h",
				Port = 80,
				Scheme = "http"
			}.Uri;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		private static bool IsBaseOf(Uri baseUri, Uri uri)
		{
			return baseUri.IsBaseOf(uri);
		}
	}
}
