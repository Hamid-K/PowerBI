using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Exceptions
{
	// Token: 0x020000BA RID: 186
	public sealed class DatabaseUnavailableException : Exception
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x00004B5A File Offset: 0x00002D5A
		public DatabaseUnavailableException(string message)
			: base(message)
		{
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00004B63 File Offset: 0x00002D63
		public DatabaseUnavailableException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
