using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200016B RID: 363
	[ComVisible(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0000000A-0000-0000-C000-000000000046")]
	[ComImport]
	internal interface ILockBytes
	{
		// Token: 0x06000D70 RID: 3440
		void ReadAt(ulong offset, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] byte[] pv, int cb, out int pcbRead);

		// Token: 0x06000D71 RID: 3441
		void WriteAt(long ulOffset, IntPtr pv, int cb, out UIntPtr pcbWritten);

		// Token: 0x06000D72 RID: 3442
		void Flush();

		// Token: 0x06000D73 RID: 3443
		void SetSize(long cb);

		// Token: 0x06000D74 RID: 3444
		void LockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x06000D75 RID: 3445
		void UnlockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x06000D76 RID: 3446
		void Stat(out global::System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, STATFLAG grfStatFlag);
	}
}
