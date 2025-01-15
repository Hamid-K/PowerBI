using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001FC RID: 508
	internal sealed class PlanOperationCrossJoin : PlanOperation
	{
		// Token: 0x060011C5 RID: 4549 RVA: 0x00047C2F File Offset: 0x00045E2F
		internal PlanOperationCrossJoin(IList<PlanOperation> tables)
		{
			this.Tables = tables.AsReadOnlyCollection<PlanOperation>();
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x00047C43 File Offset: 0x00045E43
		public ReadOnlyCollection<PlanOperation> Tables { get; }

		// Token: 0x060011C7 RID: 4551 RVA: 0x00047C4B File Offset: 0x00045E4B
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00047C54 File Offset: 0x00045E54
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationCrossJoin planOperationCrossJoin;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationCrossJoin>(this, other, out flag, out planOperationCrossJoin))
			{
				return flag;
			}
			return this.Tables.SequenceEqual(planOperationCrossJoin.Tables);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00047C81 File Offset: 0x00045E81
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("CrossJoin");
			builder.WriteProperty<ReadOnlyCollection<PlanOperation>>("Tables", this.Tables, false);
			builder.EndObject();
		}
	}
}
