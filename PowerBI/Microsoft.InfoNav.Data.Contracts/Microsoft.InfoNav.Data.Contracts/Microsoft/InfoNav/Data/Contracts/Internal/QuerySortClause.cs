using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002CF RID: 719
	[DataContract(Name = "SortClause", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QuerySortClause
	{
		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x0002ACB4 File Offset: 0x00028EB4
		// (set) Token: 0x060017FB RID: 6139 RVA: 0x0002ACBC File Offset: 0x00028EBC
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x0002ACC5 File Offset: 0x00028EC5
		// (set) Token: 0x060017FD RID: 6141 RVA: 0x0002ACCD File Offset: 0x00028ECD
		[DataMember(IsRequired = true, Order = 2)]
		public QuerySortDirection Direction { get; set; }

		// Token: 0x060017FE RID: 6142 RVA: 0x0002ACD8 File Offset: 0x00028ED8
		internal void WriteQueryString(QueryStringWriter w)
		{
			this.Expression.WriteQueryString(w);
			QuerySortDirection direction = this.Direction;
			if (direction == QuerySortDirection.Ascending)
			{
				w.Write(" ascending");
				return;
			}
			if (direction != QuerySortDirection.Descending)
			{
				return;
			}
			w.Write(" descending");
		}
	}
}
