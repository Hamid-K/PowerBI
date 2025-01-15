using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001AB RID: 427
	internal sealed class QueryOperatorExpression : QueryExpression
	{
		// Token: 0x060015CF RID: 5583 RVA: 0x0003CC20 File Offset: 0x0003AE20
		internal QueryOperatorExpression(EdmOperator edmOperator, IReadOnlyList<QueryExpression> arguments, bool useBinaryEquivalent)
			: base(edmOperator.ConceptualReturnType)
		{
			this.Operator = edmOperator;
			this.Arguments = arguments;
			this.UseBinaryEquivalent = useBinaryEquivalent;
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x0003CC43 File Offset: 0x0003AE43
		public EdmOperator Operator { get; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x0003CC4B File Offset: 0x0003AE4B
		public IReadOnlyList<QueryExpression> Arguments { get; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0003CC53 File Offset: 0x0003AE53
		public bool UseBinaryEquivalent { get; }

		// Token: 0x060015D3 RID: 5587 RVA: 0x0003CC5B File Offset: 0x0003AE5B
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0003CC64 File Offset: 0x0003AE64
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryOperatorExpression queryOperatorExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryOperatorExpression>(this, other, out flag, out queryOperatorExpression))
			{
				return flag;
			}
			return this.Operator.Equals(queryOperatorExpression.Operator) && this.Arguments.SequenceEqual(queryOperatorExpression.Arguments, QueryExpression.Comparer) && this.UseBinaryEquivalent.Equals(queryOperatorExpression.UseBinaryEquivalent);
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0003CCC4 File Offset: 0x0003AEC4
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Operator.GetHashCode(), Hashing.CombineHashReadonly<QueryExpression>(this.Arguments, QueryExpression.Comparer), this.UseBinaryEquivalent.GetHashCode());
		}
	}
}
