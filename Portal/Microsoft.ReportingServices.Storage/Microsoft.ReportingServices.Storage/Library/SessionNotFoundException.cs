using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	internal sealed class SessionNotFoundException : ReportCatalogException
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00008017 File Offset: 0x00006217
		public SessionNotFoundException(string sessionId, string userName)
			: this(sessionId, userName, null)
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00008022 File Offset: 0x00006222
		public SessionNotFoundException(string sessionId, string userName, Exception innerException)
			: base(ErrorCode.rsSessionNotFound, ErrorStringsWrapper.rsSessionNotFound(sessionId, userName), innerException, null, Array.Empty<object>())
		{
			this.SessionId = sessionId;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007D2D File Offset: 0x00005F2D
		private SessionNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000156 RID: 342
		public readonly string SessionId;
	}
}
