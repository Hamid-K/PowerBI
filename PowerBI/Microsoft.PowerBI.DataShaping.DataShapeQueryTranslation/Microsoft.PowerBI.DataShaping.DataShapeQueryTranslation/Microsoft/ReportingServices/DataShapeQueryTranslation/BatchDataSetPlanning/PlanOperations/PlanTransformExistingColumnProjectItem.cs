using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200021A RID: 538
	internal sealed class PlanTransformExistingColumnProjectItem : PlanPreserveColumnsProjectItem
	{
		// Token: 0x0600129D RID: 4765 RVA: 0x00049298 File Offset: 0x00047498
		internal PlanTransformExistingColumnProjectItem(Expression newColumnExpression, ExpressionContext expressionContext, IEnumerable<ExpressionId> planIdentities)
			: base(planIdentities)
		{
			this.NewColumnExpression = newColumnExpression;
			this.ExpressionContext = expressionContext;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x000492AF File Offset: 0x000474AF
		public Expression NewColumnExpression { get; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x000492B7 File Offset: 0x000474B7
		public ExpressionContext ExpressionContext { get; }

		// Token: 0x060012A0 RID: 4768 RVA: 0x000492BF File Offset: 0x000474BF
		public override void Accept(IPlanProjectItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000492C8 File Offset: 0x000474C8
		protected override int GetHashCodeInternal()
		{
			return Hashing.CombineHash(this.GetHashCode(), this.NewColumnExpression.GetHashCode());
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x000492E0 File Offset: 0x000474E0
		public override bool Equals(PlanProjectItem other)
		{
			PlanTransformExistingColumnProjectItem planTransformExistingColumnProjectItem = other as PlanTransformExistingColumnProjectItem;
			return planTransformExistingColumnProjectItem != null && base.Equals(other) && this.ExpressionContext.Equals(planTransformExistingColumnProjectItem.ExpressionContext) && this.NewColumnExpression == planTransformExistingColumnProjectItem.NewColumnExpression;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00049323 File Offset: 0x00047523
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("TransformExistingColumnProjectItem");
			builder.WriteProperty<IEnumerable<ExpressionId>>("PlanIdentities", base.PlanIdentities, false);
			builder.WriteProperty<Expression>("NewColumnExpression", this.NewColumnExpression, false);
			builder.EndObject();
		}
	}
}
