using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200021E RID: 542
	internal sealed class PlanPreserveCalculationProjectItem : PlanProjectItem
	{
		// Token: 0x060012B7 RID: 4791 RVA: 0x00049562 File Offset: 0x00047762
		internal PlanPreserveCalculationProjectItem(Calculation calculation)
		{
			this.Calculation = calculation;
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x00049571 File Offset: 0x00047771
		public Calculation Calculation { get; }

		// Token: 0x060012B9 RID: 4793 RVA: 0x00049579 File Offset: 0x00047779
		public override void Accept(IPlanProjectItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00049582 File Offset: 0x00047782
		protected override int GetHashCodeInternal()
		{
			return this.Calculation.GetHashCode();
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00049590 File Offset: 0x00047790
		public override bool Equals(PlanProjectItem other)
		{
			PlanPreserveCalculationProjectItem planPreserveCalculationProjectItem = other as PlanPreserveCalculationProjectItem;
			return planPreserveCalculationProjectItem != null && this.Calculation == planPreserveCalculationProjectItem.Calculation;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x000495B7 File Offset: 0x000477B7
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("PreserveCalculationProjectItem");
			builder.WriteAttribute<string>("Calculation", this.Calculation.Id.Value, false, false);
			builder.EndObject();
		}
	}
}
