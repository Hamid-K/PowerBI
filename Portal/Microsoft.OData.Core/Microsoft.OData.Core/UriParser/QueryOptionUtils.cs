using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000168 RID: 360
	internal static class QueryOptionUtils
	{
		// Token: 0x0600124C RID: 4684 RVA: 0x00037B24 File Offset: 0x00035D24
		internal static Dictionary<string, string> GetParameterAliases(this List<CustomQueryOptionToken> queryOptions)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
			List<CustomQueryOptionToken> list = new List<CustomQueryOptionToken>();
			foreach (CustomQueryOptionToken customQueryOptionToken in queryOptions)
			{
				if (!string.IsNullOrEmpty(customQueryOptionToken.Name) && customQueryOptionToken.Name[0] == '@')
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

		// Token: 0x0600124D RID: 4685 RVA: 0x00037BF4 File Offset: 0x00035DF4
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

		// Token: 0x0600124E RID: 4686 RVA: 0x00037C6C File Offset: 0x00035E6C
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

		// Token: 0x0600124F RID: 4687 RVA: 0x00037CD0 File Offset: 0x00035ED0
		internal static List<CustomQueryOptionToken> ParseQueryOptions(Uri uri)
		{
			List<CustomQueryOptionToken> list = new List<CustomQueryOptionToken>();
			string text = uri.Query.Replace('+', ' ');
			int num;
			if (text != null)
			{
				if (text.Length > 0 && text[0] == '?')
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
					char c = text[i];
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
				if (i == num - 1 && text[i] == '&')
				{
					list.Add(new CustomQueryOptionToken(null, string.Empty));
				}
			}
			return list;
		}
	}
}
