using System;

namespace Microsoft.Owin.Security.Notifications
{
	// Token: 0x02000014 RID: 20
	public class AuthenticationFailedNotification<TMessage, TOptions> : BaseNotification<TOptions>
	{
		// Token: 0x06000041 RID: 65 RVA: 0x0000269F File Offset: 0x0000089F
		public AuthenticationFailedNotification(IOwinContext context, TOptions options)
			: base(context, options)
		{
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000026A9 File Offset: 0x000008A9
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000026B1 File Offset: 0x000008B1
		public TMessage ProtocolMessage { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000026BA File Offset: 0x000008BA
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000026C2 File Offset: 0x000008C2
		public Exception Exception { get; set; }
	}
}
