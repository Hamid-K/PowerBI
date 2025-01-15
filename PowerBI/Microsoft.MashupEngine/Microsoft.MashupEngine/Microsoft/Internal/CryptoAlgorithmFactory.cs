using System;
using System.Security.Cryptography;
using Microsoft.Mashup.Security;
using Microsoft.Mashup.Security.Cryptography;

namespace Microsoft.Internal
{
	// Token: 0x0200018F RID: 399
	internal class CryptoAlgorithmFactory
	{
		// Token: 0x060007CA RID: 1994 RVA: 0x0000ED70 File Offset: 0x0000CF70
		public static SHA256 CreateSHA256Algorithm()
		{
			return SHA256CryptoProvider.Create();
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0000ED77 File Offset: 0x0000CF77
		public static HMAC CreateHMACSHA256Algorithm()
		{
			if (CryptoAlgorithmFactory.hmacSHA256AlgorithmFactory == null)
			{
				CryptoAlgorithmFactory.hmacSHA256AlgorithmFactory = CryptoConfig2.CreateFactoryFromName("HMACSHA256Cng");
			}
			return (HMAC)CryptoAlgorithmFactory.hmacSHA256AlgorithmFactory();
		}

		// Token: 0x0400049F RID: 1183
		private static Func<object> hmacSHA256AlgorithmFactory;
	}
}
