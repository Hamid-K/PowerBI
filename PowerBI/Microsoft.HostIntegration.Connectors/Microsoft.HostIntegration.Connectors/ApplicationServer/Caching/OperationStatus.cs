using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002B4 RID: 692
	internal enum OperationStatus
	{
		// Token: 0x04000DAD RID: 3501
		Success,
		// Token: 0x04000DAE RID: 3502
		SendFailed,
		// Token: 0x04000DAF RID: 3503
		ChannelOpening,
		// Token: 0x04000DB0 RID: 3504
		ChannelOpenFailed,
		// Token: 0x04000DB1 RID: 3505
		ChannelCreationFailed,
		// Token: 0x04000DB2 RID: 3506
		InstanceClosed,
		// Token: 0x04000DB3 RID: 3507
		VerificationFailed,
		// Token: 0x04000DB4 RID: 3508
		AsyncFailureReceived,
		// Token: 0x04000DB5 RID: 3509
		MessageLargerThanConfiguredSize,
		// Token: 0x04000DB6 RID: 3510
		AuthorizationFailed,
		// Token: 0x04000DB7 RID: 3511
		ChannelAuthFailure,
		// Token: 0x04000DB8 RID: 3512
		ChannelAuthOfflineRevocationFailure
	}
}
