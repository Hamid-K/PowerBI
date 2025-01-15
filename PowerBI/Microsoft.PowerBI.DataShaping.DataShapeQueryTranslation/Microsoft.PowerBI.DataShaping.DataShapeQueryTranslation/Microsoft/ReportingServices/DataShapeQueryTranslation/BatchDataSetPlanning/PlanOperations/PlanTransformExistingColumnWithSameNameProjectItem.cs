using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200021B RID: 539
	internal sealed class PlanTransformExistingColumnWithSameNameProjectItem : PlanPreserveColumnsProjectItem
	{
		// Token: 0x060012A4 RID: 4772 RVA: 0x0004935A File Offset: 0x0004755A
		internal PlanTransformExistingColumnWithSameNameProjectItem(Expression existingColumnExpression, IEnumerable<ExpressionId> planIdentities)
			: base(planIdentities)
		{
			this.ExistingColumnExpression = existingColumnExpression;
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x0004936A File Offset: 0x0004756A
		public Expression ExistingColumnExpression { get; }

		// Token: 0x060012A6 RID: 4774 RVA: 0x00049372 File Offset: 0x00047572
		public override void Accept(IPlanProjectItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0004937B File Offset: 0x0004757B
		protected override int GetHashCodeInternal()
		{
			return Hashing.CombineHash(this.GetHashCode(), this.ExistingColumnExpression.GetHashCode());
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00049394 File Offset: 0x00047594
		public override bool Equals(PlanProjectItem other)
		{
			PlanTransformExistingColumnWithSameNameProjectItem planTransformExistingColumnWithSameNameProjectItem = other as PlanTransformExistingColumnWithSameNameProjectItem;
			return planTransformExistingColumnWithSameNameProjectItem != null && base.Equals(other) && this.ExistingColumnExpression == planTransformExistingColumnWithSameNameProjectItem.ExistingColumnExpression;
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x000493C4 File Offset: 0x000475C4
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("PlanTransformExistingColumnWithSameNameProjectItem");
			builder.WriteProperty<IEnumerable<ExpressionId>>("PlanIdentities", base.PlanIdentities, false);
			builder.WriteProperty<Expression>("ExistingColumnExpression", this.ExistingColumnExpression, false);
			builder.EndObject();
		}
	}
}
