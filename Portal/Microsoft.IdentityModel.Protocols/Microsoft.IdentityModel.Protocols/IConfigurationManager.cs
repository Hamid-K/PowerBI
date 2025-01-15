using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.IdentityModel.Protocols
{
	// Token: 0x02000007 RID: 7
	public interface IConfigurationManager<T> where T : class
	{
		// Token: 0x06000032 RID: 50
		Task<T> GetConfigurationAsync(CancellationToken cancel);

		// Token: 0x06000033 RID: 51
		void RequestRefresh();
	}
}
