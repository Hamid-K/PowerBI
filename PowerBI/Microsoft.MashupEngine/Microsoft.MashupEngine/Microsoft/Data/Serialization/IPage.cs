using System;
using System.Collections.Generic;

namespace Microsoft.Data.Serialization
{
	// Token: 0x02000152 RID: 338
	public interface IPage : IDisposable
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060005EA RID: 1514
		int ColumnCount { get; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060005EB RID: 1515
		int RowCount { get; }

		// Token: 0x060005EC RID: 1516
		IColumn GetColumn(int ordinal);

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060005ED RID: 1517
		IDictionary<int, IExceptionRow> ExceptionRows { get; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060005EE RID: 1518
		ISerializedException PageException { get; }
	}
}
