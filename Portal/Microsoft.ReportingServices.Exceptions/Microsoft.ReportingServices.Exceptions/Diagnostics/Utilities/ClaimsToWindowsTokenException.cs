using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000BA RID: 186
	internal sealed class ClaimsToWindowsTokenException : RSException
	{
		// Token: 0x060002BE RID: 702 RVA: 0x00005875 File Offset: 0x00003A75
		public ClaimsToWindowsTokenException(Exception innerException)
			: base(ErrorCode.rsClaimsToWindowsTokenError, ErrorStringsWrapper.rsClaimsToWindowsTokenError, innerException, RSTrace.IsTraceInitialized ? RSTrace.WebServerTracer : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000589D File Offset: 0x00003A9D
		private ClaimsToWindowsTokenException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
