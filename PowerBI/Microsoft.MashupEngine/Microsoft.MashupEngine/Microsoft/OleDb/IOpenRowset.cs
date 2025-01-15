using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EFA RID: 7930
	[Guid("0c733a69-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IOpenRowset
	{
		// Token: 0x0600C292 RID: 49810
		[PreserveSig]
		unsafe int OpenRowset(IntPtr pUnkOuter, DBID* pTableID, DBID* pIndexID, ref Guid iid, uint cPropertySets, DBPROPSET* rgPropertySets, out IntPtr ppRowset);
	}
}
