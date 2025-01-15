using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AE0 RID: 2784
	[Flags]
	public enum ControlFlag1
	{
		// Token: 0x04004557 RID: 17751
		None = 0,
		// Token: 0x04004558 RID: 17752
		ConfirmRequest = 1,
		// Token: 0x04004559 RID: 17753
		Error = 2,
		// Token: 0x0400455A RID: 17754
		RequestClose = 4,
		// Token: 0x0400455B RID: 17755
		CloseChannel = 8,
		// Token: 0x0400455C RID: 17756
		FirstSegment = 16,
		// Token: 0x0400455D RID: 17757
		LastSegment = 32,
		// Token: 0x0400455E RID: 17758
		RequestAccept = 64,
		// Token: 0x0400455F RID: 17759
		DlqUsed = 128
	}
}
