using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000094 RID: 148
	[Serializable]
	internal sealed class InvalidModelDrillthroughReportException : ReportCatalogException
	{
		// Token: 0x06000267 RID: 615 RVA: 0x00004FEF File Offset: 0x000031EF
		public InvalidModelDrillthroughReportException(string reportName)
			: base(ErrorCode.rsInvalidModelDrillthroughReport, ErrorStringsWrapper.rsInvalidModelDrillthroughReport(reportName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00005006 File Offset: 0x00003206
		private InvalidModelDrillthroughReportException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
