using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000123 RID: 291
	internal static class QueryOptionUtils
	{
		// Token: 0x06000D74 RID: 3444 RVA: 0x000281F0 File Offset: 0x000263F0
		internal static Dictionary<string, string> GetParameterAliases(this List<CustomQueryOptionToken> queryOptions)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
			List<CustomQueryOptionToken> list = new List<CustomQueryOptionToken>();
			foreach (CustomQueryOptionToken customQueryOptionToken in queryOptions)
			{
				if (!string.IsNullOrEmpty(customQueryOptionToken.Name) && customQueryOptionToken.Name.get_Chars(0) == '@')
				{
					dictionary[customQueryOptionToken.Name] = customQueryOptionToken.Value;
					list.Add(customQueryOptionToken);
				}
			}
			foreach (CustomQueryOptionToken customQueryOptionToken2 in list)
			{
				queryOptions.Remove(customQueryOptionToken2);
			}
			return dictionary;
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x000282C0 File Offset: 0x000264C0
		internal static string GetQueryOptionValue(this List<CustomQueryOptionToken> queryOptions, string queryOptionName)
		{
			CustomQueryOptionToken customQueryOptionToken = null;
			foreach (CustomQueryOptionToken customQueryOptionToken2 in queryOptions)
			{
				if (customQueryOptionToken2.Name == queryOptionName)
				{
					if (customQueryOptionToken != null)
					{
						throw new ODataException(Strings.QueryOptionUtils_QueryParameterMustBeSpecifiedOnce(queryOptionName));
					}
					customQueryOptionToken = customQueryOptionToken2;
				}
			}
			if (customQueryOptionToken != null)
			{
				return customQueryOptionToken.Value;
			}
			return null;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00028338 File Offset: 0x00026538
		internal static string GetQueryOptionValueAndRemove(this List<CustomQueryOptionToken> queryOptions, string queryOptionName)
		{
			CustomQueryOptionToken customQueryOptionToken = null;
			for (int i = 0; i < queryOptions.Count; i++)
			{
				if (queryOptions[i].Name == queryOptionName)
				{
					if (customQueryOptionToken != null)
					{
						throw new ODataException(Strings.QueryOptionUtils_QueryParameterMustBeSpecifiedOnce(queryOptionName));
					}
					customQueryOptionToken = queryOptions[i];
					queryOptions.RemoveAt(i);
					i--;
				}
			}
			if (customQueryOptionToken != null)
			{
				return customQueryOptionToken.Value;
			}
			return null;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0002839C File Offset: 0x0002659C
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
	}
}
