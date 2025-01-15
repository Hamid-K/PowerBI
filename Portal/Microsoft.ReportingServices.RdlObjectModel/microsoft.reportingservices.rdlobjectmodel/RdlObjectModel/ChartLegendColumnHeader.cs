using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200008B RID: 139
	public class ChartLegendColumnHeader : ReportObject
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x00018C45 File Offset: 0x00016E45
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x00018C53 File Offset: 0x00016E53
		[ReportExpressionDefaultValue]
		public ReportExpression Value
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

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00018C67 File Offset: 0x00016E67
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x00018C7A File Offset: 0x00016E7A
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00018C89 File Offset: 0x00016E89
		public ChartLegendColumnHeader()
		{
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00018C91 File Offset: 0x00016E91
		internal ChartLegendColumnHeader(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000341 RID: 833
		internal class Definition : DefinitionStore<ChartLegendColumnHeader, ChartLegendColumnHeader.Definition.Properties>
		{
			// Token: 0x060017C4 RID: 6084 RVA: 0x0003AB43 File Offset: 0x00038D43
			private Definition()
			{
			}

			// Token: 0x02000460 RID: 1120
			internal enum Properties
			{
				// Token: 0x0400099B RID: 2459
				Value,
				// Token: 0x0400099C RID: 2460
				Style,
				// Token: 0x0400099D RID: 2461
				PropertyCount
			}
		}
	}
}
