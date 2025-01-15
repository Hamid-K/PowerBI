using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200006A RID: 106
	[Guid("00000001-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IClassFactory
	{
		// Token: 0x06000297 RID: 663
		[PreserveSig]
		int CreateInstance(IntPtr punkOuter, ref Guid iid, out IntPtr ppv);

		// Token: 0x06000298 RID: 664
		void LockServer(int lockFlag);
	}
}
