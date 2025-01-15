using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	internal sealed class ProgressiveMessageWriteException : ReportCatalogException
	{
		// Token: 0x06000126 RID: 294 RVA: 0x00007FB0 File Offset: 0x000061B0
		public ProgressiveMessageWriteException(string command, Exception innerException, string additionalTraceMessage)
			: base(ErrorCode.rsProgressiveMessageWriteError, ErrorStringsWrapper.rsProgressiveMessageWriteError(command), innerException, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007FCA File Offset: 0x000061CA
		public ProgressiveMessageWriteException(string key, string command, Exception innerException, string additionalTraceMessage)
			: base(ErrorCode.rsProgressiveMessageWriteError, ErrorStringsWrapper.rsProgressiveMessageWriteElementError(key, command), innerException, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007D2D File Offset: 0x00005F2D
		private ProgressiveMessageWriteException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
