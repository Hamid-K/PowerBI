using System;

namespace Microsoft.BIServer.HostingEnvironment.Cryptography.Exceptions
{
	// Token: 0x0200003B RID: 59
	public class CannotValidateEncryptedDataException : CryptoException
	{
		// Token: 0x06000199 RID: 409 RVA: 0x00005A9D File Offset: 0x00003C9D
		public CannotValidateEncryptedDataException()
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005AA5 File Offset: 0x00003CA5
		public CannotValidateEncryptedDataException(string message)
			: base(message)
		{
		}
	}
}
