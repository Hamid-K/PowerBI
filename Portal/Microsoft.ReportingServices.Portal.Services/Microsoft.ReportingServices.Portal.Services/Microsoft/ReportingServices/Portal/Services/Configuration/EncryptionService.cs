using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Portal.Interfaces;

namespace Microsoft.ReportingServices.Portal.Services.Configuration
{
	// Token: 0x0200006B RID: 107
	internal sealed class EncryptionService : IEncryptionService
	{
		// Token: 0x0600032A RID: 810 RVA: 0x00015188 File Offset: 0x00013388
		public byte[] Encrypt(byte[] unencryptedData)
		{
			return CatalogEncryption.Instance.Encrypt(unencryptedData);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00015195 File Offset: 0x00013395
		public byte[] Encrypt(string unencryptedString)
		{
			return CatalogEncryption.Instance.Encrypt(unencryptedString);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000151A2 File Offset: 0x000133A2
		public string EncryptToString(string unencryptedString)
		{
			return CatalogEncryption.Instance.EncryptToString(unencryptedString);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000151AF File Offset: 0x000133AF
		public byte[] Decrypt(byte[] encryptedData)
		{
			return CatalogEncryption.Instance.Decrypt(encryptedData, true);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000151BD File Offset: 0x000133BD
		public string DecryptToString(string encryptedString)
		{
			return CatalogEncryption.Instance.DecryptToString(encryptedString);
		}
	}
}
