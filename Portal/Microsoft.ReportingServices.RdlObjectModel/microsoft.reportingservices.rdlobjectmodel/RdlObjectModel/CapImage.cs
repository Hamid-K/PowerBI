using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000161 RID: 353
	public class CapImage : BaseGaugeImage
	{
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0001F921 File Offset: 0x0001DB21
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x0001F92F File Offset: 0x0001DB2F
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

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0001F943 File Offset: 0x0001DB43
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0001F951 File Offset: 0x0001DB51
		[ReportExpressionDefaultValue(typeof(ReportSize))]
		public ReportExpression<ReportSize> OffsetX
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0001F965 File Offset: 0x0001DB65
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x0001F973 File Offset: 0x0001DB73
		[ReportExpressionDefaultValue(typeof(ReportSize))]
		public ReportExpression<ReportSize> OffsetY
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

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001F987 File Offset: 0x0001DB87
		public CapImage()
		{
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001F98F File Offset: 0x0001DB8F
		internal CapImage(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000392 RID: 914
		internal new class Definition : DefinitionStore<CapImage, CapImage.Definition.Properties>
		{
			// Token: 0x06001835 RID: 6197 RVA: 0x0003B4D3 File Offset: 0x000396D3
			private Definition()
			{
			}

			// Token: 0x020004AB RID: 1195
			internal enum Properties
			{
				// Token: 0x04000D65 RID: 3429
				Source,
				// Token: 0x04000D66 RID: 3430
				Value,
				// Token: 0x04000D67 RID: 3431
				MIMEType,
				// Token: 0x04000D68 RID: 3432
				TransparentColor,
				// Token: 0x04000D69 RID: 3433
				HueColor,
				// Token: 0x04000D6A RID: 3434
				OffsetX,
				// Token: 0x04000D6B RID: 3435
				OffsetY,
				// Token: 0x04000D6C RID: 3436
				PropertyCount
			}
		}
	}
}
