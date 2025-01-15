using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000013 RID: 19
	internal interface IDbCommandProperties
	{
		// Token: 0x0600002A RID: 42
		bool SetRequestMemoryLimit(int limit);

		// Token: 0x0600002B RID: 43
		bool SetRequestIDAndCurrentActivityID();
	}
}
