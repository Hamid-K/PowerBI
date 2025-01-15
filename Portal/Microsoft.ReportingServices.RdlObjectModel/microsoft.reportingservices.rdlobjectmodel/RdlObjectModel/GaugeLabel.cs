using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200015B RID: 347
	public class GaugeLabel : GaugePanelItem
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0001F52A File Offset: 0x0001D72A
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x0001F539 File Offset: 0x0001D739
		[ReportExpressionDefaultValue]
		public ReportExpression Text
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0001F54E File Offset: 0x0001D74E
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x0001F55D File Offset: 0x0001D75D
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Angle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0001F572 File Offset: 0x0001D772
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x0001F581 File Offset: 0x0001D781
		[ReportExpressionDefaultValue(typeof(ResizeModes), ResizeModes.AutoFit)]
		public ReportExpression<ResizeModes> ResizeMode
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ResizeModes>>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0001F596 File Offset: 0x0001D796
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x0001F5A5 File Offset: 0x0001D7A5
		[ReportExpressionDefaultValue(typeof(ReportSize))]
		public ReportExpression<ReportSize> TextShadowOffset
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0001F5BA File Offset: 0x0001D7BA
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x0001F5C9 File Offset: 0x0001D7C9
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> UseFontPercent
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0001F5DE File Offset: 0x0001D7DE
		public GaugeLabel()
		{
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0001F5E6 File Offset: 0x0001D7E6
		internal GaugeLabel(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0200038C RID: 908
		internal new class Definition : DefinitionStore<GaugeLabel, GaugeLabel.Definition.Properties>
		{
			// Token: 0x0600182F RID: 6191 RVA: 0x0003B4A3 File Offset: 0x000396A3
			private Definition()
			{
			}

			// Token: 0x020004A5 RID: 1189
			internal enum Properties
			{
				// Token: 0x04000D20 RID: 3360
				Name,
				// Token: 0x04000D21 RID: 3361
				Style,
				// Token: 0x04000D22 RID: 3362
				Top,
				// Token: 0x04000D23 RID: 3363
				Left,
				// Token: 0x04000D24 RID: 3364
				Height,
				// Token: 0x04000D25 RID: 3365
				Width,
				// Token: 0x04000D26 RID: 3366
				ZIndex,
				// Token: 0x04000D27 RID: 3367
				Hidden,
				// Token: 0x04000D28 RID: 3368
				ToolTip,
				// Token: 0x04000D29 RID: 3369
				ActionInfo,
				// Token: 0x04000D2A RID: 3370
				ParentItem,
				// Token: 0x04000D2B RID: 3371
				Text,
				// Token: 0x04000D2C RID: 3372
				Angle,
				// Token: 0x04000D2D RID: 3373
				ResizeMode,
				// Token: 0x04000D2E RID: 3374
				TextShadowOffset,
				// Token: 0x04000D2F RID: 3375
				UseFontPercent,
				// Token: 0x04000D30 RID: 3376
				PropertyCount
			}
		}
	}
}
