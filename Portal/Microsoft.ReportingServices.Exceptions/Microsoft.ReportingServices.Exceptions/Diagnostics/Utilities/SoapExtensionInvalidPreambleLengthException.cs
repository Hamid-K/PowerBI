using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B4 RID: 180
	[Serializable]
	internal sealed class SoapExtensionInvalidPreambleLengthException : RSException
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x000056E3 File Offset: 0x000038E3
		public SoapExtensionInvalidPreambleLengthException(long length)
			: base(ErrorCode.rsSoapExtensionInvalidPreambleLengthError, ErrorStringsWrapper.rsSoapExtensionInvalidPreambleLengthError(length.ToString(CultureInfo.CurrentCulture)), null, RSTrace.IsTraceInitialized ? RSTrace.WebServerTracer : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00005717 File Offset: 0x00003917
		public SoapExtensionInvalidPreambleLengthException(string length)
			: base(ErrorCode.rsSoapExtensionInvalidPreambleLengthError, ErrorStringsWrapper.rsSoapExtensionInvalidPreambleLengthError(length), null, RSTrace.IsTraceInitialized ? RSTrace.WebServerTracer : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00005740 File Offset: 0x00003940
		private SoapExtensionInvalidPreambleLengthException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
