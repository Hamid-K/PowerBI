using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000043 RID: 67
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	public interface ICells : IDisposable
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600025C RID: 604
		IColumnsInfo ColumnsInfo { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600025D RID: 605
		IAccessor Accessor { get; }

		// Token: 0x0600025E RID: 606
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		unsafe int GetCells(HACCESSOR accessor, DBORDINAL startCell, DBORDINAL endCell, void* destBuffer);
	}
}
