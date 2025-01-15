using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000091 RID: 145
	[Serializable]
	internal sealed class ReportServerServiceUnavailableException : ReportCatalogException
	{
		// Token: 0x0600025F RID: 607 RVA: 0x00004F60 File Offset: 0x00003160
		public ReportServerServiceUnavailableException(string serviceName)
			: base(ErrorCode.rsReportServerServiceUnavailable, ErrorStringsWrapper.rsReportServerServiceUnavailable(serviceName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00004F77 File Offset: 0x00003177
		private ReportServerServiceUnavailableException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
