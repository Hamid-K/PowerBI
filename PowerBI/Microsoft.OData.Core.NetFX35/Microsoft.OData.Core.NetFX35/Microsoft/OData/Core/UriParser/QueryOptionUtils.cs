using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Syntactic;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x0200021B RID: 539
	internal static class QueryOptionUtils
	{
		// Token: 0x060013A0 RID: 5024 RVA: 0x000484C4 File Offset: 0x000466C4
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

		// Token: 0x060013A1 RID: 5025 RVA: 0x00048594 File Offset: 0x00046794
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

		// Token: 0x060013A2 RID: 5026 RVA: 0x0004860C File Offset: 0x0004680C
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
	}
}
