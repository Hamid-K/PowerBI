using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Exceptions
{
	// Token: 0x020000B1 RID: 177
	public sealed class InvalidDataModelParameterException : Exception
	{
		// Token: 0x0600057C RID: 1404 RVA: 0x00004B5A File Offset: 0x00002D5A
		public InvalidDataModelParameterException(string message)
			: base(message)
		{
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00004B63 File Offset: 0x00002D63
		public InvalidDataModelParameterException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
