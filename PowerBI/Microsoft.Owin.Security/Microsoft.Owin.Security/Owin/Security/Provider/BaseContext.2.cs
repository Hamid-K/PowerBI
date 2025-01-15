using System;

namespace Microsoft.Owin.Security.Provider
{
	// Token: 0x02000010 RID: 16
	public abstract class BaseContext<TOptions>
	{
		// Token: 0x06000029 RID: 41 RVA: 0x0000259E File Offset: 0x0000079E
		protected BaseContext(IOwinContext context, TOptions options)
		{
			this.OwinContext = context;
			this.Options = options;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000025B4 File Offset: 0x000007B4
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000025BC File Offset: 0x000007BC
		public IOwinContext OwinContext { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000025C5 File Offset: 0x000007C5
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000025CD File Offset: 0x000007CD
		public TOptions Options { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000025D6 File Offset: 0x000007D6
		public IOwinRequest Request
		{
			get
			{
				return this.OwinContext.Request;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025E3 File Offset: 0x000007E3
		public IOwinResponse Response
		{
			get
			{
				return this.OwinContext.Response;
			}
		}
	}
}
