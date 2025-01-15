using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000A5 RID: 165
	[Serializable]
	internal sealed class ReportBuilderFileTransmissionException : RSException
	{
		// Token: 0x0600028D RID: 653 RVA: 0x000052EA File Offset: 0x000034EA
		public ReportBuilderFileTransmissionException(Exception innerException, string fileName)
			: base(ErrorCode.rsReportBuilderFileTransmissionError, ErrorStringsWrapper.rsReportBuilderFileTransmissionError(fileName), innerException, RSTrace.IsTraceInitialized ? RSTrace.WebServerTracer : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00005313 File Offset: 0x00003513
		private ReportBuilderFileTransmissionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
