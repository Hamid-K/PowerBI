using System;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.Azure.Common;
using Microsoft.Cloud.Platform.Azure.WindowsFabric;

namespace Microsoft.AnalysisServices.Azure.DatabaseResolver
{
	// Token: 0x0200002A RID: 42
	public interface IDatabaseResolver
	{
		// Token: 0x060002BB RID: 699
		Task<ExtendedAnalyticsServiceResolveResult> ResolvePowerBIDatabaseAsync(DatabaseMoniker dbMoniker, WindowsFabricEndpoint previousEndpoint);

		// Token: 0x060002BC RID: 700
		Task<string> GetAuthorityIdAsync(DatabaseMoniker dbMoniker);
	}
}
