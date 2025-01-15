using System;
using System.Diagnostics;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000E7 RID: 231
	[DebuggerDisplay("[Calculation] Id={Calculation.Id} [AsMeasureJoinPredicateOnly={AsMeasureJoinPredicateOnly}]")]
	internal sealed class CalculationPlanElement : NestedPlanElement
	{
		// Token: 0x06000962 RID: 2402 RVA: 0x00023CF2 File Offset: 0x00021EF2
		internal CalculationPlanElement(Calculation calculation, bool neededForQueryCalculationContext, bool asMeasureJoinPredicateOnly)
		{
			this.m_calculation = calculation;
			this.m_neededForQueryCalculationContext = neededForQueryCalculationContext;
			this.m_asMeasureJoinPredicateOnly = asMeasureJoinPredicateOnly;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00023D0F File Offset: 0x00021F0F
		public Calculation Calculation
		{
			get
			{
				return this.m_calculation;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00023D17 File Offset: 0x00021F17
		public bool NeededForQueryCalculationContext
		{
			get
			{
				return this.m_neededForQueryCalculationContext;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00023D1F File Offset: 0x00021F1F
		public bool AsMeasureJoinPredicateOnly
		{
			get
			{
				return this.m_asMeasureJoinPredicateOnly;
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00023D27 File Offset: 0x00021F27
		public NestedPlanElement OmitMeasureJoinPredicateOnly()
		{
			if (!this.AsMeasureJoinPredicateOnly)
			{
				return this;
			}
			return new CalculationPlanElement(this.m_calculation, this.m_neededForQueryCalculationContext, false);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00023D45 File Offset: 0x00021F45
		public override NestedPlanElement OmitProjection()
		{
			if (!this.NeededForQueryCalculationContext)
			{
				return null;
			}
			if (this.AsMeasureJoinPredicateOnly)
			{
				return this;
			}
			return new CalculationPlanElement(this.m_calculation, this.m_neededForQueryCalculationContext, true);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00023D6D File Offset: 0x00021F6D
		public override void Accept(DataSetPlanElementVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00023D78 File Offset: 0x00021F78
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("CalculationPlanElement");
			builder.WriteAttribute<bool>("AsMeasureJoinPredicateOnly", this.m_asMeasureJoinPredicateOnly, false, false);
			builder.WriteAttribute<bool>("NeededForQueryCalculationContext", this.m_neededForQueryCalculationContext, false, false);
			builder.WriteProperty<Calculation>("Calculation", this.m_calculation, false);
			builder.EndObject();
		}

		// Token: 0x0400046F RID: 1135
		private readonly Calculation m_calculation;

		// Token: 0x04000470 RID: 1136
		private readonly bool m_neededForQueryCalculationContext;

		// Token: 0x04000471 RID: 1137
		private readonly bool m_asMeasureJoinPredicateOnly;
	}
}
