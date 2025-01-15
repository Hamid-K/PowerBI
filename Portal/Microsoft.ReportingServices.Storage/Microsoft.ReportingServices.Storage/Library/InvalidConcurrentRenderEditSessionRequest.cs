using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000031 RID: 49
	[Serializable]
	internal sealed class InvalidConcurrentRenderEditSessionRequest : ReportCatalogException
	{
		// Token: 0x06000134 RID: 308 RVA: 0x0000807A File Offset: 0x0000627A
		public InvalidConcurrentRenderEditSessionRequest(string sessionId)
			: base(ErrorCode.rsInvalidConcurrentRenderEditSessionRequest, ErrorStringsWrapper.rsInvalidConcurrentRenderEditSessionRequest(sessionId), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007D2D File Offset: 0x00005F2D
		private InvalidConcurrentRenderEditSessionRequest(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
