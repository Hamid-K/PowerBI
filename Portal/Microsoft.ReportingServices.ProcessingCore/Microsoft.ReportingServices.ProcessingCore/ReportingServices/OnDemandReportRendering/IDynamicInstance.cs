using System;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002D1 RID: 721
	internal interface IDynamicInstance
	{
		// Token: 0x06001B15 RID: 6933
		void ResetContext();

		// Token: 0x06001B16 RID: 6934
		bool MoveNext();

		// Token: 0x06001B17 RID: 6935
		int GetInstanceIndex();

		// Token: 0x06001B18 RID: 6936
		bool SetInstanceIndex(int index);

		// Token: 0x06001B19 RID: 6937
		ScopeID GetScopeID();

		// Token: 0x06001B1A RID: 6938
		void SetScopeID(ScopeID scopeID);
	}
}
