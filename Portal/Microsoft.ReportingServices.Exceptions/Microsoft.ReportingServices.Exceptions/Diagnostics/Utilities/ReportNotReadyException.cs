using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	internal sealed class ReportNotReadyException : ReportCatalogException
	{
		// Token: 0x06000184 RID: 388 RVA: 0x00004081 File Offset: 0x00002281
		public ReportNotReadyException()
			: base(ErrorCode.rsReportNotReady, ErrorStringsWrapper.rsReportNotReady, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00004097 File Offset: 0x00002297
		private ReportNotReadyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
