using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200027F RID: 639
	public static class SecureStringUtils
	{
		// Token: 0x0600110D RID: 4365 RVA: 0x0003AB08 File Offset: 0x00038D08
		public static SecureString StringToSecureString(string plainText)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(plainText, "plainText");
			SecureString secureString = new SecureString();
			foreach (char c in plainText)
			{
				secureString.AppendChar(c);
			}
			return secureString;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0003AB4C File Offset: 0x00038D4C
		public static byte[] Base64SecureStringToByteArray(SecureString secureString)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SecureString>(secureString, "secureString");
			ExtendedDiagnostics.EnsureArgumentIsPositive(secureString.Length, "secureString is empty");
			IntPtr intPtr = IntPtr.Zero;
			char[] array = new char[secureString.Length];
			byte[] array2;
			try
			{
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
				Marshal.Copy(intPtr, array, 0, secureString.Length);
				array2 = Convert.FromBase64CharArray(array, 0, array.Length);
			}
			finally
			{
				Array.Clear(array, 0, array.Length);
				Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
			}
			return array2;
		}
	}
}
