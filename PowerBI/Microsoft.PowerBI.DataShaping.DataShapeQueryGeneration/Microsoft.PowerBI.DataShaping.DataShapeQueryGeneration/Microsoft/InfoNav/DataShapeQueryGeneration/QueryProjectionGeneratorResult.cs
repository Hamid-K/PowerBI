using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000BB RID: 187
	internal sealed class QueryProjectionGeneratorResult
	{
		// Token: 0x060006BC RID: 1724 RVA: 0x00019460 File Offset: 0x00017660
		internal QueryProjectionGeneratorResult(Dictionary<ResolvedSemanticQueryDataShape, QueryProjections> mapping)
		{
			this._mapping = mapping;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00019470 File Offset: 0x00017670
		internal QueryProjections GetProjections(ResolvedSemanticQueryDataShape rsqds)
		{
			QueryProjections queryProjections;
			if (this._mapping.TryGetValue(rsqds, out queryProjections))
			{
				return queryProjections;
			}
			return null;
		}

		// Token: 0x04000395 RID: 917
		private readonly Dictionary<ResolvedSemanticQueryDataShape, QueryProjections> _mapping;
	}
}
