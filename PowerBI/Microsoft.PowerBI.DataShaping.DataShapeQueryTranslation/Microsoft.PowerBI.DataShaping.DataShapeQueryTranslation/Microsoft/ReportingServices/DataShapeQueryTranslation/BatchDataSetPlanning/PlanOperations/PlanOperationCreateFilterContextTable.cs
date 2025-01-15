using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001FB RID: 507
	internal sealed class PlanOperationCreateFilterContextTable : PlanOperation
	{
		// Token: 0x060011C0 RID: 4544 RVA: 0x00047BBD File Offset: 0x00045DBD
		internal PlanOperationCreateFilterContextTable(FilterCondition condition)
		{
			this.Condition = condition;
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00047BCC File Offset: 0x00045DCC
		internal FilterCondition Condition { get; }

		// Token: 0x060011C2 RID: 4546 RVA: 0x00047BD4 File Offset: 0x00045DD4
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00047BE0 File Offset: 0x00045DE0
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationCreateFilterContextTable planOperationCreateFilterContextTable;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationCreateFilterContextTable>(this, other, out flag, out planOperationCreateFilterContextTable))
			{
				return flag;
			}
			return this.Condition == planOperationCreateFilterContextTable.Condition;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00047C0A File Offset: 0x00045E0A
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("CreateFilterContextTable");
			builder.WriteProperty<FilterCondition>("Condition", this.Condition, false);
			builder.EndObject();
		}
	}
}
