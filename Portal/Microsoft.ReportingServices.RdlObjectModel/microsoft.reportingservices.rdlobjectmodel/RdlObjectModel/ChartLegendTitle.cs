using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000089 RID: 137
	public class ChartLegendTitle : ReportObject
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00018A45 File Offset: 0x00016C45
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x00018A53 File Offset: 0x00016C53
		public ReportExpression Caption
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00018A67 File Offset: 0x00016C67
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x00018A75 File Offset: 0x00016C75
		[ReportExpressionDefaultValue(typeof(ChartTitleSeparatorTypes), ChartTitleSeparatorTypes.None)]
		public ReportExpression<ChartTitleSeparatorTypes> TitleSeparator
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartTitleSeparatorTypes>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00018A89 File Offset: 0x00016C89
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x00018A9C File Offset: 0x00016C9C
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00018AAB File Offset: 0x00016CAB
		public ChartLegendTitle()
		{
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00018AB3 File Offset: 0x00016CB3
		internal ChartLegendTitle(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0200033F RID: 831
		internal class Definition : DefinitionStore<ChartLegendTitle, ChartLegendTitle.Definition.Properties>
		{
			// Token: 0x060017C2 RID: 6082 RVA: 0x0003AB33 File Offset: 0x00038D33
			private Definition()
			{
			}

			// Token: 0x0200045E RID: 1118
			internal enum Properties
			{
				// Token: 0x04000988 RID: 2440
				Caption,
				// Token: 0x04000989 RID: 2441
				CaptionLocID,
				// Token: 0x0400098A RID: 2442
				TitleSeparator,
				// Token: 0x0400098B RID: 2443
				Style,
				// Token: 0x0400098C RID: 2444
				PropertyCount
			}
		}
	}
}
