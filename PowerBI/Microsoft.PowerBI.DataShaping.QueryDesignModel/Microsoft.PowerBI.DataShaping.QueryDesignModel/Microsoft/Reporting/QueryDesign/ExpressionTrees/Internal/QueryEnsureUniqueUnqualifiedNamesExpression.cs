using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000177 RID: 375
	internal sealed class QueryEnsureUniqueUnqualifiedNamesExpression : QueryExtensionExpressionBase
	{
		// Token: 0x0600148F RID: 5263 RVA: 0x0003B193 File Offset: 0x00039393
		internal QueryEnsureUniqueUnqualifiedNamesExpression(QueryExpression table, bool forceRename)
			: base(table.ConceptualResultType)
		{
			this._table = table;
			this._forceRename = forceRename;
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x0003B1AF File Offset: 0x000393AF
		public QueryExpression Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0003B1B7 File Offset: 0x000393B7
		public bool ForceRename
		{
			get
			{
				return this._forceRename;
			}
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0003B1BF File Offset: 0x000393BF
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0003B1C8 File Offset: 0x000393C8
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryEnsureUniqueUnqualifiedNamesExpression queryEnsureUniqueUnqualifiedNamesExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryEnsureUniqueUnqualifiedNamesExpression>(this, other, out flag, out queryEnsureUniqueUnqualifiedNamesExpression))
			{
				return flag;
			}
			return this.Table.Equals(queryEnsureUniqueUnqualifiedNamesExpression.Table) && this.ForceRename == queryEnsureUniqueUnqualifiedNamesExpression.ForceRename;
		}

		// Token: 0x04000B36 RID: 2870
		private readonly QueryExpression _table;

		// Token: 0x04000B37 RID: 2871
		private readonly bool _forceRename;
	}
}
