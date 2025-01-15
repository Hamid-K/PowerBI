using System;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x0200003C RID: 60
	internal interface IQueryResultDataWriter : IExecuteSemanticQueryResultWriter
	{
		// Token: 0x060001E8 RID: 488
		void EndQueryResultData();

		// Token: 0x060001E9 RID: 489
		void DiscardQueryResultDataProgress();
	}
}
