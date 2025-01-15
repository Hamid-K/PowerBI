using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x0200000C RID: 12
	public interface IDbCollationProperties
	{
		// Token: 0x0600001A RID: 26
		bool GetCollationProperties(out string cultureName, out bool caseSensitive, out bool accentSensitive, out bool kanatypeSensitive, out bool widthSensitive);
	}
}
