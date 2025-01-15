using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000183 RID: 387
	internal sealed class QueryFieldReferenceNameExpression : QueryExtensionExpressionBase
	{
		// Token: 0x0600150F RID: 5391 RVA: 0x0003B6A2 File Offset: 0x000398A2
		internal QueryFieldReferenceNameExpression(QueryExpression table, string internalFieldName, ConceptualResultType conceptualResultType)
			: base(conceptualResultType)
		{
			this._table = table;
			this._internalFieldName = internalFieldName;
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x0003B6B9 File Offset: 0x000398B9
		public QueryExpression Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x0003B6C1 File Offset: 0x000398C1
		public string InternalFieldName
		{
			get
			{
				return this._internalFieldName;
			}
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0003B6C9 File Offset: 0x000398C9
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0003B6D4 File Offset: 0x000398D4
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryFieldReferenceNameExpression queryFieldReferenceNameExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryFieldReferenceNameExpression>(this, other, out flag, out queryFieldReferenceNameExpression))
			{
				return flag;
			}
			return this.Table.Equals(queryFieldReferenceNameExpression.Table) && QueryNamingContext.NameComparer.Equals(this.InternalFieldName, queryFieldReferenceNameExpression.InternalFieldName);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0003B71B File Offset: 0x0003991B
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Table.GetHashCode(), this.InternalFieldName.GetHashCode());
		}

		// Token: 0x04000B45 RID: 2885
		private readonly QueryExpression _table;

		// Token: 0x04000B46 RID: 2886
		private readonly string _internalFieldName;
	}
}
