using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000066 RID: 102
	public class SendingRequest2EventArgs : EventArgs
	{
		// Token: 0x0600037C RID: 892 RVA: 0x0000D541 File Offset: 0x0000B741
		internal SendingRequest2EventArgs(IODataRequestMessage requestMessage, Descriptor descriptor, bool isBatchPart)
		{
			this.RequestMessage = requestMessage;
			this.Descriptor = descriptor;
			this.IsBatchPart = isBatchPart;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000D55E File Offset: 0x0000B75E
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000D566 File Offset: 0x0000B766
		public IODataRequestMessage RequestMessage { get; private set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000D56F File Offset: 0x0000B76F
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000D577 File Offset: 0x0000B777
		public Descriptor Descriptor { get; private set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000D580 File Offset: 0x0000B780
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000D588 File Offset: 0x0000B788
		public bool IsBatchPart { get; private set; }
	}
}
