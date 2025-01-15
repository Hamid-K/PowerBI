using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x0200000E RID: 14
	public interface IDataRow
	{
		// Token: 0x06000024 RID: 36
		object GetObject(int index);

		// Token: 0x06000025 RID: 37
		double? GetAsDouble(int index);

		// Token: 0x06000026 RID: 38
		long? GetAsInt64(int index);

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000027 RID: 39
		int Count { get; }

		// Token: 0x06000028 RID: 40
		IDataRow AddColumns(IReadOnlyList<object> columns);
	}
}
