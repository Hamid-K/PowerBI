using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000086 RID: 134
	[Serializable]
	internal sealed class ServerBusyException : ReportCatalogException
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00004DB3 File Offset: 0x00002FB3
		public ServerBusyException()
			: base(ErrorCode.rsServerBusy, ErrorStringsWrapper.rsServerBusy, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00004DCC File Offset: 0x00002FCC
		private ServerBusyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
