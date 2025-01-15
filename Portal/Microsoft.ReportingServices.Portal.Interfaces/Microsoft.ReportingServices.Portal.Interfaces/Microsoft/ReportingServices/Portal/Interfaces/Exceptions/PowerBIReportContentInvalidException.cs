using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Exceptions
{
	// Token: 0x020000B2 RID: 178
	public sealed class PowerBIReportContentInvalidException : Exception
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x00004B5A File Offset: 0x00002D5A
		public PowerBIReportContentInvalidException(string message)
			: base(message)
		{
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00004B63 File Offset: 0x00002D63
		public PowerBIReportContentInvalidException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
