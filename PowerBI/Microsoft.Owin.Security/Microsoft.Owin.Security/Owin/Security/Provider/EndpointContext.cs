using System;

namespace Microsoft.Owin.Security.Provider
{
	// Token: 0x02000011 RID: 17
	public abstract class EndpointContext : BaseContext
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000025F0 File Offset: 0x000007F0
		protected EndpointContext(IOwinContext context)
			: base(context)
		{
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000025F9 File Offset: 0x000007F9
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002601 File Offset: 0x00000801
		public bool IsRequestCompleted { get; private set; }

		// Token: 0x06000033 RID: 51 RVA: 0x0000260A File Offset: 0x0000080A
		public void RequestCompleted()
		{
			this.IsRequestCompleted = true;
		}
	}
}
