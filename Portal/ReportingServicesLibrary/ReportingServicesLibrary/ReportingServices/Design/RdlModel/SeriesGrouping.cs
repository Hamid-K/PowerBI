using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003AC RID: 940
	public class SeriesGrouping
	{
		// Token: 0x06001EB4 RID: 7860 RVA: 0x000025F4 File Offset: 0x000007F4
		public SeriesGrouping()
		{
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x0007D81E File Offset: 0x0007BA1E
		public SeriesGrouping(List<StaticMember> labels)
		{
			this.DynamicSeries = null;
			this.StaticSeries = labels;
		}

		// Token: 0x04000D22 RID: 3362
		public DynamicSeries DynamicSeries;

		// Token: 0x04000D23 RID: 3363
		public List<StaticMember> StaticSeries;
	}
}
