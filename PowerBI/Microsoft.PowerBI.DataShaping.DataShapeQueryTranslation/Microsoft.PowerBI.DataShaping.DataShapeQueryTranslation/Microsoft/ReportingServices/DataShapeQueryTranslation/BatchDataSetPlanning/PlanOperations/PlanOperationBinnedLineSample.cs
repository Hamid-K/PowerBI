using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001F2 RID: 498
	internal sealed class PlanOperationBinnedLineSample : PlanOperation
	{
		// Token: 0x06001139 RID: 4409 RVA: 0x00046C64 File Offset: 0x00044E64
		internal PlanOperationBinnedLineSample(PlanOperation input, PlanBinnedLineSampleItem axis, IReadOnlyList<PlanBinnedLineSampleItem> measures, IReadOnlyList<PlanBinnedLineSampleMember> series, PlanExpression targetPointCount, PlanExpression minPointsPerSeries, PlanExpression maxPointsPerSeries, PlanExpression maxDynamicSeriesCount)
		{
			this.m_input = input;
			this.m_axis = axis;
			this.m_measures = measures;
			this.m_series = series;
			this.m_targetPointCount = targetPointCount;
			this.m_minPointsPerSeries = minPointsPerSeries;
			this.m_maxPointsPerSeries = maxPointsPerSeries;
			this.m_maxDynamicSeriesCount = maxDynamicSeriesCount;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00046CB4 File Offset: 0x00044EB4
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x00046CBD File Offset: 0x00044EBD
		public PlanOperation Input
		{
			get
			{
				return this.m_input;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x00046CC5 File Offset: 0x00044EC5
		internal PlanBinnedLineSampleItem Axis
		{
			get
			{
				return this.m_axis;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x00046CCD File Offset: 0x00044ECD
		internal IReadOnlyList<PlanBinnedLineSampleItem> Measures
		{
			get
			{
				return this.m_measures;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x00046CD5 File Offset: 0x00044ED5
		internal IReadOnlyList<PlanBinnedLineSampleMember> Series
		{
			get
			{
				return this.m_series;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x00046CDD File Offset: 0x00044EDD
		public PlanExpression TargetPointCount
		{
			get
			{
				return this.m_targetPointCount;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x00046CE5 File Offset: 0x00044EE5
		internal PlanExpression MinPointsPerSeries
		{
			get
			{
				return this.m_minPointsPerSeries;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x00046CED File Offset: 0x00044EED
		internal PlanExpression MaxPointsPerSeries
		{
			get
			{
				return this.m_maxPointsPerSeries;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x00046CF5 File Offset: 0x00044EF5
		internal PlanExpression MaxDynamicSeriesCount
		{
			get
			{
				return this.m_maxDynamicSeriesCount;
			}
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00046D00 File Offset: 0x00044F00
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationBinnedLineSample planOperationBinnedLineSample;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationBinnedLineSample>(this, other, out flag, out planOperationBinnedLineSample))
			{
				return flag;
			}
			return this.Input.Equals(planOperationBinnedLineSample.Input) && this.Axis.Equals(planOperationBinnedLineSample.Axis) && this.Measures.SequenceEqual(planOperationBinnedLineSample.Measures) && this.Series.SequenceEqualReadOnly(planOperationBinnedLineSample.Series) && this.TargetPointCount.Equals(planOperationBinnedLineSample.TargetPointCount) && this.MinPointsPerSeries.Equals(planOperationBinnedLineSample.MinPointsPerSeries) && this.MaxPointsPerSeries.Equals(planOperationBinnedLineSample.MaxPointsPerSeries) && this.MaxDynamicSeriesCount.Equals(planOperationBinnedLineSample.MaxDynamicSeriesCount);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00046DB8 File Offset: 0x00044FB8
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("BinnedLineSample");
			builder.WriteProperty<PlanOperation>("Input", this.Input, false);
			builder.WriteProperty<PlanBinnedLineSampleItem>("Axis", this.Axis, false);
			builder.WriteProperty<IReadOnlyList<PlanBinnedLineSampleItem>>("Measures", this.Measures, false);
			builder.WriteProperty<IReadOnlyList<PlanBinnedLineSampleMember>>("Series", this.Series, false);
			builder.WriteProperty<PlanExpression>("TargetPointCount", this.TargetPointCount, false);
			builder.WriteProperty<PlanExpression>("MinPointsPerSeries", this.MinPointsPerSeries, false);
			builder.WriteProperty<PlanExpression>("MaxPointsPerSeries", this.MaxPointsPerSeries, false);
			builder.WriteProperty<PlanExpression>("MaxDynamicSeriesCount", this.MaxDynamicSeriesCount, false);
			builder.EndObject();
		}

		// Token: 0x040007FB RID: 2043
		private readonly PlanOperation m_input;

		// Token: 0x040007FC RID: 2044
		private readonly PlanBinnedLineSampleItem m_axis;

		// Token: 0x040007FD RID: 2045
		private readonly IReadOnlyList<PlanBinnedLineSampleItem> m_measures;

		// Token: 0x040007FE RID: 2046
		private readonly IReadOnlyList<PlanBinnedLineSampleMember> m_series;

		// Token: 0x040007FF RID: 2047
		private readonly PlanExpression m_targetPointCount;

		// Token: 0x04000800 RID: 2048
		private readonly PlanExpression m_minPointsPerSeries;

		// Token: 0x04000801 RID: 2049
		private readonly PlanExpression m_maxPointsPerSeries;

		// Token: 0x04000802 RID: 2050
		private readonly PlanExpression m_maxDynamicSeriesCount;
	}
}
