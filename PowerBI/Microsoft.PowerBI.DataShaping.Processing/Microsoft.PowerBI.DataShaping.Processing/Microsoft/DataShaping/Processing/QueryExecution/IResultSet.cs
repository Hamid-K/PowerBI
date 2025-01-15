using System;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.QueryExecution
{
	// Token: 0x02000060 RID: 96
	internal interface IResultSet
	{
		// Token: 0x06000249 RID: 585
		IDataRow ReadRow();

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600024A RID: 586
		long RowCount { get; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600024B RID: 587
		ResultSetSchema Schema { get; }
	}
}
