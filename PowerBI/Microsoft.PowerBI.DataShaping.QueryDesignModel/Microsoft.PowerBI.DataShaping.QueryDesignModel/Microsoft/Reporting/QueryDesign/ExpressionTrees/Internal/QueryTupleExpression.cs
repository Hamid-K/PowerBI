using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001C5 RID: 453
	internal sealed class QueryTupleExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001661 RID: 5729 RVA: 0x0003DE42 File Offset: 0x0003C042
		internal QueryTupleExpression(ConceptualResultType conceptualResultType, IReadOnlyList<KeyValuePair<string, QueryExpression>> namedColumns)
			: base(conceptualResultType)
		{
			this._namedColumns = namedColumns;
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x0003DE52 File Offset: 0x0003C052
		internal IReadOnlyList<KeyValuePair<string, QueryExpression>> NamedColumns
		{
			get
			{
				return this._namedColumns;
			}
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0003DE5A File Offset: 0x0003C05A
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0003DE64 File Offset: 0x0003C064
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryTupleExpression queryTupleExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryTupleExpression>(this, other, out flag, out queryTupleExpression))
			{
				return flag;
			}
			return this.NamedColumns.SequenceEqual(queryTupleExpression.NamedColumns);
		}

		// Token: 0x04000BF2 RID: 3058
		private readonly IReadOnlyList<KeyValuePair<string, QueryExpression>> _namedColumns;
	}
}
