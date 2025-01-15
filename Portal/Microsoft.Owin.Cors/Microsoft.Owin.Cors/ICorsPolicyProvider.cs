using System;
using System.Threading.Tasks;
using System.Web.Cors;

namespace Microsoft.Owin.Cors
{
	// Token: 0x02000005 RID: 5
	public interface ICorsPolicyProvider
	{
		// Token: 0x06000012 RID: 18
		Task<CorsPolicy> GetCorsPolicyAsync(IOwinRequest request);
	}
}
