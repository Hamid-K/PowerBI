using System;

namespace Microsoft.Owin.Security.Provider
{
	// Token: 0x02000012 RID: 18
	public abstract class EndpointContext<TOptions> : BaseContext<TOptions>
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002613 File Offset: 0x00000813
		protected EndpointContext(IOwinContext context, TOptions options)
			: base(context, options)
		{
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000261D File Offset: 0x0000081D
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002625 File Offset: 0x00000825
		public bool IsRequestCompleted { get; private set; }

		// Token: 0x06000037 RID: 55 RVA: 0x0000262E File Offset: 0x0000082E
		public void RequestCompleted()
		{
			this.IsRequestCompleted = true;
		}
	}
}
