using System;
using System.Security;

namespace Microsoft.BIServer.HostingEnvironment.Cryptography
{
	// Token: 0x02000036 RID: 54
	public interface ICrypto
	{
		// Token: 0x06000166 RID: 358
		string GetName();

		// Token: 0x06000167 RID: 359
		byte[] Encrypt(byte[] unprotectedData);

		// Token: 0x06000168 RID: 360
		byte[] Encrypt(byte[] unprotectedData, string tag);

		// Token: 0x06000169 RID: 361
		byte[] Encrypt(string unprotectedData);

		// Token: 0x0600016A RID: 362
		byte[] Encrypt(string unprotectedData, string tag);

		// Token: 0x0600016B RID: 363
		string EncryptToString(string unprotectedData);

		// Token: 0x0600016C RID: 364
		string EncryptToString(string unprotectedData, string tag);

		// Token: 0x0600016D RID: 365
		byte[] Decrypt(byte[] protectedData);

		// Token: 0x0600016E RID: 366
		byte[] Decrypt(byte[] protectedData, string tag);

		// Token: 0x0600016F RID: 367
		string DecryptToString(string protectedData);

		// Token: 0x06000170 RID: 368
		string DecryptToString(string protectedData, string tag);

		// Token: 0x06000171 RID: 369
		string DecryptToString(byte[] protectedData);

		// Token: 0x06000172 RID: 370
		string DecryptToString(byte[] protectedData, string tag);

		// Token: 0x06000173 RID: 371
		SecureString DecryptToSecureString(string protectedData);

		// Token: 0x06000174 RID: 372
		SecureString DecryptToSecureString(string protectedData, string tag);

		// Token: 0x06000175 RID: 373
		SecureString DecryptToSecureString(byte[] protectedData);

		// Token: 0x06000176 RID: 374
		SecureString DecryptToSecureString(byte[] protectedData, string tag);
	}
}
