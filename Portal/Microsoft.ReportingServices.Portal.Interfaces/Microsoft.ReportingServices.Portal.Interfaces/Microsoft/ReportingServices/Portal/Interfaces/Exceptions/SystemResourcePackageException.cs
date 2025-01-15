using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Exceptions
{
	// Token: 0x020000B7 RID: 183
	public sealed class SystemResourcePackageException : Exception
	{
		// Token: 0x06000589 RID: 1417 RVA: 0x00004B5A File Offset: 0x00002D5A
		public SystemResourcePackageException(string message)
			: base(message)
		{
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00004B63 File Offset: 0x00002D63
		public SystemResourcePackageException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
