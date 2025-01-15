using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F11 RID: 7953
	[Guid("0c733a90-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IMultipleResults
	{
		// Token: 0x0600C2CB RID: 49867
		[PreserveSig]
		unsafe int GetResult([In] IntPtr pUnkOuter, [In] IntPtr lResultFlag, [In] ref Guid riid, [Out] DBROWCOUNT* cRowsAffected, out IntPtr ppRowset);
	}
}
