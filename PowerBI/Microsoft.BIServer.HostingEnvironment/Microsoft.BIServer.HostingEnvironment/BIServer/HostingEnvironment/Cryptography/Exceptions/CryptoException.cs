using System;

namespace Microsoft.BIServer.HostingEnvironment.Cryptography.Exceptions
{
	// Token: 0x0200003C RID: 60
	public class CryptoException : Exception
	{
		// Token: 0x0600019B RID: 411 RVA: 0x00004B88 File Offset: 0x00002D88
		public CryptoException(string message)
			: base(message)
		{
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005AAE File Offset: 0x00003CAE
		public CryptoException(Exception ex, string message)
			: base(message, ex)
		{
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00004B91 File Offset: 0x00002D91
		public CryptoException()
		{
		}

		// Token: 0x0200006E RID: 110
		public class TagMissing : CryptoException
		{
			// Token: 0x06000222 RID: 546 RVA: 0x00005AA5 File Offset: 0x00003CA5
			public TagMissing(string message)
				: base(message)
			{
			}
		}
	}
}
