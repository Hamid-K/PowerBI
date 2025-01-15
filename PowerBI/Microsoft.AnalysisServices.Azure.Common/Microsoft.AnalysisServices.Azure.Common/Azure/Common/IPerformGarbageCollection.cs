using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000088 RID: 136
	public interface IPerformGarbageCollection
	{
		// Token: 0x06000505 RID: 1285
		Task PerformGarbageCollection(CancellationToken ct);
	}
}
