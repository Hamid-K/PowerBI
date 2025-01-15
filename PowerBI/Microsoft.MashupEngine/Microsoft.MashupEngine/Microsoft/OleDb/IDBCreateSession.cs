using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EF7 RID: 7927
	[Guid("0c733a5d-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDBCreateSession
	{
		// Token: 0x0600C28D RID: 49805
		[PreserveSig]
		int CreateSession(IntPtr punkOuter, ref Guid iid, out IntPtr session);
	}
}
