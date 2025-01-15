using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000165 RID: 357
	public class GaugeTickMarks : TickMarkStyle
	{
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0001FBAF File Offset: 0x0001DDAF
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x0001FBBE File Offset: 0x0001DDBE
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Interval
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x0001FBD3 File Offset: 0x0001DDD3
		// (set) Token: 0x06000B0D RID: 2829 RVA: 0x0001FBE2 File Offset: 0x0001DDE2
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> IntervalOffset
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0001FBF7 File Offset: 0x0001DDF7
		public GaugeTickMarks()
		{
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0001FBFF File Offset: 0x0001DDFF
		internal GaugeTickMarks(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000396 RID: 918
		internal new class Definition : DefinitionStore<GaugeTickMarks, GaugeTickMarks.Definition.Properties>
		{
			// Token: 0x06001839 RID: 6201 RVA: 0x0003B4F3 File Offset: 0x000396F3
			private Definition()
			{
			}

			// Token: 0x020004AF RID: 1199
			internal enum Properties
			{
				// Token: 0x04000D84 RID: 3460
				Style,
				// Token: 0x04000D85 RID: 3461
				DistanceFromScale,
				// Token: 0x04000D86 RID: 3462
				Placement,
				// Token: 0x04000D87 RID: 3463
				EnableGradient,
				// Token: 0x04000D88 RID: 3464
				GradientDensity,
				// Token: 0x04000D89 RID: 3465
				TickMarkImage,
				// Token: 0x04000D8A RID: 3466
				Length,
				// Token: 0x04000D8B RID: 3467
				Width,
				// Token: 0x04000D8C RID: 3468
				Shape,
				// Token: 0x04000D8D RID: 3469
				Hidden,
				// Token: 0x04000D8E RID: 3470
				Interval,
				// Token: 0x04000D8F RID: 3471
				IntervalOffset,
				// Token: 0x04000D90 RID: 3472
				PropertyCount
			}
		}
	}
}
