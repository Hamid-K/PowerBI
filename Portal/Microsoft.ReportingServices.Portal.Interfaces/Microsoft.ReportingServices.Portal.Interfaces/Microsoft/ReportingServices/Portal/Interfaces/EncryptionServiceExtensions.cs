using System;

namespace Microsoft.ReportingServices.Portal.Interfaces
{
	// Token: 0x02000080 RID: 128
	public static class EncryptionServiceExtensions
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x00004910 File Offset: 0x00002B10
		public static bool TryDecrypt(this IEncryptionService encryptionService, byte[] encryptedData, out byte[] unencryptedData)
		{
			unencryptedData = null;
			bool flag;
			try
			{
				unencryptedData = encryptionService.Decrypt(encryptedData);
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00004944 File Offset: 0x00002B44
		public static bool TryDecryptToString(this IEncryptionService encryptionService, string encryptedString, out string unencryptedString)
		{
			unencryptedString = null;
			bool flag;
			try
			{
				unencryptedString = encryptionService.DecryptToString(encryptedString);
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}
	}
}
