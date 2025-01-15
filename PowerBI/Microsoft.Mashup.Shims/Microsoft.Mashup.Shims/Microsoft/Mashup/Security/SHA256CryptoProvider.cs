using System;
using System.Security.Cryptography;

namespace Microsoft.Mashup.Security
{
	// Token: 0x02000010 RID: 16
	public static class SHA256CryptoProvider
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000022AB File Offset: 0x000004AB
		public static SHA256 Create()
		{
			return new SHA256CryptoServiceProvider();
		}
	}
}
