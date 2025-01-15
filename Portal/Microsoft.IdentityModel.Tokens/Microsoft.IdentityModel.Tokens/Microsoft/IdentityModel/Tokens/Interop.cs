using System;
using System.Runtime.InteropServices;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200016A RID: 362
	internal class Interop
	{
		// Token: 0x0200027C RID: 636
		internal static class BCrypt
		{
			// Token: 0x060014E2 RID: 5346 RVA: 0x00055391 File Offset: 0x00053591
			internal static Exception CreateCryptographicException(Interop.BCrypt.NTSTATUS ntStatus)
			{
				return ((int)(ntStatus | (Interop.BCrypt.NTSTATUS)16777216U)).ToCryptographicException();
			}

			// Token: 0x060014E3 RID: 5347 RVA: 0x000553A0 File Offset: 0x000535A0
			internal unsafe static SafeKeyHandle BCryptImportKey(SafeAlgorithmHandle hAlg, byte[] key)
			{
				int num = key.Length;
				int num2 = sizeof(Interop.BCrypt.BCRYPT_KEY_DATA_BLOB_HEADER) + num;
				byte[] array = new byte[num2];
				byte[] array2;
				byte* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				Interop.BCrypt.BCRYPT_KEY_DATA_BLOB_HEADER* ptr2 = (Interop.BCrypt.BCRYPT_KEY_DATA_BLOB_HEADER*)ptr;
				ptr2->dwMagic = 1296188491U;
				ptr2->dwVersion = 1U;
				ptr2->cbKeyData = (uint)num;
				array2 = null;
				key.CopyTo(array, sizeof(Interop.BCrypt.BCRYPT_KEY_DATA_BLOB_HEADER));
				SafeKeyHandle safeKeyHandle;
				Interop.BCrypt.NTSTATUS ntstatus = Interop.BCrypt.BCryptImportKey(hAlg, IntPtr.Zero, "KeyDataBlob", out safeKeyHandle, IntPtr.Zero, 0, array, num2, 0);
				if (ntstatus != Interop.BCrypt.NTSTATUS.STATUS_SUCCESS)
				{
					throw LogHelper.LogExceptionMessage(Interop.BCrypt.CreateCryptographicException(ntstatus));
				}
				return safeKeyHandle;
			}

			// Token: 0x060014E4 RID: 5348
			[DllImport("BCrypt.dll", CharSet = CharSet.Unicode)]
			public unsafe static extern Interop.BCrypt.NTSTATUS BCryptEncrypt(SafeKeyHandle hKey, byte* pbInput, int cbInput, IntPtr paddingInfo, [In] [Out] byte[] pbIV, int cbIV, byte* pbOutput, int cbOutput, out int cbResult, int dwFlags);

			// Token: 0x060014E5 RID: 5349
			[DllImport("BCrypt.dll", CharSet = CharSet.Unicode)]
			public unsafe static extern Interop.BCrypt.NTSTATUS BCryptDecrypt(SafeKeyHandle hKey, byte* pbInput, int cbInput, IntPtr paddingInfo, [In] [Out] byte[] pbIV, int cbIV, byte* pbOutput, int cbOutput, out int cbResult, int dwFlags);

			// Token: 0x060014E6 RID: 5350
			[DllImport("BCrypt.dll", CharSet = CharSet.Unicode)]
			private static extern Interop.BCrypt.NTSTATUS BCryptImportKey(SafeAlgorithmHandle hAlgorithm, IntPtr hImportKey, string pszBlobType, out SafeKeyHandle hKey, IntPtr pbKeyObject, int cbKeyObject, byte[] pbInput, int cbInput, int dwFlags);

			// Token: 0x060014E7 RID: 5351
			[DllImport("BCrypt.dll", CharSet = CharSet.Unicode)]
			public static extern Interop.BCrypt.NTSTATUS BCryptOpenAlgorithmProvider(out SafeAlgorithmHandle phAlgorithm, string pszAlgId, string pszImplementation, int dwFlags);

			// Token: 0x060014E8 RID: 5352
			[DllImport("BCrypt.dll", CharSet = CharSet.Unicode)]
			public static extern Interop.BCrypt.NTSTATUS BCryptSetProperty(SafeAlgorithmHandle hObject, string pszProperty, string pbInput, int cbInput, int dwFlags);

			// Token: 0x060014E9 RID: 5353
			[DllImport("BCrypt.dll", CharSet = CharSet.Unicode, EntryPoint = "BCryptSetProperty")]
			private static extern Interop.BCrypt.NTSTATUS BCryptSetIntPropertyPrivate(SafeBCryptHandle hObject, string pszProperty, ref int pdwInput, int cbInput, int dwFlags);

			// Token: 0x060014EA RID: 5354 RVA: 0x0005543D File Offset: 0x0005363D
			public static Interop.BCrypt.NTSTATUS BCryptSetIntProperty(SafeBCryptHandle hObject, string pszProperty, ref int pdwInput, int dwFlags)
			{
				return Interop.BCrypt.BCryptSetIntPropertyPrivate(hObject, pszProperty, ref pdwInput, 4, dwFlags);
			}

			// Token: 0x02000295 RID: 661
			internal struct BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO
			{
				// Token: 0x06001525 RID: 5413 RVA: 0x00055FA8 File Offset: 0x000541A8
				public unsafe static Interop.BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO Create()
				{
					return new Interop.BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO
					{
						cbSize = sizeof(Interop.BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO),
						dwInfoVersion = 1U
					};
				}

				// Token: 0x04000B78 RID: 2936
				private int cbSize;

				// Token: 0x04000B79 RID: 2937
				private uint dwInfoVersion;

				// Token: 0x04000B7A RID: 2938
				internal unsafe byte* pbNonce;

				// Token: 0x04000B7B RID: 2939
				internal int cbNonce;

				// Token: 0x04000B7C RID: 2940
				internal unsafe byte* pbAuthData;

				// Token: 0x04000B7D RID: 2941
				internal int cbAuthData;

				// Token: 0x04000B7E RID: 2942
				internal unsafe byte* pbTag;

				// Token: 0x04000B7F RID: 2943
				internal int cbTag;

				// Token: 0x04000B80 RID: 2944
				internal unsafe byte* pbMacContext;

				// Token: 0x04000B81 RID: 2945
				internal int cbMacContext;

				// Token: 0x04000B82 RID: 2946
				internal int cbAAD;

				// Token: 0x04000B83 RID: 2947
				internal ulong cbData;

				// Token: 0x04000B84 RID: 2948
				internal uint dwFlags;
			}

			// Token: 0x02000296 RID: 662
			private struct BCRYPT_KEY_DATA_BLOB_HEADER
			{
				// Token: 0x04000B85 RID: 2949
				public uint dwMagic;

				// Token: 0x04000B86 RID: 2950
				public uint dwVersion;

				// Token: 0x04000B87 RID: 2951
				public uint cbKeyData;

				// Token: 0x04000B88 RID: 2952
				public const uint BCRYPT_KEY_DATA_BLOB_MAGIC = 1296188491U;

				// Token: 0x04000B89 RID: 2953
				public const uint BCRYPT_KEY_DATA_BLOB_VERSION1 = 1U;
			}

			// Token: 0x02000297 RID: 663
			internal enum NTSTATUS : uint
			{
				// Token: 0x04000B8B RID: 2955
				STATUS_SUCCESS,
				// Token: 0x04000B8C RID: 2956
				STATUS_NOT_FOUND = 3221226021U,
				// Token: 0x04000B8D RID: 2957
				STATUS_INVALID_PARAMETER = 3221225485U,
				// Token: 0x04000B8E RID: 2958
				STATUS_NO_MEMORY = 3221225495U,
				// Token: 0x04000B8F RID: 2959
				STATUS_AUTH_TAG_MISMATCH = 3221266434U
			}

			// Token: 0x02000298 RID: 664
			internal static class BCryptPropertyStrings
			{
				// Token: 0x04000B90 RID: 2960
				internal const string BCRYPT_CHAINING_MODE = "ChainingMode";

				// Token: 0x04000B91 RID: 2961
				internal const string BCRYPT_ECC_PARAMETERS = "ECCParameters";

				// Token: 0x04000B92 RID: 2962
				internal const string BCRYPT_EFFECTIVE_KEY_LENGTH = "EffectiveKeyLength";

				// Token: 0x04000B93 RID: 2963
				internal const string BCRYPT_HASH_LENGTH = "HashDigestLength";

				// Token: 0x04000B94 RID: 2964
				internal const string BCRYPT_MESSAGE_BLOCK_LENGTH = "MessageBlockLength";
			}
		}

		// Token: 0x0200027D RID: 637
		internal static class Kernel32
		{
			// Token: 0x060014EB RID: 5355
			[DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Unicode, EntryPoint = "FormatMessageW", ExactSpelling = true, SetLastError = true)]
			private unsafe static extern int FormatMessage(int dwFlags, IntPtr lpSource, uint dwMessageId, int dwLanguageId, void* lpBuffer, int nSize, IntPtr arguments);

			// Token: 0x060014EC RID: 5356 RVA: 0x00055449 File Offset: 0x00053649
			internal static string GetMessage(int errorCode)
			{
				return Interop.Kernel32.GetMessage(errorCode, IntPtr.Zero);
			}

			// Token: 0x060014ED RID: 5357 RVA: 0x00055458 File Offset: 0x00053658
			internal unsafe static string GetMessage(int errorCode, IntPtr moduleHandle)
			{
				int num = 12800;
				if (moduleHandle != IntPtr.Zero)
				{
					num |= 2048;
				}
				char[] array = new char[256];
				char[] array2;
				char* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				int num2 = Interop.Kernel32.FormatMessage(num, moduleHandle, (uint)errorCode, 0, (void*)ptr, array.Length, IntPtr.Zero);
				if (num2 > 0)
				{
					return Interop.Kernel32.GetAndTrimString(array, num2);
				}
				array2 = null;
				if (Marshal.GetLastWin32Error() == 122)
				{
					IntPtr intPtr = 0;
					try
					{
						int num3 = Interop.Kernel32.FormatMessage(num | 256, moduleHandle, (uint)errorCode, 0, (void*)(&intPtr), 0, IntPtr.Zero);
						if (num3 > 0)
						{
							return Interop.Kernel32.GetAndTrimString(Marshal.PtrToStringAnsi(intPtr).ToCharArray(), num3);
						}
					}
					finally
					{
						Marshal.FreeHGlobal(intPtr);
					}
				}
				return string.Format("Unknown error (0x{0:x})", errorCode);
			}

			// Token: 0x060014EE RID: 5358 RVA: 0x0005553C File Offset: 0x0005373C
			private static string GetAndTrimString(char[] buffer, int length)
			{
				while (length > 0 && buffer[length - 1] <= ' ')
				{
					length--;
				}
				return new string(buffer, 0, length);
			}

			// Token: 0x04000B47 RID: 2887
			private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

			// Token: 0x04000B48 RID: 2888
			private const int FORMAT_MESSAGE_FROM_HMODULE = 2048;

			// Token: 0x04000B49 RID: 2889
			private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

			// Token: 0x04000B4A RID: 2890
			private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;

			// Token: 0x04000B4B RID: 2891
			private const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 256;

			// Token: 0x04000B4C RID: 2892
			private const int ERROR_INSUFFICIENT_BUFFER = 122;
		}

		// Token: 0x0200027E RID: 638
		internal static class Libraries
		{
			// Token: 0x04000B4D RID: 2893
			internal const string BCrypt = "BCrypt.dll";

			// Token: 0x04000B4E RID: 2894
			internal const string Kernel32 = "kernel32.dll";
		}
	}
}
