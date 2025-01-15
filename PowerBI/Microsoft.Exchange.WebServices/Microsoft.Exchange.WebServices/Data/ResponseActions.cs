using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000230 RID: 560
	[Flags]
	public enum ResponseActions
	{
		// Token: 0x04000F41 RID: 3905
		None = 0,
		// Token: 0x04000F42 RID: 3906
		Accept = 1,
		// Token: 0x04000F43 RID: 3907
		TentativelyAccept = 2,
		// Token: 0x04000F44 RID: 3908
		Decline = 4,
		// Token: 0x04000F45 RID: 3909
		Reply = 8,
		// Token: 0x04000F46 RID: 3910
		ReplyAll = 16,
		// Token: 0x04000F47 RID: 3911
		Forward = 32,
		// Token: 0x04000F48 RID: 3912
		Cancel = 64,
		// Token: 0x04000F49 RID: 3913
		RemoveFromCalendar = 128,
		// Token: 0x04000F4A RID: 3914
		SuppressReadReceipt = 256,
		// Token: 0x04000F4B RID: 3915
		PostReply = 512
	}
}
