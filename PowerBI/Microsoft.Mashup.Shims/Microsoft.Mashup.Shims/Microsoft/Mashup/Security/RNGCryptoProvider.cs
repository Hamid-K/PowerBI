using System;
using System.Security.Cryptography;

namespace Microsoft.Mashup.Security
{
	// Token: 0x0200000F RID: 15
	public static class RNGCryptoProvider
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000022A4 File Offset: 0x000004A4
		public static RandomNumberGenerator Create()
		{
			return new RNGCryptoServiceProvider();
		}
	}
}
