using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x02000014 RID: 20
	public interface ISchemaRow
	{
		// Token: 0x06000030 RID: 48
		IColumn GetColumn(int index);

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000031 RID: 49
		int Count { get; }

		// Token: 0x06000032 RID: 50
		ISchemaRow AddColumns(IReadOnlyList<IColumn> columns);

		// Token: 0x06000033 RID: 51
		IColumn CreateColumn(string name, DataType dataType, string role, ISortInformation sortInformation);
	}
}
