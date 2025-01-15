using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000172 RID: 370
	internal sealed class QueryDataTableExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001475 RID: 5237 RVA: 0x0003AF27 File Offset: 0x00039127
		internal QueryDataTableExpression(ConceptualResultType conceptualResultType, IReadOnlyList<QueryTupleExpression> rows, IReadOnlyList<string> columnNames)
			: base(conceptualResultType)
		{
			this._rows = rows;
			this._columnNames = columnNames;
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x0003AF3E File Offset: 0x0003913E
		public IReadOnlyList<QueryTupleExpression> Rows
		{
			get
			{
				return this._rows;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0003AF46 File Offset: 0x00039146
		public IReadOnlyList<string> ColumnNames
		{
			get
			{
				return this._columnNames;
			}
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0003AF4E File Offset: 0x0003914E
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0003AF58 File Offset: 0x00039158
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryDataTableExpression queryDataTableExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryDataTableExpression>(this, other, out flag, out queryDataTableExpression))
			{
				return flag;
			}
			return this.Rows.SequenceEqual(queryDataTableExpression.Rows) && this.ColumnNames.SequenceEqual(queryDataTableExpression.ColumnNames);
		}

		// Token: 0x04000B2E RID: 2862
		private readonly IReadOnlyList<QueryTupleExpression> _rows;

		// Token: 0x04000B2F RID: 2863
		private readonly IReadOnlyList<string> _columnNames;
	}
}
