using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000035 RID: 53
	[Serializable]
	internal sealed class InvalidReportLinkException : ReportCatalogException
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00004185 File Offset: 0x00002385
		public InvalidReportLinkException()
			: base(ErrorCode.rsInvalidReportLink, ErrorStringsWrapper.rsInvalidReportLink, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000419B File Offset: 0x0000239B
		private InvalidReportLinkException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
