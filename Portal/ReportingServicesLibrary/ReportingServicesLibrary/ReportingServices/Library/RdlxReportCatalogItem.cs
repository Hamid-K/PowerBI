using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000D7 RID: 215
	[CatalogItemType(ItemType.RdlxReport)]
	internal class RdlxReportCatalogItem : FullReportCatalogItem
	{
		// Token: 0x06000983 RID: 2435 RVA: 0x00025A3A File Offset: 0x00023C3A
		internal RdlxReportCatalogItem(RSService service)
			: base(service)
		{
			this.m_sharedDataSets = new DataSetInfoCollection();
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool IsRdlx
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00025A4E File Offset: 0x00023C4E
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x00025A56 File Offset: 0x00023C56
		internal override DataSetInfoCollection SharedDataSets
		{
			get
			{
				return this.m_sharedDataSets;
			}
			set
			{
				RSTrace.CatalogTrace.Assert(value == null || value.Count == 0, "Cannot set shared dataset on RDLX report.");
			}
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal override void FlushDataCache()
		{
		}
	}
}
