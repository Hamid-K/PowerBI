using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000202 RID: 514
	internal sealed class PlanOperationFilterBy : PlanOperation
	{
		// Token: 0x060011E5 RID: 4581 RVA: 0x00047F97 File Offset: 0x00046197
		internal PlanOperationFilterBy(PlanOperation input, FilterCondition condition)
		{
			this.m_input = input;
			this.m_condition = condition;
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00047FAD File Offset: 0x000461AD
		internal PlanOperationFilterBy(PlanOperation input, PlanExpression predicate)
		{
			this.m_input = input;
			this.m_predicate = predicate;
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x00047FC3 File Offset: 0x000461C3
		public PlanOperation Input
		{
			get
			{
				return this.m_input;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x00047FCB File Offset: 0x000461CB
		internal FilterCondition Condition
		{
			get
			{
				return this.m_condition;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00047FD3 File Offset: 0x000461D3
		internal PlanExpression Predicate
		{
			get
			{
				return this.m_predicate;
			}
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00047FDB File Offset: 0x000461DB
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00047FE4 File Offset: 0x000461E4
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationFilterBy planOperationFilterBy;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationFilterBy>(this, other, out flag, out planOperationFilterBy))
			{
				return flag;
			}
			return this.Input.Equals(planOperationFilterBy.Input) && object.Equals(this.Condition, planOperationFilterBy.Condition) && object.Equals(this.Predicate, planOperationFilterBy.Predicate);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0004803C File Offset: 0x0004623C
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("FilterBy");
			builder.WriteProperty<FilterCondition>("Condition", this.Condition, false);
			builder.WriteProperty<PlanExpression>("Predicate", this.Predicate, false);
			builder.WriteProperty<PlanOperation>("Input", this.Input, false);
			builder.EndObject();
		}

		// Token: 0x04000818 RID: 2072
		private readonly PlanOperation m_input;

		// Token: 0x04000819 RID: 2073
		private readonly FilterCondition m_condition;

		// Token: 0x0400081A RID: 2074
		private readonly PlanExpression m_predicate;
	}
}
