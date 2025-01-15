using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Microsoft.Mashup.Security
{
	// Token: 0x02000011 RID: 17
	public static class UserProtectedDataServices
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000022B4 File Offset: 0x000004B4
		public static byte[] Protect(byte[] userData, byte[] optionalEntropy)
		{
			byte[] array;
			try
			{
				array = UserProtectedDataServices.Protect(userData, optionalEntropy, DataProtectionScope.CurrentUser);
			}
			catch (CryptographicException)
			{
				UserProtectedDataServices.SynchronizeMasterKeys();
				array = UserProtectedDataServices.Protect(userData, optionalEntropy, DataProtectionScope.CurrentUser);
			}
			return array;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022F0 File Offset: 0x000004F0
		public static byte[] Unprotect(byte[] userData, byte[] optionalEntropy)
		{
			byte[] array;
			try
			{
				array = UserProtectedDataServices.Unprotect(userData, optionalEntropy, DataProtectionScope.CurrentUser);
			}
			catch (CryptographicException)
			{
				UserProtectedDataServices.SynchronizeMasterKeys();
				array = UserProtectedDataServices.Unprotect(userData, optionalEntropy, DataProtectionScope.CurrentUser);
			}
			return array;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000232C File Offset: 0x0000052C
		private static byte[] Protect(byte[] userData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			return ProtectedData.Protect(userData, optionalEntropy, scope);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002338 File Offset: 0x00000538
		private static void SynchronizeMasterKeys()
		{
			UserProtectedDataServices.DATA_BLOB data_BLOB = default(UserProtectedDataServices.DATA_BLOB);
			data_BLOB.cbData = 0;
			data_BLOB.pbData = Marshal.StringToHGlobalUni("");
			UserProtectedDataServices.DATA_BLOB data_BLOB2 = default(UserProtectedDataServices.DATA_BLOB);
			UserProtectedDataServices.DATA_BLOB data_BLOB3 = default(UserProtectedDataServices.DATA_BLOB);
			try
			{
				UserProtectedDataServices.CryptoApi.CryptProtectData(ref data_BLOB, null, ref data_BLOB2, IntPtr.Zero, IntPtr.Zero, UserProtectedDataServices.CryptProtectFlags.CRYPTPROTECT_CRED_SYNC, ref data_BLOB3);
			}
			finally
			{
				Marshal.FreeHGlobal(data_BLOB.pbData);
				Marshal.FreeHGlobal(data_BLOB3.pbData);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023B8 File Offset: 0x000005B8
		private static byte[] Unprotect(byte[] userData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			return ProtectedData.Unprotect(userData, optionalEntropy, scope);
		}

		// Token: 0x0200001B RID: 27
		private class CryptoApi
		{
			// Token: 0x06000047 RID: 71
			[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern bool CryptProtectData([In] ref UserProtectedDataServices.DATA_BLOB pDataIn, [In] string szDataDescr, [In] ref UserProtectedDataServices.DATA_BLOB pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] UserProtectedDataServices.CryptProtectFlags dwFlags, [In] [Out] ref UserProtectedDataServices.DATA_BLOB pDataBlob);
		}

		// Token: 0x0200001C RID: 28
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct DATA_BLOB
		{
			// Token: 0x0400000D RID: 13
			public int cbData;

			// Token: 0x0400000E RID: 14
			public IntPtr pbData;
		}

		// Token: 0x0200001D RID: 29
		[Flags]
		private enum CryptProtectFlags
		{
			// Token: 0x04000010 RID: 16
			CRYPTPROTECT_UI_FORBIDDEN = 1,
			// Token: 0x04000011 RID: 17
			CRYPTPROTECT_LOCAL_MACHINE = 4,
			// Token: 0x04000012 RID: 18
			CRYPTPROTECT_CRED_SYNC = 8,
			// Token: 0x04000013 RID: 19
			CRYPTPROTECT_AUDIT = 16,
			// Token: 0x04000014 RID: 20
			CRYPTPROTECT_NO_RECOVERY = 32,
			// Token: 0x04000015 RID: 21
			CRYPTPROTECT_VERIFY_PROTECTION = 64
		}
	}
}
