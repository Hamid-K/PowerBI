using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000026 RID: 38
	internal interface IDbCommandProperties
	{
		// Token: 0x060000A2 RID: 162
		bool SetRequestMemoryLimit(int limit);

		// Token: 0x060000A3 RID: 163
		bool SetRequestIDAndCurrentActivityID();
	}
}
