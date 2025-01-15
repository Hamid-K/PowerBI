using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EF2 RID: 7922
	[Guid("00000001-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IClassFactory
	{
		// Token: 0x0600C27D RID: 49789
		[PreserveSig]
		int CreateInstance(IntPtr punkOuter, ref Guid iid, out IntPtr ppv);

		// Token: 0x0600C27E RID: 49790
		void LockServer(int fLock);
	}
}
