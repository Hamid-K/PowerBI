using System;
using System.Threading.Tasks;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200005B RID: 91
	public interface IRateLimitController
	{
		// Token: 0x06000393 RID: 915
		Task<bool> TryRequestAccessAsync();

		// Token: 0x06000394 RID: 916
		Task<bool> TryRequestAccessAsync(TimeSpan timeout);
	}
}
