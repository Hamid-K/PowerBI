using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Exceptions
{
	// Token: 0x020000AD RID: 173
	public sealed class ExcelWorkbookWopiInvalidUrlException : Exception
	{
		// Token: 0x06000574 RID: 1396 RVA: 0x00004B5A File Offset: 0x00002D5A
		public ExcelWorkbookWopiInvalidUrlException(string message)
			: base(message)
		{
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00004B63 File Offset: 0x00002D63
		public ExcelWorkbookWopiInvalidUrlException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
