using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200002A RID: 42
	[Serializable]
	internal sealed class ProgressiveFormatElementMissingException : ReportCatalogException
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00007F7C File Offset: 0x0000617C
		public ProgressiveFormatElementMissingException(string key)
			: base(ErrorCode.rsProgressiveFormatElementMissingError, ErrorStringsWrapper.rsProgressiveFormatElementMissingError(key), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007F96 File Offset: 0x00006196
		public ProgressiveFormatElementMissingException(string key, Exception innerException, string additionalTraceMessage)
			: base(ErrorCode.rsProgressiveFormatElementMissingError, ErrorStringsWrapper.rsProgressiveFormatElementMissingError(key), innerException, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007D2D File Offset: 0x00005F2D
		private ProgressiveFormatElementMissingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
