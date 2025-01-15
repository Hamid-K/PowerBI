using System;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x02000010 RID: 16
	internal interface IEncryptionService
	{
		// Token: 0x0600003B RID: 59
		string Encrypt(string value);

		// Token: 0x0600003C RID: 60
		string Decrypt(string value);
	}
}
