using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000A1 RID: 161
	internal class RSPropertyProvider
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x0001FB48 File Offset: 0x0001DD48
		internal virtual object GetProperty(string propertyName)
		{
			throw new InternalCatalogException("Not Supported in Native mode");
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001FB54 File Offset: 0x0001DD54
		internal virtual string GetSystemUrl()
		{
			return string.Empty;
		}
	}
}
