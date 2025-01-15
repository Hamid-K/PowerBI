using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B5 RID: 181
	[Serializable]
	internal sealed class SoapExtensionInvalidPreambleException : RSException
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x0000574C File Offset: 0x0000394C
		public SoapExtensionInvalidPreambleException(Exception innerException, string reason, string preamble)
			: base(ErrorCode.rsSoapExtensionInvalidPreambleError, ErrorStringsWrapper.rsSoapExtensionInvalidPreambleError, innerException, RSTrace.IsTraceInitialized ? RSTrace.WebServerTracer : null, string.Format(CultureInfo.InvariantCulture, "reason={0}{1}", reason, RSTrace.WebServerTracer.TraceVerbose ? (":\n" + preamble) : "."), Array.Empty<object>())
		{
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000057AC File Offset: 0x000039AC
		private SoapExtensionInvalidPreambleException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
