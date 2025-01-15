using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Exceptions
{
	// Token: 0x020000B3 RID: 179
	public sealed class PowerBIMigrateInvalidUrlException : Exception
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x00004B5A File Offset: 0x00002D5A
		public PowerBIMigrateInvalidUrlException(string message)
			: base(message)
		{
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00004B63 File Offset: 0x00002D63
		public PowerBIMigrateInvalidUrlException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
