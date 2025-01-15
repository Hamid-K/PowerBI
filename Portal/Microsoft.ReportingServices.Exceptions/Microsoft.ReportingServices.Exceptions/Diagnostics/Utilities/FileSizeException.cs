using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000030 RID: 48
	internal sealed class FileSizeException : ReportCatalogException
	{
		// Token: 0x0600018A RID: 394 RVA: 0x000040E1 File Offset: 0x000022E1
		public FileSizeException()
			: base(ErrorCode.rsFileSize, ErrorStringsWrapper.rsFileSize, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000040F7 File Offset: 0x000022F7
		private FileSizeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
