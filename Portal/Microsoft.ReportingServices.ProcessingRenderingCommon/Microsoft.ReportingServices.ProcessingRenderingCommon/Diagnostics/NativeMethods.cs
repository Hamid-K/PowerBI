using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000045 RID: 69
	internal sealed class NativeMethods
	{
		// Token: 0x06000202 RID: 514
		[DllImport("crypt32", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool CryptProtectData(SafeCryptoBlobIn dataIn, string szDataDescr, IntPtr optionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, int dwFlags, SafeCryptoBlobOut pDataOut);

		// Token: 0x06000203 RID: 515
		[DllImport("crypt32", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool CryptUnprotectData(SafeCryptoBlobIn dataIn, StringBuilder ppszDataDescr, IntPtr optionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, int dwFlags, SafeCryptoBlobOut pDataOut);

		// Token: 0x06000204 RID: 516
		[DllImport("kernel32")]
		internal static extern IntPtr LocalFree(IntPtr hMem);

		// Token: 0x040000FC RID: 252
		public const int CurrentUser = 0;

		// Token: 0x040000FD RID: 253
		public const int UIForbidden = 1;

		// Token: 0x040000FE RID: 254
		public const int LocalMachine = 4;

		// Token: 0x020000E4 RID: 228
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DATA_BLOB
		{
			// Token: 0x0400049F RID: 1183
			public int cbData;

			// Token: 0x040004A0 RID: 1184
			public IntPtr pbData;
		}
	}
}
