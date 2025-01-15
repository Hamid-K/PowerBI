using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000A6 RID: 166
	[Serializable]
	internal sealed class InternalResourceNotFoundException : RSException
	{
		// Token: 0x0600028F RID: 655 RVA: 0x0000531D File Offset: 0x0000351D
		public InternalResourceNotFoundException()
			: base(ErrorCode.rsInternalResourceNotSpecifiedError, ErrorStringsWrapper.rsInternalResourceNotSpecifiedError, null, RSTrace.IsTraceInitialized ? RSTrace.WebServerTracer : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00005345 File Offset: 0x00003545
		public InternalResourceNotFoundException(string imageId)
			: base(ErrorCode.rsInternalResourceNotFoundError, ErrorStringsWrapper.rsInternalResourceNotFoundError(imageId), null, RSTrace.IsTraceInitialized ? RSTrace.WebServerTracer : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000536E File Offset: 0x0000356E
		private InternalResourceNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
