using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B9 RID: 185
	internal sealed class UnhandledHttpApplicationException : RSException
	{
		// Token: 0x060002BC RID: 700 RVA: 0x00005843 File Offset: 0x00003A43
		public UnhandledHttpApplicationException(Exception innerException)
			: base(ErrorCode.rsUnhandledHttpApplicationError, ErrorStringsWrapper.rsUnhandledHttpApplicationError, innerException, RSTrace.IsTraceInitialized ? RSTrace.WebServerTracer : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000586B File Offset: 0x00003A6B
		private UnhandledHttpApplicationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
