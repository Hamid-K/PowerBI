using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000277 RID: 631
	public enum ItemIndexError
	{
		// Token: 0x040012DB RID: 4827
		None,
		// Token: 0x040012DC RID: 4828
		GenericError,
		// Token: 0x040012DD RID: 4829
		Timeout,
		// Token: 0x040012DE RID: 4830
		StaleEvent,
		// Token: 0x040012DF RID: 4831
		MailboxOffline,
		// Token: 0x040012E0 RID: 4832
		AttachmentLimitReached,
		// Token: 0x040012E1 RID: 4833
		MarsWriterTruncation
	}
}
