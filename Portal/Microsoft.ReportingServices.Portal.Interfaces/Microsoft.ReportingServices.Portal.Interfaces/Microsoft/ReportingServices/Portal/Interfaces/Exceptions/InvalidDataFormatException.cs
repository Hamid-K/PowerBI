using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Exceptions
{
	// Token: 0x020000B0 RID: 176
	public sealed class InvalidDataFormatException : Exception
	{
		// Token: 0x0600057A RID: 1402 RVA: 0x00004B5A File Offset: 0x00002D5A
		public InvalidDataFormatException(string message)
			: base(message)
		{
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00004B63 File Offset: 0x00002D63
		public InvalidDataFormatException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
