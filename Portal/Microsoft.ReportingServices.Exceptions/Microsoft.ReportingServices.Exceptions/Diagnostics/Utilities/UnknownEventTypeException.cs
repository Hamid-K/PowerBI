using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000067 RID: 103
	[Serializable]
	internal sealed class UnknownEventTypeException : ReportCatalogException
	{
		// Token: 0x06000203 RID: 515 RVA: 0x000048C4 File Offset: 0x00002AC4
		public UnknownEventTypeException(string eventType)
			: base(ErrorCode.rsUnknownEventType, ErrorStringsWrapper.rsUnknownEventType(eventType), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000048DB File Offset: 0x00002ADB
		private UnknownEventTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
