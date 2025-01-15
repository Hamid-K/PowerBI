using System;

namespace Microsoft.BIServer.HostingEnvironment.Cryptography.Exceptions
{
	// Token: 0x0200003E RID: 62
	public class SymmetricKeyNotInitializedException : CryptoException
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00005AB8 File Offset: 0x00003CB8
		public SymmetricKeyNotInitializedException()
			: base("Symmetric key not initialized")
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005AA5 File Offset: 0x00003CA5
		public SymmetricKeyNotInitializedException(string message)
			: base(message)
		{
		}
	}
}
