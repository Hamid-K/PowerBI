using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000068 RID: 104
	[Serializable]
	internal sealed class CannotSubscribeToEventException : ReportCatalogException
	{
		// Token: 0x06000205 RID: 517 RVA: 0x000048E5 File Offset: 0x00002AE5
		public CannotSubscribeToEventException(string eventType)
			: base(ErrorCode.rsCannotSubscribeToEvent, ErrorStringsWrapper.rsCannotSubscribeToEvent(eventType), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000048FC File Offset: 0x00002AFC
		private CannotSubscribeToEventException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
