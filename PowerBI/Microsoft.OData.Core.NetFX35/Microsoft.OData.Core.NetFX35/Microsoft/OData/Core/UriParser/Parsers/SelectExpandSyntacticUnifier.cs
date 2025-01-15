using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Syntactic;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001DB RID: 475
	internal static class SelectExpandSyntacticUnifier
	{
		// Token: 0x06001175 RID: 4469 RVA: 0x0003E0C0 File Offset: 0x0003C2C0
		public static ExpandToken Combine(ExpandToken expand, SelectToken select)
		{
			ExpandTermToken expandTermToken = new ExpandTermToken(new SystemToken("$it", null), select, expand);
			List<ExpandTermToken> list = new List<ExpandTermToken>();
			list.Add(expandTermToken);
			return new ExpandToken(list);
		}
	}
}
