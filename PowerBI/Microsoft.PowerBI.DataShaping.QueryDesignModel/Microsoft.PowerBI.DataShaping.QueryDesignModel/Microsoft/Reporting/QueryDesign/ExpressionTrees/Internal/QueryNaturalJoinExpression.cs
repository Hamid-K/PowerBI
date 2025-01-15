using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001A5 RID: 421
	internal sealed class QueryNaturalJoinExpression : QueryExtensionExpressionBase
	{
		// Token: 0x060015BB RID: 5563 RVA: 0x0003CA40 File Offset: 0x0003AC40
		internal QueryNaturalJoinExpression(NaturalJoinKind joinKind, ConceptualResultType conceptualResultType, QueryExpressionBinding left, QueryExpressionBinding right)
			: base(conceptualResultType)
		{
			this._joinKind = joinKind;
			this._left = ArgumentValidation.CheckNotNull<QueryExpressionBinding>(left, "left");
			this._right = ArgumentValidation.CheckNotNull<QueryExpressionBinding>(right, "right");
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x0003CA73 File Offset: 0x0003AC73
		public NaturalJoinKind JoinKind
		{
			get
			{
				return this._joinKind;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x0003CA7B File Offset: 0x0003AC7B
		public QueryExpressionBinding Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x0003CA83 File Offset: 0x0003AC83
		public QueryExpressionBinding Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0003CA8B File Offset: 0x0003AC8B
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0003CAA0 File Offset: 0x0003ACA0
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryNaturalJoinExpression queryNaturalJoinExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryNaturalJoinExpression>(this, other, out flag, out queryNaturalJoinExpression))
			{
				return flag;
			}
			return this.JoinKind == queryNaturalJoinExpression.JoinKind && this.Left.Equals(queryNaturalJoinExpression.Left) && this.Right.Equals(queryNaturalJoinExpression.Right);
		}

		// Token: 0x04000B9D RID: 2973
		private readonly NaturalJoinKind _joinKind;

		// Token: 0x04000B9E RID: 2974
		private readonly QueryExpressionBinding _left;

		// Token: 0x04000B9F RID: 2975
		private readonly QueryExpressionBinding _right;
	}
}
