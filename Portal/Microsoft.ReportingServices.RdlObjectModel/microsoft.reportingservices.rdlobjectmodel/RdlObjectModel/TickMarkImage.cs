using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000162 RID: 354
	public class TickMarkImage : BaseGaugeImage
	{
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0001F998 File Offset: 0x0001DB98
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x0001F9A6 File Offset: 0x0001DBA6
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

		// Token: 0x06000AEC RID: 2796 RVA: 0x0001F9BA File Offset: 0x0001DBBA
		public TickMarkImage()
		{
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0001F9C2 File Offset: 0x0001DBC2
		internal TickMarkImage(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000393 RID: 915
		internal new class Definition : DefinitionStore<TickMarkImage, TickMarkImage.Definition.Properties>
		{
			// Token: 0x06001836 RID: 6198 RVA: 0x0003B4DB File Offset: 0x000396DB
			private Definition()
			{
			}

			// Token: 0x020004AC RID: 1196
			internal enum Properties
			{
				// Token: 0x04000D6E RID: 3438
				Source,
				// Token: 0x04000D6F RID: 3439
				Value,
				// Token: 0x04000D70 RID: 3440
				MIMEType,
				// Token: 0x04000D71 RID: 3441
				TransparentColor,
				// Token: 0x04000D72 RID: 3442
				HueColor,
				// Token: 0x04000D73 RID: 3443
				PropertyCount
			}
		}
	}
}
