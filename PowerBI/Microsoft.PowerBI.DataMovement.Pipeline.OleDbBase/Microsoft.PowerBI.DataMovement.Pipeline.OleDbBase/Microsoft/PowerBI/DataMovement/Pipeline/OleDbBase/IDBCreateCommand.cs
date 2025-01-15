using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000076 RID: 118
	[Guid("0c733a1d-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDBCreateCommand
	{
		// Token: 0x060002B4 RID: 692
		[PreserveSig]
		int CreateCommand(IntPtr punkOuter, ref Guid iid, out IntPtr command);
	}
}
