using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EFE RID: 7934
	[Guid("0c733a1d-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDBCreateCommand
	{
		// Token: 0x0600C299 RID: 49817
		[PreserveSig]
		int CreateCommand(IntPtr punkOuter, ref Guid iid, out IntPtr command);
	}
}
