using System;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x0200003B RID: 59
	internal interface IQueryResultsWriter
	{
		// Token: 0x060001E6 RID: 486
		void WriteFailedQueryResult(ServiceError error);

		// Token: 0x060001E7 RID: 487
		IQueryResultDataWriter BeginQueryResultData();
	}
}
