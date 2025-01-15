using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000016 RID: 22
	[OriginalName("HistorySnapshotOptionsSingle")]
	public class HistorySnapshotOptionsSingle : DataServiceQuerySingle<HistorySnapshotOptions>
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00003265 File Offset: 0x00001465
		public HistorySnapshotOptionsSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000326F File Offset: 0x0000146F
		public HistorySnapshotOptionsSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000327A File Offset: 0x0000147A
		public HistorySnapshotOptionsSingle(DataServiceQuerySingle<HistorySnapshotOptions> query)
			: base(query)
		{
		}
	}
}
