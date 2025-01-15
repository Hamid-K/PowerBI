using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Exceptions
{
	// Token: 0x020000B9 RID: 185
	public sealed class CatalogItemPropertyInvalidException : Exception
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x00004B5A File Offset: 0x00002D5A
		public CatalogItemPropertyInvalidException(string message)
			: base(message)
		{
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00004B63 File Offset: 0x00002D63
		public CatalogItemPropertyInvalidException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
