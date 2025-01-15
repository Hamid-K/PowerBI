using System;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007C1 RID: 1985
	internal sealed class ExpandedColumnClause
	{
		// Token: 0x060039C3 RID: 14787 RVA: 0x000BA970 File Offset: 0x000B8B70
		public ExpandedColumnClause(SelectExpandClause selectExpandClause, ExpandedColumnFilter expandedColumnFilter)
		{
			this.selectExpandClause = selectExpandClause;
			this.expandedColumnFilter = expandedColumnFilter;
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x060039C4 RID: 14788 RVA: 0x000BA986 File Offset: 0x000B8B86
		public SelectExpandClause SelectExpandClause
		{
			get
			{
				return this.selectExpandClause;
			}
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x060039C5 RID: 14789 RVA: 0x000BA98E File Offset: 0x000B8B8E
		public ExpandedColumnFilter Filter
		{
			get
			{
				return this.expandedColumnFilter;
			}
		}

		// Token: 0x04001DEF RID: 7663
		private readonly SelectExpandClause selectExpandClause;

		// Token: 0x04001DF0 RID: 7664
		private readonly ExpandedColumnFilter expandedColumnFilter;
	}
}
