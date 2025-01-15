using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001F0 RID: 496
	internal sealed class PlanOperationAddMissingItemsCompatPattern : PlanOperation
	{
		// Token: 0x0600112A RID: 4394 RVA: 0x00046A00 File Offset: 0x00044C00
		internal PlanOperationAddMissingItemsCompatPattern(IEnumerable<PlanGroupByMember> members, IReadOnlyList<Calculation> measureJoinConstraints, IReadOnlyList<PlanOperation> contextTables, bool allowBlankRow)
		{
			this.Members = members.ToReadOnlyCollection<PlanGroupByMember>();
			Contract.RetailAssert(this.Members.Count > 0, "AddMissingItemsCompatPattern requires at least one member.");
			this.ContextTables = contextTables;
			this.MeasureJoinConstraints = measureJoinConstraints;
			this.AllowBlankRow = allowBlankRow;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x00046A4D File Offset: 0x00044C4D
		public IReadOnlyList<PlanGroupByMember> Members { get; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x00046A55 File Offset: 0x00044C55
		public IReadOnlyList<PlanOperation> ContextTables { get; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x00046A5D File Offset: 0x00044C5D
		public IReadOnlyList<Calculation> MeasureJoinConstraints { get; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00046A65 File Offset: 0x00044C65
		public bool AllowBlankRow { get; }

		// Token: 0x0600112F RID: 4399 RVA: 0x00046A6D File Offset: 0x00044C6D
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00046A78 File Offset: 0x00044C78
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationAddMissingItemsCompatPattern planOperationAddMissingItemsCompatPattern;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationAddMissingItemsCompatPattern>(this, other, out flag, out planOperationAddMissingItemsCompatPattern))
			{
				return flag;
			}
			return this.ContextTables.SequenceEqual(planOperationAddMissingItemsCompatPattern.ContextTables) && ((this.MeasureJoinConstraints == null) ? (planOperationAddMissingItemsCompatPattern.MeasureJoinConstraints == null) : this.MeasureJoinConstraints.SequenceEqual(planOperationAddMissingItemsCompatPattern.MeasureJoinConstraints)) && this.Members.SequenceEqual(planOperationAddMissingItemsCompatPattern.Members) && this.AllowBlankRow == planOperationAddMissingItemsCompatPattern.AllowBlankRow;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00046AF0 File Offset: 0x00044CF0
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("AddMissingItemsCompatPattern");
			builder.WriteProperty<IReadOnlyList<PlanGroupByMember>>("ShowAllMembers", this.Members, false);
			builder.WriteProperty<IReadOnlyList<Calculation>>("MeasureJoinConstraints", this.MeasureJoinConstraints, false);
			builder.WriteProperty<IReadOnlyList<PlanOperation>>("ContextTables", this.ContextTables, false);
			builder.WriteProperty<bool>("AllowBlankRow", this.AllowBlankRow, false);
			builder.EndObject();
		}
	}
}
