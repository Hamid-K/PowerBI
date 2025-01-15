using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000032 RID: 50
	[Serializable]
	internal sealed class RequestQueueTimeoutException : RSException
	{
		// Token: 0x06000136 RID: 310 RVA: 0x00008094 File Offset: 0x00006294
		public RequestQueueTimeoutException()
			: base(ErrorCode.pvInternalError, null, null, RSTrace.RenderingTracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000080AE File Offset: 0x000062AE
		public RequestQueueTimeoutException(Exception innerException)
			: base(ErrorCode.pvInternalError, null, innerException, RSTrace.RenderingTracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000080C8 File Offset: 0x000062C8
		private RequestQueueTimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
