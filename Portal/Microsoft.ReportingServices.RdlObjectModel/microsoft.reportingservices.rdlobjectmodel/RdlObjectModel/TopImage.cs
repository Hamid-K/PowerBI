using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200015E RID: 350
	public class TopImage : BaseGaugeImage
	{
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x0001F800 File Offset: 0x0001DA00
		// (set) Token: 0x06000ACF RID: 2767 RVA: 0x0001F80E File Offset: 0x0001DA0E
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

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0001F822 File Offset: 0x0001DA22
		public TopImage()
		{
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0001F82A File Offset: 0x0001DA2A
		internal TopImage(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0200038F RID: 911
		internal new class Definition : DefinitionStore<TopImage, TopImage.Definition.Properties>
		{
			// Token: 0x06001832 RID: 6194 RVA: 0x0003B4BB File Offset: 0x000396BB
			private Definition()
			{
			}

			// Token: 0x020004A8 RID: 1192
			internal enum Properties
			{
				// Token: 0x04000D4C RID: 3404
				Source,
				// Token: 0x04000D4D RID: 3405
				Value,
				// Token: 0x04000D4E RID: 3406
				MIMEType,
				// Token: 0x04000D4F RID: 3407
				TransparentColor,
				// Token: 0x04000D50 RID: 3408
				HueColor,
				// Token: 0x04000D51 RID: 3409
				PropertyCount
			}
		}
	}
}
