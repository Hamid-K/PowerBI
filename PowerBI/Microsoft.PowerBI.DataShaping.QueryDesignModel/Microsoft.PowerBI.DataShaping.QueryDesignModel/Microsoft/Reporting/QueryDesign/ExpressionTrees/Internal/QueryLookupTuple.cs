using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001A1 RID: 417
	internal sealed class QueryLookupTuple : IEquatable<QueryLookupTuple>
	{
		// Token: 0x060015A1 RID: 5537 RVA: 0x0003C78A File Offset: 0x0003A98A
		internal QueryLookupTuple(QueryExpression searchColumn, QueryExpression searchValue)
		{
			this._searchColumn = searchColumn;
			this._searchValue = searchValue;
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x0003C7A0 File Offset: 0x0003A9A0
		public QueryExpression SearchColumn
		{
			get
			{
				return this._searchColumn;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x0003C7A8 File Offset: 0x0003A9A8
		public QueryExpression SearchValue
		{
			get
			{
				return this._searchValue;
			}
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0003C7B0 File Offset: 0x0003A9B0
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QuerySwitchCase);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0003C7BE File Offset: 0x0003A9BE
		public bool Equals(QueryLookupTuple other)
		{
			return this != other && other != null && this.SearchColumn.Equals(other.SearchColumn) && this.SearchValue.Equals(other.SearchValue);
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0003C7ED File Offset: 0x0003A9ED
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.SearchColumn.GetHashCode(), this.SearchValue.GetHashCode());
		}

		// Token: 0x04000B93 RID: 2963
		private readonly QueryExpression _searchColumn;

		// Token: 0x04000B94 RID: 2964
		private readonly QueryExpression _searchValue;
	}
}
