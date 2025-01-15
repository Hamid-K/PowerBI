using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Notifications
{
	// Token: 0x02000018 RID: 24
	public class RedirectToIdentityProviderNotification<TMessage, TOptions> : BaseContext<TOptions>
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002729 File Offset: 0x00000929
		public RedirectToIdentityProviderNotification(IOwinContext context, TOptions options)
			: base(context, options)
		{
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002733 File Offset: 0x00000933
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000273B File Offset: 0x0000093B
		public TMessage ProtocolMessage { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002744 File Offset: 0x00000944
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000274C File Offset: 0x0000094C
		public NotificationResultState State { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002755 File Offset: 0x00000955
		public bool HandledResponse
		{
			get
			{
				return this.State == NotificationResultState.HandledResponse;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002760 File Offset: 0x00000960
		public void HandleResponse()
		{
			this.State = NotificationResultState.HandledResponse;
		}
	}
}
