using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000AF RID: 175
	internal class QueryGroupIntervalValue : QueryGroupValue
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x00018A70 File Offset: 0x00016C70
		internal QueryGroupIntervalValue(ProjectedDsqExpression minExpression, ProjectedDsqExpression maxExpression, IConceptualColumn sourceField)
		{
			this.MinExpression = minExpression;
			this.MaxExpression = maxExpression;
			this.SourceField = sourceField;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00018A8D File Offset: 0x00016C8D
		internal ProjectedDsqExpression MinExpression { get; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00018A95 File Offset: 0x00016C95
		internal ProjectedDsqExpression MaxExpression { get; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00018A9D File Offset: 0x00016C9D
		internal IConceptualColumn SourceField { get; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00018AA5 File Offset: 0x00016CA5
		internal override bool? IsScalar
		{
			get
			{
				return new bool?(false);
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00018AAD File Offset: 0x00016CAD
		internal override T Accept<T>(IQueryGroupValueVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00018AB6 File Offset: 0x00016CB6
		internal override bool MatchesExpression(ExpressionNode expression)
		{
			return false;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00018AB9 File Offset: 0x00016CB9
		internal QueryGroupValue CloneWithOverride(ProjectedDsqExpression minExpression, ProjectedDsqExpression maxExpression)
		{
			return new QueryGroupIntervalValue(minExpression, maxExpression, this.SourceField);
		}
	}
}
