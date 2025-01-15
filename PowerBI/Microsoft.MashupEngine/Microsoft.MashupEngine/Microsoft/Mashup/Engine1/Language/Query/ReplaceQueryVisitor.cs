using System;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200189E RID: 6302
	internal class ReplaceQueryVisitor : QueryVisitor
	{
		// Token: 0x06009FF9 RID: 40953 RVA: 0x00210A3E File Offset: 0x0020EC3E
		public ReplaceQueryVisitor(Query oldQuery, Query newQuery)
		{
			this.oldQuery = oldQuery;
			this.newQuery = newQuery;
		}

		// Token: 0x06009FFA RID: 40954 RVA: 0x00210A54 File Offset: 0x0020EC54
		public override Query VisitQuery(Query query)
		{
			if (query == this.oldQuery)
			{
				return this.newQuery;
			}
			return base.VisitQuery(query);
		}

		// Token: 0x040053C4 RID: 21444
		private Query oldQuery;

		// Token: 0x040053C5 RID: 21445
		private Query newQuery;
	}
}
