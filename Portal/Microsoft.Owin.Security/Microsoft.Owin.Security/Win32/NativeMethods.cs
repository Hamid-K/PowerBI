using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Win32
{
	// Token: 0x02000002 RID: 2
	[Localizable(false)]
	internal static class NativeMethods
	{
		// Token: 0x06000001 RID: 1
		[DllImport("crypt32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CryptEncodeObject(uint dwCertEncodingType, IntPtr lpszStructType, ref NativeMethods.CERT_PUBLIC_KEY_INFO pvStructInfo, byte[] pbEncoded, ref uint pcbEncoded);

		// Token: 0x04000001 RID: 1
		public const int X509_ASN_ENCODING = 1;

		// Token: 0x04000002 RID: 2
		public const int X509_PUBLIC_KEY_INFO = 8;

		// Token: 0x02000035 RID: 53
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_BLOB
		{
			// Token: 0x04000052 RID: 82
			public int cbData;

			// Token: 0x04000053 RID: 83
			public IntPtr pbData;
		}

		// Token: 0x02000036 RID: 54
		internal struct CERT_CONTEXT
		{
			// Token: 0x04000054 RID: 84
			public int dwCertEncodingType;

			// Token: 0x04000055 RID: 85
			public IntPtr pbCertEncoded;

			// Token: 0x04000056 RID: 86
			public int cbCertEncoded;

			// Token: 0x04000057 RID: 87
			public IntPtr pCertInfo;

			// Token: 0x04000058 RID: 88
			public IntPtr hCertStore;
		}

		// Token: 0x02000037 RID: 55
		internal struct CRYPT_ALGORITHM_IDENTIFIER
		{
			// Token: 0x04000059 RID: 89
			public string pszObjId;

			// Token: 0x0400005A RID: 90
			public NativeMethods.CRYPT_BLOB Parameters;
		}

		// Token: 0x02000038 RID: 56
		internal struct CRYPT_BIT_BLOB
		{
			// Token: 0x0400005B RID: 91
			public int cbData;

			// Token: 0x0400005C RID: 92
			public IntPtr pbData;

			// Token: 0x0400005D RID: 93
			public int cUnusedBits;
		}

		// Token: 0x02000039 RID: 57
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_PUBLIC_KEY_INFO
		{
			// Token: 0x0400005E RID: 94
			public NativeMethods.CRYPT_ALGORITHM_IDENTIFIER Algorithm;

			// Token: 0x0400005F RID: 95
			public NativeMethods.CRYPT_BIT_BLOB PublicKey;
		}

		// Token: 0x0200003A RID: 58
		[StructLayout(LayoutKind.Sequential)]
		internal class CERT_INFO
		{
			// Token: 0x04000060 RID: 96
			public int dwVersion;

			// Token: 0x04000061 RID: 97
			public NativeMethods.CRYPT_BLOB SerialNumber;

			// Token: 0x04000062 RID: 98
			public NativeMethods.CRYPT_ALGORITHM_IDENTIFIER SignatureAlgorithm;

			// Token: 0x04000063 RID: 99
			public NativeMethods.CRYPT_BLOB Issuer;

			// Token: 0x04000064 RID: 100
			public global::System.Runtime.InteropServices.ComTypes.FILETIME NotBefore;

			// Token: 0x04000065 RID: 101
			public global::System.Runtime.InteropServices.ComTypes.FILETIME NotAfter;

			// Token: 0x04000066 RID: 102
			public NativeMethods.CRYPT_BLOB Subject;

			// Token: 0x04000067 RID: 103
			public NativeMethods.CERT_PUBLIC_KEY_INFO SubjectPublicKeyInfo;

			// Token: 0x04000068 RID: 104
			public NativeMethods.CRYPT_BIT_BLOB IssuerUniqueId;

			// Token: 0x04000069 RID: 105
			public NativeMethods.CRYPT_BIT_BLOB SubjectUniqueId;

			// Token: 0x0400006A RID: 106
			public int cExtension;

			// Token: 0x0400006B RID: 107
			public IntPtr rgExtension;
		}
	}
}
