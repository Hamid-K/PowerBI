using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000225 RID: 549
	internal sealed class PlanOperationSample : PlanOperationLimitByCount
	{
		// Token: 0x060012EA RID: 4842 RVA: 0x00049A2A File Offset: 0x00047C2A
		internal PlanOperationSample(PlanOperation input, PlanExpression rowCount, IEnumerable<PlanSortItem> sorts)
			: base(input, rowCount, sorts)
		{
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00049A35 File Offset: 0x00047C35
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00049A40 File Offset: 0x00047C40
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationSample planOperationSample;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationSample>(this, other, out flag, out planOperationSample))
			{
				return flag;
			}
			return base.CommonEquals(planOperationSample);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00049A64 File Offset: 0x00047C64
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("Sample");
			builder.WriteProperty<PlanExpression>("RowCount", base.RowCount, false);
			builder.WriteProperty<ReadOnlyCollection<PlanSortItem>>("Sorts", base.Sorts, false);
			builder.WriteProperty<PlanOperation>("Input", base.Input, false);
			builder.EndObject();
		}
	}
}
