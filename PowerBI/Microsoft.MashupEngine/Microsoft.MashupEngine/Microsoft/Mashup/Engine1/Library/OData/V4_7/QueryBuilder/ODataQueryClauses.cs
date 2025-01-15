using System;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007E8 RID: 2024
	internal sealed class ODataQueryClauses
	{
		// Token: 0x06003AA9 RID: 15017 RVA: 0x000BE1D0 File Offset: 0x000BC3D0
		public ODataQueryClauses(SelectExpandClause selectExpandClause, FilterClause outerFilterClause)
		{
			this.selectExpandClause = selectExpandClause;
			this.outerFilterClause = outerFilterClause;
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x06003AAA RID: 15018 RVA: 0x000BE1E6 File Offset: 0x000BC3E6
		public SelectExpandClause SelectExpandClause
		{
			get
			{
				return this.selectExpandClause;
			}
		}

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x06003AAB RID: 15019 RVA: 0x000BE1EE File Offset: 0x000BC3EE
		public FilterClause OuterFilterClause
		{
			get
			{
				return this.outerFilterClause;
			}
		}

		// Token: 0x04001E6F RID: 7791
		private readonly SelectExpandClause selectExpandClause;

		// Token: 0x04001E70 RID: 7792
		private readonly FilterClause outerFilterClause;
	}
}
