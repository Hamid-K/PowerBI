using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200021C RID: 540
	internal sealed class PlanCalculationProjectItem : PlanProjectItem
	{
		// Token: 0x060012AA RID: 4778 RVA: 0x000493FB File Offset: 0x000475FB
		internal PlanCalculationProjectItem(Calculation calculation)
		{
			this.Calculation = calculation;
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x0004940A File Offset: 0x0004760A
		public Calculation Calculation { get; }

		// Token: 0x060012AC RID: 4780 RVA: 0x00049412 File Offset: 0x00047612
		public override void Accept(IPlanProjectItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0004941B File Offset: 0x0004761B
		protected override int GetHashCodeInternal()
		{
			return this.Calculation.GetHashCode();
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00049428 File Offset: 0x00047628
		public override bool Equals(PlanProjectItem other)
		{
			PlanCalculationProjectItem planCalculationProjectItem = other as PlanCalculationProjectItem;
			return planCalculationProjectItem != null && this.Calculation == planCalculationProjectItem.Calculation;
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0004944F File Offset: 0x0004764F
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("CalculationProjectItem");
			builder.WriteAttribute<string>("Calculation", this.Calculation.Id.Value, false, false);
			builder.EndObject();
		}
	}
}
