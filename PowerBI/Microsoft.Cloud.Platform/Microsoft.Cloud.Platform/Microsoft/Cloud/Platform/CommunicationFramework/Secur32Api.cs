using System;
using System.Runtime.InteropServices;

namespace Microsoft.Cloud.Platform.CommunicationFramework
{
	// Token: 0x02000473 RID: 1139
	internal static class Secur32Api
	{
		// Token: 0x0600237D RID: 9085
		[DllImport("Secur32.dll")]
		internal static extern uint QueryContextAttributes([In] IntPtr phContext, [In] uint ulAttribute, out IntPtr pBuffer);

		// Token: 0x0600237E RID: 9086
		[DllImport("Secur32.dll")]
		internal static extern uint FreeContextBuffer([In] IntPtr pvContextBuffer);

		// Token: 0x04000C60 RID: 3168
		public const uint SEC_E_OK = 0U;

		// Token: 0x04000C61 RID: 3169
		public const uint SECPKG_ATTR_CIPHER_INFO = 100U;

		// Token: 0x0200082B RID: 2091
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct SecPkgContext_CipherInfo
		{
			// Token: 0x040018EA RID: 6378
			public uint dwVersion;

			// Token: 0x040018EB RID: 6379
			public uint dwProtocol;

			// Token: 0x040018EC RID: 6380
			public uint dwCipherSuite;

			// Token: 0x040018ED RID: 6381
			public uint dwBaseCipherSuite;

			// Token: 0x040018EE RID: 6382
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szCipherSuite;

			// Token: 0x040018EF RID: 6383
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szCipher;

			// Token: 0x040018F0 RID: 6384
			public uint dwCipherLen;

			// Token: 0x040018F1 RID: 6385
			public uint dCipherBlockLen;

			// Token: 0x040018F2 RID: 6386
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szHash;

			// Token: 0x040018F3 RID: 6387
			public uint dwHashLen;

			// Token: 0x040018F4 RID: 6388
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szExchange;

			// Token: 0x040018F5 RID: 6389
			public uint dwMinExchangeLen;

			// Token: 0x040018F6 RID: 6390
			public uint dwMaxExchangeLen;

			// Token: 0x040018F7 RID: 6391
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szCertificate;

			// Token: 0x040018F8 RID: 6392
			public uint dwKeyType;
		}
	}
}
