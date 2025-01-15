using System;

namespace Microsoft.Owin.Security.Notifications
{
	// Token: 0x0200001A RID: 26
	public class SecurityTokenValidatedNotification<TMessage, TOptions> : BaseNotification<TOptions>
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00002784 File Offset: 0x00000984
		public SecurityTokenValidatedNotification(IOwinContext context, TOptions options)
			: base(context, options)
		{
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000278E File Offset: 0x0000098E
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002796 File Offset: 0x00000996
		public AuthenticationTicket AuthenticationTicket { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000279F File Offset: 0x0000099F
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000027A7 File Offset: 0x000009A7
		public TMessage ProtocolMessage { get; set; }
	}
}
