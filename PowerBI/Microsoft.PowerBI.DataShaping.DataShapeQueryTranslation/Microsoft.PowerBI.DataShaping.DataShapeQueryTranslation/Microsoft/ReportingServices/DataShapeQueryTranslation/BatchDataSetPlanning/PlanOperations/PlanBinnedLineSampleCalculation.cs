using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001F5 RID: 501
	internal sealed class PlanBinnedLineSampleCalculation : PlanBinnedLineSampleItem
	{
		// Token: 0x06001154 RID: 4436 RVA: 0x00046F3B File Offset: 0x0004513B
		internal PlanBinnedLineSampleCalculation(Calculation calculation)
		{
			this.m_calculation = calculation;
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x00046F4A File Offset: 0x0004514A
		public Calculation Calculation
		{
			get
			{
				return this.m_calculation;
			}
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00046F52 File Offset: 0x00045152
		internal override void Accept(IPlanBinnedLineSampleItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00046F5B File Offset: 0x0004515B
		protected override int GetHashCodeInternal()
		{
			return this.m_calculation.GetHashCode();
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00046F68 File Offset: 0x00045168
		public override bool Equals(PlanBinnedLineSampleItem other)
		{
			PlanBinnedLineSampleCalculation planBinnedLineSampleCalculation = other as PlanBinnedLineSampleCalculation;
			return planBinnedLineSampleCalculation != null && this.Calculation == planBinnedLineSampleCalculation.Calculation;
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00046F8F File Offset: 0x0004518F
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("BinnedLineSampleCalculation");
			builder.WriteProperty<Calculation>("Calculation", this.m_calculation, false);
			builder.EndObject();
		}

		// Token: 0x04000804 RID: 2052
		private readonly Calculation m_calculation;
	}
}
