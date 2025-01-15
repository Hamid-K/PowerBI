using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000072 RID: 114
	[Guid("0c733a69-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IOpenRowset
	{
		// Token: 0x060002AD RID: 685
		unsafe void OpenRowset(IntPtr punkOuter, DBID* tableID, DBID* indexID, ref Guid iid, uint propertySetCount, DBPROPSET* propertySets, out IntPtr rowset);
	}
}
