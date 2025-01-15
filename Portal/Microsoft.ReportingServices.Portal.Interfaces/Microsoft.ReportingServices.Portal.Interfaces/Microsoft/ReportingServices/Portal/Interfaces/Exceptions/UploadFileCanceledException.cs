using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Exceptions
{
	// Token: 0x020000B6 RID: 182
	public sealed class UploadFileCanceledException : Exception
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x00004B5A File Offset: 0x00002D5A
		public UploadFileCanceledException(string message)
			: base(message)
		{
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00004B63 File Offset: 0x00002D63
		public UploadFileCanceledException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
