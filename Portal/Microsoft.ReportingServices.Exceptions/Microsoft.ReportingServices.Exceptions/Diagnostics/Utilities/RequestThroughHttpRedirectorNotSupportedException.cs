using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B7 RID: 183
	[Serializable]
	internal sealed class RequestThroughHttpRedirectorNotSupportedException : RSException
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x000057E9 File Offset: 0x000039E9
		public RequestThroughHttpRedirectorNotSupportedException(string message)
			: base(ErrorCode.rsRequestThroughHttpRedirectorNotSupportedError, ErrorStringsWrapper.rsRequestThroughHttpRedirectorNotSupportedError, null, RSTrace.IsTraceInitialized ? RSTrace.WebServerTracer : null, message, Array.Empty<object>())
		{
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00005811 File Offset: 0x00003A11
		private RequestThroughHttpRedirectorNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
