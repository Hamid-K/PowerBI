using System;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007C2 RID: 1986
	internal abstract class ExpandedColumnFilter
	{
		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x060039C6 RID: 14790
		public abstract SingleResourceNode Source { get; }

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x060039C7 RID: 14791
		public abstract SingleValueNode OuterFilterNode { get; }

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x060039C8 RID: 14792
		public abstract FilterClause InnerFilterClause { get; }

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x060039C9 RID: 14793
		public abstract FilterClause OuterFilterClause { get; }

		// Token: 0x060039CA RID: 14794
		public abstract ExpandedColumnFilter AppendOuterFilter(ExpandedColumnFilter right);
	}
}
