using System;

namespace Microsoft.Owin.Security.Notifications
{
	// Token: 0x02000019 RID: 25
	public class SecurityTokenReceivedNotification<TMessage, TOptions> : BaseNotification<TOptions>
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002769 File Offset: 0x00000969
		public SecurityTokenReceivedNotification(IOwinContext context, TOptions options)
			: base(context, options)
		{
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002773 File Offset: 0x00000973
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000277B File Offset: 0x0000097B
		public TMessage ProtocolMessage { get; set; }
	}
}
