using System;
using System.Collections.Generic;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001C2 RID: 450
	internal sealed class QueryTopNPerLevelWindowExpansion
	{
		// Token: 0x06001655 RID: 5717 RVA: 0x0003DD5F File Offset: 0x0003BF5F
		public QueryTopNPerLevelWindowExpansion(IReadOnlyList<QueryExpression> values, IReadOnlyList<QueryTopNPerLevelWindowExpansionValue> windowValues, IReadOnlyList<QueryTopNPerLevelWindowExpansion> children)
		{
			this.Values = values;
			this.WindowValues = windowValues;
			this.Children = children;
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x0003DD7C File Offset: 0x0003BF7C
		public IReadOnlyList<QueryExpression> Values { get; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x0003DD84 File Offset: 0x0003BF84
		public IReadOnlyList<QueryTopNPerLevelWindowExpansionValue> WindowValues { get; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x0003DD8C File Offset: 0x0003BF8C
		public IReadOnlyList<QueryTopNPerLevelWindowExpansion> Children { get; }
	}
}
