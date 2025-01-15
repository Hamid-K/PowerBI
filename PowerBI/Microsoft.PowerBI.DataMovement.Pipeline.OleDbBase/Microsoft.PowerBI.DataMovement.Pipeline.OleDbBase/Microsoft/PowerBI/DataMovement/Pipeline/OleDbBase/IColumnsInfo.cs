using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200007B RID: 123
	[Guid("0c733a11-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IColumnsInfo
	{
		// Token: 0x060002C2 RID: 706
		unsafe void GetColumnInfo(out DBORDINAL countColumnInfos, out DBCOLUMNINFO* nativeColumnInfos, out char* nativeStrings);

		// Token: 0x060002C3 RID: 707
		unsafe void MapColumnIDs(DBORDINAL columnIDCount, DBID* columnIDs, DBORDINAL* columns);
	}
}
