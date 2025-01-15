using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000AE RID: 174
	internal class QueryGroupSingleValue : QueryGroupValue
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x00018A18 File Offset: 0x00016C18
		internal QueryGroupSingleValue(ProjectedDsqExpression projectedExpression, QueryGroupValueBindingHints bindingHints)
		{
			this.ProjectedExpression = projectedExpression;
			this.BindingHints = bindingHints;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00018A2E File Offset: 0x00016C2E
		internal override bool? IsScalar
		{
			get
			{
				return this.ProjectedExpression.IsScalar;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00018A3B File Offset: 0x00016C3B
		internal ProjectedDsqExpression ProjectedExpression { get; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00018A43 File Offset: 0x00016C43
		internal QueryGroupValueBindingHints BindingHints { get; }

		// Token: 0x0600066B RID: 1643 RVA: 0x00018A4B File Offset: 0x00016C4B
		internal override bool MatchesExpression(ExpressionNode expression)
		{
			return this.ProjectedExpression.MatchesExpression(expression);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00018A59 File Offset: 0x00016C59
		internal override T Accept<T>(IQueryGroupValueVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00018A62 File Offset: 0x00016C62
		internal QueryGroupValue CloneWithOverride(ProjectedDsqExpression projectedExpression)
		{
			return new QueryGroupSingleValue(projectedExpression, this.BindingHints);
		}
	}
}
