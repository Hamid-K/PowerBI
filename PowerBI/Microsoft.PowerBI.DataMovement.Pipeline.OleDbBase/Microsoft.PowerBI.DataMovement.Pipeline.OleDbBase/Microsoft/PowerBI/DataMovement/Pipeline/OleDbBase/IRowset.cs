using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000079 RID: 121
	[Guid("0c733a7c-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IRowset
	{
		// Token: 0x060002BC RID: 700
		unsafe void AddRefRows(DBCOUNTITEM rowCount, HROW* nativeRows, uint* nativeRefCounts, DBROWSTATUS* nativeRowStatus);

		// Token: 0x060002BD RID: 701
		unsafe void GetData(HROW row, HACCESSOR accessor, byte* data);

		// Token: 0x060002BE RID: 702
		[PreserveSig]
		unsafe int GetNextRows(HCHAPTER reserved, DBROWOFFSET rowsOffset, DBROWCOUNT rowCount, out DBCOUNTITEM countRowsObtained, HROW** rows);

		// Token: 0x060002BF RID: 703
		unsafe void ReleaseRows(DBCOUNTITEM rowCount, HROW* nativeRows, void* nativeRowOptions, uint* nativeRefCounts, DBROWSTATUS* nativeRowStatus);

		// Token: 0x060002C0 RID: 704
		void RestartPosition(HCHAPTER reserved);
	}
}
