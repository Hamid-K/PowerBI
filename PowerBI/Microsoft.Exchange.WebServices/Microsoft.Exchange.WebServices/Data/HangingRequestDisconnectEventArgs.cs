using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200012C RID: 300
	internal class HangingRequestDisconnectEventArgs : EventArgs
	{
		// Token: 0x06000E91 RID: 3729 RVA: 0x0002C664 File Offset: 0x0002B664
		internal HangingRequestDisconnectEventArgs(HangingRequestDisconnectReason reason, Exception exception)
		{
			this.Reason = reason;
			this.Exception = exception;
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0002C67A File Offset: 0x0002B67A
		// (set) Token: 0x06000E93 RID: 3731 RVA: 0x0002C682 File Offset: 0x0002B682
		public HangingRequestDisconnectReason Reason { get; internal set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x0002C68B File Offset: 0x0002B68B
		// (set) Token: 0x06000E95 RID: 3733 RVA: 0x0002C693 File Offset: 0x0002B693
		public Exception Exception { get; internal set; }
	}
}
