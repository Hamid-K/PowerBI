using System;

namespace Microsoft.DataShaping.Processing.QueryExecution
{
	// Token: 0x02000062 RID: 98
	internal interface IRowSource : IResultSet, IResultSetSource
	{
		// Token: 0x0600024E RID: 590
		long GetRowCount(int resultSetIndex);
	}
}
