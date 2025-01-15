using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000209 RID: 521
	internal sealed class PlanGroupByCalculation : PlanGroupByItem
	{
		// Token: 0x0600122C RID: 4652 RVA: 0x00048928 File Offset: 0x00046B28
		internal PlanGroupByCalculation(Calculation calculation)
		{
			this.m_calculation = calculation;
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x00048937 File Offset: 0x00046B37
		public Calculation Calculation
		{
			get
			{
				return this.m_calculation;
			}
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0004893F File Offset: 0x00046B3F
		internal override void Accept(IPlanGroupByItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00048948 File Offset: 0x00046B48
		protected override int GetHashCodeInternal()
		{
			return this.m_calculation.GetHashCode();
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00048958 File Offset: 0x00046B58
		public override bool Equals(PlanGroupByItem other)
		{
			PlanGroupByCalculation planGroupByCalculation = other as PlanGroupByCalculation;
			return planGroupByCalculation != null && this.Calculation == planGroupByCalculation.Calculation;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0004897F File Offset: 0x00046B7F
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("GroupByCalculation");
			builder.WriteProperty<Calculation>("Calculation", this.m_calculation, false);
			builder.EndObject();
		}

		// Token: 0x04000834 RID: 2100
		private readonly Calculation m_calculation;
	}
}
