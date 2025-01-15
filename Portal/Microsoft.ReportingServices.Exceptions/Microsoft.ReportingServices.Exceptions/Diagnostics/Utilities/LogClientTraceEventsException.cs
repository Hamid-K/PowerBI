using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200005E RID: 94
	[Serializable]
	internal sealed class LogClientTraceEventsException : ReportCatalogException
	{
		// Token: 0x060001EC RID: 492 RVA: 0x0000476E File Offset: 0x0000296E
		public LogClientTraceEventsException(string message, ErrorCode errorCode)
			: this(message, errorCode, null)
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00004779 File Offset: 0x00002979
		public LogClientTraceEventsException(string message, ErrorCode errorCode, Exception innerException)
			: base(errorCode, message, innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000478A File Offset: 0x0000298A
		private LogClientTraceEventsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
