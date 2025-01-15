using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x02000011 RID: 17
	public sealed class EncryptionService : IEncryptionService
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000028F4 File Offset: 0x00000AF4
		public string Encrypt(string value)
		{
			return CatalogEncryption.Instance.EncryptToString(value);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002901 File Offset: 0x00000B01
		public string Decrypt(string value)
		{
			return CatalogEncryption.Instance.DecryptToString(value);
		}
	}
}
