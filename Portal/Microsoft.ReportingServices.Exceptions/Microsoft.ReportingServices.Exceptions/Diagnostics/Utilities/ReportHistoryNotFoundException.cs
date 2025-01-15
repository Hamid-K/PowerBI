using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200003E RID: 62
	[Serializable]
	internal sealed class ReportHistoryNotFoundException : ReportCatalogException
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x000042B6 File Offset: 0x000024B6
		public ReportHistoryNotFoundException(string reportPath, string snapshotId)
			: base(ErrorCode.rsReportHistoryNotFound, ErrorStringsWrapper.rsReportHistoryNotFound(reportPath, snapshotId), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000042CE File Offset: 0x000024CE
		private ReportHistoryNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
