using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200008D RID: 141
	[Serializable]
	internal sealed class RemotePublicKeyUnavailableException : ReportCatalogException
	{
		// Token: 0x06000257 RID: 599 RVA: 0x00004EDA File Offset: 0x000030DA
		public RemotePublicKeyUnavailableException()
			: base(ErrorCode.rsRemotePublicKeyUnavailable, ErrorStringsWrapper.rsRemotePublicKeyUnavailable, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00004EF0 File Offset: 0x000030F0
		private RemotePublicKeyUnavailableException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
