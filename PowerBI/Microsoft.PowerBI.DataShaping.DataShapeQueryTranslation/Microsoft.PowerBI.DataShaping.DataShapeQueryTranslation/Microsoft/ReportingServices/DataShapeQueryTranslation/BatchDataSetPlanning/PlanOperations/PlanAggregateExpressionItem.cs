using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000210 RID: 528
	internal sealed class PlanAggregateExpressionItem : PlanAggregateItem
	{
		// Token: 0x0600125B RID: 4699 RVA: 0x00048C3B File Offset: 0x00046E3B
		internal PlanAggregateExpressionItem(string planName, ExpressionId expressionId, ExpressionContext expressionContext, bool preferPlanName = false)
		{
			this.m_planName = planName;
			this.m_expressionId = expressionId;
			this.m_expressionContext = expressionContext;
			this.m_preferPlanName = preferPlanName;
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x00048C60 File Offset: 0x00046E60
		public string PlanName
		{
			get
			{
				return this.m_planName;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00048C68 File Offset: 0x00046E68
		public ExpressionId ExpressionId
		{
			get
			{
				return this.m_expressionId;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x00048C70 File Offset: 0x00046E70
		public ExpressionContext ExpressionContext
		{
			get
			{
				return this.m_expressionContext;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00048C78 File Offset: 0x00046E78
		internal override bool PreferPlanName
		{
			get
			{
				return this.m_preferPlanName;
			}
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00048C80 File Offset: 0x00046E80
		internal override void Accept(IPlanAggregateItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00048C8C File Offset: 0x00046E8C
		protected override int GetHashCodeInternal()
		{
			return Hashing.CombineHash(this.m_planName.GetHashCode(), Hashing.CombineHash(this.m_expressionId.GetHashCode(), this.m_expressionContext.GetHashCode()));
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00048CD0 File Offset: 0x00046ED0
		public override bool Equals(PlanAggregateItem other)
		{
			PlanAggregateExpressionItem planAggregateExpressionItem = other as PlanAggregateExpressionItem;
			return planAggregateExpressionItem != null && this.PlanName == planAggregateExpressionItem.PlanName && this.ExpressionId == planAggregateExpressionItem.ExpressionId && this.ExpressionContext == planAggregateExpressionItem.ExpressionContext && this.PreferPlanName == planAggregateExpressionItem.PreferPlanName;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00048D2B File Offset: 0x00046F2B
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("AggregateExpression");
			builder.WriteAttribute<string>("PlanName", this.PlanName, false, false);
			builder.WriteProperty<ExpressionId>("Expression", this.ExpressionId, false);
			builder.EndObject();
		}

		// Token: 0x0400083A RID: 2106
		private readonly string m_planName;

		// Token: 0x0400083B RID: 2107
		private readonly ExpressionId m_expressionId;

		// Token: 0x0400083C RID: 2108
		private readonly ExpressionContext m_expressionContext;

		// Token: 0x0400083D RID: 2109
		private readonly bool m_preferPlanName;
	}
}
