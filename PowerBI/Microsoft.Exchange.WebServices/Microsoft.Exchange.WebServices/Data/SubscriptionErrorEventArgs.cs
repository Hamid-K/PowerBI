using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002C1 RID: 705
	public class SubscriptionErrorEventArgs : EventArgs
	{
		// Token: 0x0600191A RID: 6426 RVA: 0x000447D7 File Offset: 0x000437D7
		internal SubscriptionErrorEventArgs(StreamingSubscription subscription, Exception exception)
		{
			this.Subscription = subscription;
			this.Exception = exception;
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x000447ED File Offset: 0x000437ED
		// (set) Token: 0x0600191C RID: 6428 RVA: 0x000447F5 File Offset: 0x000437F5
		public StreamingSubscription Subscription { get; internal set; }

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x000447FE File Offset: 0x000437FE
		// (set) Token: 0x0600191E RID: 6430 RVA: 0x00044806 File Offset: 0x00043806
		public Exception Exception { get; internal set; }
	}
}
