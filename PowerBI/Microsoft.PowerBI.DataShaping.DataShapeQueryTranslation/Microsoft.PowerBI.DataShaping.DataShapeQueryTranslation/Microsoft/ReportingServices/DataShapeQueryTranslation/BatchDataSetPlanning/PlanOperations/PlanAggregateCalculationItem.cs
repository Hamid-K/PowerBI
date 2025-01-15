using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200020F RID: 527
	internal sealed class PlanAggregateCalculationItem : PlanAggregateItem
	{
		// Token: 0x06001254 RID: 4692 RVA: 0x00048BAE File Offset: 0x00046DAE
		internal PlanAggregateCalculationItem(Calculation calculation)
		{
			this.m_calculation = calculation;
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x00048BBD File Offset: 0x00046DBD
		public Calculation Calculation
		{
			get
			{
				return this.m_calculation;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x00048BC5 File Offset: 0x00046DC5
		internal override bool PreferPlanName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00048BC8 File Offset: 0x00046DC8
		internal override void Accept(IPlanAggregateItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00048BD1 File Offset: 0x00046DD1
		protected override int GetHashCodeInternal()
		{
			return this.m_calculation.GetHashCode();
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00048BE0 File Offset: 0x00046DE0
		public override bool Equals(PlanAggregateItem other)
		{
			PlanAggregateCalculationItem planAggregateCalculationItem = other as PlanAggregateCalculationItem;
			return planAggregateCalculationItem != null && this.Calculation == planAggregateCalculationItem.Calculation && this.PreferPlanName == planAggregateCalculationItem.PreferPlanName;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00048C15 File Offset: 0x00046E15
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("AggregateCalculation");
			builder.WriteAttribute<Calculation>("Calculation", this.m_calculation, false, false);
			builder.EndObject();
		}

		// Token: 0x04000839 RID: 2105
		private readonly Calculation m_calculation;
	}
}
