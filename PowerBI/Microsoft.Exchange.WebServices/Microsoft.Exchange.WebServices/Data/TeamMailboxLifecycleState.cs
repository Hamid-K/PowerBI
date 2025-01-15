using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200024B RID: 587
	public enum TeamMailboxLifecycleState
	{
		// Token: 0x040011EF RID: 4591
		[EwsEnum("Active")]
		Active,
		// Token: 0x040011F0 RID: 4592
		[EwsEnum("Closed")]
		Closed,
		// Token: 0x040011F1 RID: 4593
		[EwsEnum("Unlinked")]
		Unlinked,
		// Token: 0x040011F2 RID: 4594
		[EwsEnum("PendingDelete")]
		PendingDelete
	}
}
