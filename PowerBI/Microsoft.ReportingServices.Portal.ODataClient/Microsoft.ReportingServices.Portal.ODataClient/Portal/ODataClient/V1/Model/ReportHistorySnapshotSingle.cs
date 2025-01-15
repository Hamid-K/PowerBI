using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000105 RID: 261
	[OriginalName("ReportHistorySnapshotSingle")]
	public class ReportHistorySnapshotSingle : DataServiceQuerySingle<ReportHistorySnapshot>
	{
		// Token: 0x06000B56 RID: 2902 RVA: 0x00016489 File Offset: 0x00014689
		public ReportHistorySnapshotSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00016493 File Offset: 0x00014693
		public ReportHistorySnapshotSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0001649E File Offset: 0x0001469E
		public ReportHistorySnapshotSingle(DataServiceQuerySingle<ReportHistorySnapshot> query)
			: base(query)
		{
		}
	}
}
