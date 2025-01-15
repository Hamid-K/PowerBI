using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x0200001C RID: 28
	public abstract class AuthenticationHandler<TOptions> : AuthenticationHandler where TOptions : AuthenticationOptions
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002D08 File Offset: 0x00000F08
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002D10 File Offset: 0x00000F10
		private protected TOptions Options { protected get; private set; }

		// Token: 0x0600007E RID: 126 RVA: 0x00002D19 File Offset: 0x00000F19
		internal Task Initialize(TOptions options, IOwinContext context)
		{
			this.Options = options;
			return base.BaseInitializeAsync(options, context);
		}
	}
}
