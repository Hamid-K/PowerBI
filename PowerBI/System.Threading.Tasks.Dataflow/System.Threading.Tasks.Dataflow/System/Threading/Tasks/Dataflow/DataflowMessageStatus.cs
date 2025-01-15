using System;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x02000020 RID: 32
	public enum DataflowMessageStatus
	{
		// Token: 0x04000030 RID: 48
		Accepted,
		// Token: 0x04000031 RID: 49
		Declined,
		// Token: 0x04000032 RID: 50
		Postponed,
		// Token: 0x04000033 RID: 51
		NotAvailable,
		// Token: 0x04000034 RID: 52
		DecliningPermanently
	}
}
