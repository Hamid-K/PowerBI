using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007B2 RID: 1970
	public enum UOWState : byte
	{
		// Token: 0x040029AD RID: 10669
		RESET_STATE = 1,
		// Token: 0x040029AE RID: 10670
		COMMITTED,
		// Token: 0x040029AF RID: 10671
		UNKNOWN_STATE,
		// Token: 0x040029B0 RID: 10672
		INDOUBT_STATE,
		// Token: 0x040029B1 RID: 10673
		COLD_STATE
	}
}
