using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200014E RID: 334
	public class LinearGauge : Gauge
	{
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0001E1F8 File Offset: 0x0001C3F8
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x0001E207 File Offset: 0x0001C407
		[ReportExpressionDefaultValue(typeof(Orientations), Orientations.Auto)]
		public ReportExpression<Orientations> Orientation
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<Orientations>>(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001E21C File Offset: 0x0001C41C
		public LinearGauge()
		{
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001E224 File Offset: 0x0001C424
		internal LinearGauge(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0200037F RID: 895
		internal new class Definition : DefinitionStore<LinearGauge, LinearGauge.Definition.Properties>
		{
			// Token: 0x06001822 RID: 6178 RVA: 0x0003B43B File Offset: 0x0003963B
			private Definition()
			{
			}

			// Token: 0x02000498 RID: 1176
			internal enum Properties
			{
				// Token: 0x04000C12 RID: 3090
				Name,
				// Token: 0x04000C13 RID: 3091
				Style,
				// Token: 0x04000C14 RID: 3092
				Top,
				// Token: 0x04000C15 RID: 3093
				Left,
				// Token: 0x04000C16 RID: 3094
				Height,
				// Token: 0x04000C17 RID: 3095
				Width,
				// Token: 0x04000C18 RID: 3096
				ZIndex,
				// Token: 0x04000C19 RID: 3097
				Hidden,
				// Token: 0x04000C1A RID: 3098
				ToolTip,
				// Token: 0x04000C1B RID: 3099
				ActionInfo,
				// Token: 0x04000C1C RID: 3100
				ParentItem,
				// Token: 0x04000C1D RID: 3101
				GaugeScales,
				// Token: 0x04000C1E RID: 3102
				BackFrame,
				// Token: 0x04000C1F RID: 3103
				ClipContent,
				// Token: 0x04000C20 RID: 3104
				TopImage,
				// Token: 0x04000C21 RID: 3105
				AspectRatio,
				// Token: 0x04000C22 RID: 3106
				Orientation,
				// Token: 0x04000C23 RID: 3107
				PropertyCount
			}
		}
	}
}
