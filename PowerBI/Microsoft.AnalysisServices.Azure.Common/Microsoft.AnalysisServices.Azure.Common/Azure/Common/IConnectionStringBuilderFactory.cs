using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000096 RID: 150
	public interface IConnectionStringBuilderFactory
	{
		// Token: 0x06000538 RID: 1336
		IConnectionStringBuilder Create(DatabaseMoniker databaseMoniker, DatabaseResolutionInfo databaseResolutionInfo);
	}
}
