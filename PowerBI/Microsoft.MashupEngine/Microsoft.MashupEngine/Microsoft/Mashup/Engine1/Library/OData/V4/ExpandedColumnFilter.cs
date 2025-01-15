using System;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200084A RID: 2122
	internal abstract class ExpandedColumnFilter
	{
		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x06003D38 RID: 15672
		public abstract SingleValueNode OuterFilterNode { get; }

		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x06003D39 RID: 15673
		public abstract FilterClause InnerFilterClause { get; }

		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x06003D3A RID: 15674
		public abstract FilterClause OuterFilterClause { get; }

		// Token: 0x06003D3B RID: 15675
		public abstract ExpandedColumnFilter AppendOuterFilter(ExpandedColumnFilter right);

		// Token: 0x06003D3C RID: 15676
		public abstract QueryNode ExpandInnerNavigationPropertyNode(bool isRecordExpansion, Microsoft.OData.Edm.IEdmNavigationProperty navigationProperty);
	}
}
