using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000079 RID: 121
	[OriginalName("ReportHistorySnapshotSingle")]
	public class ReportHistorySnapshotSingle : DataServiceQuerySingle<ReportHistorySnapshot>
	{
		// Token: 0x06000550 RID: 1360 RVA: 0x0000AD15 File Offset: 0x00008F15
		public ReportHistorySnapshotSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000AD1F File Offset: 0x00008F1F
		public ReportHistorySnapshotSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000AD2A File Offset: 0x00008F2A
		public ReportHistorySnapshotSingle(DataServiceQuerySingle<ReportHistorySnapshot> query)
			: base(query)
		{
		}
	}
}
