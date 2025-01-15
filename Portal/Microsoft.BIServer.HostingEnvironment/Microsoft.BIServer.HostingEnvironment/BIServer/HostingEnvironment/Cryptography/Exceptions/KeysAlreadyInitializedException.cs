using System;

namespace Microsoft.BIServer.HostingEnvironment.Cryptography.Exceptions
{
	// Token: 0x0200003D RID: 61
	public class KeysAlreadyInitializedException : CryptoException
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00005A9D File Offset: 0x00003C9D
		public KeysAlreadyInitializedException()
		{
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005AA5 File Offset: 0x00003CA5
		public KeysAlreadyInitializedException(string message)
			: base(message)
		{
		}
	}
}
