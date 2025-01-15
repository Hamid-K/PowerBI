using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000061 RID: 97
	public class ReceivingResponseEventArgs : EventArgs
	{
		// Token: 0x06000327 RID: 807 RVA: 0x0000CD11 File Offset: 0x0000AF11
		public ReceivingResponseEventArgs(IODataResponseMessage responseMessage, Descriptor descriptor)
			: this(responseMessage, descriptor, false)
		{
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000CD1C File Offset: 0x0000AF1C
		public ReceivingResponseEventArgs(IODataResponseMessage responseMessage, Descriptor descriptor, bool isBatchPart)
		{
			this.ResponseMessage = responseMessage;
			this.Descriptor = descriptor;
			this.IsBatchPart = isBatchPart;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000CD39 File Offset: 0x0000AF39
		// (set) Token: 0x0600032A RID: 810 RVA: 0x0000CD41 File Offset: 0x0000AF41
		public IODataResponseMessage ResponseMessage { get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000CD4A File Offset: 0x0000AF4A
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0000CD52 File Offset: 0x0000AF52
		public bool IsBatchPart { get; private set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000CD5B File Offset: 0x0000AF5B
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000CD63 File Offset: 0x0000AF63
		public Descriptor Descriptor { get; private set; }
	}
}
