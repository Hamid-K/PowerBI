using System;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;

namespace Microsoft.ReportingServices.Portal.Interfaces.Exceptions
{
	// Token: 0x020000B4 RID: 180
	public sealed class PowerBIReportNotSupportedException : Exception
	{
		// Token: 0x06000582 RID: 1410 RVA: 0x00004B6D File Offset: 0x00002D6D
		public PowerBIReportNotSupportedException(string message, ErrorCode errorCode)
			: base(message)
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00004B7D File Offset: 0x00002D7D
		public PowerBIReportNotSupportedException(string message, ErrorCode errorCode, Exception innerException)
			: base(message, innerException)
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00004B63 File Offset: 0x00002D63
		public PowerBIReportNotSupportedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x040002E4 RID: 740
		public ErrorCode ErrorCode;
	}
}
