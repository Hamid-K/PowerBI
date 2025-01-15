using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F02 RID: 7938
	[Guid("0c733a11-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IColumnsInfo
	{
		// Token: 0x0600C2A6 RID: 49830
		unsafe void GetColumnInfo(out DBORDINAL countColumnInfos, out DBCOLUMNINFO* nativeColumnInfos, out char* nativeStrings);

		// Token: 0x0600C2A7 RID: 49831
		unsafe void MapColumnIDs(DBORDINAL cColumnIDs, DBID* rgColumnIDs, DBORDINAL* rgColumns);
	}
}
