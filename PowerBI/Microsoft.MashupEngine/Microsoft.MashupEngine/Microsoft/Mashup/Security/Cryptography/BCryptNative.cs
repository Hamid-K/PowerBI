using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Mashup.Security.Cryptography.Properties;

namespace Microsoft.Mashup.Security.Cryptography
{
	// Token: 0x02001FDB RID: 8155
	internal static class BCryptNative
	{
		// Token: 0x0600C72A RID: 50986 RVA: 0x0027A90C File Offset: 0x00278B0C
		[SecurityCritical]
		internal static SafeBCryptHashHandle CreateHash(SafeBCryptAlgorithmHandle algorithm, byte[] secret)
		{
			IntPtr intPtr = IntPtr.Zero;
			SafeBCryptHashHandle safeBCryptHashHandle = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			SafeBCryptHashHandle safeBCryptHashHandle2;
			try
			{
				int int32Property = BCryptNative.GetInt32Property<SafeBCryptAlgorithmHandle>(algorithm, "ObjectLength");
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					intPtr = Marshal.AllocCoTaskMem(int32Property);
				}
				BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptCreateHash(algorithm, out safeBCryptHashHandle, intPtr, int32Property, secret, (secret != null) ? secret.Length : 0, 0);
				if (errorCode != BCryptNative.ErrorCode.Success)
				{
					throw new CryptographicException(Win32Native.GetNTStatusMessage((int)errorCode));
				}
				safeBCryptHashHandle.DataBuffer = intPtr;
				safeBCryptHashHandle2 = safeBCryptHashHandle;
			}
			finally
			{
				if (intPtr != IntPtr.Zero && (safeBCryptHashHandle == null || safeBCryptHashHandle.DataBuffer == IntPtr.Zero))
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
			}
			return safeBCryptHashHandle2;
		}

		// Token: 0x0600C72B RID: 50987 RVA: 0x0027A9BC File Offset: 0x00278BBC
		[SecurityCritical]
		internal static byte[] FinishHash(SafeBCryptHashHandle hash)
		{
			byte[] array = new byte[BCryptNative.GetInt32Property<SafeBCryptHashHandle>(hash, "HashDigestLength")];
			BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptFinishHash(hash, array, array.Length, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException(Win32Native.GetNTStatusMessage((int)errorCode));
			}
			return array;
		}

		// Token: 0x0600C72C RID: 50988 RVA: 0x0027A9F8 File Offset: 0x00278BF8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static void GenerateRandomBytes(SafeBCryptAlgorithmHandle algorithm, byte[] buffer)
		{
			BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptGenRandom(algorithm, buffer, buffer.Length, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException(Win32Native.GetNTStatusMessage((int)errorCode));
			}
		}

		// Token: 0x0600C72D RID: 50989 RVA: 0x0027AA20 File Offset: 0x00278C20
		[SecurityCritical]
		internal static int GetInt32Property<T>(T bcryptObject, string property) where T : SafeHandle
		{
			return BitConverter.ToInt32(BCryptNative.GetProperty<T>(bcryptObject, property), 0);
		}

		// Token: 0x0600C72E RID: 50990 RVA: 0x0027AA30 File Offset: 0x00278C30
		[SecurityCritical]
		internal unsafe static string GetStringProperty<T>(T bcryptObject, string property) where T : SafeHandle
		{
			byte[] property2 = BCryptNative.GetProperty<T>(bcryptObject, property);
			if (property2 == null)
			{
				return null;
			}
			if (property2.Length == 0)
			{
				return string.Empty;
			}
			byte[] array;
			byte* ptr;
			if ((array = property2) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return Marshal.PtrToStringUni(new IntPtr((void*)ptr));
		}

		// Token: 0x0600C72F RID: 50991 RVA: 0x0027AA78 File Offset: 0x00278C78
		[SecurityCritical]
		internal unsafe static TProperty GetValueTypeProperty<THandle, TProperty>(THandle bcryptObject, string property) where THandle : SafeHandle where TProperty : struct
		{
			byte[] property2 = BCryptNative.GetProperty<THandle>(bcryptObject, property);
			if (property2 == null || property2.Length == 0)
			{
				return default(TProperty);
			}
			byte[] array;
			byte* ptr;
			if ((array = property2) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return (TProperty)((object)Marshal.PtrToStructure(new IntPtr((void*)ptr), typeof(TProperty)));
		}

		// Token: 0x0600C730 RID: 50992 RVA: 0x0027AAD0 File Offset: 0x00278CD0
		[SecurityCritical]
		internal static byte[] GetProperty<T>(T bcryptObject, string property) where T : SafeHandle
		{
			BCryptNative.BCryptPropertyGetter<T> bcryptPropertyGetter = null;
			if (typeof(T) == typeof(SafeBCryptAlgorithmHandle))
			{
				bcryptPropertyGetter = new BCryptNative.BCryptPropertyGetter<SafeBCryptAlgorithmHandle>(BCryptNative.UnsafeNativeMethods.BCryptGetAlgorithmProperty) as BCryptNative.BCryptPropertyGetter<T>;
			}
			else if (typeof(T) == typeof(SafeBCryptHashHandle))
			{
				bcryptPropertyGetter = new BCryptNative.BCryptPropertyGetter<SafeBCryptHashHandle>(BCryptNative.UnsafeNativeMethods.BCryptGetHashProperty) as BCryptNative.BCryptPropertyGetter<T>;
			}
			int num = 0;
			BCryptNative.ErrorCode errorCode = bcryptPropertyGetter(bcryptObject, property, null, 0, ref num, 0);
			if (errorCode != BCryptNative.ErrorCode.Success && errorCode != BCryptNative.ErrorCode.BufferToSmall)
			{
				throw new CryptographicException(Win32Native.GetNTStatusMessage((int)errorCode));
			}
			byte[] array = new byte[num];
			errorCode = bcryptPropertyGetter(bcryptObject, property, array, array.Length, ref num, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException(Win32Native.GetNTStatusMessage((int)errorCode));
			}
			return array;
		}

		// Token: 0x0600C731 RID: 50993 RVA: 0x0027AB80 File Offset: 0x00278D80
		[SecurityCritical]
		internal static void HashData(SafeBCryptHashHandle hash, byte[] data)
		{
			BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptHashData(hash, data, data.Length, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException(Win32Native.GetNTStatusMessage((int)errorCode));
			}
		}

		// Token: 0x0600C732 RID: 50994 RVA: 0x0027ABA8 File Offset: 0x00278DA8
		[SecurityCritical]
		internal unsafe static SafeBCryptKeyHandle ImportSymmetricKey(SafeBCryptAlgorithmHandle algorithm, byte[] key)
		{
			IntPtr intPtr = IntPtr.Zero;
			SafeBCryptKeyHandle safeBCryptKeyHandle = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			SafeBCryptKeyHandle safeBCryptKeyHandle2;
			try
			{
				byte[] array = new byte[Marshal.SizeOf(typeof(BCryptNative.BCRYPT_KEY_DATA_BLOB)) + key.Length];
				try
				{
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
					BCryptNative.BCRYPT_KEY_DATA_BLOB* ptr2 = (BCryptNative.BCRYPT_KEY_DATA_BLOB*)ptr;
					ptr2->dwMagic = BCryptNative.KeyBlobMagicNumber.KeyDataBlob;
					ptr2->dwVersion = 1;
					ptr2->cbKeyData = key.Length;
				}
				finally
				{
					byte[] array2 = null;
				}
				Buffer.BlockCopy(key, 0, array, Marshal.SizeOf(typeof(BCryptNative.BCRYPT_KEY_DATA_BLOB)), key.Length);
				int int32Property = BCryptNative.GetInt32Property<SafeBCryptAlgorithmHandle>(algorithm, "ObjectLength");
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					intPtr = Marshal.AllocCoTaskMem(int32Property);
				}
				BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptImportKey(algorithm, IntPtr.Zero, "KeyDataBlob", out safeBCryptKeyHandle, intPtr, int32Property, array, array.Length, 0);
				if (errorCode != BCryptNative.ErrorCode.Success)
				{
					throw new CryptographicException(Win32Native.GetNTStatusMessage((int)errorCode));
				}
				safeBCryptKeyHandle.DataBuffer = intPtr;
				safeBCryptKeyHandle2 = safeBCryptKeyHandle;
			}
			finally
			{
				if (intPtr != IntPtr.Zero && (safeBCryptKeyHandle == null || safeBCryptKeyHandle.DataBuffer == IntPtr.Zero))
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
			}
			return safeBCryptKeyHandle2;
		}

		// Token: 0x0600C733 RID: 50995 RVA: 0x0027ACE0 File Offset: 0x00278EE0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static void InitializeAuthnenticatedCipherModeInfo(ref BCryptNative.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO authInfo)
		{
			authInfo.cbSize = Marshal.SizeOf(typeof(BCryptNative.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO));
			authInfo.dwInfoVersion = 1;
		}

		// Token: 0x0600C734 RID: 50996 RVA: 0x0027ACFE File Offset: 0x00278EFE
		internal static string MapChainingMode(CipherMode mode)
		{
			if (mode == CipherMode.CBC)
			{
				return "ChainingModeCBC";
			}
			throw new ArgumentException(Resources.UnsupportedCipherMode, "mode");
		}

		// Token: 0x0600C735 RID: 50997 RVA: 0x0027AD19 File Offset: 0x00278F19
		internal static CipherMode MapChainingMode(string mode)
		{
			if (string.Equals(mode, "ChainingModeCBC", StringComparison.Ordinal))
			{
				return CipherMode.CBC;
			}
			throw new ArgumentException(Resources.UnsupportedCipherMode, "mode");
		}

		// Token: 0x0600C736 RID: 50998 RVA: 0x0027AD3A File Offset: 0x00278F3A
		[SecurityCritical]
		internal static SafeBCryptAlgorithmHandle OpenAlgorithm(string algorithm, string implementation)
		{
			return BCryptNative.OpenAlgorithm(algorithm, implementation, BCryptNative.AlgorithmProviderOptions.None);
		}

		// Token: 0x0600C737 RID: 50999 RVA: 0x0027AD44 File Offset: 0x00278F44
		[SecurityCritical]
		internal static SafeBCryptAlgorithmHandle OpenAlgorithm(string algorithm, string implementation, BCryptNative.AlgorithmProviderOptions options)
		{
			SafeBCryptAlgorithmHandle safeBCryptAlgorithmHandle = null;
			BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptOpenAlgorithmProvider(out safeBCryptAlgorithmHandle, algorithm, implementation, options);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException(Win32Native.GetNTStatusMessage((int)errorCode));
			}
			return safeBCryptAlgorithmHandle;
		}

		// Token: 0x0600C738 RID: 51000 RVA: 0x0027AD6E File Offset: 0x00278F6E
		[SecurityCritical]
		internal static void SetInt32Property<T>(T bcryptObject, string property, int value) where T : SafeHandle
		{
			BCryptNative.SetProperty<T>(bcryptObject, property, BitConverter.GetBytes(value));
		}

		// Token: 0x0600C739 RID: 51001 RVA: 0x0027AD7D File Offset: 0x00278F7D
		[SecurityCritical]
		internal static void SetStringProperty<T>(T bcryptObject, string property, string value) where T : SafeHandle
		{
			BCryptNative.SetProperty<T>(bcryptObject, property, Encoding.Unicode.GetBytes(value));
		}

		// Token: 0x0600C73A RID: 51002 RVA: 0x0027AD94 File Offset: 0x00278F94
		[SecurityCritical]
		internal static void SetProperty<T>(T bcryptObject, string property, byte[] value) where T : SafeHandle
		{
			BCryptNative.BCryptPropertySetter<T> bcryptPropertySetter = null;
			if (typeof(T) == typeof(SafeBCryptAlgorithmHandle))
			{
				bcryptPropertySetter = new BCryptNative.BCryptPropertySetter<SafeBCryptAlgorithmHandle>(BCryptNative.UnsafeNativeMethods.BCryptSetAlgorithmProperty) as BCryptNative.BCryptPropertySetter<T>;
			}
			else if (typeof(T) == typeof(SafeBCryptHashHandle))
			{
				bcryptPropertySetter = new BCryptNative.BCryptPropertySetter<SafeBCryptHashHandle>(BCryptNative.UnsafeNativeMethods.BCryptSetHashProperty) as BCryptNative.BCryptPropertySetter<T>;
			}
			BCryptNative.ErrorCode errorCode = bcryptPropertySetter(bcryptObject, property, value, value.Length, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException(Win32Native.GetNTStatusMessage((int)errorCode));
			}
		}

		// Token: 0x0600C73B RID: 51003 RVA: 0x0027AE14 File Offset: 0x00279014
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static byte[] SymmetricDecrypt(SafeBCryptKeyHandle key, byte[] iv, byte[] input)
		{
			byte[] array = new byte[input.Length];
			int num = 0;
			BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptDecrypt(key, input, input.Length, IntPtr.Zero, iv, (iv != null) ? iv.Length : 0, array, array.Length, out num, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException(Win32Native.GetNTStatusMessage((int)errorCode));
			}
			if (num != array.Length)
			{
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(array, 0, array2, 0, array2.Length);
				array = array2;
			}
			return array;
		}

		// Token: 0x0600C73C RID: 51004 RVA: 0x0027AE78 File Offset: 0x00279078
		[SecurityCritical]
		internal static byte[] SymmetricDecrypt(SafeBCryptKeyHandle key, byte[] input, byte[] chainData, ref BCryptNative.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO authenticationInfo)
		{
			byte[] array = new byte[(input != null) ? input.Length : 0];
			int num = 0;
			BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptDecrypt(key, input, (input != null) ? input.Length : 0, ref authenticationInfo, chainData, (chainData != null) ? chainData.Length : 0, array, array.Length, out num, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException(Win32Native.GetNTStatusMessage((int)errorCode));
			}
			if (num != array.Length)
			{
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(array, 0, array2, 0, array2.Length);
				array = array2;
			}
			return array;
		}

		// Token: 0x0600C73D RID: 51005 RVA: 0x0027AEE4 File Offset: 0x002790E4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static byte[] SymmetricEncrypt(SafeBCryptKeyHandle key, byte[] iv, byte[] input)
		{
			byte[] array = new byte[input.Length];
			int num = 0;
			BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptEncrypt(key, input, (input != null) ? input.Length : 0, IntPtr.Zero, iv, (iv != null) ? iv.Length : 0, array, array.Length, out num, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException(Win32Native.GetNTStatusMessage((int)errorCode));
			}
			if (num != array.Length)
			{
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(array, 0, array2, 0, array2.Length);
				array = array2;
			}
			return array;
		}

		// Token: 0x0600C73E RID: 51006 RVA: 0x0027AF50 File Offset: 0x00279150
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static byte[] SymmetricEncrypt(SafeBCryptKeyHandle key, byte[] input, byte[] chainData, ref BCryptNative.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO authenticationInfo)
		{
			byte[] array = new byte[(input != null) ? input.Length : 0];
			int num = 0;
			BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptEncrypt(key, input, (input != null) ? input.Length : 0, ref authenticationInfo, chainData, (chainData != null) ? chainData.Length : 0, array, array.Length, out num, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException(Win32Native.GetNTStatusMessage((int)errorCode));
			}
			if (num != array.Length)
			{
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(array, 0, array2, 0, array2.Length);
				array = array2;
			}
			return array;
		}

		// Token: 0x02001FDC RID: 8156
		internal static class AlgorithmName
		{
			// Token: 0x04006597 RID: 26007
			internal const string Aes = "AES";

			// Token: 0x04006598 RID: 26008
			internal const string Rng = "RNG";

			// Token: 0x04006599 RID: 26009
			internal const string Rsa = "RSA";

			// Token: 0x0400659A RID: 26010
			internal const string TripleDes = "3DES";
		}

		// Token: 0x02001FDD RID: 8157
		[Flags]
		internal enum AlgorithmProviderOptions
		{
			// Token: 0x0400659C RID: 26012
			None = 0,
			// Token: 0x0400659D RID: 26013
			HmacAlgorithm = 8
		}

		// Token: 0x02001FDE RID: 8158
		[Flags]
		internal enum AuthenticatedCipherModeInfoFlags
		{
			// Token: 0x0400659F RID: 26015
			None = 0,
			// Token: 0x040065A0 RID: 26016
			ChainCalls = 1,
			// Token: 0x040065A1 RID: 26017
			InProgress = 2
		}

		// Token: 0x02001FDF RID: 8159
		internal static class ChainingMode
		{
			// Token: 0x040065A2 RID: 26018
			internal const string Cbc = "ChainingModeCBC";

			// Token: 0x040065A3 RID: 26019
			internal const string Ccm = "ChainingModeCCM";

			// Token: 0x040065A4 RID: 26020
			internal const string Cfb = "ChainingModeCFB";

			// Token: 0x040065A5 RID: 26021
			internal const string Ecb = "ChainingModeECB";

			// Token: 0x040065A6 RID: 26022
			internal const string Gcm = "ChainingModeGCM";
		}

		// Token: 0x02001FE0 RID: 8160
		internal enum ErrorCode
		{
			// Token: 0x040065A8 RID: 26024
			Success,
			// Token: 0x040065A9 RID: 26025
			AuthenticationTagMismatch = -1073700862,
			// Token: 0x040065AA RID: 26026
			BufferToSmall = -1073741789
		}

		// Token: 0x02001FE1 RID: 8161
		internal static class HashPropertyName
		{
			// Token: 0x040065AB RID: 26027
			internal const string HashLength = "HashDigestLength";
		}

		// Token: 0x02001FE2 RID: 8162
		internal enum KeyBlobMagicNumber
		{
			// Token: 0x040065AD RID: 26029
			RsaPublic = 826364754,
			// Token: 0x040065AE RID: 26030
			RsaPrivate = 843141970,
			// Token: 0x040065AF RID: 26031
			KeyDataBlob = 1296188491
		}

		// Token: 0x02001FE3 RID: 8163
		internal static class KeyBlobType
		{
			// Token: 0x040065B0 RID: 26032
			internal const string KeyDataBlob = "KeyDataBlob";

			// Token: 0x040065B1 RID: 26033
			internal const string RsaFullPrivateBlob = "RSAFULLPRIVATEBLOB";

			// Token: 0x040065B2 RID: 26034
			internal const string RsaPrivateBlob = "RSAPRIVATEBLOB";

			// Token: 0x040065B3 RID: 26035
			internal const string RsaPublicBlob = "RSAPUBLICBLOB";
		}

		// Token: 0x02001FE4 RID: 8164
		internal static class ObjectPropertyName
		{
			// Token: 0x040065B4 RID: 26036
			internal const string AuthTagLength = "AuthTagLength";

			// Token: 0x040065B5 RID: 26037
			internal const string BlockLength = "BlockLength";

			// Token: 0x040065B6 RID: 26038
			internal const string ChainingMode = "ChainingMode";

			// Token: 0x040065B7 RID: 26039
			internal const string InitializationVector = "IV";

			// Token: 0x040065B8 RID: 26040
			internal const string KeyLength = "KeyLength";

			// Token: 0x040065B9 RID: 26041
			internal const string ObjectLength = "ObjectLength";
		}

		// Token: 0x02001FE5 RID: 8165
		internal static class ProviderName
		{
			// Token: 0x040065BA RID: 26042
			internal const string MicrosoftPrimitiveProvider = "Microsoft Primitive Provider";
		}

		// Token: 0x02001FE6 RID: 8166
		internal struct BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO
		{
			// Token: 0x040065BB RID: 26043
			internal int cbSize;

			// Token: 0x040065BC RID: 26044
			internal int dwInfoVersion;

			// Token: 0x040065BD RID: 26045
			internal IntPtr pbNonce;

			// Token: 0x040065BE RID: 26046
			internal int cbNonce;

			// Token: 0x040065BF RID: 26047
			internal IntPtr pbAuthData;

			// Token: 0x040065C0 RID: 26048
			internal int cbAuthData;

			// Token: 0x040065C1 RID: 26049
			internal IntPtr pbTag;

			// Token: 0x040065C2 RID: 26050
			internal int cbTag;

			// Token: 0x040065C3 RID: 26051
			internal IntPtr pbMacContext;

			// Token: 0x040065C4 RID: 26052
			internal int cbMacContext;

			// Token: 0x040065C5 RID: 26053
			internal int cbAAD;

			// Token: 0x040065C6 RID: 26054
			internal long cbData;

			// Token: 0x040065C7 RID: 26055
			internal BCryptNative.AuthenticatedCipherModeInfoFlags dwFlags;
		}

		// Token: 0x02001FE7 RID: 8167
		internal struct BCRYPT_KEY_DATA_BLOB
		{
			// Token: 0x040065C8 RID: 26056
			internal BCryptNative.KeyBlobMagicNumber dwMagic;

			// Token: 0x040065C9 RID: 26057
			internal int dwVersion;

			// Token: 0x040065CA RID: 26058
			internal int cbKeyData;
		}

		// Token: 0x02001FE8 RID: 8168
		internal struct BCRYPT_KEY_LENGTHS_STRUCT
		{
			// Token: 0x040065CB RID: 26059
			internal int dwMinLength;

			// Token: 0x040065CC RID: 26060
			internal int dwMaxLength;

			// Token: 0x040065CD RID: 26061
			internal int dwIncrement;
		}

		// Token: 0x02001FE9 RID: 8169
		internal struct BCRYPT_OAEP_PADDING_INFO
		{
			// Token: 0x040065CE RID: 26062
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszAlgId;

			// Token: 0x040065CF RID: 26063
			internal IntPtr pbLabel;

			// Token: 0x040065D0 RID: 26064
			internal int cbLabel;
		}

		// Token: 0x02001FEA RID: 8170
		internal struct BCRYPT_PKCS1_PADDING_INFO
		{
			// Token: 0x040065D1 RID: 26065
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszAlgId;
		}

		// Token: 0x02001FEB RID: 8171
		internal struct BCRYPT_PSS_PADDING_INFO
		{
			// Token: 0x040065D2 RID: 26066
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszAlgId;

			// Token: 0x040065D3 RID: 26067
			internal int cbSalt;
		}

		// Token: 0x02001FEC RID: 8172
		internal struct BCRYPT_RSAKEY_BLOB
		{
			// Token: 0x040065D4 RID: 26068
			internal BCryptNative.KeyBlobMagicNumber Magic;

			// Token: 0x040065D5 RID: 26069
			internal int BitLength;

			// Token: 0x040065D6 RID: 26070
			internal int cbPublicExp;

			// Token: 0x040065D7 RID: 26071
			internal int cbModulus;

			// Token: 0x040065D8 RID: 26072
			internal int cbPrime1;

			// Token: 0x040065D9 RID: 26073
			internal int cbPrime2;
		}

		// Token: 0x02001FED RID: 8173
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		private static class UnsafeNativeMethods
		{
			// Token: 0x0600C73F RID: 51007
			[DllImport("bcrypt.dll")]
			internal static extern BCryptNative.ErrorCode BCryptCreateHash(SafeBCryptAlgorithmHandle hAlgorithm, out SafeBCryptHashHandle hHash, IntPtr pbHashObject, int cbHashObject, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSecret, int cbSecret, int dwFlags);

			// Token: 0x0600C740 RID: 51008
			[DllImport("bcrypt.dll")]
			internal static extern BCryptNative.ErrorCode BCryptDecrypt(SafeBCryptKeyHandle hKey, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbInput, int cbInput, IntPtr pPaddingInfo, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbIV, int cbIV, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbOutput, int cbOutput, out int pcbResult, int dwFlags);

			// Token: 0x0600C741 RID: 51009
			[DllImport("bcrypt.dll")]
			internal static extern BCryptNative.ErrorCode BCryptDecrypt(SafeBCryptKeyHandle hKey, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbInput, int cbInput, [In] [Out] ref BCryptNative.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO pPaddingInfo, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbIV, int cbIV, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbOutput, int cbOutput, out int pcbResult, int dwFlags);

			// Token: 0x0600C742 RID: 51010
			[DllImport("bcrypt.dll")]
			internal static extern BCryptNative.ErrorCode BCryptEncrypt(SafeBCryptKeyHandle hKey, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbInput, int cbInput, IntPtr pPaddingInfo, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbIV, int cbIV, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbOutput, int cbOutput, out int pcbResult, int dwFlags);

			// Token: 0x0600C743 RID: 51011
			[DllImport("bcrypt.dll")]
			internal static extern BCryptNative.ErrorCode BCryptEncrypt(SafeBCryptKeyHandle hKey, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbInput, int cbInput, [In] [Out] ref BCryptNative.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO pPaddingInfo, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbIV, int cbIV, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbOutput, int cbOutput, out int pcbResult, int dwFlags);

			// Token: 0x0600C744 RID: 51012
			[DllImport("bcrypt.dll")]
			internal static extern BCryptNative.ErrorCode BCryptFinishHash(SafeBCryptHashHandle hHash, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbOutput, int cbOutput, int dwFlags);

			// Token: 0x0600C745 RID: 51013
			[DllImport("bcrypt.dll")]
			internal static extern BCryptNative.ErrorCode BCryptGenRandom(SafeBCryptAlgorithmHandle hAlgorithm, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbBuffer, int cbBuffer, int dwFlags);

			// Token: 0x0600C746 RID: 51014
			[DllImport("bcrypt.dll", EntryPoint = "BCryptGetProperty")]
			internal static extern BCryptNative.ErrorCode BCryptGetAlgorithmProperty(SafeBCryptAlgorithmHandle hObject, [MarshalAs(UnmanagedType.LPWStr)] string pszProperty, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbOutput, int cbOutput, [In] [Out] ref int pcbResult, int flags);

			// Token: 0x0600C747 RID: 51015
			[DllImport("bcrypt.dll", EntryPoint = "BCryptGetProperty")]
			internal static extern BCryptNative.ErrorCode BCryptGetHashProperty(SafeBCryptHashHandle hObject, [MarshalAs(UnmanagedType.LPWStr)] string pszProperty, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbOutput, int cbOutput, [In] [Out] ref int pcbResult, int flags);

			// Token: 0x0600C748 RID: 51016
			[DllImport("bcrypt.dll")]
			internal static extern BCryptNative.ErrorCode BCryptHashData(SafeBCryptHashHandle hHash, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbInput, int cbInput, int dwFlags);

			// Token: 0x0600C749 RID: 51017
			[DllImport("bcrypt.dll")]
			internal static extern BCryptNative.ErrorCode BCryptImportKey(SafeBCryptAlgorithmHandle hAlgorithm, IntPtr hImportKey, [MarshalAs(UnmanagedType.LPWStr)] string pszBlobType, out SafeBCryptKeyHandle phKey, [In] [Out] IntPtr pbKeyObject, int cbKeyObject, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbInput, int cbInput, int dwFlags);

			// Token: 0x0600C74A RID: 51018
			[DllImport("bcrypt.dll")]
			internal static extern BCryptNative.ErrorCode BCryptOpenAlgorithmProvider(out SafeBCryptAlgorithmHandle phAlgorithm, [MarshalAs(UnmanagedType.LPWStr)] string pszAlgId, [MarshalAs(UnmanagedType.LPWStr)] string pszImplementation, BCryptNative.AlgorithmProviderOptions dwFlags);

			// Token: 0x0600C74B RID: 51019
			[DllImport("bcrypt.dll", EntryPoint = "BCryptSetProperty")]
			internal static extern BCryptNative.ErrorCode BCryptSetAlgorithmProperty(SafeBCryptAlgorithmHandle hObject, [MarshalAs(UnmanagedType.LPWStr)] string pszProperty, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbInput, int cbInput, int dwFlags);

			// Token: 0x0600C74C RID: 51020
			[DllImport("bcrypt.dll", EntryPoint = "BCryptSetProperty")]
			internal static extern BCryptNative.ErrorCode BCryptSetHashProperty(SafeBCryptHashHandle hObject, [MarshalAs(UnmanagedType.LPWStr)] string pszProperty, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbInput, int cbInput, int dwFlags);
		}

		// Token: 0x02001FEE RID: 8174
		// (Invoke) Token: 0x0600C74E RID: 51022
		[SecurityCritical]
		private delegate BCryptNative.ErrorCode BCryptPropertyGetter<T>(T hObject, string pszProperty, byte[] pbOutput, int cbOutput, ref int pcbResult, int dwFlags) where T : SafeHandle;

		// Token: 0x02001FEF RID: 8175
		// (Invoke) Token: 0x0600C752 RID: 51026
		[SecurityCritical]
		private delegate BCryptNative.ErrorCode BCryptPropertySetter<T>(T hObject, string pszProperty, byte[] pbInput, int cbInput, int dwFlags) where T : SafeHandle;
	}
}
