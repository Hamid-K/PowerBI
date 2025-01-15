using System;
using System.Security.Cryptography;
using Microsoft.Mashup.Security;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B4C RID: 6988
	public sealed class UserEncryptedPersistentCache : EncryptedPersistentCache
	{
		// Token: 0x0600AED2 RID: 44754 RVA: 0x0023CA54 File Offset: 0x0023AC54
		public UserEncryptedPersistentCache(PersistentCache cache, SymmetricAlgorithm algorithm, string encryptionKeyPathName)
			: base(cache, algorithm, encryptionKeyPathName)
		{
		}

		// Token: 0x0600AED3 RID: 44755 RVA: 0x0023CA5F File Offset: 0x0023AC5F
		protected override byte[] UnencryptEncryptionKey(byte[] encryptedEncryptionKey)
		{
			return UserProtectedDataServices.Unprotect(encryptedEncryptionKey, UserEncryptedPersistentCache.dataExplorerApplicationIdentifer);
		}

		// Token: 0x0600AED4 RID: 44756 RVA: 0x0023CA6C File Offset: 0x0023AC6C
		protected override byte[] EncryptEncryptionKey(byte[] encryptionKey)
		{
			return UserProtectedDataServices.Protect(encryptionKey, UserEncryptedPersistentCache.dataExplorerApplicationIdentifer);
		}

		// Token: 0x04005A21 RID: 23073
		private static readonly byte[] dataExplorerApplicationIdentifer = new byte[]
		{
			221, 110, 80, 24, 24, 30, 146, 229, 223, 163,
			203, 186, 135, 29, 151, 80, 41, 54, 69, 232,
			137, 237, 208, 124, 114, 25, 52, 103, 223, 127,
			124, 192
		};
	}
}
