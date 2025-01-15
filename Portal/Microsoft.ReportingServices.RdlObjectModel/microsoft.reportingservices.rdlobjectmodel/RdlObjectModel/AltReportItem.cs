using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000AD RID: 173
	public class AltReportItem : ReportObject
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0001B53D File Offset: 0x0001973D
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x0001B550 File Offset: 0x00019750
		public ReportItem ReportItem
		{
			get
			{
				return (ReportItem)base.PropertyStore.GetObject(0);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001B56D File Offset: 0x0001976D
		public AltReportItem()
		{
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001B575 File Offset: 0x00019775
		internal AltReportItem(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0200035E RID: 862
		internal class Definition : DefinitionStore<AltReportItem, AltReportItem.Definition.Properties>
		{
			// Token: 0x060017E1 RID: 6113 RVA: 0x0003AC2B File Offset: 0x00038E2B
			private Definition()
			{
			}

			// Token: 0x0200047D RID: 1149
			internal enum Properties
			{
				// Token: 0x04000ACB RID: 2763
				ReportItem
			}
		}
	}
}
