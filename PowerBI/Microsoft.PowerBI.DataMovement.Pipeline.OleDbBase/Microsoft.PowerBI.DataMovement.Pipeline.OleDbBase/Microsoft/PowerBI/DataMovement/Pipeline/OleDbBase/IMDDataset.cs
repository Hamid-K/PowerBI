using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200007C RID: 124
	[Guid("a07cccd1-8148-11d0-87bb-00c04fc33942")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IMDDataset
	{
		// Token: 0x060002C4 RID: 708
		unsafe void FreeAxisInfo(DBCOUNTITEM axesCount, MDAXISINFO* axisInfos);

		// Token: 0x060002C5 RID: 709
		unsafe void GetAxisInfo(out DBCOUNTITEM axesCount, out MDAXISINFO* axisInfos);

		// Token: 0x060002C6 RID: 710
		[PreserveSig]
		unsafe int GetAxisRowset(IntPtr unkOuter, DBCOUNTITEM axisIndex, ref Guid riid, uint propertySetsCount, DBPROPSET* propertySets, out IntPtr rowset);

		// Token: 0x060002C7 RID: 711
		[PreserveSig]
		unsafe int GetCellData(HACCESSOR accessor, DBORDINAL startCell, DBORDINAL endCell, void* data);

		// Token: 0x060002C8 RID: 712
		[PreserveSig]
		int GetSpecification(ref Guid riid, out IntPtr specification);
	}
}
