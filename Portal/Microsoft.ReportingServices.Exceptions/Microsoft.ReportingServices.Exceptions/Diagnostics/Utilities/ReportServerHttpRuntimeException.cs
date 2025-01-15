using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000A1 RID: 161
	[Serializable]
	internal sealed class ReportServerHttpRuntimeException : ReportCatalogException
	{
		// Token: 0x06000283 RID: 643 RVA: 0x000051EA File Offset: 0x000033EA
		public ReportServerHttpRuntimeException(Exception innerException, string appDomain, string additionalTraceMessage)
			: base(ErrorCode.rsHttpRuntimeError, ErrorStringsWrapper.rsHttpRuntimeError(appDomain), innerException, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00005204 File Offset: 0x00003404
		public ReportServerHttpRuntimeException(string appDomain, string additionalTraceMessage)
			: base(ErrorCode.rsHttpRuntimeError, ErrorStringsWrapper.rsHttpRuntimeError(appDomain), null, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000521E File Offset: 0x0000341E
		private ReportServerHttpRuntimeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
