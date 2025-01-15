using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000166 RID: 358
	public class ScaleLabels : ReportObject
	{
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0001FC08 File Offset: 0x0001DE08
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x0001FC1B File Offset: 0x0001DE1B
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0001FC2A File Offset: 0x0001DE2A
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x0001FC38 File Offset: 0x0001DE38
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Interval
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0001FC4C File Offset: 0x0001DE4C
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x0001FC5A File Offset: 0x0001DE5A
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> IntervalOffset
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0001FC6E File Offset: 0x0001DE6E
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x0001FC7C File Offset: 0x0001DE7C
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> AllowUpsideDown
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0001FC90 File Offset: 0x0001DE90
		// (set) Token: 0x06000B19 RID: 2841 RVA: 0x0001FC9E File Offset: 0x0001DE9E
		[ReportExpressionDefaultValue(typeof(double), 2.0)]
		public ReportExpression<double> DistanceFromScale
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0001FCB2 File Offset: 0x0001DEB2
		// (set) Token: 0x06000B1B RID: 2843 RVA: 0x0001FCC0 File Offset: 0x0001DEC0
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> FontAngle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0001FCD4 File Offset: 0x0001DED4
		// (set) Token: 0x06000B1D RID: 2845 RVA: 0x0001FCE2 File Offset: 0x0001DEE2
		[ReportExpressionDefaultValue(typeof(Placements), Placements.Inside)]
		public ReportExpression<Placements> Placement
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<Placements>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0001FCF6 File Offset: 0x0001DEF6
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x0001FD04 File Offset: 0x0001DF04
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> RotateLabels
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0001FD18 File Offset: 0x0001DF18
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x0001FD26 File Offset: 0x0001DF26
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> ShowEndLabels
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0001FD3A File Offset: 0x0001DF3A
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x0001FD49 File Offset: 0x0001DF49
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0001FD5E File Offset: 0x0001DF5E
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0001FD6D File Offset: 0x0001DF6D
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> UseFontPercent
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0001FD82 File Offset: 0x0001DF82
		public ScaleLabels()
		{
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0001FD8A File Offset: 0x0001DF8A
		internal ScaleLabels(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0001FD93 File Offset: 0x0001DF93
		public override void Initialize()
		{
			base.Initialize();
			this.Placement = Placements.Inside;
			this.DistanceFromScale = 2.0;
		}

		// Token: 0x02000397 RID: 919
		internal class Definition : DefinitionStore<ScaleLabels, ScaleLabels.Definition.Properties>
		{
			// Token: 0x0600183A RID: 6202 RVA: 0x0003B4FB File Offset: 0x000396FB
			private Definition()
			{
			}

			// Token: 0x020004B0 RID: 1200
			internal enum Properties
			{
				// Token: 0x04000D92 RID: 3474
				Style,
				// Token: 0x04000D93 RID: 3475
				Interval,
				// Token: 0x04000D94 RID: 3476
				IntervalOffset,
				// Token: 0x04000D95 RID: 3477
				AllowUpsideDown,
				// Token: 0x04000D96 RID: 3478
				DistanceFromScale,
				// Token: 0x04000D97 RID: 3479
				FontAngle,
				// Token: 0x04000D98 RID: 3480
				Placement,
				// Token: 0x04000D99 RID: 3481
				RotateLabels,
				// Token: 0x04000D9A RID: 3482
				ShowEndLabels,
				// Token: 0x04000D9B RID: 3483
				Hidden,
				// Token: 0x04000D9C RID: 3484
				UseFontPercent,
				// Token: 0x04000D9D RID: 3485
				PropertyCount
			}
		}
	}
}
