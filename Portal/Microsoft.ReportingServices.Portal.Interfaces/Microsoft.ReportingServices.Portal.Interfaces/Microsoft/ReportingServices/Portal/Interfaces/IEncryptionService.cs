using System;

namespace Microsoft.ReportingServices.Portal.Interfaces
{
	// Token: 0x02000081 RID: 129
	public interface IEncryptionService
	{
		// Token: 0x060003FA RID: 1018
		byte[] Encrypt(byte[] unencryptedData);

		// Token: 0x060003FB RID: 1019
		byte[] Encrypt(string unencryptedString);

		// Token: 0x060003FC RID: 1020
		string EncryptToString(string unencryptedString);

		// Token: 0x060003FD RID: 1021
		byte[] Decrypt(byte[] encryptedData);

		// Token: 0x060003FE RID: 1022
		string DecryptToString(string encryptedString);
	}
}
