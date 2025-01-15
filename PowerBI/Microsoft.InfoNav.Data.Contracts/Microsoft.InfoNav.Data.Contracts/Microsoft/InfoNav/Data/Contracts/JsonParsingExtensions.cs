using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x0200008D RID: 141
	internal static class JsonParsingExtensions
	{
		// Token: 0x06000339 RID: 825 RVA: 0x00009343 File Offset: 0x00007543
		internal static List<T> Parse<T>(this JToken listToken, Func<JObject, T> parseItem)
		{
			return listToken.Parse(parseItem);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000934C File Offset: 0x0000754C
		internal static List<T> Parse<T, JsonValueType>(this JToken listToken, Func<JsonValueType, T> parseItem) where JsonValueType : JToken
		{
			JArray jarray = (JArray)listToken;
			List<T> list = new List<T>(jarray.Count);
			foreach (JToken jtoken in jarray)
			{
				list.Add(parseItem((JsonValueType)((object)jtoken)));
			}
			return list;
		}
	}
}
