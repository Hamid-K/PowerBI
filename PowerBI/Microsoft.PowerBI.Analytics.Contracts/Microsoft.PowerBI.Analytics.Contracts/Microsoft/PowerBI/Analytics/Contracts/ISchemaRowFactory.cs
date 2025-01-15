using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x02000015 RID: 21
	public interface ISchemaRowFactory
	{
		// Token: 0x06000034 RID: 52
		ISchemaRow CreateSchemaRow(IReadOnlyList<IColumn> columns);
	}
}
