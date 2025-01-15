using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004D0 RID: 1232
	public enum NonContractualExceptionBehavior
	{
		// Token: 0x04000D46 RID: 3398
		NoCrash,
		// Token: 0x04000D47 RID: 3399
		CrashOnNonMonitoredExceptions,
		// Token: 0x04000D48 RID: 3400
		CrashOnNonSpecifiedExceptions,
		// Token: 0x04000D49 RID: 3401
		CrashOnNonMonitoredExceptionsButAllowSpecifiedExceptions
	}
}
