using System;
using System.Collections.Generic;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200004F RID: 79
	internal static class QueryOptionUtils
	{
		// Token: 0x060001EF RID: 495 RVA: 0x0000AC08 File Offset: 0x00008E08
		internal static string GetQueryOptionValueAndRemove(this List<QueryOptionQueryToken> queryOptions, string queryOptionName)
		{
			QueryOptionQueryToken queryOptionQueryToken = null;
			for (int i = 0; i < queryOptions.Count; i++)
			{
				if (queryOptions[i].Name == queryOptionName)
				{
					if (queryOptionQueryToken != null)
					{
						throw new ODataException(Strings.QueryOptionUtils_QueryParameterMustBeSpecifiedOnce(queryOptionName));
					}
					queryOptionQueryToken = queryOptions[i];
					queryOptions.RemoveAt(i);
					i--;
				}
			}
			if (queryOptionQueryToken != null)
			{
				return queryOptionQueryToken.Value;
			}
			return null;
		}
	}
}
