using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B6 RID: 182
	[Serializable]
	internal sealed class UrlRemapException : RSException
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x000057B6 File Offset: 0x000039B6
		public UrlRemapException(Exception innerException, string url)
			: base(ErrorCode.rsUrlRemapError, ErrorStringsWrapper.rsUrlRemapError(url), innerException, RSTrace.IsTraceInitialized ? RSTrace.WebServerTracer : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000057DF File Offset: 0x000039DF
		private UrlRemapException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
