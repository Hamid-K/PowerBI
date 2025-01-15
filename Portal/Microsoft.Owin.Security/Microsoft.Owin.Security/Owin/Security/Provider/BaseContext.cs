using System;

namespace Microsoft.Owin.Security.Provider
{
	// Token: 0x0200000F RID: 15
	public abstract class BaseContext
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002564 File Offset: 0x00000764
		protected BaseContext(IOwinContext context)
		{
			this.OwinContext = context;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002573 File Offset: 0x00000773
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000257B File Offset: 0x0000077B
		public IOwinContext OwinContext { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002584 File Offset: 0x00000784
		public IOwinRequest Request
		{
			get
			{
				return this.OwinContext.Request;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002591 File Offset: 0x00000791
		public IOwinResponse Response
		{
			get
			{
				return this.OwinContext.Response;
			}
		}
	}
}
