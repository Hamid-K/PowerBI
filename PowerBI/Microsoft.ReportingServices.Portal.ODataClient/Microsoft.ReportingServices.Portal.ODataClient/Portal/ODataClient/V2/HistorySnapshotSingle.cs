using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200007B RID: 123
	[OriginalName("HistorySnapshotSingle")]
	public class HistorySnapshotSingle : DataServiceQuerySingle<HistorySnapshot>
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x0000AE2D File Offset: 0x0000902D
		public HistorySnapshotSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000AE37 File Offset: 0x00009037
		public HistorySnapshotSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000AE42 File Offset: 0x00009042
		public HistorySnapshotSingle(DataServiceQuerySingle<HistorySnapshot> query)
			: base(query)
		{
		}
	}
}
