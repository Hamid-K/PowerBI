using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Exceptions
{
	// Token: 0x020000AE RID: 174
	public sealed class ExcelWorkbookContentInvalidException : Exception
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x00004B5A File Offset: 0x00002D5A
		public ExcelWorkbookContentInvalidException(string message)
			: base(message)
		{
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00004B63 File Offset: 0x00002D63
		public ExcelWorkbookContentInvalidException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
