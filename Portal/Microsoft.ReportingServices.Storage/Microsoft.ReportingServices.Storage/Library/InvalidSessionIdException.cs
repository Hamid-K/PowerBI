using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	internal sealed class InvalidSessionIdException : ReportCatalogException
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00007FFD File Offset: 0x000061FD
		public InvalidSessionIdException(string sessionId)
			: base(ErrorCode.rsInvalidSessionId, ErrorStringsWrapper.rsInvalidSessionId(sessionId), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007D2D File Offset: 0x00005F2D
		private InvalidSessionIdException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
