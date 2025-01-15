using System;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000883 RID: 2179
	internal sealed class ODataQueryClauses
	{
		// Token: 0x06003EB6 RID: 16054 RVA: 0x000CD379 File Offset: 0x000CB579
		public ODataQueryClauses(SelectExpandClause selectExpandClause, FilterClause outerFilterClause)
		{
			this.selectExpandClause = selectExpandClause;
			this.outerFilterClause = outerFilterClause;
		}

		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x06003EB7 RID: 16055 RVA: 0x000CD38F File Offset: 0x000CB58F
		public SelectExpandClause SelectExpandClause
		{
			get
			{
				return this.selectExpandClause;
			}
		}

		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x06003EB8 RID: 16056 RVA: 0x000CD397 File Offset: 0x000CB597
		public FilterClause OuterFilterClause
		{
			get
			{
				return this.outerFilterClause;
			}
		}

		// Token: 0x040020E5 RID: 8421
		private readonly SelectExpandClause selectExpandClause;

		// Token: 0x040020E6 RID: 8422
		private readonly FilterClause outerFilterClause;
	}
}
