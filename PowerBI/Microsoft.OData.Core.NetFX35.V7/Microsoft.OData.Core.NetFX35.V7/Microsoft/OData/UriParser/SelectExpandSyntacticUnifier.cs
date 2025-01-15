using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000F8 RID: 248
	internal static class SelectExpandSyntacticUnifier
	{
		// Token: 0x06000BE4 RID: 3044 RVA: 0x0001F2D4 File Offset: 0x0001D4D4
		public static ExpandToken Combine(ExpandToken expand, SelectToken select)
		{
			ExpandTermToken expandTermToken = new ExpandTermToken(new SystemToken("$it", null), select, expand);
			List<ExpandTermToken> list = new List<ExpandTermToken>();
			list.Add(expandTermToken);
			return new ExpandToken(list);
		}
	}
}
