using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Exceptions
{
	// Token: 0x020000B8 RID: 184
	public sealed class CatalogItemInvalidException : Exception
	{
		// Token: 0x0600058B RID: 1419 RVA: 0x00004B5A File Offset: 0x00002D5A
		public CatalogItemInvalidException(string message)
			: base(message)
		{
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00004B63 File Offset: 0x00002D63
		public CatalogItemInvalidException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
