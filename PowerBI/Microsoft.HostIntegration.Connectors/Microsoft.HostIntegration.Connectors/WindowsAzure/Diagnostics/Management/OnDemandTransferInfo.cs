using System;

namespace Microsoft.WindowsAzure.Diagnostics.Management
{
	// Token: 0x0200046E RID: 1134
	[Obsolete("This API is deprecated.")]
	public class OnDemandTransferInfo
	{
		// Token: 0x06002778 RID: 10104 RVA: 0x00077B94 File Offset: 0x00075D94
		public static OnDemandTransferInfo FromQueueMessage(string queueMessage)
		{
			return new OnDemandTransferInfo();
		}
	}
}
