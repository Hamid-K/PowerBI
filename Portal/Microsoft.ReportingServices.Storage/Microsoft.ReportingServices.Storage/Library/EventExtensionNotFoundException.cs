using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200002C RID: 44
	[Serializable]
	internal sealed class EventExtensionNotFoundException : ReportCatalogException
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00007FE6 File Offset: 0x000061E6
		public EventExtensionNotFoundException(string eventType)
			: base(ErrorCode.rsEventExtensionNotFoundException, ErrorStringsWrapper.rsEventExtensionNotFoundException(eventType), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00007D2D File Offset: 0x00005F2D
		private EventExtensionNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
