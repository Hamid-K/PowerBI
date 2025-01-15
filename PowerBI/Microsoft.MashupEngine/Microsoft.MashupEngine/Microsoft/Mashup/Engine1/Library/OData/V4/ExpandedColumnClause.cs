using System;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000849 RID: 2121
	internal sealed class ExpandedColumnClause
	{
		// Token: 0x06003D35 RID: 15669 RVA: 0x000C72DB File Offset: 0x000C54DB
		public ExpandedColumnClause(SelectExpandClause selectExpandClause, ExpandedColumnFilter expandedColumnFilter)
		{
			this.selectExpandClause = selectExpandClause;
			this.expandedColumnFilter = expandedColumnFilter;
		}

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x06003D36 RID: 15670 RVA: 0x000C72F1 File Offset: 0x000C54F1
		public SelectExpandClause SelectExpandClause
		{
			get
			{
				return this.selectExpandClause;
			}
		}

		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x06003D37 RID: 15671 RVA: 0x000C72F9 File Offset: 0x000C54F9
		public ExpandedColumnFilter Filter
		{
			get
			{
				return this.expandedColumnFilter;
			}
		}

		// Token: 0x04002007 RID: 8199
		private readonly SelectExpandClause selectExpandClause;

		// Token: 0x04002008 RID: 8200
		private readonly ExpandedColumnFilter expandedColumnFilter;
	}
}
