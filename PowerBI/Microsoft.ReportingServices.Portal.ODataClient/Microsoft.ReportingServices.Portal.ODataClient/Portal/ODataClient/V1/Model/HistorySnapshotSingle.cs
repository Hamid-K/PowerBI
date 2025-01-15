using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000107 RID: 263
	[OriginalName("HistorySnapshotSingle")]
	public class HistorySnapshotSingle : DataServiceQuerySingle<HistorySnapshot>
	{
		// Token: 0x06000B64 RID: 2916 RVA: 0x000165A1 File Offset: 0x000147A1
		public HistorySnapshotSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x000165AB File Offset: 0x000147AB
		public HistorySnapshotSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x000165B6 File Offset: 0x000147B6
		public HistorySnapshotSingle(DataServiceQuerySingle<HistorySnapshot> query)
			: base(query)
		{
		}
	}
}
