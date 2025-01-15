using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	internal sealed class ReportSnapshotNotEnabledException : ReportCatalogException
	{
		// Token: 0x06000188 RID: 392 RVA: 0x000040C1 File Offset: 0x000022C1
		public ReportSnapshotNotEnabledException()
			: base(ErrorCode.rsReportSnapshotNotEnabled, ErrorStringsWrapper.rsReportSnapshotNotEnabled, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000040D7 File Offset: 0x000022D7
		private ReportSnapshotNotEnabledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
