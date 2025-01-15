using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200016D RID: 365
	public class FrameImage : BaseGaugeImage
	{
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x00020449 File Offset: 0x0001E649
		// (set) Token: 0x06000B8D RID: 2957 RVA: 0x00020457 File Offset: 0x0001E657
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

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x0002046B File Offset: 0x0001E66B
		// (set) Token: 0x06000B8F RID: 2959 RVA: 0x00020479 File Offset: 0x0001E679
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

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0002048D File Offset: 0x0001E68D
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x0002049B File Offset: 0x0001E69B
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> ClipImage
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000204AF File Offset: 0x0001E6AF
		public FrameImage()
		{
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x000204B7 File Offset: 0x0001E6B7
		internal FrameImage(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0200039E RID: 926
		internal new class Definition : DefinitionStore<FrameImage, FrameImage.Definition.Properties>
		{
			// Token: 0x06001841 RID: 6209 RVA: 0x0003B533 File Offset: 0x00039733
			private Definition()
			{
			}

			// Token: 0x020004B7 RID: 1207
			internal enum Properties
			{
				// Token: 0x04000DDE RID: 3550
				Source,
				// Token: 0x04000DDF RID: 3551
				Value,
				// Token: 0x04000DE0 RID: 3552
				MIMEType,
				// Token: 0x04000DE1 RID: 3553
				TransparentColor,
				// Token: 0x04000DE2 RID: 3554
				HueColor,
				// Token: 0x04000DE3 RID: 3555
				Transparency,
				// Token: 0x04000DE4 RID: 3556
				ClipImage,
				// Token: 0x04000DE5 RID: 3557
				PropertyCount
			}
		}
	}
}
