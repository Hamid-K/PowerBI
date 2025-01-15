using System;

namespace Microsoft.BIServer.Configuration.Key
{
	// Token: 0x02000037 RID: 55
	public interface IKeyRepository : IDisposable
	{
		// Token: 0x060001D9 RID: 473
		byte[] GetEncryptedSymmetricKey();
	}
}
