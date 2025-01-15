using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200014F RID: 335
	public class RadialGauge : Gauge
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0001E22D File Offset: 0x0001C42D
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x0001E23C File Offset: 0x0001C43C
		[ReportExpressionDefaultValue(typeof(double), 50.0)]
		public ReportExpression<double> PivotX
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0001E251 File Offset: 0x0001C451
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x0001E260 File Offset: 0x0001C460
		[ReportExpressionDefaultValue(typeof(double), 50.0)]
		public ReportExpression<double> PivotY
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(17);
			}
			set
			{
				base.PropertyStore.SetObject(17, value);
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0001E275 File Offset: 0x0001C475
		public RadialGauge()
		{
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0001E27D File Offset: 0x0001C47D
		internal RadialGauge(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001E286 File Offset: 0x0001C486
		public override void Initialize()
		{
			base.Initialize();
			this.PivotX = 50.0;
			this.PivotY = 50.0;
		}

		// Token: 0x02000380 RID: 896
		internal new class Definition : DefinitionStore<RadialGauge, RadialGauge.Definition.Properties>
		{
			// Token: 0x06001823 RID: 6179 RVA: 0x0003B443 File Offset: 0x00039643
			private Definition()
			{
			}

			// Token: 0x02000499 RID: 1177
			internal enum Properties
			{
				// Token: 0x04000C25 RID: 3109
				Name,
				// Token: 0x04000C26 RID: 3110
				Style,
				// Token: 0x04000C27 RID: 3111
				Top,
				// Token: 0x04000C28 RID: 3112
				Left,
				// Token: 0x04000C29 RID: 3113
				Height,
				// Token: 0x04000C2A RID: 3114
				Width,
				// Token: 0x04000C2B RID: 3115
				ZIndex,
				// Token: 0x04000C2C RID: 3116
				Hidden,
				// Token: 0x04000C2D RID: 3117
				ToolTip,
				// Token: 0x04000C2E RID: 3118
				ActionInfo,
				// Token: 0x04000C2F RID: 3119
				ParentItem,
				// Token: 0x04000C30 RID: 3120
				GaugeScales,
				// Token: 0x04000C31 RID: 3121
				BackFrame,
				// Token: 0x04000C32 RID: 3122
				ClipContent,
				// Token: 0x04000C33 RID: 3123
				TopImage,
				// Token: 0x04000C34 RID: 3124
				AspectRatio,
				// Token: 0x04000C35 RID: 3125
				PivotX,
				// Token: 0x04000C36 RID: 3126
				PivotY,
				// Token: 0x04000C37 RID: 3127
				PropertyCount
			}
		}
	}
}
