using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001A2 RID: 418
	internal sealed class QueryMeasureDeclarationExpression : QueryBaseDeclarationExpression
	{
		// Token: 0x060015A7 RID: 5543 RVA: 0x0003C80A File Offset: 0x0003AA0A
		internal QueryMeasureDeclarationExpression(QueryExpression expr, QueryMeasureExpression measureRef)
			: base(expr.ConceptualResultType)
		{
			this._expr = expr;
			this._measureRef = measureRef;
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x0003C826 File Offset: 0x0003AA26
		public QueryExpression Expression
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x0003C82E File Offset: 0x0003AA2E
		public QueryMeasureExpression MeasureRef
		{
			get
			{
				return this._measureRef;
			}
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0003C838 File Offset: 0x0003AA38
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryMeasureDeclarationExpression queryMeasureDeclarationExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryMeasureDeclarationExpression>(this, other, out flag, out queryMeasureDeclarationExpression))
			{
				return flag;
			}
			return this.Expression.Equals(queryMeasureDeclarationExpression.Expression) && this.MeasureRef.Equals(queryMeasureDeclarationExpression.MeasureRef);
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0003C87A File Offset: 0x0003AA7A
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Expression.GetHashCode(), this.MeasureRef.GetHashCode());
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0003C897 File Offset: 0x0003AA97
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000B95 RID: 2965
		private readonly QueryExpression _expr;

		// Token: 0x04000B96 RID: 2966
		private readonly QueryMeasureExpression _measureRef;
	}
}
