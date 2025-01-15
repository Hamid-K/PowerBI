using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200045C RID: 1116
	internal enum TransportState
	{
		// Token: 0x04001721 RID: 5921
		Created,
		// Token: 0x04001722 RID: 5922
		Opening,
		// Token: 0x04001723 RID: 5923
		Opened,
		// Token: 0x04001724 RID: 5924
		Closing,
		// Token: 0x04001725 RID: 5925
		Closed,
		// Token: 0x04001726 RID: 5926
		Aborted,
		// Token: 0x04001727 RID: 5927
		Faulted
	}
}
