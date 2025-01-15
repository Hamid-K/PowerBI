using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200007E RID: 126
	[Serializable]
	internal sealed class ReportServerStorageSingleRefreshConnectionExpectedException : ReportCatalogException
	{
		// Token: 0x06000239 RID: 569 RVA: 0x00004C88 File Offset: 0x00002E88
		public ReportServerStorageSingleRefreshConnectionExpectedException(long modelId, int actualCount)
			: base(ErrorCode.rsReportServerStorageSingleRefreshConnectionExpected, ErrorStringsWrapper.rsReportServerStorageSingleRefreshConnectionExpected(modelId.ToString(), actualCount.ToString()), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00004CAF File Offset: 0x00002EAF
		private ReportServerStorageSingleRefreshConnectionExpectedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
