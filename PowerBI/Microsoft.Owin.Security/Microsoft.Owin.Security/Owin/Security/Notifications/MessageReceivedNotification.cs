using System;

namespace Microsoft.Owin.Security.Notifications
{
	// Token: 0x02000016 RID: 22
	public class MessageReceivedNotification<TMessage, TOptions> : BaseNotification<TOptions>
	{
		// Token: 0x0600004D RID: 77 RVA: 0x0000270E File Offset: 0x0000090E
		public MessageReceivedNotification(IOwinContext context, TOptions options)
			: base(context, options)
		{
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002718 File Offset: 0x00000918
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002720 File Offset: 0x00000920
		public TMessage ProtocolMessage { get; set; }
	}
}
