using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000A3 RID: 163
	[Serializable]
	internal sealed class ReportServerHttpRuntimeClientDisconnectionException : RSException
	{
		// Token: 0x06000289 RID: 649 RVA: 0x00005266 File Offset: 0x00003466
		public ReportServerHttpRuntimeClientDisconnectionException(Exception innerException, string appDomain, int hr)
			: base(ErrorCode.rsHttpRuntimeClientDisconnectionError, ErrorStringsWrapper.rsHttpRuntimeClientDisconnectionError(appDomain, hr.ToString("X", CultureInfo.CurrentCulture)), innerException, RSTrace.IsTraceInitialized ? RSTrace.HttpRuntimeTracer : null, null, TraceLevel.Verbose, Array.Empty<object>())
		{
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000052A1 File Offset: 0x000034A1
		private ReportServerHttpRuntimeClientDisconnectionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
