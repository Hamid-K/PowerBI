using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000031 RID: 49
	internal sealed class FileSizeNotSupportedException : ReportCatalogException
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00004101 File Offset: 0x00002301
		public FileSizeNotSupportedException()
			: base(ErrorCode.rsFileSize, ErrorStringsWrapper.rsFileSizeNotSupported, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00004117 File Offset: 0x00002317
		private FileSizeNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
