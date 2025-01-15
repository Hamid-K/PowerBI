using System;
using System.Runtime.InteropServices;

namespace Microsoft.InfoNav.Common.WindowsInterop
{
	// Token: 0x0200008A RID: 138
	[ComVisible(false)]
	[Guid("00000001-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IClassFactory
	{
		// Token: 0x060004F1 RID: 1265
		int CreateInstance([MarshalAs(UnmanagedType.Interface)] object unknownOuter, ref Guid interfaceIdentifier, out IntPtr instance);

		// Token: 0x060004F2 RID: 1266
		int LockServer(bool incrementLockCount);
	}
}
