using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000A2 RID: 162
	[Serializable]
	internal sealed class ReportServerHttpRuntimeInternalException : ReportCatalogException
	{
		// Token: 0x06000286 RID: 646 RVA: 0x00005228 File Offset: 0x00003428
		public ReportServerHttpRuntimeInternalException(Exception innerException, string appDomain, string additionalTraceMessage)
			: base(ErrorCode.rsHttpRuntimeInternalError, ErrorStringsWrapper.rsHttpRuntimeInternalError(appDomain), innerException, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00005242 File Offset: 0x00003442
		public ReportServerHttpRuntimeInternalException(string appDomain, string additionalTraceMessage)
			: base(ErrorCode.rsHttpRuntimeInternalError, ErrorStringsWrapper.rsHttpRuntimeInternalError(appDomain), null, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000525C File Offset: 0x0000345C
		private ReportServerHttpRuntimeInternalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
