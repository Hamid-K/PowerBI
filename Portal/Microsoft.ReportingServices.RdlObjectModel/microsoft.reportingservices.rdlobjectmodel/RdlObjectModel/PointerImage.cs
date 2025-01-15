using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000160 RID: 352
	public class PointerImage : BaseGaugeImage
	{
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0001F888 File Offset: 0x0001DA88
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x0001F896 File Offset: 0x0001DA96
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

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0001F8AA File Offset: 0x0001DAAA
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x0001F8B8 File Offset: 0x0001DAB8
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

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0001F8CC File Offset: 0x0001DACC
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x0001F8DA File Offset: 0x0001DADA
		[ReportExpressionDefaultValue(typeof(ReportSize))]
		public ReportExpression<ReportSize> OffsetX
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0001F8EE File Offset: 0x0001DAEE
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x0001F8FC File Offset: 0x0001DAFC
		[ReportExpressionDefaultValue(typeof(ReportSize))]
		public ReportExpression<ReportSize> OffsetY
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001F910 File Offset: 0x0001DB10
		public PointerImage()
		{
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0001F918 File Offset: 0x0001DB18
		internal PointerImage(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000391 RID: 913
		internal new class Definition : DefinitionStore<PointerImage, PointerImage.Definition.Properties>
		{
			// Token: 0x06001834 RID: 6196 RVA: 0x0003B4CB File Offset: 0x000396CB
			private Definition()
			{
			}

			// Token: 0x020004AA RID: 1194
			internal enum Properties
			{
				// Token: 0x04000D5B RID: 3419
				Source,
				// Token: 0x04000D5C RID: 3420
				Value,
				// Token: 0x04000D5D RID: 3421
				MIMEType,
				// Token: 0x04000D5E RID: 3422
				TransparentColor,
				// Token: 0x04000D5F RID: 3423
				HueColor,
				// Token: 0x04000D60 RID: 3424
				Transparency,
				// Token: 0x04000D61 RID: 3425
				OffsetX,
				// Token: 0x04000D62 RID: 3426
				OffsetY,
				// Token: 0x04000D63 RID: 3427
				PropertyCount
			}
		}
	}
}
