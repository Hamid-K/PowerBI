using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000103 RID: 259
	[OriginalName("HistorySnapshotOptionsSingle")]
	public class HistorySnapshotOptionsSingle : DataServiceQuerySingle<HistorySnapshotOptions>
	{
		// Token: 0x06000B4A RID: 2890 RVA: 0x00016399 File Offset: 0x00014599
		public HistorySnapshotOptionsSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000163A3 File Offset: 0x000145A3
		public HistorySnapshotOptionsSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000163AE File Offset: 0x000145AE
		public HistorySnapshotOptionsSingle(DataServiceQuerySingle<HistorySnapshotOptions> query)
			: base(query)
		{
		}
	}
}
