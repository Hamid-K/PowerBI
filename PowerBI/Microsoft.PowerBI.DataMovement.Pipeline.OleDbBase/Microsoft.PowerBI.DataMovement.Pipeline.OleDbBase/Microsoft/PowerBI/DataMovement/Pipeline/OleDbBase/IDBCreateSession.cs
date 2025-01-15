using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200006E RID: 110
	[Guid("0c733a5d-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDBCreateSession
	{
		// Token: 0x060002A2 RID: 674
		[PreserveSig]
		int CreateSession(IntPtr punkOuter, ref Guid iid, out IntPtr session);
	}
}
