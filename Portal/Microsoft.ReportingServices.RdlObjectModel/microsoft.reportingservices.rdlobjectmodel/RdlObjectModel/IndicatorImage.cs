using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200015F RID: 351
	public class IndicatorImage : BaseGaugeImage
	{
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0001F833 File Offset: 0x0001DA33
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x0001F841 File Offset: 0x0001DA41
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> HueColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0001F855 File Offset: 0x0001DA55
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x0001F863 File Offset: 0x0001DA63
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Transparency
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

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0001F877 File Offset: 0x0001DA77
		public IndicatorImage()
		{
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0001F87F File Offset: 0x0001DA7F
		internal IndicatorImage(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000390 RID: 912
		internal new class Definition : DefinitionStore<IndicatorImage, IndicatorImage.Definition.Properties>
		{
			// Token: 0x06001833 RID: 6195 RVA: 0x0003B4C3 File Offset: 0x000396C3
			private Definition()
			{
			}

			// Token: 0x020004A9 RID: 1193
			internal enum Properties
			{
				// Token: 0x04000D53 RID: 3411
				Source,
				// Token: 0x04000D54 RID: 3412
				Value,
				// Token: 0x04000D55 RID: 3413
				MIMEType,
				// Token: 0x04000D56 RID: 3414
				TransparentColor,
				// Token: 0x04000D57 RID: 3415
				HueColor,
				// Token: 0x04000D58 RID: 3416
				Transparency,
				// Token: 0x04000D59 RID: 3417
				PropertyCount
			}
		}
	}
}
