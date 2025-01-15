using System;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000167 RID: 359
	internal static class Cng
	{
		// Token: 0x06001074 RID: 4212 RVA: 0x000401C4 File Offset: 0x0003E3C4
		public static SafeAlgorithmHandle BCryptOpenAlgorithmProvider(string pszAlgId, string pszImplementation, Cng.OpenAlgorithmProviderFlags dwFlags)
		{
			SafeAlgorithmHandle safeAlgorithmHandle;
			Interop.BCrypt.NTSTATUS ntstatus = Interop.BCrypt.BCryptOpenAlgorithmProvider(out safeAlgorithmHandle, pszAlgId, pszImplementation, (int)dwFlags);
			if (ntstatus != Interop.BCrypt.NTSTATUS.STATUS_SUCCESS)
			{
				throw LogHelper.LogExceptionMessage(Cng.CreateCryptographicException(ntstatus));
			}
			return safeAlgorithmHandle;
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x000401EC File Offset: 0x0003E3EC
		public static void SetCipherMode(this SafeAlgorithmHandle hAlg, string cipherMode)
		{
			Interop.BCrypt.NTSTATUS ntstatus = Interop.BCrypt.BCryptSetProperty(hAlg, "ChainingMode", cipherMode, (cipherMode.Length + 1) * 2, 0);
			if (ntstatus != Interop.BCrypt.NTSTATUS.STATUS_SUCCESS)
			{
				throw LogHelper.LogExceptionMessage(Cng.CreateCryptographicException(ntstatus));
			}
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00040220 File Offset: 0x0003E420
		private static Exception CreateCryptographicException(Interop.BCrypt.NTSTATUS ntStatus)
		{
			return ((int)(ntStatus | (Interop.BCrypt.NTSTATUS)16777216U)).ToCryptographicException();
		}

		// Token: 0x04000614 RID: 1556
		public const string BCRYPT_AES_ALGORITHM = "AES";

		// Token: 0x04000615 RID: 1557
		public const string BCRYPT_CHAIN_MODE_GCM = "ChainingModeGCM";

		// Token: 0x0200027A RID: 634
		[Flags]
		public enum OpenAlgorithmProviderFlags
		{
			// Token: 0x04000B45 RID: 2885
			NONE = 0,
			// Token: 0x04000B46 RID: 2886
			BCRYPT_ALG_HANDLE_HMAC_FLAG = 8
		}
	}
}
