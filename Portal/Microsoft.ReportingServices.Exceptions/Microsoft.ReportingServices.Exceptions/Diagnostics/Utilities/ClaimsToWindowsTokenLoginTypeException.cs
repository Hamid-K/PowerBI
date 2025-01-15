using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000BB RID: 187
	internal sealed class ClaimsToWindowsTokenLoginTypeException : RSException
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x000058A7 File Offset: 0x00003AA7
		public ClaimsToWindowsTokenLoginTypeException()
			: base(ErrorCode.rsClaimsToWindowsTokenLoginTypeError, ErrorStringsWrapper.rsClaimsToWindowsTokenLoginTypeError, null, RSTrace.IsTraceInitialized ? RSTrace.WebServerTracer : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000058CF File Offset: 0x00003ACF
		private ClaimsToWindowsTokenLoginTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
