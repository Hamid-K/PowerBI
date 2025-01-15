using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000AE RID: 174
	internal class ReportSectionState
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x0001382F File Offset: 0x00011A2F
		internal ReportSectionState(List<Filter> filters, Dictionary<string, ReportItemState> reportItems)
		{
			this._filters = filters;
			this._reportItems = reportItems;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00013845 File Offset: 0x00011A45
		public List<Filter> Filters
		{
			get
			{
				return this._filters;
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00013850 File Offset: 0x00011A50
		public ReportItemState FindReportItem(string reportItemName)
		{
			ReportItemState reportItemState;
			this._reportItems.TryGetValue(reportItemName, out reportItemState);
			return reportItemState;
		}

		// Token: 0x0400023B RID: 571
		private readonly List<Filter> _filters;

		// Token: 0x0400023C RID: 572
		private readonly Dictionary<string, ReportItemState> _reportItems;
	}
}
