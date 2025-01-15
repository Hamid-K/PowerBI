using System;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x02000008 RID: 8
	[Flags]
	public enum ValueTaskSourceOnCompletedFlags
	{
		// Token: 0x04000010 RID: 16
		None = 0,
		// Token: 0x04000011 RID: 17
		UseSchedulingContext = 1,
		// Token: 0x04000012 RID: 18
		FlowExecutionContext = 2
	}
}
