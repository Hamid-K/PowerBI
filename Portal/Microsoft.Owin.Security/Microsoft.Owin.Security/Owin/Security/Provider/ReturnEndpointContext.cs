using System;
using System.Security.Claims;

namespace Microsoft.Owin.Security.Provider
{
	// Token: 0x02000013 RID: 19
	public abstract class ReturnEndpointContext : EndpointContext
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002637 File Offset: 0x00000837
		protected ReturnEndpointContext(IOwinContext context, AuthenticationTicket ticket)
			: base(context)
		{
			if (ticket != null)
			{
				this.Identity = ticket.Identity;
				this.Properties = ticket.Properties;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000265B File Offset: 0x0000085B
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002663 File Offset: 0x00000863
		public ClaimsIdentity Identity { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000266C File Offset: 0x0000086C
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002674 File Offset: 0x00000874
		public AuthenticationProperties Properties { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000267D File Offset: 0x0000087D
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002685 File Offset: 0x00000885
		public string SignInAsAuthenticationType { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000268E File Offset: 0x0000088E
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002696 File Offset: 0x00000896
		public string RedirectUri { get; set; }
	}
}
