using System;
using System.Threading.Tasks;
using System.Web.Cors;

namespace Microsoft.Owin.Cors
{
	// Token: 0x02000004 RID: 4
	public class CorsPolicyProvider : ICorsPolicyProvider
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002365 File Offset: 0x00000565
		public CorsPolicyProvider()
		{
			this.PolicyResolver = (IOwinRequest request) => Task.FromResult<CorsPolicy>(null);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002392 File Offset: 0x00000592
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000239A File Offset: 0x0000059A
		public Func<IOwinRequest, Task<CorsPolicy>> PolicyResolver { get; set; }

		// Token: 0x06000011 RID: 17 RVA: 0x000023A3 File Offset: 0x000005A3
		public virtual Task<CorsPolicy> GetCorsPolicyAsync(IOwinRequest request)
		{
			return this.PolicyResolver(request);
		}
	}
}
