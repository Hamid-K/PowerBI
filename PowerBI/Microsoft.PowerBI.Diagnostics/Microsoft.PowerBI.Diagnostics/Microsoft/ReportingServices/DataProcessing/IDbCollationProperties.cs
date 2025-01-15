using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000020 RID: 32
	public interface IDbCollationProperties
	{
		// Token: 0x06000093 RID: 147
		bool GetCollationProperties(out string cultureName, out bool caseSensitive, out bool accentSensitive, out bool kanatypeSensitive, out bool widthSensitive);
	}
}
