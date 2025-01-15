using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	internal sealed class ReportSnapshotEnabledException : ReportCatalogException
	{
		// Token: 0x06000186 RID: 390 RVA: 0x000040A1 File Offset: 0x000022A1
		public ReportSnapshotEnabledException()
			: base(ErrorCode.rsReportSnapshotEnabled, ErrorStringsWrapper.rsReportSnapshotEnabled, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000040B7 File Offset: 0x000022B7
		private ReportSnapshotEnabledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
