using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F01 RID: 7937
	[Guid("0c733a7c-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IRowset
	{
		// Token: 0x0600C2A1 RID: 49825
		unsafe void AddRefRows(DBCOUNTITEM cRows, HROW* nativeRows, uint* nativeRefCounts, DBROWSTATUS* nativeRowStatus);

		// Token: 0x0600C2A2 RID: 49826
		unsafe void GetData(HROW hRow, HACCESSOR hAccessor, byte* pData);

		// Token: 0x0600C2A3 RID: 49827
		[PreserveSig]
		unsafe int GetNextRows(HCHAPTER hReserved, DBROWOFFSET lRowsOffset, DBROWCOUNT cRows, out DBCOUNTITEM countRowsObtained, HROW** pRows);

		// Token: 0x0600C2A4 RID: 49828
		unsafe void ReleaseRows(DBCOUNTITEM cRows, HROW* nativeRows, void* nativeRowOptions, uint* nativeRefCounts, DBROWSTATUS* nativeRowStatus);

		// Token: 0x0600C2A5 RID: 49829
		void RestartPosition(HCHAPTER hReserved);
	}
}
