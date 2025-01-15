using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002B9 RID: 697
	public class NotificationEventArgs : EventArgs
	{
		// Token: 0x060018DA RID: 6362 RVA: 0x00043E41 File Offset: 0x00042E41
		internal NotificationEventArgs(StreamingSubscription subscription, IEnumerable<NotificationEvent> events)
		{
			this.Subscription = subscription;
			this.Events = events;
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x00043E57 File Offset: 0x00042E57
		// (set) Token: 0x060018DC RID: 6364 RVA: 0x00043E5F File Offset: 0x00042E5F
		public StreamingSubscription Subscription { get; internal set; }

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x00043E68 File Offset: 0x00042E68
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x00043E70 File Offset: 0x00042E70
		public IEnumerable<NotificationEvent> Events { get; internal set; }
	}
}
