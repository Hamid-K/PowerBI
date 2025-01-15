using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000082 RID: 130
	[Serializable]
	internal sealed class ReportTimeoutExpiredException : ReportCatalogException
	{
		// Token: 0x06000241 RID: 577 RVA: 0x00004D2F File Offset: 0x00002F2F
		public ReportTimeoutExpiredException(Exception innerException)
			: base(ErrorCode.rsReportTimeoutExpired, ErrorStringsWrapper.rsReportTimeoutExpired, innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00004D45 File Offset: 0x00002F45
		private ReportTimeoutExpiredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
