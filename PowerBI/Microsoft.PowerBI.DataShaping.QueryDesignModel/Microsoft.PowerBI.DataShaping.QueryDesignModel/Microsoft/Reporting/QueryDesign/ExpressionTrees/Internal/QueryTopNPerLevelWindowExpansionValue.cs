using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001C3 RID: 451
	internal sealed class QueryTopNPerLevelWindowExpansionValue
	{
		// Token: 0x06001659 RID: 5721 RVA: 0x0003DD94 File Offset: 0x0003BF94
		public QueryTopNPerLevelWindowExpansionValue(IReadOnlyList<QueryExpression> values, WindowKind windowKind)
		{
			this.Values = values;
			this.WindowKind = windowKind;
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0003DDAA File Offset: 0x0003BFAA
		public IReadOnlyList<QueryExpression> Values { get; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x0003DDB2 File Offset: 0x0003BFB2
		public WindowKind WindowKind { get; }
	}
}
