using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000D7 RID: 215
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	public interface IPage : IDisposable
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003F0 RID: 1008
		int ColumnCount { get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003F1 RID: 1009
		int RowCount { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003F2 RID: 1010
		IDictionary<int, IExceptionRow> ExceptionRows { get; }

		// Token: 0x060003F3 RID: 1011
		IColumn GetColumn(int ordinal);
	}
}
