using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000200 RID: 512
	internal sealed class PlanOperationDistinctRows : PlanOperation
	{
		// Token: 0x060011DA RID: 4570 RVA: 0x00047E7C File Offset: 0x0004607C
		internal PlanOperationDistinctRows(PlanOperation input)
		{
			this.m_input = input;
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x00047E8B File Offset: 0x0004608B
		public PlanOperation Input
		{
			get
			{
				return this.m_input;
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00047E93 File Offset: 0x00046093
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00047E9C File Offset: 0x0004609C
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationDistinctRows planOperationDistinctRows;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationDistinctRows>(this, other, out flag, out planOperationDistinctRows))
			{
				return flag;
			}
			return this.Input.Equals(planOperationDistinctRows.Input);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00047EC9 File Offset: 0x000460C9
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("DistinctRows");
			builder.WriteProperty<PlanOperation>("Input", this.Input, false);
			builder.EndObject();
		}

		// Token: 0x04000815 RID: 2069
		private readonly PlanOperation m_input;
	}
}
