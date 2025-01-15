using System;
using System.Runtime.InteropServices;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000347 RID: 839
	internal struct OSVERSIONINFOEX
	{
		// Token: 0x040010C3 RID: 4291
		public int dwOSVersionInfoSize;

		// Token: 0x040010C4 RID: 4292
		public int dwMajorVersion;

		// Token: 0x040010C5 RID: 4293
		public int dwMinorVersion;

		// Token: 0x040010C6 RID: 4294
		public int dwBuildNumber;

		// Token: 0x040010C7 RID: 4295
		public int dwPlatformId;

		// Token: 0x040010C8 RID: 4296
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string szCSDVersion;

		// Token: 0x040010C9 RID: 4297
		public ushort wServicePackMajor;

		// Token: 0x040010CA RID: 4298
		public ushort wServicePackMinor;

		// Token: 0x040010CB RID: 4299
		public ushort wSuiteMask;

		// Token: 0x040010CC RID: 4300
		public byte wProductType;

		// Token: 0x040010CD RID: 4301
		public byte wReserved;
	}
}
