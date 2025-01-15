using System;

namespace Microsoft.DataShaping.Processing.QueryExecution
{
	// Token: 0x02000061 RID: 97
	internal interface IResultSetSource : IResultSet
	{
		// Token: 0x0600024C RID: 588
		bool NextResultSet();

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600024D RID: 589
		int CurrentResultSetIndex { get; }
	}
}
