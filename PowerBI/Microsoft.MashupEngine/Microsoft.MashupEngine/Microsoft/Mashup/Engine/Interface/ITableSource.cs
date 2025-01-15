using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000065 RID: 101
	public interface ITableSource
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001A1 RID: 417
		ValueShape ValueShape { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001A2 RID: 418
		int ColumnCount { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001A3 RID: 419
		IEnumerable<string> KeyColumnNames { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001A4 RID: 420
		IEnumerable<IRelationship> Relationships { get; }

		// Token: 0x060001A5 RID: 421
		IColumnIdentity ColumnIdentity(int index);
	}
}
