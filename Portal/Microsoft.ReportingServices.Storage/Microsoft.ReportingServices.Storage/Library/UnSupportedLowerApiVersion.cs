using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	internal sealed class UnSupportedLowerApiVersion : ReportCatalogException
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00008044 File Offset: 0x00006244
		public UnSupportedLowerApiVersion(string serverVersion, string clientVersion)
			: base(ErrorCode.rsApiVersionDiscontinued, ErrorStringsWrapper.rsApiVersionDiscontinued(serverVersion, clientVersion), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00007D2D File Offset: 0x00005F2D
		private UnSupportedLowerApiVersion(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
