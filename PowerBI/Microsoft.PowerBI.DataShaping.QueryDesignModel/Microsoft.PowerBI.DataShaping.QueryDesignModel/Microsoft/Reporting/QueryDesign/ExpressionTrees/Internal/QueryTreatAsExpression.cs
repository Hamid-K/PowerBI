using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001C4 RID: 452
	internal sealed class QueryTreatAsExpression : QueryExtensionExpressionBase
	{
		// Token: 0x0600165C RID: 5724 RVA: 0x0003DDBA File Offset: 0x0003BFBA
		internal QueryTreatAsExpression(ConceptualResultType conceptualResultType, QueryExpression inputTable, IReadOnlyList<KeyValuePair<string, QueryExpression>> columns)
			: base(conceptualResultType)
		{
			IReadOnlyList<ConceptualTypeColumn> columns2 = ((ConceptualTableType)inputTable.ConceptualResultType).RowType.Columns;
			this._inputTable = inputTable;
			this._columns = columns;
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x0003DDE7 File Offset: 0x0003BFE7
		internal QueryExpression InputTable
		{
			get
			{
				return this._inputTable;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0003DDEF File Offset: 0x0003BFEF
		internal IReadOnlyList<KeyValuePair<string, QueryExpression>> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0003DDF7 File Offset: 0x0003BFF7
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0003DE00 File Offset: 0x0003C000
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryTreatAsExpression queryTreatAsExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryTreatAsExpression>(this, other, out flag, out queryTreatAsExpression))
			{
				return flag;
			}
			return this.InputTable.Equals(queryTreatAsExpression.InputTable) && this.Columns.SequenceEqual(queryTreatAsExpression.Columns);
		}

		// Token: 0x04000BF0 RID: 3056
		private readonly QueryExpression _inputTable;

		// Token: 0x04000BF1 RID: 3057
		private readonly IReadOnlyList<KeyValuePair<string, QueryExpression>> _columns;
	}
}
