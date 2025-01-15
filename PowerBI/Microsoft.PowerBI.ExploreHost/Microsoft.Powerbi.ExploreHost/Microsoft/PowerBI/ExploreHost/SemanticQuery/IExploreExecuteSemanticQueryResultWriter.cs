using System;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.ExploreHost.ServiceContracts;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x0200003A RID: 58
	internal interface IExploreExecuteSemanticQueryResultWriter : IDisposable
	{
		// Token: 0x060001E3 RID: 483
		string GetResult();

		// Token: 0x060001E4 RID: 484
		void WriteRequestError(ServiceError error);

		// Token: 0x060001E5 RID: 485
		IQueryResultsWriter BeginResults(ExecuteSemanticQueryRequest request);
	}
}
